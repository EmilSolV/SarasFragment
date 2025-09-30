using UnityEngine;

public class Grabbable : MonoBehaviour
{
    private Vector3 initialPosition;
    private Quaternion initialRotation;

    void Start()
    {
        // Guardar la posición y rotación al inicio
        initialPosition = transform.position;
        initialRotation = transform.rotation;
    }

    public void ResetObject()
    {
        // Restaurar la posición y rotación
        transform.position = initialPosition;
        transform.rotation = initialRotation;

        // Si tenía algún parent (por ejemplo la mano), se lo quitamos
        transform.SetParent(null);
    }
}
