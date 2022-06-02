///BO: MaterialAdicionalAulaVirtualPEspecificoBO
///Autor: Lourdes Priscila Pacsi Gamboa
///Fecha: 18/06/2021
///<summary>
///Columnas de la tabla T_MaterialAdicionalAulaVirtualPEspecifico
///</summary>
using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class MaterialAdicionalAulaVirtualPEspecificoBO : BaseBO
    {
        public int IdPespecifico { get; set; }
        public int? IdMaterialAdicionalAulaVirtual { get; set; }
    }
}
