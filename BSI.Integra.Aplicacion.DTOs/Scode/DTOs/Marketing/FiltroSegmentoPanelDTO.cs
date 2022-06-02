using System;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class FiltroSegmentoPanelDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int? IdFiltroSegmentoTipoContacto { get; set; }
        public string NombreFiltroSegmentoTipoContacto { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public bool FiltroEjecutado { get; set; }
    }
}
