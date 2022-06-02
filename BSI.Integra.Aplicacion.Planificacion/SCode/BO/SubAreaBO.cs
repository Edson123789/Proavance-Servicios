using System;
using System.Collections.Generic;
using System.Text;
using BSI.Integra.Persistencia.SCode.Repository;
using System.Linq;
using Newtonsoft.Json;
using BSI.Integra.Aplicacion.Classes;
namespace BSI.Integra.Aplicacion.Planificacion.BO
{
    public class SubAreaBO : BaseEntity
    {
        public int IdArea { get; set; }
        public string Nombre { get; set; }
        public byte[] RowVersion { get; set; }

        DapperRepository _dapperRepository;
        public List<SubAreaBO> SubAreaByArea;
        public SubAreaBO()
        {
            _dapperRepository = new DapperRepository();
            SubAreaByArea = new List<SubAreaBO>();

        }

        public void GetSubAreaByArea (int IdArea)
        {
            string _querySubArea = "Select Id,Nombre From pla.T_SubArea Where IdArea=@IdArea";
            var querySubArea = _dapperRepository.QueryDapper(_querySubArea,new { IdArea});
            SubAreaByArea = JsonConvert.DeserializeObject<List<SubAreaBO>>(querySubArea);

        }
    }
}
