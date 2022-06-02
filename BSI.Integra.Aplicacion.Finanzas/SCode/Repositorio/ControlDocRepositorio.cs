using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Finanzas.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Finanzas.Repositorio
{
    public class ControlDocRepositorio : BaseRepository<TControlDoc, ControlDocBO>
    {
        #region Metodos Base
        public ControlDocRepositorio() : base()
        {
        }
        public ControlDocRepositorio(integraDBContext contexto) : base(contexto)
        {
        }

        public bool Insert(ControlDocBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TControlDoc entidad = MapeoEntidad(objetoBO);

                bool resultado = base.Insert(entidad);
                if (resultado)
                    AsignacionId(entidad, objetoBO);

                return resultado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(IEnumerable<ControlDocBO> listadoBO)
        {
            try
            {
                foreach (var objetoBO in listadoBO)
                {
                    bool resultado = Insert(objetoBO);
                    if (resultado == false)
                        return false;
                }

                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool Update(ControlDocBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TControlDoc entidad = MapeoEntidad(objetoBO);

                bool resultado = base.Update(entidad);
                if (resultado)
                    AsignacionId(entidad, objetoBO);

                return resultado;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool Update(IEnumerable<ControlDocBO> listadoBO)
        {
            try
            {
                foreach (var objetoBO in listadoBO)
                {
                    bool resultado = Update(objetoBO);
                    if (resultado == false)
                        return false;
                }

                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        private void AsignacionId(TControlDoc entidad, ControlDocBO objetoBO)
        {
            try
            {
                if (entidad != null && objetoBO != null)
                {
                    objetoBO.Id = entidad.Id;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        private TControlDoc MapeoEntidad(ControlDocBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TControlDoc entidad = new TControlDoc();
                entidad = Mapper.Map<ControlDocBO, TControlDoc>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos

                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        #endregion

        /// <summary>
        /// Obtiene los documentos para un alumno por matricula
        /// </summary>
        /// <param name="idMatriculaCabecera"></param>
        /// <returns></returns>
        public List<DocumentoMatriculaDTO> ObtenerDocumentosPorMatriculaCabecera(string CodigoMatricula) {
            try
            {
                List<DocumentoMatriculaDTO> matriculas = new List<DocumentoMatriculaDTO>();
                var _query = "SELECT CodigoMatricula, IdCriterioDocs, NombreDocumento, Estado,EstadoEntero FROM fin.V_ObtenerDatosDocumentosPorMatriculaCabecera WHERE CodigoMatricula = @CodigoMatricula AND Estado=1";
                var matriculasBD = _dapper.QueryDapper(_query, new { CodigoMatricula });
                if (!matriculasBD.Contains("[]") && !string.IsNullOrEmpty(matriculasBD))
                {
                    matriculas = JsonConvert.DeserializeObject<List<DocumentoMatriculaDTO>>(matriculasBD);
                }
                return matriculas;
            }
            catch (Exception e) 
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene los documentos para un alumno por Matricula cabecera
        /// </summary>
        /// <param name="idMatriculaCabecera"></param>
        /// <returns></returns>
        public List<ControlDocumentoMatriculaDTO> ObtenerDocumentosPorMatriculaCabecera(int idMatriculaCabecera)
        {
            try
            {
                List<ControlDocumentoMatriculaDTO> matriculas = new List<ControlDocumentoMatriculaDTO>();
                var _query = "SELECT IdControlDoc, IdMatriculaCabecera, CodigoMatricula, IdCriterioDoc, NombreDocumento, EstadoDocumento FROM fin.V_ObtenerDatosDocumentosPorMatricula WHERE IdMatriculaCabecera = @idMatriculaCabecera AND EstadoCriterioDocumento = 1 AND EstadoMatriculaCabecera = 1";
                var matriculasBD = _dapper.QueryDapper(_query, new { idMatriculaCabecera });
                if (!matriculasBD.Contains("[]") && !string.IsNullOrEmpty(matriculasBD))
                {
                    matriculas = JsonConvert.DeserializeObject<List<ControlDocumentoMatriculaDTO>>(matriculasBD);
                }
                return matriculas;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene los documentos filtrado por alumno o programa especifico o asesor o coordinador o matricula
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public List<MatriculaControlDocumentoDTO> ObtenerDocumentosFiltrado(FiltroControlDocumentoDTO filtro)
        {
            try
            {
                List<MatriculaControlDocumentoDTO> matriculasControlDocumentos = new List<MatriculaControlDocumentoDTO>();
                var matriculasControlDocumentosDB = _dapper.QuerySPDapper("fin.SP_ControlDocumentosAlumnosFiltro", new { filtro.IdAlumno, filtro.IdPEspecifico, filtro.IdAsesor, filtro.IdCoordinador, filtro.IdMatriculaCabecera, filtro.Estado });
                if (!matriculasControlDocumentosDB.Contains("[]") && !string.IsNullOrEmpty(matriculasControlDocumentosDB))
                {
                    matriculasControlDocumentos = JsonConvert.DeserializeObject<List<MatriculaControlDocumentoDTO>>(matriculasControlDocumentosDB);
                }
                return matriculasControlDocumentos;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

    }
}
