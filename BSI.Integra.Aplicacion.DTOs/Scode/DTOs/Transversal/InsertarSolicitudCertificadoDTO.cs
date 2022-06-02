using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class InsertarSolicitudCertificadoDTO
    {
        public int Id { get; set; }
        public int IdCertificadoTipo { get; set; }
        public int IdMatriculaCabecera { get; set; }
        public string CodigoMatricula { get; set; }
        public int IdPespecifico { get; set; }
        public string NombreAlumno { get; set; }
        public string Genero { get; set; }
        public int IdCentroCosto { get; set; }
        public bool Solicitado { get; set; }
        public bool SolicitadoPartner { get; set; }
        public string DireccionEnvio { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public decimal Nota { get; set; }
        public decimal EscalaCalificacion { get; set; }
        public string NombreUsuario { get; set; }
        public string MensajeCondicion { get; set; }
    }
}
