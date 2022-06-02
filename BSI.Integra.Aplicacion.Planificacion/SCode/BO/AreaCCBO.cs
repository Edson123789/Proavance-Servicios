using System;
using System.Collections.Generic;
using System.Text;
using BSI.Integra.Aplicacion.Classes;
using BSI.Integra.Persistencia.SCode.Repository;
using Newtonsoft.Json;
using System.Linq;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Planificacion.Repositorio;
using BSI.Integra.Aplicacion.Base.BO;

namespace BSI.Integra.Aplicacion.Planificacion.BO
{
    public class AreaCcBO : BaseBO
    {
        public string Nombre { get; set; }
        public string Codigo { get; set; }
        public int? IdMigracion { get; set; }
        //Adicional
        public int Total { get; set; }

        private AreaCcRepositorio _repAreaCCosto;


        DapperRepository _dapperRepository;
        public List<AreaCcBO> AreaCC;
        public List<AreaCcBO> AreaBs;

        public AreaCcBO()
        {
            _dapperRepository = new DapperRepository();
            AreaCC = new List<AreaCcBO>();
            AreaBs = new List<AreaCcBO>();
        }

        public void GetAllAreaBs(Paginador paginador, GridFilters filter = null)
        {
            FiltroAreaBs filtro = new FiltroAreaBs();
            if (filter == null)
            {
                filtro.Area = "_";
                filtro.Skip = paginador.skip;
                filtro.Take = paginador.take;
            }
            else
            {
                filtro.Area = filter.Filters.Where(w => w.Field == "Nombre").FirstOrDefault() == null ? "_" : filter.Filters.Where(w => w.Field == "Nombre").FirstOrDefault().Value;
                filtro.Skip = paginador.skip;
                filtro.Take = paginador.take;
            }
            string _queryAreaBs = "pla.SP_GetAllAreasBS";
            var queryAreaBs = _dapperRepository.QuerySPDapper(_queryAreaBs, filtro);
            AreaBs = JsonConvert.DeserializeObject<List<AreaCcBO>>(queryAreaBs);
        }

        public void GetAllAreaCC()
        {
            string _queryAreaCC = "Select Id,Nombre,Concat(Id,'-',Codigo)as Codigo from pla.T_AreaCC Where Estado=1";
            var queryAreaCC = _dapperRepository.QueryDapper(_queryAreaCC, null);
            AreaCC = JsonConvert.DeserializeObject<List<AreaCcBO>>(queryAreaCC);
        }


        public AreaCcBO(int id)
        {

            _repAreaCCosto = new AreaCcRepositorio();
            var AreaCCosto = _repAreaCCosto.FirstById(id);
            if (AreaCCosto != null)
            {
                this.Id = AreaCCosto.Id;
                this.Nombre = AreaCCosto.Nombre;
                this.Codigo = AreaCCosto.Codigo;
                this.Estado = AreaCCosto.Estado;
                this.UsuarioCreacion = AreaCCosto.UsuarioCreacion;
                this.UsuarioModificacion = AreaCCosto.UsuarioModificacion;
                this.FechaCreacion = AreaCCosto.FechaModificacion;
                this.RowVersion = AreaCCosto.RowVersion;
            }

        }
        public class FiltroAreaBs
        {
            public string Area { get; set; }
            public int Skip { get; set; }
            public int Take { get; set; }

        }
    }
}
