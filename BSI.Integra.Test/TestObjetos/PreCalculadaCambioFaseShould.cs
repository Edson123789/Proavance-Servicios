using BSI.Integra.Aplicacion.Comercial.BO;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace BSI.Integra.Servicios.Controllers
{
    public class PreCalculadaCambioFaseShould
    {
        public readonly PreCalculadaCambioFaseBO objeto;

        public PreCalculadaCambioFaseShould()
        {
            objeto = new PreCalculadaCambioFaseBO()
            {
                IdPersonal = 125,
                Fecha = DateTime.Now,
                IdCentroCosto = 10,
                IdFaseOportunidadOrigen = 5,
                IdFaseOportunidadDestino = 6,
                IdTipoDato = 7,
                IdOrigen = 8,
                IdCategoriaOrigen = 9,
                IdCampania = 15,
                Contador = 3
            };
        }

        //IdPersonal
        [Fact]
        public void validarIdPersonal_ValorNoCeroAssert()
        {
            Assert.NotEqual(0, objeto.IdPersonal);
        }
        [Fact]
        public void ValidarIdPersonal_NotNull()
        {
            Assert.NotNull(objeto.IdPersonal);
        }

        //IdPersonal
        [Fact]
        public void ValidarFecha_NotNull()
        {
            Assert.NotNull(objeto.Fecha);
        }

        //IdCentroCosto
        [Fact]
        public void validarIdCentroCosto_ValorNoCeroAssert()
        {
            Assert.NotEqual(0, objeto.IdCentroCosto);
        }
        [Fact]
        public void ValidarIdCentroCosto_NotNull()
        {
            Assert.NotNull(objeto.IdCentroCosto);
        }

        //IdFaseOportunidadOrigen
        [Fact]
        public void validarIdFaseOportunidadOrigen_ValorNoCeroAssert()
        {
            Assert.NotEqual(0, objeto.IdFaseOportunidadOrigen);
        }
        [Fact]
        public void ValidarIdFaseOportunidadOrigen_NotNull()
        {
            Assert.NotNull(objeto.IdFaseOportunidadOrigen);
        }

        //IdFaseOportunidadDestino
        [Fact]
        public void validarIdFaseOportunidadDestino_ValorNoCeroAssert()
        {
            Assert.NotEqual(0, objeto.IdFaseOportunidadDestino);
        }
        [Fact]
        public void ValidarIdFaseOportunidadDestino_NotNull()
        {
            Assert.NotNull(objeto.IdFaseOportunidadDestino);
        }

        //IdTipoDato
        [Fact]
        public void validarIdTipoDato_ValorNoCeroAssert()
        {
            Assert.NotEqual(0, objeto.IdTipoDato);
        }
        [Fact]
        public void ValidarIdTipoDato_NotNull()
        {
            Assert.NotNull(objeto.IdTipoDato);
        }

        //IdOrigen
        [Fact]
        public void validarIdOrigen_ValorNoCeroAssert()
        {
            Assert.NotEqual(0, objeto.IdOrigen);
        }
        [Fact]
        public void ValidarIdOrigen_NotNull()
        {
            Assert.NotNull(objeto.IdOrigen);
        }

        //IdCategoriaOrigen
        [Fact]
        public void validarIdCategoriaOrigen_ValorNoCeroAssert()
        {
            Assert.NotEqual(0, objeto.IdCategoriaOrigen);
        }
        [Fact]
        public void ValidarIIdCategoriaOrigen_NotNull()
        {
            Assert.NotNull(objeto.IdCategoriaOrigen);
        }

        //IdTipoDato
        [Fact]
        public void validarIdCampania_ValorNoCeroAssert()
        {
            Assert.NotEqual(0, objeto.IdCampania);
        }
        [Fact]
        public void ValidarIdCampania_NotNull()
        {
            Assert.NotNull(objeto.IdCampania);
        }

        //Contador
        [Fact]
        public void ValidarContador_NotNull()
        {
            Assert.NotNull(objeto.Contador);
        }
    }
}
