using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Marketing.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    public class ConjuntoAnuncioTipoObjetivoRepositorio : BaseRepository<TConjuntoAnuncioTipoObjetivo, ConjuntoAnuncioTipoObjetivoBO>
    {
        #region Metodos Base
        public ConjuntoAnuncioTipoObjetivoRepositorio() : base()
        {
        }
        public ConjuntoAnuncioTipoObjetivoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<ConjuntoAnuncioTipoObjetivoBO> GetBy(Expression<Func<TConjuntoAnuncioTipoObjetivo, bool>> filter)
        {
            IEnumerable<TConjuntoAnuncioTipoObjetivo> listado = base.GetBy(filter);
            List<ConjuntoAnuncioTipoObjetivoBO> listadoBO = new List<ConjuntoAnuncioTipoObjetivoBO>();
            foreach (var itemEntidad in listado)
            {
                ConjuntoAnuncioTipoObjetivoBO objetoBO = Mapper.Map<TConjuntoAnuncioTipoObjetivo, ConjuntoAnuncioTipoObjetivoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public ConjuntoAnuncioTipoObjetivoBO FirstById(int id)
        {
            try
            {
                TConjuntoAnuncioTipoObjetivo entidad = base.FirstById(id);
                ConjuntoAnuncioTipoObjetivoBO objetoBO = new ConjuntoAnuncioTipoObjetivoBO();
                Mapper.Map<TConjuntoAnuncioTipoObjetivo, ConjuntoAnuncioTipoObjetivoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public ConjuntoAnuncioTipoObjetivoBO FirstBy(Expression<Func<TConjuntoAnuncioTipoObjetivo, bool>> filter)
        {
            try
            {
                TConjuntoAnuncioTipoObjetivo entidad = base.FirstBy(filter);
                ConjuntoAnuncioTipoObjetivoBO objetoBO = Mapper.Map<TConjuntoAnuncioTipoObjetivo, ConjuntoAnuncioTipoObjetivoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(ConjuntoAnuncioTipoObjetivoBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TConjuntoAnuncioTipoObjetivo entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<ConjuntoAnuncioTipoObjetivoBO> listadoBO)
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

        public bool Update(ConjuntoAnuncioTipoObjetivoBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TConjuntoAnuncioTipoObjetivo entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<ConjuntoAnuncioTipoObjetivoBO> listadoBO)
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
        private void AsignacionId(TConjuntoAnuncioTipoObjetivo entidad, ConjuntoAnuncioTipoObjetivoBO objetoBO)
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

        private TConjuntoAnuncioTipoObjetivo MapeoEntidad(ConjuntoAnuncioTipoObjetivoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TConjuntoAnuncioTipoObjetivo entidad = new TConjuntoAnuncioTipoObjetivo();
                entidad = Mapper.Map<ConjuntoAnuncioTipoObjetivoBO, TConjuntoAnuncioTipoObjetivo>(objetoBO,
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
        ///  Obtiene la lista de registros [Id, NOmbre] (Estado=1) de T_FormularioPlantilla (Usado para el llenado de combobox). PARA FACEBOOK
        /// </summary>
        /// <returns></returns>
        public List<FiltroDTO> ObtenerTodoConjuntoAnuncioTipoObjetivoFacebookFiltro(int IdConjuntoAnuncioFuente)
        {
            try
            {
                List<FiltroDTO> Registros = new List<FiltroDTO>();
                var _query = "SELECT Id,  Nombre FROM [mkt].[V_TConjuntoAnuncioTipoObjetivoFacebook] WHERE IdConjuntoAnuncioFuente=@IdConjuntoAnuncioFuente";
                var result = _dapper.QueryDapper(_query, new { IdConjuntoAnuncioFuente = IdConjuntoAnuncioFuente });
                if (!string.IsNullOrEmpty(result) && !result.Contains("[]"))
                {
                    Registros = JsonConvert.DeserializeObject<List<FiltroDTO>>(result);
                }
                return Registros;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        ///  Obtiene la lista de registros [Id, NOmbre] (Estado=1) de T_FormularioPlantilla (Usado para el llenado de combobox). para ADWORDS
        /// </summary>
        /// <returns></returns>
        public List<FiltroDTO> ObtenerTodoConjuntoAnuncioTipoObjetivoAdwordsFiltro()
        {
            try
            {
                List<FiltroDTO> Registros = new List<FiltroDTO>();
                var _query = "SELECT Id,  Nombre FROM [mkt].[V_TConjuntoAnuncioTipoObjetivoAdwords]";
                var result = _dapper.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(result) && !result.Contains("[]"))
                {
                    Registros = JsonConvert.DeserializeObject<List<FiltroDTO>>(result);
                }
                return Registros;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
