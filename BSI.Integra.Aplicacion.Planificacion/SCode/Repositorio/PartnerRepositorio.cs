using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BSI.Integra.Aplicacion.Planificacion.Repositorio
{
    public class PartnerRepositorio : BaseRepository<TSubAreaCapacitacion, SubAreaCapacitacionBO>
    {
        #region Metodos Base
        public PartnerRepositorio() : base()
        {
        }
        public PartnerRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        #endregion

        /// Autor: Cesar Santillana
        /// Fecha: 08/07/2021
        /// Version: 1.0
        /// <summary>
        /// Función que trae una lista de partners para los filtros.
        /// </summary>
        /// <returns>Retorma una lista List<PartnerExtraFiltroDTO> </returns>

        public List<PartnerExtraFiltroDTO> ObtenerPartner()
        {
            try
            {
                List<PartnerExtraFiltroDTO> programasGenerales = new List<PartnerExtraFiltroDTO>();
                var query = string.Empty;
                query = "SELECT Id, Nombre FROM pla.T_Partner_PW WHERE Estado = 1";
                var pgeneralDB = _dapper.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(pgeneralDB) && !pgeneralDB.Contains("[]"))
                {
                    programasGenerales = JsonConvert.DeserializeObject<List<PartnerExtraFiltroDTO>>(pgeneralDB);
                }
                return programasGenerales;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

    }
}
