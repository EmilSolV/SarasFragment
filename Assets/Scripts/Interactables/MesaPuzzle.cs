using UnityEngine;

public class MesaPuzzle : MonoBehaviour
{
    public CupSlot[] slots; // Asignás los 4 slots desde el Inspector
    public int[] correctOrder = { 0, 2, 1 }; // Ejemplo: orden correcto de las tazas
    public GameObject polaroidMesa;
    private bool puzzleSolved = false;
    public DoorController puertaParaDesbloquear; // opcional

    public bool IsPuzzleSolved()
    {
        return puzzleSolved;
    }

    public void CheckPuzzle()
    {
        if (puzzleSolved) return;

        // Solo chequea la cantidad de slots que define el orden correcto
        for (int i = 0; i < correctOrder.Length; i++)
        {
            if (i >= slots.Length)
            {
                Debug.LogWarning($"[MesaPuzzle] Slot {i} está fuera del array de slots.");
                return;
            }

            if (slots[i] == null)
            {
                Debug.LogWarning($"[MesaPuzzle] Slot {i} no está asignado.");
                return;
            }

            if (slots[i].currentCup == null)
            {
                Debug.Log($"[MesaPuzzle] Slot {i} está vacío.");
                return;
            }

            int actualID = slots[i].currentCup.cupID;
            int esperadoID = correctOrder[i];

            Debug.Log($"[MesaPuzzle] Slot {i}: actual={actualID}, esperado={esperadoID}");

            if (actualID != esperadoID)
                return;
        }

        puzzleSolved = true;
        SpawnPolaroid();

        if (puertaParaDesbloquear != null)
            puertaParaDesbloquear.UnlockDoor();

        foreach (var slot in slots)
        {
            if (slot.currentCup != null)
            {
                Collider col = slot.currentCup.GetComponent<Collider>();
                if (col != null) col.enabled = false;

                var grabbable = slot.currentCup.GetComponent<Grabbable>();
                if (grabbable != null) grabbable.enabled = false;
            }
        }

        PuzzleManager.Instance.PuzzleResuelto("Puzzle_Mesa");
    }

    public void ResetPuzzle()
    {
        foreach (var slot in slots)
        {
            if (slot != null)
                slot.ResetSlot();
        }
    }

    void SpawnPolaroid()
    {
        if (polaroidMesa != null)
        {
            polaroidMesa.SetActive(true);
            Debug.Log("📸 Polaroid de la mesa activada");
        }
    }
}
