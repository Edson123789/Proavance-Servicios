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
    public class PostComentarioDetalleRepositorio : BaseRepository<TPostComentarioDetalle, PostComentarioDetalleBO>
    {
        #region Metodos Base
        public PostComentarioDetalleRepositorio() : base()
        {
        }
        public PostComentarioDetalleRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<PostComentarioDetalleBO> GetBy(Expression<Func<TPostComentarioDetalle, bool>> filter)
        {
            IEnumerable<TPostComentarioDetalle> listado = base.GetBy(filter);
            List<PostComentarioDetalleBO> listadoBO = new List<PostComentarioDetalleBO>();
            foreach (var itemEntidad in listado)
            {
                PostComentarioDetalleBO objetoBO = Mapper.Map<TPostComentarioDetalle, PostComentarioDetalleBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public PostComentarioDetalleBO FirstById(int id)
        {
            try
            {
                TPostComentarioDetalle entidad = base.FirstById(id);
                PostComentarioDetalleBO objetoBO = new PostComentarioDetalleBO();
                Mapper.Map<TPostComentarioDetalle, PostComentarioDetalleBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public PostComentarioDetalleBO FirstBy(Expression<Func<TPostComentarioDetalle, bool>> filter)
        {
            try
            {
                TPostComentarioDetalle entidad = base.FirstBy(filter);
                PostComentarioDetalleBO objetoBO = Mapper.Map<TPostComentarioDetalle, PostComentarioDetalleBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(PostComentarioDetalleBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TPostComentarioDetalle entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<PostComentarioDetalleBO> listadoBO)
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

        public bool Update(PostComentarioDetalleBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TPostComentarioDetalle entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<PostComentarioDetalleBO> listadoBO)
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
        private void AsignacionId(TPostComentarioDetalle entidad, PostComentarioDetalleBO objetoBO)
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

        private TPostComentarioDetalle MapeoEntidad(PostComentarioDetalleBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TPostComentarioDetalle entidad = new TPostComentarioDetalle();
                entidad = Mapper.Map<PostComentarioDetalleBO, TPostComentarioDetalle>(objetoBO,
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
		/// Obtiene todos los post comentarios por IdUsuario
		/// </summary>
		/// <param name="IdUsuario"></param>
		/// <returns></returns>
        public List<PostComentarioDetalleDTO> ObtenerPostComentarioUsuarioPorPersonal(string IdUsuario, string IdPostFacebook)
        {
            try
            {

                string _queryComentario = string.Empty;
                _queryComentario = "com.SP_MessengerGetComentariosDetalle";
                var Comentario = _dapper.QuerySPDapper(_queryComentario, new { IdUsuario, IdPostFacebook });

                return JsonConvert.DeserializeObject<List<PostComentarioDetalleDTO>>(Comentario);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
    }
}
