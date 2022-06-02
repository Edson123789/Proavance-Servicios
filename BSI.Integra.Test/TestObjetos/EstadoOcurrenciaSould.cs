using System;
using System.Collections.Generic;
using System.Text;
using BSI.Integra.Aplicacion.Comercial.BO;
using Xunit;

namespace BSI.Integra.Test.TestObjetos
{
    public class EstadoOcurrenciaSould
    {
        public readonly EstadoOcurrenciaBO objeto;

        public EstadoOcurrenciaSould()
        {
            objeto = new EstadoOcurrenciaBO()
            {
                Nombre = "Nombre",
                Descripcion = "Descripcion"
            };
        }

        //Nombre
        [Fact]
        public void validarNombre_NotEmpty()
        {
            Assert.NotEmpty(objeto.Nombre);
        }

        [Fact]
        public void validarNombre_SubStringAssert()
        {
            Assert.Contains("omb", objeto.Nombre);
        }

        //Descripcion
        [Fact]
        public void validarDescripcion_NotEmpty()
        {
            Assert.NotEmpty(objeto.Descripcion);
        }

        [Fact]
        public void validarDescripcion_SubStringAssert()
        {
            Assert.Contains("crip", objeto.Descripcion);
        }
    }
}
