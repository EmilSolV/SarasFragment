using UnityEngine;

/// <summary>
/// Sistema centralizado de raycast que detecta objetos con InteractableMessageTrigger desde Camera.main
/// Funciona automáticamente con cualquier cámara que sea la principal (primera persona o inspección)
/// Similar a cómo funciona HighlightEffect
/// </summary>
public class PlayerRaycastInteraction : MonoBehaviour
{
    [Header("Configuración del Raycast")]
    [Tooltip("Distancia máxima del raycast")]
    public float raycastDistance = 5f;

    [Tooltip("Layers que el raycast puede detectar")]
    public LayerMask interactableLayers = ~0;

    [Header("Visualización Debug")]
    [Tooltip("Mostrar el raycast en la vista de escena")]
    public bool showDebugRay = true;

    [Tooltip("Color del rayo cuando no detecta nada")]
    public Color rayColorMiss = Color.red;

    [Tooltip("Color del rayo cuando detecta un objeto")]
    public Color rayColorHit = Color.green;

    // Estado interno
    private InteractableMessageTrigger lastTrigger;

    void Update()
    {
        CheckInteractableMessages();
    }

    /// <summary>
    /// Verifica y gestiona los mensajes de objetos interactuables usando Camera.main
    /// Esto funciona automáticamente con cualquier cámara que tenga el tag "MainCamera"
    /// </summary>
    private void CheckInteractableMessages()
    {
        if (Camera.main == null) return;

        // Raycast desde el centro de la pantalla (como ObjectGrabber y ObjectGrabberMesa)
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit;

        bool hitSomething = Physics.Raycast(ray, out hit, raycastDistance, interactableLayers);

        // Debug visual
        if (showDebugRay)
        {
            Color rayColor = hitSomething ? rayColorHit : rayColorMiss;
            float rayLength = hitSomething ? hit.distance : raycastDistance;
            Debug.DrawRay(ray.origin, ray.direction * rayLength, rayColor);
        }

        InteractableMessageTrigger currentTrigger = null;

        if (hitSomething)
        {
            currentTrigger = hit.collider.GetComponent<InteractableMessageTrigger>();
            
            if (currentTrigger != null)
            {
                // Si es un nuevo objetivo
                if (currentTrigger != lastTrigger)
                {
                    if (lastTrigger != null)
                    {
                        lastTrigger.OnRaycastExit();
                    }
                    currentTrigger.OnRaycastEnter();
                    lastTrigger = currentTrigger;
                }
                else
                {
                    // Si seguimos apuntando al mismo objeto
                    currentTrigger.OnRaycastStay();
                }
            }
            else if (lastTrigger != null)
            {
                // Apuntamos a algo sin el componente
                lastTrigger.OnRaycastExit();
                lastTrigger = null;
            }
        }
        else if (lastTrigger != null)
        {
            // No apuntamos a nada
            lastTrigger.OnRaycastExit();
            lastTrigger = null;
        }
    }

    /// <summary>
    /// Obtiene el trigger actual
    /// </summary>
    public InteractableMessageTrigger GetCurrentTrigger()
    {
        return lastTrigger;
    }

    /// <summary>
    /// Verifica si actualmente estamos apuntando a algún objeto interactuable
    /// </summary>
    public bool IsPointingAtInteractable()
    {
        return lastTrigger != null;
    }

    void OnDisable()
    {
        if (lastTrigger != null)
        {
            lastTrigger.OnRaycastExit();
            lastTrigger = null;
        }
    }

    void OnDrawGizmos()
    {
        if (Camera.main == null) return;

        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        
        Gizmos.color = lastTrigger != null ? rayColorHit : rayColorMiss;
        Gizmos.DrawRay(ray.origin, ray.direction * raycastDistance);

        if (lastTrigger != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(lastTrigger.transform.position, 0.3f);
        }
    }
}
