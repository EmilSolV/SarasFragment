using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CodigoNotebook : MonoBehaviour
{
    [Header("UI")]
    public TMP_InputField inputCodigo;
    public TMP_Text textoFeedback;

    [Header("Logica")]
    public string codigoCorrecto = "181222";
    public GameObject polaroid;       // objeto a revelar
    public Animator polaroidAnimator; // opcional, para animación al revelar
    public DoorController puertaParaDesbloquear; // Asigna en el inspector

    private Button botonConfirmar;
    private bool resuelto = false;

    void Awake()
    {
        // Buscar el botón automáticamente en hijos
        botonConfirmar = transform.Find("BotonConfirmar")?.GetComponent<Button>();
        if (botonConfirmar != null)
            botonConfirmar.onClick.AddListener(Validar);
        else
            Debug.LogWarning("No se encontró el botón Confirmar en hijos de " + gameObject.name);

        if (inputCodigo != null)
            inputCodigo.onEndEdit.AddListener(OnEndEdit);

        SetFeedback("");
    }

    void OnEnable()
    {
        if (inputCodigo != null)
        {
            inputCodigo.text = "";
            inputCodigo.Select();
            inputCodigo.ActivateInputField();
        }

        SetFeedback("");
    }

    void OnEndEdit(string value)
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
            Validar();
    }

    public void Validar()
    {
        Debug.Log("VALIDAR EJECUTADO");

        if (resuelto) return;

        string valor = inputCodigo.text.Trim();
        Debug.Log("Código ingresado: " + valor);

        if (valor == codigoCorrecto)
        {
            resuelto = true;
            SetFeedback("Código correcto");
            RevelarPolaroid();
            puertaParaDesbloquear?.UnlockDoor();
            PuzzleManager.Instance.PuzzleResuelto("Puzzle_3");
        }
        else
        {
            SetFeedback("Código incorrecto");

            // Feedback visual
            var anim = GetComponent<Animator>();
            if (anim) anim.SetTrigger("Shake");

            // 🔧 Resetear campo para reintentar
            inputCodigo.text = "";
            inputCodigo.ActivateInputField();
            inputCodigo.Select();
        }
    }

    void RevelarPolaroid()
    {
        if (polaroid != null)
            polaroid.SetActive(true);

        if (polaroidAnimator != null)
            polaroidAnimator.SetTrigger("Revelar");
    }

    void SetFeedback(string msg)
    {
        if (textoFeedback != null)
        {
            textoFeedback.text = msg;
            textoFeedback.color = (msg == "Código correcto") ? Color.green : Color.red;
        }
    }
}