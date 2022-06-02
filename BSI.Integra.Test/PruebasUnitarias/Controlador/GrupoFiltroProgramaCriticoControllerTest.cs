using BSI.Integra.Persistencia.Models;
using BSI.Integra.Servicios.Controllers;
using BSI.Integra.Test.Fixtures;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;

namespace BSI.Integra.Test.PruebasUnitarias.Controlador
{
    [TestFixture]
    public class GrupoFiltroProgramaCriticoControllerTest
    {
        private readonly integraDBContext _integraDBContext;
        private readonly TestContextServicios injection;

        public GrupoFiltroProgramaCriticoControllerTest()
        {
            _integraDBContext = new integraDBContext();
            injection = new TestContextServicios();
        }

        [Test]
        public void ObtenerPGeneralAsociadoIdCero()
        {
            // Arrange
            GrupoFiltroProgramaCriticoController grupoFiltroProgramaCriticoController = new GrupoFiltroProgramaCriticoController(_integraDBContext);
            int idGrupo = 0;

            // Act
            ActionResult resultadoLista = grupoFiltroProgramaCriticoController.ObtenerPGeneralAsociado(idGrupo);

            //Assert
            Assert.NotNull(resultadoLista);
            ObjectResult resultado = resultadoLista as ObjectResult;
            Assert.NotNull(resultado);
            Assert.AreEqual(200, resultado.StatusCode);
        }

        [Test]
        public void ObtenerPGeneralAsociadoIdCorrecto()
        {
            // Arrange
            GrupoFiltroProgramaCriticoController grupoFiltroProgramaCriticoController = new GrupoFiltroProgramaCriticoController(_integraDBContext);
            int idGrupo = 1;

            // Act
            ActionResult resultadoLista = grupoFiltroProgramaCriticoController.ObtenerPGeneralAsociado(idGrupo);

            //Assert
            Assert.NotNull(resultadoLista);
            OkObjectResult resultado = resultadoLista as OkObjectResult;
            Assert.NotNull(resultado);
            Assert.AreEqual(200, resultado.StatusCode);
        }
    }
}
