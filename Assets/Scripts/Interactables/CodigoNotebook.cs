using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class CodigoNotebook : MonoBehaviour
{
    public TMP_InputField inputCodigo;
    public Button botonConfirmar;
    public GameObject polaroid;
    public string codigoCorrecto = "181222"; // ← tu código personalizado
    public GameObject canvasCodigo;

    void OnMouseDown()
    {
        canvasCodigo.SetActive(true); // activa el canvas
        StartCoroutine(ForzarFoco()); // enfoca el campo
    }



    void Update()
    {
        if (inputCodigo != null && inputCodigo.isFocused)
        {
            Debug.Log("🟢 El campo InputCodigo está enfocado");
        }
    }






    void Start()

    {
        botonConfirmar.onClick.AddListener(ValidarCodigo);
        inputCodigo.onValueChanged.AddListener(DetectarEscritura);

        if (polaroid != null)
            polaroid.SetActive(false);
    }

    void DetectarEscritura(string texto)
    {
        Debug.Log("✍️ Escribiendo: " + texto);
    }

    void ValidarCodigo()
    {
        string codigoIngresado = inputCodigo.text.Trim();

        if (codigoIngresado == codigoCorrecto)
        {
            Debug.Log("✅ Código correcto: se muestra el polaroid");
            if (polaroid != null)
                polaroid.SetActive(true);
        }
        else
        {
            Debug.Log("❌ Código incorrecto");
            // Podés agregar texto de error o efectos aquí
        }
    }
    void OnEnable()
    {
        StartCoroutine(ForzarFoco());
    }

    IEnumerator ForzarFoco()
    {
        yield return new WaitForSeconds(0.1f); // da tiempo a que todo se active
        inputCodigo.text = ""; // limpia el campo
        inputCodigo.ActivateInputField(); // enfoca
        inputCodigo.Select(); // selecciona
        Debug.Log("🟢 Campo activado manualmente");
    }


}
