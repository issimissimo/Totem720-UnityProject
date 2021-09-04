    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Diagnostics;
using System.ComponentModel.Design;

public class PrinterHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        string printerName = "Microsoft Print to PDF";
        string _filePath = "C:\test.jpg";
        string fullCommand = "rundll32 C:\\WINDOWS\\system32\\shimgvw.dll,ImageView_PrintTo " + "\"" + _filePath + "\"" + " " + "\"" + printerName + "\"";
        // PrintImage(fullCommand);
        // PrintScreen();
    }


    /// PROVA che in teoria dovrebbe funzionare
    void PrintImage(string _cmd)
    {
        try
        {
            UnityEngine.Debug.Log("YYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYY");
            Process myProcess = new Process();
            //myProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            myProcess.StartInfo.CreateNoWindow = true;
            myProcess.StartInfo.UseShellExecute = false;
            myProcess.StartInfo.FileName = "cmd.exe";
            myProcess.StartInfo.Arguments = "/c " + _cmd;
            myProcess.EnableRaisingEvents = true;
            myProcess.Start();
            myProcess.WaitForExit();
            UnityEngine.Debug.Log("OOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOO");


        }
        catch (Exception e)
        {
            UnityEngine.Debug.Log(e);
        }
    }



    /// PROVA CHE dovrebbe aprire la preview (ma che non funziona)
    void PrintFiles()
    {
        string path = "file:///C:\test.jpg";
        Process process = new Process();
        process.StartInfo.CreateNoWindow = false;
        process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
        process.StartInfo.UseShellExecute = true;
        process.StartInfo.FileName = path;
        process.StartInfo.Verb = "print";
        process.Start();
    }


/// PROVA altra
    public void PrintScreen()
    {
        System.Diagnostics.Process.Start("mspaint.exe", "C:\\test.png");
    }
}

