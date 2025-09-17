using UnityEngine;
using UnityEngine.SceneManagement;

public class InspectTrigger : MonoBehaviour
{
    public GameObject objectPrefab;
    private bool playerInRange = false;

    //void Update()
    //{
    // if (playerInRange && Input.GetKeyDown(KeyCode.E))
    //{
    // if (objectPrefab != null)
    //{
    //PlayerReturnData.returnPosition = GameObject.FindWithTag("Player").transform.position;
    //InspectionData.objectToInspect = objectPrefab;
    //  SceneManager.LoadScene("InspectScene");
    //}
    //  else
    //{
    //      Debug.LogWarning("No se asign� ning�n Prefab al campo objectPrefab.");
    //    }
    //  }
    //}

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (objectPrefab != null)
            {
                PlayerReturnData.returnPosition = GameObject.FindWithTag("Player").transform.position;
                InspectionData.objectToInspect = objectPrefab;
                GameObject.Find("FadeCanvas").GetComponent<SceneFader>().FadeToScene("InspectObjectScene");
            }
        }
    }



    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}




