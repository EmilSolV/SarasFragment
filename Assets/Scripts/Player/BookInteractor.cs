using UnityEngine;
public class BookInteractor : MonoBehaviour
{
    public float interactionRange = 2f;
    public LayerMask slotLayer;
    public Transform handPoint;
    public LayerMask grabLayer;

    private Book heldBook;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            TryGrabBook();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (heldBook == null)
                TryTakeBook();
            else
                TryPlaceBook();
        }
    }

    void TryGrabBook()
    {
        if (heldBook != null) return;

        Collider[] hits = Physics.OverlapSphere(transform.position, interactionRange, grabLayer);
        foreach (var hit in hits)
        {
            Book book = hit.GetComponent<Book>();
            if (book != null)
            {
                heldBook = book;

                heldBook.transform.SetParent(handPoint);
                heldBook.transform.localPosition = Vector3.zero;
                heldBook.transform.localRotation = Quaternion.identity;

                Rigidbody rb = heldBook.GetComponent<Rigidbody>();
                if (rb != null) rb.isKinematic = true;

                break;
            }
        }
    }

    void TryTakeBook()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, interactionRange, slotLayer);
        foreach (var hit in hits)
        {
            BookSlot slot = hit.GetComponent<BookSlot>();
            if (slot != null && !slot.IsEmpty)
            {
                heldBook = slot.currentBook;
                slot.RemoveBook();

                heldBook.transform.SetParent(handPoint);
                heldBook.transform.localPosition = Vector3.zero;
                heldBook.transform.localRotation = Quaternion.identity;

                Rigidbody rb = heldBook.GetComponent<Rigidbody>();
                if (rb != null) rb.isKinematic = true;
            

                break;
            }
        }
    }

    void TryPlaceBook()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, interactionRange, slotLayer);
        foreach (var hit in hits)
        {
            BookSlot slot = hit.GetComponent<BookSlot>();
            if (slot != null && slot.IsEmpty)
            {
                slot.PlaceBook(heldBook);
                heldBook = null;

                FindFirstObjectByType<BookshelfPuzzle>().CheckPuzzle();
                break;
            }
        }
    }


}
