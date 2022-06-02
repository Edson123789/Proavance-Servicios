using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class SolicitudCambioDTO
    {
        public int IdMatriculaCabecera { get; set; }
        public string CodigoMatricula { get; set; }
        public int Version { get; set; }
        public string NombreSolicitante { get; set; }
        public DateTime Fecha { get; set; }
        public string Observacion { get; set; }
        public string Cambios { get; set; }
        public string IdsCambios { get; set; }
    }
    public class ResultadoFinalDTO
    {
        public int Valor { get; set; }
    }
    public class ResultadoFinalVisualizarOportunidadDTO
    {
        public int Id { get; set; }
        public DateTime? FechaVisibleHasta { get; set; }
        public int Valor { get; set; }

    }
    public class SolicitudVisualizarOportunidadDTO
    {
        public int IdOportunidad { get; set; }
        public int IdPersonal { get; set; }

    }
    public class ResultadoFinaltextoDTO
    {
        public string Valor { get; set; }
    }
    public class ComparacionProcesosSeleccionDTO
    {
        public int IdProcesoSeleccionEtapaDestino { get; set; }
        public string NombreProcesoSeleccionEtapaDestino { get; set; }
        public int IdProcesoSeleccionEtapaOrigen { get; set; }
        public string NombreProcesoSeleccionEtapaOrigen { get; set; }
        public bool EsEtapaAprobada { get; set; }
        public int Convalida { get; set; }
    }
    public class ResultadoFinaltextoV2DTO
    {
        public string Nombre { get; set; }
    }
    public class ResultadoFinalNuloDTO
    {
        public int? Valor { get; set; }
    }
}
