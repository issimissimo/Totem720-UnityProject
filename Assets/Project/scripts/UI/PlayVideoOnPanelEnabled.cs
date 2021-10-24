using UnityEngine;
using System.IO;

public class PlayVideoOnPanelEnabled : MonoBehaviour
{
    public string videoName;
    public GameManager gameManager;

    private void Awake()
    {
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        if (!string.IsNullOrEmpty(videoName))
        {
            string path = Path.Combine(Globals.data.videoFolder, Globals.engagementVideoFolder);
            string videoUrl = Path.Combine(path, videoName);

            if (File.Exists(videoUrl))
            {
                gameManager.videoManager.Play(videoUrl, true);
            }
            else{
                ErrorManager.instance.ShowError(ErrorManager.TYPE.ERROR, "Il video " + videoUrl + " non esiste");
            }
        }
    }

    private void OnDisable()
    {
        gameManager.videoManager.Stop();
    }
}
