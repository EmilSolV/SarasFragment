using UnityEngine;
using TMPro;
using System.Collections;

public class DialogManager : MonoBehaviour
{
    public static DialogManager Instance { get; private set; }
    public TextMeshProUGUI dialogText;

    private Coroutine currentDialogCoroutine;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    // --- MÉTODO EXISTENTE (lo dejo igual) ---
    public void ShowMessage(string message, float duration = 3f)
    {
        if (currentDialogCoroutine != null)
        {
            StopCoroutine(currentDialogCoroutine);
            currentDialogCoroutine = null;
        }

        dialogText.text = message;
        currentDialogCoroutine = StartCoroutine(ClearAfterDelay(duration));
    }

    private IEnumerator ClearAfterDelay(float duration)
    {
        yield return new WaitForSeconds(duration);

        dialogText.text = "";
        currentDialogCoroutine = null;
    }

    public void ClearMessage()
    {
        if (currentDialogCoroutine != null)
        {
            StopCoroutine(currentDialogCoroutine);
            currentDialogCoroutine = null;
        }

        dialogText.text = "";
    }


    // -------------------------------------------------------------------
    // 🆕 MÉTODO NUEVO: SHOW + WAIT (para poder secuenciar diálogos fácil)
    // -------------------------------------------------------------------
    public IEnumerator ShowAndWait(string message, float duration = 3f)
    {
        // Cancelo diálogo previo
        if (currentDialogCoroutine != null)
        {
            StopCoroutine(currentDialogCoroutine);
            currentDialogCoroutine = null;
        }

        dialogText.text = message;

        // Espero el tiempo del diálogo
        yield return new WaitForSeconds(duration);

        dialogText.text = "";
    }
}
