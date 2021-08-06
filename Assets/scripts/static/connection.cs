using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class connection
{
    public static IEnumerator checkInternet(Action<bool> action = null)
    {
        WWW www = new WWW("http://google.com");
        yield return www;
        if (www.error != null)
        {

            ErrorManager.instance.ShowError("No Internet cooection");

            if (action != null) action(false);
        }
        else
        {
            if (action != null) action(true);
        }
    }
}
