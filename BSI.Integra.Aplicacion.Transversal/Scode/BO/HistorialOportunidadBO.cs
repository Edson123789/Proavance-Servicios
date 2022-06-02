using BSI.Integra.Aplicacion.Classes;
using BSI.Integra.Persistencia.SCode.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class HistorialOportunidadBO : BaseEntity
    {
        public int IdOportunidad { get; set; }
        public string CentroCosto { get; set; }
        public string FaseFinal { get; set; }
        //public DateTime FechaCreacion { get; set; }
        public string CategoriaDato { get; set; }
        public string FaseMaxima { get; set; }
        public string Grupo { get; set; }

        private DapperRepository _dapperRepository;

        public HistorialOportunidadBO()
        {
            _dapperRepository = new DapperRepository();
        }

        /// <summary>
        /// Obtiene una lista de historial de oportunidades por id contacto
        /// </summary>
        public List<HistorialOportunidadBO> ObtenerHistorialOportunidadPorIdContacto(int idContacto)
        {
            List<HistorialOportunidadBO> historialOportunidad = new List<HistorialOportunidadBO>();
            var _query = string.Empty;
            _query = "SELECT IdOportunidad, CentroCosto, FaseFinal, FechaCreacion, CategoriaDato, FaseMaxima, Grupo FROM[com].[V_ObtenerHistorialOportunidadesPorContacto] where IdContacto = @idContacto AND Estado = 1";
            var historialOportunidadDB = _dapperRepository.QueryDapper(_query, new { idContacto });
            historialOportunidad = JsonConvert.DeserializeObject<List<HistorialOportunidadBO>>(historialOportunidadDB);
            return historialOportunidad;
        }
    }





}
