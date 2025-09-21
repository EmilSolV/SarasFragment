using UnityEngine;

public class LoopManager : MonoBehaviour
{
    public int currentLoop = 1;
    public int maxLoops = 5;
    public TimeManager timeManager; // Referencia al TimeManager

    public void StartNewLoop()
    {
        if (currentLoop < maxLoops)
        {
            currentLoop++;
            Debug.Log($"Iniciando loop {currentLoop}");
            // Reinicia el entorno, objetos y puzzles aquí

            // Reinicia el timer
            if (timeManager != null)
            {
                timeManager.StartLoopTimer();
            }
        }
        else
        {
            Debug.Log("Se alcanzó el máximo de loops.");
            // Aquí puedes finalizar el juego o mostrar pantalla de victoria/derrota
        }
    }

    public void ResetLoops()
    {
        currentLoop = 1;
        Debug.Log("Loops reiniciados.");
    }
}
