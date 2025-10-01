using UnityEngine;
public class PillowReveal : MonoBehaviour
{
    public Transform targetPosition; // Posición final de la almohada
    public float moveSpeed = 2f;
    private bool hasMoved = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !hasMoved && PlayerIsNear())
        {
            hasMoved = true;
        }

        if (hasMoved)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition.position, moveSpeed * Time.deltaTime);
        }
    }

    bool PlayerIsNear()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player == null) return false;

        float distance = Vector3.Distance(transform.position, player.transform.position);
        return distance < 2f;
    }
}