///BO: MaterialAdicionalAulaVirtualRegistroBO
///Autor: Lourdes Priscila Pacsi Gamboa
///Fecha: 18/06/2021
///<summary>
///Columnas de la tabla T_MaterialAdicionalAulaVirtualRegistro
///</summary>
using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class MaterialAdicionalAulaVirtualRegistroBO : BaseBO
    {
        public int IdMaterialAdicionalAulaVirtual { get; set; }
        public string NombreArchivo { get; set; }
        public string RutaArchivo { get; set; }
        public bool EsEnlace { get; set; }
    }
}
