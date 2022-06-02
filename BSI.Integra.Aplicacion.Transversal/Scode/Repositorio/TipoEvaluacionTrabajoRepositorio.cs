using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    public class TipoEvaluacionTrabajoRepositorio : BaseRepository<TConfigurarVideoPrograma, ConfigurarVideoProgramaBO>
    {
        #region Metodos Base
        public TipoEvaluacionTrabajoRepositorio() : base()
        {
        }
        public TipoEvaluacionTrabajoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }

        #endregion

        public List<listaTipoEvaluacionTrabajoBO> listaTipoEvaluacionTrabajo()
        {
            List<listaTipoEvaluacionTrabajoBO> rpta = new List<listaTipoEvaluacionTrabajoBO>();
            string _query = "Select Id,Nombre From pla.T_TipoEvaluacionTrabajo Where Estado=1";
            string query = _dapper.QueryDapper(_query, null);
            if (!string.IsNullOrEmpty(query) && !query.Contains("null"))
            {
                rpta = JsonConvert.DeserializeObject<List<listaTipoEvaluacionTrabajoBO>>(query);
                return rpta;
            }
            else
            {
                return null;
            }
        }
    }
}
