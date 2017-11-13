using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ManejadorInterfaz : MonoBehaviour {
    GameObject panelcartainfo;
    GameObject panelComodin;
    GameObject panelFinRonda;

    
    
    void Awake()
    {
        encontrarPanel();
       
    }

    //hace la busqueda de los paneles y los oculta
    public void encontrarPanel()
    {
        panelcartainfo = GameObject.FindGameObjectWithTag("infoCarta");
        panelcartainfo.SetActive(false);

        panelComodin = GameObject.FindGameObjectWithTag("panelComodin");
        panelComodin.SetActive(false);

        panelFinRonda = GameObject.FindGameObjectWithTag("panelFinRonda");
        panelFinRonda.SetActive(false);

    }

//aparece el panel de la informacion de la carta
    public void aparecerPanelCarta(bool activa, string info)
    {
        if (activa == true)
        {
            panelcartainfo.SetActive(true);
            GameObject.FindGameObjectWithTag("textoInfoCarta").GetComponent<Text>().text = info;
        }
        else
        {
            panelcartainfo.SetActive(false);
        }
    }
    //aparece panel para seleccionar un suit par ala carta comodin
    public void aparecerPanelComodin()
    {
        panelComodin.SetActive(true);
          
    }

    //oculta panel de informacion de la carta
    public void esconderPanelCarta()
    {
        panelcartainfo.SetActive(false);
        
        
    }
    // elegir locura o puntos
    public void aparecerPanelFinRonda()
    {
        panelFinRonda.SetActive(true);
    }
   
}