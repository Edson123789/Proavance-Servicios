using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TDatoAdicionalPagina
    {
        public int Id { get; set; }
        public int IdFormularioLandingPage { get; set; }
        public int IdTitulo { get; set; }
        public string NombreTitulo { get; set; }
        public string Descripcion { get; set; }
        public string NombreImagen { get; set; }
        public string ColorTitulo { get; set; }
        public string ColorDescripcion { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }

        public virtual TFormularioLandingPage IdFormularioLandingPageNavigation { get; set; }
    }
}
