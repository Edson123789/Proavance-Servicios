using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Planificacion.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Planificacion.Repositorio
{
    public class TagPwRepositorio : BaseRepository<TTagPw, TagPwBO>
    {
        #region Metodos Base
        public TagPwRepositorio() : base()
        {
        }
        public TagPwRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<TagPwBO> GetBy(Expression<Func<TTagPw, bool>> filter)
        {
            IEnumerable<TTagPw> listado = base.GetBy(filter);
            List<TagPwBO> listadoBO = new List<TagPwBO>();
            foreach (var itemEntidad in listado)
            {
                TagPwBO objetoBO = Mapper.Map<TTagPw, TagPwBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public TagPwBO FirstById(int id)
        {
            try
            {
                TTagPw entidad = base.FirstById(id);
                TagPwBO objetoBO = new TagPwBO();
                Mapper.Map<TTagPw, TagPwBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public TagPwBO FirstBy(Expression<Func<TTagPw, bool>> filter)
        {
            try
            {
                TTagPw entidad = base.FirstBy(filter);
                TagPwBO objetoBO = Mapper.Map<TTagPw, TagPwBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(TagPwBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TTagPw entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<TagPwBO> listadoBO)
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

        public bool Update(TagPwBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TTagPw entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<TagPwBO> listadoBO)
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
        private void AsignacionId(TTagPw entidad, TagPwBO objetoBO)
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

        private TTagPw MapeoEntidad(TagPwBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TTagPw entidad = new TTagPw();
                entidad = Mapper.Map<TagPwBO, TTagPw>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos
                if (objetoBO.TagParametroSeo != null && objetoBO.TagParametroSeo.Count > 0)
                {
                    foreach (var hijo in objetoBO.TagParametroSeo)
                    {
                        TTagParametroSeoPw entidadHijo = new TTagParametroSeoPw();
                        entidadHijo = Mapper.Map<TagParametroSeoPwBO, TTagParametroSeoPw>(hijo,
                            opt => opt.ConfigureMap(MemberList.None));
                        entidad.TTagParametroSeoPw.Add(entidadHijo);
                    }
                }
                if (objetoBO.PGeneralTag != null && objetoBO.PGeneralTag.Count > 0)
                {
                    foreach (var hijo in objetoBO.PGeneralTag)
                    {
                        TPgeneralTagsPw entidadHijo = new TPgeneralTagsPw();
                        entidadHijo = Mapper.Map<PGeneralTagPwBO, TPgeneralTagsPw>(hijo,
                            opt => opt.ConfigureMap(MemberList.None));
                        entidad.TPgeneralTagsPw.Add(entidadHijo);
                    }
                }

                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<TagPwBO> GetFiltered<KProperty>(IEnumerable<Expression<Func<TTagPw, bool>>> filters, Expression<Func<TTagPw, KProperty>> orderBy, bool ascending)
        {
            IEnumerable<TTagPw> listado = base.GetFiltered(filters, orderBy, ascending);
            List<TagPwBO> listadoBO = new List<TagPwBO>();

            foreach (var itemEntidad in listado)
            {
                TagPwBO objetoBO = Mapper.Map<TTagPw, TagPwBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }
            return listadoBO;
        }
        #endregion

        /// <summary>
        /// Obtiene la lista de los parametrosSeo asociados a un determinado Tag [Id,IdTag,IdParametroSeo,Descripcion]
        /// </summary>
        /// <param name="IdArticulo"></param>
        /// <returns>List<FiltroDTO></returns>
        public List<ParametroSeoPorTagDTO> ObtenerParametroSeoAsociadosPorIdTag(int IdTag)
        {
            try
            {
                List<ParametroSeoPorTagDTO> tags = new List<ParametroSeoPorTagDTO>();
                var query = _dapper.QueryDapper("SELECT Id, IdParametroSeo, NombreParametroSeo, Descripcion FROM [pla].[V_TagParametroSeo] WHERE IdTag=@IdTag", new { IdTag=IdTag });
                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    tags = JsonConvert.DeserializeObject<List<ParametroSeoPorTagDTO>>(query);
                }
                return tags;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Obtiene la lista de todos los registros con estado=1 de pla.T_Tag_PW (usado para llenado de grilla)
        /// </summary>
        /// <returns></returns>
        public List<TagPwDTO> OntenerListaTags()
        {
            try
            {
                List<TagPwDTO> items = new List<TagPwDTO>();
                var _query = string.Empty;
                _query = "Select Id,Nombre,Descripcion,TagWebId,Codigo from pla.V_TagPw ";
                var respuestaDapper = _dapper.QueryDapper(_query, null);

                if (!string.IsNullOrEmpty(respuestaDapper) && !respuestaDapper.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<TagPwDTO>>(respuestaDapper);
                }

                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        /// <summary>
        /// Obtiene la lista de todos los registros con estado=1 de pla.T_Tag_PW (usado para llenado de filtros)
        /// </summary>
        /// <returns></returns>
        public List<FiltroDTO> OntenerComboTags()
        {
            try
            {
                List<FiltroDTO> items = new List<FiltroDTO>();
                var _query = string.Empty;
                _query = "Select Id,Nombre from pla.V_TagPw ";
                var respuestaDapper = _dapper.QueryDapper(_query, null);

                if (!string.IsNullOrEmpty(respuestaDapper) && !respuestaDapper.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<FiltroDTO>>(respuestaDapper);
                }

                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
    }
}
