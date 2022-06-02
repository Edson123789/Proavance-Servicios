using System;
using System.Collections.Generic;
using System.Linq;
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
    public class SuscripcionProgramaGeneralRepositorio : BaseRepository<TSuscripcionProgramaGeneral, SuscripcionProgramaGeneralBO>
    {
        #region Metodos Base
        public SuscripcionProgramaGeneralRepositorio() : base()
        {
        }
        public SuscripcionProgramaGeneralRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<SuscripcionProgramaGeneralBO> GetBy(Expression<Func<TSuscripcionProgramaGeneral, bool>> filter)
        {
            IEnumerable<TSuscripcionProgramaGeneral> listado = base.GetBy(filter);
            List<SuscripcionProgramaGeneralBO> listadoBO = new List<SuscripcionProgramaGeneralBO>();
            foreach (var itemEntidad in listado)
            {
                SuscripcionProgramaGeneralBO objetoBO = Mapper.Map<TSuscripcionProgramaGeneral, SuscripcionProgramaGeneralBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public SuscripcionProgramaGeneralBO FirstById(int id)
        {
            try
            {
                TSuscripcionProgramaGeneral entidad = base.FirstById(id);
                SuscripcionProgramaGeneralBO objetoBO = new SuscripcionProgramaGeneralBO();
                Mapper.Map<TSuscripcionProgramaGeneral, SuscripcionProgramaGeneralBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public SuscripcionProgramaGeneralBO FirstBy(Expression<Func<TSuscripcionProgramaGeneral, bool>> filter)
        {
            try
            {
                TSuscripcionProgramaGeneral entidad = base.FirstBy(filter);
                SuscripcionProgramaGeneralBO objetoBO = Mapper.Map<TSuscripcionProgramaGeneral, SuscripcionProgramaGeneralBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(SuscripcionProgramaGeneralBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TSuscripcionProgramaGeneral entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<SuscripcionProgramaGeneralBO> listadoBO)
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

        public bool Update(SuscripcionProgramaGeneralBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TSuscripcionProgramaGeneral entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<SuscripcionProgramaGeneralBO> listadoBO)
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
        private void AsignacionId(TSuscripcionProgramaGeneral entidad, SuscripcionProgramaGeneralBO objetoBO)
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

        private TSuscripcionProgramaGeneral MapeoEntidad(SuscripcionProgramaGeneralBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TSuscripcionProgramaGeneral entidad = new TSuscripcionProgramaGeneral();
                entidad = Mapper.Map<SuscripcionProgramaGeneralBO, TSuscripcionProgramaGeneral>(objetoBO,
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
        ///  Obtiene la lista de suscripciones  por Programa General  registradas en el sistema 
        /// </summary>
        /// <returns></returns>
        public List<SuscripcionProgramaDTO> ObtenerSuscripcionesPorPrograma(int idPGeneral)
        {
            try
            {
                List<SuscripcionProgramaDTO> suscripciones = new List<SuscripcionProgramaDTO>();
                var _query = string.Empty;
                _query = "SELECT Id,IdPGeneral,Titulo,Descripcion,OrdenBeneficio FROM pla.V_SuscripcionProgramaGeneral WHERE Estado = 1 and IdPGeneral = @idPGeneral";
                var respuestaDapper = _dapper.QueryDapper(_query, new { idPGeneral });
                if (!string.IsNullOrEmpty(respuestaDapper) && !respuestaDapper.Contains("[]"))
                {
                    suscripciones = JsonConvert.DeserializeObject<List<SuscripcionProgramaDTO>>(respuestaDapper);
                }

                return suscripciones;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
        /// <summary>
        /// Elimina (Actualiza estado a false ) todos las Suscripciones  Por Programa
        /// </summary>
        /// <param name="idPGeneral"></param>
        /// <returns></returns>
        public void DeleteLogicoPorPrograma(int idPGeneral, string usuario, List<SuscripcionProgramaDTO> nuevos)
        {
            try
            {
                List<EliminacionIdsDTO> listaBorrar = new List<EliminacionIdsDTO>();
                string _query = "SELECT Id FROM  pla.T_SuscripcionProgramaGeneral WHERE Estado = 1 and IdPGeneral = @idPGeneral ";
                var query = _dapper.QueryDapper(_query, new { idPGeneral });
                listaBorrar = JsonConvert.DeserializeObject<List<EliminacionIdsDTO>>(query);
                listaBorrar.RemoveAll(x => nuevos.Any(y => y.Id == x.Id));
                foreach (var item in listaBorrar)
                {
                    Delete(item.Id, usuario);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        ///  Obtiene la lista de suscripciones (activo)  registradas en el sistema 
        /// </summary>
        /// <returns></returns>
        public List<SuscripcionProgramaFiltroDTO> ObtenerSuscripcionesCombos()
        {
            try
            {
                List<SuscripcionProgramaFiltroDTO> suscripciones = new List<SuscripcionProgramaFiltroDTO>();
                var _query = string.Empty;
                _query = "SELECT Id,Nombre FROM pla.V_Suscripciones_Filtro WHERE Estado = 1 " ;
                var respuestaDapper = _dapper.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(respuestaDapper) && !respuestaDapper.Contains("[]"))
                {
                    suscripciones = JsonConvert.DeserializeObject<List<SuscripcionProgramaFiltroDTO>>(respuestaDapper);
                }

                return suscripciones;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
        /// <summary>
        ///  Obtiene la lista de suscripciones (activo) con Id, Nombre  registradas en el sistema 
        /// </summary>
        /// <returns></returns>
        public List<SuscripcionProgramaFiltroDTO> ObtenerSuscripcionesPorProgramaNombre(int idPGeneral)
        {
            try
            {
                List<SuscripcionProgramaFiltroDTO> suscripciones = new List<SuscripcionProgramaFiltroDTO>();
                var _query = string.Empty;
                _query = "SELECT Id,Nombre FROM pla.V_Suscripciones_Filtro WHERE Estado = 1 and IdPGeneral = @idPGeneral";
                var respuestaDapper = _dapper.QueryDapper(_query, new { idPGeneral });
                if (!string.IsNullOrEmpty(respuestaDapper) && !respuestaDapper.Contains("[]"))
                {
                    suscripciones = JsonConvert.DeserializeObject<List<SuscripcionProgramaFiltroDTO>>(respuestaDapper);
                }

                return suscripciones;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
    }
}
