using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

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
