using UnityEngine;

public class PlayerReturnManager : MonoBehaviour
{
    public Transform player;
    private CharacterController controller;

    void Start()
    {
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player").transform;

        controller = player.GetComponent<CharacterController>();

        if (PlayerReturnData.initialPosition == Vector3.zero)
        {
            PlayerReturnData.initialPosition = player.position;
        }

        Debug.Log("Posición inicial guardada: " + player.position);
    }

    public void MoveToReturnPosition()
    {
        if (controller != null)
            controller.enabled = false;

        Vector3 destino = PlayerReturnData.returnPosition != Vector3.zero
            ? PlayerReturnData.returnPosition
            : PlayerReturnData.initialPosition;

        player.position = destino;
        PlayerReturnData.returnPosition = Vector3.zero;

        if (controller != null)
            controller.enabled = true;
    }

    public void ForceTeleport(Vector3 targetPosition)
    {
        if (controller != null)
            controller.enabled = false;

        player.position = targetPosition;

        if (controller != null)
            controller.enabled = true;
    }
}