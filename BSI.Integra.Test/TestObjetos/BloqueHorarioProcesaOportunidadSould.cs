using BSI.Integra.Aplicacion.Transversal.BO;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace BSI.Integra.Test.TestObjetos
{
    public class BloqueHorarioProcesaOportunidadSould
    {
        public readonly BloqueHorarioProcesaOportunidadBO objeto;

        public BloqueHorarioProcesaOportunidadSould()
        {
            objeto = new BloqueHorarioProcesaOportunidadBO()
            {
                Descripcion = "Descripcion",
                //ProbabilidadOportunidad = new List<string>() { };
            };
        }

        //Descripcion
        [Fact]
        public void validarDescripcion_NotEmpty()
        {
            Assert.NotEmpty(objeto.Descripcion);
        }

        [Fact]
        public void validarDescripcion_SubStringAssert()
        {
            Assert.Contains("Des", objeto.Descripcion);
        }

        //ProbabilidadOportunidad
        [Fact]
        public void validarProbabilidadOportunidad_NotEmpty()
        {
            Assert.NotEmpty(objeto.ProbabilidadOportunidad);
        }

        [Fact]
        public void validarProbabilidadOportunidad_SubStringAssert()
        {
            Assert.Contains("Opor", objeto.ProbabilidadOportunidad);
        }
        //Activo
        [Fact]
        public void validarActivo_EsVerdaderoAssert()
        {
            objeto.Activo = true;

            Assert.True(objeto.Activo, "El Valor debe ser Verdadero");
        }

        [Fact]
        public void validarActivo_NoVerdaderoAssert()
        {
            objeto.Activo = false;

            Assert.False(objeto.Activo, "El Valor debe ser Falso");
        }
    }
}
