using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Marketing.SCode.BO
{
    ///BO: DiccionarioPalabraOfensivaBO
    ///Autor: Edgar S.
    ///Fecha: 03/03/2021
    ///<summary>
    ///Columnas y funciones de la tabla T_DiccionarioPalabraOfensiva
    ///</summary>
    public class DiccionarioPalabraOfensivaBO : BaseBO
    {
        ///Propiedades		        Significado
        ///-------------	        -----------------------
        ///PalabraFiltrada          Palabra Ofensiva
        public string PalabraFiltrada { get; set; }
        
    }
}
