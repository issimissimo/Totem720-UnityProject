using UnityEngine;
using System.IO;

public class PlayVideoOnPanelEnabled : MonoBehaviour
{
    public string videoPathInter;
    public string videoPathMilan;
    public string videoPathInterMilan;
    public GameManager gameManager;
    public bool loop = true;

    string path = null;

    private void Awake()
    {
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        if (Globals._SQUADRA == Globals.Squadra.Milan) path = videoPathMilan;
        if (Globals._SQUADRA == Globals.Squadra.Inter) path = videoPathInter;
        if (Globals._SQUADRA == Globals.Squadra.Inter_Milan) path = videoPathInterMilan;

        if (!string.IsNullOrEmpty(path))
        {
            string videoUrl = Path.Combine(Globals.data.videoFolder, path);

            if (File.Exists(videoUrl))
            {
                gameManager.videoManager.Play(videoUrl, loop);
            }
            else
            {
                ErrorManager.instance.ShowError(ErrorManager.TYPE.ERROR, "Il video " + videoUrl + " non esiste");
            }
        }
    }

    private void OnDisable()
    {
        if (!string.IsNullOrEmpty(path))
            gameManager.videoManager.Stop();
    }
}
