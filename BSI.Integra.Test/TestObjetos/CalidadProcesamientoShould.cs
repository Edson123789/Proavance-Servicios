using BSI.Integra.Aplicacion.Comercial.BO;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace BSI.Integra.Test.TestObjetos
{
    public class CalidadProcesamientoShould
    {
        public readonly CalidadProcesamientoBO objeto;

        public CalidadProcesamientoShould()
        {
            objeto = new CalidadProcesamientoBO()
            {
                IdOportunidad = 125,
                PerfilCamposLlenos = 12,
                PerfilCamposTotal = 10,
                Dni = true,
                PgeneralValidados = 5,
                PgeneralTotal = 6,
                PespecificoValidados = 7,
                PespecificoTotal = 8,
                BeneficiosValidados = 9,
                BeneficiosTotales = 15,
                CompetidoresVerificacion = false,
                ProblemaSeleccionados = 3,
                ProblemaSolucionados = 1
            };
        }

        //IdOportunidad
        [Fact]
        public void validarIdOportunidad_ValorNoCeroAssert()
        {
            Assert.NotEqual(0, objeto.IdOportunidad);
        }
        [Fact]
        public void ValidarIdOportunidad_NotNull()
        {
            Assert.NotNull(objeto.IdOportunidad);
        }

        //PerfilCamposLlenos
        [Fact]
        public void ValidarPerfilCamposLlenos_NotNull()
        {
            Assert.NotNull(objeto.PerfilCamposLlenos);
        }

        //PerfilCamposTotal
        [Fact]
        public void ValidarPerfilCamposTotal_NotNull()
        {
            Assert.NotNull(objeto.PerfilCamposTotal);
        }

        //Dni
        [Fact]
        public void ValidarDni_NotNull()
        {
            Assert.NotNull(objeto.Dni);
        }

        //PgeneralValidados
        [Fact]
        public void ValidarPgeneralValidados_NotNull()
        {
            Assert.NotNull(objeto.PgeneralValidados);
        }

        //PgeneralTotal
        [Fact]
        public void ValidarPgeneralTotal_NotNull()
        {
            Assert.NotNull(objeto.PgeneralTotal);
        }

        //PespecificoValidados
        [Fact]
        public void ValidarPespecificoValidados_NotNull()
        {
            Assert.NotNull(objeto.PespecificoValidados);
        }

        //PespecificoTotal
        [Fact]
        public void ValidarPespecificoTotal_NotNull()
        {
            Assert.NotNull(objeto.PespecificoTotal);
        }

        //BeneficiosValidados
        [Fact]
        public void ValidarBeneficiosValidados_NotNull()
        {
            Assert.NotNull(objeto.BeneficiosValidados);
        }

        //BeneficiosTotales
        [Fact]
        public void ValidarBeneficiosTotales_NotNull()
        {
            Assert.NotNull(objeto.BeneficiosTotales);
        }

        //CompetidoresVerificacion
        [Fact]
        public void ValidarCompetidoresVerificacion_NotNull()
        {
            Assert.NotNull(objeto.CompetidoresVerificacion);
        }

        //ProblemaSeleccionados
        [Fact]
        public void ValidarProblemaSeleccionados_NotNull()
        {
            Assert.NotNull(objeto.ProblemaSeleccionados);
        }

        //ProblemaSolucionados
        [Fact] 
        public void ValidarProblemaSolucionados_NotNull()
        {
            Assert.NotNull(objeto.ProblemaSolucionados);
        }
    }
}
