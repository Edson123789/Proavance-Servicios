using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Marketing.BO
{
    ///BO: GrupoFiltroProgramaCriticoCentroCosto
    ///Autor: Gian Miranda
    ///Fecha: 23/04/2021
    ///<summary>
    ///BO de GrupoFiltroProgramaCriticoCentroCosto, contiene las propiedades y logica de negocios
    ///</summary>
    public class GrupoFiltroProgramaCriticoCentroCostoBO : BaseBO
    {
        ///Propiedades		                        Significado
        ///-------------	                        -----------------------
        ///IdGrupoFiltroProgramaCritico             Id del grupo de filtro programa critico (PK de la tabla pla.T_GrupoFiltroProgramaCritico)
        ///IdCentroCosto                            Id del centro de costo (PK de la tabla pla.T_CentroCosto)
        ///IdMigracion                              Id de migracion de V3 (campo nullable)
        public int IdGrupoFiltroProgramaCritico { get; set; }
        public int IdCentroCosto { get; set; }
        public int? IdMigracion { get; set; }

    }
}
