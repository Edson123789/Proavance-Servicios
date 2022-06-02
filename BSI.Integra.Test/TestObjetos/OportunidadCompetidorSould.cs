using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using BSI.Integra.Aplicacion.Comercial.BO;

namespace BSI.Integra.Test.TestObjetos
{
    public class OportunidadCompetidorSould
    {
        public readonly OportunidadCompetidorBO objeto;

        public OportunidadCompetidorSould()
        {
            objeto = new OportunidadCompetidorBO()
            {
                IdOportunidad = 12,
                OtroBeneficio = "Otro Beneficio"
            };

        }



        //IdOportunidad
        [Fact]
        public void validarIdOportunidad_ValorNoCero()
        {
            Assert.NotEqual(0, objeto.IdOportunidad);
        }

        //OtroBeneficio
        [Fact]
        public void validarOtroBeneficio_NotEmpty()
        {
            Assert.NotEmpty(objeto.OtroBeneficio);
        }

        [Fact]
        public void validadOtroBeneficio_SubStringAssert()
        {
            Assert.Contains("Otro", objeto.OtroBeneficio);
        }
    }
}
