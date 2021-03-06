using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ErrorManager : MonoBehaviour
{
    public static ErrorManager instance;
    [SerializeField] GameObject panelError;
    [SerializeField] GameObject prefab;

    public enum TYPE { WARNING, ERROR, INFO, NONE };

    private int totalPrefabsNumber = 0;

    private List<GameObject> prefabs = new List<GameObject>();

    private void Awake()
    {
        instance = this;
        panelError.SetActive(false);
    }

    private void RefreshUI()
    {
        LayoutRebuilder.ForceRebuildLayoutImmediate(panelError.GetComponent<RectTransform>());
    }


    public void ShowError(TYPE type, string msg, string buttonText = null, Action buttonAction = null)
    {
        if (type != TYPE.NONE)
        {
            panelError.SetActive(true);

            GameObject go = Instantiate(prefab, panelError.transform);
            prefabs.Add(go);

            /// setup the prefab
            ErrorMessageSetup prefabPanel = go.GetComponent<ErrorMessageSetup>();
            prefabPanel.Setup(type, prefabs.Count - 1, msg, buttonText, buttonAction);

            totalPrefabsNumber++;

            RefreshUI();
        }
    }


    public void CloseErrorPrefabPanel(int prefabNumber)
    {
        Destroy(prefabs[prefabNumber]);
        totalPrefabsNumber--;

        /// hide the panel if no error panels are shown anymore
        if (totalPrefabsNumber == 0)
        {
            prefabs.Clear();
            panelError.SetActive(false);
        }
    }


    public void CloseAll(){
        foreach(GameObject go in prefabs){
            Destroy(go);
        }
        totalPrefabsNumber = 0;
        prefabs.Clear();
        panelError.SetActive(false);
    }
}
