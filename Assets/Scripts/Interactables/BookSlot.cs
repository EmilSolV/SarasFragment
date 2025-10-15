using UnityEngine;

public class BookSlot : MonoBehaviour
{
    public int slotID;
    public Book currentBook;
    public BookshelfPuzzle bookshelfPuzzle;
    public GameObject indicadorVisual;

    public bool IsEmpty()
    {
        return currentBook == null;
    }

    public void RemoveBook()
    {
        // Solo permite quitar el libro si el puzzle NO está resuelto
        if (bookshelfPuzzle != null && bookshelfPuzzle.IsPuzzleSolved())
            return;

        if (currentBook != null)
        {
            currentBook.transform.SetParent(null);

            Rigidbody rb = currentBook.GetComponent<Rigidbody>();
            if (rb != null) rb.isKinematic = false;

            // Si el puzzle NO está resuelto, vuelve a poner la capa "Grabbable"
            currentBook.gameObject.layer = LayerMask.NameToLayer("Grabbable");

            currentBook = null;
        }
    }


    public void PlaceBook(Book book)
    {
        // Solo permite colocar si el puzzle NO está resuelto
        if (bookshelfPuzzle != null && bookshelfPuzzle.IsPuzzleSolved())
            return;

        currentBook = book;
        book.transform.SetParent(transform);
        book.transform.localPosition = Vector3.zero;
        book.transform.localRotation = Quaternion.identity;

        Rigidbody rb = book.GetComponent<Rigidbody>();
        if (rb != null) rb.isKinematic = true;

        if (bookshelfPuzzle != null)
        {
            bookshelfPuzzle.CheckPuzzle();
        }
    }

    public void SetIndicadorVisual(bool activo)
    {
        if (indicadorVisual != null)
            indicadorVisual.SetActive(activo);
    }

    public void ResetSlot()
    {
        currentBook = null;
        // Opcional: puedes apagar el indicador visual si lo usas
        SetIndicadorVisual(false);
    }

    void Start()
    {
        if (indicadorVisual != null)
            indicadorVisual.SetActive(false);
    }
}