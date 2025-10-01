using UnityEngine;
public class ResaltadoInteractivo : MonoBehaviour
{
    public GameObject indicadorVisual;

    void Start()
    {
        if (indicadorVisual != null)
        {
            indicadorVisual.SetActive(false);
        }
    }

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.gameObject == this.gameObject)
            {
                indicadorVisual.SetActive(true);
            }
            else
            {
                indicadorVisual.SetActive(false);
            }
        }
    }
}