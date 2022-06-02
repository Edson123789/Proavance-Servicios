using BSI.Integra.Aplicacion.Maestros.BO;
using BSI.Integra.Servicios.Controllers;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace BSI.Integra.Test.TestObjetos
{
    public class AreaTrabajoShould
    {
        public readonly AreaTrabajoBO objeto;
        AreaTrabajoController _controlador;
        ValidadorAreaTrabajoDTO _validaObjeto;

        public AreaTrabajoShould()
        {
            objeto = new AreaTrabajoBO();
            _validaObjeto = new ValidadorAreaTrabajoDTO();

        }

        [Fact]
        public void validarNombre_NotEmpty()
        {

            objeto.Nombre = "Nombre";

            Assert.NotEmpty(objeto.Nombre);
        }

        [Fact]
        public void validarNombre_IgnoreCaseAssert()
        {

            objeto.Nombre = "Nombre";

            Assert.Equal("Nombre", objeto.Nombre, ignoreCase: true);
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
    }
}
