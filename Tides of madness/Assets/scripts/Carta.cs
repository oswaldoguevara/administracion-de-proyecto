using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Carta : MonoBehaviour
{

    public int id;
    public int puntos;
    public int locuras;
    public string color;
    public string texto;
    public Mazos mazos;

    public bool seleccionada = false;
   
    public void CambiarSpriteFrente()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load(id + "", typeof(Sprite)) as Sprite;
        identificarCartas();
    }


    public void CambiarSpriteAtras()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load("trasera", typeof(Sprite)) as Sprite;
        identificarCartas();
    }
    private void OnMouseDown()
    {
       
        if (mazos.tipoMazo == Mazos.TipoMazo.mazoJug1)
        {


            if (seleccionada)
            {
              
                SetSeleccionada(false);
            }
            else if (!seleccionada)
            {
                //Seleccionar
                if (FindObjectOfType<Administrador>().AgregarASeleccion(gameObject, true))
                {
                    SetSeleccionada(true);
                }
            }
        }
        // SIMULADOR PARA LA SELECCION QUE HACE EL OPONENTE
        if (mazos.tipoMazo == Mazos.TipoMazo.mazoOponente)
        {


             if (!seleccionada)
            {
                //Seleccionar
                if (FindObjectOfType<Administrador>().AgregarASeleccion2(gameObject, true))
                {
                    SetSeleccionada(true);
                }
            }
        }

    }
    public void OnMouseOver()
    {
        if (mazos.tipoMazo == Mazos.TipoMazo.mazoJug1)
        {
            FindObjectOfType<ManejadorInterfaz>().aparecerPanelCarta(true, texto);
        }
       
   }
    private void OnMouseExit()
    {
             FindObjectOfType<ManejadorInterfaz>().esconderPanelCarta();
    }



    public void SetSeleccionada(bool param)
    {
       seleccionada = param;
    }


    public void identificarCartas()
    {
        gameObject.name = id + "";
        switch (id)
        {
            case 0:
                puntos = 3;
                locuras = 0;
                color = "amarillo";
                texto = "Gana 3 puntos por cada Grandes Ancestros.";
                
                break;
            case 1:
                puntos = 13;
                locuras = 0;
                color = "amarillo";
                texto = "Gana 13 puntos por cada set de Dioses Exteriores, Localizaciones, Manuscritos, Grandes Ancestros,  y Razas.";
               
                break;
            case 2:
                puntos = 7;
                locuras = 1;
                color = "amarillo";
                texto = "Gana 7 puntos por cada mayoría de Localizaciones.";
          
                break;

            case 3:
                puntos = 1;
                locuras = 1;
                color = "verde";
                texto = "Gana 1 punto por cada locura que tengas.";
             
                break;
            case 4:
                puntos = 7;
                locuras = 1;
                color = "verde";
                texto = "Gana 7 puntos por cada mayoría de Dioses Exteriores.";
            
                break;
            case 5:
                puntos = 3;
                locuras = 0;
                color = "verde";
                texto = "Gana 3 puntos por cada Localizaciones.";
               
                break;
            case 6:
                puntos = 3;
                locuras = 0;
                color = "azul";
                texto = "Gana 3 puntos por cada Manuscrito.";
               
                break;
            case 7:
                puntos = 7;
                locuras = 1;
                color = "azul";
                texto = "Gana 7 puntos por cada mayoría de Razas.";
           
                break;
            case 8:
                puntos = 9;
                locuras = 0;
                color = "azul";
                texto = "Gana 9 puntos por cada set de Dioses Exteriores, Localizaciones, y Razas.";
            
                break;
            case 9:
                puntos = 3;
                locuras = 0;
                color = "rojo";
                texto = "Gana 3 puntos por cada Razas.";
              
                break;
            case 10:
                puntos = 3;
                locuras = 0;
                color = "rojo";
                texto = "Gana 3 puntos por cada suit que no tengas.";
               
                break;
            case 11:
                puntos = 7;
                locuras = 1;
                color = "rojo";
                texto = "Gana 7 puntos por cada mayoría de Grandes Ancestros.";
               
                break;
            case 12:
                // COMODIN CAMBIA DE COLOR,AGARRO UNA LOCURA DEL OPONENTE
                puntos = 0;
                locuras = 1; //PERO LE RESTO UNA A LAS DEL OPONENTE
                color = "sincolor";
                texto = "Toma una Locura del oponente y esta carta toma cualquier suit.";
                
                break;
            case 13:
                //DOBLE DE LA CARTA ANTERIOR
                //    puntos = 0;
                locuras = 1;
                color = "sincolor";
                texto = "Gana el doble de puntos de la carta jugada anteriormente.";
               
                break;
            case 14:
                puntos = 4; //SIN COLOR //SE GANAN POR CADA MAYORIA QUE TENGA VS EL OPONENTE
                locuras = 1;
                color = "sincolor";
                texto = "Gana 4 puntos por cada mayoría que tengas.";
                
                break;
            case 15:
                puntos = 3;
                locuras = 0;
                color = "rosa";
                texto = "Gana 3 puntos por cada Dioses Exteriores.";
              
                break;
            case 16:
                puntos = 7;
                locuras = 1;
                color = "rosa";
                texto = "Gana 7 puntos por cada mayoría de Manuscritos.";
                
                break;
            case 17:
                puntos = 6;
                locuras = 1;
                color = "rosa";
                texto = "Gana 6 puntos por cada set de Grandes Ancestros y Manuscritos.";
                
                break;


        }
    }


}



