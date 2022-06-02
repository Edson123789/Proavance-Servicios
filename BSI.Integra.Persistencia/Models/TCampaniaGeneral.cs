using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TCampaniaGeneral
    {
        public TCampaniaGeneral()
        {
            TCampaniaGeneralDetalle = new HashSet<TCampaniaGeneralDetalle>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public int? IdCategoriaOrigen { get; set; }
        public DateTime? FechaEnvio { get; set; }
        public int? IdCategoriaObjetoFiltro { get; set; }
        public int? IdHoraEnvioMailing { get; set; }
        public int? IdTipoAsociacion { get; set; }
        public int? IdProbabilidadRegistroPw { get; set; }
        public int? NroMaximoSegmentos { get; set; }
        public int? CantidadPeriodoSinCorreo { get; set; }
        public int? IdTiempoFrecuencia { get; set; }
        public int? IdFiltroSegmento { get; set; }
        public int? IdPlantillaMailing { get; set; }
        public int? IdRemitenteMailing { get; set; }
        public bool? IncluyeWhatsapp { get; set; }
        public DateTime? FechaInicioEnvioWhatsapp { get; set; }
        public DateTime? FechaFinEnvioWhatsapp { get; set; }
        public int? NumeroMinutosPrimerEnvio { get; set; }
        public int? IdHoraEnvioWhatsapp { get; set; }
        public int? DiasSinWhatsapp { get; set; }
        public int? IdPlantillaWhatsapp { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
        public bool? IncluirRebotes { get; set; }
        public int IdEstadoEnvioMailing { get; set; }
        public int IdEstadoEnvioWhatsapp { get; set; }

        public virtual TCategoriaObjetoFiltro IdCategoriaObjetoFiltroNavigation { get; set; }
        public virtual TCategoriaOrigen IdCategoriaOrigenNavigation { get; set; }
        public virtual TFiltroSegmento IdFiltroSegmentoNavigation { get; set; }
        public virtual TProbabilidadRegistroPw IdProbabilidadRegistroPwNavigation { get; set; }
        public virtual TRemitenteMailing IdRemitenteMailingNavigation { get; set; }
        public virtual TTiempoFrecuencia IdTiempoFrecuenciaNavigation { get; set; }
        public virtual TTipoAsociacion IdTipoAsociacionNavigation { get; set; }
        public virtual ICollection<TCampaniaGeneralDetalle> TCampaniaGeneralDetalle { get; set; }
    }
}
