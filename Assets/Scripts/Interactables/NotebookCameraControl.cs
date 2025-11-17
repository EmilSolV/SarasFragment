using UnityEngine;
using UnityEngine.UI;

public class NotebookManager : MonoBehaviour
{
    [Header("Cámaras")]
    public Camera cameraNotebook;
    public Camera cameraPrincipal;

    [Header("Control del jugador")]
    public MonoBehaviour controladorJugador;

    [Header("Canvas y cursor")]
    public GameObject canvasNotebook;           // Canvas en World Space
    public RectTransform cursorUI;              // Imagen PNG del cursor falso
    public RectTransform areaNotebook;          // Panel que define el área de movimiento

    [Header("Tecla de salida")]
    public KeyCode teclaSalir = KeyCode.Escape;

    private bool notebookActivo = false;

    void Start()
    {
        cameraNotebook.enabled = false;
        cameraPrincipal.enabled = true;
        canvasNotebook.SetActive(false);
        cursorUI.gameObject.SetActive(false);
    }

    void OnMouseDown()
    {
        if (!notebookActivo)
        {
            ActivarNotebook();
        }
    }

    void Update()
    {
        if (notebookActivo)
        {
            ActualizarCursorFalso();

            // Salir con tecla definida (ej. Escape)
            if (Input.GetKeyDown(teclaSalir))
            {
                SalirNotebook();
            }

            // Salir con clic derecho
            if (Input.GetMouseButtonDown(1)) // botón derecho del mouse
            {
                SalirNotebook();
            }
        }
    }

    void ActivarNotebook()
    {
        notebookActivo = true;

        cameraNotebook.enabled = true;
        cameraPrincipal.enabled = false;

        if (controladorJugador != null)
            controladorJugador.enabled = false;

        canvasNotebook.SetActive(true);
        cursorUI.gameObject.SetActive(true);

        Cursor.visible = false;                  // ocultar cursor del sistema
        Cursor.lockState = CursorLockMode.None;  // dejarlo libre para UI
        GetComponent<Collider>().enabled = false;
    }

    void SalirNotebook()
    {
        notebookActivo = false;

        cameraNotebook.enabled = false;
        cameraPrincipal.enabled = true;

        if (controladorJugador != null)
            controladorJugador.enabled = true;

        canvasNotebook.SetActive(false);
        cursorUI.gameObject.SetActive(false);

        Cursor.visible = false;                  // ocultar cursor del sistema
        Cursor.lockState = CursorLockMode.Locked; // bloquearlo al centro
        GetComponent<Collider>().enabled = true;
    }

    void ActualizarCursorFalso()
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