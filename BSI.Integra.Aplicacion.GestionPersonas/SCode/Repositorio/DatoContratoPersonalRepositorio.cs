using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.DTOs.Scode.DTOs.GestionPersonas;
using BSI.Integra.Aplicacion.GestionPersonas.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.GestionPersonas.Repositorio
{
    /// Repositorio: DatoContratoPersonalRepositorio
    /// Autor: Britsel Calluchi - Luis Huallpa - Edgar Serruto .
    /// Fecha: 16/06/2021
    /// <summary>
    /// Repositorio para de tabla T_DatoContratoPersonal
    /// </summary>
    public class DatoContratoPersonalRepositorio : BaseRepository<TDatoContratoPersonal, DatoContratoPersonalBO>
    {
        #region Metodos Base
        public DatoContratoPersonalRepositorio() : base()
        {
        }
        public DatoContratoPersonalRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<DatoContratoPersonalBO> GetBy(Expression<Func<TDatoContratoPersonal, bool>> filter)
        {
            IEnumerable<TDatoContratoPersonal> listado = base.GetBy(filter);
            List<DatoContratoPersonalBO> listadoBO = new List<DatoContratoPersonalBO>();
            foreach (var itemEntidad in listado)
            {
                DatoContratoPersonalBO objetoBO = Mapper.Map<TDatoContratoPersonal, DatoContratoPersonalBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public DatoContratoPersonalBO FirstById(int id)
        {
            try
            {
                TDatoContratoPersonal entidad = base.FirstById(id);
                DatoContratoPersonalBO objetoBO = new DatoContratoPersonalBO();
                Mapper.Map<TDatoContratoPersonal, DatoContratoPersonalBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public DatoContratoPersonalBO FirstBy(Expression<Func<TDatoContratoPersonal, bool>> filter)
        {
            try
            {
                TDatoContratoPersonal entidad = base.FirstBy(filter);
                DatoContratoPersonalBO objetoBO = Mapper.Map<TDatoContratoPersonal, DatoContratoPersonalBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(DatoContratoPersonalBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TDatoContratoPersonal entidad = MapeoEntidad(objetoBO);

                bool resultado = base.Insert(entidad);
                if (resultado)
                    AsignacionId(entidad, objetoBO);

                return resultado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(IEnumerable<DatoContratoPersonalBO> listadoBO)
        {
            try
            {
                foreach (var objetoBO in listadoBO)
                {
                    bool resultado = Insert(objetoBO);
                    if (resultado == false)
                        return false;
                }

                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool Update(DatoContratoPersonalBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TDatoContratoPersonal entidad = MapeoEntidad(objetoBO);

                bool resultado = base.Update(entidad);
                if (resultado)
                    AsignacionId(entidad, objetoBO);

                return resultado;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool Update(IEnumerable<DatoContratoPersonalBO> listadoBO)
        {
            try
            {
                foreach (var objetoBO in listadoBO)
                {
                    bool resultado = Update(objetoBO);
                    if (resultado == false)
                        return false;
                }

                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        private void AsignacionId(TDatoContratoPersonal entidad, DatoContratoPersonalBO objetoBO)
        {
            try
            {
                if (entidad != null && objetoBO != null)
                {
                    objetoBO.Id = entidad.Id;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        private TDatoContratoPersonal MapeoEntidad(DatoContratoPersonalBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TDatoContratoPersonal entidad = new TDatoContratoPersonal();
                entidad = Mapper.Map<DatoContratoPersonalBO, TDatoContratoPersonal>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos

                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        #endregion

        /// <summary>
        /// Obtiene la lista de todos los elementos en la tabla DatoContratoPersonal
        /// </summary>
        /// <returns></returns>
        public List<DatoContratoPersonalDTO> ObtenerContratosRegistrados()
        {
            try
            {
                return this.GetBy(x => x.Estado == true).Select(x => new DatoContratoPersonalDTO
                {
                    Id = x.Id,
                    IdPersonal = x.IdPersonal,
                    FechaInicio = x.FechaInicio,
                    FechaFin = x.FechaFin,
                    RemuneracionFija = x.RemuneracionFija
                }).ToList(); ;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
		/// Obtiene informacion de contrato por filtro
		/// </summary>
		/// <param name="filtro"></param>
		/// <returns></returns>
		public List<DatoContratoPersonalFiltroDTO> ObtenerContratoInformacion(ContratoFiltroDTO filtro)
        {
            try
            {

                var filtros = new
                {
                    ListaPersonalAreaTrabajo = filtro.ListaPersonalAreaTrabajo == null ? "" : string.Join(",", filtro.ListaPersonalAreaTrabajo.Select(x => x)),
                    ListaPuestoTrabajo = filtro.ListaPuestoTrabajo == null ? "" : string.Join(",", filtro.ListaPuestoTrabajo.Select(x => x)),
                    ListaPersonal = filtro.ListaPersonal == null ? "" : string.Join(",", filtro.ListaPersonal.Select(x => x)),
                    ListaSedeTrabajo = filtro.ListaSedeTrabajo == null ? "" : string.Join(",", filtro.ListaSedeTrabajo.Select(x => x)),
                    FechaInicio = filtro.FechaInicio,
                    FechaFin = filtro.FechaFin,
                    OpcionFecha = filtro.OpcionFecha
                };

                List<DatoContratoPersonalFiltroDTO> contratoFiltro = new List<DatoContratoPersonalFiltroDTO>();
                string query = string.Empty;
                query = "gp.SP_ObtenerContratos";
                var PContratoDB = _dapper.QuerySPDapper(query, filtros);

                if (!string.IsNullOrEmpty(PContratoDB) && !PContratoDB.Contains("[]"))
                {
                    contratoFiltro = JsonConvert.DeserializeObject<List<DatoContratoPersonalFiltroDTO>>(PContratoDB);
                }
                return contratoFiltro;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        /// <summary>
        /// Este método obtiene la lista de contratos historicos de determinado personal
        /// </summary>
        /// <returns></returns>
        public List<ContratoHistoricoRegistroDTO> ObtenerContratoHistorico(int IdPersonal)
        {
            try
            {
                List<ContratoHistoricoRegistroDTO> listaContratoHistorico = new List<ContratoHistoricoRegistroDTO>();
                string _query = string.Empty;
                _query = "SELECT * from [gp].[V_TDatoContratoPersonal_ObtenerHistorico] WHERE IdPersonal = @IdPersonal AND Estado = 1 ORDER BY FechaFin DESC";
                var ContratoDB = _dapper.QueryDapper(_query, new { IdPersonal });
                if (!string.IsNullOrEmpty(ContratoDB) && !ContratoDB.Contains("[]"))
                {
                    listaContratoHistorico = JsonConvert.DeserializeObject<List<ContratoHistoricoRegistroDTO>>(ContratoDB);
                }
                return listaContratoHistorico;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }    
}
