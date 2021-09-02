using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class FileManager : MonoBehaviour
{
    public static string defPath;
    private string fileExtension = ".mp4";

    public string GetFile(Globals.Scenario scenario, Globals.Squadra squadra, int videoNumber){


        
        return null;
    }

    public void List_Milan_Calciatori(Action<List<string>> callback)
    {
        GetFilesInFolder(defPath + "/Milan/Calciatori", callback);
    }

    public void List_Milan_Coppe(Action<List<string>> callback)
    {
        GetFilesInFolder(defPath + "/Milan/Coppe", callback);
    }

    public void List_Inter_Calciatori(Action<List<string>> callback)
    {
        GetFilesInFolder(defPath + "/Inter/Calciatori", callback);
    }

    public void List_Inter_Coppe(Action<List<string>> callback)
    {
        GetFilesInFolder(defPath + "/Inter/Coppe", callback);
    }



    public static bool CheckDirectory(string path, bool isBlocking = false)
    {
        if (Directory.Exists(path))
        {
            return true;
        }
        else
        {
            ErrorManager.instance.ShowError(path + " don't exist", isBlocking);
            return false;
        }
    }


    void GetFilesInFolder(string path, Action<List<string>> callback = null)
    {
        if (Directory.Exists(path))
        {
            DirectoryInfo dir = new DirectoryInfo(path);
            FileInfo[] info = dir.GetFiles("*" + fileExtension);

            if (info.Length == 0)
            {
                ErrorManager.instance.ShowError("Folder is empty");
            }
            else
            {
                List<string> videos = new List<string>();

                foreach (FileInfo fullFileName in info)
                {
                    string fileName = fullFileName.ToString().Remove(0, defPath.Length + 1);
                    fileName = fileName.Substring(0, fileName.Length - 4);
                    videos.Add(fileName);
                }
                videos.Sort();

                if (callback != null) callback(videos);
            }
        }
    }
}
