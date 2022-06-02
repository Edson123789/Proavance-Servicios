using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.DTOs.Scode.DTOs.Transversal;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    public class PostComentarioUsuarioRepositorio : BaseRepository<TPostComentarioUsuario, PostComentarioUsuarioBO>
    {
        #region Metodos Base
        public PostComentarioUsuarioRepositorio() : base()
        {
        }
        public PostComentarioUsuarioRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<PostComentarioUsuarioBO> GetBy(Expression<Func<TPostComentarioUsuario, bool>> filter)
        {
            IEnumerable<TPostComentarioUsuario> listado = base.GetBy(filter);
            List<PostComentarioUsuarioBO> listadoBO = new List<PostComentarioUsuarioBO>();
            foreach (var itemEntidad in listado)
            {
                PostComentarioUsuarioBO objetoBO = Mapper.Map<TPostComentarioUsuario, PostComentarioUsuarioBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public PostComentarioUsuarioBO FirstById(int id)
        {
            try
            {
                TPostComentarioUsuario entidad = base.FirstById(id);
                PostComentarioUsuarioBO objetoBO = new PostComentarioUsuarioBO();
                Mapper.Map<TPostComentarioUsuario, PostComentarioUsuarioBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public PostComentarioUsuarioBO FirstBy(Expression<Func<TPostComentarioUsuario, bool>> filter)
        {
            try
            {
                TPostComentarioUsuario entidad = base.FirstBy(filter);
                PostComentarioUsuarioBO objetoBO = Mapper.Map<TPostComentarioUsuario, PostComentarioUsuarioBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(PostComentarioUsuarioBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TPostComentarioUsuario entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<PostComentarioUsuarioBO> listadoBO)
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

        public bool Update(PostComentarioUsuarioBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TPostComentarioUsuario entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<PostComentarioUsuarioBO> listadoBO)
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
        private void AsignacionId(TPostComentarioUsuario entidad, PostComentarioUsuarioBO objetoBO)
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

        private TPostComentarioUsuario MapeoEntidad(PostComentarioUsuarioBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TPostComentarioUsuario entidad = new TPostComentarioUsuario();
                entidad = Mapper.Map<PostComentarioUsuarioBO, TPostComentarioUsuario>(objetoBO,
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
        /// Inserta Registro de comentarios de Facebook
        /// </summary>
        /// <param name="pSID"></param>
        /// <param name="nombres"></param>
        /// <param name="mensaje"></param>
        /// <param name="comment_id"></param>
        /// <param name="post_id"></param>
        /// <param name="parent_id"></param>
        /// <param name="usuario"></param>
        /// <param name="asesor"></param>
        /// <returns></returns>
        public List<ComnentInsertarFacebookResultDTO> PostComentarioInsertarWebHook(string pSID, string nombres, string mensaje, string comment_id, string post_id, string parent_id, string usuario, int? asesor, DateTime FechaCreacion)
        {
            string _queryInsertar = "com.SP_PostComentarioInsertarWebHook";
            var queryInsert = _dapper.QuerySPDapper(_queryInsertar, new { PSID = pSID, Nombres = nombres, Mensaje = mensaje, comment_id, post_id, parent_id, Usuario = usuario, Asesor = asesor, FechaCreacion });
            return JsonConvert.DeserializeObject<List<ComnentInsertarFacebookResultDTO>>(queryInsert);
        }
        /// <summary>
        /// obtiene registro de comentario de un usuario
        /// </summary>
        /// <param name="pSID"></param>
        /// <returns></returns>
        public PostComentarioUsuarioDTO ObtenerComentarioFacebookPorIdUsuario(string pSID)
        {
            string _queryInsertar = "SELECT Id,IdUsuarioFacebook,Nombres,TieneRespuesta,IdAreaCapacitacion FROM com.V_TPostComentarioUsuario_ObtenerDatos Where IdUsuarioFacebook=@PSID and Estado=1 ";
            var queryInsert = _dapper.FirstOrDefault(_queryInsertar, new { PSID = pSID });
            return JsonConvert.DeserializeObject<PostComentarioUsuarioDTO>(queryInsert);
        }

		/// <summary>
		/// Obtiene Lista de comentarios facebook
		/// </summary>
		/// <returns></returns>
        public List<PostComentarioUsuarioDTO> ObtenerComentarioFacebook()
        {
            string _queryInsertar = "SELECT Id,IdUsuarioFacebook,Nombres,TieneRespuesta,IdPersonal FROM com.V_TPostComentarioUsuario_ObtenerDatos Where IdPersonal is null and TieneRespuesta=0 and FechaCreacion>=DATEADD(day,-2,GETDATE()) and Estado=1 ";
            var queryInsert = _dapper.QueryDapper(_queryInsertar, null);
            return JsonConvert.DeserializeObject<List<PostComentarioUsuarioDTO>>(queryInsert);
        }
        /// <summary>
        /// Obtiene el Id de poscomentarioUsuario
        /// </summary>
        /// <param name="pSID"></param>
        /// <returns></returns>
        public IdComentarioFacebookDTO ObtenerIdComentarioFacebook(string pSID)
        {
			try
			{
				string _queryInsertar = "SELECT Id FROM com.V_TPostComentarioUsuario_ObtenerId Where IdUsuarioFacebook=@PSID and Estado=1 ";
				var queryInsert = _dapper.FirstOrDefault(_queryInsertar, new { PSID = pSID });
				return JsonConvert.DeserializeObject<IdComentarioFacebookDTO>(queryInsert);
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
           
        }

		/// <summary>
		/// Obtiene todos los comentarios por  IdPersonal
		/// </summary>
		/// <param name="IdPersonal"></param>
		/// <returns></returns>
        public List<PostComentarioUsuarioCompuestoDTO> ObtenerPostComentarioUsuarioPorPersonal(int IdPersonal)
        {
            try
            {
                string _queryComentario = string.Empty;
                _queryComentario = "com.SP_MessengerGetComentariosByPersonal";
                var Comentario = _dapper.QuerySPDapper(_queryComentario, new { IdPersonal });

                return JsonConvert.DeserializeObject<List<PostComentarioUsuarioCompuestoDTO>>(Comentario);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

		public List<PostComentarioDTO> GuardarMensajeInicio(MensajeInicioDTO objeto, string id)
		{
			try
			{
				string _queryInsertar = "com.SP_PostInsertarMensajeInicio";
				var queryInsert = _dapper.QuerySPDapper(_queryInsertar, new {
					IdUsuario = objeto.Id,
					IdPostFacebook = objeto.post_id,
					IdCommentFacebook_old = objeto.comment_id,
					IdUsuarioFacebook = objeto.usuario_id,
					IdCommentFacebook_new = id,
					comentario = objeto.comentario,
					user = "Webhook"
				});
				return JsonConvert.DeserializeObject<List<PostComentarioDTO>>(queryInsert);

			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
	}
}
