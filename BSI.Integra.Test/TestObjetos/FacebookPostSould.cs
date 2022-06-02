using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using BSI.Integra.Aplicacion.Comercial.BO;

namespace BSI.Integra.Test.TestObjetos
{
    public class FacebookPostSould
    {
        public readonly FacebookPostBO objeto;

        public FacebookPostSould()
        {
            objeto = new FacebookPostBO()
            {
                Link = "Link",
                PermalinkUrl = "PermalinkUrl",
                IdPostFacebook = "1231231",
                IdPgeneral=643,
                ConjuntoAnuncioIdFacebook="1231231231",
                IdAnuncioFacebook="364565646745"
            };
        }
        //Link
        [Fact]
        public void validarLink_NotEmpty()
        {
            Assert.NotEmpty(objeto.Link);
        }

        [Fact]
        public void validarLink_SubStringAssert()
        {
            Assert.Contains("ink", objeto.Link);
        }

        //PermalinkUrl
        [Fact]
        public void validarPermalinkUrl_NotEmpty()
        {
            Assert.NotEmpty(objeto.PermalinkUrl);
        }

        [Fact]
        public void validarPermalinkUrl_SubStringAssert()
        {
            Assert.Contains("link", objeto.PermalinkUrl);
        }

        //IdPostFacebook
        [Fact]
        public void validarIdPostFacebook_NotEmpty()
        {
            Assert.NotEmpty(objeto.IdPostFacebook);
        }

        [Fact]
        public void validarIdPostFacebook_SubStringAssert()
        {
            Assert.Contains("123", objeto.IdPostFacebook);
        }

        //ConjuntoAnuncioIdFacebook
        [Fact]
        public void validarConjuntoAnuncioIdFacebook_NotEmpty()
        {
            Assert.NotEmpty(objeto.ConjuntoAnuncioIdFacebook);
        }

        [Fact]
        public void validarConjuntoAnuncioIdFacebook_SubStringAssert()
        {
            Assert.Contains("123", objeto.ConjuntoAnuncioIdFacebook);
        }

        //IdAnuncioFacebook
        [Fact]
        public void validarIdAnuncioFacebook_NotEmpty()
        {
            Assert.NotEmpty(objeto.IdAnuncioFacebook);
        }

        [Fact]
        public void validarIdAnuncioFacebook_SubStringAssert()
        {
            Assert.Contains("364", objeto.IdAnuncioFacebook);
        }


        //IdPgeneral
        [Fact]
        public void ValidarIdPgeneral_ValorNoCeroAssert()
        {
            Assert.NotEqual(0, objeto.IdPgeneral);
        }
    }
}
