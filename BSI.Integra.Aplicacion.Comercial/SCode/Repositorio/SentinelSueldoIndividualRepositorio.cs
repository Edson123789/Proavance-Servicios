using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.Comercial.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;
using BSI.Integra.Aplicacion.DTOs;
namespace BSI.Integra.Aplicacion.Comercial.Repositorio
{
    public class SentinelSueldoIndividualRepositorio : BaseRepository<TSentinelSueldoIndividual, SentinelSueldoIndividualBO>
    {
        #region Metodos Base
        public SentinelSueldoIndividualRepositorio() : base()
        {
        }
        public SentinelSueldoIndividualRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<SentinelSueldoIndividualBO> GetBy(Expression<Func<TSentinelSueldoIndividual, bool>> filter)
        {
            IEnumerable<TSentinelSueldoIndividual> listado = base.GetBy(filter);
            List<SentinelSueldoIndividualBO> listadoBO = new List<SentinelSueldoIndividualBO>();
            foreach (var itemEntidad in listado)
            {
                SentinelSueldoIndividualBO objetoBO = Mapper.Map<TSentinelSueldoIndividual, SentinelSueldoIndividualBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public SentinelSueldoIndividualBO FirstById(int id)
        {
            try
            {
                TSentinelSueldoIndividual entidad = base.FirstById(id);
                SentinelSueldoIndividualBO objetoBO = new SentinelSueldoIndividualBO();
                Mapper.Map<TSentinelSueldoIndividual, SentinelSueldoIndividualBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public SentinelSueldoIndividualBO FirstBy(Expression<Func<TSentinelSueldoIndividual, bool>> filter)
        {
            try
            {
                TSentinelSueldoIndividual entidad = base.FirstBy(filter);
                SentinelSueldoIndividualBO objetoBO = Mapper.Map<TSentinelSueldoIndividual, SentinelSueldoIndividualBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(SentinelSueldoIndividualBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TSentinelSueldoIndividual entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<SentinelSueldoIndividualBO> listadoBO)
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

        public bool Update(SentinelSueldoIndividualBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TSentinelSueldoIndividual entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<SentinelSueldoIndividualBO> listadoBO)
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
        private void AsignacionId(TSentinelSueldoIndividual entidad, SentinelSueldoIndividualBO objetoBO)
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

        private TSentinelSueldoIndividual MapeoEntidad(SentinelSueldoIndividualBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TSentinelSueldoIndividual entidad = new TSentinelSueldoIndividual();
                entidad = Mapper.Map<SentinelSueldoIndividualBO, TSentinelSueldoIndividual>(objetoBO,
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

        public SentinelSueldoIndividualPromedioDTO ObtenerSentinelSueldoPromedio(string dni)
        {
            try
            {
                string _querySueldoindividuales = "Select Id,SePromedio from com.V_TSentinelSueldoIndividual_SePromedio Where Dni=@Dni and Estado=1";
                var querySueldoindividuales = _dapper.FirstOrDefault(_querySueldoindividuales, new { Dni = dni });
                return JsonConvert.DeserializeObject<SentinelSueldoIndividualPromedioDTO>(querySueldoindividuales);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        

        /// <summary>
        /// Obtiene registros de la tabla T_SueldoIndividualPromedio, para ser llenados en una grilla mediante filtros para ser
        /// llenados en una grilla
        /// </summary>
        /// <param></param>
        /// <returns></returns>
        public List<SentinelSueldoIndividualDTO> ObtenerTodosSentinelSueldoIdividualesPorFiltro(List<int> Industrias, List<int> Categorias, List<int> Empresas, List<int> Cargos, List<int> AreaTrabajos, List<int> AreaFormaciones, List<int> Paises)
        {
           

            try
            {
                List<SentinelSueldoIndividualDTO> SentinelSueldosIndividuales = new List<SentinelSueldoIndividualDTO>();
                var _query = string.Empty;
                _query = "SELECT Id,Nombres,Industria,IdIndustria,TamanioEmpresa,IdTamanioEmpresa,EmpresaNombre,IdEmpresa,Cargo,IdCargo,AreaTrabajo,IdAreaTrabajo,AreaFormacion,IdAreaFormacion,Ciudad,IdCodigoCiudad,Pais,IdCodigoPais,SeLimiteInferior,SeLimiteSuperior,SePromedio,OrigenInformacion,Incluir FROM com.T_SentinelSueldoIndividual WHERE Estado=1 ";

                // filtro por IdIndustria
                if (Industrias != null && Industrias.Count > 0)
                {
                    _query += " AND IdIndustria IN ( ";
                    for (int i = 0; i < Industrias.Count; ++i)
                        if (i == 0) _query += Industrias[i];
                        else _query += ", " + Industrias[i];

                    _query += ") ";
                }

                // filtro por IdTamanioEmpresa
                if (Categorias != null && Categorias.Count > 0)
                {
                    _query += " AND IdTamanioEmpresa IN ( ";
                    for (int i = 0; i < Categorias.Count; ++i)
                        if (i == 0) _query += Categorias[i];
                        else _query += ", " + Categorias[i];

                    _query += ") ";
                }

                // filtro por IdEmpresa
                if (Empresas != null && Empresas.Count > 0)
                {
                    _query += " AND IdEmpresa IN ( ";
                    for (int i = 0; i < Empresas.Count; ++i)
                        if (i == 0) _query += Empresas[i];
                        else _query += ", " + Empresas[i];

                    _query += ") ";
                }

                // filtro por IdCargo
                if (Cargos != null && Cargos.Count > 0)
                {
                    _query += " AND IdCargo IN ( ";
                    for (int i = 0; i < Cargos.Count; ++i)
                        if (i == 0) _query += Cargos[i];
                        else _query += ", " + Cargos[i];

                    _query += ") ";
                }

                // filtro por IdAreaTrabajo
                if (AreaTrabajos != null && AreaTrabajos.Count > 0)
                {
                    _query += " AND IdAreaTrabajo IN ( ";
                    for (int i = 0; i < AreaTrabajos.Count; ++i)
                        if (i == 0) _query += AreaTrabajos[i];
                        else _query += ", " + AreaTrabajos[i];

                    _query += ") ";
                }

                // filtro por IdAreaFormacion
                if (AreaFormaciones != null && AreaFormaciones.Count > 0)
                {
                    _query += " AND IdAreaFormacion IN ( ";
                    for (int i = 0; i < AreaFormaciones.Count; ++i)
                        if (i == 0) _query += AreaFormaciones[i];
                        else _query += ", " + AreaFormaciones[i];

                    _query += ") ";
                }

                // filtro por IdCodigoPais
                if (Paises != null && Paises.Count > 0)
                {
                    _query += " AND IdCodigoPais IN ( ";
                    for (int i = 0; i < Paises.Count; ++i)
                        if (i == 0) _query += Paises[i];
                        else _query += ", " + Paises[i];

                    _query += ") ";
                }

                var resultadoDB = _dapper.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(resultadoDB) && !resultadoDB.Contains("[]"))
                {
                    SentinelSueldosIndividuales = JsonConvert.DeserializeObject<List<SentinelSueldoIndividualDTO>>(resultadoDB);
                }
                return SentinelSueldosIndividuales;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }
    }
}
