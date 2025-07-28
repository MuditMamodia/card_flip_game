using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class overall_score_and_complection_checker : MonoBehaviour
{
    [Header("complection indicator")]
    [SerializeField] private GameObject complection_indicator_level_1;
    [SerializeField] private GameObject complection_indicator_level_2;
    [SerializeField] private GameObject complection_indicator_level_3;

    [Header("combined score refrence")]
    [SerializeField] private TextMeshProUGUI score_text;
    int levels_id;
    public int overall_score;

   

    private void Start()
    {
        overall_score = 0;

        for (int i = 0; i < 3; i++) // assuming 3 levels
        {
            SavedData data = Save_manager.Load_saved_data(i);
            if (data != null)
            {
                overall_score += data.total_score[i]; // each file contains one score at index i
            }
        }

        score_text.text = overall_score.ToString();

        // Activate completion indicators based on scores
        if (Save_manager.Load_saved_data(0) != null) complection_indicator_level_1.SetActive(true);
        if (Save_manager.Load_saved_data(1) != null) complection_indicator_level_2.SetActive(true);
        if (Save_manager.Load_saved_data(2) != null) complection_indicator_level_3.SetActive(true);
    }
}
