using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    

public class SaveData : MonoBehaviour
{

    // [SerializeField] private PotionData _PotionData = new PotionData();

    // public void SaveIntoJson()
    // {
    //     string potion = JsonUtility.ToJson(_PotionData, true);
    //     System.IO.File.WriteAllText(Application.dataPath + "/PotionData.json", potion);
    // }

    
    
    MainData mainData = new MainData();
    
    public void SaveIntoJson()
    {
        
        
        // mainData.email = new Email();
        
        // mainData.email.from = "empty";
        // mainData.email.to = "empty";
        
        string configFile = JsonUtility.ToJson(mainData, true);
        System.IO.File.WriteAllText(Application.dataPath + "/config.json", configFile);
    }

}

// [System.Serializable]
// public class MainData
// {
//     public string videoFolder;
//     public Email email = new Email();
// }

// [System.Serializable]
// public class Email
// {
//     public string da;
//     public string soggetto;
//     public string descrizione;
//     public string SMTP;
//     public string password;
// }


/////////////////////////////////////////////////////////////////////


// [System.Serializable]
// public class PotionData
// {
//     public string potion_name;
//     public int value;
//     public List<Effect> effect = new List<Effect>();
// }

// [System.Serializable]
// public class Effect
// {
//     public string name;
//     public string desc;
// }
