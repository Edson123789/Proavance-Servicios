
using BSI.Integra.Aplicacion.DTOs.Scode.AutoMap;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Test.Fixtures;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Test.PruebasUnitarias.BO
{
    [TestFixture]
    public class AsignacionAutomaticaTest
    {
        public readonly TestContextServicios _testContextServicios;
        public readonly integraDBContext _integraDBContext;
        public AsignacionAutomaticaTest()
        {
            _integraDBContext = new integraDBContext();
            _testContextServicios = new TestContextServicios();
        }

        [Test]
        public void ProcesarRegistroFormularioNuevoPortalWeb_empty()
        {
            // Arrange
            AsignacionAutomaticaTempRepositorio _repAsignacionAutomaticaTemp = new AsignacionAutomaticaTempRepositorio(_integraDBContext);
            AsignacionAutomaticaTempBO asignacionAutomaticaTemp = new AsignacionAutomaticaTempBO();

            var idRegistroPortalWeb = "7DD93A93-941C-4DA9-86CF-097B0B92A1B5";
            int idPagina = 1;

            var registrosAEliminar = _repAsignacionAutomaticaTemp.GetBy(w => w.IdFaseOportunidadPortal == new Guid(idRegistroPortalWeb), x => new List<int> { x.Id });

            foreach (var idAsignacionAutomaticaTemp in registrosAEliminar)
            {
                _repAsignacionAutomaticaTemp.Delete(idAsignacionAutomaticaTemp, "PruebaUnitaria");
            }
            

            // Act
            asignacionAutomaticaTemp.ProcesarRegistroFormularioNuevoPortalWeb(idRegistroPortalWeb, idPagina);

            // Assert
            Assert.AreNotEqual(asignacionAutomaticaTemp.Apellidos, null);

        }

        [Test]
        public void ProcesarRegistroFormularioNuevoPortalWeb_llenadocorrecto()
        {
            // Arrange
            AsignacionAutomaticaTempRepositorio _repAsignacionAutomaticaTemp = new AsignacionAutomaticaTempRepositorio(_integraDBContext);
            AsignacionAutomaticaTempBO asignacionAutomaticaTemp = new AsignacionAutomaticaTempBO();
            var idRegistroPortalWeb = "7DD93A93-941C-4DA9-86CF-097B0B92A1B5";
            int idPagina = 1;


            var registrosAEliminar = _repAsignacionAutomaticaTemp.GetBy(w => w.IdFaseOportunidadPortal == new Guid(idRegistroPortalWeb), x => new List<int> { x.Id });

            foreach (var idAsignacionAutomaticaTemp in registrosAEliminar)
            {
                _repAsignacionAutomaticaTemp.Delete(idAsignacionAutomaticaTemp, "PruebaUnitaria");
            }

            // Act
            asignacionAutomaticaTemp.ProcesarRegistroFormularioNuevoPortalWeb(idRegistroPortalWeb, idPagina);

            // Assert
            Assert.AreEqual(asignacionAutomaticaTemp.Apellidos, "Rodriguez");
            Assert.AreEqual(asignacionAutomaticaTemp.Correo, "Dydda1020@gmail.com");

        }


        [Test]
        public void insertarAsignacionAutomaticaTempRep_correct()
        {

            //Arrange
            AsignacionAutomaticaTempRepositorio _repAsignacionAutomaticaTemp = new AsignacionAutomaticaTempRepositorio(_integraDBContext);
            AsignacionAutomaticaTempBO asignacionAutomaticaTemp = new AsignacionAutomaticaTempBO();
            var idRegistroPortalWeb = "7DD93A93-941C-4DA9-86CF-097B0B92A1B5";
            int idPagina = 1;

            var registrosAEliminar = _repAsignacionAutomaticaTemp.GetBy(w => w.IdFaseOportunidadPortal == new Guid(idRegistroPortalWeb), x => new List<int> { x.Id });

            foreach (var idAsignacionAutomaticaTemp in registrosAEliminar)
            {
                _repAsignacionAutomaticaTemp.Delete(idAsignacionAutomaticaTemp, "PruebaUnitaria");
            }

            asignacionAutomaticaTemp.ProcesarRegistroFormularioNuevoPortalWeb(idRegistroPortalWeb, idPagina);
            AsignacionAutomaticaTempRepositorio _asignacionAutomaticaTempRep = new AsignacionAutomaticaTempRepositorio();

            //MapperConfig.RegistarMapp();
            //Act
            bool rpta_insercion = _asignacionAutomaticaTempRep.Insert(asignacionAutomaticaTemp);

            //Assert
            Assert.AreEqual(rpta_insercion, true);

        }

        [Test]
        public void insertarAsignacionAutomaticaTempRep_incorrect()
        {
            //Arrange
            AsignacionAutomaticaTempBO AsignacionAutomaticaTemp = new AsignacionAutomaticaTempBO();            
            AsignacionAutomaticaTemp = null;
            AsignacionAutomaticaTempRepositorio _asignacionAutomaticaTempRep = new AsignacionAutomaticaTempRepositorio();
            int count = 0;
            try
            {
                MapperConfig.RegistarMapp();
                //Act
                var rpta_insercion = _asignacionAutomaticaTempRep.Insert(AsignacionAutomaticaTemp);

            }
            catch (Exception e) {
                count++;
            }
            Assert.AreNotEqual(0, count);
        }
                
        [Test]
        public void CadenaEsNula()
        {
            //Arange
            var _cadena = new AsignacionAutomaticaTempBO();
            string cad = "";

            //Act
            var cadena = _cadena.QuitarCaracteres(cad);

            //Assert
            Assert.IsEmpty(cadena);
        }

        [Test]
        public void CadenaNoNula()
        {
            //Arange
            var _cadena = new AsignacionAutomaticaTempBO();
            string cad = "unico_libre";

            //Act
            var cadena = _cadena.QuitarCaracteres(cad);

            //Assert
            Assert.AreEqual(cadena, "unico libre");
        }

        [Test]
        public void GenerarCentroCosto()
        {
            //Arange
            var _cadena = new AsignacionAutomaticaTempBO();
            //string cad = "  unico solo -bsg-institute logo";
            string cad2 = "BSG INSTIUTE Logo 12 - Arequipa";

            //Act
            var cadena = _cadena.ObtenerCentroCosto(cad2);

            //Assert
            Assert.AreEqual(cadena, "BSG INSTIUTE Logo 12");
        }

        [Test]
        public void LograrQuitarEspacios()
        {
            //Arange
            var _cadena = new AsignacionAutomaticaTempBO();
            string cad2 = "   BSG INSTIUTE  ";

            //Act
            var cadena = _cadena.QuitarEspacios(cad2);

            //Assert
            Assert.AreEqual(cadena, "BSG INSTIUTE");
        }

        [Test]
        public void GeneraNumeroTelefonicoValido()
        {
            //Arange
            var _numero = new AsignacionAutomaticaTempBO();
            string cad2 = "12345 &6789";
            string cad1 = "234g67";

            //Act
            var numero = _numero.ObtenerNumeroTelefonico(cad2);
            var numero2 = _numero.ObtenerNumeroTelefonico(cad1);

            //Assert
            Assert.AreEqual(numero, "123456789");
            Assert.AreEqual(numero2, "23467");
        }

        [Test]
        public void ProcesarNombreValido()
        {
            //Arange
            var _nombre = new AsignacionAutomaticaTempBO();
            string fullname2 = "cAñROLina AleXSANdra luUCía López Gutiéññez";
            string fullname1 = "beautifull ALONSÓ gonÑales";
            string fullname3 = "lUCÁS beAUTIFull roDRÍGueñ";

            //Act
            var nombre = _nombre.ProcesarNombre(fullname2);
            var nombre1 = _nombre.ProcesarNombre(fullname1);
            var nombre2 = _nombre.ProcesarNombre(fullname3);


            //Assert
            Assert.AreEqual(nombre[0], "Canrolina Alexsandra Luucia");
            Assert.AreEqual(nombre[1], "Lopez Gutiennez");

            Assert.AreEqual(nombre1[0], "Beautifull");
            Assert.AreEqual(nombre1[1], "Alonso Gonnales");

            Assert.AreEqual(nombre2[0], "Lucas");
            Assert.AreEqual(nombre2[1], "Beautifull Rodriguen");
            
        }

        [Test]
        public void ObtenerSeparadoNombreApellido()
        {
            //Arange
            var _nombre = new AsignacionAutomaticaTempBO();
            var lista = new List<String>();
            lista.AddRange(new[] { "LUCIA Eva", "Campos Zegarra" });

            //Act
            var nombre = _nombre.SepararNombresApellidos(lista);

            //Assert
            Assert.AreEqual(nombre, lista);
        }
    }
}