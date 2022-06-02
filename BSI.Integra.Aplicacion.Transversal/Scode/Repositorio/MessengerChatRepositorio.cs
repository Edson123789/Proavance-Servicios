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
    public class MessengerChatRepositorio : BaseRepository<TMessengerChat, MessengerChatBO>
    {
        #region Metodos Base
        public MessengerChatRepositorio() : base()
        {
        }
        public MessengerChatRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<MessengerChatBO> GetBy(Expression<Func<TMessengerChat, bool>> filter)
        {
            IEnumerable<TMessengerChat> listado = base.GetBy(filter);
            List<MessengerChatBO> listadoBO = new List<MessengerChatBO>();
            foreach (var itemEntidad in listado)
            {
                MessengerChatBO objetoBO = Mapper.Map<TMessengerChat, MessengerChatBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public MessengerChatBO FirstById(int id)
        {
            try
            {
                TMessengerChat entidad = base.FirstById(id);
                MessengerChatBO objetoBO = new MessengerChatBO();
                Mapper.Map<TMessengerChat, MessengerChatBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public MessengerChatBO FirstBy(Expression<Func<TMessengerChat, bool>> filter)
        {
            try
            {
                TMessengerChat entidad = base.FirstBy(filter);
                MessengerChatBO objetoBO = Mapper.Map<TMessengerChat, MessengerChatBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(MessengerChatBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TMessengerChat entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<MessengerChatBO> listadoBO)
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

        public bool Update(MessengerChatBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TMessengerChat entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<MessengerChatBO> listadoBO)
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
        private void AsignacionId(TMessengerChat entidad, MessengerChatBO objetoBO)
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

        private TMessengerChat MapeoEntidad(MessengerChatBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TMessengerChat entidad = new TMessengerChat();
                entidad = Mapper.Map<MessengerChatBO, TMessengerChat>(objetoBO,
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
		/// Inserta Registro de chats de Facebook
		/// </summary>
		/// <param name="PSID"></param>
		/// <param name="mensaje"></param>
		/// <param name="usuario"></param>
		/// <param name="asesor"></param>
		/// <returns></returns>
		public List<MessengerInsertarFacebookResultDTO> InsertarMessengerChatFacebook(string PSID, string mensaje, string usuario, Nullable<int> asesor)
        {
            string _queryInsertar = "com.SP_ChatMessengerInsertarFacebook";
            var queryInsertar = _dapper.QuerySPDapper(_queryInsertar, new { PSID, Mensaje = mensaje, Usuario = usuario, Asesor = asesor });
            return JsonConvert.DeserializeObject<List<MessengerInsertarFacebookResultDTO>>(queryInsertar);
        }

		/// <summary>
		/// Obtiene todos los chats historicos del asesor por IdPersonal
		/// </summary>
		/// <param name="IdPersonal"></param>
		/// <returns></returns>
        public List<MessengerChatDTO> ObtenerMessengerChatPorPersonal(int IdPersonal)
        {
            try
            {

                string _queryMessenger = string.Empty;
                _queryMessenger = "com.SP_MessengerGetChatsByPersonal";
                var Messenger = _dapper.QuerySPDapper(_queryMessenger, new { IdPersonal });

                return JsonConvert.DeserializeObject<List<MessengerChatDTO>>(Messenger);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
        public List<MessengerChatDTO> ObtenerMessengerChatRecibidosPorPersonal(int IdPersonal)
        {
            try
            {

                string _queryMessenger = string.Empty;
                _queryMessenger = "com.SP_MessengerChatsByPersonal";
                var Messenger = _dapper.QuerySPDapper(_queryMessenger, new { IdPersonal });

                return JsonConvert.DeserializeObject<List<MessengerChatDTO>>(Messenger);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
        public List<MessengerChatDTO> ObtenerMessengerChatEnviadoPorPersonal(int IdPersonal)
        {
            try
            {

                string _queryMessenger = string.Empty;
                _queryMessenger = "com.SP_MessengerChatsEnviadosByPersonal";
                var Messenger = _dapper.QuerySPDapper(_queryMessenger, new { IdPersonal });

                return JsonConvert.DeserializeObject<List<MessengerChatDTO>>(Messenger);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
        /// <summary>
        /// Obtiene historial de messenger chat
        /// </summary>
        /// <param name="IdPersonal"></param>
        /// <param name="IdAlumno"></param>
        /// <returns></returns>
        public List<MessengerChatDTO> ObtenerHistorialMessengerChatPorPersonal(int IdPersonal, int IdAlumno)
        {
            try
            {

                string _queryMessenger = string.Empty;
                _queryMessenger = "com.SP_HisotrialChatMessenger";
                var Messenger = _dapper.QuerySPDapper(_queryMessenger, new { IdPersonal,IdAlumno });

                return JsonConvert.DeserializeObject<List<MessengerChatDTO>>(Messenger);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

		/// <summary>
		/// Obtiene los  chat detalles por IdUsuario
		/// </summary>
		/// <param name="IdUsuario"></param>
		/// <returns></returns>
        public List<MessengerChatDetalleDTO> ObtenerMessengerChatDetallePorUsuario(int IdUsuario)
        {
            try
            {

                string _queryMessengerDetalle = string.Empty;
                _queryMessengerDetalle = "com.SP_MessengerGetChatDetalle";
                var MessengerDetalle = _dapper.QuerySPDapper(_queryMessengerDetalle, new { IdUsuario });

                return JsonConvert.DeserializeObject<List<MessengerChatDetalleDTO>>(MessengerDetalle);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
    }
}
