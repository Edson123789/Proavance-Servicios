using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ActividadRealizadaDTO
    {
        public int Id { get; set; }
        public string CentroCosto { get; set; }
        public string Contacto { get; set; }
        public string CodigoFase { get; set; }
        public string NombreTipoDato { get; set; }
        public string Origen { get; set; }
        public DateTime? FechaProgramada { get; set; }
        public DateTime? FechaReal { get; set; }
        public int? DuracionReal { get; set; }
        public string UnicoTimbrado { get; set; }
        public string UnicoContesto { get; set; }
        public string UnicoEstadoLlamada { get; set; }
        public DateTime? UnicoFechaLlamada { get; set; }
        public string Clasificacion { get; set; }
        public string Actividad { get; set; }
        public string Ocurrencia { get; set; }
        public string Comentario { get; set; }
        public string Asesor { get; set; }
        public int IdAlumno { get; set; }
        public int IdOportunidad { get; set; }
        public string probabilidadActualDesc { get; set; }
        public string NombreCategoriaOrigen { get; set; }
        public int IdCategoriaOrigen { get; set; }
        public string FaseInicial { get; set; }
        public string FaseMaxima { get; set; }
        public int NumeroLlamadas { get; set; }
        public string Estado { get; set; }
        public string DuracionTimbrado { get; set; }
        public string DuracionContesto { get; set; }
        public string EstadoLlamada { get; set; }
        public DateTime? FechaLlamada { get; set; }
        public string EstadoClasificacion { get; set; }
    }
}
