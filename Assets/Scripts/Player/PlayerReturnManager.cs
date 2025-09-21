using UnityEngine;

public class PlayerReturnManager : MonoBehaviour
{
    void Start()
    {
        if (PlayerReturnData.returnPosition != Vector3.zero)
        {
            transform.position = PlayerReturnData.returnPosition;
            PlayerReturnData.returnPosition = Vector3.zero; // Limpiar para evitar errores futuros
        }
    }
}