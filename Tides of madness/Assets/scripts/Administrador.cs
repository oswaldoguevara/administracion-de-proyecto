﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
[SelectionBase]
public class Administrador : MonoBehaviour
{   //PREFABS y MAZOS

    public GameObject prefabCarta;
    public GameObject prefabLocura;
    public GameObject locuras;
    public GameObject[] cartas;
    public GameObject[] hijos;

    bool estanBarajeadas = false;


    //LISTA DE MAZO
    public Mazo mazoJalar, mazoJugador, mazoOponente, tableroJugador, tableroOponente, mazoDejar, mazoVacio;

    //DATOS PARTIDA
    public bool turnoJugadores = true;
    public int ronda = 1;
    bool hostGana;
    bool invitadoGana;
    bool hostPierde;
    bool invitadoPierde;

    public Jugador tipoJugador;
    public enum Jugador { Host, Invitado} //para saber si creó la partida o se unió
    Cliente cliente;
    Servidor servidor;

   
    void Awake()
    {
        aparecerCartas();
        aparecerLocuras();
        hijos = ObtenerHijos();
      
        //Obtener el cliete y servidor
        cliente = FindObjectOfType<Cliente>();
        servidor = FindObjectOfType<Servidor>();
       

        //Determinar que tipo de jugador es este
        DeterminarTipoJugador();
        DeterminarTurno();
        //manejador al cliente al servidor
       
      

       
    }
    private void Start()
    {
        StartCoroutine(InciarJuego());
        StartCoroutine(RepartirAJugadoresMovimientoDividido());// se supone que solo va en el iniciador juego
        
    }
   
    public void accionesFinRonda()
    {
        if ((AprobarCambioMazosJugadores() == true)&&(tableroJugador.contarCartasEnMazo()>1))
        {   
            intercambiarMazos();
        }
        else
        {
            Debug.Log("AUN NO ESTAN AMBAS CARTAS PUESTAS");
        }
    }
    public bool AprobarCambioMazosJugadores()
    {   
        if (tableroJugador.contarCartasEnMazo()==tableroOponente.contarCartasEnMazo())  //si ambos jugadores ya pusieron cartas
        {
            //validar que se mayor a 1 porque puede que sean 0==0 y se cambia mazo
           
            return true;
                  
        }
        else{
     
            return false; //que no estan aun las dos cartas puestas
        }
          
    }

    IEnumerator InciarJuego()
    {
        Accion acc = new Accion();

        if (servidor == null && cliente == null)
        {
            mazoJalar.barajear();
        }

        //Si es ANFITRIÓN
        if (tipoJugador == Jugador.Host)
        {
            //Revolver las cartas
          
            acc.tipoAccion = Accion.TipoAccion.OrdenMazoJalar;
            cliente.enviarAccion(acc);
            yield return new WaitForEndOfFrame();

            //Mandar orden de las cartas al otro cliente
           
            //acc.tipoAccion = Accion.TipoAccion.OrdenMazoJalar;
            
            

        }

        //Si es solo CLIENTE
        if (tipoJugador==Jugador.Invitado)
        {
            EjecutarMovimientoOponente(acc);
            yield return new WaitUntil(() => estanBarajeadas);
        }

        
      
        Debug.Log("Juego INICIADO");
       

        yield return new WaitForEndOfFrame();
    }
    IEnumerator RepartirAJugadoresMovimientoDividido()
    {
       
        //Repartir al jugador
        foreach (Carta x in mazoJalar.ObtenerUltimasCartas(5))
        {
            if (tipoJugador == Jugador.Host)
            {
                DarCartaAJugador(x);
            }
            else
            {
                DarCartaAOponente(x);
            }
              yield return new WaitForSeconds(0.3f);

        }

        //Repartir al oponente
        foreach (Carta x in mazoJalar.ObtenerUltimasCartas(5))
        {
            if (tipoJugador==Jugador.Host)
            {
                DarCartaAOponente(x);
            }
            else
            {
                DarCartaAJugador(x);
            }
             yield return new WaitForSeconds(0.3f);
        }

      

        yield return new WaitForEndOfFrame();
    }

    public void RepartirCartasAJugadores()
    {
      

        //Repartir al jugador
        foreach (Carta x in mazoJalar.ObtenerUltimasCartas(5))
        {
            DarCartaAJugador(x);
        }

        //Repartir al oponente
        foreach (Carta x in mazoJalar.ObtenerUltimasCartas(5))
        {
            DarCartaAOponente(x);
        }

    }

    public void DarCartaAJugador(Carta carta)
    {

        carta.transform.SetParent(mazoJugador.transform);
        carta.GetComponent<Carta>().CambiarSpriteFrente();


    }

    public void DarCartaAOponente(Carta carta)
    {

        carta.transform.SetParent(mazoOponente.transform);
        carta.GetComponent<Carta>().CambiarSpriteAtras();
    }

    public void EmpezarTurno()
    {
        turnoJugadores = true;

    }

