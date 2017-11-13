using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

public class Accion : MessageBase {

    public static short TipoMensaje = 80;  
    
    public int id; //para la carta enviada
    public TipoMovimiento tipoMovimiento;

    public enum TipoMovimiento { Ninguno, Perdio, Gano, OrdenMazoJalar }

}
public class Reparto: MessageBase
{
    public static short TipoMensaje = 81;
    public int[] idcartas;
}

