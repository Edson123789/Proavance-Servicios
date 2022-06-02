using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using BSI.Integra.Aplicacion.Comercial.BO;

namespace BSI.Integra.Test.TestObjetos
{
    public class OportunidadTasaConversionHistoricaSould
    {
        public readonly OportunidadTasaConversionHistoricaBO objeto;
        
        public OportunidadTasaConversionHistoricaSould()
        {
            objeto = new OportunidadTasaConversionHistoricaBO()
            {
                IdArea = 12,
                IdSubArea = 13,
                IdPgeneral = 12,
                IdPespecifico = 13,
                NombreContacto = "Nombre",
                IdcategoriaDato = 12,
                ValorCategoriaD = 1,
                IdAformacion = 12,
                ValorAformacion = 1,
                IdCargo = 12,
                ValorCargo = 12,
                IdAtrabajo = 12,
                ValorAtrabajo = 1,
                IdIndustria = 12,
                ValorIndustria = 1,
                IdPais = 12,
                ValorPais = 1,
                SumaModelo = 1,
                Probabilidad = 1,
                ProbabilidaDesc = "ProbabilidaDesc"

            };
        }

        //IdArea
        [Fact]
        public void validarIdArea_ValorNoCero()
        {
            Assert.NotEqual(0, objeto.IdArea);
        }

        //IdSubArea
        [Fact]
        public void validarIdSubArea_ValorNoCero()
        {
            Assert.NotEqual(0, objeto.IdSubArea);
        }

        //IdPgeneral
        [Fact]
        public void validarIdPgeneral_ValorNoCero()
        {
            Assert.NotEqual(0, objeto.IdPgeneral);
        }

        //IdPespecifico
        [Fact]
        public void validarIdPespecifico_ValorNoCero()
        {
            Assert.NotEqual(0, objeto.IdPespecifico);
        }

        //NombreContacto
        [Fact]
        public void validarNombre_NotEmpty()
        {
            Assert.NotEmpty(objeto.NombreContacto);
        }

        [Fact]
        public void validarNombreContacto_SubStringAssert()
        {
            Assert.Contains("omb", objeto.NombreContacto);
        }

        //IdcategoriaDato
        [Fact]
        public void validarIdcategoriaDato_ValorNoCero()
        {
            Assert.NotEqual(0, objeto.IdcategoriaDato);
        }
        //ValorCategoriaD
        [Fact]
        public void validarValorCategoriaD_NotNull()
        {
            Assert.NotNull(objeto.ValorCategoriaD);
        }

        //IdAformacion
        [Fact]
        public void validarIdAformacion_ValorNoCero()
        {
            Assert.NotEqual(0, objeto.IdAformacion);
        }

        //ValorAformacion
        [Fact]
        public void validarValorAformacion_NotNull()
        {
            Assert.NotNull(objeto.ValorAformacion);
        }

        //IdCargo
        [Fact]
        public void validarIdCargo_ValorNoCero()
        {
            Assert.NotEqual(0, objeto.IdCargo);
        }

        //ValorCargo
        [Fact]
        public void validarValorCargo_NotNull()
        {
            Assert.NotNull(objeto.ValorCargo);
        }

        //IdAtrabajo
        [Fact]
        public void validarIdAtrabajo_ValorNoCero()
        {
            Assert.NotEqual(0, objeto.IdAtrabajo);
        }

        //ValorAtrabajo
        [Fact]
        public void validarValorAtrabajo_NotNull()
        {
            Assert.NotNull(objeto.ValorAtrabajo);
        }

        //IdIndustria
        [Fact]
        public void validarIdIndustria_ValorNoCero()
        {
            Assert.NotEqual(0, objeto.IdIndustria);
        }

        //ValorIndustria
        [Fact]
        public void validarValorIndustria_NotNull()
        {
            Assert.NotNull(objeto.ValorIndustria);
        }

        //IdPais
        [Fact]
        public void validarIdPais_ValorNoCero()
        {
            Assert.NotEqual(0, objeto.IdPais);
        }

        //ValorPais
        [Fact]
        public void validarValorPais_NotNull()
        {
            Assert.NotNull(objeto.ValorPais);
        }

        //SumaModelo
        [Fact]
        public void validarSumaModelo_NotNull()
        {
            Assert.NotNull(objeto.SumaModelo);
        }

        //Probabilidad
        [Fact]
        public void validarProbabilidad_ValorNoCero()
        {
            Assert.NotEqual(0, objeto.Probabilidad);
        }

        //ProbabilidaDesc

        [Fact]
        public void validarProbabilidadDesc_NotEmpte()
        {
            Assert.NotEmpty(objeto.ProbabilidaDesc);
        }

        [Fact]
        public void validarProbabilidadDesc_SubStringAssert()
        {
            Assert.Contains("Prob", objeto.ProbabilidaDesc);
        }


    }
}
