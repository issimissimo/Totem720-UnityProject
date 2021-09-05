using System.Collections.Generic;
using System;

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


    public static MainData data;


    /////////////////////////////////
    /// main
    /////////////////////////////////
    [Serializable]
    public class MainData
    {
        public string videoFolder;
        public string scenario;
        public string squadra;
        public Email email = new Email();
    }




    /////////////////////////////////
    /// email
    /////////////////////////////////
    [Serializable]
    public class Email
    {
        public string da;
        public string soggetto;
        public string descrizione;
        public string SMTP;
        public string password;
    }
}







