using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TAgendaTabConfiguracion
    {
        public int Id { get; set; }
        public int IdAgendaTab { get; set; }
        public string IdTipoCategoriaOrigen { get; set; }
        public string IdCategoriaOrigen { get; set; }
        public string IdTipoDato { get; set; }
        public string IdFaseOportunidad { get; set; }
        public string IdEstadoOportunidad { get; set; }
        public string Probabilidad { get; set; }
        public string VistaBaseDatos { get; set; }
        public string CamposVista { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
    }
}
