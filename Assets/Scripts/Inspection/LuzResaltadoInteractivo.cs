using UnityEngine;
public class LuzResaltadoInteractivo : MonoBehaviour
{
    public GameObject luzVisual;

    void Start()
    {
        if (luzVisual != null)
        {
            luzVisual.SetActive(false);
        }
    }

    void Update()
    {
        Camera cam = Camera.main; // Siempre apunta a la cámara activa con el tag "MainCamera"
        if (cam == null) return;

        Ray ray = cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.gameObject == this.gameObject)
            {
                if (luzVisual != null)
                    luzVisual.SetActive(true);
            }
            else
            {
                if (luzVisual != null)
                    luzVisual.SetActive(false);
            }
        }
        else
        {
            if (luzVisual != null)
                luzVisual.SetActive(false);
        }
    }
}