using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif


public class card_info_holder : MonoBehaviour
{

    [Header("🔖 Card Type (Auto-assigned by script)")]
    [Tooltip("This CardType will be assigned automatically at runtime.")]

    [SerializeField] public CardType cardType;


#if UNITY_EDITOR
    // This ensures the Inspector repaints when the value is changed from code
    private void OnValidate()
    {
        EditorUtility.SetDirty(this);
    }
#endif


    

    //private void Start()
    //{
    //    Debug.Log(cardType.ToString());
    //}

}
