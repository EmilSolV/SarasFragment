using UnityEngine;

public class Collectable : MonoBehaviour, IGrabbable
{
    [TextArea]
    public string collectMessage = "Recuerdo recolectado: polaroid.\nEsto parece una foto de Sara peleando con alguien...";
    //public string doorMessage = "Eso se escuchó como una puerta desbloqueandose.";
    public string doorMessage = null;

    public virtual void OnGrab(Transform handPoint)
    {
        // Mostrar mensaje de recuerdo usando ShowMessage (no ShowAndWait)
        if (DialogManager.Instance != null)
        {
            DialogManager.Instance.ShowMessage(collectMessage, 4f);
            
            // Si hay un mensaje de puerta, mostrarlo también
            if (!string.IsNullOrEmpty(doorMessage))
            {
                // Iniciar corrutina para mostrar el segundo mensaje después del primero
                StartCoroutine(ShowDoorMessageDelayed());
            }
        }

        // Desactivar o destruir la polaroid en el mundo
        gameObject.SetActive(false);
        // Alternativamente: Destroy(gameObject);
    }

    private System.Collections.IEnumerator ShowDoorMessageDelayed()
    {
        // Esperar a que termine el primer mensaje
        yield return new UnityEngine.WaitForSeconds(4f);
        
        // Mostrar el segundo mensaje
        if (DialogManager.Instance != null)
        {
            DialogManager.Instance.ShowMessage(doorMessage, 2f);
        }
    }

    public virtual void OnDrop()
    {
        // No hace nada, la polaroid no se puede soltar
    }
}