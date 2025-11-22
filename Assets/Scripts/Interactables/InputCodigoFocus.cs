
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputCodigoFocus : MonoBehaviour, IPointerClickHandler
{
    private TMP_InputField inputField;

    void Awake()
    {
        inputField = GetComponent<TMP_InputField>();
    }

    // Este método se dispara cuando hacés click en el campo
    public void OnPointerClick(PointerEventData eventData)
    {
        if (inputField != null)
        {
            inputField.ActivateInputField();
            inputField.Select();
        }
    }
}
