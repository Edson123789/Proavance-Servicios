using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TAgendaTab
    {
        public TAgendaTab()
        {
            TOportunidadClasificacionOperaciones = new HashSet<TOportunidadClasificacionOperaciones>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public bool VisualizarActividad { get; set; }
        public bool CargarInformacionInicial { get; set; }
        public int Numeracion { get; set; }
        public bool ValidarFecha { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
        public string CodigoAreaTrabajo { get; set; }

        public virtual ICollection<TOportunidadClasificacionOperaciones> TOportunidadClasificacionOperaciones { get; set; }
    }
}
