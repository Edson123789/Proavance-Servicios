using BSI.Integra.Aplicacion.Maestros.BO;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace BSI.Integra.Test.TestObjetos
{
    public class PaisSould
    {
        public readonly PaisBO objeto;

        public PaisSould()
        {
            objeto = new PaisBO();
        }

        [Fact]
        public void validarCodigoPais_ValorNoCeroAssert()
        {
            objeto.CodigoPais = 1;

            Assert.NotEqual(0,objeto.CodigoPais);
        }

        [Fact]
        public void validarNombrePais_NotEmpty()
        {
            objeto.NombrePais = "Nombre Pais";

            Assert.NotEmpty(objeto.NombrePais);
        }

        [Fact]
        public void validarNombrePais_SubStringAssert()
        {
            objeto.NombrePais = "Nombre";

            Assert.Contains("bre", objeto.NombrePais);
        }

        [Fact]
        public void validarNombrePais_SoloLetrasAssert()
        {
            objeto.NombrePais = "Nombre";

            Assert.Matches(@"^[a-zA-Z]+$", objeto.NombrePais);
        }


    }
}
