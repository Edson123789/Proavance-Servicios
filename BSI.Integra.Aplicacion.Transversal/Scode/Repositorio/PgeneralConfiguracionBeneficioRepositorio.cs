using System;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;
using BSI.Integra.Aplicacion.DTOs.Scode.DTOs.Transversal;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    /// Repositorio: PgeneralConfiguracionBeneficioRepositorio
    /// Autor:Jose Villena
    /// Fecha: 03/05/2021
    /// <summary>
    /// Gestión de PgeneralConfiguracionBeneficio
    /// </summary>
    public class PgeneralConfiguracionBeneficioRepositorio : BaseRepository<TConfiguracionBeneficioProgramaGeneral, TConfiguracionBeneficioProgramaGeneralBO>
    {
        #region Metodos Base
        public PgeneralConfiguracionBeneficioRepositorio() : base()
        {
        }
        public PgeneralConfiguracionBeneficioRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<TConfiguracionBeneficioProgramaGeneralBO> GetBy(Expression<Func<TConfiguracionBeneficioProgramaGeneral, bool>> filter)
        {
            IEnumerable<TConfiguracionBeneficioProgramaGeneral> listado = base.GetBy(filter);
            List<TConfiguracionBeneficioProgramaGeneralBO> listadoBO = new List<TConfiguracionBeneficioProgramaGeneralBO>();
            foreach (var itemEntidad in listado)
            {
                TConfiguracionBeneficioProgramaGeneralBO objetoBO = Mapper.Map<TConfiguracionBeneficioProgramaGeneral, TConfiguracionBeneficioProgramaGeneralBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public TConfiguracionBeneficioProgramaGeneralBO FirstById(int id)
        {
            try
            {
                TConfiguracionBeneficioProgramaGeneral entidad = base.FirstById(id);
                TConfiguracionBeneficioProgramaGeneralBO objetoBO = new TConfiguracionBeneficioProgramaGeneralBO();
                Mapper.Map<TConfiguracionBeneficioProgramaGeneral, TConfiguracionBeneficioProgramaGeneralBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public TConfiguracionBeneficioProgramaGeneralBO FirstBy(Expression<Func<TConfiguracionBeneficioProgramaGeneral, bool>> filter)
        {
            try
            {
                TConfiguracionBeneficioProgramaGeneral entidad = base.FirstBy(filter);
                TConfiguracionBeneficioProgramaGeneralBO objetoBO = Mapper.Map<TConfiguracionBeneficioProgramaGeneral, TConfiguracionBeneficioProgramaGeneralBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(TConfiguracionBeneficioProgramaGeneralBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TConfiguracionBeneficioProgramaGeneral entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<TConfiguracionBeneficioProgramaGeneralBO> listadoBO)
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

        public bool Update(TConfiguracionBeneficioProgramaGeneralBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TConfiguracionBeneficioProgramaGeneral entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<TConfiguracionBeneficioProgramaGeneralBO> listadoBO)
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
        private void AsignacionId(TConfiguracionBeneficioProgramaGeneral entidad, TConfiguracionBeneficioProgramaGeneralBO objetoBO)
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

        private TConfiguracionBeneficioProgramaGeneral MapeoEntidad(TConfiguracionBeneficioProgramaGeneralBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TConfiguracionBeneficioProgramaGeneral entidad = new TConfiguracionBeneficioProgramaGeneral();
                entidad = Mapper.Map<TConfiguracionBeneficioProgramaGeneralBO, TConfiguracionBeneficioProgramaGeneral>(objetoBO,
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

        ///Repositorio: PgeneralConfiguracionBeneficioRepositorio
        ///Autor: Jose Villena
        ///Fecha: 03/05/2021
        /// <summary>
        /// Obtener Programa General Configuracion Beneficios
        /// </summary>
        /// <param name="IdPgeneral">Id Programa General </param>
        /// <returns> Lista Configuracion Beneficios: List<PgeneralConfiguracionBeneficioDTO></returns> 
        public List<PgeneralConfiguracionBeneficioDTO> ObtenerPgeneralConfiuracionBeneficios(int IdPgeneral)
        {
            string query = string.Empty;

            try
            {
                List<PgeneralConfiguracionBeneficioDTO> ConfiguracionBeneficio = new List<PgeneralConfiguracionBeneficioDTO>();

                query = "SELECT Id, IdPGeneral,IdBeneficio,OrdenBeneficio,DatosAdicionales,TipoBeneficio,Descripcion,Entrega,Asosiar,AvanceAcademico,DeudaPendiente FROM pla.V_BeniciosPartnerDocumento WHERE IdPGeneral=@IdPgeneral";
                //query = "SELECT Id, IdPGeneral,IdBeneficio,TipoBeneficio,Descripcion,Entrega,Asosiar,AvanceAcademico,DeudaPendiente FROM pla.V_BeniciosPartnerDocumento WHERE IdPGeneral=@IdPgeneral";
                var pgeneralBeneficios = _dapper.QueryDapper(query, new { IdPGeneral = IdPgeneral });
                if (!string.IsNullOrEmpty(pgeneralBeneficios) && !pgeneralBeneficios.Contains("[]") && pgeneralBeneficios != null)
                {
                    ConfiguracionBeneficio = JsonConvert.DeserializeObject<List<PgeneralConfiguracionBeneficioDTO>>(pgeneralBeneficios);

                    foreach (var estados in ConfiguracionBeneficio)
                    {
                        estados.IdEstadoMatricula = new List<ConfiguracionBeneficioProgramaGeneralEstadoMatriculaDTO>();
                        estados.IdSubEstadoMatricula = new List<ConfiguracionBeneficioProgramaGeneralSubEstadoDTO>();
                        estados.IdPais = new List<ConfiguracionBeneficioProgramaGeneralPaisDTO>();
                        estados.IdVersion = new List<ConfiguracionBeneficioProgramaGeneralVersionDTO>();
                        //estados.IdDatoAdicional = new List<ConfiguracionBeneficioProgramaGeneralDatoAdicionalDTO>();

                        string queryEstado = "SELECT IdEstadoMatricula as Id,IdEstadoMatricula FROM pla.T_ConfiguracionBeneficioProgramaGeneralEstadoMatricula WHERE Estado = 1 and IdConfiguracionBeneficioPGneral=@IdConfiguracionBeneficioPGneral";
                        var queryEstadoDB = _dapper.QueryDapper(queryEstado, new { IdConfiguracionBeneficioPGneral = estados.Id });
                        if (!string.IsNullOrEmpty(queryEstadoDB) && !queryEstadoDB.Contains("[]"))
                        {
                            estados.IdEstadoMatricula = JsonConvert.DeserializeObject<List<ConfiguracionBeneficioProgramaGeneralEstadoMatriculaDTO>>(queryEstadoDB);
                        }

                        string querySubEstado = "SELECT IdSubEstadoMatricula AS Id,IdSubEstadoMatricula FROM pla.T_ConfiguracionBeneficioProgramaGeneralSubEstado WHERE Estado = 1 and IdConfiguracionBeneficioPGneral=@IdConfiguracionBeneficioPGneral";
                        var querySubEstadoDB = _dapper.QueryDapper(querySubEstado, new { IdConfiguracionBeneficioPGneral = estados.Id });
                        if (!string.IsNullOrEmpty(querySubEstadoDB) && !querySubEstadoDB.Contains("[]"))
                        {
                            estados.IdSubEstadoMatricula = JsonConvert.DeserializeObject<List<ConfiguracionBeneficioProgramaGeneralSubEstadoDTO>>(querySubEstadoDB);
                        }

                        string queryPais = "SELECT IdPais AS Id,IdPais FROM pla.T_ConfiguracionBeneficioProgramaGeneralPais WHERE Estado = 1 and IdConfiguracionBeneficioPGneral=@IdConfiguracionBeneficioPGneral";
                        var queryPaisDB = _dapper.QueryDapper(queryPais, new { IdConfiguracionBeneficioPGneral = estados.Id });
                        if (!string.IsNullOrEmpty(queryPaisDB) && !queryPaisDB.Contains("[]"))
                        {
                            estados.IdPais = JsonConvert.DeserializeObject<List<ConfiguracionBeneficioProgramaGeneralPaisDTO>>(queryPaisDB);
                        }

                        string queryVersion = "SELECT Id,IdVersionPrograma as IdVersion FROM pla.T_ConfiguracionBeneficioProgramaGeneralVersion WHERE Estado = 1 and IdConfiguracionBeneficioPGneral=@IdConfiguracionBeneficioPGneral";
                        var queryVersionDB = _dapper.QueryDapper(queryVersion, new { IdConfiguracionBeneficioPGneral = estados.Id });
                        if (!string.IsNullOrEmpty(queryVersionDB) && !queryVersionDB.Contains("[]"))
                        {
                            estados.IdVersion = JsonConvert.DeserializeObject<List<ConfiguracionBeneficioProgramaGeneralVersionDTO>>(queryVersionDB);
                        }

                        string queryDatoAdicional = "SELECT IdBeneficioDatoAdicional as Id,IdBeneficioDatoAdicional FROM [pla].[T_ConfiguracionBeneficioProgramaGeneralDatoAdicional]WHERE Estado = 1 and IdConfiguracionBeneficioPGeneral=@IdConfiguracionBeneficioPGeneral";
                        var queryDatoAdicionalDB = _dapper.QueryDapper(queryDatoAdicional, new { IdConfiguracionBeneficioPGeneral = estados.Id });
                        if (!string.IsNullOrEmpty(queryDatoAdicionalDB) && !queryDatoAdicionalDB.Contains("[]"))
                        {
                            estados.IdDatoAdicional = JsonConvert.DeserializeObject<List<ConfiguracionBeneficioProgramaGeneralDatoAdicionalDTO>>(queryDatoAdicionalDB);
                        }
                    }
                }

                return ConfiguracionBeneficio;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

    }
}
