using UnityEngine;
using UnityEngine.UI;

public class NotebookCursor : MonoBehaviour
{
    public RectTransform cursorUI;     // Image CursorNotebook
    public RectTransform areaNotebook; // Panel AreaNotebook
    public Camera cameraNotebook;      // Cámara que mira la pantalla

    void OnEnable()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void OnDisable()
    {
        Cursor.lockState = CursorLockMode.None;   // libera el cursor
        Cursor.visible = true;                    // vuelve a mostrarlo
    }

    void Update()
    {
        Vector2 mousePos = Input.mousePosition;
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            areaNotebook, mousePos, cameraNotebook, out localPoint
        );

        Vector2 clamped = new Vector2(
            Mathf.Clamp(localPoint.x, areaNotebook.rect.xMin, areaNotebook.rect.xMax),
            Mathf.Clamp(localPoint.y, areaNotebook.rect.yMin, areaNotebook.rect.yMax)
        );

        cursorUI.localPosition = clamped;
    }
}