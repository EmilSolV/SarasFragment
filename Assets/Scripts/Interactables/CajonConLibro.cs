using UnityEngine;
public class CajonConLibro : MonoBehaviour
{ 
           public interface IInteractable
            {
              void Interact();
             }

    public Transform posicionAbierta;
    public Transform posicionCerrada;
    public GameObject libro;
    public Transform slotPuzzle;
    public float velocidad = 2f;
    public Transform jugador; // arrastrás el objeto del jugador en el Inspector
    public float distanciaActivacion = 2f; // ajustás según tu escena

    private bool abierto = false;
    private bool libroTomado = false;

    void Update()
    {
        // Abrir o cerrar el cajón con la tecla E
        float distancia = Vector3.Distance(jugador.position, transform.position);

        if (distancia < distanciaActivacion && Input.GetKeyDown(KeyCode.E))
        {
            abierto = !abierto;
        }

        // Movimiento suave del cajón
        transform.localPosition = Vector3.Lerp(
            transform.localPosition,
            abierto ? posicionAbierta.localPosition : posicionCerrada.localPosition,
            Time.deltaTime * velocidad
        );

        // Tomar el libro con clic si el cajón está abierto
        if (abierto && !libroTomado && Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider.gameObject == libro)
                {
                    libro.transform.SetParent(slotPuzzle);
                    libro.transform.position = slotPuzzle.position;
                    libro.transform.rotation = slotPuzzle.rotation;
                    libroTomado = true;
                }
            }
        }
    }
}