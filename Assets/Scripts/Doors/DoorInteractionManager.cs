using UnityEngine;

public class DoorInteractionManager : MonoBehaviour
{
    public Camera firstPersonCamera;
    public Camera thirdPersonCamera;
    public float interactionDistance = 3f;

    private void Start()
    {
        Debug.Log("DoorInteractionManager iniciado");
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Presionaste Click Izq<");
            Camera activeCam = GetActiveCamera();
            if (activeCam == null)
            {
                Debug.LogWarning("No hay cámara activa asignada.");
                return;
            }

            Ray ray = activeCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            if (Physics.Raycast(ray, out RaycastHit hit, interactionDistance))
            {
                Debug.Log("Raycast hit: " + hit.collider.name);
                IDoorInteractable door = hit.collider.GetComponentInParent<IDoorInteractable>();
                if (door != null)
                {
                    Debug.Log("Puerta detectada, ejecutando ToggleDoor");
                    door.ToggleDoor();
                }
                else
                {
                    Debug.Log("El objeto tocado no tiene IDoorInteractable");
                }
            }
            else
            {
                Debug.Log("Raycast no tocó ningún objeto");
            }
        }
    }

    private Camera GetActiveCamera()
    {
        if (firstPersonCamera != null && firstPersonCamera.gameObject.activeInHierarchy)
            return firstPersonCamera;
        if (thirdPersonCamera != null && thirdPersonCamera.gameObject.activeInHierarchy)
            return thirdPersonCamera;
        return Camera.main; // Fallback por si acaso
    }
}