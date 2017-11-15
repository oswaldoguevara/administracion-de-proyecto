using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Contador : MonoBehaviour {
    //Arreglo que contiene los hijos de este objeto (osea las cartas)
    public GameObject[] hijos;
    public Carta[] hijosCarta;
    public TipoMazo tipoMazo = TipoMazo.mazoJalar;
    public GameObject mazoTableroJugador;
    public GameObject mazoTableroOponente;

    public GameObject comodin;

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



    

   
    public void hacerConteo()
    {
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


    //carta comodin
    public void valorComodin(string daenerys)
    {
        //nos da el nuevo color que sera la carta comodin
        switch (daenerys)
        {
            case "verde":
                totalverde++;
                
                totalocurajug2 = totalocurajug2 - 1;
                comodin.SetActive(false);
                break;
            case "azul":
                totalazul++;
                
                totalocurajug2 = totalocurajug2 - 1;
                comodin.SetActive(false);
                break;
            case "amarillo":
                totalamarillo++;
               
                totalocurajug2 = totalocurajug2 - 1;
                comodin.SetActive(false);
                break;
            case "rojo":
                totalrojo++;
                
                totalocurajug2 = totalocurajug2 - 1;
                comodin.SetActive(false);
                break;
            case "rosa":
                totalrosa++;
                
                totalocurajug2 = totalocurajug2 - 1;
                comodin.SetActive(false);
                break;

        }
    }
    //carta comodin
    public void valorComodiOponente(string daenerys)
    {
        //nos da el nuevo color que sera la carta comodin
        switch (daenerys)
        {
            case "verde":
                totalverdejug2++;
                totalocura = totalocura - 1;
                comodin.SetActive(false);
                break;
            case "azul":
                totalazuljug2++;
                totalocura = totalocura - 1;
                comodin.SetActive(false);
                break;
            case "amarillo":
                totalamarillojug2++;
                totalocura = totalocura - 1;
                comodin.SetActive(false);
                break;
            case "rojo":
                totalrojojug2++;
                totalocura = totalocura - 1;
                comodin.SetActive(false);
                break;
            case "rosa":
                totalrosajug2++;
                totalocura = totalocura - 1;
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

    public int doble = 0;
    public int dobleoponente = 0;
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
                    doble = habilidad;
                    break;

                case "1":
                    if ((totalazul >= 1) && (totalamarillo >= 1) && (totalrojo >= 1) && (totalrosa >= 1) && (totalverde >= 1))
                    {
                        puntos = puntos + 13;
                        doble = 13;
                    }
                    break;

                case "2":
                    if (totalrojo > totalrojojug2)
                    {
                        puntos = puntos + 7;
                        doble = 7;
                    }
                    break;

                case "3":
                    puntos = puntos + totalocura;
                    doble = totalocura;
                    break;

                case "4":
                    if (totalamarillo > totalamarillojug2)
                    {
                        puntos = puntos + 7;
                        doble = 7;
                    }
                    break;

                case "5":
                    int habilidad5 = 0;
                    habilidad5 = totalrojo * 3;
                    puntos = puntos + habilidad5;
                    doble = habilidad5;
                    break;

                case "6":
                    int habilidad6 = 0;
                    habilidad6 = totalverde * 3;
                    puntos = puntos + habilidad6;
                    doble = habilidad6;
                    break;

                case "7":
                    if (totalrosa > totalrosajug2)
                    {
                        puntos = puntos + 7;
                        doble = 7;
                    }
                    break;

                case "8":
                    int habilidad8 = 0;
                    if ((totalamarillo >= totalrojo) && (totalamarillo > totalrosa) && (totalrojo > totalrosa))
                    {
                        habilidad8 = totalrosa * 9;
                        puntos = puntos + habilidad8;
                        doble = habilidad8;
                    }
                    else if ((totalrojo >= totalamarillo) && (totalrojo >= totalrosa) && (totalrosa >= totalamarillo))
                    {
                        habilidad8 = totalamarillo * 9;
                        puntos = puntos + habilidad8;
                        doble = habilidad8;
                    }
                    else if ((totalrosa >= totalamarillo) && (totalrosa >= totalrojo) && (totalamarillo >= totalrojo))
                    {
                        habilidad8 = totalrojo * 9;
                        puntos = puntos + habilidad8;
                        doble = habilidad8;
                    }
                    break;

                case "9":
                    int habilidad9 = 0;
                    habilidad9 = totalrosa * 3;
                    puntos = puntos + habilidad9;
                    doble = habilidad9;
                    break;

                case "10":
                    if (totalrojo < 1)
                    {
                        puntos = puntos + 3;
                        doble = doble + 3;
                    }
                    if (totalazul < 1)
                    {
                        puntos = puntos + 3;
                        doble = doble + 3;
                    }
                    if (totalrosa < 1)
                    {
                        puntos = puntos + 3;
                        doble = doble + 3;
                    }
                    if (totalverde < 1)
                    {
                        puntos = puntos + 3;
                        doble = doble + 3;
                    }
                    if (totalamarillo < 1)
                    {
                        puntos = puntos + 3;
                        doble = doble + 3;
                    }
                    break;

                case "11":
                    if (totalazul > totalazuljug2)
                    {
                        puntos = puntos + 7;
                        doble = 7;
                    }
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
                        doble = doble + 4;
                    }
                    if (totalverde > totalverdejug2)
                    {
                        puntos = puntos + 4;
                        doble = doble + 4;

                    }
                    if (totalazul > totalazuljug2)
                    {
                        puntos = puntos + 4;
                        doble = doble + 4;
                    }
                    if (totalrojo > totalrojojug2)
                    {
                        puntos = puntos + 4;
                        doble = doble + 4;
                    }
                    if (totalrosa > totalrosajug2)
                    {
                        puntos = puntos + 4;
                        doble = doble + 4;
                    }

                    break;

                case "15":
                    int habilidad15 = 0;
                    habilidad15 = totalamarillo * 3;
                    puntos = puntos + habilidad15;
                    doble = habilidad15;
                    break;

                case "16":
                    if (totalverde > totalverdejug2)
                    {
                        puntos = puntos + 7;
                        doble = 7;
                    }
                    break;
                //fallo
                case "17":
                    int habilidad17 = 0;
                    if (totalverde >= totalazul)
                    {
                        habilidad17 = totalazul * 6;
                        puntos = puntos + habilidad17;
                        doble = habilidad17;


                    }
                    else
                    {
                        habilidad17 = totalverde * 6;
                        puntos = puntos + habilidad17;
                        doble = habilidad17;
                    }
                    
                    break;

            }
            Debug.Log("puntos" + puntos);
            Debug.Log("totalocura" + totalocura);
            GameObject.Find("puntosJugador").GetComponent<Text>().text = puntos.ToString();
            GameObject.Find("locurasJugador").GetComponent<Text>().text = totalocura.ToString();

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
                    dobleoponente = habilidad;
                    break;

                case "1":
                    if (totalazuljug2 >= 1 && totalamarillojug2 >= 1 && totalrojojug2 >= 1 && totalrosajug2 >= 1 && totalverdejug2 >= 1)
                    {
                        puntosjug2 = puntosjug2 + 13;
                        dobleoponente = 13;
                    }
                    break;

                case "2":
                    if (totalrojojug2 > totalrojo)
                    {
                        puntosjug2 = puntosjug2 + 7;
                        dobleoponente = 7;
                    }
                    break;

                case "3":
                    puntosjug2 = puntosjug2 + totalocurajug2;
                    dobleoponente = totalocurajug2;
                    break;

                case "4":
                    if (totalamarillojug2 > totalamarillo)
                    {
                        puntosjug2 = puntosjug2 + 7;
                        dobleoponente = 7;
                    }
                    break;

                case "5":
                    int habilidad5 = 0;
                    habilidad5 = totalrojojug2 * 3;
                    puntosjug2 = puntosjug2 + habilidad5;
                    dobleoponente = habilidad5;
                    break;

                case "6":
                    int habilidad6 = 0;
                    habilidad6 = totalverdejug2 * 3;
                    puntosjug2 = puntosjug2 + habilidad6;
                    dobleoponente = habilidad6;
                    break;

                case "7":
                    if (totalrosajug2 > totalrosa)
                    {
                        puntosjug2 = puntosjug2 + 7;
                        dobleoponente = 7;
                    }
                    break;

                case "8":
                    int habilidad8 = 0;
                    if (totalamarillojug2 > totalrojojug2 && totalamarillojug2 > totalrosajug2 && totalrojojug2 >= totalrosajug2)
                    {
                        habilidad8 = totalrosajug2 * 9;
                        puntosjug2 = puntosjug2 + habilidad8;
                        dobleoponente = habilidad8;
                    }
                    else if (totalrojojug2 >= totalamarillojug2 && totalrojojug2 >= totalrosajug2 && totalrosajug2 >= totalamarillojug2)
                    {
                        habilidad8 = totalamarillojug2 * 9;
                        puntosjug2 = puntosjug2 + habilidad8;
                        dobleoponente = habilidad8;
                    }
                    else if (totalrosajug2 >= totalamarillojug2 && totalrosajug2 >= totalrojojug2 && totalamarillojug2 >= totalrojojug2)
                    {
                        habilidad8 = totalrojojug2 * 9;
                        puntosjug2 = puntosjug2 + habilidad8;
                        dobleoponente = habilidad8;
                    }
                    break;

                case "9":
                    int habilidad9 = 0;
                    habilidad9 = totalrosajug2 * 3;
                    puntosjug2 = puntosjug2 + habilidad9;
                    dobleoponente = habilidad9;
                    break;

                case "10":
                    if (totalrojojug2 < 1)
                    {
                        puntosjug2 = puntosjug2 + 3;
                        dobleoponente = dobleoponente +3;
                    }
                    if (totalazuljug2 < 1)
                        dobleoponente = 3;
                    {
                        puntosjug2 = puntosjug2 + 3;
                        dobleoponente = dobleoponente+ 3;
                    }
                    if (totalrosajug2 < 1)
                    {
                        puntosjug2 = puntosjug2 + 3;
                        dobleoponente = dobleoponente + 3;
                    }
                    if (totalverdejug2 < 1)
                    {
                        puntosjug2 = puntosjug2 + 3;
                        dobleoponente = dobleoponente+  3;
                    }
                    if (totalamarillojug2 < 1)
                    {
                        puntosjug2 = puntosjug2 + 3;
                        dobleoponente = dobleoponente +3;
                    }
                    break;

                case "11":
                    if (totalazuljug2 > totalazul)
                    {
                        puntosjug2 = puntosjug2 + 7;
                        dobleoponente = 7;
                    }
                    break;

                case "12":
                    totalocurajug2 = totalocurajug2 + 1;

                    break;

                case "13":
                    puntosjug2 = puntosjug2 + dobleoponente;
                    break;

                case "14":
                    //en todos los colores
                    if (totalamarillojug2 > totalamarillo)
                    {
                        puntosjug2 = puntosjug2 + 4;
                        dobleoponente = dobleoponente + 4;
                    }
                    if (totalverdejug2 > totalverde)
                    {
                        puntosjug2 = puntosjug2 + 4;
                        dobleoponente = dobleoponente + 4;
                    }
                    if (totalazuljug2 > totalazul)
                    {
                        puntosjug2 = puntosjug2 + 4;
                        dobleoponente = dobleoponente + 4;
                    }
                    if (totalrojojug2 > totalrojo)
                    {
                        puntosjug2 = puntosjug2 + 4;
                        dobleoponente = dobleoponente + 4;
                    }
                    if (totalrosajug2 > totalrosa)
                    {
                        puntosjug2 = puntosjug2 + 4;
                        dobleoponente = dobleoponente + 4;
                    }

                    break;

                case "15":
                    int habilidad15 = 0;
                    habilidad15 = totalamarillojug2 * 3;
                    puntosjug2 = puntosjug2 + habilidad15;
                    dobleoponente = habilidad15;
                    break;

                case "16":
                    if (totalverdejug2 > totalverde)
                    {
                        puntosjug2 = puntosjug2 + 7;
                        dobleoponente = 7;
                    }
                    break;

                case "17":
                    int habilidad17 = 0;
                    if (totalverdejug2 > totalazuljug2)
                    {
                        habilidad17 = totalazuljug2 * 6;
                        puntosjug2 = puntosjug2 + habilidad17;
                        dobleoponente = habilidad17; 
                    }
                    else
                    {
                        habilidad17 = totalverdejug2 * 6;
                        puntosjug2 = puntosjug2 + habilidad17;
                        dobleoponente = habilidad17;
                    }
                    
                    break;

            }

            GameObject.Find("puntosOponente").GetComponent<Text>().text = puntosjug2.ToString();
            GameObject.Find("locurasOponente").GetComponent<Text>().text = totalocurajug2.ToString();

            Debug.Log("puntosOponente:" + puntosjug2);
            Debug.Log("totalocuraOponente:" + totalocurajug2);

        }
    }

    public enum TipoMazo { mazoJalar, mazoDejar, mazoJugador, mazoOponente, tableroJugador, tableroOponente, mazoVacio, locuras }
}



