using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boton : MonoBehaviour
{

    [SerializeField] Material botonSinPulsar;
    [SerializeField] Material botonPulsado;
    [SerializeField] SpawnObjetivo objetivo;
    [SerializeField] Transform spwanObjetivo;

    MeshRenderer meshRenderer;

    bool usarBoton = true;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>(); 
    }

    public void ResetarBoton()
    {
        meshRenderer.material = botonSinPulsar;
        transform.localScale = new Vector3(0.3f, 0.9f, 0.3f);
        transform.parent.localPosition = new Vector3(Random.Range(2.0f, 5.5f), transform.parent.localPosition.y, Random.Range(-3.4f, 3.4f));
        usarBoton = true;
    }

    public void UsarBoton()
    {
        if(usarBoton)
        {
            meshRenderer.material = botonPulsado;
            transform.localScale = new Vector3(0.3f, 0.2f, 0.3f);
            objetivo.SpawnearObjetivo();
            usarBoton = false;
        }
    }

    public bool PuedeUsarBoton()
    {
        return usarBoton;
    }
}


