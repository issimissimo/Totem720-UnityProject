using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class FileManager : MonoBehaviour
{
    Dictionary<Globals.Scenario, string> url_scenario = new Dictionary<Globals.Scenario, string>();
    Dictionary<Globals.Squadra, string> url_squadra = new Dictionary<Globals.Squadra, string>();
    public static string defPath;
    private string fileExtension = ".webm";


    private void Awake()
    {
        url_scenario[Globals.Scenario.Coppe] = "COPPE";
        url_scenario[Globals.Scenario.Calciatori] = "CALCIATORI";
        url_squadra[Globals.Squadra.Inter] = "INTER";
        url_squadra[Globals.Squadra.Milan] = "MILAN";
        url_squadra[Globals.Squadra.Inter_Milan] = "INTER-MILAN";
    }


    public string GetFile(Globals.Scenario scenario, Globals.Squadra squadra, int videoNumber)
    {
        string path = defPath + "/" + url_scenario[scenario] + "/" + url_squadra[squadra];

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
            else{
                return null;
            }
        }
        else
        {
            return null;
        }
    }




    // public static void CheckDirectory(string path, ErrorManager.TYPE errorType, Action<bool> result = null)
    // {
    //     if (Directory.Exists(path))
    //     {
    //         if (result != null) result(true);
    //     }
    //     else
    //     {
    //         ErrorManager.instance.ShowError(errorType, "La cartella \n" + path + "\n non esiste");
    //         if (result != null) result(false);
    //     }
    // }

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
}
