
using BSI.Integra.Aplicacion.Comercial.BO;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;


namespace BSI.Integra.Test.PruebasUnitarias.BO
{
    [TestFixture]
    public class MensajeTextoBOTests
    {
        [Test]
        public void GenerarMesajeTexto_SetTexto()
        {
            // Arrange
            var _mensajeTexto = new MensajeTextoBO();
            _mensajeTexto.Mensaje = "Este es un Mensaje de Prueba";

            // Act
            var mensaje = _mensajeTexto.Mensaje;
          
            // Assert
            Assert.AreEqual("Este es un Mensaje de Prueba",mensaje);

        }

        //[Test]
        //public void GenerarMesajeTexto_SegunCodigoPaisColombia()
        //{
        //    //VerificarMensajeTexto_EliminarCaracateresDeCodigoMAtricula
        //    // Arrange
        //    var _mensajeTexto = new MensajeTextoBO();
        //    _mensajeTexto.CodigoPais = 57;
        //    _mensajeTexto.IdMatriculaCabecera = "9637775A12304";
        //    _mensajeTexto.origenAgenda = true;

        //    // Act
        //    var mensaje = _mensajeTexto.Mensaje;
        //    var codigoMatricula = _mensajeTexto.IdMatriculaCabecera.Replace("A", "");
        //    // Assert
        //    Assert.AreEqual(mensaje, "BSG Institute: Codigo de referencia " + codigoMatricula);

        //}
        //[Test]
        //public void GenerarMesajeTexto_SegunCodigoPaisOtros()
        //{
        //    // Arrange
        //    var _mensajeTexto = new MensajeTextoBO();
        //    _mensajeTexto.CodigoPais = 51;
        //    _mensajeTexto.IdMatriculaCabecera = "9637775A12304";
        //    _mensajeTexto.origenAgenda = true;

        //    // Act
        //    var mensaje = _mensajeTexto.Mensaje;
          
        //    // Assert
        //    Assert.AreEqual(mensaje, "BSG Institute: Codigo Matricula " + _mensajeTexto.IdMatriculaCabecera);

        //}
        [Test]
        public void GenerarNumeroCelular_SegunPaisPeru()
        {
            // Arrange
            var _mensajeTexto = new MensajeTextoBO();
            _mensajeTexto.CodigoPais = 51;
            _mensajeTexto.Numero = "916565286";

            // Act
            var numeroCelular = _mensajeTexto.Numero;


            // Assert
            Assert.AreEqual("+51916565286", numeroCelular);
        }
        [Test]
        public void GenerarNumeroCelular_SegunPaisOtros_ConIncioNumerosCeros()
        {
            // Arrange
            var _mensajeTexto = new MensajeTextoBO();
            _mensajeTexto.CodigoPais = 57;
            _mensajeTexto.Numero = "00916565286";

            // Act
            var numeroCelular = _mensajeTexto.Numero;

            // Assert
            Assert.AreEqual(numeroCelular, "+916565286");

        }
        [Test]
        public void GenerarNumeroCelular_SegunPaisOtros_ConIncioNumerosSinCeros()
        {
            // Arrange
            var _mensajeTexto = new MensajeTextoBO();
            _mensajeTexto.CodigoPais = 57;
            _mensajeTexto.Numero = "916565286";

            // Act
            var numeroCelular = _mensajeTexto.Numero;

            // Assert
            Assert.AreEqual(numeroCelular, "+916565286");

        }
        [Test]
        public void GenerarNumeroCelular_SegunNumeroEsEmpty()
        {
            // Arrange
            var _mensajeTexto = new MensajeTextoBO();
            _mensajeTexto.CodigoPais = 57;
            _mensajeTexto.Numero = "";

            // Act
            var existeErrores = _mensajeTexto.HasErrors;

            // Assert
            Assert.True(existeErrores);

        }
        [Test]
        public void GenerarNumeroCelular_SegunNumeroEsNull()
        {
            // Arrange
            var _mensajeTexto = new MensajeTextoBO();
            _mensajeTexto.CodigoPais = 57;
            _mensajeTexto.Numero = null;

            // Act
            var existeErrores = _mensajeTexto.HasErrors;

            // Assert
            Assert.True(existeErrores);

        }
    }
}
