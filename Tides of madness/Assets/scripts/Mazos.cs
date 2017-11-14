﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mazos : MonoBehaviour
{
    //Arreglo que contiene los hijos de este objeto (osea las cartas)
    public GameObject[] hijos;
    public Carta[] hijosCarta;
    public TipoMazo tipoMazo = TipoMazo.mazoJalar;
    float velocidad = 1.8f;
    public float separacionx;
    GameObject cartaSeleccionada;
    bool autoActualizarHijos = true;



    void Awake()
    {
        hijos = ObtenerHijos();


  
}

    void Update()
    {  //MOVIMIENTO SUAVE DE LAS CARTAS
         int indice = 0;
        
      foreach (GameObject hijo in hijos)
        {
            
            Vector2 posicionDeseada = ObtenerPosicion(indice);

            hijo.transform.position = Vector2.Lerp(hijo.transform.position, posicionDeseada, velocidad * Time.deltaTime);
            hijo.GetComponent<SpriteRenderer>().sortingOrder = indice;
            indice++;
        }

    }


    public Vector2 ObtenerPosicion(int indice)
    {
        if (tipoMazo == TipoMazo.mazoJugador)
        {   
            return new Vector2(transform.position.x + (separacionx * indice), transform.position.y );
        }
        if (tipoMazo == TipoMazo.mazoOponente)
        {
            return new Vector2(transform.position.x + (separacionx * indice), transform.position.y);
        }
        if (tipoMazo == TipoMazo.tableroJugador)
        {
        //    separacionx = 5f;
            return new Vector2(transform.position.x + (separacionx * indice), transform.position.y );
        }
        if (tipoMazo == TipoMazo.tableroOponente)
        {
            return new Vector2(transform.position.x + (separacionx * indice), transform.position.y );
        }

        return transform.position;
}

    public GameObject[] ObtenerHijos()
    {
        GameObject[] hijos = new GameObject[transform.childCount];
        int i = 0;
        foreach (Transform hijo in transform)
        {
            hijos[i] = hijo.gameObject;
            i++;
            
          
        }
         return hijos;
    }


    private void OnTransformChildrenChanged()
    {
        if (autoActualizarHijos)
        {
            //Obtener los GameObject hijos
            hijos = ObtenerHijos();


            hijosCarta = new Carta[hijos.Length];
            for (int x = 0; x < hijosCarta.Length; x++)
            {
                hijosCarta[x] = hijos[x].GetComponent<Carta>();
                hijosCarta[x].mazos = GetComponent<Mazos>();
            }

<<<<<<< HEAD
    //carta comodin
    public void valorComodin(string daenerys)
    {
        //nos da el nuevo color que sera la carta comodin
        switch (daenerys)
        {
            case "verde":
                totalverde++;
                totalocura = totalocura + 1;
                totalocurajug2 = totalocurajug2 - 1;
                comodin.SetActive(false);
                break;
            case "azul":
                totalazul++;
                totalocura = totalocura + 1;
                totalocurajug2 = totalocurajug2 - 1;
                comodin.SetActive(false);
                break;
            case "amarillo":
                totalamarillo++;
                totalocura = totalocura + 1;
                totalocurajug2 = totalocurajug2 - 1;
                comodin.SetActive(false);
                break;
            case "rojo":
                totalrojo++;
                totalocura = totalocura + 1;
                totalocurajug2 = totalocurajug2 - 1;
                comodin.SetActive(false);
                break;
            case "rosa":
                totalrosa++;
                totalocura = totalocura + 1;
                totalocurajug2 = totalocurajug2 - 1;
                comodin.SetActive(false);
                break;
=======
>>>>>>> origin/master
        }
    }

    /* public void barajar()
     {
         foreach (Transform hijo in transform)
         {
             hijo.SetSiblingIndex(Random.Range(0, transform.childCount - 1));
         }
   }
   */


    public Carta[] ObtenerUltimasCartas(int cantidad)
    {
        if (hijos.Length >= cantidad)
        {
            //Mandar las cartas
            Carta[] cartas = new Carta[cantidad];
            int ultimoIndex = hijos.Length - 1;
            for (int x = 0; x < cantidad; x++)
            {
                cartas[x] = hijos[ultimoIndex].GetComponent<Carta>();
                ultimoIndex--;
            }
            return cartas;
        }
        else
        {
            //No hay suficientes cartas para satisfacer la peticion
            return null;
        }
    }

    public GameObject ObtenerUltimaCarta()
    {
        if (hijos.Length >= 1)
        {
            GameObject x = hijos[hijos.Length - 1];
            return x;
        }
        else
        {
            Debug.Log("Solo tengo " + hijos.Length + " hijos.");
            return null;
        }
    }

    public Carta[] barajear()
    {
        autoActualizarHijos = false;
        List<Carta> estadoCartas = new List<Carta>();

        foreach (Transform hijo in transform)
        {
            hijo.SetSiblingIndex(Random.Range(0, transform.childCount - 1));
        }
        //Checar en que orden quedaron para regresarlo despues
        foreach (Transform hijo in transform)
        {
            estadoCartas.Add(hijo.GetComponent<Carta>());
        }

        autoActualizarHijos = true;

       return estadoCartas.ToArray();
    }

    public void OrdenarCartasPorId(int[] orden)
    {
        autoActualizarHijos = false;

        //Obtener todas las cartas del juego
        Carta[] todasCartas = FindObjectsOfType<Carta>();

        //Sacar a todos los hijos del transform
        foreach (Carta carta in todasCartas)
        {
            carta.transform.SetParent(null);
        }

        //Agregar las cartas en el orden indicado
        for (int x = 0; x < orden.Length; x++)
        {
            int idActual = orden[x];
            foreach (Carta carta in todasCartas)
            {
                if (carta.id == idActual)
                {
                    carta.transform.SetParent(transform);
                    carta.transform.SetSiblingIndex(x);
                }
            }
        }

        OnTransformChildrenChanged();

        autoActualizarHijos = true;
    }

    public int[] ObtenerOrdenPorId()
    {
        List<int> orden = new List<int>();
        foreach (Transform hijo in transform)
        {
            orden.Add(hijo.GetComponent<Carta>().id);
        }
        return orden.ToArray();
    }



    public enum TipoMazo { mazoJalar, mazoDejar, mazoJugador, mazoOponente, tableroJugador, tableroOponente, mazoVacio, locuras }
}



