using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public float loopDuration = 300f; // 5 minutos
    private float timeRemaining;
    public bool loopActive = false;

    public LoopManager loopManager;

    void Start()
    {
        StartLoopTimer();
    }

    void Update()
    {
        if (loopActive)
        {
            timeRemaining -= Time.deltaTime;
            if (timeRemaining <= 0f)
            {
                loopActive = false;
                timeRemaining = 0f;
                Debug.Log("¡Tiempo terminado! Reiniciando loop.");
                if (loopManager != null)
                {
                    loopManager.StartNewLoop();
                }
            }
        }
    }

    public void StartLoopTimer()
    {
        timeRemaining = loopDuration;
        loopActive = true;
    }

    public float GetTimeRemaining()
    {
        return timeRemaining;
    }
}
