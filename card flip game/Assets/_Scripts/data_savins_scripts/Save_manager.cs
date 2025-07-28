using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
public class Save_manager
{
    public static void save_level_data(score_and_mach_checker smc, int level_id)
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        string path = Application.persistentDataPath + $"/Level_Dta.mc{level_id}";

        FileStream stream = new FileStream(path, FileMode.Create);

        SavedData data = new SavedData(smc,level_id);

        binaryFormatter.Serialize(stream, data);
        stream.Close();

    }

    public static SavedData Load_saved_data(int level_id)
    {
        string path = Application.persistentDataPath + $"/Level_Dta.mc{level_id}";
        if (File.Exists(path))
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            SavedData datasaved = binaryFormatter.Deserialize(stream) as SavedData;
            stream.Close();

            return datasaved;
        }
        else
        {
            Debug.Log("save file not found" + path);
            return null;
        }
    }

}
