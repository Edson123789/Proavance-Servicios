using System;
using System.Collections.Generic;
using System.Text;
using BSI.Integra.Aplicacion.Classes;
using BSI.Integra.Persistencia.SCode.Repository;
using System.Linq;
using Newtonsoft.Json;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Aplicacion.Base.BO;


namespace BSI.Integra.Aplicacion.Planificacion.BO
{
    ///BO: GrupoFiltroProgramaCriticoBO
    ///Autor: Gian Miranda
    ///Fecha: 23/04/2021
    ///<summary>
    ///BO de GrupoFiltroProgramaCritico, contiene las propiedades y logica de negocios
    ///</summary>
    public class GrupoFiltroProgramaCriticoBO : BaseBO
    {
        ///Propiedades		                        Significado
        ///-------------	                        -----------------------
        ///Nombre                                   Nombre del GrupoFiltroProgramaCritico
        ///Descripcion                              Descripcion del GrupoFiltroProgramaCritico
        ///IdMigracion                              Id de migración de V3 (campo nullable)
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public Guid? IdMigracion { get; set; }
        public List<GrupoFiltroProgramaCriticoPorAsesorBO> GrupoFiltroProgramaCriticoPorAsesor { get; set; }

    }
}
