using UnityEngine;

public class BookSlot : MonoBehaviour
{
    public int slotID;
    public Book currentBook;
    public BookshelfPuzzle bookshelfPuzzle; // Asignás esto desde el Inspector o lo buscás en Start
    public GameObject indicadorVisual;

    public bool IsEmpty()
    {
        return currentBook == null;
    }

    public void RemoveBook()
    {
        if (currentBook != null)
        {
            currentBook.transform.SetParent(null);

            Rigidbody rb = currentBook.GetComponent<Rigidbody>();
            if (rb != null) rb.isKinematic = false;

            currentBook = null;
        }
    }

    public void PlaceBook(Book book)
    {
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

    void Start()
    {
        if (indicadorVisual != null)
            indicadorVisual.SetActive(false);
    }
}
