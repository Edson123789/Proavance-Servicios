using BSI.Integra.Aplicacion.Comercial.BO;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace BSI.Integra.Test.TestObjetos
{
    public class ChatDetalleIntegraShould
    {
        public readonly ChatDetalleIntegraBO objeto;

        public ChatDetalleIntegraShould()
        {
            objeto = new ChatDetalleIntegraBO()
            {
                IdInteraccionChatIntegra = 1,
                NombreRemitente = "User test",
                Mensaje = "Mensaje de usuario ..................",
                Fecha = DateTime.Now
            };

        }

        //Id Interaccion usuario chat
        [Fact]
        public void validarIdInteraccionChatIntegra_NotNull()
        {
            Assert.NotNull(objeto.IdInteraccionChatIntegra);
        }

        //Nombre remitente
        [Fact]
        public void validarNombreRemitente_NotEmpty()
        {
            Assert.NotEmpty(objeto.NombreRemitente);
        }
        [Fact]
        public void validarNombreRemitente_NotNull()
        {
            Assert.NotNull(objeto.NombreRemitente);
        }
        //Id remitente
        [Fact]
        public void validarIdRemitente_NotNull()
        {
            Assert.NotNull(objeto.IdRemitente);
        }
        //Mensaje
        [Fact]
        public void validarMensaje_NotEmpty()
        {
            Assert.NotEmpty(objeto.Mensaje);
        }
        [Fact]
        public void validarMensaje_NotNull()
        {
            Assert.NotNull(objeto.Mensaje);
        }
        //Fecha
        [Fact]
        public void validarFecha_NotNull()
        {
            Assert.NotNull(objeto.Fecha);
        }

    }
}
