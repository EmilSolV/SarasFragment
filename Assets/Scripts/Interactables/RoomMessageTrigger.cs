using UnityEngine;

public class RoomMessageTrigger : MonoBehaviour
{
    [TextArea]
    public string message = "Aquí es donde pondria un segundo puzzle, si tuviera uno... Gracias por jugar! :)";

    public float duration = 15f;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            DialogManager.Instance.ShowMessage(message, duration);
        }
    }
}