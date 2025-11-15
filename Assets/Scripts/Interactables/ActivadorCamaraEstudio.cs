using UnityEngine;

public class ActivadorCamaraEstudio : MonoBehaviour
{
    public CamaraEstudioInteractiva camaraEstudio;

    void OnMouseDown()
    {
        if (camaraEstudio != null)
        {
            camaraEstudio.ActivarInspeccion();
        }
    }
}
