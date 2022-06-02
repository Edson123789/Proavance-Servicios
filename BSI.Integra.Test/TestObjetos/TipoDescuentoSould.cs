using BSI.Integra.Aplicacion.Planificacion.BO;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace BSI.Integra.Test.TestObjetos
{
    public class TipoDescuentoSould
    {
        public readonly TipoDescuentoBO objeto;

        public TipoDescuentoSould()
        {
            objeto = new TipoDescuentoBO()
            {
                Codigo = "Codigo",
                Descripcion = "Descripcion"
            };

        }

        //Codigo
        [Fact]
        public void validarCodigo_NotEmpty()
        {
            Assert.NotEmpty(objeto.Codigo);
        }

        [Fact]
        public void validarCodigo_SubStringAssert()
        {
            Assert.Contains("odi", objeto.Codigo);
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
            Assert.Contains("cion", objeto.Descripcion);
        }
    }
}
