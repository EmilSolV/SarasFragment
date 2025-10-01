using UnityEngine;
public class ActivarLuzAlMirar : MonoBehaviour
{
    public GameObject luzVisual; // arrastrás el Point Light aquí

    void Start()
    {
        if (luzVisual != null)
        {
            luzVisual.SetActive(false);
        }
    }

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.gameObject == this.gameObject)
            {
                luzVisual.SetActive(true);
            }
            else
            {
                luzVisual.SetActive(false);
            }
        }
    }
}