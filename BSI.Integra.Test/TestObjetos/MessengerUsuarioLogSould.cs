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
    public class MessengerUsuarioLogSould
    {
        public readonly MessengerUsuarioLogBO objeto;


        public MessengerUsuarioLogSould()
        {
            objeto = new MessengerUsuarioLogBO()
            {
                IdMessengerUsuario = 10,
                IdPersonal = 644,
                IdAreaCapacitacion = 643
            };
        }

        //IdMessengerUsuario

        [Fact]
        public void validarIdMeseengerUsuario_ValorNoCeroAssert()
        {
            Assert.NotEqual(0, objeto.IdMessengerUsuario);
        }       

        //IdPersonal

        [Fact]
        public void validarIdPersonal_ValorNoCeroAssert()
        {
            Assert.NotEqual(0, objeto.IdPersonal);
        }

        //IdAreaCapacitacion

        [Fact]
        public void validarIdAreaCapacitacion_ValorNoCeroAssert()
        {
            Assert.NotEqual(0, objeto.IdAreaCapacitacion);
        }
    }
}
