using UnityEngine;
using Cinemachine;

public class PuzzleVelador : MonoBehaviour

{
    public bool inspeccionActiva => _inspeccionActiva;
    private bool _inspeccionActiva = false;

    [Header("Referencias")]
    public GameObject camaraPuzzle; // Cámara para inspección (asígnala en el inspector)
    public GameObject fpCamera;

    [Header("Configuración")]
    public float distanciaInteraccion = 2.5f;

    void Update()
    {
        // Click izquierdo para inspeccionar
        if (Input.GetMouseButtonDown(0) && !_inspeccionActiva)
        {
            Ray ray = new Ray(fpCamera.transform.position, fpCamera.transform.forward);
            if (Physics.Raycast(ray, out RaycastHit hit, distanciaInteraccion))
            {
                if (hit.collider.CompareTag("Velador"))
                {
                    _inspeccionActiva = true;
                    if (camaraPuzzle != null) camaraPuzzle.SetActive(true);
                    if (fpCamera != null) fpCamera.SetActive(false);
                }
            }
        }

        // Click derecho para salir de inspección
        if (Input.GetMouseButtonDown(1) && _inspeccionActiva)
        {
            _inspeccionActiva = false;
            if (camaraPuzzle != null) camaraPuzzle.SetActive(false);
            if (fpCamera != null) fpCamera.SetActive(true);
        }
    }

    public void ForzarSalidaInspeccion()
    {
        _inspeccionActiva = false;
        if (camaraPuzzle != null) camaraPuzzle.SetActive(false);
        if (fpCamera != null) fpCamera.SetActive(true);
    }
}
