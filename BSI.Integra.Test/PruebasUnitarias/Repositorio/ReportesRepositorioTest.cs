using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Reportes.Comercial.Repositorio;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Test.Fixtures;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Test.PruebasUnitarias.Repositorio
{
    [TestFixture]
    public class ReportesRepositorioTest
    {
        private readonly TestContextServicios injection;
        private readonly integraDBContext _integraDBContext;
        private ReportesRepositorio _repReportes;
        public ReportesRepositorioTest()
        {
            _integraDBContext = new integraDBContext();
            _repReportes = new ReportesRepositorio();
            injection = new TestContextServicios();
        }
        [Test]
        public void ObtenerReporteSeguimientoOperacionesCorrecto()
        {
            SeguimientoFiltroFinalDTO filtro = new SeguimientoFiltroFinalDTO()
            {
                CentroCostos=null,
                Asesores="225",
                FasesOportunidad="5,23,25",
                FechaInicio=DateTime.Now,
                FechaFin= DateTime.Now,
                OpcionFase=0,
                FasesOportunidadOrigen=null,
                FasesOportunidadDestino=null,
                EstadosMatricula=null,
                DocumentoIdentidad=null,
                CodigoMatricula=null,
                ControlFiltroFecha=1
            };
            var reporte = _repReportes.ObtenerReporteSeguimientoOperaciones(filtro);
            Assert.AreNotEqual(0, (int)reporte.Count);
        }
        [Test]
        public void ObtenerReporteSeguimientoOperacionesFechaSinDiferencia()
        {
            SeguimientoFiltroFinalDTO filtro = new SeguimientoFiltroFinalDTO()
            {
                CentroCostos = null,
                Asesores = "225",
                FasesOportunidad = "5,23,25",
                FechaInicio = DateTime.Now,
                FechaFin = DateTime.Now,
                OpcionFase = 0,
                FasesOportunidadOrigen = null,
                FasesOportunidadDestino = null,
                EstadosMatricula = null,
                DocumentoIdentidad = null,
                CodigoMatricula = null,
                ControlFiltroFecha = 0
            };
            var reporte = _repReportes.ObtenerReporteSeguimientoOperaciones(filtro);
            Assert.AreEqual(0, (int)reporte.Count);
        }
    }
}
