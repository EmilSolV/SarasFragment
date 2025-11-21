using UnityEngine;
using System.Collections.Generic;

public class LoopManager : MonoBehaviour
{
    public int currentLoop = 1;
    public int maxLoops = 5;
    public TimeManager timeManager; // Referencia al TimeManager
    public PlayerReturnManager playerReturnManager; // Referencia al PlayerReturnManager

    [Header("SFX Settings")]
    [Tooltip("Duración en segundos durante la cual los SFX estarán silenciados al iniciar un loop")]
    public float sfxDelayOnLoopStart = 8f;

    [Header("Diálogos por Loop")]
    [Tooltip("Duración en segundos que cada diálogo se mostrará en pantalla")]
    public float dialogDuration = 5f;
    
    // Diccionario con los mensajes personalizados para cada loop
    private Dictionary<int, string> loopDialogs = new Dictionary<int, string>()
    {
        { 1, "¿Otra vez desde el comienzo?" },
        { 2, "Los recuerdos se repiten. Debo encontrar mis recuerdos." },
        { 3, "Las sombras del pasado me persiguen. ¿Quién es el culpable?" },
        { 4, "No me queda mucho tiempo, pero puedo sentir que estoy cerca de la verdad." },
        { 5, "Este es el último intento. Debo recordar todo." }
    };

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

            ShowLoopDialog(currentLoop);
        }
        else
        {
            MetricManager.Instance.RegistrarEvento("MaxLoopsAlcanzado", currentLoop);
            Debug.Log("Se alcanzó el máximo de loops.");
            FinalManager.Instance.ActivarFinal();
        }
    }

    private void ShowLoopDialog(int loopNumber)
    {
        if (DialogManager.Instance != null && loopDialogs.ContainsKey(loopNumber))
        {
            StartCoroutine(DialogManager.Instance.ShowAndWait(loopDialogs[loopNumber], dialogDuration));
        }
    }

    public void ResetLoops()
    {
        currentLoop = 1;
        Debug.Log("Loops reiniciados.");
    }
    
    /// <summary>
    /// Permite establecer o modificar el diálogo de un loop específico en tiempo de ejecución
    /// </summary>
    public void SetLoopDialog(int loopNumber, string message)
    {
        if (loopDialogs.ContainsKey(loopNumber))
        {
            loopDialogs[loopNumber] = message;
        }
        else
        {
            loopDialogs.Add(loopNumber, message);
        }
    }
    
    /// <summary>
    /// Obtiene el diálogo configurado para un loop específico
    /// </summary>
    public string GetLoopDialog(int loopNumber)
    {
        return loopDialogs.ContainsKey(loopNumber) ? loopDialogs[loopNumber] : "";
    }
}
