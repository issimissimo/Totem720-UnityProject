using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading.Tasks;
using UnityEngine.Networking;


public class InternetConnection : MonoBehaviour
{
    
    public static InternetConnection instance;

    private void Awake() {
        instance = this;
    }
    
    // public static IEnumerator check(Action<bool> action = null)
    // {
    //     WWW www = new WWW("http://google.com");
    //     yield return www;
    //     if (www.error != null)
    //     {

    //         ErrorManager.instance.ShowError("No Internet connection");

    //         if (action != null) action(false);
    //     }
    //     else
    //     {
    //         if (action != null) action(true);
    //     }
    // }



    private IEnumerator TryToCheckDefaultUrl(ErrorManager.TYPE errorType, Action<bool> result)
    {
        UnityWebRequest request = UnityWebRequest.Get("http://unity3d.com/");
        {
            yield return request.SendWebRequest();

            if (request.isNetworkError) // Error
            {
                ErrorManager.instance.ShowError(errorType, "Non c'è accesso ad Internet");
                if (result != null) result(false);
            }
            else // Success
            {
                Debug.Log("SUCCESS!!!!!");
                if (result != null) result(true);
            }
        }
    }




    public void Check(ErrorManager.TYPE errorType, Action<bool> result = null)
    {
        StartCoroutine(TryToCheckDefaultUrl(errorType, result));
        
        // UnityWebRequest www = new UnityWebRequest("https://google.com");
        // while (!www.isDone)
        // {
        //     Debug.Log(www.isDone);
        //     await Task.Yield();
        // }

        // Debug.Log("YEPPA!!!" + www.result);

        // if (www.error == null)
        // {
        //     Debug.Log("---------- YESSSSSSSSSSSSS");
        //     if (action != null) action(true);
        // }
        // else
        // {
        //     Debug.Log("---------- NOOOOOOOOOOOOOOOO");
        //     ErrorManager.instance.ShowError(errorType, "No Internet connection");
        //     if (action != null) action(false);
        // }


    }
}



