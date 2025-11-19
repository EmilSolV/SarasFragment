using UnityEngine;


public class MesaRaycast : MonoBehaviour
{
    [Header("Configuración")]
    public Camera mainCamera;
    public float rayDistance = 3f;
    public LayerMask slotLayer;

    [Header("Inventario simple")]
    public VasoInteractivo tazaEnMano; // Se asigna cuando el jugador agarra una taza

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Click izquierdo
        {
            Ray ray = mainCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, rayDistance, slotLayer))
            {
                VasoSlot slot = hit.collider.GetComponent<VasoSlot>();
                if (slot != null && tazaEnMano != null)
                {
                    slot.ColocarTaza(tazaEnMano);
                    tazaEnMano = null; // Liberar la mano
                }
            }
        }
    }
}
