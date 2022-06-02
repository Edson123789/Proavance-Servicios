using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    public class ArticuloSeoRepositorio : BaseRepository<TArticuloSeo, ArticuloSeoBO>
    {
        #region Metodos Base
        public ArticuloSeoRepositorio() : base()
        {
        }
        public ArticuloSeoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<ArticuloSeoBO> GetBy(Expression<Func<TArticuloSeo, bool>> filter)
        {
            IEnumerable<TArticuloSeo> listado = base.GetBy(filter);
            List<ArticuloSeoBO> listadoBO = new List<ArticuloSeoBO>();
            foreach (var itemEntidad in listado)
            {
                ArticuloSeoBO objetoBO = Mapper.Map<TArticuloSeo, ArticuloSeoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public ArticuloSeoBO FirstById(int id)
        {
            try
            {
                TArticuloSeo entidad = base.FirstById(id);
                ArticuloSeoBO objetoBO = new ArticuloSeoBO();
                Mapper.Map<TArticuloSeo, ArticuloSeoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public ArticuloSeoBO FirstBy(Expression<Func<TArticuloSeo, bool>> filter)
        {
            try
            {
                TArticuloSeo entidad = base.FirstBy(filter);
                ArticuloSeoBO objetoBO = Mapper.Map<TArticuloSeo, ArticuloSeoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(ArticuloSeoBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TArticuloSeo entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<ArticuloSeoBO> listadoBO)
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

        public bool Update(ArticuloSeoBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TArticuloSeo entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<ArticuloSeoBO> listadoBO)
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
        private void AsignacionId(TArticuloSeo entidad, ArticuloSeoBO objetoBO)
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

        private TArticuloSeo MapeoEntidad(ArticuloSeoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TArticuloSeo entidad = new TArticuloSeo();
                entidad = Mapper.Map<ArticuloSeoBO, TArticuloSeo>(objetoBO,
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

        /// Autor: Jose Villena
        /// Fecha: 30/07/2020
        /// Version: 1.0
        /// <summary>
        /// Obtiena Articulo Seo Parametro
        /// </summary>
        /// <param>IdArticulo</param>
        /// <returns>Objeto</returns>
        public List<ParametroContenidoArticuloDTO> ObtenerArticuloSeoParametro(int IdArticulo)
        {
            List<ParametroContenidoArticuloDTO> rpta = new List<ParametroContenidoArticuloDTO>();
            string queryArticuloSeo= "Select Id,Nombre,NumeroCaracteres,Descripcion From mkt.V_ObtenerParamentroArticulo where IdArticulo=@IdArticulo and EstadoParametroSeo=1 and EstadoArticuloSeo=1";
            string resultadoQueryArticuloSeo = _dapper.QueryDapper(queryArticuloSeo, new { IdArticulo });
            if (!string.IsNullOrEmpty(resultadoQueryArticuloSeo) && !resultadoQueryArticuloSeo.Contains("[]"))
            {
                rpta = JsonConvert.DeserializeObject<List<ParametroContenidoArticuloDTO>>(resultadoQueryArticuloSeo);
            }

            return rpta;
        }
        /// Autor: 
        /// Fecha: 24/02/2021
        /// Version: 1.0
        /// <summary>
        /// Obtiene Parametro Se parao Filtro
        /// </summary>
        /// <param></param>
        /// <returns>Objeto</returns>
        public List<ParametroSeoPwFiltroDTO> ObtnerParametroSeoFiltro()
        {
            try
            {
                List<ParametroSeoPwFiltroDTO> parametroSeo = new List<ParametroSeoPwFiltroDTO>();
                string querySeo = string.Empty;
                querySeo = "SELECT Id,Nombre FROM pla.V_TParametroSEOPW_Filtro WHERE Estado=1";
                var resultadoQuerySeo = _dapper.QueryDapper(querySeo, null);
                if (!string.IsNullOrEmpty(resultadoQuerySeo) && !resultadoQuerySeo.Contains("[]"))
                {
                    parametroSeo = JsonConvert.DeserializeObject<List<ParametroSeoPwFiltroDTO>>(resultadoQuerySeo);
                }
                return parametroSeo;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

  
        /// Autor: Richard Zenteno
        /// Fecha: 24/02/2021
        /// Version: 1.0
        /// <summary>
        /// Obtiene la lista de Tags asociados a determinado articulo
        /// </summary>
        /// <param>IdArticulo</param>
        /// <returns>Objeto</returns>
        public List<FiltroDTO> ObtenerTagsAsociadosArticulo(int IdArticulo)
        {
            try
            {
                List<FiltroDTO> tags = new List<FiltroDTO>();
                string _query = string.Empty;
                _query = "SELECT Id,Nombre FROM [pla].[V_ArticulosTagsAsociados] WHERE IdArticulo="+IdArticulo;
                var querySeo = _dapper.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(querySeo) && !querySeo.Contains("[]"))
                {
                    tags = JsonConvert.DeserializeObject<List<FiltroDTO>>(querySeo);
                }
                return tags;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
