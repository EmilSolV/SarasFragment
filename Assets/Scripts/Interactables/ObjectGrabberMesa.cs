using UnityEngine;

public class ObjectGrabberMesa : MonoBehaviour
{
    public Transform handPoint;
    public float grabRange = 2f;
    public LayerMask grabLayer;
    public LayerMask slotLayer;

    private IGrabbable heldObject;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (heldObject == null)
                TryGrab();
            else
                TryPlaceOrDrop();
        }
        HighlightObjectInView();
    }

    void TryGrab()
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        if (Physics.Raycast(ray, out RaycastHit hit, grabRange, grabLayer))
        {
            Cup cup = hit.collider.GetComponent<Cup>();
            if (cup != null)
            {
                CupSlot slot = cup.GetComponentInParent<CupSlot>();
                if (slot != null && slot.currentCup == cup)
                {
                    slot.RemoveCup();
                }
                heldObject = cup;
                heldObject.OnGrab(handPoint);
                return;
            }

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
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        if (Physics.Raycast(ray, out RaycastHit hit, grabRange, slotLayer))
        {
            var slot = hit.collider.GetComponent<CupSlot>();
            Cup cupObj = (heldObject as MonoBehaviour) as Cup;

            if (slot != null && slot.IsEmpty() && cupObj != null)
            {
                slot.PlaceCup(cupObj);
                heldObject = null;
                return;
            }
        }
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

        foreach (var highlight in FindObjectsOfType<HighlightEffect>())
        {
            if (highlight != lastHighlight)
                highlight.HighlightOff();
        }
    }
}
