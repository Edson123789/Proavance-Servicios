using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TModeloGeneral
    {
        public TModeloGeneral()
        {
            TModeloGeneralAformacion = new HashSet<TModeloGeneralAformacion>();
            TModeloGeneralAtrabajo = new HashSet<TModeloGeneralAtrabajo>();
            TModeloGeneralCargo = new HashSet<TModeloGeneralCargo>();
            TModeloGeneralCategoriaDato = new HashSet<TModeloGeneralCategoriaDato>();
            TModeloGeneralEscala = new HashSet<TModeloGeneralEscala>();
            TModeloGeneralIndustria = new HashSet<TModeloGeneralIndustria>();
            TModeloGeneralPgeneral = new HashSet<TModeloGeneralPgeneral>();
            TModeloGeneralTipoDato = new HashSet<TModeloGeneralTipoDato>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public decimal PeIntercepto { get; set; }
        public int PeEstado { get; set; }
        public int PeVersion { get; set; }
        public int? IdPadre { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }

        public virtual ICollection<TModeloGeneralAformacion> TModeloGeneralAformacion { get; set; }
        public virtual ICollection<TModeloGeneralAtrabajo> TModeloGeneralAtrabajo { get; set; }
        public virtual ICollection<TModeloGeneralCargo> TModeloGeneralCargo { get; set; }
        public virtual ICollection<TModeloGeneralCategoriaDato> TModeloGeneralCategoriaDato { get; set; }
        public virtual ICollection<TModeloGeneralEscala> TModeloGeneralEscala { get; set; }
        public virtual ICollection<TModeloGeneralIndustria> TModeloGeneralIndustria { get; set; }
        public virtual ICollection<TModeloGeneralPgeneral> TModeloGeneralPgeneral { get; set; }
        public virtual ICollection<TModeloGeneralTipoDato> TModeloGeneralTipoDato { get; set; }
    }
}
