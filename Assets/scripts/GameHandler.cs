using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class GameHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // if (Input.GetKeyDown(KeyCode.Space)){
        //     Debug.Log("aaaaa");
        //     ScreenshotHandler.TakeScreenshot_Static(1080,1920);
        // }

        if (Keyboard.current[Key.Space].wasPressedThisFrame){
            Debug.Log("aaaaa");
            ScreenshotHandler.TakeScreenshot_Static(1080,1920);
        }
    }
}
