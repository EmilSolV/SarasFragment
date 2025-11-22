using UnityEngine;
using TMPro;
using System.Collections;

public class DialogManager : MonoBehaviour
{
    public static DialogManager Instance { get; private set; }

    public TextMeshProUGUI dialogText;

    private Coroutine currentDialogCoroutine;
    
    [Header("Debug (solo para testing en build)")]
    public bool showDebugInfo = false;
    private string lastError = "";

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnEnable()
    {
        UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        UnityEngine.SceneManagement.SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    /// <summary>
    /// Se llama cada vez que se carga una nueva escena
    /// </summary>
    private void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, UnityEngine.SceneManagement.LoadSceneMode mode)
    {
        // Buscar y reasignar la referencia al texto de diálogo en la nueva escena
        StartCoroutine(FindDialogTextDelayed());
    }

    /// <summary>
    /// Busca el texto con un pequeño delay para asegurar que la escena esté completamente cargada
    /// </summary>
    private IEnumerator FindDialogTextDelayed()
    {
        // Esperar un frame para que la escena se cargue completamente
        yield return new WaitForEndOfFrame();
        
        FindDialogTextInScene();
    }

    /// <summary>
    /// Busca el TextMeshProUGUI de diálogos en la escena actual
    /// </summary>
    private void FindDialogTextInScene()
    {
        string sceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        
        // Primero intenta mantener la referencia existente si sigue válida
        if (dialogText != null && dialogText.gameObject.scene.name == sceneName)
        {
            Debug.Log($"✅ DialogManager: Referencia al texto sigue válida en '{sceneName}'");
            lastError = $"[{sceneName}] Texto OK";
            return;
        }

        // Limpiar referencia anterior
        dialogText = null;

        // ESTRATEGIA 1: Buscar Canvas con DialogManager o DialogCanvas
        Canvas[] allCanvas = FindObjectsOfType<Canvas>(true);
        foreach (Canvas canvas in allCanvas)
        {
            if (canvas.gameObject.name.Contains("Dialog") || canvas.gameObject.name.Contains("UI"))
            {
                TextMeshProUGUI[] texts = canvas.GetComponentsInChildren<TextMeshProUGUI>(true);
                foreach (TextMeshProUGUI txt in texts)
                {
                    if (txt.gameObject.name.Contains("Dialog") || txt.gameObject.name.Contains("Text"))
                    {
                        dialogText = txt;
                        Debug.Log($"✅ DialogManager: Texto encontrado en Canvas '{canvas.name}' > '{txt.name}'");
                        lastError = $"[{sceneName}] Encontrado en {canvas.name}/{txt.name}";
                        return;
                    }
                }
            }
        }

        // ESTRATEGIA 2: Buscar por tag
        try
        {
            GameObject taggedDialog = GameObject.FindGameObjectWithTag("DialogText");
            if (taggedDialog != null)
            {
                dialogText = taggedDialog.GetComponent<TextMeshProUGUI>();
                if (dialogText != null)
                {
                    Debug.Log($"✅ DialogManager: Texto encontrado por tag en '{sceneName}'");
                    lastError = $"[{sceneName}] Encontrado por tag";
                    return;
                }
            }
        }
        catch { }

        // ESTRATEGIA 3: Buscar por nombre directo
        string[] possibleNames = { "DialogText", "DialogueText", "MessageText", "Dialog", "Dialogue" };
        foreach (string name in possibleNames)
        {
            GameObject textObj = GameObject.Find(name);
            if (textObj != null)
            {
                dialogText = textObj.GetComponent<TextMeshProUGUI>();
                if (dialogText != null)
                {
                    Debug.Log($"✅ DialogManager: Texto encontrado por nombre '{name}' en '{sceneName}'");
                    lastError = $"[{sceneName}] Encontrado: {name}";
                    return;
                }
            }
        }

        // ESTRATEGIA 4: Buscar CUALQUIER TextMeshProUGUI en Canvas
        TextMeshProUGUI[] allTexts = FindObjectsOfType<TextMeshProUGUI>(true);
        if (allTexts.Length > 0)
        {
            // Buscar el primero que esté en un Canvas
            foreach (TextMeshProUGUI txt in allTexts)
            {
                if (txt.GetComponentInParent<Canvas>() != null)
                {
                    dialogText = txt;
                    Debug.LogWarning($"⚠️ DialogManager: Usando primer TextMeshProUGUI encontrado '{txt.name}' en '{sceneName}'");
                    lastError = $"[{sceneName}] Usando fallback: {txt.name}";
                    return;
                }
            }
        }

        // Si llegamos aquí, no encontramos nada
        Debug.LogError($"❌ DialogManager: No se encontró TextMeshProUGUI en escena '{sceneName}'");
        lastError = $"[{sceneName}] ERROR: No encontrado";
    }

