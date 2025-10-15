using UnityEngine;

public class Collectable : MonoBehaviour, IGrabbable
{
    [TextArea]
    public string collectMessage = "Recuerdo recolectado: polaroid.\nEsto parece una foto de Sara peleando con alguien...";

    public virtual void OnGrab(Transform handPoint)
    {
        // Mostrar mensaje de recuerdo
        DialogManager.Instance.ShowMessage(collectMessage, 6f);
        DialogManager.Instance.ShowMessage("Parece que se ha desbloqueado una puerta.", 5f);

        // Desactivar o destruir la polaroid en el mundo
        gameObject.SetActive(false);
        // Alternativamente: Destroy(gameObject);
    }

    public virtual void OnDrop()
    {
        // No hace nada, la polaroid no se puede soltar
    }
}