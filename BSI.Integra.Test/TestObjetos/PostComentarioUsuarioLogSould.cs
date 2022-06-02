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
    public class PostComentarioUsuarioLogSould
    {
        public readonly PostComentarioUsuarioLogBO objeto;

        public PostComentarioUsuarioLogSould()
        {
            objeto = new PostComentarioUsuarioLogBO()
            {
                IdPostComentarioUsuario = 123,
                IdPersonal = 123,
                IdAreaCapacitacion= 123,

            };
        }

        

        //IdPostComentarioUsuario

        [Fact]
        public void validarIdPostComentarioUsuario_ValorNoCeroAssert()
        {
            Assert.NotEqual(0, objeto.IdPostComentarioUsuario);
        }

        //IdPersonal

        [Fact]
        public void validarIdPersonal_ValorNoCeroAssert()
        {
            Assert.NotEqual(0, objeto.IdPersonal);
        }

        //IdAreaCapacitacion

        [Fact]
        public void validarIdAreaCapacitacion_ValorNoCeroAssert()
        {
            Assert.NotEqual(0, objeto.IdAreaCapacitacion);
        }
        
    }
}
