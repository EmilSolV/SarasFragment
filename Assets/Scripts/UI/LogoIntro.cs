using UnityEngine;
using UnityEngine.UI;

public class LogoIntro : MonoBehaviour
{
    public Image logoImage;
    public float showDuration = 1.5f;
    public float fadeDuration = 1f;

    void Start()
    {
        StartCoroutine(FadeOutLogo());
    }

    private System.Collections.IEnumerator FadeOutLogo()
    {
        // Mantiene el logo visible unos segundos
        yield return new WaitForSeconds(showDuration);

        float t = 0f;
        Color c = logoImage.color;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            float alpha = 1f - (t / fadeDuration);
            logoImage.color = new Color(c.r, c.g, c.b, alpha);
            yield return null;
        }

        logoImage.gameObject.SetActive(false);
    }
}
