using UnityEngine;


public class NotebookInteractiva : MonoBehaviour
{
    public Camera camaraNotebook;
    public Camera camaraPrincipal;
    public GameObject canvasCodigo; // opcional, si querés mostrar el canvas al entrar

    private bool notebookActiva = false;

    void Update()
    {
        if (!notebookActiva && Input.GetMouseButtonDown(0)) // Click izquierdo
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.transform == transform)
                {
                    ActivarNotebook();
                }
            }
        }

        if (notebookActiva && (Input.GetKeyDown(KeyCode.Escape) || Input.GetMouseButtonDown(1))) // Esc o click derecho
        {
            SalirDeNotebook();
        }
    }

    void ActivarNotebook()
    {
        camaraNotebook.gameObject.SetActive(true);
        camaraPrincipal.gameObject.SetActive(false);

        if (canvasCodigo != null)
            canvasCodigo.SetActive(true);

        notebookActiva = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        Debug.Log("Notebook activada: cámara notebook encendida.");
    }

    void SalirDeNotebook()
    {
        camaraNotebook.gameObject.SetActive(false);
        camaraPrincipal.gameObject.SetActive(true);

        if (canvasCodigo != null)
            canvasCodigo.SetActive(false);

        notebookActiva = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        Debug.Log("Notebook desactivada: cámara principal restaurada.");
    }
}