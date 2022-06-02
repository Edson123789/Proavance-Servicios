using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Maestros.Repositorio;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class CargoBO : BaseBO
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int? Orden { get; set; }

        private CargoRepositorio _repCargo;

        public CargoBO()
        {
            _repCargo = new CargoRepositorio();
        }

        /// <summary>
        /// Obtiene los cargos para ser filtrados
        /// </summary>
        /// <returns></returns>
        public List<CargoFiltroDTO> ObtenerTodoFiltro()
        {
            return _repCargo.ObtenerCargoFiltro();
        }
    }
}
