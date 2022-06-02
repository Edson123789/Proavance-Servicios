using BSI.Integra.Aplicacion.Classes;
using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.SCode.Repository;
using System.Linq;

namespace BSI.Integra.Aplicacion.Planificacion.BO
{
    public class OrigenProgramaBO: BaseEntity
    {
        public string Descripcion { get; set; }
        public byte[] RowVersion { get; set; }

        DapperRepository _dapperRepository;
        public List<OrigenProgramaDTO> OrigenPrograma;
        public OrigenProgramaBO()
        {
            _dapperRepository = new DapperRepository();
            OrigenPrograma = new List<OrigenProgramaDTO>();
        }

        public void GetOrigenPrograma()
        {
            string _queryOrigenPrograma = "Select Id,Nombre From pla.V_DatosOrigenPrograma Where Estado=1";
            var queryOrigenPrograma = _dapperRepository.QueryDapper(_queryOrigenPrograma, null);
            OrigenPrograma = JsonConvert.DeserializeObject<List<OrigenProgramaDTO>>(queryOrigenPrograma);
        }

    }
    public class OrigenProgramaDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
    }
}
