using BSI.Integra.Aplicacion.Comercial.BO;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace BSI.Integra.Test.TestObjetos
{
    public class SolucionClienteByActividadShould
    {
        public readonly SolucionClienteByActividadBO objeto;

        public SolucionClienteByActividadShould()
        {
            objeto = new SolucionClienteByActividadBO()
            {
                IdOportunidad = 125,
                IdActividadDetalle = 12,
                IdCausa = 10,
                IdPersonal = 5,
                Solucionado = true,
                IdProblemaCliente = 6, 
                OtroProblema = "otro"
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

        //IdActividadDetalle
        [Fact]
        public void validarIdActividadDetalle_ValorNoCeroAssert()
        {
            Assert.NotEqual(0, objeto.IdActividadDetalle);
        }
        [Fact]
        public void ValidarIdActividadDetalle_NotNull()
        {
            Assert.NotNull(objeto.IdActividadDetalle);
        }

        //IdCausa
        [Fact]
        public void validarIdCausa_ValorNoCeroAssert()
        {
            Assert.NotEqual(0, objeto.IdCausa);
        }
        [Fact]
        public void ValidarIdCausa_NotNull()
        {
            Assert.NotNull(objeto.IdCausa);
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

        //Solucionado
        [Fact]
        public void ValidarSolucionado_NotNull()
        {
            Assert.NotNull(objeto.Solucionado);
        }

        //IdProblema
        [Fact]
        public void validarIdProblema_ValorNoCeroAssert()
        {
            Assert.NotEqual(0, objeto.IdProblemaCliente);
        }
        [Fact]
        public void ValidarIdProblema_NotNull()
        {
            Assert.NotNull(objeto.IdProblemaCliente);
        }

        //OtroProblema
        [Fact]
        public void ValidarOtroProblema_NotNull()
        {
            Assert.NotNull(objeto.OtroProblema);
        }
    }
}
