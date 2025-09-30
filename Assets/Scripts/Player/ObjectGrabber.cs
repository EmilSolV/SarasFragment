using UnityEngine;

public class ObjectGrabber : MonoBehaviour
{
    public Transform handPoint;        // punto de la mano
    public float grabRange = 2f;       // distancia máxima para agarrar
    public LayerMask grabLayer;        // capa de objetos agarrables

    private GameObject heldObject;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (heldObject == null)
            {
                TryGrab();
            }
            else
            {
                Drop();
            }
        }
    }

    void TryGrab()
    {
        // Raycast hacia adelante desde el jugador
        Ray ray = new Ray(transform.position + Vector3.up * 1f, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, grabRange, grabLayer))
        {
            GameObject obj = hit.collider.gameObject;

            heldObject = obj;
            Rigidbody rb = heldObject.GetComponent<Rigidbody>();
            if (rb != null) rb.isKinematic = true;

            heldObject.transform.SetParent(handPoint);
            heldObject.transform.localPosition = Vector3.zero;
            heldObject.transform.localRotation = Quaternion.identity;
        }
    }

    void Drop()
    {
        if (heldObject != null)
        {
            Rigidbody rb = heldObject.GetComponent<Rigidbody>();
            if (rb != null) rb.isKinematic = false;

            heldObject.transform.SetParent(null);
            heldObject = null;
        }
    }
}
