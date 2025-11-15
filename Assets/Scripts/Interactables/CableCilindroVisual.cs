using UnityEngine;

public class CableCilindroVisual : MonoBehaviour
{
    public Transform extremoFijo;
    public Transform extremoLibre;

    void Update()
    {
        if (extremoFijo == null || extremoLibre == null) return;

        Vector3 direccion = extremoLibre.position - extremoFijo.position;
        float distancia = direccion.magnitude;

        // Posicionar el cilindro en el medio
        transform.position = extremoFijo.position + direccion / 2f;

        // Rotar el cilindro para que apunte al extremo libre
        transform.rotation = Quaternion.LookRotation(direccion);
        transform.Rotate(90f, 0f, 0f); // Ajuste porque el cilindro apunta en Y

        // Escalar el cilindro en Y para que cubra la distancia
        transform.localScale = new Vector3(0.02f, distancia / 2f, 0.02f);
    }
}
