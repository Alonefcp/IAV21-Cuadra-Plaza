using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pala : MonoBehaviour
{
    [SerializeField]public float velocidad = 6; //Velocidad de la pala
    [SerializeField]public GameObject muroArriba, muroAbajo;



    void Update()
    {
        //Movemos la pala, y tenemos en cuenta que no se salga de los muros de arriba y abajo

        float move = Input.GetAxis("Vertical") * Time.deltaTime * velocidad;  

        if ((transform.position.y < muroAbajo.transform.position.y +
            transform.localScale.y / 2 && move < 0))
        {
            return; 
        }
        if ((move > 0) && (transform.position.y > muroArriba.transform.position.y -
            transform.localScale.y / 2))
        {
            return; 
        }
        transform.Translate(move * Vector2.up);
    }

    /// <summary>
    /// Si la pelota choca con la pala cambiamos su direccion y aumentamos su velocidad
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Pelota")
        {
            collision.GetComponent<Pelota>().CambiarDirX();
            collision.GetComponent<Pelota>().AumentarVelocidad();
        }
        
    }
}
