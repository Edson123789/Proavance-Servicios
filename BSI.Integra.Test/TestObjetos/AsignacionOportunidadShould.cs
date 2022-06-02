using BSI.Integra.Aplicacion.Marketing.BO;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace BSI.Integra.Test.TestObjetos
{
    public class AsignacionOportunidadShould
    {
        public readonly AsignacionOportunidadBO objeto;

        public AsignacionOportunidadShould()
        {
            objeto = new AsignacionOportunidadBO()
            {
                IdOportunidad = 60,
                IdPersonal = 12,
                IdCentroCosto = 122,
                IdAlumno = 244,
                FechaAsignacion = DateTime.Now,
                IdTipoDato = 12,
                IdFaseOportunidad = 12
            };
        }

        //Id Oportunidad
        [Fact]
        public void validarIdOportunidad_NotNull()
        {
            Assert.NotNull(objeto.IdOportunidad);
        }
        //IdAsesor
        [Fact]
        public void validarIdAsesor_NotNull()
        {
            Assert.NotNull(objeto.IdPersonal);
        }
        //IdCentroCosto
        [Fact]
        public void validarIdCentroCosto_NotNull()
        {
            Assert.NotNull(objeto.IdCentroCosto);
        }
        //IdAlumno
        [Fact]
        public void validarIdAlumno_NotNull()
        {
            Assert.NotNull(objeto.IdAlumno);
        }
        //FechaAsignacion
        [Fact]
        public void validarFechaAsignacion_NotNull()
        {
            Assert.NotNull(objeto.FechaAsignacion);
        }
        //IdTipoDato
        [Fact]
        public void validarIdTipoDato_NotNull()
        {
            Assert.NotNull(objeto.IdTipoDato);
        }
        //IdFaseOportunidad
        [Fact]
        public void validarIdFaseOportunidad_NotNull()
        {
            Assert.NotNull(objeto.IdFaseOportunidad);
        }
    }
}
