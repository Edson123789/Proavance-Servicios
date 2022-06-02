using BSI.Integra.Aplicacion.Planificacion.BO;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace BSI.Integra.Test.TestObjetos
{
    public class AreaCapacitacionSould
    {
        public readonly AreaCapacitacionBO objeto;

        public AreaCapacitacionSould()
        {
            objeto = new AreaCapacitacionBO();
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
        public void validarDescripcion_NotEmpty()
        {
            objeto.Descripcion = "Nombre";

            Assert.NotEmpty(objeto.Descripcion);
        }

        [Fact]
        public void validarDescripcion_SubStringAssert()
        {
            objeto.Descripcion = "Nombre";

            Assert.Contains("bre", objeto.Descripcion);
        }

        [Fact]
        public void validarImgPortada_NotEmpty()
        {
            objeto.ImgPortada = "Nombre";

            Assert.NotEmpty(objeto.ImgPortada);
        }

        [Fact]
        public void validarImgPortada_SubStringAssert()
        {
            objeto.ImgPortada = "Nombre";

            Assert.Contains("bre", objeto.ImgPortada);
        }

        [Fact]
        public void validarImgSecundaria_NotEmpty()
        {
            objeto.ImgSecundaria = "Nombre";

            Assert.NotEmpty(objeto.ImgSecundaria);
        }

        [Fact]
        public void validarImgSecundaria_SubStringAssert()
        {
            objeto.ImgSecundaria = "Nombre";

            Assert.Contains("bre", objeto.ImgSecundaria);
        }

        [Fact]
        public void validarImgPortadaAlt_NotEmpty()
        {
            objeto.ImgPortadaAlt = "Nombre";

            Assert.NotEmpty(objeto.ImgPortadaAlt);
        }

        [Fact]
        public void validarImgPortadaAlt_SubStringAssert()
        {
            objeto.ImgPortadaAlt = "Nombre";

            Assert.Contains("bre", objeto.ImgPortadaAlt);
        }

        [Fact]
        public void validarImgSecundariaAlt_NotEmpty()
        {
            objeto.ImgSecundariaAlt = "Nombre";

            Assert.NotEmpty(objeto.ImgSecundariaAlt);
        }

        [Fact]
        public void validarImgSecundariaAlt_SubStringAssert()
        {
            objeto.ImgSecundariaAlt = "Nombre";

            Assert.Contains("bre", objeto.ImgSecundariaAlt);
        }

        [Fact]
        public void validarEsVisibleWeb_EsVerdaderoAssert()
        {
            objeto.EsVisibleWeb = true;

            Assert.True(objeto.EsVisibleWeb, "El Valor debe ser Verdadero");
        }

        [Fact]
        public void validarEsVisibleWeb_NoVerdaderoAssert()
        {
            objeto.EsVisibleWeb = false;

            Assert.False(objeto.EsVisibleWeb, "El Valor debe ser Falso");
        }

        [Fact]
        public void validarIdArea_ValorNoCeroAssert()
        {
            objeto.IdArea = 1;

            Assert.NotEqual(0, objeto.IdArea);
        }

        [Fact]
        public void validarEsWeb_EsVerdaderoAssert()
        {
            objeto.EsWeb = true;

            Assert.True(objeto.EsWeb, "El Valor debe ser Verdadero");
        }

        [Fact]
        public void validarEsWeb_NoVerdaderoAssert()
        {
            objeto.EsWeb = false;

            Assert.False(objeto.EsWeb, "El Valor debe ser Falso");
        }

        [Fact]
        public void validarDescripcionHtml_NotEmpty()
        {
            objeto.DescripcionHtml = "Nombre de Envio";

            Assert.NotEmpty(objeto.DescripcionHtml);
        }

        [Fact]
        public void validarDescripcionHtml_SubStringAssert()
        {
            objeto.DescripcionHtml = "Nombre";

            Assert.Contains("bre", objeto.DescripcionHtml);
        }
        
    }
}
