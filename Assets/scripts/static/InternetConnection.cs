using System.Collections;
using UnityEngine;
using System;
using UnityEngine.Networking;


public class InternetConnection : MonoBehaviour
{
    
    public static InternetConnection instance;

    private void Awake() {
        instance = this;
    }
    
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
                if (result != null) result(true);
            }
        }
    }

    public void Check(ErrorManager.TYPE errorType, Action<bool> result = null)
    {
        StartCoroutine(TryToCheckDefaultUrl(errorType, result));
    }
}



