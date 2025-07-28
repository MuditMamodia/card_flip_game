using UnityEngine;


[System.Serializable]
public class SavedData
{
    public int[] total_score;
    public bool[] iscompleted;

    public SavedData(score_and_mach_checker ssmc, int level_id  )
    {

        total_score = new int[3];
        iscompleted = new bool[3];

        total_score[level_id] = ssmc.total_score;
        iscompleted[level_id] = ssmc.level_completed;



    }
}
