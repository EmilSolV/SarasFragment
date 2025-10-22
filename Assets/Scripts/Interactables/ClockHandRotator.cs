using UnityEngine;

public class ClockHandRotator : MonoBehaviour
{
    public float hoursPerMinute = 12f;
    public float secondsPerHour = 5f;

    void Update()
    {
        float degreesPerSecond = 360f / (hoursPerMinute * secondsPerHour);
        transform.Rotate(Vector3.forward, -degreesPerSecond * Time.deltaTime);
    }
}