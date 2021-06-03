using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObjetivo : MonoBehaviour
{
    [SerializeField] GameObject objetivo;

    bool haSpawneado = false;
    GameObject objSpawneado;

    public void SpawnearObjetivo()
    {
        objSpawneado = Instantiate<GameObject>(objetivo,transform.position , Quaternion.identity);
        
        haSpawneado = true;
    }

    public bool HayObjetivo()
    {
        return haSpawneado;
    }

    public GameObject GetObjetivo()
    {
        return objSpawneado;
    }

    public void setHaSpawneado(bool b)
    {
        haSpawneado = b;
    }
}
