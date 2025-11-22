using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;

/// <summary>
/// Sistema de selección con botones que muestra diferentes diálogos según el botón presionado
/// </summary>
public class ButtonDialogSystem : MonoBehaviour
{
    [System.Serializable]
    public class ButtonDialogPair
    {
        [Tooltip("El botón que activará este diálogo")]
        public Button button;
        
        [TextArea(3, 6)]
        [Tooltip("El diálogo que se mostrará cuando se presione este botón")]
        public string dialogText;
        
        [Tooltip("Duración en segundos que el diálogo permanecerá visible")]
        public float dialogDuration = 5f;
        
        [Tooltip("¿Es esta la respuesta correcta?")]
        public bool isCorrectAnswer = false;
    }

    [System.Serializable]
    public class CreditLine
    {
        [TextArea(2, 3)]
        [Tooltip("Línea de crédito a mostrar")]
        public string text;
        
        [Tooltip("Duración en segundos")]
        public float duration = 3f;
    }

    [Header("Configuración de Botones y Diálogos")]
    [Tooltip("Lista de botones con sus respectivos diálogos")]
    public List<ButtonDialogPair> buttonDialogPairs = new List<ButtonDialogPair>();

    [Header("Mensajes de Resultado")]
    [TextArea(2, 4)]
    [Tooltip("Mensaje que se muestra si el jugador gana")]
    public string winMessage = "¡Lo sabía! Era él... ahora todo tiene sentido.";
    
    [TextArea(2, 4)]
    [Tooltip("Mensaje que se muestra si el jugador pierde")]
    public string loseMessage = "No... no tiene sentido. Algo no cuadra...";
    
    [Tooltip("Duración del mensaje de resultado")]
    public float resultMessageDuration = 5f;

    [Header("Créditos del Equipo")]
    [Tooltip("Lista de líneas de créditos que se mostrarán una por una")]
    public List<CreditLine> credits = new List<CreditLine>()
    {
        new CreditLine() { text = "SARA'S FRAGMENT", duration = 3f },
        new CreditLine() { text = "Un juego desarrollado por:", duration = 3f },
        new CreditLine() { text = "Emil Sol\nProgramación", duration = 4f },
        new CreditLine() { text = "[Nombre]\nArte 3D", duration = 4f },
        new CreditLine() { text = "[Nombre]\nDiseño de Niveles", duration = 4f },
        new CreditLine() { text = "[Nombre]\nDiseño Sonoro", duration = 4f },
        new CreditLine() { text = "[Nombre]\nNarrativa", duration = 4f },
        new CreditLine() { text = "Gracias por jugar", duration = 4f }
    };

    [Header("Opciones")]
    [Tooltip("Deshabilitar botones después de hacer una selección")]
    public bool disableButtonsAfterSelection = true;
    
    [Tooltip("Ocultar botones después de mostrar el resultado")]
    public bool hideButtonsAfterResult = true;

    private bool selectionMade = false;

    void Start()
    {
        // Verificar que existe DialogManager
        if (DialogManager.Instance == null)
        {
            Debug.LogError("ButtonDialogSystem: DialogManager no encontrado.");
            return;
        }

        // Configurar los listeners de cada botón
        SetupButtons();
    }

    /// <summary>
    /// Configura los listeners de todos los botones
    /// </summary>
    private void SetupButtons()
    {
        for (int i = 0; i < buttonDialogPairs.Count; i++)
        {
            ButtonDialogPair pair = buttonDialogPairs[i];
            
            if (pair.button == null)
            {
                Debug.LogWarning($"ButtonDialogSystem: Botón {i} no asignado.");
                continue;
            }

            // Capturar el índice en una variable local para el closure
            int index = i;
            
            // Agregar el listener al botón
            pair.button.onClick.AddListener(() => OnButtonClicked(index));
        }
    }

    /// <summary>
    /// Maneja el evento cuando se hace click en un botón
    /// </summary>
    private void OnButtonClicked(int index)
    {
        if (selectionMade) return;

        if (index < 0 || index >= buttonDialogPairs.Count)
        {
            Debug.LogError($"ButtonDialogSystem: Índice {index} fuera de rango.");
            return;
        }

        selectionMade = true;
        ButtonDialogPair pair = buttonDialogPairs[index];

        // Mostrar el diálogo correspondiente
        if (!string.IsNullOrEmpty(pair.dialogText))
        {
            DialogManager.Instance.ShowMessage(pair.dialogText, pair.dialogDuration);
            Debug.Log($"ButtonDialogSystem: Mostrando diálogo del botón '{pair.button.name}'");
        }

        // Deshabilitar botones si está configurado
        if (disableButtonsAfterSelection)
        {
            DisableAllButtons();
        }

        // Iniciar corrutina para mostrar resultado y créditos
        StartCoroutine(ShowResultAndCredits(pair.isCorrectAnswer, pair.dialogDuration));
    }

