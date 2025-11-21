using UnityEngine;
using System.Collections;

public class LoopTransitionManager : MonoBehaviour
{
    public static LoopTransitionManager Instance;
    public CanvasGroup fadeGroup;
    public float fadeDuration = 2f;

    void Awake()
    {
        Instance = this;
    }

    public IEnumerator DoTransition(System.Action midpointAction)
    {
        // Fade to black
        yield return StartCoroutine(Fade(1f));

        // Acción en medio de la transición (resetear objetos, jugador, timer, etc.)
        midpointAction?.Invoke();

        // Fade in
        yield return StartCoroutine(Fade(0f));
    }

    private IEnumerator Fade(float target)
    {
        float start = fadeGroup.alpha;
        float time = 0f;

        while (time < fadeDuration)
        {
            fadeGroup.alpha = Mathf.Lerp(start, target, time / fadeDuration);
            time += Time.deltaTime;
            yield return null;
        }

        fadeGroup.alpha = target;
    }
}
