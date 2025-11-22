using UnityEngine;
using TMPro;
using System.Collections;

/// <summary>
/// Versión simplificada del DialogManager sin DontDestroyOnLoad
/// Usa esta versión si tienes problemas con el DialogManager persistente
/// </summary>
public class DialogManagerSimple : MonoBehaviour
{
    public static DialogManagerSimple Instance { get; private set; }

    [Header("Asignar en el Inspector")]
    public TextMeshProUGUI dialogText;

    private Coroutine currentDialogCoroutine;

    void Awake()
    {
        // No usa DontDestroyOnLoad - cada escena tiene su propio manager
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    public void ShowMessage(string message, float duration = 3f)
    {
        if (dialogText == null)
        {
            Debug.LogError("? DialogManagerSimple: dialogText no asignado en el Inspector!");
            return;
        }

        if (currentDialogCoroutine != null)
        {
            StopCoroutine(currentDialogCoroutine);
        }

        dialogText.text = message;
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

    public IEnumerator ShowAndWait(string message, float duration = 3f)
    {
        if (dialogText == null)
        {
            Debug.LogError("? DialogManagerSimple: dialogText no asignado!");
            yield break;
        }

        if (currentDialogCoroutine != null)
        {
            StopCoroutine(currentDialogCoroutine);
            currentDialogCoroutine = null;
        }

        dialogText.text = message;
        yield return new WaitForSeconds(duration);

        if (dialogText != null)
        {
            dialogText.text = "";
        }
    }
}
