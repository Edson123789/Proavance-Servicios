using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Persistencia.SCode.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace BSI.Integra.Aplicacion.Comercial.BO
{
    public class AsesorChatDetalleBO : BaseBO
    {

        public int Id { get; set; }
        public int? IdAsesorChat { get; set; }
        public int IdPais { get; set; }
        public int IdPgeneral { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }




        private DapperRepository _dapperRepository;
        public AsesorChatDetalleBO() {
            _dapperRepository = new DapperRepository();
        }


        /// <summary>
        /// Obtiene un AsesorChatDetalle filtrado por pais y programa general
        /// </summary>
        /// <param name="idPais"></param>
        /// <param name="idProgramaGeneral"></param>
        /// <returns></returns>
        public AsesorChatDetalleBO ObtenerAsesorChatDetallePorPaisyProgramaGeneral(int idPais, int idProgramaGeneral) {
            AsesorChatDetalleBO asesorChatDetalleBO = new AsesorChatDetalleBO();
            var _query = string.Empty;
            _query = "SELECT Id AS Id,IdAsesorChat AS IdAsesorChat,IdPais AS IdPais,IdPGeneral AS IdPGeneral FROM com.V_TAsesorChatDetalle_ObtenerParaValidarPorChatSignalR WHERE  Estado = 1  AND IdPais = @idPais and IdPGeneral = @idProgramaGeneral";
            var asesorChatDetalleDB = _dapperRepository.FirstOrDefault(_query,new { idPais, idProgramaGeneral});
            asesorChatDetalleBO = JsonConvert.DeserializeObject<AsesorChatDetalleBO>(asesorChatDetalleDB);
            return asesorChatDetalleBO;
        }


        /// <summary>
        /// Obtiene la lista de programas asignados por asesor
        /// </summary>
        /// <param name="idPersonal"></param>
        public List<AsesorChatDetalleBO> ObtenerListaProgramasAsignadosPorAsesor(int idPersonal)
        {
            List<AsesorChatDetalleBO> asesorChats = new List<AsesorChatDetalleBO>();
            string _query = string.Empty;
            _query = "select AC.Id,IdPais,IdPGeneral from com.T_AsesorChat as ac inner join com.T_AsesorChatDetalle  as acd on ac.id = acd.IdAsesorChat where AC.ESTADO = 1 and IdPersonal = @idPersonal";
            var asesorChatsDB = _dapperRepository.QueryDapper(_query, new { idPersonal });
            asesorChats = JsonConvert.DeserializeObject<List<AsesorChatDetalleBO>>(asesorChatsDB);
            return asesorChats;
        }
    }
}
