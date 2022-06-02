///BO: AccesosIntegraDetalleLogBO
///Autor: Edgar S.
///Fecha: 27/01/2021
///<summary>
///Columnas de la tabla T_AccesosIntegraDetalleLog
///</summary>
using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class AccesosIntegraDetalleLogBO : BaseBO
    {
        ///Propiedades		            Significado
        ///-------------	            -----------------------
        ///IdAccesosIntegraLog          FK de T_ AccesosIntegraLog   
        ///Tipo                         Tipo
        ///Valor                        Valor
        ///Fecha                        Fecha
        public int IdAccesosIntegraLog { get; set; }
        public string Tipo { get; set; }
        public string Valor { get; set; }
        public DateTime Fecha { get; set; }        
    }
}
