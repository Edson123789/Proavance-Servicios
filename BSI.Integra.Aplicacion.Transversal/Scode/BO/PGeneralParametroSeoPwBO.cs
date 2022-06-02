using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class PgeneralParametroSeoPwBO : BaseBO
    {
        public string Descripcion { get; set; }
        public int IdPgeneral { get; set; }
        public int IdParametroSeo { get; set; }
        public Guid? IdMigracion { get; set; }

        public PgeneralParametroSeoPwBO(int id)
        {
         

        }
        public PgeneralParametroSeoPwBO()
        {

        }
    }
}
