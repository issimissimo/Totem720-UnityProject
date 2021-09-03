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
        set { _scenario = value; }
    }


    public static Squadra _SQUADRA
    {
        get { return _squadra; }
        set { _squadra = value; }
    }


}


