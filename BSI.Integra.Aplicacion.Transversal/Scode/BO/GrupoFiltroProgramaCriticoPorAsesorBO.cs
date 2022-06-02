using BSI.Integra.Aplicacion.Classes;
using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.SCode.Repository;
using System.Linq;
using System.Transactions;
using BSI.Integra.Aplicacion.Base.BO;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    ///BO: GrupoFiltroProgramaCriticoPorAsesorBO
    ///Autor: Gian Miranda
    ///Fecha: 23/04/2021
    ///<summary>
    ///BO de GrupoFiltroProgramaCriticoPorAsesor, contiene las propiedades y logica de negocios
    ///</summary>
    public class GrupoFiltroProgramaCriticoPorAsesorBO : BaseBO
    {
        ///Propiedades		                        Significado
        ///-------------	                        -----------------------
        ///IdGrupoFiltroProgramaCritico             Id del grupo de filtro programa critico (PK de la tabla pla.T_GrupoFiltroProgramaCritico)
        ///IdPersonal                               Id del personal (PK de la tabla gp.T_Personal)
        ///IdMigracion                              Id de migración de V3 (campo nullable)
        public int? IdGrupoFiltroProgramaCritico { get; set; }
        public int IdPersonal { get; set; }
        public Guid? IdMigracion { get; set; }
    }

}
