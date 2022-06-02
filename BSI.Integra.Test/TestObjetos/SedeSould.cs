using BSI.Integra.Aplicacion.Planificacion.BO;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace BSI.Integra.Test.TestObjetos
{
    public class SedeSould
    {
        public readonly SedeBO objeto;

        public SedeSould()
        {
            objeto = new SedeBO();
        }

        [Fact]
        public void validarIdPais_ValorNoCeroAssert()
        {
            objeto.IdPais = 1;

            Assert.NotEqual(0, objeto.IdPais);
        }

        [Fact]
        public void validarCodigo_NotEmpty()
        {
            objeto.Codigo = "Nombre";

            Assert.NotEmpty(objeto.Codigo);
        }

        [Fact]
        public void validarCodigo_SubStringAssert()
        {
            objeto.Codigo = "Nombre";

            Assert.Contains("bre", objeto.Codigo);
        }

        [Fact]
        public void validarCodigo_SoloLetrasAssert()
        {
            objeto.Codigo = "Nombre";

            Assert.Matches(@"^[a-zA-Z]+$", objeto.Codigo);
        }

        [Fact]
        public void validarIdCiudad_ValorNoCeroAssert()
        {
            objeto.IdCiudad = 1;

            Assert.NotEqual(0, objeto.IdCiudad);
        }
    }
}
