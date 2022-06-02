using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class ModuloSistemaBO : BaseBO
    {
            public string Nombre { get; set; }
            public string IdModuloSistemaGrupo { get; set; }
            public string Url { get; set; }
            public int? IdMigracion { get; set; }
            public virtual ICollection<PlantillaAsociacionModuloSistemaBO> ListaPlantillaAsociacionModuloSistema { get; set; }

            public ModuloSistemaBO()
            {
                ListaPlantillaAsociacionModuloSistema = new HashSet<PlantillaAsociacionModuloSistemaBO>();
            }
    }
}
