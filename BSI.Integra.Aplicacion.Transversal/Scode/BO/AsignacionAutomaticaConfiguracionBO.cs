using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.Classes;
using BSI.Integra.Persistencia.SCode.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class AsignacionAutomaticaConfiguracionBO : BaseBO
    {
        public int IdFaseOportunidad { get; set; }//IdFase 
        public int? IdTipoDato { get; set; }
        public int? IdOrigen { get; set; }
        public bool Inclusivo { get; set; }
        public bool Habilitado { get; set; }

        private DapperRepository _dapperRepository;

        public AsignacionAutomaticaConfiguracionBO() {
            _dapperRepository = new DapperRepository();
        }
    }
}