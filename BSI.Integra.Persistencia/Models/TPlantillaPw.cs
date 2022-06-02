using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TPlantillaPw
    {
        public TPlantillaPw()
        {
            TConfiguracionInvitacion = new HashSet<TConfiguracionInvitacion>();
            TDocumentoPw = new HashSet<TDocumentoPw>();
            TDocumentoSeccionPw = new HashSet<TDocumentoSeccionPw>();
            TPlantillaPais = new HashSet<TPlantillaPais>();
            TPlantillaPlantillaMaestroPw = new HashSet<TPlantillaPlantillaMaestroPw>();
            TPlantillaRevisionPw = new HashSet<TPlantillaRevisionPw>();
            TSeccionPw = new HashSet<TSeccionPw>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int IdPlantillaMaestroPw { get; set; }
        public int IdRevisionPw { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }

        public virtual TPlantillaMaestroPw IdPlantillaMaestroPwNavigation { get; set; }
        public virtual TRevisionPw IdRevisionPwNavigation { get; set; }
        public virtual ICollection<TConfiguracionInvitacion> TConfiguracionInvitacion { get; set; }
        public virtual ICollection<TDocumentoPw> TDocumentoPw { get; set; }
        public virtual ICollection<TDocumentoSeccionPw> TDocumentoSeccionPw { get; set; }
        public virtual ICollection<TPlantillaPais> TPlantillaPais { get; set; }
        public virtual ICollection<TPlantillaPlantillaMaestroPw> TPlantillaPlantillaMaestroPw { get; set; }
        public virtual ICollection<TPlantillaRevisionPw> TPlantillaRevisionPw { get; set; }
        public virtual ICollection<TSeccionPw> TSeccionPw { get; set; }
    }
}
