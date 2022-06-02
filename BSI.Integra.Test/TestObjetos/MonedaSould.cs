using System;
using System.Collections.Generic;
using System.Text;
using BSI.Integra.Aplicacion.Planificacion.BO;
using Xunit;

namespace BSI.Integra.Test.TestObjetos
{
    public class MonedaSould
    {
        public readonly MonedaBO objeto;

        public MonedaSould ()
        {
            objeto = new MonedaBO()
            {
                Nombre = "Nombre",
                NombreCorto = "Nombre",
                NombrePlural = "Nombre",
                Simbolo = "Nombre",
                Codigo = "Nombre",
                IdPais = 12            
            };

        }

        //Nombre
        [Fact]
        public void validarNombre_NotEmpty()
        {
            Assert.NotEmpty(objeto.Nombre);
        }

        [Fact]
        public void validadNombre_SubStringAssert()
        {
            Assert.Contains("omb", objeto.Nombre);
        }

        //NombreCorto
        [Fact]
        public void validarNombreCorto_NotEmpty()
        {
            Assert.NotEmpty(objeto.NombreCorto);
        }

        [Fact]
        public void validadNombreCorto_SubStringAssert()
        {
            Assert.Contains("omb", objeto.NombreCorto);
        }

        //NombrePlural
        [Fact]
        public void validarNombrePlural_NotEmpty()
        {
            Assert.NotEmpty(objeto.NombrePlural);
        }

        [Fact]
        public void validadNombrePlural_SubStringAssert()
        {
            Assert.Contains("omb", objeto.NombrePlural);
        }

        //Simbolo
        [Fact]
        public void validarSimbolo_NotEmpty()
        {
            Assert.NotEmpty(objeto.Simbolo);
        }

        [Fact]
        public void validadSimbolo_SubStringAssert()
        {
            Assert.Contains("omb", objeto.Simbolo);
        }

        //Codigo
        [Fact]
        public void validarCodigo_NotEmpty()
        {
            Assert.NotEmpty(objeto.Codigo);
        }

        [Fact]
        public void validadCodigo_SubStringAssert()
        {
            Assert.Contains("omb", objeto.Codigo);
        }

        //Idpais
        [Fact]
        public void validarIdPais_ValorNoCeroAssert()
        {
            Assert.NotEqual(0, objeto.IdPais);
        }
    }
}
