using UnityEngine;

public class FPCamera : MonoBehaviour
{
    public Transform playerBody;
    public float sensibilidadX = 200f;
    public float sensibilidadY = 200f;

    private float xRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensibilidadX * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensibilidadY * Time.deltaTime;

        // Rotación vertical (cámara)
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -60f, 60f);

        // Solo aplicamos rotación vertical a la cámara
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Rotación horizontal al cuerpo del jugador
        playerBody.Rotate(Vector3.up * mouseX);
    }
}