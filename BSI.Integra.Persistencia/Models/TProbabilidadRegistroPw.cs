using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TProbabilidadRegistroPw
    {
        public TProbabilidadRegistroPw()
        {
            TCampaniaGeneral = new HashSet<TCampaniaGeneral>();
            TProbabilidadRegistroPwVentaCruzadaProbabilidad = new HashSet<TProbabilidadRegistroPwVentaCruzadaProbabilidad>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public Guid IdCodigo { get; set; }
        public int Codigo { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }

        public virtual ICollection<TCampaniaGeneral> TCampaniaGeneral { get; set; }
        public virtual ICollection<TProbabilidadRegistroPwVentaCruzadaProbabilidad> TProbabilidadRegistroPwVentaCruzadaProbabilidad { get; set; }
    }
}