    public void DeterminarTipoJugador()
    {
        if (servidor != null)
        {
            Debug.Log("Soy Host");
            tipoJugador = Jugador.Host;
        }
        else
        {
            Debug.Log("Soy Invitado");
            tipoJugador = Jugador.Invitado;
        }
    }

    public void DeterminarTurno()
    {
        if (tableroJugador.contarCartasEnMazo() == tableroOponente.contarCartasEnMazo())  //si ambos ya pusieron o ambos no han puesto 
        {
            turnoJugadores = true;
            Debug.Log("PUEDEN PONER CARTA");
        }
        else
        {
            turnoJugadores = false;
            Debug.Log(" NO PUEDEN PONER CARTA");
        }

        }
    public void recibirAccion(Accion recibido)
    {
        Carta[] cartas = mazoOponente.GetComponentsInChildren<Carta>();

        foreach (Carta carta in cartas)
        {
            if (carta.id == recibido.id)
            {
                carta.transform.SetParent(tableroOponente.transform);
                carta.transform.localScale = new Vector2(1.8f, 1.8f);

                turnoJugadores = true;
            }
         
        }
    }
    public void EjecutarMovimientoOponente(Accion acc)
    {
        Debug.Log("ACION RECIBIDA:   "+acc);

        //Obtener todas las cartas del juego
        Carta[] todasCartas = ObtenerTodasLasCartas();
        Debug.Log("TODAS LAS CARTAS  "+todasCartas);
        switch (acc.tipoAccion)
        {
            case Accion.TipoAccion.OrdenMazoJalar:
                mazoJalar.OrdenarCartasPorId(acc.idcartas);
                estanBarajeadas = true;
               
                foreach (Carta carta in todasCartas)
                {
                    
                            carta.transform.SetParent(mazoOponente.transform);
                 
                }
                break;
        }
    }
    public void intercambiarMazos()
    {

        Carta[] cartasJugador = mazoJugador.GetComponentsInChildren<Carta>();
        Carta[] cartasOponente = mazoOponente.GetComponentsInChildren<Carta>();

        foreach (Carta hijo in cartasJugador)
        {
            hijo.transform.SetParent(mazoOponente.transform);
            hijo.CambiarSpriteAtras();
        }
        foreach (Carta hijo in cartasOponente)
        {
            hijo.transform.SetParent(mazoJugador.transform);
            hijo.CambiarSpriteFrente();
        }




    }
    public void cerrarJuego()
    {
        FindObjectOfType<AdministradorRed>().TerminarConexion();
        Application.Quit();
    }
    public void aparecerCartas()
    {
        int id = 0;
        // Carta[] objetos = FindObjectsOfType<Carta>();
        for (int i = 0; i < 18; i++) //objetos.Length
        {
            //Debug.Log("carta");
            GameObject objeto = Instantiate(prefabCarta);
            objeto.GetComponent<Carta>().id = i;
            objeto.GetComponent<Carta>().CambiarSpriteAtras();

            objeto.transform.position = mazoJalar.transform.position;
            objeto.transform.parent = mazoJalar.transform;
            id++;
        }
    }
    public void aparecerLocuras()
    {
        for (int l = 0; l < 20; l++) //aparece las locuras
        {
            GameObject objeto = Instantiate(prefabLocura);


            objeto.transform.position = locuras.transform.position;
            objeto.transform.parent = locuras.transform;


        }
    }
    GameObject[] ObtenerHijos()
    {
        GameObject[] hijos = new GameObject[transform.childCount];
        int cont = 0;
        foreach (Transform hijo in transform)
        {
            hijos[cont] = hijo.gameObject;
            cont++;

        }
        return hijos;
    }
    public Carta[] ObtenerTodasLasCartas()
    {
        return FindObjectsOfType<Carta>();
    }
    public Carta ObtenerCartaPorId(int id)
    {
        Carta[] todasCartas = ObtenerTodasLasCartas();
        foreach (Carta carta in todasCartas)
        {
            if (carta.id == id)
            {
                return carta;
            }
        }
        return null;
    }

    public bool SeleccionarCarta(GameObject objeto)
    {
  
      
        //Revisar si es el turno del jugador
        if (turnoJugadores==true)
        {
            Carta carta = objeto.GetComponent<Carta>();
            carta.SetSeleccionada(true);

            //CAMBIA LA CARTA DEL MAZO JUG A EL MAZO TABLERO
            objeto.transform.SetParent(tableroJugador.transform); //cambia el padre, de mazo
            carta.GetComponent<Carta>().CambiarSpriteAtras();   //voltea la carta
            carta.transform.localScale = new Vector2(1.8f, 1.8f); //cambiar scale de la carta

            //mandar carta al cliente
            Accion ac = new Accion();
            ac.id = carta.GetComponent<Carta>().id;
            FindObjectOfType<Cliente>().enviarAccion(ac);
       
           


        }
        
        return true;


    }
}

