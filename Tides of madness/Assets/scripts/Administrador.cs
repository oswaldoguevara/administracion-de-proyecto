using System.Collections;
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
    //GUARDA LOS HIJOS
    public GameObject[] hijos;

    bool estanBarajeadas = false;
    public GameObject ultimacarta;

    //LISTA DE MAZO
    public Mazos mazoJalar, mazoJugador, mazoOponente, tableroJugador, tableroOponente, mazoDejar, mazoVacio;

    //DATOS PARTIDA
    public bool turnoJugador = true;
    public int ronda = 1;
    public Jugador tipoJugador;
    public enum Jugador { Host, Invitado}
    Cliente cliente;
    Servidor servidor;

    Contador contador;
    void Awake()
    {
        aparecerCartas();
        aparecerLocuras();
        hijos = ObtenerHijos();
        //jalarAjuadores();
        //Obtener el cliete y servidor
        cliente = FindObjectOfType<Cliente>();
        servidor = FindObjectOfType<Servidor>();
        contador = FindObjectOfType<Contador>();

        //Determinar que tipo de jugador es este
        DeterminarTipoJugador();
        DeterminarTurno();
        //manejador al cliente al servidor
       
      

       
    }
    private void Start()
    {
        StartCoroutine(Juego());
        contador.hacerConteo();
        

    }
    IEnumerator Juego()
    {
        Accion acc = new Accion();
        acc.tipoAccion = Accion.TipoAccion.OrdenMazoJalar;
        acc.idcartas = mazoJalar.ObtenerOrdenPorId();
        yield return new WaitForSeconds(1f);

       
        if (servidor == null && cliente == null)
        {
            mazoJalar.barajear();
        }

        
        if (tipoJugador==Jugador.Host)
        {
            mazoJalar.barajear();

            yield return new WaitForEndOfFrame();

            //Mandar orden de las cartas al otro cliente
           
            cliente.enviarAccion(acc);

            
        }

        //Si es solo CLIENTE
        if (tipoJugador==Jugador.Invitado)
        {
            EjecutarMovimientoOponente(acc);
            yield return new WaitUntil(() => estanBarajeadas);
        }
             

        //Repartir sus 5 cartas a los jugadores
        StartCoroutine(RepartirAJugadoresAnimado());
        Debug.Log("CARTAS ENTREGADAS CORRECTAMENTE!");
       

        yield return new WaitForEndOfFrame();
    }

    IEnumerator RepartirAJugadoresAnimado()
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

    public void TerminarTurno()
    {
        turnoJugador = false;

    }

    public void EmpezarTurno()
    {
        turnoJugador = true;

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
        if (tipoJugador == Jugador.Host)
        {
            if (ronda == 1)
            {
                turnoJugador = true;
            }
            else
            {
                turnoJugador = false;
            }
        }
        else if (tipoJugador == Jugador.Invitado)
        {
            if (ronda == 1)
            {
                turnoJugador = false;
            }
            else
            {
                turnoJugador = true;
            }
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


            }
         
        }
    }
    public void EjecutarMovimientoOponente(Accion acc)
    {


        
        switch (acc.tipoAccion)
        {
            case Accion.TipoAccion.OrdenMazoJalar:
                mazoJalar.OrdenarCartasPorId(acc.idcartas);
                estanBarajeadas = true;
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

    //OBTIENE LOS HIJOS DENTRO
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
    public bool SeleccionarCarta(GameObject objeto, bool checarSeleccion)
    {

        if (!turnoJugador)
        {
            return false;
        }
        Carta carta = objeto.GetComponent<Carta>();


        carta.SetSeleccionada(true);


        //CAMBIA LA CARTA DEL MAZO JUG A EL MAZO TABLERO
        objeto.transform.SetParent(tableroJugador.transform); //cambia el padre, de mazo
        carta.GetComponent<Carta>().CambiarSpriteAtras();
        carta.transform.localScale = new Vector2(1.8f, 1.8f); //cambiar scale de la carta
        ultimacarta = objeto;
        //mandar carta al cliente
        Accion ac = new Accion();
        ac.id = ultimacarta.GetComponent<Carta>().id;
        FindObjectOfType<Cliente>().enviarAccion(ac);

        return true;



    }




}

