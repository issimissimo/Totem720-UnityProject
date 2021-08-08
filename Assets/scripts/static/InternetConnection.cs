using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class InternetConnection
{
    public static IEnumerator check(Action<bool> action = null)
    {
        WWW www = new WWW("http://google.com");
        yield return www;
        if (www.error != null)
        {

            ErrorManager.instance.ShowError("No Internet connection");

            if (action != null) action(false);
        }
        else
        {
            if (action != null) action(true);
        }
    }
}
