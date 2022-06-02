using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TOportunidadMaximaPorCategoria
    {
        public int Id { get; set; }
        public int IdPersonal { get; set; }
        public int IdTipoCategoriaOrigen { get; set; }
        public int IdPais { get; set; }
        public int OportunidadesMaximas { get; set; }
        public int OportunidadesSinGenerarIs { get; set; }
        public int Meta { get; set; }
        public string Grupo { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }
    }
}
