using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    /// Repositorio: WhatsAppMensajeEnviadoRepositorio
    /// Autor:   - Edgar S, Jashin Salazar.
    /// Fecha: 10/05/2021
    /// <summary>
    /// Repositorio para consultas de mensajes enviados via WhatsApp
    /// </summary>
    public class WhatsAppMensajeEnviadoRepositorio : BaseRepository<TWhatsAppMensajeEnviado, WhatsAppMensajeEnviadoBO>
    {
        #region Metodos Base
        public WhatsAppMensajeEnviadoRepositorio() : base()
        {
        }
        public WhatsAppMensajeEnviadoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<WhatsAppMensajeEnviadoBO> GetBy(Expression<Func<TWhatsAppMensajeEnviado, bool>> filter)
        {
            IEnumerable<TWhatsAppMensajeEnviado> listado = base.GetBy(filter);
            List<WhatsAppMensajeEnviadoBO> listadoBO = new List<WhatsAppMensajeEnviadoBO>();
            foreach (var itemEntidad in listado)
            {
                WhatsAppMensajeEnviadoBO objetoBO = Mapper.Map<TWhatsAppMensajeEnviado, WhatsAppMensajeEnviadoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public WhatsAppMensajeEnviadoBO FirstById(int id)
        {
            try
            {
                TWhatsAppMensajeEnviado entidad = base.FirstById(id);
                WhatsAppMensajeEnviadoBO objetoBO = new WhatsAppMensajeEnviadoBO();
                Mapper.Map<TWhatsAppMensajeEnviado, WhatsAppMensajeEnviadoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public WhatsAppMensajeEnviadoBO FirstBy(Expression<Func<TWhatsAppMensajeEnviado, bool>> filter)
        {
            try
            {
                TWhatsAppMensajeEnviado entidad = base.FirstBy(filter);
                WhatsAppMensajeEnviadoBO objetoBO = Mapper.Map<TWhatsAppMensajeEnviado, WhatsAppMensajeEnviadoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(WhatsAppMensajeEnviadoBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TWhatsAppMensajeEnviado entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<WhatsAppMensajeEnviadoBO> listadoBO)
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

        public bool Update(WhatsAppMensajeEnviadoBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TWhatsAppMensajeEnviado entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<WhatsAppMensajeEnviadoBO> listadoBO)
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
        private void AsignacionId(TWhatsAppMensajeEnviado entidad, WhatsAppMensajeEnviadoBO objetoBO)
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

        private TWhatsAppMensajeEnviado MapeoEntidad(WhatsAppMensajeEnviadoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TWhatsAppMensajeEnviado entidad = new TWhatsAppMensajeEnviado();
                entidad = Mapper.Map<WhatsAppMensajeEnviadoBO, TWhatsAppMensajeEnviado>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        #endregion

        /// Repositorio: WhatsAppMensajeEnviadoRepositorio
        /// Autor: , Jashin Salazar.
        /// Fecha: 10/05/2021
        /// <summary>
        /// Obtiene Lista de contactos
        /// </summary>
        /// <param name="idPersonal"> Id de Personal </param>
        /// <returns> Lista de ObjetosDTO: List<WhatsAppContactoDTO> </returns>
        public List<WhatsAppContactoChatDTO> ChatsGeneradosContactos(int idPersonal)
        {
            try
            {
                List<WhatsAppContactoChatDTO> listaContactos = new List<WhatsAppContactoChatDTO>();
                var _query = string.Empty;
                _query = "SELECT Y.Id AS IdContacto, Y.Nombre1, Y.Nombre2, Y.ApellidoPaterno, Y.ApellidoMaterno, X.WaTo AS NumeroCelular, Y.IdCodigoPais " +
                         "FROM mkt.T_WhatsAppMensajeEnviado AS X "+
                         "INNER JOIN mkt.T_Alumno AS Y ON X.IdPais=Y.IdPais AND X.IdAlumno=Y.Id "+
                         "WHERE X.IdPersonal=@idPersonal "+
                         "GROUP BY Y.Nombre1, Y.Nombre2, Y.ApellidoPaterno,Y.ApellidoMaterno,Y.Celular,X.WaTo "+
                         "ORDER BY x.FechaCreacion DESC";

                var CredencialTokenExpiraDB = _dapper.FirstOrDefault(_query, new { idPersonal });
                listaContactos = JsonConvert.DeserializeObject<List<WhatsAppContactoChatDTO>>(CredencialTokenExpiraDB);
                return listaContactos;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Repositorio: WhatsAppMensajeEnviadoRepositorio
        /// Autor: , Jashin Salazar.
        /// Fecha: 10/05/2021
        /// <summary>
        /// Obtiene Lista de último mensaje de chat por IdPersonal
        /// </summary>
        /// <param name="idPersonal"> Id de Personal </param>
        /// <returns> Lista de ObjetosDTO: List<WhatsAppMensajesDTO> </returns>
        public List<WhatsAppMensajesDTO> ListaUltimoMensajeChats(int idPersonal)
        {
            try
            {
                List<WhatsAppMensajesDTO> listaMensajes = new List<WhatsAppMensajesDTO>();
                var _query = string.Empty;
                _query = "SELECT wa.Numero, wa.Mensaje, wa.IdPersonal, wa.FechaCreacion, wa.IdPais, ISNULL(wa.IdAlumno,0) IdAlumno, CASE WHEN al.Nombre1 is null THEN wa.Numero ELSE CONCAT(al.Nombre1,' ',al.ApellidoPaterno) END NombreAlumno " +
                         "FROM mkt.V_UltimoChatWhatsAppContacto wa " +
                         "LEFT JOIN mkt.T_Alumno al on wa.IdAlumno=al.Id " +
                         "LEFT JOIN conf.T_ClasificacionPersona cp on cp.IdTablaOriginal=al.Id" + // Agregado
                                                                                                  //"WHERE wa.IdPersonal=@idPersonal" +
                         "WHERE cp.Id=@idPersonal" +
                         " Order by wa.FechaCreacion Desc";

                var CredencialTokenExpiraDB = _dapper.QueryDapper(_query, new { idPersonal });
                listaMensajes = JsonConvert.DeserializeObject<List<WhatsAppMensajesDTO>>(CredencialTokenExpiraDB);
                return listaMensajes;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        /// Repositorio: WhatsAppMensajeEnviadoRepositorio
        /// Autor: , Edgar S.
        /// Fecha: 10/03/2021
        /// <summary>
        /// Obtiene Lista de último mensaje de chat de alumnos por IdPersonal
        /// </summary>
        /// <param name="idPersonal"> Id de Personal </param>
        /// <returns> Lista de ObjetosDTO: List<WhatsAppMensajesDTO> </returns>
        public List<WhatsAppMensajesDTO> ListaUltimoMensajeChatsRecibido(int idPersonal)
        {
            try
            {
                List<WhatsAppMensajesDTO> listaMensajes = new List<WhatsAppMensajesDTO>();
                var query = string.Empty;
                query = "SELECT wa.Numero, wa.Mensaje, wa.IdPersonal, wa.FechaCreacion, wa.IdPais, ISNULL(wa.IdAlumno,0) IdAlumno, CASE WHEN al.Nombre1 is null THEN wa.Numero ELSE CONCAT(al.Nombre1,' ',al.ApellidoPaterno) END NombreAlumno " +
                         "FROM mkt.V_UltimoChatWhatsAppContactoRecibido wa " +
                         "LEFT JOIN mkt.T_Alumno al on wa.IdAlumno=al.Id " +
                         "WHERE wa.IdPersonal=@idPersonal Order by wa.FechaCreacion Desc";

                var credencialTokenExpiraDB = _dapper.QueryDapper(query, new { idPersonal });
                listaMensajes = JsonConvert.DeserializeObject<List<WhatsAppMensajesDTO>>(credencialTokenExpiraDB);
                return listaMensajes;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        /// Repositorio: WhatsAppMensajeEnviadoRepositorio
        /// Autor: Edgar S.
        /// Fecha: 10/03/2021
        /// <summary>
        /// Obtiene Historial de Chats recibidos
        /// </summary>
        /// <param name="idPersonal"> Id de Personal </param>
        /// <param name="numero"> Número de WhatsApp </param>
        /// <param name="area"> Area </param>
        /// <returns> Lista de ObjetosDTO: List<WhatsAppMensajesDTO> </returns>
        public List<WhatsAppMensajesDTO> HistorialChatsRecibido(int idPersonal,string numero, string area)
        {
            try
            {
                List<WhatsAppMensajesDTO> listaMensajes = new List<WhatsAppMensajesDTO>();
                var query = string.Empty;

                if (area == "VE")
                {
                    query = "SELECT wa.Numero, wa.Mensaje, wa.IdPersonal, wa.FechaCreacion, wa.IdPais, ISNULL(wa.IdAlumno,0) IdAlumno, CASE WHEN al.Nombre1 is null THEN wa.Numero ELSE CONCAT(al.Nombre1,' ',al.ApellidoPaterno) END NombreAlumno " +
                         "FROM mkt.V_HistorialChatWhatsAppRecibido AS wa " +
                         "LEFT JOIN mkt.T_Alumno AS al WITH(NOLOCK) on wa.IdAlumno=al.Id " +
                         "WHERE wa.IdPersonal=@idPersonal and wa.Numero=@numero Order by wa.FechaCreacion Desc";
                }
                else if (area == "OP")
                {
                    query = "SELECT wa.Numero, wa.Mensaje, wa.IdPersonal, wa.FechaCreacion, wa.IdPais, ISNULL(wa.IdAlumno,0) IdAlumno, CASE WHEN al.Nombre1 is null THEN wa.Numero ELSE CONCAT(al.Nombre1,' ',al.ApellidoPaterno) END NombreAlumno " +
                         "FROM mkt.V_HistorialChatWhatsAppRecibido AS wa " +
                         "LEFT JOIN mkt.T_Alumno AS al WITH(NOLOCK) on wa.IdAlumno=al.Id " +
                         "WHERE wa.Numero=@numero Order by wa.FechaCreacion Desc";
                }
                else if (area == "OP-DOC")
                {
                    query = @"SELECT wa.Numero, wa.Mensaje, wa.IdPersonal, wa.FechaCreacion, wa.IdPais, ISNULL(wa.IdAlumno,0) IdAlumno, CASE WHEN al.Nombre1 is null THEN wa.Numero ELSE CONCAT(al.Nombre1,' ',al.ApellidoPaterno) END NombreAlumno 
                            FROM mkt.V_HistorialChatWhatsAppRecibido AS wa 
                            LEFT JOIN mkt.T_Alumno AS al WITH(NOLOCK) ON wa.IdAlumno=al.Id 
                            LEFT JOIN gp.T_Personal AS personal ON personal.Id=wa.IdPersonal
                            WHERE wa.Numero=@numero AND personal.AreaAbrev='OP'
                            ORDER by wa.FechaCreacion DESC";
                }
                else
                {
                    query = "SELECT wa.Numero, wa.Mensaje, wa.IdPersonal, wa.FechaCreacion, wa.IdPais, ISNULL(wa.IdAlumno,0) IdAlumno, CASE WHEN al.Nombre1 is null THEN wa.Numero ELSE CONCAT(al.Nombre1,' ',al.ApellidoPaterno) END NombreAlumno " +
                         "FROM mkt.V_HistorialChatWhatsAppRecibido AS wa " +
                         "LEFT JOIN mkt.T_Alumno AS al WITH(NOLOCK) on wa.IdAlumno=al.Id " +
                         "WHERE wa.IdPersonal=@idPersonal and wa.Numero=@numero Order by wa.FechaCreacion Desc";
                }
                

                var credencialTokenExpiraDB = _dapper.QueryDapper(query, new { idPersonal, numero});
                listaMensajes = JsonConvert.DeserializeObject<List<WhatsAppMensajesDTO>>(credencialTokenExpiraDB);
                return listaMensajes;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        /// Repositorio: WhatsAppMensajeEnviadoRepositorio
        /// Autor: , Jashin Salazar.
        /// Fecha: 10/05/2021
        /// <summary>
        /// Obtiene el historial en base al tipo de agenda
        /// </summary>
        /// <param name="idPersonal"> Id de Personal </param>
        /// <param name="Numero"> Numero de celular </param>
        /// <param name="Area"> Area del asesor </param>
        /// <param name="idTipoAgenda"> Id del tipo de Agenda </param>
        /// <returns> Lista de ObjetosDTO: List<WhatsAppMensajesDTO> </returns>
        public List<WhatsAppMensajesDTO> HistorialChatsRecibido(int idPersonal, string Numero, string Area, int idTipoAgenda)
        {
            try
            {
                List<WhatsAppMensajesDTO> listaMensajes = new List<WhatsAppMensajesDTO>();
                var _query = string.Empty;

                if (idTipoAgenda == 3)
                {
                    _query = $@"
                                SELECT Numero, 
                                        Mensaje, 
                                        IdPersonal, 
                                        FechaCreacion, 
                                        IdPais, 
                                        IdAlumno, 
                                        NombreAlumno
                                FROM ope.V_ObtenerHistorialChatWhatsAppRecibidoProveedor
                                WHERE Numero = @Numero
                                ORDER BY FechaCreacion DESC;
                                ";
                }
                else {
                    if (Area == "VE")
                    {
                        _query = "SELECT wa.Numero, wa.Mensaje, wa.IdPersonal, wa.FechaCreacion, wa.IdPais, ISNULL(wa.IdAlumno,0) IdAlumno, CASE WHEN al.Nombre1 is null THEN wa.Numero ELSE CONCAT(al.Nombre1,' ',al.ApellidoPaterno) END NombreAlumno " +
                             "FROM mkt.V_HistorialChatWhatsAppRecibido wa " +
                             "LEFT JOIN mkt.T_Alumno al on wa.IdAlumno=al.Id " +
                             "WHERE wa.IdPersonal=@idPersonal and wa.Numero=@Numero Order by wa.FechaCreacion Desc";
                    }
                    else if (Area == "OP")
                    {
                        _query = "SELECT wa.Numero, wa.Mensaje, wa.IdPersonal, wa.FechaCreacion, wa.IdPais, ISNULL(wa.IdAlumno,0) IdAlumno, CASE WHEN al.Nombre1 is null THEN wa.Numero ELSE CONCAT(al.Nombre1,' ',al.ApellidoPaterno) END NombreAlumno " +
                             "FROM mkt.V_HistorialChatWhatsAppRecibido wa " +
                             "LEFT JOIN mkt.T_Alumno al on wa.IdAlumno=al.Id " +
                             "WHERE wa.Numero=@Numero Order by wa.FechaCreacion Desc";
                    }
                    else
                    {
                        _query = "SELECT wa.Numero, wa.Mensaje, wa.IdPersonal, wa.FechaCreacion, wa.IdPais, ISNULL(wa.IdAlumno,0) IdAlumno, CASE WHEN al.Nombre1 is null THEN wa.Numero ELSE CONCAT(al.Nombre1,' ',al.ApellidoPaterno) END NombreAlumno " +
                             "FROM mkt.V_HistorialChatWhatsAppRecibido wa " +
                             "LEFT JOIN mkt.T_Alumno al on wa.IdAlumno=al.Id " +
                             "WHERE wa.IdPersonal=@idPersonal and wa.Numero=@Numero Order by wa.FechaCreacion Desc";
                    }
                }

                var CredencialTokenExpiraDB = _dapper.QueryDapper(_query, new { idPersonal, Numero });
                listaMensajes = JsonConvert.DeserializeObject<List<WhatsAppMensajesDTO>>(CredencialTokenExpiraDB);
                return listaMensajes;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        /// Repositorio: WhatsAppMensajeEnviadoRepositorio
        /// Autor: , Jashin Salazar.
        /// Fecha: 10/05/2021
        /// <summary>
        /// Obtiene el historial en base al tipo de agenda
        /// </summary>
        /// <param name="idPersonal">Id de personal</param>
        /// <returns> Lista de ObjetosDTO: List<WhatsAppMensajesDTO> </returns>
        public List<WhatsAppMensajesDTO> ListaUltimoMensajeChatsEnviado(int idPersonal)
        {
            try
            {
                List<WhatsAppMensajesDTO> listaMensajes = new List<WhatsAppMensajesDTO>();
                var _query = string.Empty;
                _query = "mkt.SP_UltimoChatWhatsAppContactoEnviado";

                var CredencialTokenExpiraDB = _dapper.QuerySPDapper(_query, new { idPersonal });
                listaMensajes = JsonConvert.DeserializeObject<List<WhatsAppMensajesDTO>>(CredencialTokenExpiraDB);
                return listaMensajes;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        /// Repositorio: WhatsAppMensajeEnviadoRepositorio
        /// Autor: Edgar S.
        /// Fecha: 10/03/2021
        /// <summary>
        /// Obtiene Historial de Chats recibidos
        /// </summary>
        /// <param name="idPersonal"> Id de Personal </param>
        /// <param name="numero"> Número de WhatsApp </param>
        /// <param name="area"> Area </param>
        /// <returns> Lista de ObjetosDTO: List<WhatsAppHistorialMensajesDTO> </returns>
        public List<WhatsAppHistorialMensajesDTO> ListaHistorialMensajeChat(int idPersonal, string numero, string area)
        {
            try
            {
                List<WhatsAppHistorialMensajesDTO> listaMensajes = new List<WhatsAppHistorialMensajesDTO>();
                var query = string.Empty;

                if (idPersonal==0)
                {
                    query = "SELECT Numero, Tipo, Mensaje, IdPersonal, ISNULL(IdAlumno,0) IdAlumno,IdPais, Registro, FechaCreacion, NombrePersonal " +
                         "FROM mkt.V_HistorialChatWhatsApp WHERE MensajeOfensivo = 0 AND Numero=@numero Order by FechaCreacion Asc";
                }
                else
                {
                    if (area == "VE")
                    {
                        query = "SELECT Numero, Tipo, Mensaje, IdPersonal, ISNULL(IdAlumno,0) IdAlumno,IdPais, Registro, FechaCreacion, NombrePersonal " +
                         "FROM mkt.V_HistorialChatWhatsApp WHERE MensajeOfensivo = 0 AND IdPersonal=@idPersonal AND Numero=@numero Order by FechaCreacion Asc";
                    }
                    else if (area == "OP")
                    {
                        query = "SELECT Numero, Tipo, Mensaje, IdPersonal, ISNULL(IdAlumno,0) IdAlumno,IdPais, Registro, FechaCreacion, NombrePersonal " +
                         "FROM mkt.V_HistorialChatWhatsApp WHERE MensajeOfensivo = 0 AND Numero=@numero Order by FechaCreacion Asc";
                    }
                    else if (area == "OP-DOC")
                    {
                        //query = "SELECT Numero, Tipo, Mensaje, IdPersonal, ISNULL(IdAlumno,0) IdAlumno,IdPais, Registro, FechaCreacion, NombrePersonal " +
                        // "FROM mkt.V_HistorialChatWhatsApp WHERE MensajeOfensivo = 0 AND Numero=@numero AND AreaAbrev='OP' Order by FechaCreacion Asc";
                        query = @"SELECT DISTINCT Numero, Tipo, Mensaje, IdPersonal,IdAlumno,IdPais, Registro, FechaCreacion, NombrePersonal, MAX(EstadoMensaje) AS EstadoMensaje
                                FROM (
	                                SELECT resultado.WaId,Numero, Tipo, Mensaje, IdPersonal, ISNULL(IdAlumno,0) IdAlumno,resultado.IdPais, Registro, resultado.FechaCreacion, NombrePersonal,AreaAbrev,estado.WaStatus,
		                                CASE
			                                WHEN estado.WaStatus='sent'
				                                THEN 1
			                                WHEN estado.WaStatus='delivered'
				                                THEN 2
			                                WHEN estado.WaStatus='read'
				                                THEN 3
		                                END AS EstadoMensaje
	                                FROM mkt.V_HistorialChatWhatsApp AS resultado
	                                LEFT JOIN mkt.T_WhatsAppEstadoMensajeEnviado AS estado ON estado.WaId = resultado.WaId
	                                WHERE MensajeOfensivo = 0 AND Numero=@numero AND AreaAbrev='OP'
                                ) AS final
                                GROUP BY 
                                Numero, Tipo, Mensaje, IdPersonal,IdAlumno,IdPais, Registro, FechaCreacion, NombrePersonal
                                ORDER by FechaCreacion ASC";
                    }
                    else
                    {
                        query = "SELECT Numero, Tipo, Mensaje, IdPersonal, ISNULL(IdAlumno,0) IdAlumno,IdPais, Registro, FechaCreacion, NombrePersonal " +
                         "FROM mkt.V_HistorialChatWhatsApp WHERE MensajeOfensivo = 0 AND IdPersonal=@idPersonal AND Numero=@numero Order by FechaCreacion Asc";
                    }
                }
                var credencialTokenExpiraDB = _dapper.QueryDapper(query, new { idPersonal, numero });
                listaMensajes = JsonConvert.DeserializeObject<List<WhatsAppHistorialMensajesDTO>>(credencialTokenExpiraDB);
                return listaMensajes;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        /// Repositorio: WhatsAppMensajeEnviadoRepositorio
        /// Autor: , Jashin Salazar.
        /// Fecha: 10/05/2021
        /// <summary>
        /// Obtiene mensaje multimedia de WhatsApp
        /// </summary>
        /// <param name="WaId"> Id de chat WhatsApp </param>
        /// <returns> String </returns>
        public string ObtenerMensajeMultimedia(string WaId)
        {
            try
            {
                WhatsAppHistorialMensajesDTO listaMensajes = new WhatsAppHistorialMensajesDTO();
                var _query = string.Empty;
                
                _query = "SELECT Mensaje " +
                        "FROM mkt.V_HistorialChatWhatsApp WHERE WaId=@WaId";
               
                
                var CredencialTokenExpiraDB = _dapper.FirstOrDefault(_query, new { WaId });
                listaMensajes = JsonConvert.DeserializeObject<WhatsAppHistorialMensajesDTO>(CredencialTokenExpiraDB);
                return listaMensajes.Mensaje;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        /// Repositorio: WhatsAppMensajeEnviadoRepositorio
        /// Autor: , Jashin Salazar.
        /// Fecha: 10/05/2021
        /// <summary>
        /// Obtiene conversacion por numero.
        /// </summary>
        /// <param name="Numero"> Numero de WhatsApp </param>
        /// <returns> PersonalAlumnoDTO </returns>
        public PersonalAlumnoDTO ObtenerConversacionNumero(string Numero)
        {
            PersonalAlumnoDTO Conversacion = new PersonalAlumnoDTO();
            string _queryConversacion = "Select mw.IdPersonal,ISNULL(mw.IdAlumno,0) IdAlumno From mkt.V_TWhatsAppMensajeEnviado_ObtenerAsesorAlumno mw inner join gp.T_Personal pe on mw.IdPersonal=pe.Id Where pe.Activo=1 and mw.WaTo=@Numero Order by mw.FechaCreacion desc";
            var queryConversacion = _dapper.FirstOrDefault(_queryConversacion, new { Numero});
            if (queryConversacion == null || queryConversacion == "")
            {
                return null;
            }
            else
            {
                Conversacion = JsonConvert.DeserializeObject<PersonalAlumnoDTO>(queryConversacion);
                return Conversacion;
            }
            
        }

        /// Repositorio: WhatsAppMensajeEnviadoRepositorio
        /// Autor: , Jashin Salazar.
        /// Fecha: 10/05/2021
        /// <summary>
        /// Obtiene personal con minimo de chats por personal.
        /// </summary>
        /// <returns> PersonalNumeroMinimoChatDTO </returns>
        public PersonalNumeroMinimoChatDTO ObtenerAsesorConMenorChat()
        {
            PersonalNumeroMinimoChatDTO Conversacion = new PersonalNumeroMinimoChatDTO();
            string _query = "SELECT TOP 1 WU.IdPersonal,ISNULL(CON.NumeroChats,0) AS NumeroChats " +
                            "FROM mkt.T_WhatsAppUsuario WU " +
                            "INNER JOIN gp.T_Personal PER ON WU.IdPersonal=PER.Id " +
                            "LEFT JOIN mkt.V_UltimoChatWhatsAppContactoByAsesores CON ON WU.IdPersonal=CON.IdPersonal " +
                            "WHERE WU.Estado=1 AND PER.Rol<>'Sistemas' " +
                            "ORDER BY CON.NumeroChats ASC";
            var queryAsesor = _dapper.FirstOrDefault(_query, null);
            if (queryAsesor == null || queryAsesor == "")
            {
                return null;
            }
            else
            {
                Conversacion = JsonConvert.DeserializeObject<PersonalNumeroMinimoChatDTO>(queryAsesor);
                return Conversacion;
            }

        }

        /// Repositorio: WhatsAppMensajeEnviadoRepositorio
        /// Autor: , Jashin Salazar.
        /// Fecha: 10/05/2021
        /// <summary>
        /// Valida plantillas de mensajes enviados.
        /// </summary>
        /// <param name="Plantilla"> Tipo de plantilla </param>
        /// <param name="Numero"> Numero de WhatsApp </param>
        /// <returns> Bool </returns>
        public bool ValidarPlantillasEnviadas(string Plantilla, string Numero)
        {
            PersonalNumeroMinimoChatDTO Conversacion = new PersonalNumeroMinimoChatDTO();
            string _query = "SELECT Id From mkt.T_WhatsAppMensajeEnviado Where WaRecipientType='hsm' and WaBody=@Plantilla and WaTo=@Numero ";
            var queryAsesor = _dapper.FirstOrDefault(_query, new { Plantilla, Numero});
            if (queryAsesor == "null" || queryAsesor == "")
            {
                return true;
            }
            else
            {
                //Conversacion = JsonConvert.DeserializeObject<PersonalNumeroMinimoChatDTO>(queryAsesor);
                return false;
            }

        }

        /// Repositorio: WhatsAppMensajeEnviadoRepositorio
        /// Autor: , Jashin Salazar.
        /// Fecha: 10/05/2021
        /// <summary>
        /// Obtiene mensajes recibidos en operaciones.
        /// </summary>
        /// <param name="IdPersonal"> Id de personal </param>
        /// <returns> List<WhatsAppMensajesRecibidosOperacionesDTO> </returns>
        public List<WhatsAppMensajesRecibidosOperacionesDTO> ObtenerMensajesRecibidosOperaciones(int IdPersonal)
        {
            List<WhatsAppMensajesRecibidosOperacionesDTO> Conversacion = new List<WhatsAppMensajesRecibidosOperacionesDTO>();
            string _queryConversacion = "ope.SP_MensajesRecibidosWhatsAppOperacionesVersion";
            var queryConversacion = _dapper.QuerySPDapper(_queryConversacion, new { IdPersonal });
            if (queryConversacion == null || queryConversacion == "")
            {
                return null;
            }
            else
            {
                Conversacion = JsonConvert.DeserializeObject<List<WhatsAppMensajesRecibidosOperacionesDTO>>(queryConversacion);
                return Conversacion;
            }

        }

        ///Repositorio: WhatsAppMensajeEnviadoRepositorio
        ///Autor: , Edgar S.
        ///Fecha: 10/03/2021
        /// <summary>
        /// Obtiene Lista de último mensaje de chat de alumnos por IdPersonal ordenado por Fecha Modificación para Control de Mensajes Ofensivos
        /// </summary>
        /// <param name="idPersonal"> Id de Personal </param>
        /// <returns> Lista de ObjetosDTO: List<WhatsAppMensajesDTO> </returns>
        public List<WhatsAppMensajesDTO> ListaUltimoMensajeChatsRecibidoControlMensaje(int idPersonal)
        {
            try
            {
                List<WhatsAppMensajesDTO> listaMensajes = new List<WhatsAppMensajesDTO>();
                var query = string.Empty;
                query = "mkt.SP_UltimoChatWhatsAppContactoRecibidoControlMensajes";
                var credencialTokenExpiraDB = _dapper.QuerySPDapper(query, new { idPersonal });
                listaMensajes = JsonConvert.DeserializeObject<List<WhatsAppMensajesDTO>>(credencialTokenExpiraDB);
                return listaMensajes;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        ///Repositorio: WhatsAppMensajeEnviadoRepositorio
        ///Autor: Edgar S.
        ///Fecha: 10/03/2021
        /// <summary>
        /// Obtiene Historial de Chats recibidos
        /// </summary>
        /// <param name="idPersonal"> Id de Personal </param>
        /// <param name="numero"> Número de WhatsApp </param>
        /// <param name="area"> Area </param>
        /// <returns> Lista de ObjetosDTO: List<WhatsAppHistorialMensajesDTO> </returns>
        public List<WhatsAppHistorialMensajesDTO> ListaHistorialMensajeChatControlMensaje(int idPersonal, string numero, string area)
        {
            try
            {
                List<WhatsAppHistorialMensajesDTO> listaMensajes = new List<WhatsAppHistorialMensajesDTO>();
                var query = string.Empty;

                if (idPersonal == 0)
                {
                    query = "SELECT Numero, Tipo, Mensaje, IdPersonal, ISNULL(IdAlumno,0) IdAlumno,IdPais, Registro, FechaModificacion AS FechaCreacion, NombrePersonal " +
                    "FROM mkt.V_HistorialChatWhatsAppControlMensajes WHERE MensajeOfensivo = 0 AND Numero=@numero Order by FechaModificacion Asc";
                }
                else
                {
                    if (area == "VE")
                    {
                        query = "SELECT Numero, Tipo, Mensaje, IdPersonal, ISNULL(IdAlumno,0) IdAlumno,IdPais, Registro, FechaModificacion AS FechaCreacion, NombrePersonal " +
                         "FROM mkt.V_HistorialChatWhatsAppControlMensajes WHERE MensajeOfensivo = 0 AND IdPersonal=@idPersonal AND Numero=@numero Order by FechaModificacion Asc";
                    }
                    else if (area == "OP")
                    {
                        query = "SELECT Numero, Tipo, Mensaje, IdPersonal, ISNULL(IdAlumno,0) IdAlumno,IdPais, Registro, FechaModificacion AS FechaCreacion, NombrePersonal " +
                        "FROM mkt.V_HistorialChatWhatsAppControlMensajes WHERE MensajeOfensivo = 0 AND Numero=@numero Order by FechaModificacion Asc";
                    }
                    else
                    {
                        query = "SELECT Numero, Tipo, Mensaje, IdPersonal, ISNULL(IdAlumno,0) IdAlumno,IdPais, Registro, FechaModificacion AS FechaCreacion, NombrePersonal " +
                        "FROM mkt.V_HistorialChatWhatsAppControlMensajes WHERE MensajeOfensivo = 0 AND IdPersonal=@idPersonal AND Numero=@numero Order by FechaModificacion Asc";
                    }
                }
                var credencialTokenExpiraDB = _dapper.QueryDapper(query, new { idPersonal, numero });
                listaMensajes = JsonConvert.DeserializeObject<List<WhatsAppHistorialMensajesDTO>>(credencialTokenExpiraDB);
                return listaMensajes;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
    }
}
