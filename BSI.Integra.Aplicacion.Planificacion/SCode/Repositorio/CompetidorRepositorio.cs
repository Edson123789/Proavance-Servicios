using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using AutoMapper;
using BSI.Integra.Aplicacion.Planificacion.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using BSI.Integra.Aplicacion.DTOs;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Planificacion.Repositorio
{
    public class CompetidorRepositorio : BaseRepository<TCompetidor, CompetidorBO>
    {
        #region Metodos Base
        public CompetidorRepositorio() : base()
        {
        }
        public CompetidorRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<CompetidorBO> GetBy(Expression<Func<TCompetidor, bool>> filter)
        {
            IEnumerable<TCompetidor> listado = base.GetBy(filter);
            List<CompetidorBO> listadoBO = new List<CompetidorBO>();
            foreach (var itemEntidad in listado)
            {
                CompetidorBO objetoBO = Mapper.Map<TCompetidor, CompetidorBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public CompetidorBO FirstById(int id)
        {
            try
            {
                TCompetidor entidad = base.FirstById(id);
                CompetidorBO objetoBO = new CompetidorBO();
                Mapper.Map<TCompetidor, CompetidorBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public CompetidorBO FirstBy(Expression<Func<TCompetidor, bool>> filter)
        {
            try
            {
                TCompetidor entidad = base.FirstBy(filter);
                CompetidorBO objetoBO = Mapper.Map<TCompetidor, CompetidorBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(CompetidorBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TCompetidor entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<CompetidorBO> listadoBO)
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

        public bool Update(CompetidorBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TCompetidor entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<CompetidorBO> listadoBO)
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
        private void AsignacionId(TCompetidor entidad, CompetidorBO objetoBO)
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

        private TCompetidor MapeoEntidad(CompetidorBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TCompetidor entidad = new TCompetidor();
                entidad = Mapper.Map<CompetidorBO, TCompetidor>(objetoBO,
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
        /// Obtiene una lista de los competidores de acuerdo a una Oportunidad
        /// </summary>
        /// <param name="IdOportunidad"></param>
        /// <returns></returns>
        public List<OportunidadCompetidoresDTO> CargarOportunidadCompetidores(int IdOportunidad)
        {
            try
            {
                string _query = "SELECT Id, IdOportunidad, Nombre, DuracionCronologica, CostoNeto, Precio, Categoria, Empresa, RegionCiudad, Moneda, IdCompetidorVentajaDesventaja, ContenidoCompetidorVentajaDesventaja, TipoCompetidorVentajaDesventaja FROM com.V_Oportunidad_Competidores WHERE IdOportunidad = @IdOportunidad AND (TipoCompetidorVentajaDesventaja IS NULL OR TipoCompetidorVentajaDesventaja = 0)";
                var RegistrosBO = _dapper.QueryDapper(_query, new { IdOportunidad = IdOportunidad });
                List<OportunidadCompetidoresDTO> listaCompetidores = JsonConvert.DeserializeObject<List<OportunidadCompetidoresDTO>>(RegistrosBO);
                return listaCompetidores;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }


        /// <summary>
        /// Obtiene los datos de Competidores (activos) para ser usados en una grilla (CRUD propio)
        /// </summary>
        /// <returns></returns>
        public List<CompetidorDTO> ObtenerTodoCompetidores()
        {
            try
            {
                List<CompetidorDTO> Competidores = new List<CompetidorDTO>();
                var _query = string.Empty;
                _query = "SELECT Id, Nombre, DuracionCronologica, CostoNeto, Precio, IdMoneda, IdInstitucionCompetidora, IdPais, IdCiudad, IdRegionCiudad, IdAeaCapacitacion, IdSubAreaCapacitacion, IdCategoria FROM pla.T_Competidor WHERE Estado = 1";
                var CompetidoresDB = _dapper.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(CompetidoresDB) && !CompetidoresDB.Contains("[]"))
                {
                    Competidores = JsonConvert.DeserializeObject<List<CompetidorDTO>>(CompetidoresDB);
                }
                return Competidores;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
