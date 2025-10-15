using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class DialogManager : MonoBehaviour
{
    public static DialogManager Instance { get; private set; }
    public TextMeshProUGUI dialogText;
    private Queue<(string, float)> messageQueue = new Queue<(string, float)>();
    private bool isShowing = false;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void ShowMessage(string message, float duration = 3f)
    {
        messageQueue.Enqueue((message, duration));
        if (!isShowing)
            StartCoroutine(ProcessQueue());
    }

    private System.Collections.IEnumerator ProcessQueue()
    {
        isShowing = true;
        while (messageQueue.Count > 0)
        {
            var (msg, dur) = messageQueue.Dequeue();
            if (dialogText != null)
                dialogText.text = msg;
            yield return new WaitForSeconds(dur);
        }
        if (dialogText != null)
            dialogText.text = "";
        isShowing = false;
    }

    public void ClearMessage()
    {
        messageQueue.Clear();
        if (dialogText != null)
            dialogText.text = "";
        isShowing = false;
    }
}