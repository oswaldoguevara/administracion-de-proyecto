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
                mensaje.tipoAccion = Accion.TipoAccion.iniciarJuego;
                NetworkServer.SendToAll(Accion.TIPO_MENSAJE, mensaje);
            }

        }
    }


    public void enviarMovimientoAlOtro(NetworkMessage mensajeRed)
    {
        Accion msj = mensajeRed.ReadMessage<Accion>();
        foreach (NetworkConnection conn in NetworkServer.connections)
        {
            if (conn != null)
            {
                if (conn.connectionId != mensajeRed.conn.connectionId)
                {
                    NetworkServer.SendToClient(conn.connectionId, Accion.TIPO_MENSAJE, msj);
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
        NetworkServer.RegisterHandler(Accion.TIPO_MENSAJE,enviarMovimientoAlOtro);
        NetworkServer.Listen(puerto);
    }

}
