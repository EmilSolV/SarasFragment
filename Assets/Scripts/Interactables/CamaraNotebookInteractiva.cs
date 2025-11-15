using UnityEngine;

public class CamaraNotebookInteractiva : MonoBehaviour
{
    public Camera camaraNotebook;
    public GameObject camaraJugador;
    public GameObject canvasCodigo; // opcional

    private bool inspeccionActiva = false;

    void Update()
    {
        if (inspeccionActiva && (Input.GetKeyDown(KeyCode.Escape) || Input.GetMouseButtonDown(1)))
        {
            SalirDeInspeccion();
        }
    }

    public void ActivarInspeccion()
    {
        inspeccionActiva = true;
        camaraNotebook.gameObject.SetActive(true);
        camaraJugador.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (canvasCodigo != null)
            canvasCodigo.SetActive(true);
    }

    public void SalirDeInspeccion()
    {
        inspeccionActiva = false;
        camaraNotebook.gameObject.SetActive(false);
        camaraJugador.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (canvasCodigo != null)
            canvasCodigo.SetActive(false);
    }
}
