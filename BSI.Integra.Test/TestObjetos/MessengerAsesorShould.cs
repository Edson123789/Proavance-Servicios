using BSI.Integra.Aplicacion.Comercial.BO;
using BSI.Integra.Servicios.Controllers;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace BSI.Integra.Test.TestObjetos
{
    public class MessengerAsesorShould
    {
        public readonly MessengerAsesorBO objeto;
        MessengerAsesorController _controlador;
        ValidadorMessengerAsesorDTO _validadorObjeto;

        public MessengerAsesorShould()
        {
            objeto = new MessengerAsesorBO()
            {
                IdPersonal = 1,
                ConteoClientesAsignados = 2
            };
            _validadorObjeto = new ValidadorMessengerAsesorDTO();

        }

        //IdPersonal
        [Fact]
        public void validarIdPersonal_NotNull()
        {
            Assert.NotNull(objeto.IdPersonal);
        }

        [Fact]
        public void validarIdPersonal_ValorNoCeroAssert()
        {
   
            Assert.NotEqual(0, objeto.IdPersonal);
        }

        //ConteoClientesAsignados
        [Fact]
        public void validarConteoClientesAsignados_NotNull()
        {
            Assert.NotNull(objeto.ConteoClientesAsignados);
        }

    }
}
