using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouse_click_to_detect_the_card : MonoBehaviour
{
    [Header("Detection Settings")]
    [SerializeField] private float circleRadius = 0.5f;// Radius of 2D circle used for card detection

    private Coroutine clickRoutine;// Coroutine for detecting mouse click


    [Header("card flip setting")]
    public card_flip_checker cfc;  // Reference to the flip script on the detected card
    public int flipped_card_count;         // Keeps track of how many cards are currently flipped

    public card_info_holder card;          // Reference to card info (for debug/info)

    private score_and_mach_checker smc;    // Reference to the match checking system


    // Find the score_and_mach_checker on scene load
    private void Awake()
    {
        smc = FindObjectOfType<score_and_mach_checker>();
    }

    // Start click detection when this component is enabled
    private void OnEnable()
    {
        clickRoutine = StartCoroutine(DetectMouseClick());
    }


    // Stop click detection when this component is disabled
    private void OnDisable()
    {
        if (clickRoutine != null)
            StopCoroutine(clickRoutine);
    }


    // Coroutine to detect mouse clicks and perform a 2D circle overlap
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

                if (hit != null )
                {
                    card = hit.GetComponent<card_info_holder>();
                   
                    if (card != null && flipped_card_count < 2)
                    {
                        Debug.Log($"🃏 2D Hit Card: {hit.gameObject.name} | Type: {card.cardType}");
                    }
                    else
                    {
                        Debug.Log(" click on something identety unidentifyed");
                    }
                    cfc = hit.GetComponent<card_flip_checker>();
                    if (cfc != null)
                    {
                        card_flipping();
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

    // shows the detection circle in Scene view
    private void OnDrawGizmosSelected()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector2 worldPos = Camera.main != null ? Camera.main.ScreenToWorldPoint(mousePos) : Vector2.zero;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(worldPos, circleRadius);
    }


    // Handles card flipping logic
    void card_flipping()
    {
        // Flip the card only if it's not already face up and limit to 2 flips
        if (!cfc.frount_side && flipped_card_count < 2)
        {
            cfc.frount_side = true;
            cfc.FlipToFront();
            flipped_card_count += 1;
            if (flipped_card_count == 2)
            {
                smc.mached_chekcer();
            }
        }
        //else if (cfc.frount_side)
        //{
        //    cfc.frount_side = false;
        //    cfc.FlipToBack();
        //    flipped_card_count -= 1;
        //}

        
    }
}