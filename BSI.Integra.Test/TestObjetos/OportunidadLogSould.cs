using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Aplicacion.Comercial.BO;

namespace BSI.Integra.Test.TestObjetos
{    
    public class OportunidadLogSould
    {
        public readonly TOportunidadLog objeto;

        public OportunidadLogSould()
        {
            objeto = new TOportunidadLog()
            {
                IdCentroCosto=20,
                IdPersonalAsignado=643,
                IdFaseOportunidad=12,
                IdContacto =12,
                IdTipoDato= 1,
                IdOrigen=12
            };
        }


        //IdCentroCosto
        [Fact]
        public void ValidarIdCentroCosto_ValorNoCeroAssert()
        {
            Assert.NotEqual(0, objeto.IdCentroCosto);
        }

        //IdPersonalAsignado
        [Fact]
        public void ValidarIdPersonalAsignado_ValorNoCeroAssert()
        {
            Assert.NotEqual(0, objeto.IdPersonalAsignado);
        }

        //IdFaseOportunidad
        [Fact]
        public void ValidarIdFaseOportunidad_ValorNoCeroAssert()
        {
            Assert.NotEqual(0, objeto.IdFaseOportunidad);
        }

        //IdContacto
        [Fact]
        public void ValidarIdContacto_ValorNoCeroAssert()
        {
            Assert.NotEqual(0, objeto.IdContacto);
        }

        //IdTipoDato
        [Fact]
        public void ValidarIdTipoDato_ValorNoCeroAssert()
        {
            Assert.NotEqual(0, objeto.IdTipoDato);
        }

        //IdOrigen
        [Fact]
        public void ValidarIdOrigen_ValorNoCeroAssert()
        {
            Assert.NotEqual(0, objeto.IdOrigen);
        }


    }
    
}
