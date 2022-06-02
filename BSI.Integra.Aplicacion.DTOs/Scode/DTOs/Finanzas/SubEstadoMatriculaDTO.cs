using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class SubEstadoMatriculaDTO
    {
        public List<TCRM_SubEstadoMatriculaDTO> SubEstado { get; set; }

    }
    public class TCRM_SubEstadoMatriculaDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public bool Estado { get; set; }
        public int IdEstadoMatricula { get; set; }
        public string UsuarioCreacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaModificacion { get; set; }


        //nuevo valores
        public int? IdOpcionAvanceAcademico { get; set; }
        public int? ValorAvanceAcademico1 { get; set; }
        public int? ValorAvanceAcademico2 { get; set; }
        public string IdEstadoPago { get; set; }
        public int? IdOpcionNotaPromedio { get; set; }
        public int? ValorNotaPromedio1 { get; set; }
        public int? ValorNotaPromedio2 { get; set; }
        public bool? TieneDeuda { get; set; }
        public bool? ProyectoFinal { get; set; }
        public bool? RequiereVerificacionInformacion { get; set; }
        public string EstadoMatricula { get; set; }
        public int IdAgendaTab { get; set; }
    }

    public class SubEstadoMatriculaEnvioAutomatico
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public bool Estado { get; set; }
    }

    public class EstadoMatriculaEnvioAutomatico
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public bool Estado { get; set; }
    }
}
