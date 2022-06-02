using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.Planificacion.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using BSI.Integra.Aplicacion.DTOs;
using Newtonsoft.Json;
using System.Linq;

namespace BSI.Integra.Aplicacion.Planificacion.Repositorio
{
    public class SeccionTipoDetallePwRepositorio : BaseRepository<TSeccionTipoDetallePw, SeccionTipoDetallePwBO>
    {
        #region Metodos Base
        public SeccionTipoDetallePwRepositorio() : base()
        {
        }
        public SeccionTipoDetallePwRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<SeccionTipoDetallePwBO> GetBy(Expression<Func<TSeccionTipoDetallePw, bool>> filter)
        {
            IEnumerable<TSeccionTipoDetallePw> listado = base.GetBy(filter);
            List<SeccionTipoDetallePwBO> listadoBO = new List<SeccionTipoDetallePwBO>();
            foreach (var itemEntidad in listado)
            {
                SeccionTipoDetallePwBO objetoBO = Mapper.Map<TSeccionTipoDetallePw, SeccionTipoDetallePwBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public SeccionTipoDetallePwBO FirstById(int id)
        {
            try
            {
                TSeccionTipoDetallePw entidad = base.FirstById(id);
                SeccionTipoDetallePwBO objetoBO = new SeccionTipoDetallePwBO();
                Mapper.Map<TSeccionTipoDetallePw, SeccionTipoDetallePwBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public SeccionTipoDetallePwBO FirstBy(Expression<Func<TSeccionTipoDetallePw, bool>> filter)
        {
            try
            {
                TSeccionTipoDetallePw entidad = base.FirstBy(filter);
                SeccionTipoDetallePwBO objetoBO = Mapper.Map<TSeccionTipoDetallePw, SeccionTipoDetallePwBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(SeccionTipoDetallePwBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TSeccionTipoDetallePw entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<SeccionTipoDetallePwBO> listadoBO)
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

        public bool Update(SeccionTipoDetallePwBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TSeccionTipoDetallePw entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<SeccionTipoDetallePwBO> listadoBO)
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
        private void AsignacionId(TSeccionTipoDetallePw entidad, SeccionTipoDetallePwBO objetoBO)
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

        private TSeccionTipoDetallePw MapeoEntidad(SeccionTipoDetallePwBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TSeccionTipoDetallePw entidad = new TSeccionTipoDetallePw();
                entidad = Mapper.Map<SeccionTipoDetallePwBO, TSeccionTipoDetallePw>(objetoBO,
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
        /// Obtiene lista de SeccionTipoDetallePws
        /// </summary>
        /// <returns></returns>
        public List<FiltroDTO> ObtenerSeccionTipoDetallePwPws()
        {
            try
            {
                string _querySeccionTipoDetallePws = string.Empty;
                _querySeccionTipoDetallePws = "SELECT Id,Nombre FROM pla.V_TSeccionTipoDetallePw_IdNombre WHERE Estado=1";
                var SeccionTipoDetallePws = _dapper.QueryDapper(_querySeccionTipoDetallePws, null);
                return JsonConvert.DeserializeObject<List<FiltroDTO>>(SeccionTipoDetallePws);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene todos los registros de Secciones Tipo Detalle por el IdSeccionPW.
        /// </summary>
        /// <returns></returns>
        public List<SeccionTipoDetalleGrillaPwDTO> ObtenerSeccionesTipoDetallePorIdSeccionPW(int idSeccionPw)
        {
            try
            {
                List<SeccionTipoDetalleGrillaPwDTO> documentos = new List<SeccionTipoDetalleGrillaPwDTO>();
                var _query = string.Empty;
                _query = "SELECT Id as IdSeccionTipoDetallePw,Nombre as NombreSubSeccion,IdSeccionTipoContenido as IdSubSeccionTipoContenido,NombreSeccionTipoContenido FROM pla.V_ObtenerSeccionesTipoDetallePorIdSeccion WHERE " +
                    "Estado = 1 and IdSeccionPw = @IdSeccionPw";
                var respuestaDapper = _dapper.QueryDapper(_query, new { IdSeccionPw = idSeccionPw });
                if (!string.IsNullOrEmpty(respuestaDapper) && !respuestaDapper.Contains("[]"))
                {
                    documentos = JsonConvert.DeserializeObject<List<SeccionTipoDetalleGrillaPwDTO>>(respuestaDapper);
                }

                return documentos;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        /// <summary>
        /// Elimina (Actualiza estado a false ) todos las Seccion Tipo Detalle asociados a una SesionPw
        /// </summary>
        /// <param name="idSesion"></param>
        /// <returns></returns>
        public void EliminacionLogicoPorSesionPw(int idSesion, string usuario, List<SeccionTipoDetallePwDTO> nuevos)
        {
            try
            {
                var listaBorrar = GetBy(x => x.IdSeccionPw == idSesion && x.Estado == true).ToList();
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
        /// Obtiene si existe el elemento enviado
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool ExisteIdSesionTipoDetalle(int id, int idSeccionPw)
        {
            try
            {
                
                var _resultado = new ValorBoolDTO();
                var query = $@"
                            SELECT
                                    CASE WHEN EXISTS 
                                    (
                                          SELECT * FROM pla.T_SeccionTipoDetalle_PW where Id = @Id and IdSeccionPw = @IdSeccionPw
                                    )
                                    THEN 1
                                    ELSE 0
                                 END
                            AS Valor
                            ";
                var resultado = _dapper.FirstOrDefault(query, new { Id = id, IdSeccionPw = idSeccionPw });

                if (!string.IsNullOrEmpty(resultado))
                {
                    _resultado = JsonConvert.DeserializeObject<ValorBoolDTO>(resultado);
                }
                return _resultado.Valor;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene todos los registros de Secciones Tipo Detalle.
        /// </summary>
        /// <returns></returns>
        public SeccionTipoDetallePwBO ObtenerSeccionesTipoDetalle(int id, int idSeccionPw)
        {
            try
            {
                SeccionTipoDetallePwBO documentos = new SeccionTipoDetallePwBO();
                var _query = string.Empty;
                _query = "SELECT * FROM pla.T_SeccionTipoDetalle_PW WHERE " +
                    "Estado = 1 and Id = @Id and IdSeccionPw = @IdSeccionPw";
                var respuestaDapper = _dapper.QueryDapper(_query, new { Id = id, IdSeccionPw = idSeccionPw });
                if (!string.IsNullOrEmpty(respuestaDapper) && !respuestaDapper.Contains("[]"))
                {
                    documentos = JsonConvert.DeserializeObject<SeccionTipoDetallePwBO>(respuestaDapper);
                }

                return documentos;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }


    }
}
