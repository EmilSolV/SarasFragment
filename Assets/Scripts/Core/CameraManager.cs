using StarterAssets;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public ThirdPersonController playerController;
    public GameObject mainCamera;
    public GameObject playerMainCamera;
    public GameObject firstPersonCamera;

    private bool isFirstPerson = false;

    void Start()
    {
        SetCameraMode(!isFirstPerson);
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
        Debug.Log($"Cámara activa: {(firstPerson ? "1ra persona" : "3ra persona")}");
    }
}
