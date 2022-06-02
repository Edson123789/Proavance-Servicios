using BSI.Integra.Aplicacion.Marketing.BO;
using BSI.Integra.Servicios.Controllers;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace BSI.Integra.Test.TestObjetos
{
    public class MessengerConfiguracionChatShould
    {
        public readonly MessengerConfiguracionChatBO objeto;
        ConfiguracionComentariosMessengerFacebookController _controlador;
        ValidadorMessengerConfiguracionChatDTO _validadorObjeto;

        public MessengerConfiguracionChatShould()
        {
            objeto = new MessengerConfiguracionChatBO()
            {
                NombreConfiguracion = "nombre Prueba",
                TextoOffline = "texto de Prueba",
                TextoSatisfaccionOffline = "texto de satisfaccion offline"
            };
            _validadorObjeto = new ValidadorMessengerConfiguracionChatDTO();

        }

        //NombreConfiguracion
        [Fact]
        public void validarNombreConfiguracion_NotEmpty()
        {
            Assert.NotEmpty(objeto.NombreConfiguracion);
        }

        [Fact]
        public void validarNombreConfiguracion_NotNull()
        {
            Assert.NotNull(objeto.NombreConfiguracion);
        }

        [Fact]
        public void validarNombreConfiguracion_LenghtBetween1and100()
        {
            Assert.InRange(objeto.NombreConfiguracion.Length, 1, 100);
        }

        //TextoOffline
        [Fact]
        public void validarTextoOffline_NotEmpty()
        {
            Assert.NotEmpty(objeto.TextoOffline);
        }

        [Fact]
        public void validarTextoOffline_NotNull()
        {
            Assert.NotNull(objeto.TextoOffline);
        }

        [Fact]
        public void validarTextoOffline_LenghtBetween1and500()
        {
            Assert.InRange(objeto.TextoOffline.Length, 1, 500);
        }

        //TextoSatisfaccionOffline
        [Fact]
        public void validarTextoSatisfaccionOffline_NotEmpty()
        {
            Assert.NotEmpty(objeto.TextoSatisfaccionOffline);
        }

        [Fact]
        public void validarTextoSatisfaccionOffline_NotNull()
        {
            Assert.NotNull(objeto.TextoSatisfaccionOffline);
        }

        [Fact]
        public void validarTextoSatisfaccionOffline_LenghtBetween1and500()
        {
            Assert.InRange(objeto.TextoSatisfaccionOffline.Length, 1, 500);
        }
    }
}
