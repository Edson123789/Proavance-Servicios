using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Operaciones.Repositorio;
using System;
using System.Collections.Generic;
using System.Text;
using static BSI.Integra.Aplicacion.Base.BO.Enum;
using Enum = BSI.Integra.Aplicacion.Base.BO.Enum;

namespace BSI.Integra.Aplicacion.Operaciones.BO
{
    public class ReporteBO
    {
        protected ExcelBO Excel { get; private set ; }
        private RaAlumnoRepositorio _repRaAlumno { get; }

        public ReporteBO() {
            Excel = new ExcelBO();
            _repRaAlumno = new RaAlumnoRepositorio();
        }
        public byte[] ObtenerFormatoVerificacionUltimosDetalles() {
            try
            {
               return Excel.ObtenerFormatoVerificacionUltimosDetalles();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public byte[] ObtenerEmbudo(EmbudoResumentoDTO datos)
        {
            try
            {
                return Excel.ObtenerEmbudo(datos);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public byte[] ObtenerContactosCampaniaMalingDetalle(List<ContactoCampaniaMailingDTO> datos, TipoArchivo tipoArchivo)
        {
            try
            {
                return Excel.ObtenerContactosCampaniaMalingDetalle(datos, tipoArchivo);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public byte[] ObtenerReporteDetallaPorAlumnoAsistenciaOnlineExcel(int idAlumno, ref List<ReporteAsistenciaOnlineDTO>  listadoSesionesAsistencia)
        {
            try
            {
                byte[] reporte = null;
                List<ReporteAsistenciaOnlineDTO> listadoReporte = _repRaAlumno.ListadoSesionesAlumnoCurso(idAlumno);
                listadoSesionesAsistencia = listadoReporte;
                reporte = Excel.ReporteDetalladoPorAlumnoAsistenciaOnline(listadoReporte);
                return reporte;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
          
        }
    }
}
