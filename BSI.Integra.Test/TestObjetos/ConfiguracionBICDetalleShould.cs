using BSI.Integra.Aplicacion.Marketing.BO;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace BSI.Integra.Test.TestObjetos
{
    public class ConfiguracionBICDetalleShould
    {
        public readonly ConfiguracionBicdetalleBO objeto;

        public ConfiguracionBICDetalleShould()
        {
            objeto = new ConfiguracionBicdetalleBO()
            {
                IdConfiguracionBic = 125,
                IdBloqueHorario = 10
            };
        }

        //IdConfiguracionBic
        [Fact]
        public void validarIdConfiguracionBic_ValorNoCeroAssert()
        {
            Assert.NotEqual(0, objeto.IdConfiguracionBic);
        }
        [Fact]
        public void ValidarIdConfiguracionBic_NotNull()
        {
            Assert.NotNull(objeto.IdConfiguracionBic);
        }

        //IdBloqueHorario
        [Fact]
        public void validarIdBloqueHorario_ValorNoCeroAssert()
        {
            Assert.NotEqual(0, objeto.IdBloqueHorario);
        }
        [Fact]
        public void ValidarIdBloqueHorario_NotNull()
        {
            Assert.NotNull(objeto.IdBloqueHorario);
        }

    }
}
