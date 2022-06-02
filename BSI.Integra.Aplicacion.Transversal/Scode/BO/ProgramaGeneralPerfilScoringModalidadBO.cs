using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class ProgramaGeneralPerfilScoringModalidadBO : BaseBO
    {
        public int IdPgeneral { get; set; }
        public string Nombre { get; set; }
        public int IdModalidadCurso { get; set; }
        public int IdSelect { get; set; }
        public int Valor { get; set; }
        public int Fila { get; set; }
        public int Columna { get; set; }
        public bool Validar { get; set; }
        public Guid? IdMigracion { get; set; }
    }
}
