using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    public class CategoriaCiudadRepositorio : BaseRepository<TCategoriaCiudad, CategoriaCiudadBO>
    {
        #region Metodos Base
        public CategoriaCiudadRepositorio() : base()
        {
        }
        public CategoriaCiudadRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<CategoriaCiudadBO> GetBy(Expression<Func<TCategoriaCiudad, bool>> filter)
        {
            IEnumerable<TCategoriaCiudad> listado = base.GetBy(filter);
            List<CategoriaCiudadBO> listadoBO = new List<CategoriaCiudadBO>();
            foreach (var itemEntidad in listado)
            {
                CategoriaCiudadBO objetoBO = Mapper.Map<TCategoriaCiudad, CategoriaCiudadBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public CategoriaCiudadBO FirstById(int id)
        {
            try
            {
                TCategoriaCiudad entidad = base.FirstById(id);
                CategoriaCiudadBO objetoBO = new CategoriaCiudadBO();
                Mapper.Map<TCategoriaCiudad, CategoriaCiudadBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public CategoriaCiudadBO FirstBy(Expression<Func<TCategoriaCiudad, bool>> filter)
        {
            try
            {
                TCategoriaCiudad entidad = base.FirstBy(filter);
                CategoriaCiudadBO objetoBO = Mapper.Map<TCategoriaCiudad, CategoriaCiudadBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(CategoriaCiudadBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TCategoriaCiudad entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<CategoriaCiudadBO> listadoBO)
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

        public bool Update(CategoriaCiudadBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TCategoriaCiudad entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<CategoriaCiudadBO> listadoBO)
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
        private void AsignacionId(TCategoriaCiudad entidad, CategoriaCiudadBO objetoBO)
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

        private TCategoriaCiudad MapeoEntidad(CategoriaCiudadBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TCategoriaCiudad entidad = new TCategoriaCiudad();
                entidad = Mapper.Map<CategoriaCiudadBO, TCategoriaCiudad>(objetoBO,
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
        /// se obtiene la lista de troncales
        /// </summary>
        /// <returns></returns>
        public List<TroncalListaDTO> ObtenerTroncalLista()
        {
            try
            {
                string _queryTroncal = "SELECT Id,IdCategoriaPrograma,IdRegionCiudad,TroncalCompleto,NombreRegionCiudad,NombreCategoriaPrograma  from pla.V_Troncal where Estado=1 order by Id desc";
                var queryTroncal = _dapper.QueryDapper(_queryTroncal,null);
                return JsonConvert.DeserializeObject<List<TroncalListaDTO>>(queryTroncal);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// se obtiene locacion para filtro
        /// </summary>
        /// <returns></returns>
        public List<LocacionTroncalFiltroDTO> ObtenerLocacionTroncalFiltro()
        {
            try
            {
                string _queryTroncal = "SELECT distinct RC.Id,RC.Nombre,RC.IdCiudad,RC.CodigoBS,RC.DenominacionBS from conf.T_RegionCiudad AS RC INNER JOIN pla.T_Locacion AS LO on LO.IdRegion = RC.Id and LO.ESTADO = 1 WHERE RC.ESTADO = 1";
                var queryTroncal = _dapper.QueryDapper(_queryTroncal,null);
                return JsonConvert.DeserializeObject<List<LocacionTroncalFiltroDTO>>(queryTroncal);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// se realiza la validacion para saber si un trocal ya existe
        /// </summary>
        /// <param name="troncalCompleto"></param>
        /// <returns></returns>
        public ValidarTroncalDTO ValidarTroncal(string troncalCompleto)
        {
            try
            {
                string _queryTroncalValidar = "select Id,TroncalCompleto from pla.V_Troncal where  TroncalCompleto = @TroncalCompleto";
               
                var TroncalValidar = _dapper.FirstOrDefault(_queryTroncalValidar, new { TroncalCompleto = troncalCompleto });
                return JsonConvert.DeserializeObject<ValidarTroncalDTO>(TroncalValidar);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// se realiza la validacion de locacion parainsetar troncal
        /// </summary>
        /// <param name="idCategoriaPrograma"></param>
        /// <param name="idRegionCiudad"></param>
        /// <returns></returns>
        public ValidarTroncalDTO ValidarCiudadCategoria(int idCategoriaPrograma, int idRegionCiudad)
        {
            try
            {
                string _queryTroncalValidar = "select Id,IdRegionCiudad,IdCategoriaPrograma from pla.V_Troncal where IdCategoriaPrograma=@IdCategoriaPrograma and IdRegionCiudad =@IdRegionCiudad";

                var TroncalValidar = _dapper.FirstOrDefault(_queryTroncalValidar, new { IdCategoriaPrograma = idCategoriaPrograma, IdRegionCiudad = idRegionCiudad });
                return JsonConvert.DeserializeObject<ValidarTroncalDTO>(TroncalValidar);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
