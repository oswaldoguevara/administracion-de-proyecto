using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdministradorRed : MonoBehaviour {
    public GameObject prefabCliete;
    public GameObject prefabServidor;
    int puerto= 8500;
    Servidor servidor;
    Cliente cliente;
	
    public void configurarComoHost()
    {
        servidor = Instantiate(prefabServidor).GetComponent<Servidor>();
        cliente = Instantiate(prefabCliete).GetComponent<Cliente>();

        servidor.crearServidor(8500);
        cliente.ArrancarConexion("localhost",puerto);
    }

    public void configurarComoCliente()  //recibira como parametro lo del textfield de ip del servidor
    {
       cliente = Instantiate(prefabCliete).GetComponent<Cliente>();
       cliente.ArrancarConexion("192.168.0.15", puerto);  //LA IP SE OBTIENE DEL TEXTFIELD DE EL PANEL "192.168.0.15"
    }
}
