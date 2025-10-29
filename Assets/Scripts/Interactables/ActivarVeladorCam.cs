using UnityEngine;
using Cinemachine;

public class ActivarVeladorCam : MonoBehaviour
{
    public CinemachineVirtualCamera veladorCam;
    public GameObject fpCamera;
    public float distanciaInteraccion = 2.5f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Ray ray = new Ray(fpCamera.transform.position, fpCamera.transform.forward);
            if (Physics.Raycast(ray, out RaycastHit hit, distanciaInteraccion))
            {
                if (hit.collider.CompareTag("Velador"))
                {
                    veladorCam.Priority = 20;
                }
            }
        }
    }
}