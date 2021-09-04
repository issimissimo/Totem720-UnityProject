using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    

public class SaveData : MonoBehaviour
{
    Globals.MainData mainData = new Globals.MainData();
    
    public void SaveIntoJson()
    {
        string configFile = JsonUtility.ToJson(mainData, true);
        System.IO.File.WriteAllText(Application.dataPath + "/config.json", configFile);
    }

}
