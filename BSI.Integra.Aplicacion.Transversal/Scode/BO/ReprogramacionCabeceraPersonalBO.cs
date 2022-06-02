using System;
using BSI.Integra.Aplicacion.Base.BO;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
     public class ReprogramacionCabeceraPersonalBO : BaseBO
    {
        public int Id { get; set; }
        public int IdActividadCabecera { get; set; }
        public int? IdCategoriaOrigen { get; set; }
        public int ReproDia { get; set; }
        public DateTime FechaReprogramacion { get; set; }
        public int? IdPersonal { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }
        public ReprogramacionCabeceraPersonalBO()
        {
        }
    }
}
