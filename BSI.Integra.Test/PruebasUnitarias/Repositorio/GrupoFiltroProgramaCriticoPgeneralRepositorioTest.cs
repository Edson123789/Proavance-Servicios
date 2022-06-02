using BSI.Integra.Aplicacion.Marketing.Repositorio;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Test.Fixtures;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Test.PruebasUnitarias.Repositorio
{
    [TestFixture]
    public class GrupoFiltroProgramaCriticoPgeneralRepositorioTest
    {
        private readonly TestContextServicios injection;
        private readonly integraDBContext _integraDBContext;
        private GrupoFiltroProgramaCriticoPgeneralRepositorio _repGrupoFiltroProgramaCriticoPgeneral;

        public GrupoFiltroProgramaCriticoPgeneralRepositorioTest()
        {
            _integraDBContext = new integraDBContext();
            _repGrupoFiltroProgramaCriticoPgeneral = new GrupoFiltroProgramaCriticoPgeneralRepositorio(_integraDBContext);
            injection = new TestContextServicios();
        }

        [Test]
        public void ObtenerPorIdGrupoNoExiste()
        {
            // Arrange
            int idGrupo = 0;

            // Act
            var listaPGeneralFiltrado = _repGrupoFiltroProgramaCriticoPgeneral.ObtenerPorIdGrupo(idGrupo);

            // Assert
            Assert.IsNotNull(listaPGeneralFiltrado);
            Assert.AreEqual(listaPGeneralFiltrado.Count, 0);
        }

        [Test]
        public void ObtenerPorIdGrupoExiste()
        {
            // Arrange
            int idGrupo = 1;

            //Act
            var listaPGeneralFiltrado = _repGrupoFiltroProgramaCriticoPgeneral.ObtenerPorIdGrupo(idGrupo);

            // Assert
            Assert.IsNotNull(listaPGeneralFiltrado);
            Assert.AreNotEqual(listaPGeneralFiltrado.Count, 0);
        }
    }
}
