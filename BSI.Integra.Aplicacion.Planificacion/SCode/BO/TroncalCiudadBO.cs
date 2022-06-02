using BSI.Integra.Aplicacion.Classes;
using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.SCode.Repository;
using System.Linq;
using BSI.Integra.Aplicacion.Planificacion.Repositorio;

namespace BSI.Integra.Aplicacion.Planificacion.BO
{
    public class TroncalCiudadBO : BaseEntity
    {
        public string Nombre { get; set; }
        public int? IdTroncalPais { get; set; }
        public byte[] RowVersion { get; set; }



        DapperRepository _dapperRepository;
        public List<TroncalCiudadBO> TroncalCiudad;
        public TroncalCiudadBO()
        {
            _dapperRepository = new DapperRepository();
            TroncalCiudad = new List<TroncalCiudadBO>();
        }

        //public void GetAllTroncalCiudad()
        //{
        //    string _queryCiudad = "Select Id,Nombre From pla.T_TroncalCiudad Where Estado=1";
        //    var queryCiudad = _dapperRepository.QueryDapper(_queryCiudad);
        //    TroncalCiudad = JsonConvert.DeserializeObject<List<TroncalCiudadBO>>(queryCiudad);
        //}
    }
}
