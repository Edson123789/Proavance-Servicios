using BSI.Integra.Aplicacion.Planificacion.BO;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace BSI.Integra.Test.TestObjetos
{
    public class MontoPagoSould
    {
        public readonly MontoPagoBO objeto;

        public MontoPagoSould()
        {
            objeto = new MontoPagoBO();
        }

        [Fact]
        public void validarPrecioLetras_NotEmpty()
        {
            objeto.PrecioLetras = "Nombre";

            Assert.NotEmpty(objeto.PrecioLetras);
        }

        [Fact]
        public void validarPrecioLetras_SubStringAssert()
        {
            objeto.PrecioLetras = "Nombre";

            Assert.Contains("bre", objeto.PrecioLetras);
        }

        [Fact]
        public void validarPrecioLetras_SoloLetrasAssert()
        {
            objeto.PrecioLetras = "Nombre";

            Assert.Matches(@"^[a-zA-Z]+$", objeto.PrecioLetras);
        }

        [Fact]
        public void validarVencimiento_NotEmpty()
        {
            objeto.Vencimiento = "Nombre";

            Assert.NotEmpty(objeto.Vencimiento);
        }

        [Fact]
        public void validarVencimiento_SubStringAssert()
        {
            objeto.Vencimiento = "Nombre";

            Assert.Contains("bre", objeto.Vencimiento);
        }

        [Fact]
        public void validarVencimiento_SoloLetrasAssert()
        {
            objeto.Vencimiento = "Nombre";

            Assert.Matches(@"^[a-zA-Z]+$", objeto.Vencimiento);
        }

        [Fact]
        public void validarPrimeraCuota_NotEmpty()
        {
            objeto.PrimeraCuota = "Nombre";

            Assert.NotEmpty(objeto.PrimeraCuota);
        }

        [Fact]
        public void validarPrimeraCuota_SubStringAssert()
        {
            objeto.PrimeraCuota = "Nombre";

            Assert.Contains("bre", objeto.PrimeraCuota);
        }

        [Fact]
        public void validarPrimeraCuota_SoloLetrasAssert()
        {
            objeto.PrimeraCuota = "Nombre";

            Assert.Matches(@"^[a-zA-Z]+$", objeto.PrimeraCuota);
        }
    }
}
