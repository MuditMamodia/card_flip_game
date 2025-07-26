using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouse_click_to_detect_the_card : MonoBehaviour
{
    [Header("Detection Settings")]
    [SerializeField] private float circleRadius = 0.5f;

    private Coroutine clickRoutine;


    [Header("card flip setting")]
    card_flip_checker cfc;
    private Transform selected_card;
    public float card_flip_speed;


    private void OnEnable()
    {
        clickRoutine = StartCoroutine(DetectMouseClick());
    }

    private void OnDisable()
    {
        if (clickRoutine != null)
            StopCoroutine(clickRoutine);
    }

    IEnumerator DetectMouseClick()
    {
        while (true)
        {
            if (Input.GetMouseButtonDown(0)) // Left click
            {
                Vector3 mousePos = Input.mousePosition;
                Vector2 worldPos = Camera.main.ScreenToWorldPoint(mousePos);

                // Cast a circle in 2D to detect cards
                Collider2D hit = Physics2D.OverlapCircle(worldPos, circleRadius);

                if (hit != null)
                {
                    card_info_holder card = hit.GetComponent<card_info_holder>();
                    cfc = hit.GetComponent<card_flip_checker>();
                    if (card != null)
                    {
                        Debug.Log($"🃏 2D Hit Card: {hit.gameObject.name} | Type: {card.cardType}");
                    }
                    else
                    {
                        Debug.Log("💨 Clicked something, but not a card (no card_info_holder).");
                    }
                }
                else
                {
                    Debug.Log("❌ Nothing hit with 2D circle check.");
                }
            }

            yield return null;
        }
    }

    // Optional: to visualize the circle in the editor
    private void OnDrawGizmosSelected()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector2 worldPos = Camera.main != null ? Camera.main.ScreenToWorldPoint(mousePos) : Vector2.zero;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(worldPos, circleRadius);
    }


    void card_flipping()
    {
        
    }
}