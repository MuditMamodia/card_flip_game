using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class card_flip_checker : MonoBehaviour
{
    // Tracks whether the front or back side is currently visible
    public bool frount_side;
    public bool backside_side;

    // Indicates whether this card has already been matched
    public bool mached;

    [Header("Flip Settings")]
    [SerializeField] private float flipDuration = 0.3f;// Duration for flip animation

   

    // Flip from back (180° Y) to front (0° Y)
    public void FlipToFront()
    {
        if (!mached)
        {
            StopAllCoroutines();
            StartCoroutine(RotateY(180f, 0f));
            frount_side = true;
            backside_side = false;
        }
        
    }

    // Flip from front (0° Y) to back (180° Y)
    public void FlipToBack()
    {
        if (!mached)
        {
            StopAllCoroutines();
            StartCoroutine(RotateY(0f, 180f));
            frount_side = false;
            backside_side = true;
        }
        
    }

    // Coroutine to smoothly rotate the card on the Y-axis
    private IEnumerator RotateY(float fromY, float toY)
    {
        float elapsed = 0f;
        float startY = fromY;
        float endY = toY;

        // Gradually interpolate from startY to endY over flipDuration
        while (elapsed < flipDuration)
        {
            float currentY = Mathf.Lerp(startY, endY, elapsed / flipDuration);
            transform.rotation = Quaternion.Euler(0f, currentY, 0f);
            elapsed += Time.deltaTime;
            yield return null;
        }
        // Ensure final rotation is set precisely
        transform.rotation = Quaternion.Euler(0f, toY, 0f);
    }
}

