using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class FileManager : MonoBehaviour
{
    // Dictionary<Globals.Scenario, string> url_scenario = new Dictionary<Globals.Scenario, string>();
    // Dictionary<Globals.Squadra, string> url_squadra = new Dictionary<Globals.Squadra, string>();
    private string fileExtension = ".webm";


    // private void Awake()
    // {
    //     url_scenario[Globals.Scenario.Coppe] = "COPPE";
    //     url_scenario[Globals.Scenario.Calciatori] = "CALCIATORI";
    //     url_squadra[Globals.Squadra.Inter] = "INTER";
    //     url_squadra[Globals.Squadra.Milan] = "MILAN";
    //     url_squadra[Globals.Squadra.Inter_Milan] = "INTER-MILAN";
    // }

    // private void Start() {
    //     print("//////////////////////");
    //     print(Globals.Scenario.Calciatori.ToString());
    //     print("//////////////////////");
    // }


    public void CkeckDefaultPath()
    {
        if (Globals.data.videoFolder != "" && Globals.data.videoFolder != null)
        {
            if (!CheckDirectory(Globals.data.videoFolder, ErrorManager.TYPE.WARNING))
            {
                StartCoroutine(CreateDefaultDirectories());
            }
        }
        else{
            ErrorManager.instance.ShowError(ErrorManager.TYPE.WARNING, "Nel file config.json non è stata specificata la cartella in cui ci sono i video");
        }
    }




    public void CheckConfigFile(Action<bool> callback)
    {
        string configFileName = "config.json";

        /// save the congig file with empty values
        if (!File.Exists(Application.persistentDataPath + "/" + configFileName))
        {
            Globals.MainData mainData = new Globals.MainData();
            string configFile = JsonUtility.ToJson(mainData, true);
            System.IO.File.WriteAllText(Application.persistentDataPath + "/" + configFileName, configFile);

            ErrorManager.instance.ShowError(ErrorManager.TYPE.ERROR, "Il file di configurazione config.json non esisteva, ed è stato creato nella cartella " + Application.persistentDataPath +
            ". Si prega di completarlo con tutti i parametri e riavviare l'applicazione");

            callback(false);
        }
        /// load the config file
        else
        {
            string filePath = System.IO.Path.Combine(Application.persistentDataPath, configFileName);
            string data = System.IO.File.ReadAllText(filePath);

            Globals.MainData mainData = JsonUtility.FromJson<Globals.MainData>(data);
            Globals.data = mainData;
            Debug.Log(Globals.data.videoFolder);

            callback(true);
        }
    }




    public string GetFile(Globals.Scenario scenario, Globals.Squadra squadra, int videoNumber)
    {
        // string path = Globals.data.videoFolder + "/" + url_scenario[scenario] + "/" + url_squadra[squadra];
        string path = Globals.data.videoFolder + "/" + scenario.ToString() + "/" + squadra.ToString();

        if (CheckDirectory(path, ErrorManager.TYPE.ERROR))
        {
            List<string> videos = GetFilesInFolder(path);
            if (videos != null)
            {
                if (videos.Count >= videoNumber - 1)
                {
                    return path + "/" + videos[videoNumber];
                }
                else
                {
                    ErrorManager.instance.ShowError(ErrorManager.TYPE.ERROR, "Il video selezionato non esiste");
                    return null;
                }
            }
            else
            {
                return null;
            }
        }
        else
        {
            return null;
        }
    }





    public static bool CheckDirectory(string path, ErrorManager.TYPE errorType)
    {
        if (Directory.Exists(path))
        {
            return true;
        }
        else
        {
            ErrorManager.instance.ShowError(errorType, "La cartella \n" + path + "\n non esiste");
            return false;
        }
    }





    private List<string> GetFilesInFolder(string path)
    {
        DirectoryInfo dir = new DirectoryInfo(path);
        FileInfo[] info = dir.GetFiles("*" + fileExtension);

        if (info.Length == 0)
        {
            ErrorManager.instance.ShowError(ErrorManager.TYPE.ERROR, "La cartella è vuota o non ci sono files compatibili");
            return null;
        }
        else
        {
            List<string> videos = new List<string>();

            foreach (FileInfo fullFileName in info)
            {
                string fileName = fullFileName.ToString().Remove(0, path.Length + 1);
                videos.Add(fileName);
            }
            videos.Sort();

            return videos;
        }
    }



    private IEnumerator CreateDefaultDirectories()
    {
        Directory.CreateDirectory(Globals.data.videoFolder);
        yield return new WaitForSeconds(0.5f);
        // Directory.CreateDirectory(Globals.data.videoFolder + "/" + url_scenario[Globals.Scenario.Coppe]);
        // Directory.CreateDirectory(Globals.data.videoFolder + "/" + url_scenario[Globals.Scenario.Calciatori]);
        // yield return new WaitForSeconds(0.5f);
        // Directory.CreateDirectory(Globals.data.videoFolder + "/" + url_scenario[Globals.Scenario.Coppe] + "/" + url_squadra[Globals.Squadra.Inter]);
        // Directory.CreateDirectory(Globals.data.videoFolder + "/" + url_scenario[Globals.Scenario.Coppe] + "/" + url_squadra[Globals.Squadra.Milan]);
        // Directory.CreateDirectory(Globals.data.videoFolder + "/" + url_scenario[Globals.Scenario.Coppe] + "/" + url_squadra[Globals.Squadra.Inter_Milan]);
        // Directory.CreateDirectory(Globals.data.videoFolder + "/" + url_scenario[Globals.Scenario.Calciatori] + "/" + url_squadra[Globals.Squadra.Inter]);
        // Directory.CreateDirectory(Globals.data.videoFolder + "/" + url_scenario[Globals.Scenario.Calciatori] + "/" + url_squadra[Globals.Squadra.Milan]);
        // Directory.CreateDirectory(Globals.data.videoFolder + "/" + url_scenario[Globals.Scenario.Calciatori] + "/" + url_squadra[Globals.Squadra.Inter_Milan]);

        Directory.CreateDirectory(Globals.data.videoFolder + "/" + Globals.Scenario.Coppe.ToString());
        Directory.CreateDirectory(Globals.data.videoFolder + "/" + Globals.Scenario.Calciatori.ToString());
        yield return new WaitForSeconds(0.5f);
        Directory.CreateDirectory(Globals.data.videoFolder + "/" + Globals.Scenario.Coppe.ToString() + "/" + Globals.Squadra.Inter.ToString());
        Directory.CreateDirectory(Globals.data.videoFolder + "/" + Globals.Scenario.Coppe.ToString() + "/" + Globals.Squadra.Milan.ToString());
        Directory.CreateDirectory(Globals.data.videoFolder + "/" + Globals.Scenario.Coppe.ToString() + "/" + Globals.Squadra.Inter_Milan.ToString());
        Directory.CreateDirectory(Globals.data.videoFolder + "/" + Globals.Scenario.Calciatori.ToString() + "/" + Globals.Squadra.Inter.ToString());
        Directory.CreateDirectory(Globals.data.videoFolder + "/" + Globals.Scenario.Calciatori.ToString() + "/" + Globals.Squadra.Milan.ToString());
        Directory.CreateDirectory(Globals.data.videoFolder + "/" + Globals.Scenario.Calciatori.ToString() + "/" + Globals.Squadra.Inter_Milan.ToString());

        ErrorManager.instance.ShowError(ErrorManager.TYPE.INFO, "La cartella è stata appena creata. Si prega di chiudere l'applicazione e caricare i video nelle cartelle specifiche");
    }
}
