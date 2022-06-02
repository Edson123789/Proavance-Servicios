using BSI.Integra.Aplicacion.Base.BO;
using System;

namespace BSI.Integra.Aplicacion.Marketing.BO
{
    public partial class CampaniaMailingValorTipoBO :  BaseBO
    {
        public int IdCampaniaMailing { get; set; }
        public int Valor { get; set; }
        public int IdCategoriaObjetoFiltro { get; set; }
        public int? IdMigracion { get; set; }
    }
}
