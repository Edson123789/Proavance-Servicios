using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs.Operaciones
{
    public class FlujoDetalleAgendaDTO
    {
        public int IdPespecifico { get; set; }
        public int IdClasificacionPersona { get; set; }
        public int IdFlujo { get; set; }
        public string Flujo { get; set; }
        public int? IdFlujoFase { get; set; }
        public string Fase { get; set; }
        public int? IdFlujoActividad { get; set; }
        public string Actividad { get; set; }
        public int? IdFlujoOcurrencia { get; set; }
        public string Ocurrencia { get; set; }
        public int? IdFlujoPorPEspecifico { get; set; }
        public DateTime? FechaEjecucion { get; set; }
        public DateTime? FechaSeguimiento { get; set; }

        public int OrdenFase { get; set; }
        public int OrdenActividad { get; set; }
        public int? OrdenOcurrencia { get; set; }

    }
}