    // --- MÉTODO EXISTENTE ---
    public void ShowMessage(string message, float duration = 3f)
    {
        // Verificar que tengamos referencia al texto
        if (dialogText == null)
        {
            FindDialogTextInScene();
            if (dialogText == null)
            {
                Debug.LogError($"❌ DialogManager: No hay TextMeshProUGUI asignado. Mensaje: '{message}'");
                lastError = "ERROR: No text assigned";
                return;
            }
        }

        if (currentDialogCoroutine != null)
        {
            StopCoroutine(currentDialogCoroutine);
            currentDialogCoroutine = null;
        }

        dialogText.text = message;
        lastError = $"Showing: {message.Substring(0, Mathf.Min(20, message.Length))}...";
        currentDialogCoroutine = StartCoroutine(ClearAfterDelay(duration));
    }

    private IEnumerator ClearAfterDelay(float duration)
    {
        yield return new WaitForSeconds(duration);

        if (dialogText != null)
        {
            dialogText.text = "";
        }
        currentDialogCoroutine = null;
    }

    public void ClearMessage()
    {
        if (currentDialogCoroutine != null)
        {
            StopCoroutine(currentDialogCoroutine);
            currentDialogCoroutine = null;
        }

        if (dialogText != null)
        {
            dialogText.text = "";
        }
    }

    // -------------------------------------------------------------------
    // 🆕 MÉTODO NUEVO: SHOW + WAIT
    // -------------------------------------------------------------------
    public IEnumerator ShowAndWait(string message, float duration = 3f)
    {
        // Verificar que tengamos referencia al texto
        if (dialogText == null)
        {
            FindDialogTextInScene();
            if (dialogText == null)
            {
                Debug.LogError($"❌ DialogManager: No hay TextMeshProUGUI asignado. Mensaje: '{message}'");
                lastError = "ERROR: No text in ShowAndWait";
                yield break;
            }
        }

        // Cancelo diálogo previo
        if (currentDialogCoroutine != null)
        {
            StopCoroutine(currentDialogCoroutine);
            currentDialogCoroutine = null;
        }

        dialogText.text = message;
        lastError = $"Wait: {message.Substring(0, Mathf.Min(20, message.Length))}...";

        // Espero el tiempo del diálogo
        yield return new WaitForSeconds(duration);

        if (dialogText != null)
        {
            dialogText.text = "";
        }
    }

    // Debug visual en pantalla (solo para testing en build)
    void OnGUI()
    {
        if (!showDebugInfo) return;

        GUIStyle style = new GUIStyle();
        style.fontSize = 14;
        style.normal.textColor = Color.yellow;
        style.alignment = TextAnchor.UpperLeft;

        string debugInfo = $"DialogManager Debug:\n";
        debugInfo += $"Instance: {(Instance != null ? "OK" : "NULL")}\n";
        debugInfo += $"DialogText: {(dialogText != null ? dialogText.name : "NULL")}\n";
        debugInfo += $"Scene: {UnityEngine.SceneManagement.SceneManager.GetActiveScene().name}\n";
        debugInfo += $"Last: {lastError}";

        GUI.Label(new Rect(10, 10, 400, 100), debugInfo, style);
    }
}
