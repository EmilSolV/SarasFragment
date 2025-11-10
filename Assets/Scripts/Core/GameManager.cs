using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    void Start()
    {
        string sessionName = GenerateSessionName();
        MetricManager.Instance.IniciarSesion(sessionName);

        DialogManager.Instance.ShowMessage("Bienvenido a Sara's Fragments", 4f);
        DialogManager.Instance.ShowMessage("Intenta resolver el puzzle de la habitación antes que se acabe el tiempo.", 5f);
        DialogManager.Instance.ShowMessage("Si se acaba deberás comenzarlo desde el principio.", 5f);

        MetricManager.Instance.RegistrarEvento("TiempoDeJuego", TimeSpan.FromSeconds(123.456));
        
    }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void EndGame(bool victory)
    {
        if (victory)
        {
            DialogManager.Instance.ShowMessage("¡Has resuelto el misterio!", 5f);
        }
        else
        {
            DialogManager.Instance.ShowMessage("Fin del juego. No se resolvió el misterio.", 5f);
        }
    }

    private string GenerateSessionName()
    {
        return DateTime.Now.ToString("yyyyMMdd_HHmmss");
    }
}
