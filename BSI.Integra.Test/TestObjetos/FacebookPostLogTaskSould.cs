using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using BSI.Integra.Aplicacion.Comercial.BO;

namespace BSI.Integra.Test.TestObjetos
{
    public class FacebookPostLogTaskSould
    {
        public readonly FacebookPostLogTaskBO objeto;

        public FacebookPostLogTaskSould()
        {
            objeto = new FacebookPostLogTaskBO()
            {
                Message = "Message",
                ResponseJson = "ResponseJson"
            };
        }
        //Message
        [Fact]
        public void validarMessage_NotEmpty()
        {
            Assert.NotEmpty(objeto.Message);
        }

        [Fact]
        public void validarMessage_SubStringAssert()
        {
            Assert.Contains("essa", objeto.Message);
        }



        //ResponseJson
        [Fact]
        public void validarResponseJson_NotEmpty()
        {
            Assert.NotEmpty(objeto.ResponseJson);
        }

        [Fact]
        public void validarResponseJson_SubStringAssert()
        {
            Assert.Contains("esp", objeto.ResponseJson);
        }

    }
}
