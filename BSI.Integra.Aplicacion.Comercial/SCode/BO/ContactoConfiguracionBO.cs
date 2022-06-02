using System;
using BSI.Integra.Aplicacion.Base.BO;

namespace BSI.Integra.Aplicacion.Comercial.BO
{
    public class ContactoConfiguracionBO : BaseBO
    {
        public int Id { get; set; }
        public int IdOrigen { get; set; }
        public int IdTipoDato { get; set; }
        public int IdFaseOportunidad { get; set; }
        public int IdTipoDatoRelacionado { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }
        public ContactoConfiguracionBO() {
        }
    }
}
