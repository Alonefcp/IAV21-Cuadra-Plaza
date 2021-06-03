using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

//Toda script que vaya a implementar comportamientos con ml agents necesita heredar de Agent
public class AgenteImitacion : Agent
{
    [SerializeField]Boton boton;
    [SerializeField]SpawnObjetivo spawnObjetivo;
    Rigidbody rb;
    bool usaBoton = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public override void OnEpisodeBegin()
    {
        transform.localPosition = new Vector3(Random.Range(-5.0f, 1.0f), 0.0f, Random.Range(-3.0f, 3.0f));
        boton.ResetarBoton();
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(boton.PuedeUsarBoton() ? 1 : 0);

        Vector3 direccionBoton = (boton.gameObject.transform.parent.localPosition - transform.localPosition).normalized;
        sensor.AddObservation(direccionBoton.x);
        sensor.AddObservation(direccionBoton.z);

        sensor.AddObservation(spawnObjetivo.HayObjetivo() ? 1 : 0);

        if(spawnObjetivo.HayObjetivo())
        {
            GameObject obj = spawnObjetivo.GetObjetivo();
            if(obj!=null)
            {
                Vector3 direccionObjetivo = (obj.transform.position - transform.position).normalized;
                sensor.AddObservation(direccionObjetivo.x);
                sensor.AddObservation(direccionObjetivo.z);
            }
            
        }
        else
        {
            sensor.AddObservation(0f);
            sensor.AddObservation(0f);
        }
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        int movX = actions.DiscreteActions[0]; //0 no moverse , 1 izq, 2 der
        int movZ = actions.DiscreteActions[1]; //0 no moverse , 1 atras, 2 delante

        Vector3 fuerza = Vector3.zero;

        switch(movX)
        {
            case 0: fuerza.x = 0f; break;
            case 1: fuerza.x = -1f; break;
            case 2: fuerza.x = 1f; break;
        }

        switch(movZ)
        {
            case 0: fuerza.z = 0f; break;
            case 1: fuerza.z = -1f; break;
            case 2: fuerza.z = 1f; break;
        }

        float velocidad = 6.0f;

        rb.velocity = fuerza * velocidad + new Vector3(0.0f, rb.velocity.y, 0.0f);
        usaBoton = actions.DiscreteActions[2] == 1;
        Debug.Log(usaBoton);
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

        AddReward(-1.0f / MaxStep);
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<int> acciones = actionsOut.DiscreteActions;

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


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Objetivo"))
        {
            AddReward(1.0f);
            Destroy(spawnObjetivo.GetObjetivo());
            spawnObjetivo.setHaSpawneado(false);
            EndEpisode();   //Esta funcion sirve para terminar el comportamiento que se este realizando
        }

        
    }
}
