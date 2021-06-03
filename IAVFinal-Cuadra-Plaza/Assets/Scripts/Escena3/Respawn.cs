using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    // Start is called before the first frame update
    private float random = 2; 
    private Vector3 posicion; 
    void Start()
    {
        posicion = transform.position; 
    }

    // Update is called once per frame
    public void Respaw()
    {
        Vector3 auxPosicion = posicion; 
        auxPosicion.y += Random.Range(random * -1, random);   
        transform.position = auxPosicion;
    }
}
