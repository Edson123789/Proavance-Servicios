using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using BSI.Integra.Aplicacion.Comercial.BO;

namespace BSI.Integra.Test.TestObjetos
{
    public class OportunidadBeneficioSould
    {
        public readonly OportunidadBeneficioBO objeto;

        public OportunidadBeneficioSould()
        {
            objeto = new OportunidadBeneficioBO()
            {
                IdOportunidadCompetidor = 12,
                IdBeneficio = 10
            };

        }
        //IdOportunidadCompetidor
        [Fact]
        public  void ValidarIdOportunidad_ValorNoCero()
        {
            Assert.NotEqual(0, objeto.IdOportunidadCompetidor);
        }

        //IdBeneficio
        [Fact]
        public void ValidarIdBeneficio_ValorNoCero()
        {
            Assert.NotEqual(0, objeto.IdOportunidadCompetidor);
        }
    }
}
