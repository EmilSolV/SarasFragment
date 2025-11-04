using UnityEngine;

public class FPCamera : MonoBehaviour
{
    public Transform playerBody;
    public float mouseSensitivity = 2f;
    private float xRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -25f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }
}





//using UnityEngine;

//public class FPCamera : MonoBehaviour
//{
//    public Transform playerBody;
//    public float mouseSensitivity = 2f;
//    private float xRotation = 0f;

//    void Start()
//    {
//        Cursor.lockState = CursorLockMode.Locked;
//    }

//    void LateUpdate()
//    {
//        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
//        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

//        xRotation -= mouseY;
//        xRotation = Mathf.Clamp(xRotation, -25f, 90f);

//        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
//        playerBody.Rotate(Vector3.up * mouseX);
//    }
//}
