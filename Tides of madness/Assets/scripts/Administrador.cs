using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[SelectionBase]
public class Administrador : MonoBehaviour
{   //PREFABS y MAZOS

    public GameObject prefabCarta;
    public GameObject prefabLocura;
 
    public GameObject locuras;
   
    
    public GameObject[] cartas;
    //GUARDA LOS HIJOS
    public GameObject[] hijos;

 
    public GameObject ultimacarta;

    //LISTA DE MAZO
    public Mazos mazoJalar, mazoJugador, mazoOponente, tableroJugador, tableroOponente, mazoDejar,mazoVacio;

    //DATOS PARTIDA
    public bool turnoJugador = true;
    public int ronda = 1;

    void Awake()
    {
        aparecerCartas();
        aparecerLocuras();
        hijos = ObtenerHijos();
        jalarAjuadores();


    }
    public void hacerAccion(Accion hacer)
    {
        // realiza accion que recibe del cliente 
        Debug.Log("accion " + hacer);
    }

    public void intercambiarMazos()
    {

   //     temporalJugadores = mazoJugador.ObtenerHijos();
     //   temporalOponentes = mazoOponente.ObtenerHijos();

        

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

    //PRIMER MOVIMIENTO DE MAZO JALAR AL MAZO DE CADA JUGADOR, REPARTE LAS PRIMERAS 5 CARTAS A CADA UNO
    public void jalarAjuadores()
    {   //BARAJEA EL MAZOJALAR
        mazoJalar.GetComponent<Mazos>().barajar();


        //OBTIENE LAS CARTAS DEL MAZO JALAR REVUELTO
        cartas = mazoJalar.GetComponent<Mazos>().ObtenerHijos();


        for (int i = 0; i < 5; i++)
        {

            cartas[i].transform.SetParent(mazoJugador.transform);
            cartas[i].transform.position = mazoJugador.transform.position;
            cartas[i].GetComponent<Carta>().CambiarSpriteFrente();


        }
        //MANDA A LLAMAR EL METODO DE MAZOS QUE BARAJEA

        //LE ASIGNA OTROS HIJOS A CARTAS, LAS CARTAS QUE QUEDARON EN EL MAZO JALAR
        cartas = mazoJalar.GetComponent<Mazos>().ObtenerHijos();

        for (int i = 0; i < 5; i++)
        {

            cartas[i].transform.SetParent(mazoOponente.transform);
            cartas[i].transform.position = mazoOponente.transform.position;
            cartas[i].GetComponent<Carta>().CambiarSpriteAtras();

        }

    }

    public bool AgregarASeleccion(GameObject objeto, bool checarSeleccion)
    {
        Carta carta = objeto.GetComponent<Carta>();


        carta.SetSeleccionada(true);


        //CAMBIA LA CARTA DEL MAZO JUG A EL MAZO TABLERO
        objeto.transform.SetParent(tableroJugador.transform); //cambia el padre, de mazo
        carta.GetComponent<Carta>().CambiarSpriteAtras();
        carta.transform.localScale = new Vector2(1.8f, 1.8f); //cambiar scale de la carta
        ultimacarta = objeto;
        return true;

    }

    public bool AgregarASeleccion2(GameObject objeto, bool checarSeleccion)
    {
        Carta carta = objeto.GetComponent<Carta>();



        //CAMBIA LA CARTA DEL MAZO JUG A EL MAZO TABLERO
        objeto.transform.SetParent(tableroOponente.transform); //cambia el padre, de mazo
        carta.transform.localScale = new Vector2(1.8f, 1.8f); //cambiar scale de la carta

        return true;

    }


}

