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
    public class SubNivelCcBO : BaseBO
    {
        public string Nombre { get; set; }
        public string Codigo { get; set; }
        public int IdAreaCc { get; set; }
        public byte[] RowVersion { get; set; }

        //Adicional
        public int Total { get; set; }

        private SubNivelCcRepositorio _repSubNivelCCosto;

        DapperRepository _dapperRepository;
        public List<SubNivelCcBO> SubNivelCCbyAreaCC;
        public List<SubNivelCcBO> SubArea;

        public SubNivelCcBO()
        {
            _dapperRepository = new DapperRepository();
            SubNivelCCbyAreaCC= new List<SubNivelCcBO>();
            SubArea = new List<SubNivelCcBO>();
        }

        public void GetAllSubAreaBs(Paginador paginador, GridFilters filter = null)
        {
            FiltroSubNivelBs filtro = new FiltroSubNivelBs();
            if (filter == null)
            {
                filtro.SubNivel = "_";
                filtro.Skip = paginador.skip;
                filtro.Take = paginador.take;
            }
            else
            {
                filtro.SubNivel = filter.Filters.Where(w => w.Field == "Nombre").FirstOrDefault() == null ? "_" : filter.Filters.Where(w => w.Field == "Nombre").FirstOrDefault().Value;
                filtro.Skip = paginador.skip;
                filtro.Take = paginador.take;
            }
            string _querySubNivelAreaBs = "pla.SP_GetAllSubNivel";
            var querySubNivelAreaBs = _dapperRepository.QuerySPDapper(_querySubNivelAreaBs, filtro);
            SubArea = JsonConvert.DeserializeObject<List<SubNivelCcBO>>(querySubNivelAreaBs);
        }

        public void GetSubNivelCCByAreaCC(int IdAreaCC)
        {
            string _querySubnivelCC = "Select Id,Nombre,Codigo from pla.T_SubNivelCC where Estado=1 and IdAreaCC=@IdAreaCC";
            var querySubnivelCC = _dapperRepository.QueryDapper(_querySubnivelCC,new { IdAreaCC });
            SubNivelCCbyAreaCC = JsonConvert.DeserializeObject<List<SubNivelCcBO>>(querySubnivelCC);
        }


        public SubNivelCcBO(int id)
        {

            _repSubNivelCCosto = new SubNivelCcRepositorio();
            var SubAreaCCosto = _repSubNivelCCosto.FirstById(id);
            if (SubAreaCCosto != null)
            {
                this.Id = SubAreaCCosto.Id;
                this.IdAreaCc = SubAreaCCosto.IdAreaCc;
                this.Nombre = SubAreaCCosto.Nombre;
                this.Codigo = SubAreaCCosto.Codigo;
                this.Estado = SubAreaCCosto.Estado;
                this.UsuarioCreacion = SubAreaCCosto.UsuarioCreacion;
                this.UsuarioModificacion = SubAreaCCosto.UsuarioModificacion;
                this.FechaCreacion = SubAreaCCosto.FechaModificacion;
                this.RowVersion = SubAreaCCosto.RowVersion;
            }

        }
    }
    public class FiltroSubNivelBs 
    {
        #region Atributos

        public string SubNivel { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }
        

        #endregion
    }
}