using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Operaciones.Repositorio;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Operaciones.BO
{
    public partial class GestionAsistenciaOnlineBO
    {
        private RaCentroCostoRepositorio _repRaCentroCosto;
        public GestionAsistenciaOnlineBO() {
            _repRaCentroCosto = new RaCentroCostoRepositorio();
        }

        /// <summary>
        /// Obtiene un listado de centros de costos con asistencia mensual
        /// </summary>
        public List<CentroCostoListadoMensualDTO> ListadoCentroCostoConAsistenciaMensual() {
            try
            {
                return _repRaCentroCosto.ListadoCentroCostoConAsistenciaMensual();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
