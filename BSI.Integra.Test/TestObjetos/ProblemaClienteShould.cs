using BSI.Integra.Aplicacion.Comercial.BO;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace BSI.Integra.Test.TestObjetos
{
    public class ProblemaClienteShould
    {

        public readonly ProblemaClienteBO objeto;

        public ProblemaClienteShould()
        {
            objeto = new ProblemaClienteBO()
            {
                Nombre = "nombre",
                Descripcion = "Descripcion"

            };
        }

        //Nombre
        [Fact]
        public void validarNombre_NotNull()
        {
            Assert.NotNull(objeto.Nombre);
        }
        [Fact]
        public void validarNombre_NotEmpty()
        {
            Assert.NotEmpty(objeto.Nombre);
        }
        //Descripcion
        [Fact]
        public void validarDescripcion_NotNull()
        {
            Assert.NotNull(objeto.Descripcion);
        }
        [Fact]
        public void validarDescripcion_NotEmpty()
        {
            Assert.NotEmpty(objeto.Descripcion);
        }
    }
}
