using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalManager : MonoBehaviour
{
    public static FinalManager Instance;

    [Header("Final Settings")]
    public Transform salaFinalSpawnPoint;
    public List<RecuerdoPolaroid> polaroids; // Lista de polaroids y su puzzle asociado
    public GameObject eleccionSospechososUI;
    public float tiempoObservacion = 5f;
    public PlayerReturnManager player;
    public TimeManager timeManager;
    public Camera playerCamera;
    public string nextSceneName = "DecisionFinal";


    private bool finalActivado = false;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        if (player == null)
            player = FindObjectOfType<PlayerReturnManager>();

        // Todas ocultas al inicio
        foreach (var p in polaroids)
        {
            p.polaroid.SetActive(false);
        }

        if (eleccionSospechososUI != null)
            eleccionSospechososUI.SetActive(false);
    }

    public void ActivarFinal()
    {
        if (finalActivado) return;
        finalActivado = true;

        Debug.Log("✅ Evento final activado");
        StartCoroutine(LoopTransitionManager.Instance.DoTransition(() =>
        {
            StartCoroutine(ProcesoFinal());
        }));
    }

    private void LoadNextScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }

    private IEnumerator ProcesoFinal()
    {
        // Mover al jugador al final
        if (player != null && salaFinalSpawnPoint != null)
            player.ForceTeleport(salaFinalSpawnPoint.position);

        DialogManager.Instance.ShowMessage("Ya no tengo más tiempo. Esto es lo que pude recordar.", 5f);

        // Mostrar solo polaroids correspondientes
        foreach (var p in polaroids)
        {
            if (PuzzleManager.Instance.EstaResuelto(p.puzzleID))
                p.polaroid.SetActive(true);
        }

        Debug.Log("🖼️ Mostrando recuerdos en la pared...");

        timeManager.SetTimeRemaining(tiempoObservacion, true);

        // Espera para observar
        yield return new WaitForSeconds(tiempoObservacion);

        // Mostrar panel de sospechosos
        //if (eleccionSospechososUI != null)
        //    eleccionSospechososUI.SetActive(true);

        LoadNextScene();

        Debug.Log("🕵️‍♀️ Mostrar posibles sospechosos");
    }
}

[System.Serializable]
public class RecuerdoPolaroid
{
    public string puzzleID; // Ej: "Puzzle_1"
    public GameObject polaroid;
}
