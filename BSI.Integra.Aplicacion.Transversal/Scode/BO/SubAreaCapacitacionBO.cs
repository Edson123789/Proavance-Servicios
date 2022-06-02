using BSI.Integra.Aplicacion.Classes;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.SCode.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.Transversal.Repositorio;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    /// BO: SubAreaCapacitacionBO
    /// Autor: 
    /// Fecha: 24/02/2021
    /// <summary>
    /// Metodos SubAreaCapacitacion
    /// </summary>
    public class SubAreaCapacitacionBO : BaseBO
    {
        /// Propiedades	                    Significado
        /// -----------	                    ------------
        /// Nombre                          Nombre de la sub area de capacitacion
        /// Descripcion                     Descripcion de la sub area
        /// IdAreaCapacitacion              Id del area de capacitacion (PK de la tabla pla.T_AreaCapacitacion)
        /// EsVisibleWeb                    Flag de es visible web
        /// IdSubArea                       Id de la subarea (PK de la tabla pla.T_SubArea)
        /// DescripcionHtml                 Cadena con la descripcion HTML
        /// IdMigracion                     Id de migracion (campo nullable)

        public string Nombre { get; set; }
        public string  Descripcion { get; set; }
        public int IdAreaCapacitacion { get; set; }
        public bool EsVisibleWeb { get; set; }
        public int ? IdSubArea { get; set; }
        public string DescripcionHtml { get; set; }
        public Guid? IdMigracion { get; set; }
        public string AliasFacebook { get; set; }
        public List<SubAreaParametroSeoBO> SubAreaParametroSeo { get; set; }

        private SubAreaCapacitacionRepositorio _repSubAreaCapacitacion;

        public SubAreaCapacitacionBO()
        {
            _repSubAreaCapacitacion = new SubAreaCapacitacionRepositorio();
        }

        /// Autor: Edgar S.
        /// Fecha: 09/02/2021
        /// Version: 1.0
        /// <summary>
        /// Obtiene el Id,Nombre de la subarea de capacitacion para ser listada
        /// </summary>
        /// <returns>Lista de objetos de clase SubAreaCapacitacionFiltroDTO</returns>
        public List<SubAreaCapacitacionFiltroDTO> ObtenerTodoFiltro()
        {
            return _repSubAreaCapacitacion.ObtenerSubAreasParaFiltro();
        }


        /// Autor: Edgar S.
        /// Fecha: 09/02/2021
        /// Version: 1.0
        /// <summary>
        /// Obtiene el Id,IdAreaCapacitacion,Nombre de la subarea de capacitacion ser usada en un autoselect
        /// </summary>
        /// <param></param>
        /// <returns></returns>
        public List<SubAreaCapacitacionAutoselectDTO> ObtenerTodoFiltroAutoselect()
        {
            return _repSubAreaCapacitacion.ObtenerTodoFiltroAutoSelect();
        }
    }
}
