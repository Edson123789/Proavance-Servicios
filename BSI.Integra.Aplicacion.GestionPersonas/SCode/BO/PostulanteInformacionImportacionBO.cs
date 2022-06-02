using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.GestionPersonas.SCode.BO
{
    public class PostulanteInformacionImportacionBO : BaseBO
    {
        public int Id { get; set; }
        public int IdProcesoSeleccion { get; set; }
        public int? CantidadTotal { get; set; }
        public int? CantidadPrimerCriterio { get; set; }
        public int? CantidadSegundoCriterio { get; set; }
        public int? CantidadFinal { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
    }
}
