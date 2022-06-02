using BSI.Integra.Aplicacion.Marketing.BO;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace BSI.Integra.Test.TestObjetos
{
    public class PeriodoShould
    {
        public readonly PeriodoBO objeto;

        public PeriodoShould()
        {
            objeto = new PeriodoBO()
            {
                Nombre = "Febrero 2016",
                FechaInicial = "2016-02-01",
                FechaFin = "2016-02-29",
                FechaInicialFinanzas = "2016-02-01",
                FechaFinFinanzas = "2016-02-29"
            };
        }

        //Nombre
        [Fact]
        public void validarNombre_NotNull()
        {
            Assert.NotNull(objeto.Nombre);
        }

        [Fact]
        public void validarNombre_NotEmpty()
        {
            Assert.NotEmpty(objeto.Nombre);
        }

        //FechaInicial
        [Fact]
        public void validarFechaInicial_NotNull()
        {
            Assert.NotNull(objeto.FechaInicial);
        }

        [Fact]
        public void validarFechaInicial_NotEmpty()
        {
            Assert.NotEmpty(objeto.FechaInicial);
        }

        //FechaFin
        [Fact]
        public void validarFechaFin_NotNull()
        {
            Assert.NotNull(objeto.FechaFin);
        }

        [Fact]
        public void validarFechaFin_NotEmpty()
        {
            Assert.NotEmpty(objeto.FechaFin);
        }

        //FechaInicialFinanzas
        [Fact]
        public void validarFechaInicialFinanzas_NotNull()
        {
            Assert.NotNull(objeto.FechaInicialFinanzas);
        }

        [Fact]
        public void validarFechaInicialFinanzas_NotEmpty()
        {
            Assert.NotEmpty(objeto.FechaInicialFinanzas);
        }

        //FechaFinFinanzas
        [Fact]
        public void validarFechaFinFinanzas_NotNull()
        {
            Assert.NotNull(objeto.FechaFinFinanzas);
        }

        [Fact]
        public void validarFechaFinFinanzas_NotEmpty()
        {
            Assert.NotEmpty(objeto.FechaFinFinanzas);
        }


    }
}