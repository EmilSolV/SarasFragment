using UnityEngine;

public class VasoSlot : MonoBehaviour
{
    public int slotID; // ID único del slot
    public bool ocupado = false;

    public void ColocarTaza(VasoInteractivo taza)
    {
        if (ocupado) return;

        ocupado = true;
        taza.estaColocado = true;

        taza.transform.position = transform.position;
        taza.transform.rotation = transform.rotation;

        Rigidbody rb = taza.GetComponent<Rigidbody>();
        if (rb != null) rb.isKinematic = true;

        Collider col = taza.GetComponent<Collider>();
        if (col != null) col.enabled = false;
    }
}
