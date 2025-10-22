using UnityEngine;

public class ObjectGrabber : MonoBehaviour
{
    public Transform handPoint;
    public float grabRange = 2f;
    public LayerMask grabLayer;
    public LayerMask slotLayer;

    private IGrabbable heldObject;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (heldObject == null)
                TryGrab();
            else
                TryPlaceOrDrop();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            TryInteract();
        }
        HighlightObjectInView();
    }

    void TryGrab()
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        if (Physics.Raycast(ray, out RaycastHit hit, grabRange, grabLayer))
        {
            // ¿Es un libro?
            Book book = hit.collider.GetComponent<Book>();
            if (book != null)
            {
                // ¿Está en un slot?
                BookSlot slot = book.GetComponentInParent<BookSlot>();
                if (slot != null && slot.currentBook == book)
                {
                    slot.RemoveBook(); // Libera el slot
                }
                heldObject = book;
                heldObject.OnGrab(handPoint);
                return;
            }

            // ¿Es un objeto agarrable normal?
            IGrabbable grabable = hit.collider.GetComponent<IGrabbable>();
            if (grabable != null)
            {
                heldObject = grabable;
                heldObject.OnGrab(handPoint);
            }
        }
    }

    void TryPlaceOrDrop()
    {
        // Raycast desde el centro de la pantalla para detectar el slot que miras
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        if (Physics.Raycast(ray, out RaycastHit hit, grabRange))
        {
            var slot = hit.collider.GetComponent<BookSlot>();
            Book bookObj = (heldObject as MonoBehaviour) as Book; // Solo coloca si es un libro

            if (slot != null && slot.IsEmpty() && bookObj != null)
            {
                slot.PlaceBook(bookObj);
                heldObject = null;
                return;
            }
        }

        // Si no hay slot, soltar el objeto
        Drop();
    }

    void Drop()
    {
        if (heldObject != null)
        {
            heldObject.OnDrop();
            heldObject = null;
        }
    }

    void TryInteract()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, grabRange))
        {
            IInteractable interactable = hit.collider.GetComponent<IInteractable>();
            if (interactable != null)
            {
                interactable.Interact();
            }
        }
    }

    void HighlightObjectInView()
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        Debug.DrawRay(ray.origin, ray.direction * grabRange, Color.cyan, 0.1f);

        HighlightEffect lastHighlight = null;

        if (Physics.Raycast(ray, out RaycastHit hit, grabRange))
        {
            HighlightEffect highlight = hit.collider.GetComponent<HighlightEffect>();
            if (highlight != null)
            {
                highlight.HighlightOn();
                lastHighlight = highlight;
            }
        }

        // Apagar el highlight en todos los demás objetos
        foreach (var highlight in FindObjectsOfType<HighlightEffect>())
        {
            if (highlight != lastHighlight)
                highlight.HighlightOff();
        }
    }
}