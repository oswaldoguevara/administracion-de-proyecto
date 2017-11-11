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
        cliente.Connect(ip,puerto);
       

    }

    public void conectarse(NetworkMessage mensajeRed)
    {
        Debug.Log("CONECTADO");
    }
}
