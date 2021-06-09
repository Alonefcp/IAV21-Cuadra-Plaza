using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pelota : MonoBehaviour
{
    [SerializeField] float velocidad = 6f;        //Velocidad de la pelota
    [SerializeField,Range(35,40)] float maxVelocidad = 35f;    //Velocidad maxima de la pelota
    Vector2 direccion;                  //Direccio de la pelota


    void Start()
    {
        direccion = Vector2.one.normalized; 
    }


    void Update()
    {
        //Movemos la pelota
        transform.Translate(direccion * velocidad * Time.deltaTime);

    }

    /// <summary>
    /// Cambia la direccion en X de la pelota
    /// </summary>
    public void CambiarDirX()
    {
        direccion.x = -direccion.x;
    }
    /// <summary>
    /// Cambia la direccion en Y de la pelota
    /// </summary>
    public void CambiarDirY()
    {
        direccion.y = -direccion.y;
    }
    /// <summary>
    /// Aumenta un poco la velocidad de la pelota
    /// </summary>
    public void AumentarVelocidad()
    {
        if(velocidad < maxVelocidad)
          velocidad += 0.2f;
    }
}
