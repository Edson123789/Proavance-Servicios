using BSI.Integra.Aplicacion.Marketing.BO;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace BSI.Integra.Test.TestObjetos
{
    public class ConfiguraionBICSould
    {
        public readonly ConfiguracionBicBO objeto;

        public ConfiguraionBICSould()
        {
            objeto = new ConfiguracionBicBO()
            {
                Dias = 5,
                Llamadas = 5
            };
        }

        //Dias
        [Fact]
        public void ValidarDias_ValorNoCeroAssert()
        {
            Assert.NotEqual(0, objeto.Dias);
        }

        //Llamadas
        [Fact]
        public void ValidarLlamadas_ValorNoCeroAssert()
        {
            Assert.NotEqual(0, objeto.Llamadas);
        }

        //Aplica
        [Fact]
        public void validarAplica_EsVerdaderoAssert()
        {
            objeto.Aplica = true;

            Assert.True(objeto.Aplica, "El Valor debe ser Verdadero");
        }

        [Fact]
        public void validarAplica_NoVerdaderoAssert()
        {
            objeto.Aplica = false;

            Assert.False(objeto.Aplica, "El Valor debe ser Falso");
        }

    }
}
