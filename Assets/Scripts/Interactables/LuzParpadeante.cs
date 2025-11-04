using UnityEngine;

public class LuzParpadeante : MonoBehaviour
{
    public Light luz;
    public float velocidadParpadeo = 2f;
    public bool activa = true;

    void Update()
    {
        if (activa && luz != null)
        {
            float intensidad = Mathf.PingPong(Time.time * velocidadParpadeo, luz.intensity);
            luz.intensity = intensidad;
        }
    }

    public void Apagar()
    {
        activa = false;
        if (luz != null)
            luz.enabled = false;
    }
}