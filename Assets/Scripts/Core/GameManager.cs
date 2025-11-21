using System;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    void Start()
    {
        string sessionName = GenerateSessionName();
        MetricManager.Instance.IniciarSesion(sessionName);

        StartCoroutine(ShowInitialDialog());
    }

    IEnumerator ShowInitialDialog()
    {
        yield return new WaitForSeconds(5f);

        yield return DialogManager.Instance.ShowAndWait("Ugh... ¿Qué pasó?", 3f);

        yield return DialogManager.Instance.ShowAndWait("¿Estoy en casa?", 3f);

        yield return DialogManager.Instance.ShowAndWait("No recuerdo nada...", 3f);
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
