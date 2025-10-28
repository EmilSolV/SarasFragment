using UnityEngine;
using System.Collections.Generic;

public class LightManager : MonoBehaviour
{
    [Header("Luces altas de la casa")]
    public List<Light> lucesAltas = new List<Light>();

    [Header("Configuración global")]
    public Color colorLuz = Color.white;
    [Range(0f, 8f)]
    public float intensidad = 1f;
    public bool lucesEncendidas = true;

    void Start()
    {
        AplicarConfiguracion();
    }

    void OnValidate()
    {
        AplicarConfiguracion();
    }

    public void AplicarConfiguracion()
    {
        foreach (var luz in lucesAltas)
        {
            if (luz != null)
            {
                luz.color = colorLuz;
                luz.intensity = intensidad;
                luz.enabled = lucesEncendidas;
            }
        }
    }

    public void EncenderTodas()
    {
        lucesEncendidas = true;
        foreach (var luz in lucesAltas)
            if (luz != null) luz.enabled = true;
    }

    public void ApagarTodas()
    {
        lucesEncendidas = false;
        foreach (var luz in lucesAltas)
            if (luz != null) luz.enabled = false;
    }

    public void CambiarColor(Color nuevoColor)
    {
        colorLuz = nuevoColor;
        foreach (var luz in lucesAltas)
            if (luz != null) luz.color = nuevoColor;
    }

    public void CambiarIntensidad(float nuevaIntensidad)
    {
        intensidad = nuevaIntensidad;
        foreach (var luz in lucesAltas)
            if (luz != null) luz.intensity = nuevaIntensidad;
    }
}