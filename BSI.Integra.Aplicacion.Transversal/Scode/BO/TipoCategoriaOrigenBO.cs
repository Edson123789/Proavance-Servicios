using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public partial class TipoCategoriaOrigenBO : BaseBO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int Meta { get; set; }
        public int? Orden { get; set; }
        public int OportunidadMaxima { get; set; }
        public Guid? IdMigracion { get; set; }
    }
}
