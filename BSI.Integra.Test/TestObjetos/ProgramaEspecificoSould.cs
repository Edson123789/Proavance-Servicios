using BSI.Integra.Aplicacion.Planificacion.BO;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace BSI.Integra.Test.TestObjetos
{
    public class ProgramaEspecificoSould
    {
        public readonly PespecificoBO objeto;
        public ProgramaEspecificoSould()
        {
            objeto = new PespecificoBO();
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
        public void validarCodigo_NotEmpty()
        {

            objeto.Codigo = "Codigo";

            Assert.NotEmpty(objeto.Codigo);
        }

        [Fact]
        public void validarCodigo_SubStringAssert()
        {

            objeto.Codigo = "Codigo";

            Assert.Contains("odi", objeto.Codigo);
        }

        [Fact]
        public void validarFrecuencia_NotEmpty()
        {

            objeto.Frecuencia = "Frecuencia";

            Assert.NotEmpty(objeto.Frecuencia);
        }

        [Fact]
        public void validarFrecuencia_SubStringAssert()
        {

            objeto.Frecuencia = "Frecuencia";

            Assert.Contains("cuen", objeto.Frecuencia);
        }

        [Fact]
        public void validarEstadoPrograma_NotEmpty()
        {

            objeto.EstadoP = "EstadoPrograma";

            Assert.NotEmpty(objeto.EstadoP);
        }

        [Fact]
        public void validarEstadoPrograma_SubStringAssert()
        {

            objeto.EstadoP = "EstadoPrograma";

            Assert.Contains("doPr", objeto.EstadoP);
        }
    }
}
