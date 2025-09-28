using UnityEngine;

public class LoopManager : MonoBehaviour
{
    public int currentLoop = 1;
    public int maxLoops = 5;
    public TimeManager timeManager; // Referencia al TimeManager
    public PlayerReturnManager playerReturnManager; // Referencia al PlayerReturnManager

    public void StartNewLoop()
    {
        if (currentLoop < maxLoops)
        {
            currentLoop++;
            Debug.Log($"Iniciando loop {currentLoop}");

            // Reinicia la posición del jugador
            PlayerReturnData.returnPosition = PlayerReturnData.initialPosition;
            if (playerReturnManager != null)
            {
                playerReturnManager.MoveToReturnPosition();
            }

            // Reinicia el timer
            if (timeManager != null)
            {
                timeManager.StartLoopTimer();
            }
        }
        else
        {
            Debug.Log("Se alcanzó el máximo de loops.");
        }
    }

    public void ResetLoops()
    {
        currentLoop = 1;
        Debug.Log("Loops reiniciados.");
    }
}
