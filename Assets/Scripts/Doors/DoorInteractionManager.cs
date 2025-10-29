using UnityEngine;

public class DoorInteractionManager : MonoBehaviour
{
    public Camera FPCamera;
    public float interactionDistance = 3f;

    void Start()
    {
        if (FPCamera == null)
        {
           FPCamera = Camera.main;
        }
        Debug.Log("DoorInteractionManager iniciado");
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Presionaste Click Izq<");
            Ray ray = FPCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
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
}