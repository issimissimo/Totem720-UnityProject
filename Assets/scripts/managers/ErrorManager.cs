using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ErrorManager : MonoBehaviour
{
    public static ErrorManager instance;
    [SerializeField] GameObject panelError;
    [SerializeField] GameObject prefab;
 
    [SerializeField] Text textError;
    [SerializeField] GameObject closeButton;

    public enum TYPE { WARNING, ERROR };

    private int totalPrefabsNumber = 0;

    public List<GameObject> prefabs = new List<GameObject>();

    private void Awake()
    {
        instance = this;
        panelError.SetActive(false);
    }

    private void Refresh(){
        LayoutRebuilder.ForceRebuildLayoutImmediate(panelError.GetComponent<RectTransform>());
    }

    public void ShowError(string msg, bool isBlocking = false)
    {
        Debug.LogError(msg);
        panelError.SetActive(true);
        textError.text = msg;
        if (!isBlocking) closeButton.SetActive(true);
        else closeButton.SetActive(false);
    }


    public void ShowError(TYPE type, string msg)
    {
        panelError.SetActive(true);

        GameObject go = Instantiate(prefab, panelError.transform);
        prefabs.Add(go);

        /// setup the prefab
        ErrorMessageSetup prefabPanel = go.GetComponent<ErrorMessageSetup>();
        prefabPanel.Setup(type, prefabs.Count-1, msg);

        totalPrefabsNumber ++;

        Refresh();
    }


    public void CloseErrorPrefabPanel(int prefabNumber){

        Destroy(prefabs[prefabNumber]);
        totalPrefabsNumber --;

        /// hide the panel if no error panels are shown anymore
        if (totalPrefabsNumber == 0){
            prefabs.Clear();
            panelError.SetActive(false);
        }
    }



    public void ClosePanel()
    {
        panelError.SetActive(false);
    }
}
