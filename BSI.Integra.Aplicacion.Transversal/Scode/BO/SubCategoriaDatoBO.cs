using BSI.Integra.Aplicacion.Classes;
using BSI.Integra.Persistencia.SCode.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class SubCategoriaDatoBO : BaseEntity
    {
        public int? IdCategoriaOrigen { get; set; }
        public int IdTipoFormulario { get; set; }
        public byte[] RowVersion { get; set; }

        private DapperRepository _dapperRepository;
        public SubCategoriaDatoBO()
        {
            _dapperRepository = new DapperRepository();
        }

        public List<SubCategoriaDatoBO> GetAll()
        {
            var _query = "SELECT Id, IdCategoriaOrigen, IdTipoFormulario FROM mkt.T_SubCategoriaDato WHERE Estado = 1";
            var SubCategoriaDatoDB = _dapperRepository.QueryDapper(_query,null);
            List<SubCategoriaDatoBO> SubCategoriasDatos = JsonConvert.DeserializeObject<List<SubCategoriaDatoBO>>(SubCategoriaDatoDB);
            if (SubCategoriasDatos.Count > 0 && SubCategoriasDatos != null)
            {
                return SubCategoriasDatos;
            }
            else
            {
                return null;
            }
        }
    }
}
