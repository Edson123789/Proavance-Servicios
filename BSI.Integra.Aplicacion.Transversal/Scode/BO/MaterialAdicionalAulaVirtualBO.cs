///BO: MaterialAdicionalAulaVirtualBO
///Autor: Lourdes Priscila Pacsi Gamboa
///Fecha: 18/06/2021
///<summary>
///Columnas de la tabla T_MaterialAdicionalAulaVirtual
///</summary>
using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class MaterialAdicionalAulaVirtualBO : BaseBO
    {
        public string NombreConfiguracion { get; set; }
        public int IdPgeneral { get; set; }
        public bool? EsOnline { get; set; }
    }
}
