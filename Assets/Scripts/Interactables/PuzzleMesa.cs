using UnityEngine;
using System.Collections.Generic;

public class PuzzleMesa : MonoBehaviour
{
    [Header("Configuración")]
    public Camera mainCamera;
    public float rayDistance = 3f;
    public LayerMask slotLayer;

    [Header("Slots de la mesa")]
    public VasoSlot[] slots;

    [Header("Orden correcto (IDs de slots)")]
    public int[] ordenCorrecto = { 0, 1, 2 }; // ejemplo: slot 0, luego 1, luego 2

    [Header("Recompensa final")]
    public GameObject polaroidFinal;

    private VasoInteractivo tazaEnMano;
    private List<int> ordenActual = new List<int>();
    private bool puzzleResuelto = false;

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // click izquierdo
        {
            Ray ray = mainCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, rayDistance, slotLayer))
            {
                VasoSlot slot = hit.collider.GetComponent<VasoSlot>();
                if (slot != null && tazaEnMano != null)
                {
                    slot.ColocarTaza(tazaEnMano);
                    ordenActual.Add(slot.slotID);
                    tazaEnMano = null;

                    VerificarPuzzle();
                }
            }
        }
    }

    public void AgarrarTaza(VasoInteractivo taza)
    {
        tazaEnMano = taza;
    }

    void VerificarPuzzle()
    {
        if (ordenActual.Count == ordenCorrecto.Length)
        {
            bool correcto = true;
            for (int i = 0; i < ordenCorrecto.Length; i++)
            {
                if (ordenActual[i] != ordenCorrecto[i])
                {
                    correcto = false;
                    break;
                }
            }

            if (correcto && !puzzleResuelto)
            {
                puzzleResuelto = true;
                if (polaroidFinal != null)
                    polaroidFinal.SetActive(true);
                PuzzleManager.Instance.PuzzleResuelto("Puzzle_4");

                Debug.Log("✅ Puzzle resuelto, polaroid revelada!");
            }
            else
            {
                Debug.Log("❌ Orden incorrecto, no pasa nada.");
            }
        }
    }
}
