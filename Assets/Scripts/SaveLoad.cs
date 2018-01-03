using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;

public class SaveLoad { 
    public static void Save(int score)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/highScores.gd");
        bf.Serialize(file, score);
        file.Close();
    }
    public static int Load()
    {
        int score = 0;
        if (File.Exists(Application.persistentDataPath + "/highScores.gd"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/highScores.gd", FileMode.Open);
            score = (int)bf.Deserialize(file);
            file.Close();
        }
        return score;
    }
}
