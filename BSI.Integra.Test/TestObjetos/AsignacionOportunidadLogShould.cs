using BSI.Integra.Aplicacion.Marketing.BO;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace BSI.Integra.Test.TestObjetos
{
    public class AsignacionOportunidadLogShould
    {
        public readonly AsignacionOportunidadLogBO objeto;

        public AsignacionOportunidadLogShould()
        {
            objeto = new AsignacionOportunidadLogBO()
            {
                IdAsignacionOportunidad = 12,
                IdOportunidad = 12,
                IdPersonalAnterior = 12,
                IdPersonal = 12,
                IdCentroCostoAnt = 12,
                IdCentroCosto = 12,
                IdAlumno=12,
                FechaLog = DateTime.Now,
                IdTipoDato = 12,
                IdFaseOportunidad = 12
            };
        }

        //Id Asignacion Oportunidad
        [Fact]
        public void validarIdAsignacionOportunidad_NotNull()
        {
            Assert.NotNull(objeto.IdAsignacionOportunidad);
        }
        //Id Oportunidad
        [Fact]
        public void validarIdOportunidad_NotNull()
        {
            Assert.NotNull(objeto.IdOportunidad);
        }
        //Id Asesor ant
        [Fact]
        public void validarIdAsesorAnt_NotNull()
        {
            Assert.NotNull(objeto.IdPersonalAnterior);
        }
        //Id Asesor
        [Fact]
        public void validarIdAsesor_NotNull()
        {
            Assert.NotNull(objeto.IdPersonal);
        }
        //Id Centro Costo Ant
        [Fact]
        public void validarIdCentroCostoAnt_NotNull()
        {
            Assert.NotNull(objeto.IdCentroCostoAnt);
        }
        //Id Centro Costo
        [Fact]
        public void validarIdCentroCosto_NotNull()
        {
            Assert.NotNull(objeto.IdCentroCosto);
        }
        //Id Alumno
        [Fact]
        public void validarIdAlumno_NotNull()
        {
            Assert.NotNull(objeto.IdAlumno);
        }
        //FechaLog
        [Fact]
        public void validarFechaLog_NotNull()
        {
            Assert.NotNull(objeto.FechaLog);
        }
        //Id tipo dato
        [Fact]
        public void validarIdTipoDato_NotNull()
        {
            Assert.NotNull(objeto.IdTipoDato);
        }
        //Id Fase Oportunidad
        [Fact]
        public void validarIdFaseOportunidad_NotNull()
        {
            Assert.NotNull(objeto.IdFaseOportunidad);
        }
    }

}
