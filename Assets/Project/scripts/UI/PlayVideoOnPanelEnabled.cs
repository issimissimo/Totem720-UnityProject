using UnityEngine;
using System.IO;

public class PlayVideoOnPanelEnabled : MonoBehaviour
{
    public string videoPath;
    public GameManager gameManager;

    private void Awake()
    {
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        if (!string.IsNullOrEmpty(videoPath))
        {
            string videoUrl = Path.Combine(Globals.data.videoFolder, videoPath);

            print(videoUrl);

            if (File.Exists(videoUrl))
            {
                gameManager.videoManager.Play(videoUrl, true);
            }
            else
            {
                ErrorManager.instance.ShowError(ErrorManager.TYPE.ERROR, "Il video " + videoUrl + " non esiste");
            }
        }
    }

    private void OnDisable()
    {
        if (!string.IsNullOrEmpty(videoPath))
            gameManager.videoManager.Stop();
    }
}
