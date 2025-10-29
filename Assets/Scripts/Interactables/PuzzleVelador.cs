using UnityEngine;
using Cinemachine;

public class PuzzleVelador : MonoBehaviour

{
    public bool inspeccionActiva => _inspeccionActiva;
    private bool _inspeccionActiva = false;

    [Header("Referencias")]
    public CinemachineVirtualCamera veladorCam;
    public GameObject fpCamera;

    [Header("Configuración")]
    public float distanciaInteraccion = 2.5f;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = new Ray(fpCamera.transform.position, fpCamera.transform.forward);
            if (Physics.Raycast(ray, out RaycastHit hit, distanciaInteraccion))
            {
                if (hit.collider.CompareTag("Velador"))
                {
                    _inspeccionActiva = true;
                    veladorCam.Priority = 20;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape) && _inspeccionActiva)
        {
            _inspeccionActiva = false;
            veladorCam.Priority = 0;
        }
    
    }
    public void ForzarSalidaInspeccion()
    {
        _inspeccionActiva = false;
        if (veladorCam != null)
            veladorCam.Priority = 0;
    }


}
