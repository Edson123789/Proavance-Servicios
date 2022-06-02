using System;
using System.Collections.Generic;
using System.Text;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Planificacion.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Planificacion.SCode.Repositorio
{
    public class FrecuenciaFeriadoRepositorio: BaseRepository<TFeriadoFrecuencia,FrecuenciaFeriadoBO>
    {

        public List<FrecuenciaFeriadoFiltroDTO> ObtenerFrecuenciaFeriado()
        {
            try
            {
                List<FrecuenciaFeriadoFiltroDTO> tipoFiltro = new List<FrecuenciaFeriadoFiltroDTO>();
                string _queryFrecuencia = string.Empty;
                _queryFrecuencia = "SELECT CodigoFrecuencia,NombreFrecuencia FROM pla.V_FeriadoFrecuencia_Filtro WHERE EstadoFrecuencia=1";
                var queryTroncal = _dapper.QueryDapper(_queryFrecuencia, null);
                if (!string.IsNullOrEmpty(queryTroncal) && !queryTroncal.Contains("[]"))
                {
                    tipoFiltro = JsonConvert.DeserializeObject<List<FrecuenciaFeriadoFiltroDTO>>(queryTroncal);
                }
                return tipoFiltro;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);

            }
        }
    }
}
