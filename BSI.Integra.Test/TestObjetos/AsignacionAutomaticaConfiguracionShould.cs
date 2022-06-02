using BSI.Integra.Aplicacion.Transversal.BO;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace BSI.Integra.Test.TestObjetos
{
    public class AsignacionAutomaticaConfiguracionShould
    {
        public readonly AsignacionAutomaticaConfiguracionBO objeto;

        public AsignacionAutomaticaConfiguracionShould()
        {
            objeto = new AsignacionAutomaticaConfiguracionBO()
            {
                IdFaseOportunidad = 60,
                IdTipoDato = 12,
                IdOrigen = 12,
                Inclusivo = true,
                Habilitado = false,
            };
        }

        //Id Fase
        [Fact]
        public void validarIdFase_NotNull()
        {
            Assert.NotNull(objeto.IdFaseOportunidad);
        }
        //Id tipo dato
        [Fact]
        public void validarIdTipoDato_NotNull()
        {
            Assert.NotNull(objeto.IdTipoDato);
        }
        //id origen
        [Fact]
        public void validarIdOrigen_NotNull()
        {
            Assert.NotNull(objeto.IdOrigen);
        }
        //inclusivo
        [Fact]
        public void validarInclusivo_NotNull()
        {
            Assert.NotNull(objeto.Inclusivo);
        }
        //habilitado
        [Fact]
        public void validarHabilitado_NotNull()
        {
            Assert.NotNull(objeto.Habilitado);
        }
    }
}
