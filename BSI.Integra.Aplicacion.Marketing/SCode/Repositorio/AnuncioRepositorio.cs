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
    public class AnuncioRepositorio : BaseRepository<TAnuncio, AnuncioBO>
    {
        #region Metodos Base
        public AnuncioRepositorio() : base()
        {
        }
        public AnuncioRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<AnuncioBO> GetBy(Expression<Func<TAnuncio, bool>> filter)
        {
            IEnumerable<TAnuncio> listado = base.GetBy(filter);
            List<AnuncioBO> listadoBO = new List<AnuncioBO>();
            foreach (var itemEntidad in listado)
            {
                AnuncioBO objetoBO = Mapper.Map<TAnuncio, AnuncioBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public AnuncioBO FirstById(int id)
        {
            try
            {
                TAnuncio entidad = base.FirstById(id);
                AnuncioBO objetoBO = new AnuncioBO();
                Mapper.Map<TAnuncio, AnuncioBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public AnuncioBO FirstBy(Expression<Func<TAnuncio, bool>> filter)
        {
            try
            {
                TAnuncio entidad = base.FirstBy(filter);
                AnuncioBO objetoBO = Mapper.Map<TAnuncio, AnuncioBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(AnuncioBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TAnuncio entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<AnuncioBO> listadoBO)
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

        public bool Update(AnuncioBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TAnuncio entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<AnuncioBO> listadoBO)
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
        private void AsignacionId(TAnuncio entidad, AnuncioBO objetoBO)
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

        private TAnuncio MapeoEntidad(AnuncioBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TAnuncio entidad = new TAnuncio();
                entidad = Mapper.Map<AnuncioBO, TAnuncio>(objetoBO,
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
        ///  Obtiene la lista de registros (Estado=1) de T_Anuncio y T_ConjuntoAnuncio mediante un join (Usado para el llenado de grilla).
        /// </summary>
        /// <returns></returns>
        public List<AnuncioPorConjuntoAnuncioDTO> ObtenerTodoAnuncioPorConjuntoAnuncio(int IdConjuntoAnuncioFuente)
        {
            try
            {
                List<AnuncioPorConjuntoAnuncioDTO> Registros = new List<AnuncioPorConjuntoAnuncioDTO>();
                var _query = "SELECT Id, Nombre, IdConjuntoAnuncioFacebook, IdAnuncioFacebook, IdConjuntoAnuncio, " +
                    "NombreConjuntoAnuncio, IdCentroCosto,NombreCentroCosto, IdCreativoPublicidad, EnlaceFormulario, " +
                    "IdCategoriaOrigen,NombreCategoriaOrigen, FechaCreacion, UsuarioModificacion " +
                    "FROM [mkt].[V_TAnuncioPorConjuntoAnuncio] " +
                    "WHERE IdConjuntoAnuncioFuente=@IdConjuntoAnuncioFuente order by FechaCreacion desc";
                var result = _dapper.QueryDapper(_query, new { IdConjuntoAnuncioFuente = IdConjuntoAnuncioFuente});
                if (!string.IsNullOrEmpty(result) && !result.Contains("[]"))
                {
                    Registros = JsonConvert.DeserializeObject<List<AnuncioPorConjuntoAnuncioDTO>>(result);
                }
                return Registros;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        ///  Obtiene solo un registro (Estado=1) de T_Anuncio y T_ConjuntoAnuncio mediante un join dado un IdConjuntoAnuncio (Usado para el llenado de grilla).
        /// </summary>
        /// <returns></returns>
        public List<AnuncioPorConjuntoAnuncioDTO> ObtenerTodoAnuncioPorIdConjuntoAnuncio(int IdConjuntoAnuncio)
        {
            try
            {
                List<AnuncioPorConjuntoAnuncioDTO> Registros = new List<AnuncioPorConjuntoAnuncioDTO>();
                var _query = "SELECT Id, Nombre, IdConjuntoAnuncioFacebook, IdAnuncioFacebook, IdConjuntoAnuncio, NombreConjuntoAnuncio, IdCentroCosto,NombreCentroCosto, IdCreativoPublicidad, EnlaceFormulario, IdCategoriaOrigen,NombreCategoriaOrigen, FechaCreacion, UsuarioModificacion  FROM [mkt].[V_TAnuncioPorConjuntoAnuncio] Where IdConjuntoAnuncio=@IdConjuntoAnuncio order by FechaCreacion desc";
                var result = _dapper.QueryDapper(_query, new { IdConjuntoAnuncio=IdConjuntoAnuncio});
                if (!string.IsNullOrEmpty(result) && !result.Contains("[]"))
                {
                    Registros = JsonConvert.DeserializeObject<List<AnuncioPorConjuntoAnuncioDTO>>(result);
                }
                return Registros;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        ///  Obtiene la lista de registros (Estado=1) de T_Anuncio dado un Id de ConjuntoAnuncio
        /// </summary>
        /// <returns></returns>
        public List<AnuncioDTO> ObtenerAnunciosPorConjuntoAnuncio(int IdConjuntoAnuncio)
        {
            try
            {
                List<AnuncioDTO> Registros = new List<AnuncioDTO>();
                var _query = "SELECT Id,Nombre,IdAnuncioFacebook,IdConjuntoAnuncio,IdCentroCosto,NombreCentroCosto,IdCreativoPublicidad,EnlaceFormulario FROM [mkt].[V_TAnuncio] WHERE IdConjuntoAnuncio=@IdConjuntoAnuncio";
                var result = _dapper.QueryDapper(_query, new { IdConjuntoAnuncio = IdConjuntoAnuncio });
                if (!string.IsNullOrEmpty(result) && !result.Contains("[]"))
                {
                    Registros = JsonConvert.DeserializeObject<List<AnuncioDTO>>(result);
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
