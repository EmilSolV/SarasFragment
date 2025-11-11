using UnityEngine;

public class ReveladorUV : MonoBehaviour
{
    public Light luzUV;
    public float distanciaActivacion = 3f;

    private Renderer textoRenderer;

    void Start()
    {
        textoRenderer = GetComponent<Renderer>();
        if (textoRenderer != null)
        {
            textoRenderer.enabled = false;
            Debug.Log("Renderer inicializado y oculto");
        }
    }

    void Update()
    {
        if (luzUV == null || textoRenderer == null)
        {
            Debug.LogWarning("Faltan referencias");
            return;
        }

        float distancia = Vector3.Distance(transform.position, luzUV.transform.position);

        if (luzUV.enabled && distancia < distanciaActivacion)
        {
            textoRenderer.enabled = true;
        }
        else
        {
            textoRenderer.enabled = false;
        }
    }
}