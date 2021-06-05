using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManagerEscenas : MonoBehaviour
{
    /// <summary>
    /// Carga una escena
    /// </summary>
    /// <param name="nombre">Nombre de la escena a cargar</param>
    public void CargarEscena(string nombre)
    {
        SceneManager.LoadScene(nombre);
    }

    /// <summary>
    /// Cierra la aplicacion
    /// </summary>
    public void Salir()
    {
        Application.Quit();
    }
}
