using System;
using System.Collections.Generic;
using System.Text;
using BSI.Integra.Aplicacion.Comercial.BO;
using Xunit;

namespace BSI.Integra.Test.TestObjetos
{
    public class OcurrenciaSould
    {
        public readonly OcurrenciaBO objeto;

        public OcurrenciaSould()
        {
            objeto = new OcurrenciaBO()
            {
                Nombre = "Nombre",
                IdFaseOportunidad = 12,
                IdActividadCabecera = 10,
                IdPlantillaSpeech = 12,
                IdEstadoOcurrencia = 10,
                
            };
        }

        //Nombre
        [Fact]
        public void validarNombre_NotEmpty()
        {
            Assert.NotEmpty(objeto.Nombre);
        }

        [Fact]
        public void validarNombre_SubStringAssert()
        {
            Assert.Contains("omb", objeto.Nombre);
        }

        //IdFaseOportunidad

        [Fact]
        public void validarIdFaseOportunidad_ValorNoCeroAssert()
        {
            objeto.IdFaseOportunidad = 1;

            Assert.NotEqual(0, objeto.IdFaseOportunidad);
        }

        //IdActividadCabecera
        [Fact]
        public void validarIdActividadCabecera_ValorNoCeroAssert()
        {
            Assert.NotEqual(0, objeto.IdFaseOportunidad);
        }

        //IdPlantillaSpeech
        [Fact]
        public void validarIdPlantillaSpeech_ValorNoCeroAssert()
        {

            Assert.NotEqual(0, objeto.IdFaseOportunidad);
        }

        //IdEstadoOcurrencia
        [Fact]
        public void validarIdEstadoOcurrencia_ValorNoCeroAssert()
        { 
            Assert.NotEqual(0, objeto.IdEstadoOcurrencia);
        }

        //IdOportunidad

        [Fact]
        public void validarIdOportunidad_EsVerdaderoAssert()
        {
            objeto.IdOportunidad = true;

            Assert.True(objeto.IdOportunidad, "El Valor debe ser Verdadero");
        }

        [Fact]
        public void validarIdOportunidad_NoVerdaderoAssert()
        {
            objeto.IdOportunidad = false;

            Assert.False(objeto.IdOportunidad, "El Valor debe ser Falso");
        }


    }
}
