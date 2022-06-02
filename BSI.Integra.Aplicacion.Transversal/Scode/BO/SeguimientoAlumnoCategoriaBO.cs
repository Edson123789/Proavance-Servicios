using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class SeguimientoAlumnoCategoriaBO : BaseBO
    {
        public string Nombre { get; set; }
        public int? IdMigracion { get; set; }
        public bool AplicaModalidadOnline { get; set; }
        public bool AplicaModalidadAonline { get; set; }
        public bool AplicaModalidadPresencial { get; set; }
        public int IdTipoSeguimientoAlumnoCategoria { get; set; }
    }
}
