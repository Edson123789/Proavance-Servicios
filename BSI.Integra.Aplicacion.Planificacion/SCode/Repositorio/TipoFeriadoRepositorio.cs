using System;
using System.Collections.Generic;
using System.Text;
using BSI.Integra.Aplicacion.Planificacion.BO;
using BSI.Integra.Persistencia.Repository;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Aplicacion.DTOs;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Planificacion.Repositorio
{
    public class TipoFeriadoRepositorio : BaseRepository< TFeriadoTipo,TipoFeriadoBO >
    {

        public List<TipoFeriadoFiltroDTO> ObtenerTipoFeriado()
        {
            try
            {
                List<TipoFeriadoFiltroDTO> tipoFiltro = new List<TipoFeriadoFiltroDTO>();
                string _queryTipo = string.Empty;
                _queryTipo = "SELECT CodigoTipo,NombreTipo FROM pla.V_FeriadoTipo_Filtro WHERE EstadoTipo = 1";
                var queryTroncal = _dapper.QueryDapper(_queryTipo, null);
                if (!string.IsNullOrEmpty(queryTroncal) && !queryTroncal.Contains("[]"))
                {
                    tipoFiltro = JsonConvert.DeserializeObject<List<TipoFeriadoFiltroDTO>>(queryTroncal);
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
