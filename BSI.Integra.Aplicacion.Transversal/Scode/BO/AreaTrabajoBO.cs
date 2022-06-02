using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Persistencia.SCode.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class AreaTrabajoBO : BaseBO
    {
        public string Nombre { get; set; }


        private DapperRepository _dapperRepository;
        public AreaTrabajoBO()
        {
            _dapperRepository = new DapperRepository();
        }
        /// <summary>
        /// Obtiene todos las areas de trabajo para ser usados en Combo Box
        /// </summary>
        /// <returns></returns>
        public List<AreaTrabajoBO> ObtenerTodoAreaTrabajoCB()
        {
            List<AreaTrabajoBO> areaTrabajos = new List<AreaTrabajoBO>();
            var _query = string.Empty;
            _query = "SELECT Id,Nombre FROM pla.T_AreaTrabajo WHERE Estado = 1";
            var areaTrabajoDB = _dapperRepository.QueryDapper(_query,null);
            areaTrabajos = JsonConvert.DeserializeObject<List<AreaTrabajoBO>>(areaTrabajoDB);
            return areaTrabajos;
        }
    }
}
