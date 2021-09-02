using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading.Tasks;
using UnityEngine.Networking;


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


    public static async void newCheck(Action<bool> action = null)
    {
        UnityWebRequest www = new UnityWebRequest("http://google.com");
        while (www.isDone)
        {
            await Task.Yield();
        }
        if (www.error == null){
            if (action != null) action(true);
        }
        else{
            ErrorManager.instance.ShowError("No Internet connection");
            if (action != null) action(false);
        }
    }
}
