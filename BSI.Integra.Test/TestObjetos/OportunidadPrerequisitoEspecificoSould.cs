using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using BSI.Integra.Aplicacion.Comercial.BO;

namespace BSI.Integra.Test.TestObjetos
{
    public class OportunidadPrerequisitoEspecificoSould
    {
        public readonly OportunidadPrerequisitoEspecificoBO objeto;

        public OportunidadPrerequisitoEspecificoSould()
        {
            objeto = new OportunidadPrerequisitoEspecificoBO()
            {
                IdOportunidadCompetidor = 12,
                IdProgramaGeneralBeneficio = 13
            };

        }



        //IdOportunidadCompetidor
        [Fact]
        public void validarIdOportunidadCompetidor_ValorNoCero()
        {
            Assert.NotEqual(0, objeto.IdOportunidadCompetidor);
        }

        //IdProgramaGeneralBeneficio
        [Fact]
        public void validarIdProgramaGeneralBeneficio_ValorNoCero()
        {
            Assert.NotEqual(0, objeto.IdProgramaGeneralBeneficio);
        }

    }
}
