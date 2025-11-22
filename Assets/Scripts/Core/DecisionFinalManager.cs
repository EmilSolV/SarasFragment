using UnityEngine;
using System.Collections;

public class DecisionFinalManager : MonoBehaviour
{
    [Header("Configuración de Diálogos")]
    [Tooltip("Tiempo de espera inicial antes de mostrar los diálogos")]
    public float initialDelay = 0.5f;

    void Start()
    {
        // Reproducir música principal
        if (AudioManager.Instance != null && AudioManager.Instance.backgroundMusic != null)
        {
            AudioManager.Instance.PlayMusic(AudioManager.Instance.backgroundMusic, 0.5f);
            Debug.Log("🎵 Reproduciendo música principal en DecisionFinal");
        }

        // Iniciar la secuencia de diálogos como corrutina
        StartCoroutine(MostrarDialogosIniciales());
    }

    /// <summary>
    /// Muestra los diálogos iniciales en secuencia
    /// </summary>
    private IEnumerator MostrarDialogosIniciales()
    {
        // Espera inicial opcional
        if (initialDelay > 0)
        {
            yield return new WaitForSeconds(initialDelay);
        }

        // Verificar que existe DialogManager
        if (DialogManager.Instance == null)
        {
            Debug.LogError("DecisionFinalManager: DialogManager.Instance no encontrado.");
            yield break;
        }

        // Mostrar primer diálogo y esperar
        yield return StartCoroutine(DialogManager.Instance.ShowAndWait(
            "Tengo que tomar una decisión, antes de sufrir otro ataque...", 
            5f
        ));

        // Mostrar segundo diálogo y esperar
        yield return StartCoroutine(DialogManager.Instance.ShowAndWait(
            "¿Quién lo hizo? ¿Quién intentó matarme?", 
            10f
        ));

        Debug.Log("Diálogos iniciales completados");
    }
}
