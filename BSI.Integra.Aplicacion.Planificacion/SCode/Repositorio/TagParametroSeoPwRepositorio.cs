using System;
using System.Collections.Generic;
using System.Linq;
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
    public class TagParametroSeoPwRepositorio : BaseRepository<TTagParametroSeoPw, TagParametroSeoPwBO>
    {
        #region Metodos Base
        public TagParametroSeoPwRepositorio() : base()
        {
        }
        public TagParametroSeoPwRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<TagParametroSeoPwBO> GetBy(Expression<Func<TTagParametroSeoPw, bool>> filter)
        {
            IEnumerable<TTagParametroSeoPw> listado = base.GetBy(filter);
            List<TagParametroSeoPwBO> listadoBO = new List<TagParametroSeoPwBO>();
            foreach (var itemEntidad in listado)
            {
                TagParametroSeoPwBO objetoBO = Mapper.Map<TTagParametroSeoPw, TagParametroSeoPwBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public TagParametroSeoPwBO FirstById(int id)
        {
            try
            {
                TTagParametroSeoPw entidad = base.FirstById(id);
                TagParametroSeoPwBO objetoBO = new TagParametroSeoPwBO();
                Mapper.Map<TTagParametroSeoPw, TagParametroSeoPwBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public TagParametroSeoPwBO FirstBy(Expression<Func<TTagParametroSeoPw, bool>> filter)
        {
            try
            {
                TTagParametroSeoPw entidad = base.FirstBy(filter);
                TagParametroSeoPwBO objetoBO = Mapper.Map<TTagParametroSeoPw, TagParametroSeoPwBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(TagParametroSeoPwBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TTagParametroSeoPw entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<TagParametroSeoPwBO> listadoBO)
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

        public bool Update(TagParametroSeoPwBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TTagParametroSeoPw entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<TagParametroSeoPwBO> listadoBO)
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
        private void AsignacionId(TTagParametroSeoPw entidad, TagParametroSeoPwBO objetoBO)
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

        private TTagParametroSeoPw MapeoEntidad(TagParametroSeoPwBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TTagParametroSeoPw entidad = new TTagParametroSeoPw();
                entidad = Mapper.Map<TagParametroSeoPwBO, TTagParametroSeoPw>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos

                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<TagParametroSeoPwBO> GetFiltered<KProperty>(IEnumerable<Expression<Func<TTagParametroSeoPw, bool>>> filters, Expression<Func<TTagParametroSeoPw, KProperty>> orderBy, bool ascending)
        {
            IEnumerable<TTagParametroSeoPw> listado = base.GetFiltered(filters, orderBy, ascending);
            List<TagParametroSeoPwBO> listadoBO = new List<TagParametroSeoPwBO>();

            foreach (var itemEntidad in listado)
            {
                TagParametroSeoPwBO objetoBO = Mapper.Map<TTagParametroSeoPw, TagParametroSeoPwBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }
            return listadoBO;
        }
        #endregion

        /// <summary>
        /// Obtiene registros por el IdTag
        /// </summary>
        /// <param name="idTag"></param>
        /// <returns></returns>
        public List<ParametroContenidoDTO> ObtenerTodoPorIdTag(int idTag)
        {
            try
            {
                List<ParametroContenidoDTO> obtenerTodoIdTag = new List<ParametroContenidoDTO>();
                var _query = "SELECT Id, Nombre , NumeroCaracteres, Contenido  FROM pla.V_obtenerTagParametrosSeoPorIdTag WHERE IdTagPW =   @idTag AND  EstadoTagParametroSeoPW = 1 AND EstadoParametroSeoPW = 1 ";
                var obtenerTodoIdTagDB = _dapper.QueryDapper(_query, new { idTag });
                if (!string.IsNullOrEmpty(obtenerTodoIdTagDB) && !obtenerTodoIdTagDB.Contains("[]"))
                {
                    obtenerTodoIdTag = JsonConvert.DeserializeObject<List<ParametroContenidoDTO>>(obtenerTodoIdTagDB);
                }
                return obtenerTodoIdTag;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Elimina (Actualiza estado a false ) todos las registros de ParametroSeo asociados a TagPw
        /// </summary>
        /// <param name="idTag"></param>
        /// <param name="usuario"></param>
        /// <param name="nuevos"></param>
        public void EliminacionLogicoPorTagPw(int idTag, string usuario, List<ParametroContenidoDTO> nuevos)
        {
            try
            {
                var listaBorrar = GetBy(x => x.IdTagPw == idTag && x.Estado == true).ToList();
                listaBorrar.RemoveAll(x => nuevos.Any(y => y.Equals(x.IdParametroSEOPW)));
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
    }
}
