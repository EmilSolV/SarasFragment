using UnityEngine;

public class BookshelfPuzzle : MonoBehaviour
{
    public BookSlot[] slots; // Asignás los 3 slots desde el Inspector
    public int[] correctOrder = { 0, 1, 2 }; // Guerra, Ciencia, Arte
    public GameObject polaroidPrefab;
    public Transform polaroidSpawnPoint;
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
                // Desactiva el script Grabbable para que no pueda ser agarrado
                var grabbable = slot.currentBook.GetComponent<Grabbable>();
                if (grabbable != null)
                    grabbable.enabled = false;
            }
        }
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
        Instantiate(polaroidPrefab, polaroidSpawnPoint.position, Quaternion.identity);
    }
}