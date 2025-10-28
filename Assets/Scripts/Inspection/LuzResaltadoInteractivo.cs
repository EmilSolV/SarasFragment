using UnityEngine;

public class LuzResaltadoInteractivo : MonoBehaviour
{
    public GameObject luzVisual;
    [Tooltip("Nombre del puzzle tal como se registra en PuzzleManager (ej: 'Puzzle_1')")]
    public string puzzleName = "Puzzle_1";

    void Start()
    {
        if (luzVisual != null)
        {
            luzVisual.SetActive(false);
        }
    }

    void Update()
    {
        // Si el puzzle está resuelto, nunca prender la luz
        if (!string.IsNullOrEmpty(puzzleName) &&
            PuzzleManager.Instance != null &&
            PuzzleManager.Instance.EstaResuelto(puzzleName))
        {
            if (luzVisual != null)
                luzVisual.SetActive(false);
            return;
        }

        Camera cam = Camera.main;
        if (cam == null) return;

        Ray ray = cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.gameObject == this.gameObject)
            {
                if (luzVisual != null)
                    luzVisual.SetActive(true);
            }
            else
            {
                if (luzVisual != null)
                    luzVisual.SetActive(false);
            }
        }
        else
        {
            if (luzVisual != null)
                luzVisual.SetActive(false);
        }
    }
}