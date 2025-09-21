using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;


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
            StartCoroutine(FadeAndReturn());
            SceneTransitionionData.comingFromInspection = true;
            SceneManager.LoadScene("MainScene", LoadSceneMode.Single);
        }

    }

    IEnumerator FadeAndReturn()
    {
        SceneTransitionionData.comingFromInspection = true;
        GameObject fadeCanvas = GameObject.Find("FadeCanvas");
        if (fadeCanvas != null)
        {
            Image fadeimage = fadeCanvas.GetComponent<SceneFader>().fadeImage;
            float t = 0f;
            float duration = 1f;

            while (t < duration)
            {
                t += Time.deltaTime;
                fadeimage.color = new Color(0, 0, 0, t / duration);
                yield return null;
            }



        }

        yield return new WaitForSeconds(0.2f);

        SceneManager.LoadScene("MainScene");


    }























}



