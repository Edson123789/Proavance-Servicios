using System;
using System.Collections.Generic;
using System.Text;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.Base.Classes;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Persistencia.Models;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    /// BO: CentroCostoBO
    /// Autor: Edgar S.
    /// Fecha: 30/04/2021
    /// <summary>
    /// Columnas y funciones de la tabla T_CentroCosto
    /// </summary>
    public class CentroCostoBO : BaseBO
	{
        /// Propiedades	            Significado
        /// -------------	        -----------------------
        /// IdArea                  Id de area     
        /// IdSubArea               Id de subarea
        /// IdPgeneral              Id de PGeneral
        /// Nombre                  Nombre de Centro de Costo
        /// Codigo                  Código de centro de costo
        /// IdAreaCc                Id de Area de Centro de Costo
        /// Ismtotales              Cantidad de IS, M totales
        /// Icpftotales             Cantidad de IC, PF totales
        /// IdMigracion             Id de migración
		public int? IdArea { get; set; }
		public int? IdSubArea { get; set; }
		public string IdPgeneral { get; set; }
		public string Nombre { get; set; }
		public string Codigo { get; set; }
		public string IdAreaCc { get; set; }
		public int? Ismtotales { get; set; }
		public int? Icpftotales { get; set; }
        public int? IdMigracion { get; set; }
        public PespecificoBO ProgramaEspecifico { get; set; }
	        
        private CentroCostoRepositorio _repCentroCosto;

        public CentroCostoBO()
        {
            ActualesErrores = new Dictionary<string, List<ErrorInfo>>();
        }
        public CentroCostoBO(integraDBContext contexto)
        {
            ActualesErrores = new Dictionary<string, List<ErrorInfo>>();
            _repCentroCosto = new CentroCostoRepositorio(contexto);
        }
        
        public CentroCostoBO( int id, integraDBContext contexto)
        {
            ActualesErrores = new Dictionary<string, List<ErrorInfo>>();
            _repCentroCosto = new CentroCostoRepositorio(contexto);
            var centroCosto = _repCentroCosto.FirstById(id);
            this.Id = centroCosto.Id;
            this.IdSubArea = centroCosto.IdSubArea;
            this.IdPgeneral = centroCosto.IdPgeneral;
            this.Nombre = centroCosto.Nombre;
            this.Codigo = centroCosto.Codigo;
            this.IdAreaCc = centroCosto.IdAreaCc;
            this.Ismtotales = centroCosto.Ismtotales;
            this.Icpftotales = centroCosto.Icpftotales;
            this.Estado = centroCosto.Estado;
            this.FechaCreacion = centroCosto.FechaCreacion;
            this.FechaModificacion = centroCosto.FechaModificacion;
            this.UsuarioCreacion = centroCosto.UsuarioCreacion;
            this.UsuarioModificacion = centroCosto.UsuarioModificacion;
            this.RowVersion = centroCosto.RowVersion;
        }


        /// <summary>
        /// Obtiene el Id,Nombre de los centros de costo por un parametro
        /// </summary>
        /// <param name="valor"></param>
        /// <returns></returns>
        public List<CentroCostoFiltroAutocompleteDTO> ObtenerTodoFiltroAutocomplete(string valor)
        {
            return _repCentroCosto.ObtenerTodoFiltroAutoComplete(valor);
        }

    }
}
