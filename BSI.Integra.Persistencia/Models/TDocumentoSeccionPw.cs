using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TDocumentoSeccionPw
    {
        public TDocumentoSeccionPw()
        {
            TConfigurarVideoPrograma = new HashSet<TConfigurarVideoPrograma>();
        }

        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Contenido { get; set; }
        public int IdPlantillaPw { get; set; }
        public int Posicion { get; set; }
        public int Tipo { get; set; }
        public int IdDocumentoPw { get; set; }
        public int IdSeccionPw { get; set; }
        public bool VisibleWeb { get; set; }
        public int? ZonaWeb { get; set; }
        public int? OrdenWeb { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }
        public int? IdSeccionTipoDetallePw { get; set; }
        public int? NumeroFila { get; set; }
        public string Cabecera { get; set; }
        public string PiePagina { get; set; }

        public virtual TDocumentoPw IdDocumentoPwNavigation { get; set; }
        public virtual TPlantillaPw IdPlantillaPwNavigation { get; set; }
        public virtual TSeccionPw IdSeccionPwNavigation { get; set; }
        public virtual ICollection<TConfigurarVideoPrograma> TConfigurarVideoPrograma { get; set; }
    }
}
