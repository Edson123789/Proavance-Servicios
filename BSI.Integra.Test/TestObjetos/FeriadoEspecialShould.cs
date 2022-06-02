using BSI.Integra.Aplicacion.Planificacion.BO;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
namespace BSI.Integra.Test.TestObjetos
{
    public class FeriadoEspecialShould
    {
        public readonly FeriadoEspecialBO objeto;

        public FeriadoEspecialShould()
        {
            objeto = new FeriadoEspecialBO()
            {
                Dia = DateTime.Now,
                Motivo = "motivo x",
                IdCiudad = 10
            };
        }

        //Dia
        [Fact]
        public void ValidarDia_NotNull()
        {
            Assert.NotNull(objeto.Dia);
        }

        //Motivo
        [Fact]
        public void validarMotivo_NotNull()
        {
            Assert.NotNull(objeto.Motivo);
        }
        [Fact]
        public void validarMotivo_NotEmpty()
        {
            Assert.NotEmpty(objeto.Motivo);
        }

        //IdCiudad
        [Fact]
        public void validarIdCiudad_ValorNoCeroAssert()
        {
            Assert.NotEqual(0, objeto.IdCiudad);
        }
        [Fact]
        public void ValidarIdCiudad_NotNull()
        {
            Assert.NotNull(objeto.IdCiudad);
        }
    }
}
