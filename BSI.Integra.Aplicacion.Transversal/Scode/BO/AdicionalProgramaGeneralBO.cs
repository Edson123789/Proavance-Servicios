using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class AdicionalProgramaGeneralBO : BaseBO
    {
        public int IdPgeneral { get; set; }
        public string Descripcion { get; set; }
        public string NombreImagen { get; set; }
        public int IdTitulo { get; set; }
        public string NombreTitulo { get; set; }
        public Guid? IdMigracion { get; set; }
    }
}
