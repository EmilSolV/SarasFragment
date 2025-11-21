using UnityEngine;

public class CupSlot : MonoBehaviour
{
    public int slotID;
    public Cup currentCup;
    public MesaPuzzle mesaPuzzle;
    public GameObject indicadorVisual;

    public bool IsEmpty()
    {
        return currentCup == null;
    }

    public void RemoveCup()
    {
        if (mesaPuzzle != null && mesaPuzzle.IsPuzzleSolved())
            return;

        if (currentCup != null)
        {
            currentCup.transform.SetParent(null);

            Rigidbody rb = currentCup.GetComponent<Rigidbody>();
            if (rb != null) rb.isKinematic = false;

            currentCup.gameObject.layer = LayerMask.NameToLayer("Grabbable");

            currentCup = null;
        }
    }

    public void PlaceCup(Cup cup)
    {
        if (mesaPuzzle != null && mesaPuzzle.IsPuzzleSolved())
            return;

        currentCup = cup;

        // Aseguramos que el vaso se coloque limpio
        cup.transform.SetParent(transform);
        cup.transform.localPosition = Vector3.zero;
        cup.transform.localRotation = Quaternion.identity;

        // Desactivamos física para que no se caiga ni se incline
        Rigidbody rb = cup.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
            rb.useGravity = false; // ← agregalo si no lo tenías
        }

        // Opcional: aseguramos que el layer no interfiera
        cup.gameObject.layer = LayerMask.NameToLayer("Grabbable");

        // Validamos el puzzle
        if (mesaPuzzle != null)
        {
            mesaPuzzle.CheckPuzzle();
        }
    }
    public void SetIndicadorVisual(bool activo)
    {
        if (indicadorVisual != null)
            indicadorVisual.SetActive(activo);
    }

    public void ResetSlot()
    {
        currentCup = null;
        SetIndicadorVisual(false);
    }

    void Start()
    {
        if (indicadorVisual != null)
            indicadorVisual.SetActive(false);
    }
}
