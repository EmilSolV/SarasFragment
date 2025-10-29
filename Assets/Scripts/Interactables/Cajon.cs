using UnityEngine;

public class Cajon : MonoBehaviour, IInteractable
{
    public Transform openPosition;
    public Transform closedPosition;
    public float speed = 2f;
    public Transform player;
    public float activationDistance = 2f;

    private bool isOpen = false;

    void Awake()
    {
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    void Update()
    {
        float distance = Vector3.Distance(player.position, transform.position);
        if (distance < activationDistance && Input.GetMouseButtonDown(0))
        {
            isOpen = !isOpen;
        }

        Vector3 targetPos = isOpen ? openPosition.localPosition : closedPosition.localPosition;
        transform.localPosition = Vector3.Lerp(transform.localPosition, targetPos, Time.deltaTime * speed);
    }

    public void Interact()
    {
        isOpen = !isOpen;
    }
}