using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObjetivo : MonoBehaviour
{
    [SerializeField] GameObject objetivo;   //Prefab del objetivo a spawnear

    bool haSpawneado = false;               //Para saber si el objetivo ha spawneado o no
    GameObject objSpawneado;                //Objetivo spawneado      

    /// <summary>
    /// Instancia un objetivo en el entorno
    /// </summary>
    public void SpawnearObjetivo()
    {
        objSpawneado = Instantiate<GameObject>(objetivo,transform.position , Quaternion.identity);      
        haSpawneado = true;
    }

    /// <summary>
    /// Devuelve si el objetivo ha spawneado
    /// </summary>
    /// <returns></returns>
    public bool HayObjetivo()
    {
        return haSpawneado;
    }

    /// <summary>
    /// Devuelve el objetivo que se ha spawneado
    /// </summary>
    public GameObject GetObjetivo()
    {
        return objSpawneado;
    }

    /// <summary>
    /// Establece si el objetivo ha spawneado
    /// </summary>
    /// <param name="b"></param>
    public void setHaSpawneado(bool b)
    {
        haSpawneado = b;
    }
}
