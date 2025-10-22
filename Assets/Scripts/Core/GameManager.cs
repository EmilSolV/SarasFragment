using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;


    void Start()
    {
        DialogManager.Instance.ShowMessage("Bienvenido a Sara's Fragments", 4f);
        DialogManager.Instance.ShowMessage("Intenta resolver el puzzle de la habitación antes que se acabe el tiempo.", 5f);
        DialogManager.Instance.ShowMessage("Si se acaba deberás comenzarlo desde el principio.", 5f);
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
            Debug.Log("¡Has resuelto el misterio!");
            // Mostrar pantalla de victoria, etc.
        }
        else
        {
            Debug.Log("Fin del juego. No se resolvió el misterio.");
            // Mostrar pantalla de derrota, etc.
        }
    }
}
