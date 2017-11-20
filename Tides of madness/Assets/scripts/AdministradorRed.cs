using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.Networking;

//esta
public class AdministradorRed : MonoBehaviour {
    public GameObject prefabCliete;
    public GameObject prefabServidor;
    int puerto= 8500;
    Servidor servidor;
    Cliente cliente;
    string ip = "";
  
    public void configurarComoHost()
    {
        TerminarConexion();

        servidor = Instantiate(prefabServidor).GetComponent<Servidor>();
        cliente = Instantiate(prefabCliete).GetComponent<Cliente>();

        servidor.crearServidor(8500);
        cliente.ArrancarConexion("localhost",puerto);
    }

    public void setIP(string txt)
    {
        ip = txt;
    }
    public void configurarComoCliente()  //recibira como parametro lo del textfield de ip del servidor
    {
        TerminarConexion();
      
           
            cliente = Instantiate(prefabCliete).GetComponent<Cliente>();
            cliente.ArrancarConexion(ip, puerto);  //LA IP SE OBTIENE DEL TEXTFIELD DE EL PANEL "192.168.0.15"
        }

    public void TerminarConexion()
    {
        Cliente[] clientes = FindObjectsOfType<Cliente>();
        Servidor[] servidores = FindObjectsOfType<Servidor>();
        if (clientes != null)
        {
            foreach (Cliente c in clientes) { Destroy(c.gameObject); }
        }
        if (servidores != null)
        {
            foreach (Servidor s in servidores) { Destroy(s.gameObject); }
        }

        NetworkServer.Shutdown();
        NetworkClient.ShutdownAll();
    }
}
