using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Unity.MLAgents;
using Unity.MLAgents.Sensors;

public class MLPala : Agent
{

    [SerializeField] public float velocidad = 1;
    [SerializeField] public GameObject pelota; 
    private Rigidbody2D rb;
    private Rigidbody2D rbPelota;

    [SerializeField] public GameObject muroArriba, muroAbajo;

   

 
    public override void Initialize()
    {
        rb = GetComponent<Rigidbody2D>();

        if (pelota == null) pelota = GameObject.FindGameObjectWithTag("Pelota");
        rbPelota = pelota.GetComponent<Rigidbody2D>();
    }


   
    public override void OnActionReceived(float[] vectorAction)
    {
        float lUp = vectorAction[0];

        lUp -= 1;



        if ((transform.position.y < muroAbajo.transform.position.y +
            transform.localScale.y / 2 + 0.7f && lUp < 0))
        {
            AddReward(-0.05f);
            return;
        }
        if ((lUp > 0) && (transform.position.y > muroArriba.transform.position.y -
            transform.localScale.y / 2 - 0.7f))
        {
            AddReward(-0.05f);
            return;
        }

        if (lUp != 0)  
        {
         
            rb.MovePosition(transform.position +
            transform.up * lUp * velocidad * Time.deltaTime);
            AddReward(-0.0005f);

        }

    }


    public override void CollectObservations(VectorSensor sensor)
    {       
        sensor.AddObservation(pelota.transform.localPosition.x);
        sensor.AddObservation(pelota.transform.localPosition.y);
        sensor.AddObservation(rbPelota.velocity.x);
        sensor.AddObservation(rbPelota.velocity.y);
        sensor.AddObservation(transform.localPosition.y);
    }


    public override void Heuristic(float[] actionsOut)
    {
        
        float lUp = 1f; //parado

        if (Input.GetKey(KeyCode.W))
        {
            lUp = 2f; //arriba
        }
        if (Input.GetKey(KeyCode.S))
        {
            lUp = 0f; //abajo
        }
        

        actionsOut[0] = lUp;

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Pelota")
        {
            collision.GetComponent<Pelota>().CambiarDirX();
            collision.GetComponent<Pelota>().AumentarVelocidad();
            AddReward(2.0f);
        }

        if (collision.tag == "Muro")
        {

            AddReward(-0.05f);
        }
    }

    public void AñadirPenalizacion()
    {
        AddReward(-1.0f);
    }

}
