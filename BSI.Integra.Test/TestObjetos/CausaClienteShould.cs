using BSI.Integra.Aplicacion.Comercial.BO;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace BSI.Integra.Test.TestObjetos
{
    public class CausaClienteShould
    {
        public readonly CausaClienteBO objeto;

        public CausaClienteShould()
        {
            objeto = new CausaClienteBO()
            {
                Nombre = "nombre",
                Descripcion = "Descripcion",
                IdProblema = 6

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

        //IdProblema
        [Fact]
        public void validarIdProblema_ValorNoCeroAssert()
        {
            Assert.NotEqual(0, objeto.IdProblema);
        }
        [Fact]
        public void ValidarIdProblema_NotNull()
        {
            Assert.NotNull(objeto.IdProblema);
        }
    }
}
