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
    public class PostComentarioUsuarioSould
    {
        public readonly PostComentarioUsuarioBO objeto;

        public PostComentarioUsuarioSould()
        {
            objeto = new PostComentarioUsuarioBO()
            {
                IdUsuario = "2016857531701411",
                Nombres = "Diaz Espiritu Jesus Alberto",
                IdAsesor = 568
            };
        }

        //IdUsuario

        [Fact]
        public void validarIdUsuario_NotEmpty()
        {
            Assert.NotEmpty(objeto.IdUsuario);
        }

        [Fact]
        public void validarIdUsuario_SubStringAssert()
        {
            Assert.Contains("2016", objeto.IdUsuario);
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
            Assert.Contains("Espi", objeto.Nombres);
        }

        //[Fact]
        //public void validarNombre_SoloLetrasAssert()
        //{
        //    Assert.Matches(@"^[a-zA-Z]+$", objeto.Nombres);
        //}

        //IdAsesor

        [Fact]
        public void validarIdAsesor_SubStringAssert()
        {
            Assert.NotEqual(0, objeto.IdAsesor);
        }

        //Respuesta

        [Fact]
        public void validarRespuesta_EsVerdaderoAssert()
        {
            objeto.Respuesta = true;

            Assert.True(objeto.Respuesta, "El Valor debe ser Verdadero");
        }

        [Fact]
        public void validarRespuesta_NoVerdaderoAssert()
        {
            objeto.Respuesta = false;

            Assert.False(objeto.Respuesta, "El Valor debe ser Falso");
        }
    }
}
