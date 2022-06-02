using BSI.Integra.Aplicacion.Planificacion.BO;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace BSI.Integra.Test.TestObjetos
{
    public class FeriadoSould
    {
        public readonly FeriadoBO objeto;

        public FeriadoSould()
        {
            objeto = new FeriadoBO();
        }

        [Fact]
        public void validarMotivo_NotEmpty()
        {
            objeto.Motivo = "Nombre";

            Assert.NotEmpty(objeto.Motivo);
        }

        [Fact]
        public void validarMotivo_SubStringAssert()
        {
            objeto.Motivo = "Nombre";

            Assert.Contains("bre", objeto.Motivo);
        }

        [Fact]
        public void validarMotivo_SoloLetrasAssert()
        {
            objeto.Motivo = "Nombre";

            Assert.Matches(@"^[a-zA-Z]+$", objeto.Motivo);
        }

        [Fact]
        public void validarFrecuencia_ValorNoCeroAssert()
        {
            objeto.Frecuencia = 1;

            Assert.NotEqual(0, objeto.Frecuencia);
        }

        [Fact]
        public void validarIdCiudad_ValorNoCeroAssert()
        {
            objeto.IdCiudad = 1;

            Assert.NotEqual(0, objeto.IdCiudad);
        }
    }
}
