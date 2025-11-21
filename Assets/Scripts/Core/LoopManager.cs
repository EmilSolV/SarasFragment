using UnityEngine;

public class LoopManager : MonoBehaviour
{
    public int currentLoop = 1;
    public int maxLoops = 5;
    public TimeManager timeManager; // Referencia al TimeManager
    public PlayerReturnManager playerReturnManager; // Referencia al PlayerReturnManager

    [Header("SFX Settings")]
    [Tooltip("Duración en segundos durante la cual los SFX estarán silenciados al iniciar un loop")]
    public float sfxDelayOnLoopStart = 8f;

    public void StartNewLoop(bool isFirstPuzzle = false)
    {
        if (currentLoop < maxLoops)
        {
            currentLoop++;
            Debug.Log($"Iniciando loop {currentLoop}");
            MetricManager.Instance.RegistrarEvento("LoopIniciado", currentLoop);

            // Silencia SFX temporalmente para evitar que todos suenen al reiniciar el loop            

            if (!isFirstPuzzle)
            {
                StartCoroutine(LoopTransitionManager.Instance.DoTransition(() =>
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
                    if (AudioManager.Instance != null)
                    {
                        AudioManager.Instance.DisableSFXTemporarily(sfxDelayOnLoopStart);
                    }
                }));
            }

            // Reinicia el timer
            if (timeManager != null)
            {
                timeManager.StartLoopTimer();
            }
        }
        else
        {
            MetricManager.Instance.RegistrarEvento("MaxLoopsAlcanzado", currentLoop);
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
