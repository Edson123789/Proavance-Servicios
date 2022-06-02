using BSI.Integra.Aplicacion.Classes;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Persistencia.SCode.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.Repositorio;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class AreaCapacitacionBO : BaseBO
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string ImgPortada { get; set; }
        public string ImgSecundaria { get; set; }
        public string ImgPortadaAlt { get; set; }
        public string ImgSecundariaAlt { get; set; }
        public bool EsVisibleWeb { get; set; }
        public int? IdArea { get; set; }
        public bool EsWeb { get; set; }
        public string DescripcionHtml { get; set; }
        public int? IdAreaCapacitacionFacebook { get; set; }
        public Guid? IdMigracion { get; set; }
        public List<AreaParametroSeoPwBO> AreaParametroSeoPw { get; set; }

        private AreaCapacitacionRepositorio _repAreaCapacitacion;

        public AreaCapacitacionBO()
        {
            _repAreaCapacitacion = new AreaCapacitacionRepositorio();
        }

        /// <summary>
        /// Obtiene el Id,Nombre de la area de capacitacion para ser listada
        /// </summary>
        /// <returns></returns>
        public List<FiltroDTO> ObtenerTodoFiltro()
        {
            return _repAreaCapacitacion.ObtenerTodoFiltro();
        }
    }

    //public class AreaCapacitacionFiltro
    //{
    //    public int Id { get; set; }
    //    public string Nombre { get; set; }
    //}

}
