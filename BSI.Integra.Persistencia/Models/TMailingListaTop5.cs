using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TMailingListaTop5
    {
        public int Id { get; set; }
        public int IdCampaniaMailingTop5 { get; set; }
        public int IdMailingDetalleTop5 { get; set; }
        public string AsuntoLista { get; set; }
        public string Contenido { get; set; }
        public string Asunto { get; set; }
        public int IdPersonal { get; set; }
        public string NombreAsesor { get; set; }
        public string CorreoAsesor { get; set; }
        public string Etiquetas { get; set; }
        public string Areas { get; set; }
        public string SubAreas { get; set; }
        public string IdCampaniaMailchimp { get; set; }
        public string IdListaMailchimp { get; set; }
        public bool? Enviado { get; set; }
        public DateTime? FechaEnviado { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }
    }
}
