using UnityEngine;
using TMPro;

public class BañoPuzzleController : MonoBehaviour
{
    [Header("Referencias")]
    public GameObject polaroidBaño; // Asignalo en el Inspector
    public GameObject vaporEffect;
    public GameObject vaporOverlay;
    public TextMeshProUGUI codigoText;
    public Camera playerCamera;
    public float distanciaInteraccion = 3f;
    public DoorController puertaParaDesbloquear; // Asigna en el inspector

    private bool duchaActivada = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, distanciaInteraccion))
            {
                if (hit.collider.CompareTag("Ducha") && !duchaActivada)
                {
                    ResolverPuzzle();
                }
            }
        }
    }

    void ResolverPuzzle()
    {
        ActivarDucha();
        if (puertaParaDesbloquear != null)
            puertaParaDesbloquear.UnlockDoor();
        PuzzleManager.Instance.PuzzleResuelto("Puzzle_2");
    }

    void ActivarDucha()
    {
        Debug.Log("Ducha activada");

        if (vaporEffect != null)
        {
            vaporEffect.SetActive(true);
            Debug.Log("Vapor activado");
        }

        if (vaporOverlay != null)
        {
            vaporOverlay.SetActive(true);
            Debug.Log("Overlay activado");
        }

        if (codigoText != null)
        {
            // Activa el Canvas (padre del texto)
            codigoText.transform.parent.gameObject.SetActive(true);
            codigoText.text = "1812"; // Podés cambiar el código si querés
            codigoText.gameObject.SetActive(true);
            Debug.Log("Código revelado");
        }
        if (polaroidBaño != null)
        {
            polaroidBaño.SetActive(true);
            Debug.Log("Polaroid del baño activada");
        }
        duchaActivada = true;
        PuzzleManager.Instance.PuzzleResuelto("Puzzle_2");
    }
}