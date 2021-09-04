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
        }
    }

    public static Squadra _SQUADRA
    {
        get { return _squadra; }
        set
        {
            _squadra = value;
            squadraIsDefined = true;
        }
    }

    public static bool scenarioIsDefined;
    public static bool squadraIsDefined;

    public static Dictionary<string, string> emailCredentials = new Dictionary<string, string>();

}




[Serializable]
public class MainData
{
    public string videoFolder;
    public Email email = new Email();
}




[Serializable]
public class Email
{
    public string da;
    public string soggetto;
    public string descrizione;
    public string SMTP;
    public string password;
}


