using System;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class LogFiltroSegmentoEjecutadoDTO
    {
        public int Id { get; set; }
        //public int IdCentroCosto { get; set; }
        //public int IdOrigen { get; set; }
        //public int IdTipoDato { get; set; }
        //public int IdFaseOportunidad { get; set; }
        public string NombreCentroCosto { get; set; }
        public string NombreOrigen { get; set; }
        public string NombreTipoDato { get; set; }
        public string NombreFaseOportunidad { get; set; }
        public int TotalOportunidadesCreadas { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }

    }
}
