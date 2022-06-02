using BSI.Integra.Aplicacion.Classes;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.SCode.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Comercial.BO
{
    public class ChatIntegraHistorialAsesorBO : BaseEntity
    {
        public int IdAsesorChatDetalle { get; set; }
        public int? IdPersonal { get; set; }
        public DateTime FechaAsignacion { get; set; }
        public byte[] RowVersion { get; set; }

        private DapperRepository _dapperRepository;
        integraDBContext contexto;
        IntegraRepository<TChatIntegraHistorialAsesor> _repICI;
        public ChatIntegraHistorialAsesorBO() {
            _dapperRepository = new DapperRepository();
            //_dapperRepository = new DapperRepository();
            contexto = new integraDBContext();
            _repICI = new IntegraRepository<TChatIntegraHistorialAsesor>(contexto);
        }

        public int Insertar() {
            TChatIntegraHistorialAsesor _entidad = new TChatIntegraHistorialAsesor()
            {
                IdAsesorChatDetalle = this.IdAsesorChatDetalle,
                IdPersonal = this.IdPersonal,
                FechaAsignacion = this.FechaAsignacion,
                Estado = this.Estado,
                FechaCreacion = this.FechaCreacion,
                FechaModificacion = this.FechaModificacion,
                UsuarioCreacion = this.UsuarioCreacion,
                UsuarioModificacion = this.UsuarioModificacion
            };
            _repICI.Insertar(_entidad);
            return _entidad.Id;
        }
        

        /// <summary>
        /// Obtiene un ChatIntegraHistorialAsesor que sera usado para validaciones en el ChatPW/Signalr
        /// </summary>
        /// <param name="idAsesorChatDetalle"></param>
        /// <returns></returns>
        public ChatIntegraHistorialAsesorBO ObtenerHistoricoDetallesPorAsesorChatDetalle(int idAsesorChatDetalle) {
            ChatIntegraHistorialAsesorBO chatIntegraHistorialAsesor = new ChatIntegraHistorialAsesorBO();
            var _query = string.Empty;
            _query = "SELECT Id AS Id, IdAsesorChatDetalle AS IdAsesorChatDetalle, IdPersonal AS IdPersonal, FechaAsignacion AS FechaAsignacion, Estado AS Estado FROM com.V_TChatIntegraHistorialAsesor_ObtenerParaValidarPersonaAsignadaSignalR WHERE Estado = 1 AND IdAsesorChatDetalle = @idAsesorChatDetalle ORDER BY FechaAsignacion DESC, FechaCreacion DESC";
            var chatIntegraHistorialAsesorDB = _dapperRepository.FirstOrDefault(_query, new { idAsesorChatDetalle });
            chatIntegraHistorialAsesor = JsonConvert.DeserializeObject<ChatIntegraHistorialAsesorBO>(chatIntegraHistorialAsesorDB);
            return chatIntegraHistorialAsesor;
        }

        /// <summary>
        /// Obtiene todos los chats de un asesor
        /// </summary>
        /// <param name="idPersonal"></param>
        /// <returns></returns>
        public List<ChatHistorialAsesor> ObtenerTodoHistorialChatsPorAsesor(int idPersonal)
        {
            List<ChatHistorialAsesor> chatHistorialAsesor = new List<ChatHistorialAsesor>();
            var _query = string.Empty;
            _query = "SELECT IdInteraccionChat AS IdInteraccionChat, IdAlumno AS IdAlumno,NombreAlumno AS NombreAlumno,IdAsesor AS IdAsesor,FechaFin AS FechaFin,Ubicacion AS Ubicacion,Mensajes AS Mensajes,Chatsession AS Chatsession,IdPersonal AS IdPersonal FROM [com].[V_ObtenerHistorialChatsPorAsesor] WHERE  IdPersonal = @idPersonal ORDER BY FechaFin DESC";
            var chatIntegraHistorialAsesorDB = _dapperRepository.QueryDapper(_query, new { idPersonal });
            chatHistorialAsesor = JsonConvert.DeserializeObject<List<ChatHistorialAsesor>>(chatIntegraHistorialAsesorDB);

            return chatHistorialAsesor;
        }

        /// Autor: Jose Villena.
        /// Fecha: 23/05/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los chats de un asesor soporte
        /// </summary>
        /// <param name="idPersonal"> Id de Personal </param>
        /// <returns> Lista de Objeto : List<ChatHistorialAsesor> </returns>
        public List<ChatHistorialAsesor> ObtenerTodoHistorialChatsPorAsesorSoporte(int idPersonal)
        {
            List<ChatHistorialAsesor> chatHistorialAsesorSoporte = new List<ChatHistorialAsesor>();
            var query = string.Empty;
            query = "SELECT IdInteraccionChat, IdAlumno ,NombreAlumno ,IdAsesor,FechaFin ,Ubicacion ,Mensajes ,Chatsession ,IdPersonal,Leido FROM [com].[V_ObtenerHistorialChatsPorAsesorSoporte] WHERE  IdPersonal = @idPersonal ORDER BY FechaFin DESC";
            var chatIntegraHistorialAsesorDB = _dapperRepository.QueryDapper(query, new { idPersonal });
            chatHistorialAsesorSoporte = JsonConvert.DeserializeObject<List<ChatHistorialAsesor>>(chatIntegraHistorialAsesorDB);

            return chatHistorialAsesorSoporte;
        }

        /// Autor: Jose Villena.
        /// Fecha: 23/05/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los chats de un asesor soporte
        /// </summary>
        /// <param name="IdAlumno"> Id de Alumno </param>
        /// <returns> Lista de Objeto : List<ChatHistorialAsesor> </returns>
        public List<ChatHistorialAsesor> ObtenerTodoHistorialChatsPorAlumno(int idAlumno)
        {
            List<ChatHistorialAsesor> chatHistorialAsesorSoporte = new List<ChatHistorialAsesor>();
            var query = string.Empty;
            query = "SELECT IdInteraccionChat, IdAlumno ,NombreAlumno ,IdAsesor,FechaFin ,Ubicacion ,Mensajes ,Chatsession ,IdPersonal,Leido FROM [com].[V_ObtenerHistorialChatsPorAsesorSoporte] WHERE  IdAlumno = @idAlumno ORDER BY FechaFin DESC";
            var chatIntegraHistorialAsesorDB = _dapperRepository.QueryDapper(query, new { idAlumno });
            chatHistorialAsesorSoporte = JsonConvert.DeserializeObject<List<ChatHistorialAsesor>>(chatIntegraHistorialAsesorDB);

            return chatHistorialAsesorSoporte;
        }

        public class ChatHistorialAsesor {
            public int IdInteraccionChat { get; set; }
            public int? IdAlumno { get; set; }
            public string NombreAlumno { get; set; }
            public int? IdAsesor { get; set; }
            public DateTime? FechaFin { get; set; }
            public string Tiempo { get; set; }
            public string Ubicacion { get; set; }
            public string Mensajes { get; set; }
            public Guid Chatsession { get; set; }
            public bool Leido { get; set; }
        }

    }
}
