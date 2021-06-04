using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

//Toda script que vaya a implementar comportamientos con ml agents necesita heredar de Agent
public class AgenteImitacion : Agent
{
    [SerializeField]Boton boton;                  //Componenete del boton
    [SerializeField]SpawnObjetivo spawnObjetivo;  //componente que spawnea al objetivo
    Rigidbody rb;                                 //Rigidbody del agente
    bool usaBoton = false;                        //Variable para saber si pulsa o no el boton

    private void Awake()
    {
        //Cojemos el rigidbody del agente
        rb = GetComponent<Rigidbody>();
    }

    /// <summary>
    /// Esta funcion se ejecuta al principio del la ejecucion del comportamiento.Tiene un funcionamiento similar a la
    /// funcion start de unity.
    /// </summary>
    public override void OnEpisodeBegin()
    {
        //Establecemos la posicion en una zona aleatoria del escenario
        transform.localPosition = new Vector3(Random.Range(-5.0f, 1.0f), 0.0f, Random.Range(-3.0f, 3.0f));
        //Reseteamos el boton
        boton.ResetarBoton();
    }

    /// <summary>
    /// En este metodo almacenas que atributos necesita la ia para desempeñar el comportamiento que queramos.Esto puede variar
    /// en funcion de lo que queramos hacer. En este caso necesitamos:
    /// -Saber si el agente puede pulsar o no el boton
    /// -La direccion desde el agente hacia el boton (solo la X y la Z)
    /// -Saber si ha spawneado el objetivo y en caso afirmativo conocer la direccior desde el agente hacia el objetivo (solo la X y la Z)
    /// En total son 6 observaciones
    /// </summary>
    /// <param name="sensor">Estructura en la que se alamacena lo que la ia necesita</param>
    public override void CollectObservations(VectorSensor sensor)
    {
        //puede pulsar o no el boton
        sensor.AddObservation(boton.PuedeUsarBoton() ? 1 : 0);

        //direccion desde el agente hacia el boton (solo la X y la Z)
        Vector3 direccionBoton = (boton.gameObject.transform.parent.localPosition - transform.localPosition).normalized;
        sensor.AddObservation(direccionBoton.x);
        sensor.AddObservation(direccionBoton.z);

        //Saber si ha spawneado el objetivo
        sensor.AddObservation(spawnObjetivo.HayObjetivo() ? 1 : 0);

        //Si ha spawneado el objetivo hay que conocer la direccior desde el agente hacia el objetivo (solo la X y la Z)
        if (spawnObjetivo.HayObjetivo())
        {
            GameObject obj = spawnObjetivo.GetObjetivo();
            if(obj!=null)
            {
                Vector3 direccionObjetivo = (obj.transform.position - transform.position).normalized;
                sensor.AddObservation(direccionObjetivo.x);
                sensor.AddObservation(direccionObjetivo.z);
            }
            
        }
        else //si no ha spawneado añadimos ceros como observacion
        {
            sensor.AddObservation(0f);
            sensor.AddObservation(0f);
        }
    }

    // <summary>
    /// Esta funcion recoge las aciones que va a hacer el agente.Es importante saber que el algortimo de machine learning solo entiende
    /// numeros,por eso tiene como parametro un vector de floats,es decir, que no entiende que es moverse hacia la derecha,
    /// no sabe que es un Transform, etc.
    /// </summary>
    /// <param name="actions">Buffer donde se almacenan el numero de acciones que va a hacer el agente</param>
    public override void OnActionReceived(ActionBuffers actions)
    {
        int movX = actions.DiscreteActions[0]; //0 -> no moverse , 1 -> ir a la izquierda, 2 -> ir a la derecha
        int movZ = actions.DiscreteActions[1]; //0 -> no moverse , 1 -> ir hacia atras, 2 -> ir hacia delante

        Vector3 dir = Vector3.zero;

        //En funcion de input que se reciba se establece la direccion del movimiento
        switch(movX)
        {
            case 0: dir.x = 0f; break;
            case 1: dir.x = -1f; break;
            case 2: dir.x = 1f; break;
        }
        switch(movZ)
        {
            case 0: dir.z = 0f; break;
            case 1: dir.z = -1f; break;
            case 2: dir.z = 1f; break;
        }

        //Movemos al agente
        float velocidad = 6.0f;
        rb.velocity = dir * velocidad + new Vector3(0.0f, rb.velocity.y, 0.0f);

        //Si pulsa el boton spawnea el objetivo y se le recompensa
        usaBoton = actions.DiscreteActions[2] == 1;      
        if (usaBoton)
        {
            Collider[] colliders = Physics.OverlapBox(transform.position, Vector3.one);

            foreach (Collider col in colliders)
            {
                if (col.gameObject.CompareTag("Boton") && boton.PuedeUsarBoton())
                {
                    boton.UsarBoton();
                    AddReward(1.0f);
                }
            }
        }

        //Añadimos una penalizacion para que el agente tarde menos en completar su tarea
        AddReward(-1.0f/MaxStep);
    }

    /// <summary>
    /// En este metodo se modifican las acciones que se envian al metodo de arriba,como se ha dicho antes solo necesitamos
    /// el movimiento en X y en Z. Esto sobretodo es para que el usuario pueda mover al jugador con las teclas para testear
    /// el comportamiento.
    /// </summary>
    /// <param name="actionsOut">Buffer donde se guardan las acciones</param>
    public override void Heuristic(in ActionBuffers actionsOut)
    {
        //Guardamos el array de acciones discretas que son la que el agente usa
        ActionSegment<int> acciones = actionsOut.DiscreteActions;

        //Almacenamos el input recogido en el vector de acciones
        switch(Mathf.RoundToInt(Input.GetAxisRaw("Horizontal")))
        {
            case -1: acciones[0] = 1; break;
            case 0: acciones[0] = 0; break;
            case 1: acciones[0] = 2; break;
        }

        switch (Mathf.RoundToInt(Input.GetAxisRaw("Vertical")))
        {
            case -1: acciones[1] = 1; break;
            case 0: acciones[1] = 0; break;
            case 1: acciones[1] = 2; break;
        }

        acciones[2] = Input.GetKey(KeyCode.E) ? 1 : 0;
    }

    /// <summary>
    /// Si el agente colisona con el objetivo le damos una recompensa y acabamos el comportamiento
    /// </summary>
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Objetivo"))
        {
            AddReward(1.0f);
            //Dstruimos el objetivo e indicamos que no ha spawneado
            Destroy(spawnObjetivo.GetObjetivo());
            spawnObjetivo.setHaSpawneado(false);
            EndEpisode();   //Esta funcion sirve para terminar el comportamiento que se este realizando
        }       
    }
}
