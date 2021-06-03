using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Muro : MonoBehaviour
{
    private float tiempoSuma = 0.5f;
    private float tiempo; 

    public bool cambiarX=true, cambiarY = true; 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Pelota" && Time.time > tiempo )
        {
            if (cambiarX)
                collision.GetComponent<Pelota>().cambiarDirX();

            if (cambiarY)
                collision.GetComponent<Pelota>().cambiarDirY();

            tiempo = Time.time + tiempoSuma; 

        }

    }
}
