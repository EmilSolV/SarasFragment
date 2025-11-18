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

    public void ShowMessage(string message, float duration = 3f)
    {
        // Si ya hay un diálogo, lo corto al instante
        if (currentDialogCoroutine != null)
        {
            StopCoroutine(currentDialogCoroutine);
            currentDialogCoroutine = null;
        }

        // Muestro el nuevo mensaje ya mismo
        dialogText.text = message;

        // Inicio coroutine para limpiar después del tiempo
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
}
