using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Enum to identify card types
public enum CardType
{
    Apple, Banana, Cherry, Dragon, Eagle, Fire, Gem, Heart, Ice, Joker
}


// this script will detect all the card and assin them the name according to the length
public class detecting_the_card_and_assining_the_name : MonoBehaviour
{
    [Header("All card references in the scene")]
    public List<GameObject> cards;
    public int number_of_card_in_the_scene;

    [Header("Card assignment settings")]
    [SerializeField] private List<Sprite> frount_face_cards; // Assign unique sprites here

    private List<(Sprite sprite, CardType type)> pairedCards;

    void Awake()
    {
        {
            // 1. Find all cards with the tag "card"
            cards = GameObject.FindGameObjectsWithTag("card").ToList();
            number_of_card_in_the_scene = cards.Count;

            // 2. Validation checks
            if (number_of_card_in_the_scene % 2 != 0)
            {
                Debug.LogError("⚠ Number of cards should be even.");
                return;
            }

            if (frount_face_cards.Count * 2 != number_of_card_in_the_scene)
            {
                Debug.LogError("⚠ You need exactly half as many unique sprites as the number of cards.");
                return;
            }

            // 3. Create random pairs (each sprite assigned to 2 cards)
            pairedCards = new List<(Sprite, CardType)>();
            for (int i = 0; i < frount_face_cards.Count; i++)
            {
                CardType type = (CardType)i;
                pairedCards.Add((frount_face_cards[i], type));
                pairedCards.Add((frount_face_cards[i], type));
            }

            // 4. Shuffle the pair list randomly
            pairedCards = pairedCards.OrderBy(_ => Random.value).ToList();

            // 5. Assign to cards
            AssignSpritesAndTypesRandomly();
        }

        void AssignSpritesAndTypesRandomly()
        {
            for (int i = 0; i < cards.Count; i++)
            {
                GameObject card = cards[i];
                var (sprite, type) = pairedCards[i];

                // Find child with SpriteRenderer (usually the front face)
                Transform faceChild = card.GetComponentsInChildren<Transform>()
                    .FirstOrDefault(child => child != card.transform && child.GetComponent<SpriteRenderer>() != null);

                if (faceChild == null)
                {
                    Debug.LogWarning($"⚠ No front face SpriteRenderer found in children of {card.name}");
                    continue;
                }

                // Assign the sprite
                SpriteRenderer sr = faceChild.GetComponent<SpriteRenderer>();
                sr.sprite = sprite;

                // Assign the enum to the card's info holder
                card_info_holder identity = card.GetComponent<card_info_holder>();
                if (identity == null)
                    identity = card.AddComponent<card_info_holder>();

                identity.cardType = type;

                //  Ensure card_flip_checker is attached
                if (card.GetComponent<card_flip_checker>() == null)
                    card.AddComponent<card_flip_checker>();

                Debug.Log($"🃏 {card.name} assigned Type: {type}");
            }

            Debug.Log("✅ All cards assigned randomly with matching pairs!");
        }
    }

    
}