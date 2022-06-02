using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Maestros.Repositorio;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class IndustriaBO : BaseBO
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }

        private IndustriaRepositorio _repIndustria;

        public IndustriaBO()
        {
            _repIndustria = new IndustriaRepositorio();
        }
        /// <summary>
        /// Obtiene todas las industrias para ser usados en ComboBox
        /// </summary>
        /// <returns></returns>
        public List<IndustriaFiltroDTO> ObtenerTodoFiltro()
        {
            return _repIndustria.ObtenerIndustriaFiltro();
        }
    }
}
