using UnityEngine;

public class LoopManager : MonoBehaviour
{
    public int currentLoop = 1;
    public int maxLoops = 5;
    public TimeManager timeManager; // Referencia al TimeManager
    public PlayerReturnManager playerReturnManager; // Referencia al PlayerReturnManager

    public void StartNewLoop(bool isFirstPuzzle = false)
    {
        if (currentLoop < maxLoops)
        {
            currentLoop++;
            Debug.Log($"Iniciando loop {currentLoop}");

            if (!isFirstPuzzle)
            {
                // Reinicia la posición del jugador
                PlayerReturnData.returnPosition = PlayerReturnData.initialPosition;
                if (playerReturnManager != null)
                {
                    playerReturnManager.MoveToReturnPosition();
                }

                BookshelfPuzzle puzzle = FindObjectOfType<BookshelfPuzzle>();
                if (puzzle != null && !puzzle.IsPuzzleSolved()) // Solo si el puzzle no está resuelto
                {
                    puzzle.ResetPuzzle();
                }

                Grabbable[] interactables = FindObjectsOfType<Grabbable>();
                foreach (var obj in interactables)
                {
                    if (obj.resetOnLoop)
                    {
                        obj.ResetObject();
                    }
                }
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
            FinalManager.Instance.ActivarFinal();
        }
    }

    public void ResetLoops()
    {
        currentLoop = 1;
        Debug.Log("Loops reiniciados.");
    }
}
