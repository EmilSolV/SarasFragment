using StarterAssets;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public ThirdPersonController playerController;

    public GameObject mainCamera;
    public GameObject playerMainCamera;
    public GameObject firstPersonCamera;

    public Transform playerCameraTransform; // Referencia al transform de la cámara que se mueve

    private bool isFirstPerson = false;

    void Start()
    {
        SetCameraMode(isFirstPerson);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            isFirstPerson = !isFirstPerson;
            SetCameraMode(isFirstPerson);
        }
    }

    void SetCameraMode(bool firstPerson)
    {
        firstPersonCamera.SetActive(firstPerson);
        mainCamera.SetActive(!firstPerson);
        playerMainCamera.SetActive(!firstPerson);

        playerController.isFirstPerson = firstPerson;

        if (playerCameraTransform != null)
        {
            if (firstPerson)
            {
                // Vista en primera persona (cámara en la cabeza)
                playerCameraTransform.localPosition = new Vector3(0f, 1.6f, 0f);
                playerCameraTransform.localRotation = Quaternion.Euler(0f, 0f, 0f);
            }
            else
            {
                // Vista en tercera persona estilo Alan Wake (cámara detrás y a un costado)
                playerCameraTransform.localPosition = new Vector3(0.5f, 1.6f, -2.5f);
                playerCameraTransform.localRotation = Quaternion.Euler(10f, 0f, 0f);
            }
        }

        Debug.Log($"Cámara activa: {(firstPerson ? "1ra persona" : "3ra persona")}");
    }
}

//public class CameraManager : MonoBehaviour
//{
//    public ThirdPersonController playerController;
//    public GameObject mainCamera;
//    public GameObject playerMainCamera;
//    public GameObject firstPersonCamera;

//    private bool isFirstPerson = false;

//    void Start()
//    {
//        SetCameraMode(!isFirstPerson);
//    }

//    void Update()
//    {
//        if (Input.GetKeyDown(KeyCode.P))
//        {
//            isFirstPerson = !isFirstPerson;
//            SetCameraMode(isFirstPerson);
//        }
//    }

//    void SetCameraMode(bool firstPerson)
//    {
//        firstPersonCamera.SetActive(firstPerson);
//        mainCamera.SetActive(!firstPerson);
//        playerMainCamera.SetActive(!firstPerson);
//        playerController.isFirstPerson = firstPerson;
//        Debug.Log($"Cámara activa: {(firstPerson ? "1ra persona" : "3ra persona")}");
//    }
//}
