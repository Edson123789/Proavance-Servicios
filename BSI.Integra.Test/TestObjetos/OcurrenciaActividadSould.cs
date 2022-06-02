using System;
using System.Collections.Generic;
using System.Text;
using BSI.Integra.Aplicacion.Comercial.BO;
using Xunit;

namespace BSI.Integra.Test.TestObjetos
{
    public class OcurrenciaActividadSould
    {
        public readonly OcurrenciaActividadBO objeto;

        public OcurrenciaActividadSould()
        {
            objeto = new OcurrenciaActividadBO()
            {
                IdOcurrencia = 12,
                IdActividadCabecera = 13,
                IdOcurrenciaActividadPadre = 12
            };

        }

        //IdOcurrencia
        [Fact]
        public void validarIdOcurrencia_ValorNoCero()
        {
            Assert.NotEqual(0, objeto.IdOcurrencia);
        }

        //IdActividadCabecera
        [Fact]
        public void validarIdActividadCabecera_ValorNoCero()
        {
            Assert.NotEqual(0, objeto.IdActividadCabecera);
        }

        //IdOcurrenciaActividadPadre
        [Fact]
        public void validarIdOcurrenciaActividadPadre_ValorNoCero()
        {
            Assert.NotEqual(0, objeto.IdOcurrenciaActividadPadre);
        }

        //NodoPadre
        [Fact]
        public void validarNodoPadre_EsVerdaderoAssert()
        {
            objeto.NodoPadre = true;

            Assert.True(objeto.NodoPadre, "El Valor debe ser Verdadero");
        }

        [Fact]
        public void validarVirtual_NoVerdaderoAssert()
        {
            objeto.NodoPadre = false;

            Assert.False(objeto.NodoPadre, "El Valor debe ser Falso");
        }

    }
}
