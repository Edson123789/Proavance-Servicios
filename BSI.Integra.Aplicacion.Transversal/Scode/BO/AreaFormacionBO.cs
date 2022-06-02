using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Maestros.Repositorio;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    /// BO: Transversal/AreaFormacion
    /// Autor: Fischer Valdez - Richar Zenteno - Wilber Choque 
    /// Fecha: 30/04/2019
    /// <summary>
    /// BO para la logica de T_AreaFormacion
    /// </summary>
    public class AreaFormacionBO: BaseBO
    {
        /// Propiedades             Significado
		/// -----------	            ------------
		/// Nombre                  Nombre de Área de Formación
        /// RowVersion              Byte de Versión de Fila
        public string Nombre { get; set; }
        public byte[] RowVersion { get; set; }

        private AreaFormacionRepositorio _repAreaFormacion;

        public AreaFormacionBO()
        {
            _repAreaFormacion = new AreaFormacionRepositorio();
        }

        /// Autor:
        /// Fecha: 22/03/2021
        /// <summary>
        /// Obtiene todas las area formacion para ser usado en ComboBox
        /// </summary>
        /// <returns>List<AreaFormacionFiltroDTO></returns>
        public List<AreaFormacionFiltroDTO> ObtenerTodoFiltro()
        {
            return _repAreaFormacion.ObtenerAreaFormacionFiltro();
        }
    }
}
