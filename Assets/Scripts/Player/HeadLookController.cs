using UnityEngine;
public class HeadLookController : MonoBehaviour
{
    public Transform headBone;
    public Transform cameraTarget;
    public float rotationSpeed = 5f;
    public float maxVerticalAngle = 30f;
    public float maxHorizontalAngle = 60f;

    void LateUpdate()
    {
        if (headBone == null || cameraTarget == null) return;

        Vector3 lookDirection = cameraTarget.forward;
        Quaternion targetRotation = Quaternion.LookRotation(lookDirection);

        Quaternion limitedRotation = Quaternion.Euler(
            Mathf.Clamp(targetRotation.eulerAngles.x, -maxVerticalAngle, maxVerticalAngle),
            Mathf.Clamp(targetRotation.eulerAngles.y, -maxHorizontalAngle, maxHorizontalAngle),
            0f
        );

        headBone.localRotation = Quaternion.Slerp(
            headBone.localRotation,
            limitedRotation,
            Time.deltaTime * rotationSpeed
        );
    }
}