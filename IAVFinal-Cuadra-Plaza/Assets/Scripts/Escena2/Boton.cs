using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boton : MonoBehaviour
{

    [SerializeField] Material botonSinPulsar;   //Material del boton sin pulsar
    [SerializeField] Material botonPulsado;     //Material del boton pulsado
    [SerializeField] SpawnObjetivo objetivo;    //componente que spawnea al objetivo
    [SerializeField] Transform spwanObjetivo;   //Posicion donde spawnea el objetivo

    MeshRenderer meshRenderer;                  //Meshrenderer del boton

    bool usarBoton = true;                      //Para saber si se puede usar el boton

    private void Awake()
    {
        //Cogemos el meshRenderer del boton
        meshRenderer = GetComponent<MeshRenderer>(); 
    }

    /// <summary>
    /// Al resetear el boton cambiamos su material,ponemos su escala original en Y y lo posicionamos en uns zona 
    /// aleatoria del escenario
    /// </summary>
    public void ResetarBoton()
    {
        meshRenderer.material = botonSinPulsar;
        transform.localScale = new Vector3(0.3f, 0.9f, 0.3f);
        transform.parent.localPosition = new Vector3(Random.Range(2.0f, 5.5f), transform.parent.localPosition.y, Random.Range(-3.4f, 3.4f));
        usarBoton = true;
    }

    /// <summary>
    /// Al usar el boton cambiamos su material,reducimos su escala en Y y sapwneamos el objetivo
    /// </summary>
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

    /// <summary>
    /// Devuelve si se puede usar el boton
    /// </summary>
    public bool PuedeUsarBoton()
    {
        return usarBoton;
    }
}


