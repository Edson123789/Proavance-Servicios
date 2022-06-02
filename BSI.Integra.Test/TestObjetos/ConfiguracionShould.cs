using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Servicios.Controllers;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace BSI.Integra.Test.TestObjetos
{
    public class ConfiguracionShould
    {
        [Fact]
        public void validarCodigo_NotEmpty()
        {
            TConfiguracion objeto = new TConfiguracion();

            objeto.Codigo = "Codigo";

            Assert.NotEmpty(objeto.Codigo);
        }

        [Fact]
        public void validarCodigo_IgnoreCaseAssert()
        {
            TConfiguracion objeto = new TConfiguracion();

            objeto.Codigo = "CODIGO";

            Assert.Equal("Codigo", objeto.Codigo, ignoreCase: true);
        }

        [Fact]
        public void validarCodigo_SubStringAssert()
        {
            TConfiguracion objeto = new TConfiguracion();

            objeto.Codigo = "Codigo";

            Assert.Contains("igo", objeto.Codigo);
        }

        [Fact]
        public void validarCodigo_SoloLetrasAssert()
        {
            TConfiguracion objeto = new TConfiguracion();

            objeto.Codigo = "CODIGO";

            Assert.Matches(@"^[a-zA-Z]+$", objeto.Codigo);
        }

        //Nombre
        [Fact]
        public void validarNombre_NotEmpty()
        {
            TConfiguracion objeto = new TConfiguracion();

            objeto.Nombre = "Nombre";

            Assert.NotEmpty(objeto.Nombre);
        }

        [Fact]
        public void validarNombre_IgnoreCaseAssert()
        {
            TConfiguracion objeto = new TConfiguracion();

            objeto.Nombre = "NOMBRE";

            Assert.Equal("Nombre", objeto.Nombre, ignoreCase: true);
        }

        [Fact]
        public void validarNombre_SubStringAssert()
        {
            TConfiguracion objeto = new TConfiguracion();

            objeto.Nombre = "Nombre";

            Assert.Contains("omb", objeto.Nombre);
        }

        [Fact]
        public void validarNombre_SoloLetrasAssert()
        {
            TConfiguracion objeto = new TConfiguracion();

            objeto.Nombre = "NOMBRE";

            Assert.Matches(@"^[a-zA-Z]+$", objeto.Nombre);
        }

        //Valor
        [Fact]
        public void validarValor_NotEmpty()
        {
            TConfiguracion objeto = new TConfiguracion();

            objeto.Valor = "Valor";

            Assert.NotEmpty(objeto.Valor);
        }

        [Fact]
        public void validarValor_IgnoreCaseAssert()
        {
            TConfiguracion objeto = new TConfiguracion();

            objeto.Valor = "VALOR";

            Assert.Equal("Valor", objeto.Valor, ignoreCase: true);
        }

        [Fact]
        public void validarValor_SubStringAssert()
        {
            TConfiguracion objeto = new TConfiguracion();

            objeto.Valor = "Valor";

            Assert.Contains("or", objeto.Valor);
        }

        [Fact]
        public void validarValor_SoloLetrasAssert()
        {
            TConfiguracion objeto = new TConfiguracion();

            objeto.Valor = "VALor";

            Assert.Matches(@"^[a-zA-Z]+$", objeto.Valor);
        }

    }
}
