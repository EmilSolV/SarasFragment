using UnityEngine;

public class Grabbable : MonoBehaviour, IGrabbable
{
    public bool resetOnLoop = true;
    public string onGrabMessage = "";

    [Header("Sonidos Personalizados (Opcionales)")]
    public AudioClip customPickupSound;
    public AudioClip customHitGroundSound;

    private AudioClip pickupSound;
    private AudioClip hitGroundSound;
    private Vector3 initialPosition;
    private Quaternion initialRotation;
    private Rigidbody rb;
    private bool isFirstTimeGrabbed = true;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        // Guardar la posición y rotación al inicio
        initialPosition = transform.position;
        initialRotation = transform.rotation;

        rb = GetComponent<Rigidbody>();

        // Si no hay sonidos personalizados, usar los del AudioManager
        pickupSound = customPickupSound != null ? customPickupSound : AudioManager.Instance.grabSound;
        hitGroundSound = customHitGroundSound != null ? customHitGroundSound : AudioManager.Instance.hitFloorSound;
    }

    public virtual void OnGrab(Transform handPoint)
    {
        if (rb != null) rb.isKinematic = true;
        transform.SetParent(handPoint);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;

        if (pickupSound != null)
            AudioManager.Instance.PlaySFX(pickupSound);

        if (rb != null)
            rb.isKinematic = true;

        if (isFirstTimeGrabbed)
        {
            if (!string.IsNullOrEmpty(onGrabMessage))
                DialogManager.Instance.ShowMessage(onGrabMessage, 5f);
            isFirstTimeGrabbed = false;
        }
        MetricManager.Instance.RegistrarInteraccionObjeto(this.name);
    }

    public virtual void OnDrop()
    {
        if (rb != null) rb.isKinematic = false;
        transform.SetParent(null);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Evitar sonido al agarrarlo (mientras está en la mano)
        if (rb != null && rb.isKinematic) return;

        if (hitGroundSound != null)
            AudioManager.Instance.PlaySFX(hitGroundSound);
    }

    public virtual void ResetObject()
    {
        // Restaurar la posición y rotación
        transform.position = initialPosition;
        transform.rotation = initialRotation;

        // Si tenía algún parent (por ejemplo la mano), se lo quitamos
        transform.SetParent(null);

        // Reactivar física si corresponde
        if (rb != null) rb.isKinematic = false;
    }

    public void SetPuzzleSolved(bool solved)
    {
        resetOnLoop = !solved;
    }

}