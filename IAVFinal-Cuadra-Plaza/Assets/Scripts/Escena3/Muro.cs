using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Muro : MonoBehaviour
{
    private float tiempoSuma = 0.5f; //Incremento de tiempo
    private float tiempoAux; //Variable auxiliar para guardar el tiempo

    [SerializeField] public bool cambiarX=true, cambiarY = true; //Variables para saber que eje de la pelota cambiar

    /// <summary>
    /// Si la pelota choca con algun muro y el tiempo actual de unity es mayor que el que he almacenado, 
    /// le cambiamos la direccion en el eje X o Y.
    /// </summary>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Pelota" && Time.time > tiempoAux )
        {
            if (cambiarX)
                collision.GetComponent<Pelota>().CambiarDirX();

            if (cambiarY)
                collision.GetComponent<Pelota>().CambiarDirY();

            tiempoAux = Time.time + tiempoSuma; 

        }

    }
}
