using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.GestionPersonas.SCode.BO
{
    public class DatoContratoComisionBonoBO : BaseBO
    {
        public int Id { get; set; }
        public int IdDatoContratoPersonal { get; set; }
        public decimal Monto { get; set; }
        public string Concepto { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
        public string TipoRemuneracionVariable { get; set; }
    }
}
