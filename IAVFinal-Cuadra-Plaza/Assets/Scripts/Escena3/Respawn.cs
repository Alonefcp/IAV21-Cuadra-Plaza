using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    
    private float random = 2; //Rango para el generar numeros aleatorios
    private Vector3 posicion; //posicion inicial de la pelota
    
    void Start()
    {
        //Guardo la posicion inicial de la pelota
        posicion = transform.position; 
    }

    /// <summary>
    /// Hace aparecer la pelota en el centro del escenario
    /// </summary>
    public void Respaw()
    {
        Vector3 auxPosicion = posicion; 
        auxPosicion.y += Random.Range(random * -1, random);   
        transform.position = auxPosicion;
    }
}
