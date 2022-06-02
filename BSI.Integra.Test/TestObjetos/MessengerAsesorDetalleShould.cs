using BSI.Integra.Aplicacion.Comercial.BO;
using BSI.Integra.Servicios.Controllers;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace BSI.Integra.Test.TestObjetos
{
    public class MessengerAsesorDetalleShould
    {
        public readonly MessengerAsesorDetalleBO objeto;
        MessengerAsesorDetalleController _controlador;
        ValidadorMessengerAsesorDetalleDTO _validadorObjeto;

        public MessengerAsesorDetalleShould()
        {
            objeto = new MessengerAsesorDetalleBO()
            {
                IdMessengerAsesor = 1,
                IdPgeneral = 2
            };
            _validadorObjeto = new ValidadorMessengerAsesorDetalleDTO();

        }

        //IdMessengerAsesor
        [Fact]
        public void validarIdMessengerAsesor_NotNull()
        {
            Assert.NotNull(objeto.IdMessengerAsesor);
        }

        [Fact]
        public void validarIdMessengerAsesor_ValorNoCeroAssert()
        {

            Assert.NotEqual(0, objeto.IdMessengerAsesor);
        }

        //IdPgeneral
        [Fact]
        public void validarIdPgeneral_NotNull()
        {
            Assert.NotNull(objeto.IdPgeneral);
        }

        [Fact]
        public void validarIdPgeneral_ValorNoCeroAssert()
        {

            Assert.NotEqual(0, objeto.IdPgeneral);
        }
    }
}
