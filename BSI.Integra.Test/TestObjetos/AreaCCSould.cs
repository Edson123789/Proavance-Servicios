using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using BSI.Integra.Aplicacion.Planificacion.BO;

namespace BSI.Integra.Test.TestObjetos
{
    public class AreaCCSould
    {
        public readonly AreaCcBO objeto;

        public AreaCCSould()
        {
            objeto = new AreaCcBO()
            {
                Nombre = "Nombre",
                Codigo = "22"
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

        ////Codigo
        //[Fact]
        //public void ValidarCodigo_ValorNoCeroAssert()
        //{
        //    Assert.NotEqual(0, objeto.Codigo);
        //}
    }
}
