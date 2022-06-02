using BSI.Integra.Aplicacion.Comercial.BO;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace BSI.Integra.Test.TestObjetos
{
    public class AsesorChatDetalleShould
    {
        public readonly AsesorChatDetalleBO objeto;

        public AsesorChatDetalleShould()
        {
            objeto = new AsesorChatDetalleBO()
            {
                IdAsesorChat = 1,
                IdPais = 51,
                IdPgeneral = 12
            };

        }

        //Id Asesor Chat
        [Fact]
        public void validarIdAsesorChat_NotNull()
        {
            Assert.NotNull(objeto.IdAsesorChat);
        }

       //Id pais
        [Fact]
        public void validarIdPais_NotNull()
        {
            Assert.NotNull(objeto.IdPais);
        }

        //Id GP
        [Fact]
        public void validarIdGP_NotNull()
        {
            Assert.NotNull(objeto.IdPgeneral);
        }

    }
}
