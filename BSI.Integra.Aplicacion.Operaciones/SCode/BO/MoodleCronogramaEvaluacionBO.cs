using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Operaciones.Repositorio;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Models.AulaVirtual;

namespace BSI.Integra.Aplicacion.Operaciones.SCode.BO
{
    public class MoodleCronogramaEvaluacionBO : BaseBO
    {
        public int IdMatriculaCabecera { get; set; }
        public int IdCursoMoodle { get; set; }
        public int? IdEvaluacionMoodle { get; set; }
        public string NombreEvaluacion { get; set; }
        public DateTime FechaEstimada { get; set; }
        public int Orden { get; set; }
        public int Version { get; set; }
        public int? IdMigracion { get; set; }


        //public integraDBContext _integraDBContext;
        private MoodleCronogramaEvaluacionRepositorio _repCronograma;

        //public MoodleCronogramaEvaluacionBO(integraDBContext integraDbContext)
        //{
        //    _integraDBContext = integraDbContext;
        //    _repCronograma = new MoodleCronogramaEvaluacionRepositorio();
        //}

        public MoodleCronogramaEvaluacionBO()
        {
            _repCronograma = new MoodleCronogramaEvaluacionRepositorio();
        }
        public MoodleCronogramaEvaluacionBO(integraDBContext _integraDBContext)
        {
            _repCronograma = new MoodleCronogramaEvaluacionRepositorio(_integraDBContext);
        }


        public RespuestaWebDTO CongelarCronograma(int IdMatriculaCabecera, string Usuario)
		{
            RespuestaWebDTO respuesta = new RespuestaWebDTO();

            try
            {
                respuesta = _repCronograma.CongelarCronograma(IdMatriculaCabecera);
            }
			catch (Exception e)
            {
                respuesta.Estado = false;
                respuesta.Mensaje = e.Message;
			}

            return respuesta;
        }

        public RespuestaWebDTO CongelarCronogramaMatriculasFaltantes()
        {
            RespuestaWebDTO respuesta = new RespuestaWebDTO();

            try
            {
                List<CronogramaAutoEvaluacionV2DTO> listadoMatriculasPendientes = _repCronograma.ObtenerMatriculasSinCronograma();

                RespuestaWebDTO respuestaTemporal = new RespuestaWebDTO();
                foreach (var item in listadoMatriculasPendientes)
                {
                    if (!_repCronograma.Exist(s => s.IdMatriculaCabecera == Convert.ToInt32(item.IdMatriculaCabecera)))
                        respuestaTemporal = _repCronograma.CongelarCronograma(Convert.ToInt32(item.IdMatriculaCabecera));
                }
                respuesta.Estado = true;
                respuesta.Mensaje = "Consulta Finalizada";
            }
            catch (Exception e)
            {
                respuesta.Estado = false;
                respuesta.Mensaje = e.Message;
            }

            return respuesta;
        }

        public RespuestaWebDTO CongelarCrongrogramaRecuperacionEnAonline(int IdMatriculaCabecera, int idCursoMoodle, int idUsuarioMoodle, string Usuario)
        {
            RespuestaWebDTO respuesta = new RespuestaWebDTO();

            try
            {
                respuesta = _repCronograma.CongelarCrongrogramaRecuperacionEnAonline(IdMatriculaCabecera, idCursoMoodle, idUsuarioMoodle, Usuario);
            }
            catch (Exception e)
            {
                respuesta.Estado = false;
                respuesta.Mensaje = e.Message;
            }

            return respuesta;
        }

