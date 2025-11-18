using UnityEngine;

public class HighlightEffect : MonoBehaviour
{
    private Renderer rend;
    private Material mat;

    private Color originalTint;
    private bool isHighlighted = false;

    public Color highlightColor = Color.white;
    [Range(0f, 1f)]
    public float highlightAlpha = 0.5f;

    void Start()
    {
        rend = GetComponent<Renderer>();
        if (rend == null)
        {
            Debug.LogWarning("HighlightEffect: No Renderer en " + gameObject.name);
            return;
        }

        mat = rend.material;

        // Buscamos el tint del color base (si existe)
        if (mat.HasProperty("_Color"))
            originalTint = mat.color;
        else if (mat.HasProperty("_BaseColor"))
            originalTint = mat.GetColor("_BaseColor");
        else
        {
            // Si el shader no tiene color base, creamos uno
            originalTint = Color.white;
        }
    }

    public void HighlightOn()
    {
        if (mat == null || isHighlighted) return;

        Color tint = Color.Lerp(originalTint, highlightColor, highlightAlpha);

        if (mat.HasProperty("_Color"))
            mat.color = tint;
        else if (mat.HasProperty("_BaseColor"))
            mat.SetColor("_BaseColor", tint);

        isHighlighted = true;
    }

    public void HighlightOff()
    {
        if (mat == null || !isHighlighted) return;

        if (mat.HasProperty("_Color"))
            mat.color = originalTint;
        else if (mat.HasProperty("_BaseColor"))
            mat.SetColor("_BaseColor", originalTint);

        isHighlighted = false;
    }
}
