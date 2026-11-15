using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoverEscena : MonoBehaviour
{
    [SerializeField] string nombreEscena = "Juego";

    public void Cargar()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(nombreEscena);
    }

    public void Salir()
    {
        
        Application.Quit();

    }
}
   

