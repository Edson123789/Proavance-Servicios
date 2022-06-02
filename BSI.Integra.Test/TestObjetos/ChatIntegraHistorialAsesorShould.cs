    using BSI.Integra.Aplicacion.Comercial.BO;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace BSI.Integra.Test.TestObjetos
{
    public class ChatIntegraHistorialAsesorShould
    {
        public readonly ChatIntegraHistorialAsesorBO objeto;

        public ChatIntegraHistorialAsesorShould()
        {
            objeto = new ChatIntegraHistorialAsesorBO()
            {
                IdAsesorChatDetalle = 125,
                IdPersonal = 12,
                FechaAsignacion = DateTime.Now
            };
        }

        //Id asesores chats detalles
        [Fact]
        public void validarIdAsesoresChatsDetalles_NotNull()
        {
            Assert.NotNull(objeto.IdAsesorChatDetalle);
        }

        //Id asesor
        [Fact]
        public void validarIdAsesor_NotNull()
        {
            Assert.NotNull(objeto.IdPersonal);
        }
        //dia
        [Fact]
        public void validarDia_NotNull()
        {
            Assert.NotNull(objeto.FechaAsignacion);
        }
    }
}
