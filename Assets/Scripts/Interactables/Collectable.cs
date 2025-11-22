using UnityEngine;

public class Collectable : MonoBehaviour, IGrabbable
{
    [TextArea]
    public string collectMessage = "Recuerdo recolectado: polaroid.\nEsto parece una foto de Sara peleando con alguien...";
    //public string doorMessage = "Eso se escuchó como una puerta desbloqueandose.";
    public string doorMessage = null;

    public virtual void OnGrab(Transform handPoint)
    {
        // Mostrar mensaje de recuerdo
        DialogManager.Instance.ShowAndWait(collectMessage, 4f);
        DialogManager.Instance.ShowAndWait(doorMessage, 2f);

        // Desactivar o destruir la polaroid en el mundo
        gameObject.SetActive(false);
        // Alternativamente: Destroy(gameObject);
    }

    public virtual void OnDrop()
    {
        // No hace nada, la polaroid no se puede soltar
    }
}