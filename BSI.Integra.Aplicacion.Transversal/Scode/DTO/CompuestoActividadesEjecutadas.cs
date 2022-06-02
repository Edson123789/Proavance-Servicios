using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.DTO
{
    public class CompuestoActividadesEjecutadas
    {
        public int totalOportunidades { get; set; }
        public int Id { get; set; }
        public string centroCosto { get; set; }
        public string contacto { get; set; }
        public string codigoFase { get; set; }
        public string nombreTipoDato { get; set; }
        public string origen { get; set; }
        public DateTime? fechaProgramada { get; set; }
        public DateTime? fechaReal { get; set; }
        public Nullable<int> duracion { get; set; }
        public string actividad { get; set; }
        public string ocurrencia { get; set; }
        public string comentario { get; set; }
        public string asesor { get; set; }
        public int idContacto { get; set; }
        public int idOportunidad { get; set; }

        public string probActual { get; set; }
        public string ca_nombre { get; set; }
        public int IdCategoria { get; set; }

        public string tiempoLlamadas { get; set; }
        public string faseMaxima { get; set; }
        public string faseInicial { get; set; }
        public int numeroLlamadas { get; set; }
        public string duracionTimbrado { get; set; }
        public string duracionContesto { get; set; }
        public string estadoLlamada { get; set; }

        public string unicoTimbrado { get; set; }
        public string unicoContesto { get; set; }
        public string unicoEstadoLlamada { get; set; }
        public string estado { get; set; }
        public string estadoClasificacion { get; set; }
        public string unicoClasificacion { get; set; }
        public DateTime? unicoFechaLlamada { get; set; }
        //public List<CompuestoActividadesEjecutadas_Detalle> lista { get; set; }
        public double minutosIntervale { get; set; }
        public double minutosTotalTimbrado { get; set; }
        public double minutosTotalContesto { get; set; }
        public double minutosTotalPerdido { get; set; }
        public double mayorTiempo { get; set; }
        public string tiemposTresCX { get; set; }

    }

}
