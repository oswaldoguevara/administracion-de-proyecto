using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Servidor : MonoBehaviour {

    public bool juegoiniciado=false;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    private void Update()
    {
        if (!juegoiniciado)
        {
            if (obtenerNumeroConectados()==2)
            {
                Debug.Log("Ya estan los dos jugadores, que inicie el juego");
                juegoiniciado = true;

                FindObjectOfType<CambiarMenu>().cambiarEscena("iniciar");
            }
           
        }
    }


    int obtenerNumeroConectados()
    { int cuenta=0;
        foreach (NetworkConnection con in NetworkServer.connections)
        {
            if (con!=null)
            {
                cuenta++;
            }
        }
        return cuenta;
    }
    public void crearServidor(int puerto)
    {
        NetworkServer.Listen(puerto);
    }

    public void crearPartida()
    {

    }
}
