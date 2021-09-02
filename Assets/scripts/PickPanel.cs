using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickPanel : MonoBehaviour
{
    [SerializeField] GameObject panelToOpen;
    [SerializeField] UiManager uiManager;

    public void Open(){
        uiManager.ShowPanel(panelToOpen);
    }
}