        public RespuestaWebDTO ReprogramarCronograma(int IdMatriculaCabecera, int idEvaluacionMoodle, int diasRecorrer, bool recorreCronograma, string Usuario)
        {
            RespuestaWebDTO respuesta = new RespuestaWebDTO();

            try
            {
                bool existeCronograma = _repCronograma.Exist(w => w.IdMatriculaCabecera == IdMatriculaCabecera);
                if (!existeCronograma)
                {
                    RespuestaWebDTO respuestaCreacionCronograma = _repCronograma.CongelarCronograma(IdMatriculaCabecera);
                    if (respuestaCreacionCronograma.Estado)
                    {
                        respuesta = _repCronograma.ReprogramarCronograma(IdMatriculaCabecera, idEvaluacionMoodle, diasRecorrer, recorreCronograma);
                    }
                    else
                    {
                        respuesta.Estado = false;
                        respuesta.Mensaje = "No existe o no se puede generar el cronograma.";
                    }
                }
                else
                {
                    respuesta = _repCronograma.ReprogramarCronograma(IdMatriculaCabecera, idEvaluacionMoodle, diasRecorrer, recorreCronograma);
                }
            }
            catch (Exception e)
            {
                respuesta.Estado = false;
                respuesta.Mensaje = e.Message;
            }

            return respuesta;
        }

