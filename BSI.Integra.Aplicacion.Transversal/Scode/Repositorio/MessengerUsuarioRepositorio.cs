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
    public class MessengerUsuarioRepositorio : BaseRepository<TMessengerUsuario, MessengerUsuarioBO>
    {
        #region Metodos Base
        public MessengerUsuarioRepositorio() : base()
        {
        }
        public MessengerUsuarioRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<MessengerUsuarioBO> GetBy(Expression<Func<TMessengerUsuario, bool>> filter)
        {
            IEnumerable<TMessengerUsuario> listado = base.GetBy(filter);
            List<MessengerUsuarioBO> listadoBO = new List<MessengerUsuarioBO>();
            foreach (var itemEntidad in listado)
            {
                MessengerUsuarioBO objetoBO = Mapper.Map<TMessengerUsuario, MessengerUsuarioBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public MessengerUsuarioBO FirstById(int id)
        {
            try
            {
                TMessengerUsuario entidad = base.FirstById(id);
                MessengerUsuarioBO objetoBO = new MessengerUsuarioBO();
                Mapper.Map<TMessengerUsuario, MessengerUsuarioBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public MessengerUsuarioBO FirstBy(Expression<Func<TMessengerUsuario, bool>> filter)
        {
            try
            {
                TMessengerUsuario entidad = base.FirstBy(filter);
                MessengerUsuarioBO objetoBO = new MessengerUsuarioBO();
                Mapper.Map<TMessengerUsuario, MessengerUsuarioBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(MessengerUsuarioBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TMessengerUsuario entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<MessengerUsuarioBO> listadoBO)
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

        public bool Update(MessengerUsuarioBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TMessengerUsuario entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<MessengerUsuarioBO> listadoBO)
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
        private void AsignacionId(TMessengerUsuario entidad, MessengerUsuarioBO objetoBO)
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

        private TMessengerUsuario MapeoEntidad(MessengerUsuarioBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TMessengerUsuario entidad = new TMessengerUsuario();
                entidad = Mapper.Map<MessengerUsuarioBO, TMessengerUsuario>(objetoBO,
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

        public MessengerUsuarioDTO ObtenerMessengerUsuarioPorIdUsuario(string psid)
        {
            string _queryUsuario = "SELECT Id,PSID,Nombres,Apellidos,UrlFoto,IdPersonal,SeRespondio,IdAreaCapacitacion,IdAreaCapacitacionFacebook,Telefono,Email,MensajeEnviarTelefono,MensajeEnviarEmail FROM com.V_TMessengerUsuario_ObtenerUsuario WHERE PSID=@PSID and Estado=1";
            var queryUsuario = _dapper.FirstOrDefault(_queryUsuario, new { PSID = psid });
            return JsonConvert.DeserializeObject<MessengerUsuarioDTO>(queryUsuario);

        }

        public MessengerUsuarioDTO ObtenerMessengerUsuarioPorId(int id)
        {
            string _queryUsuario = "SELECT Id,PSID,Nombres,Apellidos,UrlFoto,IdPersonal,SeRespondio,IdAreaCapacitacion,IdAreaCapacitacionFacebook,Telefono,Email,MensajeEnviarTelefono,MensajeEnviarEmail FROM com.V_TMessengerUsuario_ObtenerUsuario WHERE Id=@Id and Estado=1";
            var queryUsuario = _dapper.FirstOrDefault(_queryUsuario, new { Id = id });
            return JsonConvert.DeserializeObject<MessengerUsuarioDTO>(queryUsuario);

        }

        public List<MessengerUsuarioDTO> ObtenerMessengerUsuario()
        {
            string _queryUsuario = "SELECT Id,PSID,Nombres,Apellidos,UrlFoto,IdPersonal,SeRespondio,IdAreaCapacitacion,IdAreaCapacitacionFacebook,Telefono,Email,MensajeEnviarTelefono,MensajeEnviarEmail FROM com.V_TMessengerUsuario_ObtenerUsuario WHERE IdPersonal is null and SeRespondio=0 and FechaCreacion>= (DATEADD(day,-2,GETDATE())) and Estado=1 Order By FechaCreacion Asc";
            var queryUsuario = _dapper.QueryDapper(_queryUsuario, null);
            return JsonConvert.DeserializeObject<List<MessengerUsuarioDTO>>(queryUsuario);

        }
        /// <summary>
        /// Obtiene El Usuario de Facebook Por Asesor
        /// </summary>
        /// <param name="idPersonal"></param>
        /// <returns></returns>
        public List<UsuarioNombreFacebookDTO> ObtenerFacebookUsuarioPorAsesor( int idPersonal)
        {
            try
            {
                string _queryUsuario = "SELECT PSID,Nombres FROM com.V_TMessengerUsuario_ObtenerUsuarioPorAsesor WHERE IdPersonalMessenger = @IdPersonal or IdPersonalComentario = @IdPersonal";
                var queryUsuario = _dapper.QueryDapper(_queryUsuario, new { IdPersonal = idPersonal });
                return JsonConvert.DeserializeObject<List<UsuarioNombreFacebookDTO>>(queryUsuario);
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }

        }
		/// <summary>
		/// Obtiene informacion de Messenger chat mediante IdPersonal
		/// </summary>
		/// <param name="idPersonal"></param>
		/// <returns></returns>
        public List<MessengerUsuarioCompuestoDTO> ObtenerMessengerPorAsesor( int idPersonal)
        {
            try
            {
                string _queryUsuario = "SELECT Id,PSID,Nombres,Apellidos,UrlFoto,IdPersonal,SeRespondio,FechaCreacion,FechaModificacion,UsuarioCreacion,UsuarioModificacion,Mensaje FROM com.V_TMessengerUsuario_ObtenerMessengerPorAsesor " +
                                        " WHERE IdPersonal = @IdPersonal order by FechaModificacion desc";
                var queryUsuario = _dapper.QueryDapper(_queryUsuario, new { IdPersonal = idPersonal });
                return JsonConvert.DeserializeObject<List<MessengerUsuarioCompuestoDTO>>(queryUsuario);
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        public List<MessengerInsertarWebHook_ResultDTO> InsertarMessengerWebHook(string pSID, string nombres, string apellidos, string foto, string mensaje, string usuario, Nullable<int> asesor, bool EsPublicidad, string FacebookId, DateTime FechaInteraccion, string UrlArchivoAdjunto, int IdTipoMensajeMessenger)
        {
            string _queryProcedure = "com.SP_MessengerInsertarWebHook";
            var queryProcedure = _dapper.QuerySPDapper(_queryProcedure, new { PSID = pSID, Nombres = nombres, Apellidos = apellidos, Foto = foto, Mensaje = mensaje, Usuario = usuario, Asesor = asesor, EsPublicidad, FacebookId, FechaInteraccion, UrlArchivoAdjunto, IdTipoMensajeMessenger});
            return JsonConvert.DeserializeObject<List<MessengerInsertarWebHook_ResultDTO>>(queryProcedure);
        }
    }
}
