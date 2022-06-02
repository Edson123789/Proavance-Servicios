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
    public class MessengerChatSould
    {
        public readonly MessengerChatBO objeto;


        public MessengerChatSould()
        {
            objeto = new MessengerChatBO()
            {
                IdMeseengerUsuario = 10,
                Mensaje = "Mensaje",
                IdPersonal = 643
            };
        }

        //IdMeseengerUsuario

        [Fact]
        public void validarIdMeseengerUsuario_ValorNoCeroAssert()
        {
            Assert.NotEqual(0, objeto.IdMeseengerUsuario);
        }

        //Mensaje

        [Fact]
        public void validarMensaje_NotEmpty()
        {
            Assert.NotEmpty(objeto.Mensaje);
        }

        [Fact]
        public void validarMensaje_SubStringAssert()
        {
            Assert.Contains("Men", objeto.Mensaje);
        }


        //IdPersonal

        [Fact]
        public void validarIdPersonal_ValorNoCeroAssert()
        {
            Assert.NotEqual(0, objeto.IdPersonal);
        }        

    }
}