        public List<CronogramaAutoEvaluacionV2DTO> ObtenerCronogramaAutoEvaluacionTotal(int IdMatriculaCabecera)
        {
            List<CronogramaAutoEvaluacionV2DTO> respuesta = new List<CronogramaAutoEvaluacionV2DTO>();

            try
            {
                bool existeCronograma = _repCronograma.Exist(w => w.IdMatriculaCabecera == IdMatriculaCabecera);
                if (!existeCronograma)
                {
                    RespuestaWebDTO respuestaCreacionCronograma = _repCronograma.CongelarCronograma(IdMatriculaCabecera);
                }
                respuesta = _repCronograma.ObtenerCronogramaAutoEvaluacion_Total(IdMatriculaCabecera);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            return respuesta;
        }

        public List<CronogramaAutoEvaluacionV2DTO> ObtenerCronogramaAutoEvaluacionUltimaVersion(int IdMatriculaCabecera)
        {
            List<CronogramaAutoEvaluacionV2DTO> respuesta = new List<CronogramaAutoEvaluacionV2DTO>();

            try
            {
                bool existeCronograma = _repCronograma.Exist(w => w.IdMatriculaCabecera == IdMatriculaCabecera);
                if (!existeCronograma)
                {
                    RespuestaWebDTO respuestaCreacionCronograma = _repCronograma.CongelarCronograma(IdMatriculaCabecera);
                }
                respuesta = _repCronograma.ObtenerCronogramaAutoEvaluacion_UltimaVersion(IdMatriculaCabecera);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            return respuesta;
        }
        public List<CronogramalistaCursosOonlineV2PromedioDTO> ObtenerCronogramaAutoEvaluacionUltimaVersionPromedio(int IdMatriculaCabecera)
        {
            List<CronogramalistaCursosOonlineV2PromedioDTO> respuesta = new List<CronogramalistaCursosOonlineV2PromedioDTO>();

            try
            {
                bool existeCronograma = _repCronograma.Exist(w => w.IdMatriculaCabecera == IdMatriculaCabecera);
                if (!existeCronograma)
                {
                    RespuestaWebDTO respuestaCreacionCronograma = _repCronograma.CongelarCronograma(IdMatriculaCabecera);
                }
                respuesta = _repCronograma.ObtenerCronogramaAutoEvaluacion_UltimaVersionPromedio(IdMatriculaCabecera);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            return respuesta;
        }

        public List<CronogramaAutoEvaluacionV2DTO> ObtenerCronogramaAutoEvaluacionUltimaVersionPorCurso(int IdMatriculaCabecera, int IdCursoMoodle)
        {
            List<CronogramaAutoEvaluacionV2DTO> respuesta = new List<CronogramaAutoEvaluacionV2DTO>();

            try
            {
                bool existeCronograma = _repCronograma.Exist(w => w.IdMatriculaCabecera == IdMatriculaCabecera);
                if (!existeCronograma)
                {
                    RespuestaWebDTO respuestaCreacionCronograma = _repCronograma.CongelarCronograma(IdMatriculaCabecera);
                }
                respuesta = _repCronograma.ObtenerCronogramaAutoEvaluacion_UltimaVersionPorCurso(IdMatriculaCabecera, IdCursoMoodle);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            return respuesta;
        }

        public List<CronogramaAutoEvaluacionV2DTO> ObtenerCronogramaAutoEvaluacionVersionOriginal(int IdMatriculaCabecera)
        {
            List<CronogramaAutoEvaluacionV2DTO> respuesta = new List<CronogramaAutoEvaluacionV2DTO>();

            try
            {
                bool existeCronograma = _repCronograma.Exist(w => w.IdMatriculaCabecera == IdMatriculaCabecera);
                if (!existeCronograma)
                {
                    RespuestaWebDTO respuestaCreacionCronograma = _repCronograma.CongelarCronograma(IdMatriculaCabecera);
                }
                respuesta = _repCronograma.ObtenerCronogramaAutoEvaluacion_VersionOriginal(IdMatriculaCabecera);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            return respuesta;
        }

        public List<CronogramaAutoEvaluacionV2DTO> ObtenerCronogramaAutoEvaluacionPorVersion(int IdMatriculaCabecera, int Version)
        {
            List<CronogramaAutoEvaluacionV2DTO> respuesta = new List<CronogramaAutoEvaluacionV2DTO>();

            try
            {
                bool existeCronograma = _repCronograma.Exist(w => w.IdMatriculaCabecera == IdMatriculaCabecera);
                if (!existeCronograma)
                {
                    RespuestaWebDTO respuestaCreacionCronograma = _repCronograma.CongelarCronograma(IdMatriculaCabecera);
                }
                respuesta = _repCronograma.ObtenerCronogramaAutoEvaluacion_PorVersion(IdMatriculaCabecera, Version);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            return respuesta;
        }

        public List<VersionCronogramaAutoEvaluacionDTO> ObtenerVersionesCronograma(int idMatriculaCabecera)
        {
            List<VersionCronogramaAutoEvaluacionDTO> respuesta = new List<VersionCronogramaAutoEvaluacionDTO>();

            try
            {
                bool existeCronograma = _repCronograma.Exist(w => w.IdMatriculaCabecera == idMatriculaCabecera);
                if (!existeCronograma)
                {
                    RespuestaWebDTO respuestaCreacionCronograma = _repCronograma.CongelarCronograma(idMatriculaCabecera);
                }
                respuesta = _repCronograma.ObtenerVersionesCronograma(idMatriculaCabecera);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            return respuesta;
        }

        public bool EliminarTodasVersionesCongeladas(int idMatriculaCabecera, string Usuario)
        {
            bool respuesta = false;
            try
            {
                var listado = _repCronograma.GetBy(w => w.IdMatriculaCabecera == idMatriculaCabecera);
                if (listado != null && listado.Count() > 0)
                {
                    respuesta = _repCronograma.Delete(listado.Select(s => s.Id), Usuario);
                }

                respuesta = true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            return respuesta;
        }

        public bool EliminarUltimaVersionCongelada(int idMatriculaCabecera, string Usuario)
        {
            bool respuesta = false;
            try
            {
                if (_repCronograma.Exist(w => w.IdMatriculaCabecera == idMatriculaCabecera))
                {
                    var versionMaxima = _repCronograma.GetBy(w => w.IdMatriculaCabecera == idMatriculaCabecera)
                        .Max(w => w.Version);

                    var listado = _repCronograma.GetBy(w =>
                        w.IdMatriculaCabecera == idMatriculaCabecera && w.Version == versionMaxima);
                    if (listado != null && listado.Count() > 0)
                    {
                        respuesta = _repCronograma.Delete(listado.Select(s => s.Id), Usuario);
                    }
                }

                respuesta = true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            return respuesta;
        }
    }
}
