using BSI.Integra.Aplicacion.Comercial.BO;
using BSI.Integra.Servicios.Controllers;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace BSI.Integra.Test.TestObjetos
{
    public class MessengerHistorialAsesorShould
    {
        public readonly MessengerHistorialAsesorBO objeto;
        MessengerHistorialAsesorController _controlador;
        ValidadorMessengerHistorialAsesorDTO _validadorObjeto;

        public MessengerHistorialAsesorShould()
        {
            objeto = new MessengerHistorialAsesorBO()
            {
                IdMessengerAsesorDetalle = 1,
                IdMessengerAsesor = 1,
                Fecha = DateTime.Now
            };
            _validadorObjeto = new ValidadorMessengerHistorialAsesorDTO();

        }

        //IdMessengerAsesorDetalle
        [Fact]
        public void validarIdMessengerAsesorDetalle_NotNull()
        {
            Assert.NotNull(objeto.IdMessengerAsesorDetalle);
        }

        [Fact]
        public void validarIdMessengerAsesorDetalle_ValorNoCeroAssert()
        {

            Assert.NotEqual(0, objeto.IdMessengerAsesorDetalle);
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

        //Fecha
        [Fact]
        public void validarFechaNotNull()
        {
            Assert.NotNull(objeto.Fecha);
        }
    }
}
