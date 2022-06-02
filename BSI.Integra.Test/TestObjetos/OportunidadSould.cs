using BSI.Integra.Aplicacion.Comercial.BO;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;


namespace BSI.Integra.Test.TestObjetos
{
    public class OportunidadSould
    {
        public readonly OportunidadBO objeto;

        public OportunidadSould()
        {

            objeto = new OportunidadBO()
            {
                IdCentroCosto = 2,
                IdPersonalAsignado = 643,
                IdFaseOportunidad = 3,
                IdTipoDato = 4,
                IdAlumno = 816,
                IdOrigen = 5
            };

        }

        //IdCentroCosto
        [Fact]
        public void validarIdCentroCosto_ValorNoCeroAssert()
        {
            Assert.NotEqual(0, objeto.IdCentroCosto);
        }

        //IdPersonalAsignado
        [Fact]
        public void validarIdPersonalAsignado_ValorNoCeroAssert()
        {
            Assert.NotEqual(0, objeto.IdPersonalAsignado);
        }

        //IdFaseOportunidad
        [Fact]
        public void validarIdFaseOportunidad_ValorNoCeroAssert()
        {
            Assert.NotEqual(0, objeto.IdFaseOportunidad);
        }

        //IdTipoDato
        [Fact]
        public void validarIdTipoDato_ValorNoCeroAssert()
        {
            Assert.NotEqual(0, objeto.IdTipoDato);
        }



        //IdAlumno
        [Fact]
        public void validarIdAlumno_ValorNoCeroAssert()
        {
            Assert.NotEqual(0, objeto.IdAlumno);
        }

        //IdOrigen

        [Fact]
        public void validarIdOrigen_ValorNoCeroAssert()
        {
            Assert.NotEqual(0, objeto.IdOrigen);
        }
    }
}
