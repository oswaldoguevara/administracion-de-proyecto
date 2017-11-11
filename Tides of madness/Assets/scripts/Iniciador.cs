using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[SelectionBase]
public class Iniciador : MonoBehaviour
{   //PREFABS
    public GameObject prefabCarta;
    public GameObject prefabLocura;
    public GameObject MazoJalar;
    public GameObject ContenedorLocuras;
    public GameObject[] cartas;
    //GUARDA LOS HIJOS
    public GameObject[] hijos;
    public GameObject panelInfo;

    //LISTA DE MAZOS
    public Mazos mazoJalar, mazoJug1, mazoJug2, mazoTablero1, mazoTablero2, mazoDejar;

    //DATOS PARTIDA
    public bool turnoJugador = true;
    public int ronda = 1;

    void Awake()
    {
        aparecerCartas();
        aparecerLocuras();
        hijos = ObtenerHijos();
        jalarTOjuadores();
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

            objeto.transform.position = MazoJalar.transform.position;
            objeto.transform.parent = MazoJalar.transform;
            id++;
        }
    }



    public void aparecerLocuras()
    {
        for (int l = 0; l < 20; l++) //aparece las locuras
        {
            GameObject objeto = Instantiate(prefabLocura);


            objeto.transform.position = ContenedorLocuras.transform.position;
            objeto.transform.parent = ContenedorLocuras.transform;


        }
    }

    //OBTIENE LOS HIJOS DENTRO
    GameObject[] ObtenerHijos()
    {
        GameObject[] children = new GameObject[transform.childCount];
        int count = 0;
        foreach (Transform child in transform)
        {
            children[count] = child.gameObject;
            count++;

        }
        return children;
    }

    //PRIMER MOVIMIENTO DE MAZO JALAR AL MAZO DE CADA JUGADOR, REPARTE LAS PRIMERAS 5 CARTAS A CADA UNO
    public void jalarTOjuadores()
    {   //BARAJEA EL MAZOJALAR
        mazoJalar.GetComponent<Mazos>().barajar();
        

        //OBTIENE LAS CARTAS DEL MAZO JALAR REVUELTO
        cartas = mazoJalar.GetComponent<Mazos>().ObtenerHijos();
       

        for (int i = 0; i < 5; i++)
        {

            cartas[i].transform.SetParent(mazoJug1.transform);
            cartas[i].transform.position = mazoJug1.transform.position;
            cartas[i].GetComponent<Carta>().CambiarSpriteFrente();
          
            
        }
        //MANDA A LLAMAR EL METODO DE MAZOS QUE BARAJEA

        //LE ASIGNA OTROS HIJOS A CARTAS, LAS CARTAS QUE QUEDARON EN EL MAZO JALAR
        cartas = mazoJalar.GetComponent<Mazos>().ObtenerHijos();

        for (int i = 0; i < 5; i++)
        {
            
            cartas[i].transform.SetParent(mazoJug2.transform);
            cartas[i].transform.position = mazoJug2.transform.position;
            cartas[i].GetComponent<Carta>().CambiarSpriteAtras();
         //    cartas[i].transform.localScale = new Vector2(1.2f, 1.2f);  cambiar scale de la carta
        }
  }
  






}

