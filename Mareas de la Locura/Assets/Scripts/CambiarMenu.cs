using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CambiarMenu : MonoBehaviour {
    public GameObject menuPrincipal;
    public GameObject unirsePartida;
    public GameObject crearPartida;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {


    }

    public void cambiarMenu(string menu)
    {
        switch (menu)
        {
            case "principal":
                menuPrincipal.SetActive(true);
                unirsePartida.SetActive(false);
                crearPartida.SetActive(false);

                break;
            case "unirse":
                menuPrincipal.SetActive(false);
                unirsePartida.SetActive(true);
                crearPartida.SetActive(false);
                break;
            case "crear":
                menuPrincipal.SetActive(false);
                unirsePartida.SetActive(false);
                crearPartida.SetActive(true);

                break;
            case "iniciar":
                cambiarEscena("juego");
                break;
        }
    }

    public void ocultarTodos()
    {
        unirsePartida.SetActive(false);
                menuPrincipal.SetActive(false);
        crearPartida.SetActive(false);

    }

    public void cambiarEscena(string escena)
    {
        
        UnityEngine.SceneManagement.SceneManager.LoadScene(escena);
    }
}