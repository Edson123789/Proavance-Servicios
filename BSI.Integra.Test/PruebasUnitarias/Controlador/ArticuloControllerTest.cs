using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Servicios.Controllers;
using BSI.Integra.Test.Fixtures;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;


namespace BSI.Integra.Test.PruebasUnitarias.Controlador
{
    [TestFixture]
    public class ArticuloControllerTest 
    {
        private readonly integraDBContext _integraDBContext;
        private readonly TestContextServicios injection;
        public ArticuloControllerTest()
        {
            _integraDBContext = new integraDBContext();
            injection = new TestContextServicios();
        }

        [Test]
        public void ObtenerParametroSeoPorArticuloIdCero()
        {
            //Arrange
            ArticuloController articuloController = new ArticuloController(_integraDBContext);
            int IdArticulo = 0;

            //Act
            //var listaParametroSeo = articuloController.ObtenerParametroSeoPorArticulo(IdArticulo);
            ActionResult listaParametroSeo = articuloController.ObtenerParametroSeoPorArticulo(IdArticulo);

            //Assert
            //var actionResult = Assert.IsType<OkObjectResult>(listaParametroSeo);
            //var respuesta = Assert.IsType<List<ParametroContenidoArticuloDTO>>();
            //Assert.AreEqual(0,listaParametroSeo.ExecuteResult)

            Assert.NotNull(listaParametroSeo);

            OkObjectResult resultado = listaParametroSeo as OkObjectResult;
            Assert.NotNull(resultado);
            Assert.AreEqual(200, resultado.StatusCode);

            List<ParametroContenidoArticuloDTO> resultadoFinal = resultado.Value as List<ParametroContenidoArticuloDTO>;
            Assert.IsEmpty(resultadoFinal);
        }

        [Test]
        public void ObtenerParametroSeoPorArticuloIdExiste()
        {
            //Arrange
            ArticuloController articuloController = new ArticuloController(_integraDBContext);
            int IdArticulo = 3;

            //Act
            ActionResult listaParametroSeo = articuloController.ObtenerParametroSeoPorArticulo(IdArticulo);
            
            //Assert
            Assert.NotNull(listaParametroSeo);

            OkObjectResult resultado = listaParametroSeo as OkObjectResult;
            Assert.NotNull(resultado);
            Assert.AreEqual(200, resultado.StatusCode);

            List<ParametroContenidoArticuloDTO> resultadoFinal = resultado.Value as List<ParametroContenidoArticuloDTO>;
            Assert.True(resultadoFinal.Count > 0);
        }

        [Test]
        public void InsertarConParametrosCorrectos()
        {
            //Arrange
            ArticuloController articuloController = new ArticuloController(_integraDBContext);

            ArticuloInsertarDTO articuloPrueba = new ArticuloInsertarDTO()
            {
                Id = 0,
                IdWeb = 0,
                Nombre = "Prueba Articulo 12",
                Titulo = "Titulo Prueba Articulo 12",
                ImgPortada = "que-es-el-protocolo-de-kioto.png",
                ImgPortadaAlt = "imagen alterna Articulo 12",
                ImgSecundaria = null,
                ImgSecundariaAlt = null,
                Autor = "BSG Institute",
                IdTipoArticulo = 3,
                Contenido = "<p style='text-align:justify;'>El efecto invernadero es el fen&oacute;meno por el cual determinados gases, que son componentes de la atm&oacute;sfera planetaria, retienen parte de la energ&iacute;a que el suelo emite por haber sido calentado por la radiaci&oacute;n solar.</p><p style='text-align:justify; '>De acuerdo con la mayor&iacute;a de la comunidad cient&iacute;fica, el efecto invernadero se est&aacute; viendo acentuado en la Tierra por la emisi&oacute;n de ciertos gases, como el di&oacute;xido de carbono y el metano, debido a la actividad humana. A consecuencia de los gases de efecto invernadero y la variaci&oacute;n de energ&iacute;a retenida en la tierra se produce lo que conocemos como calentamiento global y cambio clim&aacute;tico.</p><p style='text-align:justify; '>Por estas razones se hizo el Protocolo de Kioto, uno de los acuerdos institucionales m&aacute;s importantes en relaci&oacute;n al cambio clim&aacute;tico, que tiene su origen en la convenci&oacute;n Marco de las Naciones Unidas sobre el cambio clim&aacute;tico en 1992.</p><p style='text-align:justify; '>Este protocolo busca reducir las emisiones GEIs de los principales pa&iacute;ses industrializados con el fin de que en el periodo que va de 2008 al 2012, esas emisiones descienden un 1,8% por debajo de las registradas en 1990.</p><h3 style='text-align:justify; '>Contenidos</h3><ul><li>Introducci&oacute;n</li><li>&iquest;Qu&eacute; es el Protocolo de Kioto?</li><li>Antecedentes del Protocolo de Kioto</li><li>Post - Kioto 2012</li><li>Contenido del Protocolo de Kioto</li><li>Mecanismos de Flexibilidad del Protocolo de Kioto</li><li>Pol&iacute;ticas y Medidas</li><li>Conclusi&oacute;n</li></ul>",
                IdArea = 10,
                IdSubArea = 21,
                IdExpositor = 1482,
                IdCategoria = 3,
                UrlWeb = "url web Prueba Articulo",
                UrlDocumento = "https://repositorioweb.blob.core.windows.net/paper-bs/Paper/que-es-el-protocolo-de-kioto.pdf",
                DescripcionGeneral = "<p style='text-align:justify;'>El Protocolo de Kioto es un acuerdo sobre el cambio clim&aacute;tico y busca reducir las emisiones de GEIs de los pa&iacute;ses industrializados. En este whitepaper se desarrollan sus antecedentes, pol&iacute;ticas y prop&oacute;sitos.</p>",
                Usuario = "fvaldez",
                listaArticuloSeo = new List<ArticuloInsertDTO>()
            };

            byte[] bytesContenido = Encoding.Default.GetBytes(articuloPrueba.Contenido);
            articuloPrueba.Contenido = Convert.ToBase64String(bytesContenido);

            byte[] bytesDescripcionGeneral = Encoding.Default.GetBytes(articuloPrueba.DescripcionGeneral);
            articuloPrueba.DescripcionGeneral = Convert.ToBase64String(bytesDescripcionGeneral);

            articuloPrueba.listaArticuloSeo.Add(new ArticuloInsertDTO()
            {
                Id = 0,
                Descripcion = "Articulo Seo 1",
                IdArticulo = 0,
                IdParametroSeo = 1,
            });

            articuloPrueba.listaArticuloSeo.Add(new ArticuloInsertDTO()
            {
                Id = 0,
                Descripcion = "Articulo Seo 2",
                IdArticulo = 0,
                IdParametroSeo = 2,
            });

            articuloPrueba.listaArticuloSeo.Add(new ArticuloInsertDTO()
            {
                Id = 0,
                Descripcion = "Articulo Seo 3",
                IdArticulo = 0,
                IdParametroSeo = 2,
            });

            //Act
            ActionResult articuloInsertado = articuloController.Insertar(articuloPrueba);

            //Assert
            Assert.NotNull(articuloInsertado);

            OkObjectResult resultado = articuloInsertado as OkObjectResult;
            Assert.NotNull(resultado);
            Assert.AreEqual(200, resultado.StatusCode);

            ArticuloBO resultadoFinal = resultado.Value as ArticuloBO;
            Assert.AreNotEqual(0,resultadoFinal.Id);
            
        }

    }
}
