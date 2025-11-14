using UnityEngine;

public class HighlightEffect : MonoBehaviour
{
    private Renderer rend;
    private Color originalColor;
    public Color highlightColor = Color.white;
    [Range(0f, 1f)]
    public float highlightAlpha = 0.5f; // Opacidad del color de highlight (0 = transparente, 1 = opaco)
    public bool isHighlighted = false;

    void Start()
    {
        Renderer rend = GetComponent<Renderer>();
        if (rend == null)
        {
            Debug.LogWarning("HighlightEffect: No Renderer en " + gameObject.name);
            return;
        }

        Material mat = rend.material;
        if (mat.HasProperty("_Color"))
        {
            originalColor = mat.color;
        }
        else if (mat.HasProperty("_BaseColor"))
        {
            originalColor = mat.GetColor("_BaseColor");
        }
        else
        {
            Debug.LogWarning("HighlightEffect: Shader sin propiedad de color en " + gameObject.name + " → " + mat.shader.name);
        }
    }






    //void Start()
    //{
    //    rend = GetComponent<Renderer>();
    //    if (rend != null)
    //        originalColor = rend.material.color;
    //}

    public void HighlightOn()
    {
        if (rend != null && !isHighlighted)
        {
            Color c = highlightColor;
            c.a = highlightAlpha;
            rend.material.color = c;
            isHighlighted = true;
        }
    }

    public void HighlightOff()
    {
        if (rend != null && isHighlighted)
        {
            rend.material.color = originalColor;
            isHighlighted = false;
        }
    }
}