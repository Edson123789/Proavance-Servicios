using BSI.Integra.Aplicacion.Planificacion.BO;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace BSI.Integra.Test.TestObjetos
{
    public class ProgramaGeneralSould
    {
        public readonly PgeneralBO objeto;
        public ProgramaGeneralSould()
        {
            objeto = new PgeneralBO();
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
        public void validarTitulo_NotEmpty()
        {

            objeto.PgTitulo = "Titulo";

            Assert.NotEmpty(objeto.PgTitulo);
        }

        [Fact]
        public void validarTitulo_SubStringAssert()
        {

            objeto.PgTitulo = "Titulo";

            Assert.Contains("itu", objeto.PgTitulo);
        }

        [Fact]
        public void validarTitulo_SoloLetrasAssert()
        {

            objeto.PgTitulo = "Titulo";

            Assert.Matches(@"^[a-zA-Z]+$", objeto.PgTitulo);
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

            Assert.Contains("di", objeto.Codigo);
        }

        [Fact]
        public void validarCodigo_SoloLetrasAssert()
        {

            objeto.Codigo = "Nombre";

            Assert.Matches(@"^[a-zA-Z]+$", objeto.Codigo);
        }

        [Fact]
        public void validarNombreCorto_NotEmpty()
        {

            objeto.NombreCorto = "NombreCorto";

            Assert.NotEmpty(objeto.NombreCorto);
        }

        [Fact]
        public void validarNombreCorto_SubStringAssert()
        {

            objeto.NombreCorto = "NombreCorto";

            Assert.Contains("breC", objeto.NombreCorto);
        }

        [Fact]
        public void validarNombreCorto_SoloLetrasAssert()
        {

            objeto.NombreCorto = "Nombre";

            Assert.Matches(@"^[a-zA-Z]+$", objeto.NombreCorto);
        }

        [Fact]
        public void validarIdPGeneral_NotEqualsAssert()
        {

            objeto.IdPgeneral = 2;
            Assert.NotEqual(0, objeto.IdPgeneral);
        }

        [Fact]
        public void validarIdBusqueda_NotEqualsAssert()
        {

            objeto.IdBusqueda = 1;
            Assert.NotEqual(0, objeto.IdBusqueda);
        }

        [Fact]
        public void validarIdArea_NotEqualsAssert()
        {

            objeto.IdArea = 1;
            Assert.NotEqual(0, objeto.IdArea);
        }

        [Fact]
        public void validarIdSubArea_NotEqualsAssert()
        {

            objeto.IdSubArea = 1;
            Assert.NotEqual(0, objeto.IdSubArea);
        }

        //[Fact]
        //public void validarEstadoWeb_EsVerdaderoAssert()
        //{

        //    objeto.PwEstado = true;

        //    Assert.True(objeto.EstadoWeb, "El Valor debe ser Verdadero");
        //}

        //[Fact]
        //public void validarEstadoWeb_NoVerdaderoAssert()
        //{
        //    objeto.EstadoWeb = false;

        //    Assert.False(objeto.EstadoWeb, "El Valor debe ser Falso");
        //}
        //[Fact]
        //public void validarBSPlayMostrar_EsVerdaderoAssert()
        //{

        //    objeto.PwMostrarBsplay = true;

        //    Assert.True(objeto.PwMostrarBsplay, "El Valor debe ser Verdadero");
        //}

        //[Fact]
        //public void validarBSPlayMostrar_NoVerdaderoAssert()
        //{
        //    objeto.PwMostrarBsplay = false;

        //    Assert.False(objeto.PwMostrarBsplay, "El Valor debe ser Falso");
        //}
    }
}
