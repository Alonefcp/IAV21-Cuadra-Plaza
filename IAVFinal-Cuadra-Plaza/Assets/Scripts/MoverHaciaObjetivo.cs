using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;

public class MoverHaciaObjetivo : Agent
{
    [SerializeField] Transform objetivoTr;
    [SerializeField] MeshRenderer rendererSuelo;
    [SerializeField] Material verde;
    [SerializeField] Material rojo;

    public override void OnEpisodeBegin()
    {
        //transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
        transform.localPosition = new Vector3(Random.Range(-5.0f, 1.0f), 0.0f, Random.Range(-3.0f, 3.0f));
        transform.LookAt(objetivoTr);
        objetivoTr.localPosition = new Vector3(Random.Range(2.0f, 5.5f), 0.0f, Random.Range(-3.4f, 3.4f));
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.localPosition);
        if (objetivoTr != null) sensor.AddObservation(objetivoTr.localPosition);
    }

    public override void OnActionReceived(float[] vectorAction)
    {
        float movX = vectorAction[0];
        float movZ = vectorAction[1];

        float velocidad = 3.0f;

        transform.localPosition += new Vector3(movX, 0.0f, movZ) * velocidad * Time.deltaTime;
    }

    public override void Heuristic(float[] actionsOut)
    {
        actionsOut[0] = Input.GetAxisRaw("Horizontal");
        actionsOut[1] = Input.GetAxisRaw("Vertical");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Objetivo"))
        {
            SetReward(1.0f);
            rendererSuelo.material = verde;
            EndEpisode();
        }

        if (other.gameObject.CompareTag("Muro"))
        {
            SetReward(-1.0f);
            rendererSuelo.material = rojo;
            EndEpisode();
        }

    }
}

