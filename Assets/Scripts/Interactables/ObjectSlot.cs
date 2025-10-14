using UnityEngine;

public class ObjectSlot : MonoBehaviour
{
    public System.Type acceptedType;
    public Grabbable currentObject;

    public bool IsEmpty() => currentObject == null;

    public bool CanAccept(MonoBehaviour obj)
    {
        return obj != null && obj.GetType() == acceptedType;
    }

    public void PlaceObject(Grabbable obj)
    {
        if (!CanAccept(obj)) return;
        currentObject = obj;
        obj.transform.SetParent(transform);
        obj.transform.localPosition = Vector3.zero;
        obj.transform.localRotation = Quaternion.identity;
        obj.OnDrop();
    }

    public void RemoveObject()
    {
        if (currentObject != null)
        {
            currentObject.OnGrab(null);
            currentObject = null;
        }
    }
}