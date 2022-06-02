using BSI.Integra.Aplicacion.Transversal.BO;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace BSI.Integra.Test.TestObjetos
{
    public class ModeloDataMiningShould
    {
        public readonly ModeloDataMiningBO objeto;

        public ModeloDataMiningShould()
        {
            objeto = new ModeloDataMiningBO()
            {
                Nombres = 1,
                Apellidos = 2
            };
        }

        //Nombres
        [Fact]
        public void validarNombres_NotNull()
        {
            Assert.NotNull(objeto.Nombres);
        }

        //Apellidos
        [Fact]
        public void validarApellidos_NotNull()
        {
            Assert.NotNull(objeto.Apellidos);
        }

    }
}
