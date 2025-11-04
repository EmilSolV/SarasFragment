using UnityEngine;

public class CableVisual : MonoBehaviour
{
    public Transform extremoFijo;
    public Transform extremoLibre;
    public LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer.positionCount = 2;
        lineRenderer.startWidth = 0.02f;
        lineRenderer.endWidth = 0.02f;
        lineRenderer.material.color = Color.black;
    }

    void Update()
    {
        if (extremoFijo != null && extremoLibre != null)
        {
            lineRenderer.SetPosition(0, extremoFijo.position);
            lineRenderer.SetPosition(1, extremoLibre.position);
        }
    }
}
