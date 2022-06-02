using System;
using System.Collections.Generic;
using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.Base.Classes;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class SolucionClienteByActividadBO : BaseBO
    {
        public int Id { get; set; }
        public int? IdOportunidad { get; set; }
        public int? IdActividadDetalle { get; set; }
        public int IdCausa { get; set; }
        public int? IdPersonal { get; set; }
        public bool Solucionado { get; set; }
        public int IdProblemaCliente { get; set; }
        public string OtroProblema { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }

        public SolucionClienteByActividadBO()
        {
            ActualesErrores = new Dictionary<string, List<ErrorInfo>>();
        }
    }
}
