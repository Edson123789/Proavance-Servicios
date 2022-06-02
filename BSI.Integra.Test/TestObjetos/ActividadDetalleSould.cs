using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using BSI.Integra.Aplicacion.Transversal.BO;

namespace BSI.Integra.Test.TestObjetos
{
    public class ActividadDetalleShould
    {
        public readonly ActividadDetalleBO objeto;

        public ActividadDetalleShould()
        {
            objeto = new ActividadDetalleBO()
            {
                IdActividadCabecera = 12,
                IdEstadoActividadDetalle = 13,
                IdOportunidad = 12,
                IdAlumno = 123
            };

        }

        //IdActividadCabecera
        [Fact]
        public void validarIdActividadCabecera_ValorNoCero()
        {
            Assert.NotEqual(0, objeto.IdActividadCabecera);
        }

        //IdEstadoActividadDetalle
        [Fact]
        public void validarIdEstadoActividadDetalle_ValorNoCero()
        {
            Assert.NotEqual(0, objeto.IdEstadoActividadDetalle);
        }

        //IdOportunidad
        [Fact]
        public void validarIdOportunidad_ValorNoCero()
        {
            Assert.NotEqual(0, objeto.IdOportunidad);
        }

        //IdAlumno
        [Fact]
        public void validarIdAlumno_ValorNoCero()
        {
            Assert.NotEqual(0, objeto.IdAlumno);
        }        
    }
}
