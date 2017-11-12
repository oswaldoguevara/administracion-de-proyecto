using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;
public class Cliente : MonoBehaviour {

    public NetworkClient cliente;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void ArrancarConexion(string ip,int puerto)
    {   
        cliente = new NetworkClient();
        cliente.RegisterHandler(MsgType.Connect, conectarse);
        cliente.RegisterHandler(Accion.TipoMensaje,hacerMovimiento);
        cliente.Connect(ip,puerto);
       

    }

    public void conectarse(NetworkMessage mensajeRed)
    {
        Debug.Log("CONECTADO");
    }

    public void hacerMovimiento(NetworkMessage mensajeRed)
    {
        Accion mensaje= mensajeRed.ReadMessage<Accion>();
        if (mensaje.id==-1)//INICIA LA ESCENA JUEGO
        {
            FindObjectOfType<CambiarMenu>().cambiarEscena("juego");
        }
       else
        {
            FindObjectOfType<Administrador>().recibirAccion(mensaje);
        }

    }
   
    public void enviarAccion(Accion hacer) //lo envia al servidor
    {
        cliente.Send(Accion.TipoMensaje,hacer);
    }
}
