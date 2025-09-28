using UnityEngine;

public class PlayerReturnManager : MonoBehaviour
{
    void Start()
    {
        // Solo guardar la posición inicial si aún no se ha guardado
        if (PlayerReturnData.initialPosition == Vector3.zero)
        {
            PlayerReturnData.initialPosition = transform.position;
        }
        Debug.Log("Posición inicial guardada: " + transform.position);
    }

    public void MoveToReturnPosition()
    {
        if (PlayerReturnData.returnPosition != Vector3.zero)
        {
            transform.position = PlayerReturnData.returnPosition;
            PlayerReturnData.returnPosition = Vector3.zero;
        }
        else
        {
            transform.position = PlayerReturnData.initialPosition;
        }
    }
}