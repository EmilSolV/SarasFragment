using UnityEngine;

public class RevealableMessage : MonoBehaviour
{
    public Light uvLight; // Asignalo en el Inspector
    public GameObject hiddenMessage;
    public Transform detectionPoint; // Asignalo en el Inspector
    void Update()
    {
        if (uvLight != null && hiddenMessage != null && IsIlluminated())
        {
            hiddenMessage.SetActive(true);
        }
        else
        {
            hiddenMessage.SetActive(false);
        }
    }

    bool IsIlluminated()
    {
        Vector3 dirToMessage = detectionPoint.position - uvLight.transform.position;
        float angle = Vector3.Angle(uvLight.transform.forward, dirToMessage);
        float distance = dirToMessage.magnitude;

        return angle < uvLight.spotAngle / 2 && distance < uvLight.range;
    }

}