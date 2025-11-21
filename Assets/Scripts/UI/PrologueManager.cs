using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Gestor del prólogo que muestra una secuencia de diálogos y luego carga la siguiente escena
/// </summary>
public class PrologueManager : MonoBehaviour
{
    [Header("Configuración de Escena")]
    [Tooltip("Nombre de la escena a la que se cargará después del prólogo")]
    public string nextSceneName = "MainScene";

    [Header("Configuración de Diálogos")]
    [Tooltip("Tiempo de espera antes de comenzar el primer diálogo")]
    public float initialDelay = 1f;

    [Tooltip("Tiempo de espera entre diálogos")]
    public float delayBetweenDialogs = 0.5f;

    [Tooltip("Lista de diálogos del prólogo con sus duraciones")]
    public List<DialogEntry> prologueDialogs = new List<DialogEntry>();

    [Header("Configuración de Fade")]
    [Tooltip("Duración del fade final antes de cambiar de escena")]
    public float finalFadeDuration = 1f;

    private bool isPlaying = false;

    void Start()
    {
        // Verificar que existe DialogManager
        if (DialogManager.Instance == null)
        {
            Debug.LogError("DialogManager no encontrado. Asegúrate de que existe en la escena.");
            LoadNextScene();
            return;
        }

        // Iniciar la secuencia de prólogo
        StartCoroutine(PlayPrologueSequence());
    }

    /// <summary>
    /// Reproduce la secuencia completa del prólogo
    /// </summary>
    private IEnumerator PlayPrologueSequence()
    {
        isPlaying = true;

        // Espera inicial
        yield return new WaitForSeconds(initialDelay);

        // Reproducir cada diálogo en secuencia
        foreach (var dialog in prologueDialogs)
        {
            // Mostrar el diálogo actual
            yield return StartCoroutine(DialogManager.Instance.ShowAndWait(dialog.message, dialog.duration));

            // Espera entre diálogos
            yield return new WaitForSeconds(delayBetweenDialogs);
        }

        isPlaying = false;

        // Cargar la siguiente escena después de completar todos los diálogos
        yield return StartCoroutine(TransitionToNextScene());
    }

    /// <summary>
    /// Maneja la transición con fade a la siguiente escena
    /// </summary>
    private IEnumerator TransitionToNextScene()
    {
        // Si existe LoopTransitionManager, usar su transición
        if (LoopTransitionManager.Instance != null)
        {
            bool transitionComplete = false;
            
            StartCoroutine(LoopTransitionManager.Instance.DoTransition(() =>
            {
                transitionComplete = true;
            }));

            // Esperar a que termine la transición
            yield return new WaitUntil(() => transitionComplete);
        }
        else
        {
            // Fade manual simple
            yield return new WaitForSeconds(finalFadeDuration);
        }

        // Cargar la siguiente escena
        LoadNextScene();
    }

    /// <summary>
    /// Carga la siguiente escena
    /// </summary>
    private void LoadNextScene()
    {
        if (!string.IsNullOrEmpty(nextSceneName))
        {
            Debug.Log($"Cargando escena: {nextSceneName}");
            SceneManager.LoadScene(nextSceneName);
        }
        else
        {
            Debug.LogError("El nombre de la siguiente escena no está configurado.");
        }
    }
}

/// <summary>
/// Estructura que representa una entrada de diálogo con su duración
/// </summary>
[System.Serializable]
public class DialogEntry
{
    [TextArea(2, 4)]
    public string message;
    
    [Tooltip("Duración en segundos que el diálogo permanecerá visible")]
    public float duration = 4f;
}
