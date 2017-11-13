using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Servidor : MonoBehaviour
{

    public bool juegoiniciado = false;
    public NetworkServer server;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    private void Update()
    {
        if (!juegoiniciado)
        {
            if (obtenerNumeroConectados() == 2)
            {
                Debug.Log("Ya estan los dos jugadores, que inicie el juego");
                juegoiniciado = true;

                //   FindObjectOfType<CambiarMenu>().cambiarEscena("juego");
                Accion mensaje = new Accion();
                mensaje.id = -1;
                NetworkServer.SendToAll(Accion.TipoMensaje, mensaje);
            }

        }
    }


    public void enviaeMovimientoAlOtro(NetworkMessage mensajeRed)
    {
        Accion msj = mensajeRed.ReadMessage<Accion>();
        foreach (NetworkConnection conn in NetworkServer.connections)
        {
            if (conn != null)
            {
                if (conn.connectionId != mensajeRed.conn.connectionId)
                {
                    NetworkServer.SendToClient(conn.connectionId, Accion.TipoMensaje, msj);
                }
            }
        }
    }

    int obtenerNumeroConectados()
    {
        int cuenta = 0;
        foreach (NetworkConnection con in NetworkServer.connections)
        {
            if (con != null)
            {
                cuenta++;
            }
        }
        return cuenta;
    }

    public void crearServidor(int puerto)
    {
        NetworkServer.RegisterHandler(Accion.TipoMensaje,enviaeMovimientoAlOtro);
        NetworkServer.Listen(puerto);
    }

}
