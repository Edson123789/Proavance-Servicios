using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.Classes;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.SCode.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class EmpresaBO : BaseBO
    {
        public string Nombre { get; set; }
        public string Ruc { get; set; }
        public int? IdTipoIdentificador { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string PaginaWeb { get; set; }
        public string Email { get; set; }
        public int? Trabajadores { get; set; }
        public double? NivelFacturacion { get; set; }
        public int? IdPais { get; set; }
        public int? IdRegion { get; set; }
        public int? IdCiudad { get; set; }
        public int? IdIndustria { get; set; }
        public string IdTipoEmpresa { get; set; }
        public int? IdTamanio { get; set; }
        public int? Ciiu { get; set; }
        public int? IdCodigoCiiuIndustria { get; set; }
        public Guid? IdMigracion { get; set; }
        private EmpresaRepositorio _repEmpresa;

        public EmpresaBO()
        {
            _repEmpresa = new EmpresaRepositorio();
        }

        /// <summary>
        /// Obtiene una empresa filtrada por id
        /// </summary>
        public EmpresaFiltroDTO ObtenerFiltroPorId(int id) {
            return _repEmpresa.ObtenerFiltroPorId(id);
        }

        /// <summary>
        /// Obtiene una lista de empresas competidoras
        /// </summary>
        public List<EmpresaFiltroDTO> ObtenerTodoCompetidores()
        {
            return _repEmpresa.ObtenerTodoCompetidores();
        }
    }
}
