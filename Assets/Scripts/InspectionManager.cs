using UnityEngine;
using UnityEngine.SceneManagement;

public class InspectionManager : MonoBehaviour
{
    public Transform inspectDisplay;
    private GameObject currentObject;

    void Start()
    {
        if (InspectionData.objectToInspect != null)
        {
            currentObject = Instantiate(InspectionData.objectToInspect, inspectDisplay);
            currentObject.transform.localPosition = Vector3.zero;
            currentObject.transform.localRotation = Quaternion.identity;
        }
        else
        {
            Debug.LogWarning("no se recibio ningun objeto para inspeccionar.");
        }
    }

    void Update()
    {
        if (currentObject != null)
        {
            float rotX = Input.GetAxis("Mouse X") * 100 * Time.deltaTime;
            float rotY = Input.GetAxis("Mouse Y") * 100 * Time.deltaTime;

            currentObject.transform.Rotate(Vector3.up, -rotX, Space.World);
            currentObject.transform.Rotate(Vector3.right, rotY, Space.World);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Tercerapersonacam", LoadSceneMode.Single);
        }
    }
}


