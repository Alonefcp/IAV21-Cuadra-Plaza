using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField] private Respawn[] objetosParaSpawnear;

    public Pala _paddle;



    public static GameManager gm; 

    void Start()
    {
        if (gm != this) gm = this; 
      
    }

    public void RespawnearTodos()
    {
        for (int i = 0; i < objetosParaSpawnear.Length; i++)
        {
            if (objetosParaSpawnear[i] != null)
            {
                objetosParaSpawnear[i].Respaw();
            }

        }

    }
}
