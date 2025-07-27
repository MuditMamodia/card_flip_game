using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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


    [Header("event for audioplaying")]
    [SerializeField] private UnityEvent card_flip_event;

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
            Vector2 worldPos = Vector2.zero;
            bool clicked = false;

            // PC: Mouse Click

            if (Input.GetMouseButtonDown(0))
            {
                worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                clicked = true;
            }


            if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
            {
                worldPos = Camera.main.ScreenToWorldPoint(Input.touches[0].position);
                clicked = true;
            }


            if (clicked)
            {
                Collider2D hit = Physics2D.OverlapCircle(worldPos, circleRadius);
                if (hit != null)
                {
                    card = hit.GetComponent<card_info_holder>();
                    cfc = hit.GetComponent<card_flip_checker>();

                    if (card != null && cfc != null && flipped_card_count < 2)
                    {
                        Debug.Log($"🃏 Card Clicked: {card.cardType}");
                        card_flipping();
                    }
                    else
                    {
                        Debug.Log("Card hit but conditions not met or unidentified.");
                    }
                }
                else
                {
                    Debug.Log("❌ Nothing hit at that point.");
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
            card_flip_event.Invoke();
            if (flipped_card_count == 2)
            {
                smc.mached_chekcer();
            }
        }
        else if (cfc.frount_side)
        {
            cfc.frount_side = false;
            cfc.FlipToBack();
            if (flipped_card_count > 0)
            {
                flipped_card_count -= 1;
            }
            
        }


    }
}