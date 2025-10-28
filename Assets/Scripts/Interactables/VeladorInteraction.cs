using Cinemachine;
using UnityEngine;


    public class VeladorInteraction : MonoBehaviour
    {
        public CinemachineVirtualCamera veladorCam;
        public GameObject velador;
        public float distanciaInteraccion = 2.5f;
        public GameObject fpCamera;
        public Light luzUV;

    private bool isInspecting = false;

        void Update()
        {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!fpCamera.activeInHierarchy) return; // Solo funciona si estás en primera persona

            Ray ray = new Ray(fpCamera.transform.position, fpCamera.transform.forward);
            if (Physics.Raycast(ray, out RaycastHit hit, distanciaInteraccion))
            {
                if (hit.collider.CompareTag("Velador"))
                {
                    isInspecting = !isInspecting;
                    veladorCam.Priority = isInspecting ? 10 : 0;
                    if (luzUV != null)
                    {
                        luzUV.enabled = isInspecting;
                    }
                }
            }
        }

        if (isInspecting)
            {
            ControlarCamaraConMouse();
            }

        if (fpCamera.activeInHierarchy)
        {
            // Ejecutar raycast y activar cámara UV
        }
    }


    void ControlarCamaraConMouse()
    {
        float rotX = Input.GetAxis("Mouse X") * 50f * Time.deltaTime;
        float rotY = -Input.GetAxis("Mouse Y") * 50f * Time.deltaTime;

        veladorCam.transform.Rotate(Vector3.up, rotX, Space.World);
        veladorCam.transform.Rotate(Vector3.right, rotY, Space.Self);
    }
    public void ActivarInspeccion()
    {
        isInspecting = !isInspecting;
        veladorCam.Priority = isInspecting ? 10 : 0;
    }



}
