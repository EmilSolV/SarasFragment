using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    private Vector3 initialPosition;
    private Quaternion initialRotation;

    public bool resetOnLoop = true; // Puedes cambiar esto según el estado del puzzle

    void Start()
    {
        initialPosition = transform.position;
        initialRotation = transform.rotation;
    }

    public void ResetToInitial()
    {
        transform.position = initialPosition;
        transform.rotation = initialRotation;
    }

    // Puedes añadir métodos para cambiar el estado del puzzle
    public void SetPuzzleSolved(bool solved)
    {
        resetOnLoop = !solved;
    }
}
