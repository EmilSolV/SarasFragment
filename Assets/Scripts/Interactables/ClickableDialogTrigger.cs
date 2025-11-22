using UnityEngine;

/// <summary>
/// Componente que muestra un diálogo cuando se hace click sobre el objeto
/// Usa raycast desde Camera.main para detectar el click
/// </summary>
[RequireComponent(typeof(Collider))]
public class ClickableDialogTrigger : MonoBehaviour
{
    [Header("Configuración del Diálogo")]
    [TextArea(3, 6)]
    [Tooltip("Mensaje que se mostrará cuando se haga click en el objeto")]
    public string dialogMessage = "Mensaje de ejemplo";

    [Tooltip("Duración en segundos que el diálogo permanecerá visible")]
    public float dialogDuration = 5f;

    [Header("Configuración del Raycast")]
    [Tooltip("Distancia máxima desde la cámara para detectar el click")]
    public float maxRaycastDistance = 5f;

    [Header("Comportamiento")]
    [Tooltip("Si está activado, el diálogo solo se mostrará la primera vez que se haga click")]
    public bool showOnlyOnce = false;

    [Tooltip("Si está activado, se mostrará un mensaje en consola cuando se haga click")]
    public bool debugMode = false;

    // Estado interno
    private bool hasBeenClicked = false;

    void Start()
    {
        // Verificar que el objeto tenga collider
        Collider col = GetComponent<Collider>();
        if (col == null)
        {
            Debug.LogError($"ClickableDialogTrigger en {gameObject.name} requiere un Collider.", this);
        }
    }

    void Update()
    {
        // Detectar click izquierdo
        if (Input.GetMouseButtonDown(0))
        {
            CheckForClick();
        }
    }

    /// <summary>
    /// Verifica si el click fue sobre este objeto
    /// </summary>
    private void CheckForClick()
    {
        // Si ya fue clickeado y solo se muestra una vez, no hacer nada
        if (showOnlyOnce && hasBeenClicked)
        {
            if (debugMode)
                Debug.Log($"[{gameObject.name}] Ya fue clickeado anteriormente (showOnlyOnce=true)");
            return;
        }

        // Verificar que exista Camera.main
        if (Camera.main == null)
        {
            Debug.LogWarning("ClickableDialogTrigger: No se encontró Camera.main");
            return;
        }

        // Crear rayo desde la cámara hacia adelante
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        RaycastHit hit;

        // Lanzar el raycast
        if (Physics.Raycast(ray, out hit, maxRaycastDistance))
        {
            // Verificar si el objeto impactado es este
            if (hit.collider.gameObject == gameObject)
            {
                ShowDialog();
            }
        }
    }

    /// <summary>
    /// Muestra el diálogo usando DialogManager
    /// </summary>
    private void ShowDialog()
    {
        if (DialogManager.Instance != null && !string.IsNullOrEmpty(dialogMessage))
        {
            DialogManager.Instance.ShowMessage(dialogMessage, dialogDuration);
            hasBeenClicked = true;

            if (debugMode)
                Debug.Log($"[{gameObject.name}] Diálogo mostrado: '{dialogMessage}'");
        }
        else if (DialogManager.Instance == null)
        {
            Debug.LogWarning($"DialogManager no encontrado. No se puede mostrar el diálogo de {gameObject.name}");
        }
    }

    /// <summary>
    /// Reinicia el estado del trigger (útil para testing)
    /// </summary>
    public void ResetTrigger()
    {
        hasBeenClicked = false;

        if (debugMode)
            Debug.Log($"[{gameObject.name}] Trigger reiniciado");
    }

    /// <summary>
    /// Verifica si el objeto ya ha sido clickeado
    /// </summary>
    public bool HasBeenClicked()
    {
        return hasBeenClicked;
    }

    /// <summary>
    /// Fuerza la visualización del diálogo sin necesidad de click
    /// </summary>
    public void ForceShowDialog()
    {
        ShowDialog();
    }

    // Visualización en el editor
    void OnDrawGizmos()
    {
        Collider col = GetComponent<Collider>();
        if (col == null) return;

        // Color según el estado
        if (hasBeenClicked)
        {
            Gizmos.color = Color.green; // Verde si ya fue clickeado
        }
        else
        {
            Gizmos.color = Color.cyan; // Cyan si aún no fue clickeado
        }

        Gizmos.DrawWireCube(col.bounds.center, col.bounds.size);
    }

    void OnDrawGizmosSelected()
    {
        // Mostrar el alcance del raycast cuando el objeto está seleccionado
        if (Camera.main != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * maxRaycastDistance);
        }
    }
}
