using UnityEngine;

public class BookSlot : MonoBehaviour
{
    public bool IsEmpty()
    {
        return currentBook == null;
    }

    public void RemoveBook()
    {
        currentBook = null;
    }

    public int slotID;
    public Book currentBook;
    public BookshelfPuzzle bookshelfPuzzle; // Asignás esto desde el Inspector o lo buscás en Start

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
    public GameObject indicadorVisual;

    void Start()
    {
        if (indicadorVisual != null)
            indicadorVisual.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && IsEmpty())
            indicadorVisual.SetActive(true);
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            indicadorVisual.SetActive(false);
    }

}

