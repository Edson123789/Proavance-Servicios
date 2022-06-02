using BSI.Integra.Aplicacion.Marketing.BO;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace BSI.Integra.Test.TestObjetos
{
    public class ConfiguracionChatShould
    {
        public readonly ConfiguracionChatBO Objeto;

        public ConfiguracionChatShould()
        {
            Objeto = new ConfiguracionChatBO()
            {
                NombreConfiguracion = "Nombre Chat",
                VisualizarTiempo = 3,
                TextoHeader = "Chat con un Asesor en Línea",
                TextoHeaderNotificacion = "¿Necesitas ayuda?             Chatea con un asesor especializado",
                ColorFondoHeader = "#d4206b",
                ColorTextoHeader = "#ffffff",
                TextoHeaderFuente = "font-family: Arial, Helvetica, sans-serif;",
                IconoAsesor = "Icono Asesor",
                ColorFondoAsesor= "#ecedef",
                ColorTextoAsesor= "#000000",
                IconoInteresado = "Icono Interesado",
                ColorFondoInteresado= "#feeefd",
                ColorTextoInteresado = "#000000",
                TextoChatFuente = "font-family: Arial, Helvetica, sans-serif;",
                TextoOffline = "Déjanos tu Mensaje",
                TextoSatisfaccionOffline = "Tu mensaje fue enviado, nos contactaremos contigo a la brevedad.",
                TextoInicial = "¡Hola! Bienvenido, ¿Tienes alguna consulta con la que te pueda ayudar?",
                ColorTextoEmpezarChat = "#ffffff",
                ColorFondoEmpezarChat= "#f69336",
                TextoFormularioFuente = "font-family: Verdana, Geneva, sans-serif;",
                IconoChat = "Icono .png",
                MuestraTextoInicial = 1
            };
        }
        //Nombre
        [Fact]
        public void ValidarNombreConfiguracion_NotNull()
        {

            Assert.NotNull(Objeto.NombreConfiguracion);
        }

        [Fact]
        public void ValidarNombreConfiguracion_NotEmpty()
        {

            Assert.NotEmpty(Objeto.NombreConfiguracion);
        }
        //Visualizar tiempo
        [Fact]
        public void ValidarVisualizarTiempo_NotNull()
        {

            Assert.NotNull(Objeto.VisualizarTiempo);
        }

        //Texto Header
        [Fact]
        public void ValidarTextoHeader_NotNull()
        {
            Assert.NotNull(Objeto.TextoHeader);
        }

        [Fact]
        public void ValidarTextoHeader_NotEmpty()
        {
            Assert.NotEmpty(Objeto.TextoHeader);
        }

        //Texto Header Notificacion
        [Fact]
        public void ValidarTextoHeaderNotificacion_NotNull()
        {
            Assert.NotNull(Objeto.TextoHeaderNotificacion);
        }

        [Fact]
        public void ValidarTextoHeaderNotificacion_NotEmpty()
        {
            Assert.NotEmpty(Objeto.TextoHeaderNotificacion);
        }
        //Color Fondo header
        [Fact]
        public void ValidarColorFondoHeader_NotNull()
        {
            Assert.NotNull(Objeto.ColorFondoHeader);
        }

        [Fact]
        public void ValidarColorFondoHeader_NotEmpty()
        {
            Assert.NotEmpty(Objeto.ColorFondoHeader);
        }
        //Color texto header
        [Fact]
        public void ValidarColorTextoHeader_NotNull()
        {
            Assert.NotNull(Objeto.ColorTextoHeader);
        }

        [Fact]
        public void ValidarColorTextoHeader_NotEmpty()
        {
            Assert.NotEmpty(Objeto.ColorTextoHeader);
        }
        //Texto header Fuente
        [Fact]
        public void ValidarTextoHeaderFuente_NotNull()
        {
            Assert.NotNull(Objeto.TextoHeaderFuente);
        }

        [Fact]
        public void ValidarTextoHeaderFuente_NotEmpty()
        {
            Assert.NotEmpty(Objeto.TextoHeaderFuente);
        }

        //Icono asesor
        [Fact]
        public void ValidarIconoAsesor_NotNull()
        {
            Assert.NotNull(Objeto.IconoAsesor);
        }

        [Fact]
        public void ValidarIconoAsesor_NotEmpty()
        {
            Assert.NotEmpty(Objeto.IconoAsesor);
        }

        //Color Fondo asesor
        [Fact]
        public void ValidarColorFondoAsesor_NotNull()
        {
            Assert.NotNull(Objeto.IconoAsesor);
        }

        [Fact]
        public void ValidarColorFondoAsesor_NotEmpty()
        {
            Assert.NotEmpty(Objeto.ColorFondoAsesor);
        }
        //Color Texto asesor

        [Fact]
        public void ValidarColorTextoAsesor_NotNull()
        {
            Assert.NotNull(Objeto.ColorTextoAsesor);
        }

        [Fact]
        public void ValidarColorTextoAsesor_NotEmpty()
        {
            Assert.NotEmpty(Objeto.ColorTextoAsesor);
        }

        //Icono Interesado
        [Fact]
        public void ValidarIconoInteresado_NotNull()
        {
            Assert.NotNull(Objeto.IconoInteresado);
        }

        [Fact]
        public void ValidarIconoInteresado_NotEmpty()
        {
            Assert.NotEmpty(Objeto.IconoInteresado);
        }
        //Color fondo intesado
        [Fact]
        public void ValidarColorFondoInteresado_NotNull()
        {
            Assert.NotNull(Objeto.ColorFondoInteresado);
        }

        [Fact]
        public void ValidarColorFondoInteresado_NotEmpty()
        {
            Assert.NotEmpty(Objeto.ColorFondoInteresado);
        }
        //Color texto interesado
        [Fact]
        public void ValidarColorTextoInteresado_NotNull()
        {
            Assert.NotNull(Objeto.ColorTextoInteresado);
        }

        [Fact]
        public void ValidarColorTextoInteresado_NotEmpty()
        {
            Assert.NotEmpty(Objeto.ColorTextoInteresado);
        }
        //Texto Chat Fuente
        [Fact]
        public void ValidarTextoChatFuente_NotNull()
        {
            Assert.NotNull(Objeto.TextoChatFuente);
        }

        [Fact]
        public void ValidarTextoChatFuente_NotEmpty()
        {
            Assert.NotEmpty(Objeto.TextoChatFuente);
        }
        //Texto offline
        [Fact]
        public void ValidarTextoOffline_NotNull()
        {
            Assert.NotNull(Objeto.TextoOffline);
        }

        [Fact]
        public void ValidarTextoOffline_NotEmpty()
        {
            Assert.NotEmpty(Objeto.TextoOffline);
        }
        // texto satisfaccion offline

        [Fact]
        public void ValidarTextoSatisfaccionOffline_NotNull()
        {
            Assert.NotNull(Objeto.TextoSatisfaccionOffline);
        }

        [Fact]
        public void ValidarTextoSatisfaccionOffline_NotEmpty()
        {
            Assert.NotEmpty(Objeto.TextoSatisfaccionOffline);
        }
        //Texto Inicial
        [Fact]
        public void ValidarTextoInicial_NotNull()
        {
            Assert.NotNull(Objeto.TextoInicial);
        }

        [Fact]
        public void ValidarTextoInicial_NotEmpty()
        {
            Assert.NotEmpty(Objeto.TextoInicial);
        }
        //Color texto empezar chat
        [Fact]
        public void ValidarColorTextoEmpezarChat_NotNull()
        {
            Assert.NotNull(Objeto.ColorTextoEmpezarChat);
        }

        [Fact]
        public void ValidarColorTextoEmpezarChat_NotEmpty()
        {
            Assert.NotEmpty(Objeto.ColorTextoEmpezarChat);
        }
        //Color fondo empezar chat
        [Fact]
        public void ValidarColorFondoEmpezarChat_NotNull()
        {
            Assert.NotNull(Objeto.ColorFondoEmpezarChat);
        }

        [Fact]
        public void ValidarColorFondoEmpezarChat_NotEmpty()
        {
            Assert.NotEmpty(Objeto.ColorFondoEmpezarChat);
        }
        //Texto Formulario fuente
        [Fact]
        public void ValidarTextoFormularioFuente_NotNull()
        {
            Assert.NotNull(Objeto.TextoFormularioFuente);
        }

        [Fact]
        public void ValidarTextoFormularioFuente_NotEmpty()
        {
            Assert.NotEmpty(Objeto.TextoFormularioFuente);
        }
        //Icono chat
        [Fact]
        public void ValidarIconoChat_NotNull()
        {
            Assert.NotNull(Objeto.IconoChat);
        }

        [Fact]
        public void ValidarIconoChat_NotEmpty()
        {
            Assert.NotEmpty(Objeto.IconoChat);
        }
        //Muestra texto inicial

        [Fact]
        public void ValidarMuestraTextoInicial_NotNull()
        {
            Assert.NotNull(Objeto.MuestraTextoInicial);
        }

    }
}