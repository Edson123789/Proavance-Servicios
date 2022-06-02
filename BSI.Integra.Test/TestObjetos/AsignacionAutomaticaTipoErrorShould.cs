using BSI.Integra.Aplicacion.Transversal.BO;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace BSI.Integra.Test.TestObjetos
{
    public class AsignacionAutomaticaTipoErrorShould
    {
        public readonly AsignacionAutomaticaTipoErrorBO objeto;

        public AsignacionAutomaticaTipoErrorShould()
        {
            objeto = new AsignacionAutomaticaTipoErrorBO()
            {
                Nombre = "Test",
            };
        }

        //Nombre
        [Fact]
        public void validarNombre_NotEmpty()
        {
            Assert.NotEmpty(objeto.Nombre);
        }

        [Fact]
        public void validarNombre_NotNull()
        {
            Assert.NotNull(objeto.Nombre);
        }
    }
}
