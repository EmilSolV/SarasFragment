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
        if (currentCup != null)
        {
            currentCup.transform.SetParent(null);

            Rigidbody rb = currentCup.GetComponent<Rigidbody>();
            if (rb != null) rb.isKinematic = false;

            // Al sacar → capa Grabbable
            currentCup.gameObject.layer = LayerMask.NameToLayer("Grabbable");

            currentCup = null;
        }
    }

    public void PlaceCup(Cup cup)
    {
        currentCup = cup;
        cup.transform.SetParent(transform);
        cup.transform.localPosition = Vector3.zero;
        cup.transform.localRotation = Quaternion.identity;

        Rigidbody rb = cup.GetComponent<Rigidbody>();
        //if (rb != null)
        //{
          //  rb.isKinematic = true;
            //rb.useGravity = false;
        //}

        // En slot → capa Default
        cup.gameObject.layer = LayerMask.NameToLayer("Default");

        if (mesaPuzzle != null)
            mesaPuzzle.CheckPuzzle();
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
