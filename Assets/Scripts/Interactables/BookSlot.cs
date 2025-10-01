using UnityEngine;

public class BookSlot : MonoBehaviour
{
    public Book currentBook;

    public bool IsEmpty => currentBook == null;

    public void PlaceBook(Book book)
    {
        if (IsEmpty)
        {
            currentBook = book;
            book.transform.position = transform.position;
            book.transform.SetParent(transform);
        }
    }

    public void RemoveBook()
    {
        if (!IsEmpty)
        {
            currentBook.transform.SetParent(null);
            currentBook = null;
        }
    }
}
