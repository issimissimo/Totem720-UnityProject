using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Diagnostics;

public class FileManager : MonoBehaviour
{
    private string fileExtension = ".webm";
    private string configFileName = "config.json";


    // public void CkeckDefaultPath()
    // {
    //     if (Globals.data.videoFolder != "" && Globals.data.videoFolder != null)
    //     {
    //         if (!CheckDirectory(Globals.data.videoFolder, ErrorManager.TYPE.WARNING))
    //         {
    //             StartCoroutine(CreateDefaultDirectories());
    //         }
    //     }
    //     else
    //     {
    //         ErrorManager.instance.ShowError(ErrorManager.TYPE.WARNING, "Nel file di configurazione non è stata specificata la cartella in cui ci sono i video");
    //     }
    // }

    public void CkeckDefaultPath()
    {
        if (Globals.data.videoFolder != "" && Globals.data.videoFolder != null)
        {
            StartCoroutine(CreateDefaultDirectories());
        }
        else
        {
            ErrorManager.instance.ShowError(ErrorManager.TYPE.WARNING, "Nel file di configurazione non è stata specificata la cartella in cui ci sono i video");
        }
    }



    public void CheckConfigFile(Action<bool> callback)
    {
        ///
        /// save the config file with empty values
        ///
        if (!File.Exists(Application.persistentDataPath + "/" + configFileName))
        {
            Globals.MainData mainData = new Globals.MainData();
            string configFile = JsonUtility.ToJson(mainData, true);
            print(configFile);
            System.IO.File.WriteAllText(Application.persistentDataPath + "/" + configFileName, configFile);

            ErrorManager.instance.ShowError(ErrorManager.TYPE.ERROR, "Il file di configurazione non esiste\n\n E' stato creato nella cartella\n" + Application.persistentDataPath +
            "\n\n Si prega di completarlo con tutti i parametri e riavviare l'applicazione");

            OpenFileForEdit(configFileName);
            callback(false);
        }
        ///
        /// load the config file
        ///
        else
        {
            string filePath = System.IO.Path.Combine(Application.persistentDataPath, configFileName);
            string data = System.IO.File.ReadAllText(filePath);

            try
            {
                Globals.MainData mainData = JsonUtility.FromJson<Globals.MainData>(data);
                Globals.data = mainData;

                if (Utils.ValidateString(Globals.data.scenario) && Utils.ValidateString(Globals.data.squadra))
                {
                    try
                    {
                        Globals._SCENARIO = (Globals.Scenario)System.Enum.Parse(typeof(Globals.Scenario), Globals.data.scenario);
                        Globals._SQUADRA = (Globals.Squadra)System.Enum.Parse(typeof(Globals.Squadra), Globals.data.squadra);

                        GameManager.instance.ShowInit();
                    }
                    catch (Exception e)
                    {
                        ErrorManager.instance.ShowError(ErrorManager.TYPE.WARNING, "Lo scenario e la squadra definiti nel file di configurazione non sono stati riconosciuti");
                    }
                }

                callback(true);
            }


            catch (Exception e)
            {
                ErrorManager.instance.ShowError(ErrorManager.TYPE.ERROR, e.ToString());
                callback(false);
            }
        }
    }


    public void UpdateConfigFile()
    {
        print("----------- UPDATE CONFIG FILE!!!!!!!!!!!! ---------");
        string configFile = JsonUtility.ToJson(Globals.data, true);
        print(configFile);
        System.IO.File.WriteAllText(Application.persistentDataPath + "/" + configFileName, configFile);
    }


    public void OpenFileForEdit(string fileName)
    {
        StartCoroutine(_OpenFileForEdit(fileName));
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
        bool newFolderIsCreated = false;
        newFolderIsCreated = CreateDirectoryIfNotExist(Globals.data.videoFolder);
        yield return new WaitForSeconds(0.25f);
        newFolderIsCreated = CreateDirectoryIfNotExist(Globals.data.videoFolder + "/" + Globals.Scenario.Coppe.ToString());
        newFolderIsCreated = CreateDirectoryIfNotExist(Globals.data.videoFolder + "/" + Globals.Scenario.Calciatori.ToString());
        yield return new WaitForSeconds(0.25f);
        newFolderIsCreated = CreateDirectoryIfNotExist(Globals.data.videoFolder + "/" + Globals.Scenario.Coppe.ToString() + "/" + Globals.Squadra.Inter.ToString());
        newFolderIsCreated = CreateDirectoryIfNotExist(Globals.data.videoFolder + "/" + Globals.Scenario.Coppe.ToString() + "/" + Globals.Squadra.Milan.ToString());
        newFolderIsCreated = CreateDirectoryIfNotExist(Globals.data.videoFolder + "/" + Globals.Scenario.Coppe.ToString() + "/" + Globals.Squadra.Inter_Milan.ToString());
        newFolderIsCreated = CreateDirectoryIfNotExist(Globals.data.videoFolder + "/" + Globals.Scenario.Calciatori.ToString() + "/" + Globals.Squadra.Inter.ToString());
        newFolderIsCreated = CreateDirectoryIfNotExist(Globals.data.videoFolder + "/" + Globals.Scenario.Calciatori.ToString() + "/" + Globals.Squadra.Milan.ToString());
        newFolderIsCreated = CreateDirectoryIfNotExist(Globals.data.videoFolder + "/" + Globals.Scenario.Calciatori.ToString() + "/" + Globals.Squadra.Inter_Milan.ToString());

        if (newFolderIsCreated)
        {
            ErrorManager.instance.ShowError(ErrorManager.TYPE.INFO, "Le cartelle sono state create\n Si prega di chiudere l'applicazione e caricare i video nelle cartelle specifiche");
        }

    }

    private bool CreateDirectoryIfNotExist(string path)
    {
        if (!CheckDirectory(path, ErrorManager.TYPE.NONE))
        {
            Directory.CreateDirectory(path);
            return true;
        }
        else
        {
            return false;
        }
    }


    private IEnumerator _OpenFileForEdit(string fileName)
    {
        yield return new WaitForSeconds(0.5f);
        Process.Start("notepad.exe", Application.persistentDataPath + "\\" + fileName);
    }
}
