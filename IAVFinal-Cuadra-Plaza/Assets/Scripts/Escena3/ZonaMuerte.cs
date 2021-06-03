using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZonaMuerte : MonoBehaviour
{
    [SerializeField] public MLPala mlPala; //Componenete de la pala controlada por la IA
    [SerializeField] public bool penaliza; //Para saber si penalizar o no a la pala

    /// <summary>
    /// Si la pelota entra en la zona de muerte hacemos que vuelva a aparecer  en el centro del entorno.
    /// Y si es la zona de muerte de la pala controlada por la IA la penalizamos
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Pelota")
        {
            collision.GetComponent<Respawn>().Respaw();
            if(penaliza)mlPala.AñadirPenalizacion();

        }
    }
}
