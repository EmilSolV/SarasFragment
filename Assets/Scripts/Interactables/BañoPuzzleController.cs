using UnityEngine;
using TMPro;

public class BañoPuzzleController : MonoBehaviour
{
    [Header("Referencias")]
    [Header("Ficha")]
    public FichaVisualInteractiva fichaVisual;
    public GameObject polaroidBaño; // Asignalo en el Inspector
    public GameObject vaporEffect;
    public GameObject vaporOverlay;
    public TextMeshProUGUI codigoText;
    public Camera playerCamera;
    public float distanciaInteraccion = 3f;
    public DoorController puertaParaDesbloquear; // Asigna en el inspector
    public AudioSource duchaAudioSource;

    private bool duchaActivada = false;
    private bool puzzleActivado = false;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, distanciaInteraccion))
            {
                if (hit.collider.CompareTag("Ducha"))
                {
                    ActivarDuchaSegunEstado();
                }
            }
        }
    }

    private void ActivarDuchaSegunEstado()
    {
        if (!puzzleActivado)
        {
            // Alternar la ducha mientras el puzzle no se resolvió
            if (!duchaActivada)
            {
                duchaAudioSource?.Play();
                duchaActivada = true;

                if (fichaVisual != null && fichaVisual.EstaConectada)
                {
                    ActivarPuzzle();
                    DialogManager.Instance.ShowMessage("Cuanto vapor. Casi no veo.", 5f);
                }
                else
                {
                    DialogManager.Instance.ShowMessage("Mmm... el agua fría no parece servir de mucho.", 5f);
                }
            }
            else
            {
                // Apagar la ducha si aún no está el puzzle resuelto
                duchaAudioSource?.Stop();
                duchaActivada = false;
                DialogManager.Instance.ShowMessage("Mejor así, estaba gastando agua...", 5f);
            }
        }
        else
        {
            // Puzzle ya activado → no se apaga nunca más
            if (!duchaActivada)
            {
                duchaAudioSource?.Play();
                duchaActivada = true;
            }

            DialogManager.Instance.ShowMessage("Está trabada...", 5f);
        }
    }

    private void ActivarPuzzle()
    {
        puzzleActivado = true;

        vaporEffect?.SetActive(true);
        vaporOverlay?.SetActive(true);

        if (codigoText != null)
        {
            codigoText.transform.parent.gameObject.SetActive(true);
            codigoText.text = "1812";
            codigoText.gameObject.SetActive(true);
        }

        polaroidBaño?.SetActive(true);

        puertaParaDesbloquear?.UnlockDoor();
        PuzzleManager.Instance.PuzzleResuelto("Puzzle_2");

        Debug.Log("Puzzle de baño resuelto");
    }
}