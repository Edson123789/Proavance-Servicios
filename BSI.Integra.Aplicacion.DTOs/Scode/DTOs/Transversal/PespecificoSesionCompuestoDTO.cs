using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class PespecificoSesionCompuestoDTO
    {
        public int Id { get; set; }
        public int? IdPespecifico { get; set; }
        public int? PEspecificoHijoId { get; set; }
        public DateTime FechaHoraInicio { get; set; }
        public decimal? Duracion { get; set; }
        public decimal? DuracionTotal { get; set; }
        public int? IdExpositor { get; set; }
        public int? IdProveedor { get; set; }
        public int? IdCiudad { get; set; }
        public string Comentario { get; set; }
        public string Curso { get; set; }
        public string Tipo { get; set; }
        public string ModalidadSesion { get; set; }
        public bool? SesionAutoGenerada { get; set; }
        public int? IdAmbiente { get; set; }
        public bool? Predeterminado { get; set; }
        public bool? EsSesionInicial { get; set; }
        public bool? Cruce { get; set; }
        public bool? MostrarPDF { get; set; }
    }
}
