using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class PEspecificoSesionDTO
    {
        public int Id { get; set; }
        public int IdPEspecifico { get; set; }
        public Nullable<DateTime> FechaHoraInicio { get; set; }
        public decimal Duracion { get; set; }
        public string Comentario { get; set; }
        public Nullable<bool> SesionAutoGenerada { get; set; }
        public Nullable<bool> Predeterminado { get; set; }
        public bool Estado { get; set; }
    }

    public class PEspecificoSesionDetalleDTO
    {
        public int Id { get; set; }
        public int IdPEspecifico { get; set; }
        public DateTime? FechaHoraInicio { get; set; }
        public DateTime? FechaHoraFin { get; set; }
        public string NombrePEspecifico { get; set; }
        public string NombreCurso { get; set; }
    }
    public class EspecificoFechasInicioDTO
    {
        public int IdPEspecifico { get; set; }
        public DateTime? FechaHoraInicio { get; set; }
    }
}
