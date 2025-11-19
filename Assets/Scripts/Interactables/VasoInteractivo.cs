using UnityEngine;

public class VasoInteractivo : MonoBehaviour
{
    public int vasoID;
    public bool estaColocado = false;

    public void Agarrar(PuzzleMesa puzzle)
    {
        if (estaColocado) return;

        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null) rb.isKinematic = true;

        puzzle.AgarrarTaza(this);
        Debug.Log("Taza agarrada: " + vasoID);
    }
}
