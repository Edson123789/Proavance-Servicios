using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class DatosAlumnoMatriculaDTO
    {
        public int Id { get; set; }
        public string CodigoMatricula { get; set; }
        public int IdPespecifico { get; set; }
        public string NombreAlumno { get; set; }
        public string Genero { get; set; }
        public int IdCentroCosto { get; set; }
        public string NombreCentroCosto { get; set; }
        public int IdEstadoMatricula { get; set; }
        public string NombreEstadoMatricula { get; set; }
        public bool Modulo { get; set; }
        public bool Solicitado { get; set; }
        public bool AplicaPartner { get; set; }
        public string MensajeCondicion { get; set; }
        public string EscalaCalificacion { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
    }
}
