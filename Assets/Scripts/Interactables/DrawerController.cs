using UnityEngine;

public class DrawerController : MonoBehaviour, IDrawerInteractable
{
    public Transform drawerTransform; // El objeto que se desliza
    public Vector3 openOffset = new Vector3(0, 0, -0.5f); // Dirección y distancia de apertura
    public float openSpeed = 2f;

    private bool isOpen = false;
    private Vector3 closedPos;
    private Vector3 targetPos;

    void Start()
    {
        if (drawerTransform == null) drawerTransform = transform;
        closedPos = drawerTransform.localPosition;
        targetPos = closedPos;
    }

    public void ToggleDrawer()
    {
        isOpen = !isOpen;
        targetPos = isOpen ? closedPos + openOffset : closedPos;

        AudioManager.Instance.PlaySFX(AudioManager.Instance.doorOpenSound, 0.4f);
        Debug.Log("ToggleDrawer ejecutado");
    }

    void Update()
    {
        drawerTransform.localPosition = Vector3.Lerp(drawerTransform.localPosition, targetPos, Time.deltaTime * openSpeed);
        //Debug.Log($"[DrawerController] Posición actual: {drawerTransform.localPosition}, objetivo: {targetPos}");
    }
}
