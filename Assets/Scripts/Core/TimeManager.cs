using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static TimeManager Instance { get; private set; }
    public float loopDuration = 10f; 
    private float timeRemaining;
    public bool loopActive = false;
    public bool final = false;
    public LoopManager loopManager;

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
        if(final)
        {
            timeRemaining -= Time.deltaTime;
            loopActive = false;
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

    /// <summary>
    /// Establece un nuevo tiempo restante para el temporizador del loop
    /// </summary>
    /// <param name="newTime">Nuevo tiempo en segundos</param>
    public void SetTimeRemaining(float newTime, bool isFinal = false)
    {
        final = isFinal;
        timeRemaining = Mathf.Max(0f, newTime);
    }

    /// <summary>
    /// Añade o resta tiempo al temporizador actual del loop
    /// </summary>
    /// <param name="timeToAdd">Cantidad de tiempo a añadir (puede ser negativo para restar)</param>
    public void AddTime(float timeToAdd)
    {
        timeRemaining = Mathf.Max(0f, timeRemaining + timeToAdd);
    }
}
