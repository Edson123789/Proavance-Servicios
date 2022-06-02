using BSI.Integra.Aplicacion.Comercial.BO;
using BSI.Integra.Aplicacion.Maestros.BO;
using BSI.Integra.Servicios.Controllers;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace BSI.Integra.Test.TestObjetos
{
    public class WebHookRequestLogSould
    {
        public readonly WebHookRequestLogBO objeto;


        public WebHookRequestLogSould()
        {
            objeto = new WebHookRequestLogBO()
            {
                Verb = "Verb",
                Content = "Content"
            };
        }

        //Verb

        [Fact]
        public void validarVerb_NotEmpty()
        {
            Assert.NotEmpty(objeto.Verb);
        }

        [Fact]
        public void validarVerb_SubStringAssert()
        {
            Assert.Contains("erb", objeto.Verb);
        }

        //IdPersonal

        [Fact]
        public void validarContent_NotEmpty()
        {
            Assert.NotEmpty(objeto.Content);
        }

        [Fact]
        public void validarContent_SubStringAssert()
        {
            Assert.Contains("Con", objeto.Content);
        }
    }
}