    /// <summary>
    /// Muestra el resultado (ganar/perder) y luego los créditos
    /// </summary>
    private IEnumerator ShowResultAndCredits(bool won, float dialogDelay)
    {
        // Esperar a que termine el diálogo del botón
        yield return new WaitForSeconds(dialogDelay + 0.5f);

        // Mostrar mensaje de resultado
        string resultMessage = won ? winMessage : loseMessage;
        if (!string.IsNullOrEmpty(resultMessage))
        {
            yield return StartCoroutine(DialogManager.Instance.ShowAndWait(resultMessage, resultMessageDuration));
        }

        Debug.Log(won ? "🎉 ¡Jugador GANÓ!" : "😢 Jugador perdió");

        // Ocultar botones si está configurado
        if (hideButtonsAfterResult)
        {
            HideAllButtons();
        }

        // Pequeña pausa antes de los créditos
        yield return new WaitForSeconds(1f);

        // Mostrar créditos uno por uno
        yield return StartCoroutine(ShowCreditsSequence());

        // Aquí podrías cargar otra escena, volver al menú, etc.
        Debug.Log("Fin de los créditos");
    }

    /// <summary>
    /// Muestra los créditos de a poco usando el DialogManager
    /// </summary>
    private IEnumerator ShowCreditsSequence()
    {
        Debug.Log("📜 Iniciando secuencia de créditos");

        foreach (CreditLine credit in credits)
        {
            if (!string.IsNullOrEmpty(credit.text))
            {
                // Mostrar cada línea de crédito y esperar
                yield return StartCoroutine(DialogManager.Instance.ShowAndWait(credit.text, credit.duration));
            }
        }

        Debug.Log("📜 Créditos finalizados");
    }

    /// <summary>
    /// Deshabilita todos los botones
    /// </summary>
    private void DisableAllButtons()
    {
        foreach (ButtonDialogPair pair in buttonDialogPairs)
        {
            if (pair.button != null)
            {
                pair.button.interactable = false;
            }
        }
    }

    /// <summary>
    /// Oculta todos los botones
    /// </summary>
    private void HideAllButtons()
    {
        foreach (ButtonDialogPair pair in buttonDialogPairs)
        {
            if (pair.button != null)
            {
                pair.button.gameObject.SetActive(false);
            }
        }
    }

    /// <summary>
    /// Habilita todos los botones
    /// </summary>
    public void EnableAllButtons()
    {
        foreach (ButtonDialogPair pair in buttonDialogPairs)
        {
            if (pair.button != null)
            {
                pair.button.interactable = true;
            }
        }
    }

    /// <summary>
    /// Muestra el diálogo de un botón específico por código
    /// </summary>
    public void ShowDialogByIndex(int index)
    {
        if (index >= 0 && index < buttonDialogPairs.Count)
        {
            OnButtonClicked(index);
        }
    }

    /// <summary>
    /// Cambia el texto de un diálogo en tiempo de ejecución
    /// </summary>
    public void SetDialogText(int index, string newText)
    {
        if (index >= 0 && index < buttonDialogPairs.Count)
        {
            buttonDialogPairs[index].dialogText = newText;
        }
    }

    /// <summary>
    /// Marca un botón como respuesta correcta
    /// </summary>
    public void SetCorrectAnswer(int index, bool isCorrect)
    {
        if (index >= 0 && index < buttonDialogPairs.Count)
        {
            buttonDialogPairs[index].isCorrectAnswer = isCorrect;
        }
    }

    /// <summary>
    /// Agrega una línea de crédito en tiempo de ejecución
    /// </summary>
    public void AddCreditLine(string text, float duration = 3f)
    {
        credits.Add(new CreditLine() { text = text, duration = duration });
    }

    /// <summary>
    /// Limpia todas las líneas de crédito
    /// </summary>
    public void ClearCredits()
    {
        credits.Clear();
    }
}
