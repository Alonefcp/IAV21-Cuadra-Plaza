using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pelota : MonoBehaviour
{
    public float velocidad = 6f;
    public float maxVelocidad = 35f;
    Vector2 direccion;
    Vector2 posInicial; 

    private float radio; 
    // Start is called before the first frame update
    void Start()
    {
        posInicial = transform.position;
        direccion = Vector2.one.normalized;
        radio = transform.localScale.x / 2; 
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(direccion * velocidad * Time.deltaTime);

    }
    public void cambiarDirX()
    {
        direccion.x = -direccion.x;
    }
    public void cambiarDirY()
    {
        direccion.y = -direccion.y;
    }
    public void AumentarVelocidad()
    {
        if(velocidad < maxVelocidad)
          velocidad += 0.25f;
    }
}
