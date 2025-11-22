using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class CursorNotebook : MonoBehaviour
{
    public GraphicRaycaster raycaster;   // CanvasNotebook
    public EventSystem eventSystem;      // EventSystem de la escena

    void Update()
    {
        if (!Input.GetMouseButtonDown(0)) return;

        PointerEventData pointerData = new PointerEventData(eventSystem);
        pointerData.position = Input.mousePosition;

        List<RaycastResult> results = new List<RaycastResult>();
        raycaster.Raycast(pointerData, results);

        foreach (RaycastResult result in results)
        {
            Debug.Log("Cursor falso tocando: " + result.gameObject.name);

            // ✅ Activar campo de texto si se hace click sobre él
            TMP_InputField inputField = result.gameObject.GetComponent<TMP_InputField>();
            if (inputField != null)
            {
                inputField.ActivateInputField();
                inputField.Select();
                return;
            }

            // ✅ Validar si se hace click en el botón Confirmar
            if (result.gameObject.name == "BotonConfirmar")
            {
                CodigoNotebook codigo = result.gameObject.GetComponentInParent<CodigoNotebook>();
                if (codigo != null)
                {
                    codigo.Validar();
                    return;
                }
            }
        }
    }
}