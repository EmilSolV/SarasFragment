using UnityEngine;

public class DoorController : MonoBehaviour, IDoorInteractable
{
    public Transform pivot;
    public Vector3 openRotation = new Vector3(0, 90, 0);
    public float openSpeed = 2f;

    [Header("Bloqueo de puerta")]
    public bool isLocked = true; // Empieza bloqueada

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
        if (isLocked)
        {
            DialogManager.Instance.ShowMessage("La puerta está bloqueada. Resuelve el puzzle para abrirla.", 3f);
            AudioManager.Instance.PlaySFX(AudioManager.Instance.doorLockedSound, 0.2f);
            return;
        }

        isOpen = !isOpen;
        targetRot = isOpen ? Quaternion.Euler(pivot.eulerAngles + openRotation) : closedRot;
        AudioManager.Instance.PlaySFX(AudioManager.Instance.doorOpenSound, 0.4f);
        Debug.Log("ToggleDoor ejecutado");
    }

    public void UnlockDoor()
    {
        isLocked = false;
        //DialogManager.Instance.ShowMessage("¡Puerta desbloqueada!", 3f);
        AudioManager.Instance.PlaySFX(AudioManager.Instance.doorUnlockedSound, 0.2f);
        Debug.Log("Puerta desbloqueada");
    }

    void Update()
    {
        pivot.rotation = Quaternion.Lerp(pivot.rotation, targetRot, Time.deltaTime * openSpeed);
    }
}