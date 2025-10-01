using UnityEngine;

public class BookshelfPuzzle : MonoBehaviour
{
    public BookSlot[] slots; // Asignás los 3 slots desde el Inspector
    public int[] correctOrder = { 0, 1, 2 }; // Guerra, Ciencia, Arte
    public GameObject polaroidPrefab;
    public Transform polaroidSpawnPoint;
    private bool puzzleSolved = false;




    public void CheckPuzzle()
    {
        if (puzzleSolved) return;

        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].currentBook == null || slots[i].currentBook.bookID != correctOrder[i])
                return;
        }

        puzzleSolved = true;
        SpawnPolaroid();
    }

    void SpawnPolaroid()
    {
        Instantiate(polaroidPrefab, polaroidSpawnPoint.position, Quaternion.identity);
    }
}