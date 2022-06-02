using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.Classes;
using BSI.Integra.Persistencia.SCode.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class TipoDatoBO : BaseBO
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int Prioridad { get; set; }

        public List<TipoDatoBO> ListaTipoDato;
        private DapperRepository _dapperRepository;
        public TipoDatoBO()
        {
            _dapperRepository = new DapperRepository();
            ListaTipoDato = new List<TipoDatoBO>();
        }
    }
}
