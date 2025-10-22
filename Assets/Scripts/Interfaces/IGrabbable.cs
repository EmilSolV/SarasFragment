using UnityEngine;

public interface IGrabbable
{
    void OnGrab(Transform handPoint);
    void OnDrop();
}