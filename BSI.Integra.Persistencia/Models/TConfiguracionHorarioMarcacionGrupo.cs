using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TConfiguracionHorarioMarcacionGrupo
    {
        public int Id { get; set; }
        public int IdHorarioGrupoPersonal { get; set; }
        public int IdConfiguracionHorarioMarcacion { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }

        public virtual TConfiguracionHorarioMarcacion IdConfiguracionHorarioMarcacionNavigation { get; set; }
        public virtual THorarioGrupoPersonal IdHorarioGrupoPersonalNavigation { get; set; }
    }
}
