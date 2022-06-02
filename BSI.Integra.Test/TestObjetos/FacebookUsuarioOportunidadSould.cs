using BSI.Integra.Aplicacion.Comercial.BO;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace BSI.Integra.Test.TestObjetos
{
    public class FacebookUsuarioOportunidadSould
    {
        public readonly FacebookUsuarioOportunidadBO objeto;

        public FacebookUsuarioOportunidadSould()
        {
            objeto = new FacebookUsuarioOportunidadBO()
            {
                Psid = "234234234223423",
                IdOportunidad = 5,
                IdPersonal=643
            };
        }
        //Psid
        [Fact]
        public void validarCodigo_NotEmpty()
        {
            Assert.NotEmpty(objeto.Psid);
        }

        [Fact]
        public void validarCodigo_SubStringAssert()
        {
             Assert.Contains("342", objeto.Psid);
        }

        //IdOportunidad
        [Fact]
        public void ValidarIdOportunidad_ValorNoCeroAssert()
        {
            Assert.NotEqual(0, objeto.IdOportunidad);
        }

        //IdPersonal
        [Fact]
        public void ValidarIdPersonal_ValorNoCeroAssert()
        {
            Assert.NotEqual(0, objeto.IdPersonal);
        }

        
    }
}
