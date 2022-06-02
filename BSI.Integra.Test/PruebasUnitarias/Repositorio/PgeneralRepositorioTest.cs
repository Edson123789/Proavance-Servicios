using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Test.Fixtures;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Test.PruebasUnitarias.Repositorio
{
    [TestFixture]
    public class PgeneralRepositorioTest
    {
        private readonly TestContextServicios injection;
        private readonly integraDBContext _integraDBContext;
        private PgeneralRepositorio _repPGeneral;

        public PgeneralRepositorioTest()
        {
            _integraDBContext = new integraDBContext();
            _repPGeneral = new PgeneralRepositorio(_integraDBContext);
            injection = new TestContextServicios();
        }
    }
}
