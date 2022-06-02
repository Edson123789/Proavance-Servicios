using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Persistencia.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Operaciones.BO
{
    public class MoodleCategoriaBO : BaseBO
    {        
        public int Id { get; set; }
        public int IdMoodleCategoriaTipo { get; set; }
        public int IdCategoriaMoodle { get; set; }
        public string NombreCategoria { get; set; }
        public bool AplicaProyecto { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }

        public virtual TMoodleCategoriaTipo IdMoodleCategoriaTipoNavigation { get; set; }
    }
}
