using UnityEngine;

public class DetectorDebug : MonoBehaviour
{
    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, GetComponent<Collider>().bounds.size);
    }

    void OnEnable()
    {
        Debug.Log("NumeroUV activado: " + gameObject.name);
    }
}