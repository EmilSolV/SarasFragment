using UnityEngine;

public class DoorController : MonoBehaviour, IDoorInteractable
{
    public Transform pivot;
    public Vector3 openRotation = new Vector3(0, 90, 0);
    public float openSpeed = 2f;

    private bool isOpen = false;
    private Quaternion closedRot;
    private Quaternion targetRot;

    void Start()
    {
        if (pivot == null) pivot = transform;
        closedRot = pivot.rotation;
        targetRot = closedRot;
    }

    public void ToggleDoor()
    {
        isOpen = !isOpen;
        targetRot = isOpen ? Quaternion.Euler(pivot.eulerAngles + openRotation) : closedRot;
        Debug.Log("ToggleDoor ejecutado");
    }

    void Update()
    {
        pivot.rotation = Quaternion.Lerp(pivot.rotation, targetRot, Time.deltaTime * openSpeed);
    }
}