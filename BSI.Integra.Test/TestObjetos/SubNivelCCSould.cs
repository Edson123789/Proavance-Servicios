using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using BSI.Integra.Aplicacion.Planificacion.BO;

namespace BSI.Integra.Test.TestObjetos
{
    public class SubNivelCCSould
    {
        public readonly SubNivelCcBO objeto;

        public SubNivelCCSould()
        {
            objeto = new SubNivelCcBO()
            {
                Nombre = "Nombre"
                //CODIGO = 2,
                //IDAREACC=3
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

        ////IdAreaCC
        //[Fact]
        //public void ValidarIdAreaCC_ValorNoCeroAssert()
        //{
        //    Assert.NotEqual(0, objeto.IdAreaCC);
        //}
    }
}
