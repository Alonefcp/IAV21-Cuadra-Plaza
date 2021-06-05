using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    [SerializeField] Text textoJugador; //Texto con los puntos del jugador
    [SerializeField] Text textoIA;      //Texto con los puntos de la IA

    int puntosJugador = 0;  //Puntos del jugador
    int puntosIA = 0;       //Puntos de la IA

    /// <summary>
    /// Añade puntos a un jugador u a otro
    /// </summary>
    /// <param name="jugador">Para saber a que jugador añadir puntos</param>
    public void AñadirPuntos(bool jugador)
    {
        if(jugador) //añadimos puntos al jugador
        {
            puntosJugador++;
            textoJugador.text = puntosJugador.ToString();
        }
        else //añadimos puntos a la IA
        {
            puntosIA++;
            textoIA.text = puntosIA.ToString();
        }
    }


}
