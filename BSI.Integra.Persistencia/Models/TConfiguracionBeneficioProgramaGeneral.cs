using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TConfiguracionBeneficioProgramaGeneral
    {
        public TConfiguracionBeneficioProgramaGeneral()
        {
            TConfiguracionBeneficioProgramaGeneralDatoAdicional = new HashSet<TConfiguracionBeneficioProgramaGeneralDatoAdicional>();
            TConfiguracionBeneficioProgramaGeneralVersion = new HashSet<TConfiguracionBeneficioProgramaGeneralVersion>();
        }

        public int Id { get; set; }
        public int IdPgeneral { get; set; }
        public int IdBeneficio { get; set; }
        public int Tipo { get; set; }
        public bool? Asosiar { get; set; }
        public int Entrega { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
        public int? AvanceAcademico { get; set; }
        public bool? DeudaPendiente { get; set; }
        public int? OrdenBeneficio { get; set; }
        public bool? DatosAdicionales { get; set; }
        public string Requisitos { get; set; }
        public string ProcesoSolicitud { get; set; }
        public string DetallesAdicionales { get; set; }

        public virtual TPgeneral IdPgeneralNavigation { get; set; }
        public virtual ICollection<TConfiguracionBeneficioProgramaGeneralDatoAdicional> TConfiguracionBeneficioProgramaGeneralDatoAdicional { get; set; }
        public virtual ICollection<TConfiguracionBeneficioProgramaGeneralVersion> TConfiguracionBeneficioProgramaGeneralVersion { get; set; }
    }
}
