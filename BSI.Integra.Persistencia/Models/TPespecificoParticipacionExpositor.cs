using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TPespecificoParticipacionExpositor
    {
        public int Id { get; set; }
        public int IdPespecifico { get; set; }
        public int? Orden { get; set; }
        public int? Grupo { get; set; }
        public int? IdExpositorCurso { get; set; }
        public string ExpositorCurso { get; set; }
        public int? IdExpositorGrupo { get; set; }
        public string ExpositorGrupo { get; set; }
        public int? IdExpositorV3 { get; set; }
        public string ExpositorV3 { get; set; }
        public int? IdExpositorGrupoConfirmado { get; set; }
        public int? IdProveedorFurHonorario { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
        public int? IdProveedorPlanificacionGrupo { get; set; }
        public int? IdProveedorOperacionesGrupoConfirmado { get; set; }
        public bool? EsSilaboAprobado { get; set; }
    }
}
