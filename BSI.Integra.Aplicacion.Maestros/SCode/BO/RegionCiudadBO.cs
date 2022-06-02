using BSI.Integra.Aplicacion.Classes;
using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.SCode.Repository;
using System.Linq;

namespace BSI.Integra.Aplicacion.Maestros.BO
{
    public class RegionCiudadBO : BaseEntity
    {
        public string Nombre { get; set; }
        public int IdCiudad { get; set; }
        public int IdPais { get; set; }
        public int? CodigoBs { get; set; }
        public string DenominacionBs { get; set; }
        public string NombreCorto { get; set; }
        public byte[] RowVersion { get; set; }

        DapperRepository _dapperRepository;
        public List<RegionCiudadBO> LocacionTroncal;
        public RegionCiudadBO()
        {
            _dapperRepository = new DapperRepository();
            LocacionTroncal = new List<RegionCiudadBO>();
        }

        public void GetAllLocacionForTroncal()
        {
            string _queryLocacionTroncal = "SELECT distinct RC.Id,RC.Nombre,RC.IdCiudad,RC.CodigoBS,RC.DenominacionBS FROM conf.T_RegionCiudad AS RC" +
                                           " INNER JOIN pla.T_Locacion AS LO on LO.IdRegion = RC.Id and LO.ESTADO = 1 where RC.ESTADO = 1";
            var queryLocacionTroncal = _dapperRepository.QueryDapper(_queryLocacionTroncal,null);
            LocacionTroncal = JsonConvert.DeserializeObject<List<RegionCiudadBO>>(queryLocacionTroncal);

        }
    }
}
