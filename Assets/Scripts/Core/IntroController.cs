using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class IntroController : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public string nextSceneName = "MainMenu";

    private bool isSkipping = false;

    void Start()
    {
        if (videoPlayer == null)
        {
            Debug.LogError("¡Falta asignar el VideoPlayer en el inspector!");
            return;
        }

        videoPlayer.loopPointReached += OnVideoEnd;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            SkipIntro();
        }
    }

    // Cuando el video termina solo
    private void OnVideoEnd(VideoPlayer vp)
    {
        if (!isSkipping)
            LoadNextScene();
    }

    // Cuando se presiona E
    private void SkipIntro()
    {
        if (isSkipping) return;

        isSkipping = true;
        LoadNextScene();
    }

    private void LoadNextScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}
