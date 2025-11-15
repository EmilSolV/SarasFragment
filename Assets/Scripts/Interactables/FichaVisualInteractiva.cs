using UnityEngine;

public class FichaVisualInteractiva : MonoBehaviour
{
    public Transform puntoConexionEnchufe;
    public float distanciaMaxima = 2f;
    private bool conectada = false;
    private Vector3 posicionInicial;
    private Quaternion rotacionInicial;
    public bool EstaConectada => conectada;

    void Start()
    {
        posicionInicial = transform.position;
        rotacionInicial = transform.rotation;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Esta es la línea que lanza el raycast desde la cámara
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

            // Ejecutamos el raycast
            if (Physics.Raycast(ray, out RaycastHit hit, 2f))
            {
                Debug.Log("Raycast hit: " + hit.collider.name);

                // Verificamos si el objeto impactado es el que queremos
                if (hit.collider.gameObject == gameObject)
                {
                    // Ejecutar acción: conectar o desconectar
                    if (!conectada)
                        ConectarFicha();
                    else
                        DesconectarFicha();
                }
            }
        }
    }

    void ConectarFicha()
    {
        transform.position = puntoConexionEnchufe.position;
        transform.rotation = puntoConexionEnchufe.rotation;
        conectada = true;
        Debug.Log("✅ Ficha conectada al enchufe.");
    }

    void DesconectarFicha()
    {
        transform.position = posicionInicial;
        transform.rotation = rotacionInicial;
        conectada = false;
        Debug.Log("❌ Ficha desenchufada.");
    }
}