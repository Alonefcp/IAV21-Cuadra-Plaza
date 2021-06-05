using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Unity.MLAgents;
using Unity.MLAgents.Sensors;

//Toda script que vaya a implementar comportamientos con ml agents necesita heredar de Agent
public class MLPala : Agent
{

    [SerializeField] float velocidad = 1; //Velocidad de la pala
    [SerializeField] GameObject pelota;   //Pelota del entorno
    private Rigidbody2D rb;                      //Rigidbody de la pala
    private Rigidbody2D rbPelota;                //Rigidbody de la pelota

    [SerializeField] GameObject muroArriba, muroAbajo;   //Muro de arriba y de abajo


    /// <summary>
    /// Funcion de inicializacion del agente, es similar a la funcion start de Unity
    /// </summary>
    public override void Initialize()
    {
        rb = GetComponent<Rigidbody2D>();

        if (pelota == null) pelota = GameObject.FindGameObjectWithTag("Pelota");
        rbPelota = pelota.GetComponent<Rigidbody2D>();
    }


    /// <summary>
    /// Esta funcion recoge las aciones que va a hacer el agente.Es importante saber que el algortimo de machine learning solo entiende
    /// numeros,por eso tiene como parametro un vector de floats,es decir, que no entiende que es moverse hacia la derecha,
    /// no sabe que es un Transform, etc.
    /// </summary>
    /// <param name="vectorAction">Vector donde se almacenan el numero de acciones que va a hacer el agente</param>
    public override void OnActionReceived(float[] vectorAction)
    {
        float arriba = vectorAction[0]; // -1 -> abajo, 0 -> parado, 1 -> arriba
        arriba -= 1;

        //Si la pala toca el muro de abajo la penalizamos para que no insista
        if ((transform.position.y < muroAbajo.transform.position.y +
            transform.localScale.y / 2 + 0.7f && arriba < 0))
        {
            AddReward(-0.05f);
            return;
        }

        //Si la pala toca el muro de arriba la penalizamos para que no insista
        if ((arriba > 0) && (transform.position.y > muroArriba.transform.position.y -
            transform.localScale.y / 2 - 0.7f))
        {
            AddReward(-0.05f);
            return;
        }

        //Si la pala se decide moverse le añadimos una pequeña penalizacion
        if (arriba != 0)  
        {        
            rb.MovePosition(transform.position +
            transform.up * arriba * velocidad * Time.deltaTime);
            AddReward(-0.0005f);
        }
    }

    /// <summary>
    /// En este metodo almacenas que atributos necesita la ia para desempeñar el comportamiento que queramos.Esto puede variar
    /// en funcion de lo que queramos hacer. En este caso solo necesitamos la posicion del jugador en Y, la velocidad en X e Y
    /// de la pelota y la posicion en X e Y de la pelota.
    /// </summary>
    /// <param name="sensor">Estructura en la que se alamacena lo que la ia necesita</param>
    public override void CollectObservations(VectorSensor sensor)
    {       
        sensor.AddObservation(pelota.transform.localPosition.x);
        sensor.AddObservation(pelota.transform.localPosition.y);
        sensor.AddObservation(rbPelota.velocity.x);
        sensor.AddObservation(rbPelota.velocity.y);
        sensor.AddObservation(transform.localPosition.y);
    }

    /// <summary>
    /// En este metodo se modifican las acciones que se envian al metodo de arriba,como se ha dicho antes solo necesitamos
    /// el movimiento en X y en Z. Esto sobretodo es para que el usuario pueda mover al jugador con las teclas para testear
    /// el comportamiento.
    /// </summary>
    /// <param name="actionsOut">Vector donde se guardan las acciones</param>
    public override void Heuristic(float[] actionsOut)
    {
        
        float arriba = 1f; //parado

        if (Input.GetKey(KeyCode.W))
        {
            arriba = 2f; //arriba
        }
        if (Input.GetKey(KeyCode.S))
        {
            arriba = 0f; //abajo
        }
        
        actionsOut[0] = arriba;
    }

    /// <summary>
    /// Si la pelota colisona con la pala le cambiamos su direccion,le aumentamos la velocidad y le damos una recompensa.
    /// Si la pala choca con el muro le añadimos una penalizacion.
    /// </summary>
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

    /// <summary>
    /// Añade una pequeña penalizacion a la pala
    /// </summary>
    public void AñadirPenalizacion()
    {
        AddReward(-1.0f);
    }

}
