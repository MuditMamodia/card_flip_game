using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class score_and_mach_checker : MonoBehaviour
{
    [Header("score counter")]
    public int total_score;
    public TextMeshProUGUI tmp;// score visula representative 
    public TextMeshProUGUI tmptwo;// score visula representative 

    [Header("Script reference")]
    public card_flip_checker[] cfc;
    public card_info_holder[] cih;
    public mouse_click_to_detect_the_card mctdtc;


    [Header("event for audio ")]
    [SerializeField] private UnityEvent audio_eventplay;
    [SerializeField] private UnityEvent audio_for_matching;


    [Header("game over checker setting")]
    public int count_for_score_andquit;
    public GameObject cards_holder;
    public GameObject ui_canvas_while_playing_game;
    public GameObject UI_canvs_complection_of_game;


    // Public method to check if two flipped cards match
    public void mached_chekcer()
    {
        //Debug.LogError("checker started");

        // finding all card_flip_checker and card_info_holder in scene (even inactive)
        cfc = Resources.FindObjectsOfTypeAll<card_flip_checker>();
        cih = Resources.FindObjectsOfTypeAll<card_info_holder>();
        mctdtc = FindObjectOfType<mouse_click_to_detect_the_card>();


        List<card_flip_checker> flippedCards = new List<card_flip_checker>();
        List<card_info_holder> flippedInfo = new List<card_info_holder>();

        // Step 1: Collect all cards with frount_side == true and not already matched
        for (int i = 0; i < cfc.Length; i++)
        {
            if (cfc[i].frount_side && !cfc[i].mached)
            {
                flippedCards.Add(cfc[i]);
                flippedInfo.Add(cih[i]);
            }
        }

        // Step 2: If exactly 2 cards are flipped, compare them
        if (flippedCards.Count == 2)
        {
            if (flippedInfo[0].cardType == flippedInfo[1].cardType)
            {
                // Match found: increase score and mark cards as matched
                total_score += 1;
                tmp.text = total_score.ToString();
                tmptwo.text = total_score.ToString();
                audio_for_matching.Invoke();
                flippedCards[0].mached = true;
                flippedCards[1].mached = true;
                gamecompleted();
                //  Reset flipped card count for future checks
                mctdtc.flipped_card_count = 0;
                Debug.Log($"Match Found! Card Type: {flippedInfo[0].cardType} | Score: {total_score}");
            }
            else
            {
                // No match: flip the cards back after delay
                StartCoroutine(flippingback_delaytime(flippedCards));
                Debug.Log($" No Match! {flippedInfo[0].cardType} ≠ {flippedInfo[1].cardType}");
            }
        }
        else
        {
            Debug.Log(" Waiting but nothing will happen");
        }
    }

    // Coroutine to flip unmatched cards back after 1 second
    IEnumerator flippingback_delaytime(List<card_flip_checker> flippedCards)
    {
        yield return new WaitForSeconds(1);
        audio_eventplay.Invoke();
        flippedCards[0].FlipToBack();
        flippedCards[1].FlipToBack();
        mctdtc.flipped_card_count = 0;
    }


    public void gamecompleted()
    {

        // Count how many cards are matched
        count_for_score_andquit = 0;
        foreach (card_flip_checker card in cfc)
        {
            if (card.mached)
            {
                count_for_score_andquit++;
            }
        }

        // Check if all cards are matched
        if (count_for_score_andquit == cfc.Length)
        {
            StartCoroutine(gamecomplection_delaytime());
        }

    }

    IEnumerator gamecomplection_delaytime()
    {
        yield return new WaitForSeconds(1.5f);

        // All cards are matched, game is complete
        ui_canvas_while_playing_game.SetActive(false);
        cards_holder.SetActive(false);
        UI_canvs_complection_of_game.SetActive(true);
    }

}


