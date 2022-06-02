using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ReporteSeguimientoOportunidadesFiltrosDTO
    {
        public List<int> CentroCostos { get; set; }
        public List<int> Asesores { get; set; }
        public List<int> FasesOportunidad { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public int OpcionFase { get; set; }
        public List<int> FaseOportunidadOrigen { get; set; }
        public List<int> FaseOportunidadDestino { get; set; }
        public string DocumentoIdentidad { get; set; }
        public string CodigoMatricula { get; set; }
        public List<int> EstadosMatricula { get; set; }
        //Carlos Crispin //Reporte Seguimiento por Fecha Creacion-Registro
        public int? TipoFecha { get; set; }
        public int? ControlFiltroFecha { get; set; }

    }
    public class ReporteSolicitudesVisualizacionFiltrosDTO
    {
        public List<int> CentroCostos { get; set; }
        public List<int> Asesores { get; set; }
        public List<int> FasesOportunidad { get; set; }

    }
    public class AprobacionSolicitudesVisualizacionFiltrosDTO
    {
        public int IdOportunidad { get; set; }
        public int IdPersonalSolicitante { get; set; }
        public int IdSolicitudVisualizacion { get; set; }
        public string Usuario { get; set; }

    }
}
