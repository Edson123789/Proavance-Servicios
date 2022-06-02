using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TPlantilla
    {
        public TPlantilla()
        {
            TFaseByPlantilla = new HashSet<TFaseByPlantilla>();
            TPlantillaAsociacionModuloSistema = new HashSet<TPlantillaAsociacionModuloSistema>();
            TPlantillaClaveValor = new HashSet<TPlantillaClaveValor>();
            TSmsConfiguracionEnvio = new HashSet<TSmsConfiguracionEnvio>();
            TWhatsAppConfiguracionEnvio = new HashSet<TWhatsAppConfiguracionEnvio>();
            TWhatsAppConfiguracionPreEnvio = new HashSet<TWhatsAppConfiguracionPreEnvio>();
            TWhatsAppPlantillaPorOcurrenciaActividad = new HashSet<TWhatsAppPlantillaPorOcurrenciaActividad>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int IdPlantillaBase { get; set; }
        public bool EstadoAgenda { get; set; }
        public int Documento { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }
        public int? IdPersonalAreaTrabajo { get; set; }

        public virtual ICollection<TFaseByPlantilla> TFaseByPlantilla { get; set; }
        public virtual ICollection<TPlantillaAsociacionModuloSistema> TPlantillaAsociacionModuloSistema { get; set; }
        public virtual ICollection<TPlantillaClaveValor> TPlantillaClaveValor { get; set; }
        public virtual ICollection<TSmsConfiguracionEnvio> TSmsConfiguracionEnvio { get; set; }
        public virtual ICollection<TWhatsAppConfiguracionEnvio> TWhatsAppConfiguracionEnvio { get; set; }
        public virtual ICollection<TWhatsAppConfiguracionPreEnvio> TWhatsAppConfiguracionPreEnvio { get; set; }
        public virtual ICollection<TWhatsAppPlantillaPorOcurrenciaActividad> TWhatsAppPlantillaPorOcurrenciaActividad { get; set; }
    }
}
