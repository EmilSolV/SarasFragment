using UnityEngine;

public class Grabbable : MonoBehaviour, IGrabbable
{
    private Vector3 initialPosition;
    private Quaternion initialRotation;
    private Rigidbody rb;
    public bool resetOnLoop = true; // Puedes cambiar esto según el estado del puzzle

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        // Guardar la posición y rotación al inicio
        initialPosition = transform.position;
        initialRotation = transform.rotation;
    }

    public virtual void OnGrab(Transform handPoint)
    {
        if (rb != null) rb.isKinematic = true;
        transform.SetParent(handPoint);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
    }

    public virtual void OnDrop()
    {
        if (rb != null) rb.isKinematic = false;
        transform.SetParent(null);
    }

    public virtual void ResetObject()
    {
        // Restaurar la posición y rotación
        transform.position = initialPosition;
        transform.rotation = initialRotation;

        // Si tenía algún parent (por ejemplo la mano), se lo quitamos
        transform.SetParent(null);

        // Reactivar física si corresponde
        if (rb != null) rb.isKinematic = false;
    }

    public void SetPuzzleSolved(bool solved)
    {
        resetOnLoop = !solved;
    }
}