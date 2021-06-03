using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pala : MonoBehaviour
{
    public float velocidad = 6;
    public GameObject muroArriba, muroAbajo;



    void Update()
    {
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Pelota")
        {
            collision.GetComponent<Pelota>().cambiarDirX();
            collision.GetComponent<Pelota>().AumentarVelocidad();

        }
        
    }
}
