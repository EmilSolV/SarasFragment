using UnityEngine;

public class CamaraEstudioInteractiva : MonoBehaviour
{
    public Camera camaraEstudio;
    public GameObject camaraJugador;
    public Light luzUV; // ← nueva referencia

    public float sensibilidad = 2f;
    private bool inspeccionActiva = false;
    private float rotacionX = 0f;
    private float rotacionY = 0f;

    void Update()
    {
        if (inspeccionActiva)
        {
            float mouseX = Input.GetAxis("Mouse X") * sensibilidad;
            float mouseY = Input.GetAxis("Mouse Y") * sensibilidad;

            rotacionX -= mouseY;
            rotacionY += mouseX;
            rotacionX = Mathf.Clamp(rotacionX, -45f, 45f);

            camaraEstudio.transform.localRotation = Quaternion.Euler(rotacionX, rotacionY, 0f);

            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetMouseButtonDown(1))
            {
                SalirDeInspeccion();
            }
        }
    }

    public void ActivarInspeccion()
    {
        inspeccionActiva = true;
        camaraEstudio.gameObject.SetActive(true);
        camaraJugador.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (luzUV != null)
            luzUV.enabled = true;
    }

    public void SalirDeInspeccion()
    {
        inspeccionActiva = false;
        camaraEstudio.gameObject.SetActive(false);
        camaraJugador.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (luzUV != null)
            luzUV.enabled = false;
    }
}
