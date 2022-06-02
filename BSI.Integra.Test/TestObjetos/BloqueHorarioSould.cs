using BSI.Integra.Aplicacion.Maestros.BO;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace BSI.Integra.Test.TestObjetos
{
    public class BloqueHorarioSould
    {
        public readonly BloqueHorarioBO objeto;

        public BloqueHorarioSould()
        {
            objeto = new BloqueHorarioBO();
        }

        [Fact]
        public void validarNombre_NotEmpty()
        {
            objeto.Nombre = "Nombre";

            Assert.NotEmpty(objeto.Nombre);
        }

        [Fact]
        public void validarNombre_SubStringAssert()
        {
            objeto.Nombre = "Nombre";

            Assert.Contains("bre", objeto.Nombre);
        }

        [Fact]
        public void validarNombre_SoloLetrasAssert()
        {
            objeto.Nombre = "Nombre";

            Assert.Matches(@"^[a-zA-Z]+$", objeto.Nombre);
        }

        [Fact]
        public void validarDescripcion_NotEmpty()
        {
            objeto.Descripcion = "Descripcion";

            Assert.NotEmpty(objeto.Descripcion);
        }

        [Fact]
        public void validarDescripcion_SubStringAssert()
        {
            objeto.Descripcion = "Descripcion";

            Assert.Contains("Des", objeto.Descripcion);
        }

        [Fact]
        public void validarDescripcion_SoloLetrasAssert()
        {
            objeto.Descripcion = "Descripcion";

            Assert.Matches(@"^[a-zA-Z]+$", objeto.Descripcion);
        }
    }
}
