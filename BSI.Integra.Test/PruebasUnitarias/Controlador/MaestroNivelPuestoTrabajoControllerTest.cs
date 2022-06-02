using BSI.Integra.Aplicacion.DTOs.Scode.DTOs.GestionPersonas;
using BSI.Integra.Aplicacion.GestionPersonas.SCode.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Servicios.Controllers;
using BSI.Integra.Test.Fixtures;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Test.PruebasUnitarias.Controlador
{
    [TestFixture]
    public class MaestroNivelPuestoTrabajoControllerTest
    {
        private readonly integraDBContext _integraDBContext;
        private readonly TestContextServicios injection;
        public MaestroNivelPuestoTrabajoControllerTest()
        {
            _integraDBContext = new integraDBContext();
            injection = new TestContextServicios();
        }

        [Test]
        public void ObtenerListaRegistrada()
        {
            //Arrange
            MaestroNivelPuestoTrabajoController maestroNivelPuestoTrabajoController = new MaestroNivelPuestoTrabajoController(_integraDBContext);

            //Act
            ActionResult resultadoLista = maestroNivelPuestoTrabajoController.ObtenerNivelPuestoTrabajoRegistrado();

            //Assert


            Assert.NotNull(resultadoLista);

            OkObjectResult resultado = resultadoLista as OkObjectResult;
            Assert.NotNull(resultado);
            Assert.AreEqual(200, resultado.StatusCode);

        }

        [Test]
        public void InsertarConParametrosCorrectos()
        {
            //Arrange
            MaestroNivelPuestoTrabajoController maestroNivelPuestoTrabajoController = new MaestroNivelPuestoTrabajoController(_integraDBContext);

            PuestoTrabajoNivelDTO nivelPuestoTrabajoAgregar = new PuestoTrabajoNivelDTO()
            {
                Id = 0,                
                Nombre = "Prueba Nombre Uno",
                Usuario = "UsuarioPruebaTest"
            };

            //Act
            ActionResult nivelPuestoTrabajoInsertado = maestroNivelPuestoTrabajoController.InsertarNivelPuestoTrabajo(nivelPuestoTrabajoAgregar);

            //Assert
            Assert.NotNull(nivelPuestoTrabajoInsertado);

            OkObjectResult resultado = nivelPuestoTrabajoInsertado as OkObjectResult;
            Assert.AreEqual(200, resultado.StatusCode);
        }

        [Test]
        public void InsertarConParametrosCorrectosNombreVacio()
        {
            //Arrange
            MaestroNivelPuestoTrabajoController maestroNivelPuestoTrabajoController = new MaestroNivelPuestoTrabajoController(_integraDBContext);

            PuestoTrabajoNivelDTO nivelPuestoTrabajoAgregar = new PuestoTrabajoNivelDTO()
            {
                Id = 5,
                Nombre = "",
                Usuario = "UsuarioPruebaTest"
            };

            //Act
            ActionResult nivelPuestoTrabajoInsertado = maestroNivelPuestoTrabajoController.InsertarNivelPuestoTrabajo(nivelPuestoTrabajoAgregar);

            //Assert
            Assert.NotNull(nivelPuestoTrabajoInsertado);
            OkObjectResult resultado = nivelPuestoTrabajoInsertado as OkObjectResult;
            Assert.NotNull(resultado.Value);
            Assert.AreEqual(200, resultado.StatusCode);
        }
        [Test]
        public void InsertarConParametrosIncorrectos()
        {
            //Arrange
            MaestroNivelPuestoTrabajoController maestroNivelPuestoTrabajoController = new MaestroNivelPuestoTrabajoController(_integraDBContext);

            PuestoTrabajoNivelDTO nivelPuestoTrabajoAgregar = new PuestoTrabajoNivelDTO()
            {
                Id = 5,
                Nombre = "Nombre Prueba"
            };

            //Act
            ActionResult nivelPuestoTrabajoInsertado = maestroNivelPuestoTrabajoController.InsertarNivelPuestoTrabajo(nivelPuestoTrabajoAgregar);

            //Assert
            Assert.NotNull(nivelPuestoTrabajoInsertado);
            ObjectResult resultado = nivelPuestoTrabajoInsertado as ObjectResult;
            Assert.AreEqual(400, resultado.StatusCode);
        }

        [Test]
        public void ActualizarConParametrosCorrectos()
        {
            //Arrange
            MaestroNivelPuestoTrabajoController maestroNivelPuestoTrabajoController = new MaestroNivelPuestoTrabajoController(_integraDBContext);

            PuestoTrabajoNivelDTO nivelPuestoTrabajoAgregar = new PuestoTrabajoNivelDTO()
            {
                Id = 15,
                Nombre = "Actualizar Parámetros Correctos",
                Usuario = "UsuarioPruebaTest"
            };

            //Act
            ActionResult nivelPuestoTrabajoInsertado = maestroNivelPuestoTrabajoController.ActualizarNivelPuestoTrabajo(nivelPuestoTrabajoAgregar);

            //Assert
            Assert.NotNull(nivelPuestoTrabajoInsertado);

            OkObjectResult resultado = nivelPuestoTrabajoInsertado as OkObjectResult;
            Assert.AreEqual(200, resultado.StatusCode);
        }

        [Test]
        public void ActualizarConNombreVacio()
        {
            //Arrange
            MaestroNivelPuestoTrabajoController maestroNivelPuestoTrabajoController = new MaestroNivelPuestoTrabajoController(_integraDBContext);

            PuestoTrabajoNivelDTO nivelPuestoTrabajoAgregar = new PuestoTrabajoNivelDTO()
            {
                Id = 15,
                Nombre = "",
                Usuario = "UsuarioPruebaTest"
            };

            //Act
            ActionResult nivelPuestoTrabajoInsertado = maestroNivelPuestoTrabajoController.ActualizarNivelPuestoTrabajo(nivelPuestoTrabajoAgregar);

            //Assert
            Assert.NotNull(nivelPuestoTrabajoInsertado);

            OkObjectResult resultado = nivelPuestoTrabajoInsertado as OkObjectResult;
            Assert.NotNull(resultado.StatusCode);
            Assert.AreEqual(200, resultado.StatusCode);
        }

        [Test]
        public void ActualizarConDatosIncorrectos()
        {
            //Arrange
            MaestroNivelPuestoTrabajoController maestroNivelPuestoTrabajoController = new MaestroNivelPuestoTrabajoController(_integraDBContext);

            PuestoTrabajoNivelDTO nivelPuestoTrabajoAgregar = new PuestoTrabajoNivelDTO()
            {
                Id = 15,
                Nombre = "ActualizaciónPruebaTest"
            };

            //Act
            ActionResult nivelPuestoTrabajoInsertado = maestroNivelPuestoTrabajoController.ActualizarNivelPuestoTrabajo(nivelPuestoTrabajoAgregar);

            //Assert
            Assert.NotNull(nivelPuestoTrabajoInsertado);

            ObjectResult resultado = nivelPuestoTrabajoInsertado as ObjectResult;
            Assert.NotNull(resultado.StatusCode);
            Assert.AreEqual(400, resultado.StatusCode);
        }
    }
}
