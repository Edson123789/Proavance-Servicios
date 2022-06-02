using BSI.Integra.Aplicacion.DTOs;
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
    public class MaestroPersonalControllerTest
    {
        private readonly integraDBContext _integraDBContext;
        private readonly TestContextServicios injection;
        public MaestroPersonalControllerTest()
        {
            _integraDBContext = new integraDBContext();
            injection = new TestContextServicios();
        }

        [Test]
        public void ObtenerInformacionPersonalIdCero()
        {
            //Arrange
            MaestroPersonalController maestroPersonalController = new MaestroPersonalController(_integraDBContext);

            //Act
            int idPostulantePrueba = 0;
            ActionResult resultadoLista = maestroPersonalController.ObtenerInformacionPersonal(idPostulantePrueba);

            //Assert
            Assert.NotNull(resultadoLista);
            ObjectResult resultado = resultadoLista as ObjectResult;
            Assert.NotNull(resultado);
            Assert.AreEqual(400, resultado.StatusCode);

        }

        [Test]
        public void ObtenerInformacionPersonalIdCorrecto()
        {
            //Arrange
            MaestroPersonalController maestroPersonalController = new MaestroPersonalController(_integraDBContext);

            //Act
            int idPostulantePrueba = 4793;
            ActionResult resultadoLista = maestroPersonalController.ObtenerInformacionPersonal(idPostulantePrueba);

            //Assert
            Assert.NotNull(resultadoLista);
            OkObjectResult resultado = resultadoLista as OkObjectResult;
            Assert.NotNull(resultado);
            Assert.AreEqual(200, resultado.StatusCode);
        }

        [Test]
        public void InsertarConParametrosNuloDatosPersonal()
        {
            //Arrange
            MaestroPersonalController maestroPersonalController = new MaestroPersonalController(_integraDBContext);

            MaestroPersonalCompuestoDTO compuestoDatosPersonal = new MaestroPersonalCompuestoDTO()
            {
                Usuario = "UsuarioTest"
            };
            DatosMaestroPersonalDTO informacionPersonal = new DatosMaestroPersonalDTO();  
            compuestoDatosPersonal.Personal = informacionPersonal;
            //Act
            ActionResult nivelPuestoTrabajoInsertado = maestroPersonalController.InsertarPersonal(compuestoDatosPersonal);

            //Assert
            Assert.NotNull(nivelPuestoTrabajoInsertado);
            ObjectResult resultado = nivelPuestoTrabajoInsertado as ObjectResult;
            Assert.NotNull(resultado.Value);
            Assert.AreEqual(400, resultado.StatusCode);
        }
        [Test]
        public void InsertarConParametrosDiferentesConfiguracionNivelPuestoTrabajo()
        {
            //Arrange
            MaestroPersonalController maestroPersonalController = new MaestroPersonalController(_integraDBContext);

            MaestroPersonalCompuestoDTO compuestoDatosPersonal = new MaestroPersonalCompuestoDTO()
            {
                Usuario = "UsuarioTest"
            };
            DatosMaestroPersonalDTO informacionPersonal = new DatosMaestroPersonalDTO()
            {
                Id = 0,
                Activo = true,
                IdPuestoTrabajoNivel = 999
            };
            compuestoDatosPersonal.Personal = informacionPersonal;
            //Act
            ActionResult nivelPuestoTrabajoInsertado = maestroPersonalController.InsertarPersonal(compuestoDatosPersonal);

            //Assert
            Assert.NotNull(nivelPuestoTrabajoInsertado);
            ObjectResult resultado = nivelPuestoTrabajoInsertado as ObjectResult;
            Assert.NotNull(resultado.Value);
            Assert.AreEqual(400, resultado.StatusCode);
        }        
        [Test]
        public void ActualizarConParametrosNuloDatosPersonal()
        {
            //Arrange
            MaestroPersonalController maestroPersonalController = new MaestroPersonalController(_integraDBContext);

            MaestroPersonalCompuestoDTO compuestoDatosPersonal = new MaestroPersonalCompuestoDTO()
            {
                Usuario = "UsuarioTest"
            };
            DatosMaestroPersonalDTO informacionPersonal = new DatosMaestroPersonalDTO();
            compuestoDatosPersonal.Personal = informacionPersonal;
            //Act
            ActionResult nivelPuestoTrabajoInsertado = maestroPersonalController.ActualizarPersonal(compuestoDatosPersonal);

            //Assert
            Assert.NotNull(nivelPuestoTrabajoInsertado);
            ObjectResult resultado = nivelPuestoTrabajoInsertado as ObjectResult;
            Assert.NotNull(resultado.Value);
            Assert.AreEqual(400, resultado.StatusCode);
        }
        [Test]
        public void ActualizarConParametrosDiferentesConfiguracionNivelPuestoTrabajo()
        {
            //Arrange
            MaestroPersonalController maestroPersonalController = new MaestroPersonalController(_integraDBContext);

            MaestroPersonalCompuestoDTO compuestoDatosPersonal = new MaestroPersonalCompuestoDTO()
            {
                Usuario = "UsuarioTest"
            }; 
            DatosMaestroPersonalDTO informacionPersonal = new DatosMaestroPersonalDTO()
            {
                Id = 0,
                Activo = false,
                IdPuestoTrabajoNivel = 999
            };
            compuestoDatosPersonal.Personal = informacionPersonal;
            //Act
            ActionResult nivelPuestoTrabajoInsertado = maestroPersonalController.ActualizarPersonal(compuestoDatosPersonal);

            //Assert
            Assert.NotNull(nivelPuestoTrabajoInsertado);
            ObjectResult resultado = nivelPuestoTrabajoInsertado as ObjectResult;
            Assert.NotNull(resultado.Value);
            Assert.AreEqual(400, resultado.StatusCode);
        }
    }
}
