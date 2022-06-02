using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.Marketing.BO;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Marketing.Repositorio
{
    public class AdwordsApiPalabraClaveRepositorio : BaseRepository<TAdwordsApiPalabraClave, AdwordsApiPalabraClaveBO>
    {
        #region Metodos Base
        public AdwordsApiPalabraClaveRepositorio() : base()
        {
        }
        public AdwordsApiPalabraClaveRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<AdwordsApiPalabraClaveBO> GetBy(Expression<Func<TAdwordsApiPalabraClave, bool>> filter)
        {
            IEnumerable<TAdwordsApiPalabraClave> listado = base.GetBy(filter);
            List<AdwordsApiPalabraClaveBO> listadoBO = new List<AdwordsApiPalabraClaveBO>();
            foreach (var itemEntidad in listado)
            {
                AdwordsApiPalabraClaveBO objetoBO = Mapper.Map<TAdwordsApiPalabraClave, AdwordsApiPalabraClaveBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public AdwordsApiPalabraClaveBO FirstById(int id)
        {
            try
            {
                TAdwordsApiPalabraClave entidad = base.FirstById(id);
                AdwordsApiPalabraClaveBO objetoBO = new AdwordsApiPalabraClaveBO();
                Mapper.Map<TAdwordsApiPalabraClave, AdwordsApiPalabraClaveBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public AdwordsApiPalabraClaveBO FirstBy(Expression<Func<TAdwordsApiPalabraClave, bool>> filter)
        {
            try
            {
                TAdwordsApiPalabraClave entidad = base.FirstBy(filter);
                AdwordsApiPalabraClaveBO objetoBO = Mapper.Map<TAdwordsApiPalabraClave, AdwordsApiPalabraClaveBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(AdwordsApiPalabraClaveBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TAdwordsApiPalabraClave entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<AdwordsApiPalabraClaveBO> listadoBO)
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

        public bool Update(AdwordsApiPalabraClaveBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TAdwordsApiPalabraClave entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<AdwordsApiPalabraClaveBO> listadoBO)
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
        private void AsignacionId(TAdwordsApiPalabraClave entidad, AdwordsApiPalabraClaveBO objetoBO)
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

        private TAdwordsApiPalabraClave MapeoEntidad(AdwordsApiPalabraClaveBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TAdwordsApiPalabraClave entidad = new TAdwordsApiPalabraClave();
                entidad = Mapper.Map<AdwordsApiPalabraClaveBO, TAdwordsApiPalabraClave>(objetoBO,
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
        /// Obtiene las palabras desactualizadas en el mes pasado para ser actualizadas.
        /// </summary>
        /// <returns></returns>
        public List<AdwordsApiPalabraClaveDTO> ObtenerPalabraAdwordsDesactualizado()
        {
            try
            {
                List<AdwordsApiPalabraClaveDTO> items = new List<AdwordsApiPalabraClaveDTO>();
                var query = "SELECT PalabraClave FROM mkt.V_ObtenerPalabraAdwordsDesactualizado";
                var queryRespuesta = _dapper.QueryDapper(query, null);

                if (!string.IsNullOrEmpty(queryRespuesta) && !queryRespuesta.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<AdwordsApiPalabraClaveDTO>>(queryRespuesta);
                }
                return items;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        /// <summary>
        /// Obtiene las palabras desactualizadas en el mes pasado para ser actualizadas.
        /// </summary>
        /// <returns></returns>
        public List<AdwordsApiPalabraClaveDTO> ObtenerPalabraAdwordsDesactualizadoPais(int idPais)
        {
            try
            {
                List<AdwordsApiPalabraClaveDTO> items = new List<AdwordsApiPalabraClaveDTO>();
                var resultado = _dapper.QuerySPDapper("mkt.SP_ObtenerPalabraAdwordsDesactualizadoPais", new { idPais });


                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<AdwordsApiPalabraClaveDTO>>(resultado);
                }
                return items;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
