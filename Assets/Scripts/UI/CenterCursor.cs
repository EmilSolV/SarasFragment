using UnityEngine;

public class CenterCursor : MonoBehaviour
{
    public Texture2D cursorTexture; // Opcional: puedes asignar una textura en el inspector
    public float size = 2f;         // Tamaño del punto

    void OnGUI()
    {
        float x = (Screen.width - size) / 2;
        float y = (Screen.height - size) / 2;

        if (cursorTexture != null)
        {
            GUI.DrawTexture(new Rect(x, y, size, size), cursorTexture);
        }
        else
        {
            // Dibuja un punto blanco si no hay textura
            Color prevColor = GUI.color;
            GUI.color = Color.white;
            GUI.DrawTexture(new Rect(x, y, size, size), Texture2D.whiteTexture);
            GUI.color = prevColor;
        }
    }
}