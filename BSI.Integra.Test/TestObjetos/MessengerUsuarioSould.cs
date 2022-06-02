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
    public class MessengerUsuarioSould
    {
        public readonly MessengerUsuarioBO objeto;


        public MessengerUsuarioSould()
        {
            objeto = new MessengerUsuarioBO()
            {
                Psid = "2242916959060894",
                Nombres = "Jonathan Quintana Cueva",
                IdPersonal = 643
            };
        }

        //PSID

        [Fact]
        public void validarPsId_NotEmpty()
        {
            Assert.NotEmpty(objeto.Psid);
        }

        [Fact]
        public void validarPsId_SubStringAssert()
        {
            Assert.Contains("429", objeto.Psid);
        }

        //Nombres

        [Fact]
        public void validarNombres_NotEmpty()
        {
            Assert.NotEmpty(objeto.Nombres);
        }

        [Fact]
        public void validarNombres_SubStringAssert()
        {
            Assert.Contains("Jona", objeto.Nombres);
        }

        //IdAsesor

        [Fact]
        public void validarIdAsesor_ValorNoCeroAssert()
        {
            Assert.NotEqual(0, objeto.IdPersonal);
        }

        //Respuesta

        [Fact]
        public void validarRespuesta_EsVerdaderoAssert()
        {
            objeto.SeRespondio = true;

            Assert.True(objeto.SeRespondio, "El Valor debe ser Verdadero");
        }

        [Fact]
        public void validarRespuesta_NoVerdaderoAssert()
        {
            objeto.SeRespondio = false;

            Assert.False(objeto.SeRespondio, "El Valor debe ser Falso");
        }

    }
}
