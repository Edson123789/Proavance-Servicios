using BSI.Integra.Aplicacion.Comercial.BO;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace BSI.Integra.Test.TestObjetos
{
    public class TasaConversionHistoricaBetaShould
    {
        public readonly TasaConversionHistoricaBetaBO objeto;

        public TasaConversionHistoricaBetaShould()
        {
            objeto = new TasaConversionHistoricaBetaBO()
            {
                Fecha = DateTime.Now,
                Tipo = "Presencial",
                IdCodigoPais = 51,
                FechaNacimiento = DateTime.Now,
                Ciudad = "AREQUIPA",
                ProbabilidadDesc = "Sin Probabilidad",
                IdPersonal = 481,
                IdCoordinador = 34
            };
        }

        //Fecha
        [Fact]
        public void validarFecha_NotNull()
        {
            Assert.NotNull(objeto.Fecha);
        }
        //Tipo
        [Fact]
        public void validarTipo_NotNull()
        {
            Assert.NotNull(objeto.Tipo);
        }

        [Fact]
        public void validarTipo_NotEmpty()
        {
            Assert.NotEmpty(objeto.Tipo);
        }
        //IdCodigoPais
        [Fact]
        public void validarIdCodigoPais_NotNull()
        {
            Assert.NotNull(objeto.IdCodigoPais);
        }

        //Ciudad
        [Fact]
        public void validarCiudad_NotNull()
        {
            Assert.NotNull(objeto.Ciudad);
        }

        [Fact]
        public void validarCiudad_NotEmpty()
        {
            Assert.NotEmpty(objeto.Ciudad);
        }

        //ProbabilidadDesc
        [Fact]
        public void validarProbabilidadDesc_NotNull()
        {
            Assert.NotNull(objeto.ProbabilidadDesc);
        }

        [Fact]
        public void validarProbabilidadDesc_NotEmpty()
        {
            Assert.NotEmpty(objeto.ProbabilidadDesc);
        }

        //IdPersonal
        [Fact]
        public void validarIdPersonal_NotNull()
        {
            Assert.NotNull(objeto.IdPersonal);
        }

        //IdCoordinador
        [Fact]
        public void validarIdCoordinador_NotNull()
        {
            Assert.NotNull(objeto.IdCoordinador);
        }

    }
}
