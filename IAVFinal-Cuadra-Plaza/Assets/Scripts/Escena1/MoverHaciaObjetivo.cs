using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

//Toda script que vaya a implementar comportamientos con ml agents necesita heredar de Agent
public class MoverHaciaObjetivo : Agent
{
    [SerializeField] Transform objetivoTr;          //Posicion del objetivo
    [SerializeField] MeshRenderer rendererSuelo;    //MeshRenderer del suelo
    [SerializeField] Material verde;                //Materialcon color verde
    [SerializeField] Material rojo;                 //Material con color rojo

    /// <summary>
    /// Esta funcion se ejecuta al principio del la ejecucion del comportamiento.Tiene el mismo funcionamiento que la
    /// funcion start de unity.
    /// </summary>
    public override void OnEpisodeBegin()
    {
        //transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
        transform.localPosition = new Vector3(Random.Range(-5.0f, 1.0f), 0.0f, Random.Range(-3.0f, 3.0f));
        transform.LookAt(objetivoTr);
        objetivoTr.localPosition = new Vector3(Random.Range(2.0f, 5.5f), 0.0f, Random.Range(-3.4f, 3.4f));
    }

    /// <summary>
    /// En este metodo almacenas que atributos necesita la ia para desempeñar el comportamiento que queramos.Esto puede variar
    /// en funcion de lo que queramos hacer,pero en este caso solo necesitamos la posicion del jugador y la del objetivo.
    /// </summary>
    /// <param name="sensor">Estructura en la que se alamacena lo que la ia necesita</param>
    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.localPosition);                             //Añadimos la posicion del jugador
        if (objetivoTr != null) sensor.AddObservation(objetivoTr.localPosition);    //Añadimos la posicion del objetivo
    }

    /// <summary>
    /// Esta funcion recoge las aciones que va a hacer el agente.Es importante saber que el algortimo de machine learning solo entiende
    /// numeros,por eso tiene como parametro un vector de floats,es decir, que no entiende que es moverse hacia la derecha,
    /// no sabe que es un Transform, etc.
    /// </summary>
    /// <param name="vectorAction">Vector donde se almaenan el numero de acciones que va a hacer el agente</param>
    public override void OnActionReceived(float[] vectorAction)
    {
        //Aqui recogemos dos acciones del parametro, ya que solo nos interesa el movimiento en X y en Z del jugador
        float movX = vectorAction[0];
        float movZ = vectorAction[1];

        //Finalmente movemos al jugador
        float velocidad = 3.0f;
        transform.localPosition += new Vector3(movX, 0.0f, movZ) * velocidad * Time.deltaTime;
    }
    
    /// <summary>
    /// En este metodo se modifican las acciones que se envian al metodo de arriba,como se ha dicho antes solo necesitamos
    /// el movimiento en X y en Z. Esto sobretodo es para que el usuario pueda mover al jugadro con las teclas para testear
    /// el comportamiento.
    /// </summary>
    /// <param name="actionsOut">Vector donde se guardan las acciones</param>
    public override void Heuristic(float[] actionsOut)
    {
        actionsOut[0] = Input.GetAxisRaw("Horizontal");
        actionsOut[1] = Input.GetAxisRaw("Vertical");
    }

    private void OnTriggerEnter(Collider other)
    {
        //Cuando el jugador choca con el objetivo ponemos el suelo de color verde, le establecemos una recompensa positiva
        //y acabamos el comportamiento
        if (other.gameObject.CompareTag("Objetivo"))
        {
            SetReward(1.0f);
            rendererSuelo.material = verde;
            EndEpisode();   //Esta funcion sirve para terminar el comportamiento que se este realizando
        }

        //Cuando el jugador choca con un muro ponemos el suelo de color rojo, le establecemos una recompensa negativa (penalizacion)
        //y acabamos el comportamiento
        if (other.gameObject.CompareTag("Muro"))
        {
            SetReward(-1.0f);
            rendererSuelo.material = rojo;
            EndEpisode();   //Esta funcion sirve para terminar el comportamiento que se este realizando
        }

    }
}

