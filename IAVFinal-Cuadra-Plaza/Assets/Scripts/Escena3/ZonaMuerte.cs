using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZonaMuerte : MonoBehaviour
{
    [SerializeField] GameManager gameManager; //Componente del GameManager
    [SerializeField] MLPala mlPala; //Componenete de la pala controlada por la IA
    [SerializeField] bool penaliza; //Para saber si penalizar o no a la pala
    [SerializeField] bool darPuntosAlJugador; //Para saber a que pala dar puntos

    /// <summary>
    /// Si la pelota entra en la zona de muerte hacemos que vuelva a aparecer  en el centro del entorno.
    /// Y si es la zona de muerte de la pala controlada por la IA la penalizamos.
    /// Tambien damos puntos a un jugador u otro.
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Pelota")
        {
            collision.GetComponent<Respawn>().Respaw();
            if(penaliza)mlPala.AñadirPenalizacion();
            if (darPuntosAlJugador) gameManager.AñadirPuntos(darPuntosAlJugador);
            else gameManager.AñadirPuntos(darPuntosAlJugador);

        }
    }
}
