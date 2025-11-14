using UnityEngine;

public class ReveladorPorRaycast : MonoBehaviour
{
    public float distanciaMaxima = 5f;
    public string tagNumero = "NumeroUV";

    private Renderer ultimoRenderer;

    void Update()
    {
        Light luz = GetComponent<Light>();
        if (luz == null || !luz.enabled)
            return;

        Ray ray = new Ray(transform.position, transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, distanciaMaxima))
        {
            if (hit.collider.CompareTag(tagNumero))
            {
                Renderer rend = hit.collider.GetComponent<Renderer>();
                if (rend != null)
                {
                    if (!rend.enabled)
                    {
                        rend.enabled = true;
                        Debug.Log("Número revelado por raycast de luz.");
                    }
                    ultimoRenderer = rend;
                    return;
                }
            }
        }

        // Si no golpea el número, lo ocultamos
        if (ultimoRenderer != null && ultimoRenderer.enabled)
        {
            ultimoRenderer.enabled = false;
            Debug.Log("Número ocultado porque la luz se alejó.");
            ultimoRenderer = null;
        }
    }
}