using UnityEngine;

public class TPCamera : MonoBehaviour
{
    [Header("Referencias")]
    public Transform target;         // El jugador (posición a seguir)
    public Transform playerBody;     // El cuerpo del jugador para rotarlo

    [Header("Offset detrás del jugador")]
    public Vector3 offset = new Vector3(0f, 1.6f, -3f);

    [Header("Sensibilidad")]
    public float sensibilidadX = 200f;
    public float sensibilidadY = 200f;

    [Header("Límites de pitch")]
    public float minPitch = -30f;
    public float maxPitch = 60f;

    [Header("Suavizado")]
    public float smoothTime = 0.05f;

    private float pitch = 10f;
    private Vector3 velocity = Vector3.zero;

    [Header("Recorte por obstáculos")]
    public LayerMask obstacleMask;
    public float minDistance = 0.5f;
    public float maxDistance = 3f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void LateUpdate()
    {
        if (target == null || playerBody == null) return;

        // Input de mouse
        float mouseX = Input.GetAxis("Mouse X") * sensibilidadX * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensibilidadY * Time.deltaTime;

        // Rotar jugador horizontalmente (yaw)
        playerBody.Rotate(Vector3.up * mouseX);

        // Calcular pitch (rotación vertical de la cámara)
        pitch -= mouseY;
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);

        // Obtener rotación actual del jugador
        Quaternion yawRotation = Quaternion.Euler(0f, playerBody.eulerAngles.y, 0f);
        Quaternion finalRotation = Quaternion.Euler(pitch, playerBody.eulerAngles.y, 0f);

        // Posición deseada detrás del jugador
        //Vector3 desiredPos = target.position + yawRotation * offset;

        Vector3 baseOffset = yawRotation * offset;
        Vector3 desiredPos = target.position + baseOffset;

        if (Physics.Raycast(target.position, baseOffset.normalized, out RaycastHit hit, baseOffset.magnitude, obstacleMask))
        {
            float clippedDistance = Mathf.Clamp(hit.distance, minDistance, maxDistance);
            desiredPos = target.position + baseOffset.normalized * clippedDistance;
        }

        // 🔧Ajuste de altura mínima(cintura para arriba)
float minHeight = target.position.y + 1.2f; // altura relativa a la cintura
        if (desiredPos.y < minHeight)
        {
            desiredPos.y = minHeight;
        }





        // Suavizado estable
        transform.position = Vector3.SmoothDamp(transform.position, desiredPos, ref velocity, smoothTime);

        // Aplicar rotación vertical + horizontal
        transform.rotation = finalRotation;
    }
}
