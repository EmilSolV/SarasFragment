using UnityEngine;

public class BookshelfPuzzle : MonoBehaviour
{
    public BookSlot[] slots; // Asignás los 3 slots desde el Inspector
    public int[] correctOrder = { 0, 1, 2 }; // Guerra, Ciencia, Arte
    public GameObject polaroidEstante; 
    private bool puzzleSolved = false;
    public DoorController puertaParaDesbloquear; // Asigna en el inspector

    public bool IsPuzzleSolved()
    {
        return puzzleSolved;
    }

    public void CheckPuzzle()
    {
        if (puzzleSolved) return;

        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i] == null || slots[i].currentBook == null || slots[i].currentBook.bookID != correctOrder[i])
                return;
        }

        puzzleSolved = true;
        SpawnPolaroid();

        if (puertaParaDesbloquear != null)
            puertaParaDesbloquear.UnlockDoor();

        foreach (var slot in slots)
        {
            if (slot.currentBook != null)
            {
                slot.currentBook.SetPuzzleSolved(true);
                Collider col = slot.currentBook.GetComponent<Collider>();
                if (col != null)
                    col.enabled = false;
                var grabbable = slot.currentBook.GetComponent<Grabbable>();
                if (grabbable != null)
                    grabbable.enabled = false;
            }
        }   

        PuzzleManager.Instance.PuzzleResuelto("Puzzle_1");
    }

    public void ResetPuzzle()
    {
        foreach (var slot in slots)
        {
            if (slot != null)
                slot.ResetSlot();
        }
        // Si quieres, también puedes resetear los libros aquí
    }

    void SpawnPolaroid()
    {
        if (polaroidEstante != null)
        {
            polaroidEstante.SetActive(true);
            Debug.Log("Polaroid del estante activada");
        }
    }
}