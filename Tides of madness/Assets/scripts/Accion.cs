using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

public class Accion : MessageBase {

    public static short TIPO_MENSAJE = 80;  
    
    public int id; //para la carta enviada
    public int[] idcartas; //para mandar las id ordenadas de las cartas ya barajeadas
    public TipoAccion tipoAccion;

    public enum TipoAccion { Ninguno, Perdio, Gano, OrdenMazoJalar }

}


