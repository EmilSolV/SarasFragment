
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using System.Text;
using static UnityEngine.Rendering.DebugUI.Table;

// Ejemplo de uso:

/* 
    Metricas.Instance.IniciarSesion("sesion_003");
    Metricas.Instance.RegistrarEvento("Monedas", 20);
    Metricas.Instance.RegistrarEvento("TiempoJugado", 135.7f);
    Metricas.Instance.RegistrarEvento("Saltos", 42);

    // Guardar manualmente
    Metricas.Instance.Guardar();
*/

// Resultado del csv

/* 
    sessionId, Monedas, TiempoJugado, Saltos
    sesion_001,10,123.4,25
    sesion_002,15,99.8,30
    sesion_003,20,135.7,42
*/


public class MetricManager : MonoBehaviour
{
    // Creamos la clase como un singleton para usar en cualquier lugar del juego.
    public static MetricManager Instance { get; private set; }

    // ID actual de la sesión -> para que registremos la data de cada sesión bajo un id
    private string currentSessionId;

    // Diccionario nombre de evento / valor
    private Dictionary<string, object> eventosActuales = new();

    private string rutaCSV;
    private string rutaLogs;

    private void Awake()
    {
        // EL singleton
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        // Application.persistentDataPath es la ruta de persistencia de datos que ofr3ece unity, depende de cada SO
        // Los datos se guardarán en el archivo "....ruta/metricas.csv"
        rutaCSV = Path.Combine(Application.persistentDataPath, "metricas.csv");
        rutaLogs = Path.Combine(Application.persistentDataPath, "logs.txt");
        Debug.Log("Metricas en: " + rutaCSV);
        Debug.Log("Logs en: " + rutaLogs);
    }

    // Inicia una nueva sesión con un ID (Esto se debería llamar al iniciar el juego o la sesión)
    public void IniciarSesion(string sessionId = "")
    {
        if (sessionId == "") // Si no hay un session id, va a usar de id la fecha y hora del momento de invocar el método
        {
            System.DateTime now = System.DateTime.Now;
            currentSessionId = now.ToString();
        }
        else
        {
            currentSessionId = sessionId;
        }


        eventosActuales = new Dictionary<string, object>();
    }

    // Registra o actualiza un evento
       public void RegistrarEvento(string nombreEvento, object valor)
    {
        if (string.IsNullOrEmpty(currentSessionId))
        {
            Debug.LogWarning("⚠️ No hay sesión activa. Usa IniciarSesion(sessionId) antes de registrar eventos.");
            return;
        }

        if (eventosActuales.ContainsKey(nombreEvento))
        {
            // Si ambos son float, suma. Si ambos son TimeSpan, suma. Si no, reemplaza.
            var actual = eventosActuales[nombreEvento];
            if (actual is float f1 && valor is float f2)
                eventosActuales[nombreEvento] = f1 + f2;
            else if (actual is System.TimeSpan t1 && valor is System.TimeSpan t2)
                eventosActuales[nombreEvento] = t1 + t2;
            else
                eventosActuales[nombreEvento] = valor; // Reemplaza si no se puede acumular
        }
        else
        {
            eventosActuales[nombreEvento] = valor;
        }
    }

    // Registra un evento en el archivo de logs (txt)
    public void LogEvento(string nombreEvento, float valor)
    {
        if (string.IsNullOrEmpty(currentSessionId))
        {
            Debug.LogWarning("⚠️ No hay sesión activa. Usa IniciarSesion(sessionId) antes de loguear eventos.");
            return;
        }

        try
        {
            string logLinea = $"{System.DateTime.Now:yyyy-MM-dd HH:mm:ss}\t{currentSessionId}\t{nombreEvento}\t{valor}\n";
            File.AppendAllText(rutaLogs, logLinea);
        }
        catch (System.Exception e)
        {
            Debug.LogError($"❌ Error al escribir en el log: {e.Message}");
        }
    }


    //// Obtiene el valor de un evento en la sesión actual
    //public float ObtenerValor(string nombreEvento)
    //{
    //    if (!eventosActuales.ContainsKey(nombreEvento))
    //        return 0f;

    //    return eventosActuales[nombreEvento];
    //}

    // Guarda los datos actuales en el archivo CSV (una fila por sesión)
    public void Guardar()
    {
        if (string.IsNullOrEmpty(currentSessionId))
        {
            Debug.LogWarning("⚠️ No hay sesión activa. No se puede guardar métricas.");
            return;
        }

        try
        {
            bool crearEncabezado = !File.Exists(rutaCSV);

            var nombresEventos = eventosActuales.Keys.ToList();
            var valoresEventos = eventosActuales.Values
                .Select(v =>
                {
                    if (v is float f)
                        return f.ToString("0.##");
                    if (v is System.TimeSpan ts)
                        return ts.ToString(@"hh\:mm\:ss\.fff"); // Formato legible para tiempos
                    return v?.ToString() ?? "";
                })
                .ToList();

            StringBuilder sb = new();

            // Crear encabezado si el archivo no existe
            if (crearEncabezado)
            {
                sb.Append("sessionId");
                foreach (var evento in nombresEventos)
                    sb.Append($",{evento}");
                sb.AppendLine();
            }

            // Crear la fila de datos
            sb.Append(currentSessionId);
            foreach (var valor in valoresEventos)
                sb.Append($",{valor}");
            sb.AppendLine();

            // Escribir al final del archivo
            File.AppendAllText(rutaCSV, sb.ToString());

            Debug.Log($"Métricas guardadas en CSV: {rutaCSV}");
        }
        catch (System.Exception e)
        {
            Debug.LogError($"❌ Error al guardar métricas CSV: {e.Message}");
        }
    }

    // Guarda automáticamente al cerrar el juego
    private void OnApplicationQuit()
    {
        PuzzleManager.Instance.FinPartida();
        Guardar();
    }

}