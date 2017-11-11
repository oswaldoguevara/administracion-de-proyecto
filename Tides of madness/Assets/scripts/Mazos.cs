using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mazos : MonoBehaviour
{
    //Arreglo que contiene los hijos de este objeto (osea las cartas)
    public GameObject[] hijos;
    public Carta[] hijosCarta;
    public TipoMazo tipoMazo = TipoMazo.mazoJalar;
    float velocidad = 1.8f;
    public float separacionx;
    GameObject cartaSeleccionada;



    void Awake()
    {   
        hijos = ObtenerHijos();

    }


    void Update()
    {
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
        if (tipoMazo == TipoMazo.mazoJug1)
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
        GameObject[] children = new GameObject[transform.childCount];
        int count = 0;
        foreach (Transform child in transform)
        {
            children[count] = child.gameObject;
            count++;
            
          
        }
         return children;
    }

  
    private void OnTransformChildrenChanged()
    {

        //Obtener los GameObject hijos
        hijos = ObtenerHijos();

        //Obtener los componentes Carta de los GameObject hijos

        hijosCarta = new Carta[hijos.Length];
        for (int x = 0; x < hijosCarta.Length; x++)
        {
            hijosCarta[x] = hijos[x].GetComponent<Carta>();
            hijosCarta[x].mazos = GetComponent<Mazos>();
            if (hijosCarta[x].seleccionada == true)
            {
                

            }
           
          
        }
                

            
    }

    public void barajar()
    {
        foreach (Transform hijo in transform)
        {
            hijo.SetSiblingIndex(Random.Range(0, transform.childCount - 1));
        }



    }
    public enum TipoMazo { mazoJalar, mazoDejar, mazoJug1, mazoOponente, tableroJugador, tableroOponente, mazovacio, locuras }
}



