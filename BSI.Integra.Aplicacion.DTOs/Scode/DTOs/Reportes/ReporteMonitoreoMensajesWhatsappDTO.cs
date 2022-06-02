using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ReporteMonitoreoMensajesWhatsappDTO
    {
        public int IdConjuntoLista { get; set; }
        public int IdConjuntoListaDetalle { get; set; }
        public string NombreConjuntoListaDetalle { get; set; }
        public int CantidadAlumnoTotal { get; set; }
        public int CantidadAlumnoEliminado { get; set; }
        public int CantidadAlumnoDesuscrito { get; set; }
        public int CantidadAlumnoDescartadoIntegra { get; set; }
        public int CantidadAlumnoDescartadoWhatsapp { get; set; }
        public int CantidadAlumnoReal { get; set; }
        public int CantidadAlumnoEnvio30Dias { get; set; }
        public int CantidadAlumnoDisponible { get; set; }
        public int CantidadAlumnoRecibioMensaje { get; set; }

    }
}
