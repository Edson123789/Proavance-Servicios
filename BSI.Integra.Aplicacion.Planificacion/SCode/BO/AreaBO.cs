using System;
using System.Collections.Generic;
using System.Text;
using BSI.Integra.Aplicacion.Classes;
using BSI.Integra.Persistencia.SCode.Repository;
using System.Linq;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Planificacion.BO
{
    public class AreaBO : BaseEntity
    {
        public string Nombre { get; set; }
        public byte[] RowVersion { get; set; }

        DapperRepository _dapperRepository;
        public List<AreaBO> Area;
        public AreaBO()
        {
            _dapperRepository = new DapperRepository();
            Area = new List<AreaBO>();
        }
    }
}
