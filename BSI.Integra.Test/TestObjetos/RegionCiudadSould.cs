using BSI.Integra.Aplicacion.Maestros.BO;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace BSI.Integra.Test.TestObjetos
{
    public class RegionCiudadSould
    {
        public readonly RegionCiudadBO objeto;
        
        public RegionCiudadSould()
        {
            objeto = new RegionCiudadBO();
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
        public void validarNombre_SoloLetrasAssert()
        {
            objeto.Nombre = "Nombre";

            Assert.Matches(@"^[a-zA-Z]+$", objeto.Nombre);
        }

        [Fact]
        public void validarDenominacionBs_NotEmpty()
        {
            objeto.DenominacionBs = "Nombre de Envio";

            Assert.NotEmpty(objeto.DenominacionBs);
        }

        [Fact]
        public void validarDenominacionBs_SubStringAssert()
        {
            objeto.DenominacionBs = "Nombre";

            Assert.Contains("bre", objeto.DenominacionBs);
        }

        [Fact]
        public void validarDenominacionBs_SoloLetrasAssert()
        {
            objeto.DenominacionBs = "Nombre";

            Assert.Matches(@"^[a-zA-Z]+$", objeto.DenominacionBs);
        }

        [Fact]
        public void validarNombreCorto_NotEmpty()
        {
            objeto.NombreCorto = "Nombre de Envio";

            Assert.NotEmpty(objeto.NombreCorto);
        }

        [Fact]
        public void validarNombreCorto_SubStringAssert()
        {
            objeto.NombreCorto = "Nombre";

            Assert.Contains("bre", objeto.NombreCorto);
        }

        [Fact]
        public void validarNombreCorto_SoloLetrasAssert()
        {
            objeto.NombreCorto = "Nombre";

            Assert.Matches(@"^[a-zA-Z]+$", objeto.NombreCorto);
        }



    }
}
