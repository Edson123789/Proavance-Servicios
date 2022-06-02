using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using System.Collections.Generic;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class CargoMoraBO : BaseBO
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int? Orden { get; set; }

        private CargoMoraRepositorio _repCargo;

        public CargoMoraBO()
        {
            _repCargo = new CargoMoraRepositorio();
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
