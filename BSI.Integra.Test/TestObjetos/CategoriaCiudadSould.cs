using BSI.Integra.Aplicacion.Planificacion.BO;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace BSI.Integra.Test.TestObjetos
{
    public class CategoriaCiudadSould
    {
        public readonly CategoriaCiudadBO objeto;

        public CategoriaCiudadSould()
        {
            objeto = new CategoriaCiudadBO();
        }

        [Fact]
        public void validarIdCategoria_ValorNoCeroAssert()
        {
            objeto.IdCategoriaPrograma = 1;

            Assert.NotEqual(0, objeto.IdCategoriaPrograma);
        }

        [Fact]
        public void validarIdCiudad_ValorNoCeroAssert()
        {
            objeto.IdCiudad = 1;

            Assert.NotEqual(0, objeto.IdCiudad);
        }

        //[Fact]
        //public void validarTroncalCompleto_ValorNoCeroAssert()
        //{
        //    objeto.TroncalCompleto = 1;

        //    Assert.NotEqual(0, objeto.TroncalCompleto);
        //}

        [Fact]
        public void validarIdRegionCiudad_ValorNoCeroAssert()
        {
            objeto.IdRegionCiudad = 1;

            Assert.NotEqual(0, objeto.IdRegionCiudad);
        }
    }
}
