using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class FlujoOcurrenciaDTO
    {
        public int Id { get; set; }
        public int IdFlujoActividad { get; set; }
        public int Orden { get; set; }
        public string Nombre { get; set; }
        public bool CerrarSeguimiento { get; set; }
        public int? IdFaseDestino { get; set; }
        public int? IdFlujoActividadSiguiente { get; set; }

        public string NombreUsuario { get; set; }

        public string FaseDestino { get; set; }
        public string ActividadSiguiente { get; set; }
    }
}
