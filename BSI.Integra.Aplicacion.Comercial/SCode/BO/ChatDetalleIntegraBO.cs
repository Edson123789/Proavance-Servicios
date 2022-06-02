using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.Classes;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.SCode.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Comercial.BO
{
    ///BO: ChatDetalleIntegraBO
    ///Autor: , Edgar S.
    ///Fecha: 10/03/2021
    ///<summary>
    ///Columnas y funciones de la tabla T_ChatDetalleIntegra
    ///</summary>
    public class ChatDetalleIntegraBO : BaseBO
    {
        ///Propiedades		                      Significado
        ///-------------	                      -----------------------
        /// IdInteraccionChatIntegra              Id de Interacción de chat integra
        /// NombreRemitente                       Nombre de remitente
        /// IdRemitente                           Identificador de remitente
        /// Mensaje                               Mensaje
        /// Fecha                                 Fecha de mensaje
        /// MensajeOfensivo                       Validación de mensaje ofensivo
        /// IdChatDetalleIntegraArchivo           FK de T_ChatDetalleIntegraArchivo
        public int? IdInteraccionChatIntegra { get; set; }
        public string NombreRemitente { get; set; }
        public string IdRemitente { get; set; }
        public string Mensaje { get; set; }
        public DateTime Fecha { get; set; }
        public int NroMensajesSinLeer { get; set; }
        public byte[] RowVersion { get; set; }
        public bool? MensajeOfensivo { get; set; }
        public int? IdChatDetalleIntegraArchivo { get; set; }

        private DapperRepository _dapperRepository;
        IntegraRepository<TChatDetalleIntegra> _repCDI;

        public ChatDetalleIntegraBO()
        {
            _dapperRepository = new DapperRepository();
            _repCDI = new IntegraRepository<TChatDetalleIntegra>();
        }

        public int Insertar() {
            TChatDetalleIntegra _entidad = new TChatDetalleIntegra()
            {
                //Id = this.Id,
                IdInteraccionChatIntegra = this.IdInteraccionChatIntegra,
                NombreRemitente = this.NombreRemitente,
                IdRemitente = this.IdRemitente,
                Mensaje = this.Mensaje,
                Fecha = this.Fecha,
                Estado = this.Estado,
                FechaCreacion = this.FechaCreacion,
                FechaModificacion = this.FechaModificacion,
                UsuarioCreacion = this.UsuarioCreacion,
                UsuarioModificacion = this.UsuarioModificacion
            };
            _repCDI.Insertar(_entidad);
            return _entidad.Id;
        }


        /// <summary>
        /// Obtiene un listado de ChatDetalleIntegra filtrado por idInteraccion
        /// </summary>
        /// <param name="idInteraccion"></param>
        /// <returns> Lista de Objeto BO : List<ChatDetalleIntegraBO> </returns>
        public List<ChatDetalleIntegraBO> ObtenerDetalleChatPorIdInteraccion(int idInteraccion)
        {
            List<ChatDetalleIntegraBO> chatDetallesIntegra = new List<ChatDetalleIntegraBO>();
            var query = string.Empty;
            query = "select DET.NombreRemitente ,DET.Mensaje,DET.Fecha,DET.IdRemitente  from com.T_ChatDetalleIntegra DET where DET.IdInteraccionChatIntegra=@idInteraccion order by DET.IdInteraccionChatIntegra , fecha asc ";
            var chatDetallesIntegraDB = _dapperRepository.QueryDapper(query, new { idInteraccion });
            chatDetallesIntegra = JsonConvert.DeserializeObject<List<ChatDetalleIntegraBO>>(chatDetallesIntegraDB);
            return chatDetallesIntegra;
        }
        
        /// <summary>
        /// Obtiene historial de chat para pantalla2
        /// </summary>
        /// <param name="IdPersonal"></param>
        /// <param name="IdAlumno"></param>
        /// <returns></returns>
        public List<HistorialChatRecibidosDTO> ObtenerHisotrialChatRecibidos(int IdPersonal, int IdAlumno)
        {
            List<HistorialChatRecibidosDTO> ChatDetallesIntegra = new List<HistorialChatRecibidosDTO>();
            var _query = string.Empty;
            _query = "select top 1 DET.NombreRemitente ,DET.Mensaje,DET.Fecha,DET.IdInteraccionChatIntegra as IdInteraccionChat,DET.IdAsesor,DET.Ubicacion,DET.IdChatSession as ChatSession  from com.V_HistorialChatPortal DET where DET.IdAsesor=@IdPersonal and IdAlumno=@IdAlumno  order by DET.IdInteraccionChatIntegra , fecha desc ";
            var ChatDetallesIntegraDB = _dapperRepository.QueryDapper(_query, new { IdPersonal , IdAlumno});
            ChatDetallesIntegra = JsonConvert.DeserializeObject<List<HistorialChatRecibidosDTO>>(ChatDetallesIntegraDB);
            return ChatDetallesIntegra;
        }

        /// <summary>
        /// Obtiene un listado de ChatDetalleIntegra filtrado por idInteraccionChatIntegra
        /// </summary>
        /// <param name="idsInteraccionChatIntegra"></param>
        /// <returns></returns>
        public List<ChatDetalleIntegraBO> ObtenerChatDetalleIntegraPorIdsInteraccionChatIntegra(string idsInteraccionChatIntegra)
        {
            List<ChatDetalleIntegraBO> ChatDetallesIntegra = new List<ChatDetalleIntegraBO>();
            var _query = string.Empty;
			_query = "select * from com.T_ChatDetalleIntegra where IdInteraccionChatIntegra in " + idsInteraccionChatIntegra;
			//_query = "select * from com.T_ChatDetalleIntegra where IdInteraccionChatIntegra in @idsInteraccionChatIntegra";
			//var ChatDetallesIntegraDB = _dapperRepository.QueryDapper(_query, new { idsInteraccionChatIntegra });
			var ChatDetallesIntegraDB = _dapperRepository.QueryDapper(_query, null);
            ChatDetallesIntegra = JsonConvert.DeserializeObject<List<ChatDetalleIntegraBO>>(ChatDetallesIntegraDB);
            return ChatDetallesIntegra;
        }


        /// Autor: Edgar S.
        /// Fecha: 27/04/2021
        /// Versión: 1.1
        /// <summary>
        /// Obtiene un listado de ChatDetalleIntegra filtrado por idInteraccion
        /// </summary>
        /// <param name="idInteraccion"> Id de Interacción </param>
        /// <returns> Lista de Objeto BO : List<ChatDetalleIntegraBO> </returns>
        public List<ChatDetalleIntegraBO> ObtenerDetalleChatPorIdInteraccionControlMensajes(int idInteraccion)
        {
            List<ChatDetalleIntegraBO> chatDetallesIntegra = new List<ChatDetalleIntegraBO>();
            var query = string.Empty;
            query = "SELECT NombreRemitente, Mensaje, Fecha, IdRemitente  FROM com.V_ObtenerChatDetallePorInteraccion WHERE IdInteraccionChatIntegra = @idInteraccion ORDER BY IdInteraccionChatIntegra , Fecha ASC";
            var chatDetallesIntegraDB = _dapperRepository.QueryDapper(query, new { idInteraccion });
            chatDetallesIntegra = JsonConvert.DeserializeObject<List<ChatDetalleIntegraBO>>(chatDetallesIntegraDB);
            return chatDetallesIntegra;
        }

        /// Autor: Jose Villena.
        /// Fecha: 23/05/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene un listado de ChatDetalleIntegra filtrado por IdAlumno
        /// </summary>
        /// <param name="idAlumno"> Id de Alumno </param>
        /// <returns> Lista de Objeto BO : List<ChatDetalleIntegraBO> </returns>
        public List<ChatDetalleIntegraBO> ObtenerDetalleChatPorIdInteraccionControlMensajesSoporte(int idAlumno)
        {
            List<ChatDetalleIntegraBO> detalleChatSoporte = new List<ChatDetalleIntegraBO>();
            var detalleChatSoporteDB = _dapperRepository.QuerySPDapper("pla.SP_ObtenerDetalleChatSoporteIdAlumnoArchivoAdjunto", new { IdAlumno = idAlumno });
            if (!string.IsNullOrEmpty(detalleChatSoporteDB) && !detalleChatSoporteDB.Contains("[]"))
            {
                detalleChatSoporte = JsonConvert.DeserializeObject<List<ChatDetalleIntegraBO>>(detalleChatSoporteDB);
            }
            return detalleChatSoporte;
        }
    }
}
