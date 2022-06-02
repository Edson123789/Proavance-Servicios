using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class ProgramaGeneralPerfilCategoriaCoeficienteBO : BaseBO
    {
        public int IdPgeneral { get; set; }
        public string Nombre { get; set; }
        public double Coeficiente { get; set; }
        public int IdSelect { get; set; }
        public int Columna { get; set; }
        public Guid? IdMigracion { get; set; }
    }
}
