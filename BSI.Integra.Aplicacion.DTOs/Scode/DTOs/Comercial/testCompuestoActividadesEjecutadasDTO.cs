using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class testCompuestoActividadesEjecutadasDTO
    {
        public int totalOportunidades { get; set; }
        public Guid Id { get; set; }
        public string centroCosto { get; set; }
        public string contacto { get; set; }
        public string codigoFase { get; set; }
        public string nombreTipoDato { get; set; }
        public string origen { get; set; }
        public string fechaProgramada { get; set; }
        public string fechaReal { get; set; }
        public Nullable<int> duracion { get; set; }
        public string actividad { get; set; }
        public string ocurrencia { get; set; }
        public string comentario { get; set; }
        public string asesor { get; set; }
        public int idContacto { get; set; }
        public Guid idOportunidad { get; set; }

        public string probActual { get; set; }
        public string ca_nombre { get; set; }
        public Guid IdCategoria { get; set; }

        public string tiempoLlamadas { get; set; }
        public string faseMaxima { get; set; }
        public string faseInicial { get; set; }
        public string numeroLlamadas { get; set; }

        public string duracionTimbrado { get; set; }
        public string duracionContesto { get; set; }
        public string estadoLlamada { get; set; }
        public string unicoTimbrado { get; set; }
        public string unicoContesto { get; set; }
        public string unicoEstadoLlamada { get; set; }
        public DateTime? FechaLlamada { get; set; }
        public string estado { get; set; }
        public string estadoClasificacion { get; set; }
        public string unicoClasificacion { get; set; }
        public DateTime? unicoFechaLlamada { get; set; }
        public int minutosIntervale { get; set; }
        public int minutosTotalTimbrado { get; set; }
        public int minutosTotalContesto { get; set; }
        public int minutosTotalPerdido { get; set; }
        public int mayorTiempo { get; set; }
        public string tiemposTresCX { get; set; }
    }
}
