using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    public class CronogramaCabeceraCambioRepositorio : BaseRepository<TCronogramaCabeceraCambio, CronogramaCabeceraCambioBO>
    {
        #region Metodos Base
        public CronogramaCabeceraCambioRepositorio() : base()
        {
        }
        public CronogramaCabeceraCambioRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<CronogramaCabeceraCambioBO> GetBy(Expression<Func<TCronogramaCabeceraCambio, bool>> filter)
        {
            IEnumerable<TCronogramaCabeceraCambio> listado = base.GetBy(filter);
            List<CronogramaCabeceraCambioBO> listadoBO = new List<CronogramaCabeceraCambioBO>();
            foreach (var itemEntidad in listado)
            {
                CronogramaCabeceraCambioBO objetoBO = Mapper.Map<TCronogramaCabeceraCambio, CronogramaCabeceraCambioBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public CronogramaCabeceraCambioBO FirstById(int id)
        {
            try
            {
                TCronogramaCabeceraCambio entidad = base.FirstById(id);
                CronogramaCabeceraCambioBO objetoBO = new CronogramaCabeceraCambioBO();
                Mapper.Map<TCronogramaCabeceraCambio, CronogramaCabeceraCambioBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public CronogramaCabeceraCambioBO FirstBy(Expression<Func<TCronogramaCabeceraCambio, bool>> filter)
        {
            try
            {
                TCronogramaCabeceraCambio entidad = base.FirstBy(filter);
                CronogramaCabeceraCambioBO objetoBO = Mapper.Map<TCronogramaCabeceraCambio, CronogramaCabeceraCambioBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(CronogramaCabeceraCambioBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TCronogramaCabeceraCambio entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<CronogramaCabeceraCambioBO> listadoBO)
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

        public bool Update(CronogramaCabeceraCambioBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TCronogramaCabeceraCambio entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<CronogramaCabeceraCambioBO> listadoBO)
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
        private void AsignacionId(TCronogramaCabeceraCambio entidad, CronogramaCabeceraCambioBO objetoBO)
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

        private TCronogramaCabeceraCambio MapeoEntidad(CronogramaCabeceraCambioBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TCronogramaCabeceraCambio entidad = new TCronogramaCabeceraCambio();
                entidad = Mapper.Map<CronogramaCabeceraCambioBO, TCronogramaCabeceraCambio>(objetoBO,
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

        public List<DocumentoMatriculaDTO> ObtenerDocumentosPorMatriculaCabecera(int idMatriculaCabecera)
        {
            try
            {
                List<DocumentoMatriculaDTO> matriculas = new List<DocumentoMatriculaDTO>();
                var _query = "SELECT IdMatriculaCabecera, IdCriterioDoc, Nombre FROM fin.V_ObtenerDatosDocumentosPorMatriculaCabecera WHERE  IdMatriculaCabecera = @idMatriculaCabecera AND EstadoCriterioDoc = 1";
                var matriculasBD = _dapper.QueryDapper(_query, new { idMatriculaCabecera });
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
        /// Aprueba o rechaza los cambios de un cronograma
        /// </summary>
        /// <param name="idMatriculaCabecera"></param>
        /// <param name="idCambio"></param>
        /// <param name="version"></param>
        /// <param name="aprobado"></param>
        /// <param name="cancelado"></param>
        /// <returns></returns>
        public CambiosCronogramaCabeceraDTO AprobarRechazarCambios(int idMatriculaCabecera, int idCambio, int version, bool aprobado, bool cancelado) {
            try
            {
                CambiosCronogramaCabeceraDTO cambiosCronogramaCabecera = new CambiosCronogramaCabeceraDTO();
                var matriculasBD = _dapper.QuerySPFirstOrDefault("fin.Aprobarcancelarcambios", new { idMatriculaCabecera, idCambio, version, aprobado, cancelado });
                if (!matriculasBD.Contains("null") && !string.IsNullOrEmpty(matriculasBD))
                {
                    cambiosCronogramaCabecera = JsonConvert.DeserializeObject<CambiosCronogramaCabeceraDTO>(matriculasBD);
                }
                return cambiosCronogramaCabecera;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
