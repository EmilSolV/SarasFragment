using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;

public class CursorNotebook : MonoBehaviour
{
    public GraphicRaycaster raycaster;   // CanvasNotebook
    public EventSystem eventSystem;      // EventSystem de la escena

    void Update()
    {
        PointerEventData pointerData = new PointerEventData(eventSystem);
        pointerData.position = Input.mousePosition;

        List<RaycastResult> results = new List<RaycastResult>();
        raycaster.Raycast(pointerData, results);

        foreach (RaycastResult result in results)
        {
            Debug.Log("Cursor falso tocando: " + result.gameObject.name);

            if (result.gameObject.name == "BotonConfirmar" && Input.GetMouseButtonDown(0))
            {
                CodigoNotebook codigo = result.gameObject.GetComponentInParent<CodigoNotebook>();
                if (codigo != null)
                {
                    codigo.Validar();
                }
            }
        }
    }
}