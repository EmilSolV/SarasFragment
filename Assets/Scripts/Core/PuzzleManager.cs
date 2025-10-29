using System;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public static PuzzleManager Instance { get; private set; }
    public LoopManager loopManager;

    private DateTime partidaStartTime;
    private DateTime ultimoPuzzleTime;
    private Dictionary<string, TimeSpan> tiemposPorPuzzle = new();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Llamar al iniciar la partida
    public void IniciarPartida()
    {
        partidaStartTime = DateTime.Now;
        ultimoPuzzleTime = partidaStartTime;
        tiemposPorPuzzle.Clear();
    }

    // Llamar cuando se resuelve un puzzle
    public void PuzzleResuelto(string puzzleName)
    {
        DateTime ahora = DateTime.Now;
        TimeSpan tiempoParcial = ahora - ultimoPuzzleTime;
        tiemposPorPuzzle[puzzleName] = tiempoParcial;
        ultimoPuzzleTime = ahora;

        // Registrar el tiempo parcial en MetricManager
        MetricManager.Instance.RegistrarEvento(puzzleName, tiempoParcial);
        if(puzzleName == "Puzzle_1")
            loopManager.StartNewLoop();
    }

    public bool EstaResuelto(string puzzleName)
    {
        return tiemposPorPuzzle.ContainsKey(puzzleName);
    }

    // Llamar al finalizar la partida
    public void FinPartida()
    {
        TimeSpan tiempoTotal = DateTime.Now - partidaStartTime;
        MetricManager.Instance.RegistrarEvento("TiempoTotalPartida", tiempoTotal);
    }

    // Opcional: obtener el tiempo de un puzzle
    public TimeSpan ObtenerTiempoPuzzle(string puzzleName)
    {
        return tiemposPorPuzzle.TryGetValue(puzzleName, out var tiempo) ? tiempo : TimeSpan.Zero;
    }
}