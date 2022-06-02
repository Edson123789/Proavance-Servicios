using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class SusAreaCapacitacionFacebookDTO
    {
        public int Id { get; set; }
        public string AliasFacebook { get; set; }
    }
    public class SubAreaCapacitacionAreaCapacitacionFiltroDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int? IdAreaCapacitacion { get; set; }
    }
    public class PGeneralSubAreaCapacitacionFiltroDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int? IdSubAreaCapacitacion { get; set; }
    }
    public class PGeneralCabeceraSpeechDTO
    {
        public string ProgramaGeneral { get; set; }
        public string Texto { get; set; }
        public string Color { get; set; }
    }
    public class PGeneralPublicoObjetivoDTO
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Contenido { get; set; }
        public string IdPGeneral { get; set; }
        public int Respuesta { get; set; }
    }
    public class PGeneralRequisitoCertificacionDTO
    {
        public int IdCertificacion { get; set; }
        public string NombreCertificacion { get; set; }
        public int Respuesta { get; set; }
        public string Completado { get; set; }
        public List<PGeneralArgumentoCertificacionDTO> Requisitos { get; set; }
    }
    public class PGeneralArgumentoCertificacionDTO
    {
        public int Id { get; set; }
        public int IdProgramaGeneralCertificacion { get; set; }
        public string Nombre { get; set; }
        public int? Respuesta { get; set; }
        public string Completado { get; set; }

    }
    public class PGeneralFactorBeneficioDTO
    {
        public int IdMotivacion { get; set; }
        public string NombreMotivacion { get; set; }
        public int Respuesta { get; set; }
        public string Completado { get; set; }
        public List<PGeneralArgumentoMotivacionDTO> Argumentos { get; set; }
    }
    public class PGeneralArgumentoMotivacionDTO
    {
        public int Id { get; set; }
        public int IdProgramaGeneralMotivacion { get; set; }
        public string Nombre { get; set; }

    }
    public class PGeneralProblemaDTO
    {
        public int IdProblema { get; set; }
        public string NombreProblema { get; set; }
        public int Respuesta { get; set; }
        public string Completado { get; set; }
        public List<PGeneralArgumentoProblemaDTO> Argumentos { get; set; }
    }
    public class PGeneralArgumentoProblemaDTO
    {
        public int Id { get; set; }
        public int IdProgramaGeneralProblema { get; set; }
        public string Detalle { get; set; }
        public string Solucion { get; set; }
        public bool Solucionado { get; set; }
        public bool Seleccionado { get; set; }

    }
}
