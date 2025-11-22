using UnityEngine;

/// <summary>
/// Componente que muestra un mensaje cuando el raycast del jugador apunta al objeto
/// </summary>
[RequireComponent(typeof(Collider))]
public class InteractableMessageTrigger : MonoBehaviour
{
    [Header("Configuración del Mensaje")]
    [TextArea(3, 6)]
    [Tooltip("Mensaje que se mostrará cuando el raycast apunte a este objeto")]
    public string message = "Mensaje de ejemplo";

    [Tooltip("Duración en segundos que el mensaje permanecerá visible")]
    public float messageDuration = 3f;

    [Header("Comportamiento")]
    [Tooltip("Si está activado, el mensaje solo se mostrará la primera vez que se mire el objeto")]
    public bool showOnlyOnce = false;

    [Tooltip("Si está activado, el mensaje se mostrará cada vez que el raycast entre al objeto (entrada/salida)")]
    public bool showOnEnterOnly = false;

    [Tooltip("Si está activado, el mensaje se mostrará continuamente mientras el raycast esté sobre el objeto")]
    public bool showContinuously = false;

    [Tooltip("Intervalo mínimo entre mensajes continuos (solo si showContinuously está activado)")]
    public float continuousMessageInterval = 2f;

    [Header("Debug")]
    [Tooltip("Mostrar logs en consola para depuración")]
    public bool debugMode = false;

    // Estado interno
    private bool hasBeenViewed = false;
    private bool isCurrentlyViewed = false;
    private float lastMessageTime = 0f;

    void Start()
    {
        // Verificar que el objeto tenga collider
        Collider col = GetComponent<Collider>();
        if (col == null)
        {
            Debug.LogError($"InteractableMessageTrigger en {gameObject.name} requiere un Collider.", this);
        }
    }

    /// <summary>
    /// Método llamado cuando el raycast entra en contacto con este objeto
    /// </summary>
    public void OnRaycastEnter()
    {
        if (debugMode)
            Debug.Log($"[{gameObject.name}] Raycast Enter");

        // Si ya fue visto y solo se muestra una vez, no hacer nada
        if (showOnlyOnce && hasBeenViewed)
        {
            if (debugMode)
                Debug.Log($"[{gameObject.name}] Ya fue visto, ignorando (showOnlyOnce=true)");
            return;
        }

        isCurrentlyViewed = true;

        // Mostrar mensaje según la configuración
        if (showOnEnterOnly || !showContinuously)
        {
            ShowMessage();
        }
    }

    /// <summary>
    /// Método llamado mientras el raycast permanece sobre este objeto
    /// </summary>
    public void OnRaycastStay()
    {
        if (debugMode && Time.frameCount % 60 == 0) // Log cada 60 frames para no saturar
            Debug.Log($"[{gameObject.name}] Raycast Stay");

        // Si ya fue visto y solo se muestra una vez, no hacer nada
        if (showOnlyOnce && hasBeenViewed)
            return;

        // Mostrar mensaje continuamente si está configurado
        if (showContinuously && !showOnEnterOnly)
        {
            if (Time.time - lastMessageTime >= continuousMessageInterval)
            {
                ShowMessage();
            }
        }
    }

    /// <summary>
    /// Método llamado cuando el raycast sale del objeto
    /// </summary>
    public void OnRaycastExit()
    {
        if (debugMode)
            Debug.Log($"[{gameObject.name}] Raycast Exit");

        isCurrentlyViewed = false;
    }

    /// <summary>
    /// Muestra el mensaje usando el DialogManager
    /// </summary>
    private void ShowMessage()
    {
        if (DialogManager.Instance != null && !string.IsNullOrEmpty(message))
        {
            DialogManager.Instance.ShowMessage(message, messageDuration);
            lastMessageTime = Time.time;
            hasBeenViewed = true;

            if (debugMode)
                Debug.Log($"[{gameObject.name}] Mensaje mostrado: '{message}'");
        }
        else if (DialogManager.Instance == null)
        {
            Debug.LogWarning($"DialogManager no encontrado. No se puede mostrar el mensaje de {gameObject.name}");
        }
    }

    /// <summary>
    /// Reinicia el estado del trigger (útil para testing o loops)
    /// </summary>
    public void ResetTrigger()
    {
        hasBeenViewed = false;
        isCurrentlyViewed = false;
        lastMessageTime = 0f;

        if (debugMode)
            Debug.Log($"[{gameObject.name}] Trigger reiniciado");
    }

    /// <summary>
    /// Verifica si el objeto ya ha sido visto
    /// </summary>
    public bool HasBeenViewed()
    {
        return hasBeenViewed;
    }

    /// <summary>
    /// Verifica si el raycast está actualmente sobre el objeto
    /// </summary>
    public bool IsCurrentlyViewed()
    {
        return isCurrentlyViewed;
    }

    // Visualización en el editor
    void OnDrawGizmos()
    {
        if (hasBeenViewed)
        {
            Gizmos.color = Color.green;
        }
        else
        {
            Gizmos.color = Color.yellow;
        }

        Collider col = GetComponent<Collider>();
        if (col != null)
        {
            Gizmos.DrawWireCube(col.bounds.center, col.bounds.size);
        }
    }
}
