using System;
using UnityEngine;

public static class Globals
{
    public enum Scenario { Coppe, Calciatori }

    public enum Squadra
    { Inter, Milan, Inter_Milan }

    private static Scenario _scenario;
    private static Squadra _squadra;

    public static Scenario _SCENARIO
    {
        get { return _scenario; }
        set
        {
            _scenario = value;
            scenarioIsDefined = true;
            data.scenario = value.ToString();
        }
    }

    public static Squadra _SQUADRA
    {
        get { return _squadra; }
        set
        {
            _squadra = value;
            squadraIsDefined = true;
            data.squadra = value.ToString();
        }
    }

    public static bool scenarioIsDefined;
    public static bool squadraIsDefined;


    /// screenshot
    public static string screenshotFolder = Application.persistentDataPath;
    public static string screenshotName = "screenshot";


    public static MainData data;


    /////////////////////////////////
    /// main class
    /////////////////////////////////
    [Serializable]
    public class MainData
    {
        public string videoFolder;
        public string scenario;
        public string squadra;
        public Email email = new Email();
        public string stampante;
    }




    /////////////////////////////////
    /// email class
    /////////////////////////////////
    [Serializable]
    public class Email
    {
        public string da;
        public string soggetto;
        public string oggetto;
        public string SMTP;
        public string porta;
        public string password;
    }
}







