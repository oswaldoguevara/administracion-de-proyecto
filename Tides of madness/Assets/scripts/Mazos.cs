using System.Collections;
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
    public GameObject comodin;
    public GameObject FinRonda;

    //Datos jugador para el contador
    public int totalazul = 0;
    public int totalamarillo = 0;
    public int totalverde = 0;
    public int totalrojo = 0;
    public int totalrosa = 0;
    public int totalocura = 0;
    public int puntos = 0;
    //datos jugador2 para el contador
    public int totalazuljug2 = 0;
    public int totalamarillojug2 = 0;
    public int totalverdejug2 = 0;
    public int totalrojojug2 = 0;
    public int totalrosajug2 = 0;
    public int totalocurajug2 = 0;
    public int puntosjug2 = 0;



    void Awake()
    {
        hijos = ObtenerHijos();
        foreach (GameObject hijo in hijos)
        {
            sumapuntosjug2(hijo);
            sumapuntos(hijo);
            }
        foreach (GameObject hijo in hijos)
        {
            contador(hijo);
            contadorjug2(hijo);
            
        }


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
        }         
    }
    
 public void barajar()
    {
        foreach (Transform hijo in transform)
        {
            hijo.SetSiblingIndex(Random.Range(0, transform.childCount - 1));
        }
  }

  



  
    public enum TipoMazo { mazoJalar, mazoDejar, mazoJugador, mazoOponente, tableroJugador, tableroOponente, mazoVacio, locuras }



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
        }
    }


    //Suma los puntos de las cartas en el tablero
    public void sumapuntos(GameObject hijo)
    {
        if (tipoMazo == TipoMazo.mazoJugador)
        {
            string id = hijo.name.ToString();

            switch (id)
            {
                case "0":
                    totalamarillo++;
                    break;

                case "1":
                    totalamarillo++;
                    break;

                case "2":
                    totalamarillo++;
                    totalocura++;
                    break;

                case "3":
                    totalverde++;
                    totalocura++;
                    break;

                case "4":
                    totalverde++;
                    totalocura++;
                    break;

                case "5":
                    totalverde++;
                    break;

                case "6":
                    totalazul++;
                    break;

                case "7":
                    totalazul++;
                    totalocura++;
                    break;

                case "8":
                    totalazul++;
                    break;

                case "9":
                    totalrojo++;
                    break;

                case "10":
                    totalrojo++;
                    break;

                case "11":
                    totalrojo++;
                    totalocura++;
                    break;

                case "12":
                    FindObjectOfType<ManejadorInterfaz>().aparecerPanelComodin();
                    break;

                case "13":
                    break;

                case "14":
                    totalocura++;
                    break;

                case "15":
                    totalrosa++;
                    break;

                case "16":
                    totalrosa++;
                    totalocura++;
                    break;

                case "17":
                    totalrosa++;
                    totalocura++;
                    break;

            }
            Debug.Log("totalverde" + totalverde);
            Debug.Log("totalazul" + totalazul);
            Debug.Log("totalamarilo" + totalamarillo);
            Debug.Log("totalrojo" + totalrojo);
            Debug.Log("totalrosa" + totalrosa);



        }
        

    }
    public void sumapuntosjug2(GameObject hijo)
    {
        if (tipoMazo == TipoMazo.mazoOponente)
        {
            string id = hijo.name.ToString();

            switch (id)
            {
                case "0":
                    totalamarillojug2++;
                    break;

                case "1":
                    totalamarillojug2++;
                    break;

                case "2":
                    totalamarillojug2++;
                    totalocurajug2++;
                    break;

                case "3":
                    totalverdejug2++;
                    totalocurajug2++;
                    break;

                case "4":
                    totalverdejug2++;
                    totalocurajug2++;
                    break;

                case "5":
                    totalverdejug2++;
                    break;

                case "6":
                    totalazuljug2++;
                    break;

                case "7":
                    totalazuljug2++;
                    totalocurajug2++;
                    break;

                case "8":
                    totalazuljug2++;
                    break;

                case "9":
                    totalrojojug2++;
                    break;

                case "10":
                    totalrojojug2++;
                    break;

                case "11":
                    totalrojojug2++;
                    totalocurajug2++;
                    break;

                case "14":
                    totalocurajug2++;
                    break;

                case "15":
                    totalrosajug2++;
                    break;

                case "16":
                    totalrosajug2++;
                    totalocurajug2++;
                    break;

                case "17":
                    totalrosajug2++;
                    totalocurajug2++;
                    break;
            }
            Debug.Log("totalverde2:" + totalverdejug2);
            Debug.Log("totalazul2:" + totalazuljug2);
            Debug.Log("totalamarilo2:" + totalamarillojug2);
            Debug.Log("totalrojo2:" + totalrojojug2);
            Debug.Log("totalrosa2:" + totalrosajug2);
        }

    }

    //conteo de cartas de los dos jugadores
    public void contador(GameObject hijo)
    {
        if (tipoMazo == TipoMazo.mazoJugador)
        {
            string id = hijo.name.ToString();
            switch (id)
            {
                case "0":
                    int habilidad = 0;
                    habilidad = totalazul * 3;
                    puntos = puntos + habilidad;
                    break;

                case "1":
                    if ((totalazul >= 1) && (totalamarillo >= 1) && (totalrojo >= 1) && (totalrosa >= 1) && (totalverde >= 1))
                    {
                        puntos = puntos + 13;
                    }
                    break;

                case "2":
                    if (totalrojo > totalrojojug2)
                    {
                        puntos = puntos + 7;
                    }
                    break;

                case "3":
                    puntos = puntos + totalocura;
                    break;

                case "4":
                    if (totalamarillo > totalamarillojug2)
                    {
                        puntos = puntos + 7;
                    }
                    break;

                case "5":
                    int habilidad5 = 0;
                    habilidad5 = totalrojo * 3;
                    puntos = puntos + habilidad5;
                    break;

                case "6":
                    int habilidad6 = 0;
                    habilidad6 = totalverde * 3;
                    puntos = puntos + habilidad6;
                    break;

                case "7":
                    if (totalrosa > totalrosajug2)
                    {
                        puntos = puntos + 7;
                    }
                    break;

                case "8":
                    int habilidad8 = 0;
                    if ((totalamarillo >= totalrojo) && (totalamarillo > totalrosa) && (totalrojo > totalrosa))
                    {
                        habilidad8 = totalrosa * 9;
                        puntos = puntos + habilidad8;
                    }
                    else if ((totalrojo >= totalamarillo) && (totalrojo >= totalrosa) && (totalrosa >= totalamarillo))
                    {
                        habilidad8 = totalamarillo * 9;
                        puntos = puntos + habilidad8;
                    }
                    else if ((totalrosa >= totalamarillo) && (totalrosa >= totalrojo) && (totalamarillo >= totalrojo))
                    {
                        habilidad8 = totalrojo * 9;
                        puntos = puntos + habilidad8;
                    }
                    break;

                case "9":
                    int habilidad9 = 0;
                    habilidad9 = totalrosa * 3;
                    puntos = puntos + habilidad9;
                    break;

                case "10":
                    if (totalrojo < 1)
                    {
                        puntos = puntos + 3;
                    }
                    if (totalazul < 1)
                    {
                        puntos = puntos + 3;
                    }
                    if (totalrosa < 1)
                    {
                        puntos = puntos + 3;
                    }
                    if (totalverde < 1)
                    {
                        puntos = puntos + 3;
                    }
                    if (totalamarillo < 1)
                    {
                        puntos = puntos + 3;
                    }
                    break;

                case "11":
                    if (totalazul > totalazuljug2)
                        puntos = puntos + 7;
                    break;

                case "12":
                    totalocura = totalocura + 1;

                    break;

                case "13":
                    // falta esta
                    break;

                case "14":
                    //en todos los colores
                    if (totalamarillo > totalamarillojug2)
                    {
                        puntos = puntos + 4;
                    }
                    if (totalverde > totalverdejug2)
                    {
                        puntos = puntos + 4;
                    }
                    if (totalazul > totalazuljug2)
                    {
                        puntos = puntos + 4;
                    }
                    if (totalrojo > totalrojojug2)
                    {
                        puntos = puntos + 4;
                    }
                    if (totalrosa > totalrosajug2)
                    {
                        puntos = puntos + 4;
                    }

                    break;

                case "15":
                    int habilidad15 = 0;
                    habilidad15 = totalamarillo * 3;
                    puntos = puntos + habilidad15;
                    break;

                case "16":
                    if (totalverde > totalverdejug2)
                    {
                        puntos = puntos + 7;
                    }
                    break;
                //fallo
                case "17":
                    int habilidad17 = 0;
                    if (totalverde >= totalazul)
                    {
                        habilidad17 = totalazul * 6;
                    }
                    else
                    {
                        habilidad17 = totalverde * 6;
                    }
                    puntos = puntos + habilidad17;
                    break;

            }
            Debug.Log("puntos" + puntos);
            Debug.Log("totalocura" + totalocura);
            GameObject.Find("puntosjug1").GetComponent<Text>().text = puntos.ToString();
            GameObject.Find("locurajug1").GetComponent<Text>().text = totalocura.ToString();

        }


    }

    public void contadorjug2(GameObject hijo)
    {
        if (tipoMazo == TipoMazo.mazoOponente)
        {
            string id = hijo.name.ToString();


            switch (id)
            {
                case "0":
                    int habilidad = 0;
                    habilidad = totalazuljug2 * 3;
                    puntosjug2 = puntosjug2 + habilidad;
                    break;

                case "1":
                    if (totalazuljug2 >= 1 && totalamarillojug2 >= 1 && totalrojojug2 >= 1 && totalrosajug2 >= 1 && totalverdejug2 >= 1)
                    {
                        puntosjug2 = puntosjug2 + 13;
                    }
                    break;

                case "2":
                    if (totalrojojug2 > totalrojo)
                    {
                        puntosjug2 = puntosjug2 + 7;
                    }
                    break;

                case "3":
                    puntosjug2 = puntosjug2 + totalocurajug2;
                    break;

                case "4":
                    if (totalamarillojug2 > totalamarillo)
                    {
                        puntosjug2 = puntosjug2 + 7;
                    }
                    break;

                case "5":
                    int habilidad5 = 0;
                    habilidad5 = totalrojojug2 * 3;
                    puntosjug2 = puntosjug2 + habilidad5;
                    break;

                case "6":
                    int habilidad6 = 0;
                    habilidad6 = totalverdejug2 * 3;
                    puntosjug2 = puntosjug2 + habilidad6;
                    break;

                case "7":
                    if (totalrosajug2 > totalrosa)
                    {
                        puntosjug2 = puntosjug2 + 7;
                    }
                    break;

                case "8":
                    int habilidad8 = 0;
                    if (totalamarillojug2 > totalrojojug2 && totalamarillojug2 > totalrosajug2 && totalrojojug2 >= totalrosajug2)
                    {
                        habilidad8 = totalrosajug2 * 9;
                        puntosjug2 = puntosjug2 + habilidad8;
                    }
                    else if (totalrojojug2 >= totalamarillojug2 && totalrojojug2 >= totalrosajug2 && totalrosajug2 >= totalamarillojug2)
                    {
                        habilidad8 = totalamarillojug2 * 9;
                        puntosjug2 = puntosjug2 + habilidad8;
                    }
                    else if (totalrosajug2 >= totalamarillojug2 && totalrosajug2 >= totalrojojug2 && totalamarillojug2 >= totalrojojug2)
                    {
                        habilidad8 = totalrojojug2 * 9;
                        puntosjug2 = puntosjug2 + habilidad8;
                    }
                    break;

                case "9":
                    int habilidad9 = 0;
                    habilidad9 = totalrosajug2 * 3;
                    puntosjug2 = puntosjug2 + habilidad9;
                    break;

                case "10":
                    if (totalrojojug2 < 1)
                    {
                        puntosjug2 = puntosjug2 + 3;
                    }
                    if (totalazuljug2 < 1)
                    {
                        puntosjug2 = puntosjug2 + 3;
                    }
                    if (totalrosajug2 < 1)
                    {
                        puntosjug2 = puntosjug2 + 3;
                    }
                    if (totalverdejug2 < 1)
                    {
                        puntosjug2 = puntosjug2 + 3;
                    }
                    if (totalamarillojug2 < 1)
                    {
                        puntosjug2 = puntosjug2 + 3;
                    }
                    break;

                case "11":
                    if (totalazuljug2 > totalazul)
                        puntosjug2 = puntosjug2 + 7;
                    break;

                case "12":
                    totalocurajug2 = totalocurajug2 + 1;
                    break;

                case "13":
                    // falta esta
                    break;

                case "14":
                    //en todos los colores
                    if (totalamarillojug2 > totalamarillo)
                    {
                        puntosjug2 = puntosjug2 + 4;
                    }
                    if (totalverdejug2 > totalverde)
                    {
                        puntosjug2 = puntosjug2 + 4;
                    }
                    if (totalazuljug2 > totalazul)
                    {
                        puntosjug2 = puntosjug2 + 4;
                    }
                    if (totalrojojug2 > totalrojo)
                    {
                        puntosjug2 = puntosjug2 + 4;
                    }
                    if (totalrosajug2 > totalrosa)
                    {
                        puntosjug2 = puntosjug2 + 4;
                    }

                    break;

                case "15":
                    int habilidad15 = 0;
                    habilidad15 = totalamarillojug2 * 3;
                    puntosjug2 = puntosjug2 + habilidad15;
                    break;

                case "16":
                    if (totalverdejug2 > totalverde)
                    {
                        puntosjug2 = puntosjug2 + 7;
                    }
                    break;

                case "17":
                    int habilidad17 = 0;
                    if (totalverdejug2 > totalazuljug2)
                    {
                        habilidad17 = totalazuljug2 * 6;
                    }
                    else
                    {
                        habilidad17 = totalverdejug2 * 6;
                    }
                    puntosjug2 = puntosjug2 + habilidad17;
                    break;

            }

            GameObject.Find("puntosjug2").GetComponent<Text>().text = puntosjug2.ToString();
            GameObject.Find("locurajug2").GetComponent<Text>().text = totalocurajug2.ToString();

            Debug.Log("puntos2:" + puntosjug2);
            Debug.Log("totalocura2:" + totalocurajug2);

        }
    }
    //final de ronda agregar a suma puntos
    public void finRonda(string fin)
    {
        switch (fin)
        {
            case "locura":
                totalocura++;
                FinRonda.SetActive(false);
                break;
            case "puntos":
                puntos = puntos + 4;
                FinRonda.SetActive(false);
                break;
        }
    }

}



