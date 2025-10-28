using UnityEngine;

public class ActivarCamaraVelador : MonoBehaviour
{
    public VeladorInteraction veladorInteraction;

    void OnMouseDown()
    {
        float distancia = Vector3.Distance(Camera.main.transform.position, transform.position);
        if (distancia <= veladorInteraction.distanciaInteraccion)
        {
            veladorInteraction.ActivarInspeccion();
        }
    }
}
