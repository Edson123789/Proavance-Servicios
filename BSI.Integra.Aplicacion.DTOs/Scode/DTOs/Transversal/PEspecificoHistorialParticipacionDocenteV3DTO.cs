using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class PEspecificoHistorialParticipacionDocenteV3DTO
    {
        public int Id { get; set; }
        public int Anho { get; set; }
        public int IdPEspecificoPadre { get; set; }
        public string PEspecificoPadre { get; set; }
        public string EstadoPrograma { get; set; }
        public int IdPEspecifico { get; set; }
        public string PEspecifico { get; set; }
        public string EstadoCurso { get; set; }
        public string Modalidad { get; set; }
        public string ModalidadPrograma { get; set; }
        public int IdCentroCostoPrograma { get; set; }
        public string CentroCostoPrograma { get; set; }
        public string Ciudad { get; set; }
        public int? Orden { get; set; }
        public int? Grupo { get; set; }
        public string EstadoParticipacion { get; set; }
        
        public string ExpositorPlanificacion{ get; set; }
        public string ExpositorV3 { get; set; }
        public string ExpositorConfirmado { get; set; }
        public int? IdExpositorConfirmado { get; set; }
        public int? IdProveedorPlanificacionGrupo { get; set; }
        public int? IdProveedorOperacionesGrupoConfirmado { get; set; }

        public int? IdProveedorFur { get; set; }

        public string ProveedorFur { get; set; }

        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaTermino { get; set; }

        public bool? EsNotaAprobada { get; set; }
        public bool? EsAsistenciaAprobada { get; set; }
        public bool AplicaCierreAsistencia { get; set; }
    }

    public class PEspecificoHistorialParticipacionDocentePortalDTO
    {
        public int Anho { get; set; }
        public int Id { get; set; }
        public int IdPEspecificoPadre { get; set; }
        public string PEspecificoPadre { get; set; }
        public int IdEstadoPEspecificoPadre { get; set; }
        public string EstadoPrograma { get; set; }
        public int IdModalidadPrograma { get; set; }
        public string ModalidadPrograma { get; set; }
        public int IdPEspecifico { get; set; }
        public string PEspecifico { get; set; }
        public int IdEstadoCur { get; set; }
        public int IdAreaCapacitacion { get; set; }
        public int IdSubAreaCapacitacion { get; set; }
        public int IdProveedor { get; set; }
    }
}
