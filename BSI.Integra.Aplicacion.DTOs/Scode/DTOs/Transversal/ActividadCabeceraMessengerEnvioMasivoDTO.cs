using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ActividadCabeceraMessengerEnvioMasivoDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int IdActividadBase { get; set; }
        public int IdConjuntoLista { get; set; }
        public int IdPersonalAreaTrabajo { get; set; }
        public DateTime FechaInicioActividad { get; set; }
        public DateTime? FechaFinActividad { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan? HoraFin { get; set; }
        public List<MessengerEnvioMasivoDTO> listaMessengerEnvioMasivo { get; set; }
        public int? IdFacebookCuentaPublicitaria { get; set; }
        public string Usuario { get; set; }
    }
}
