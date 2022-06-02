using BSI.Integra.Aplicacion.Planificacion.BO;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace BSI.Integra.Test.TestObjetos
{
    public class SubAreaCapacitacionSould
    {
        public readonly SubAreaCapacitacionBO objeto;

        public SubAreaCapacitacionSould()
        {
            objeto = new SubAreaCapacitacionBO();
        }

        [Fact]
        public void validarNombre_NotEmpty()
        {
            objeto.Nombre = "Nombre de Envio";

            Assert.NotEmpty(objeto.Nombre);
        }

        [Fact]
        public void validarNombre_SubStringAssert()
        {
            objeto.Nombre = "Nombre";

            Assert.Contains("bre", objeto.Nombre);
        }

        [Fact]
        public void validarDescripcion_NotEmpty()
        {
            objeto.Descripcion = "Nombre";

            Assert.NotEmpty(objeto.Descripcion);
        }

        [Fact]
        public void validarDescripcion_SubStringAssert()
        {
            objeto.Descripcion = "Nombre";

            Assert.Contains("bre", objeto.Descripcion);
        }

        [Fact]
        public void validarIdAreaCapacitacion_ValorNoCeroAssert()
        {
            objeto.IdAreaCapacitacion = 1;

            Assert.NotEqual(0, objeto.IdAreaCapacitacion);
        }

        [Fact]
        public void validaridtSubArea_ValorNoCeroAssert()
        {
            objeto.idtSubArea = 1;

            Assert.NotEqual(0, objeto.idtSubArea);
        }

        [Fact]
        public void validarCodigo_NotEmpty()
        {
            objeto.Codigo = "Nombre de Envio";

            Assert.NotEmpty(objeto.Codigo);
        }

        [Fact]
        public void validarCodigo_SubStringAssert()
        {
            objeto.Codigo = "Nombre";

            Assert.Contains("bre", objeto.Codigo);
        }

    }
}
