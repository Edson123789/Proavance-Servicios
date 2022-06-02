using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class AsignacionOportunidadVentaCruzadaDTO
    {
        public int Id { get; set; }
        public int IdOportunidad { get; set; }
        public int IdPersonal { get; set; }
        public int IdCentroCosto { get; set; }
        public int IdAlumno { get; set; }
        public int IdTipoDato { get; set; }
        public DateTime FechaAsignacion { get; set; }
        public int IdFaseOportunidad { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
    }
}
