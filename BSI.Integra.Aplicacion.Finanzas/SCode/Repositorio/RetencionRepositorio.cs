using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Finanzas.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Finanzas.Repositorio
{
    /// Repositorio: Finanzas/Retencion
    /// Autor: Miguel Mora
    /// Fecha: 10/08/2021
    /// <summary>
    /// Repositorio de retenciones
    /// </summary>

    public class RetencionRepositorio : BaseRepository<TRetencion, RetencionBO>
    {
        #region Metodos Base
        public RetencionRepositorio() : base()
        {
        }
        public RetencionRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<RetencionBO> GetBy(Expression<Func<TRetencion, bool>> filter)
        {
            IEnumerable<TRetencion> listado = base.GetBy(filter);
            List<RetencionBO> listadoBO = new List<RetencionBO>();
            foreach (var itemEntidad in listado)
            {
                RetencionBO objetoBO = Mapper.Map<TRetencion, RetencionBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public RetencionBO FirstById(int id)
        {
            try
            {
                TRetencion entidad = base.FirstById(id);
                RetencionBO objetoBO = new RetencionBO();
                Mapper.Map<TRetencion, RetencionBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public RetencionBO FirstBy(Expression<Func<TRetencion, bool>> filter)
        {
            try
            {
                TRetencion entidad = base.FirstBy(filter);
                RetencionBO objetoBO = Mapper.Map<TRetencion, RetencionBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(RetencionBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TRetencion entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<RetencionBO> listadoBO)
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

        public bool Update(RetencionBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TRetencion entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<RetencionBO> listadoBO)
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
        private void AsignacionId(TRetencion entidad, RetencionBO objetoBO)
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

        private TRetencion MapeoEntidad(RetencionBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TRetencion entidad = new TRetencion();
                entidad = Mapper.Map<RetencionBO, TRetencion>(objetoBO,
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


        /// Autor: Miguel Mora
        /// Fecha: 10/08/2021
        /// Version: 1.0
        /// <summary>
        /// Obtiene [Id, Valor] de las Retenciones existentes en una lista 
        /// para ser mostradas en un ComboBox (utilizado en CRUD 'RendicionRequerimientos')
        /// </summary>
        /// <returns>List<FiltroDTO></returns>
        public List<FiltroDTO> ObtenerListaRetencion()
        {
            try
            {
                List<FiltroDTO> listaRetencion = new List<FiltroDTO>();
                var query = string.Empty;
                query = "SELECT Id, Valor as Nombre FROM fin.T_Retencion WHERE Estado = 1";
                var datosRetencion = _dapper.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(datosRetencion) && !datosRetencion.Contains("[]"))
                {
                    listaRetencion = JsonConvert.DeserializeObject<List<FiltroDTO>>(datosRetencion);
                }
                return listaRetencion;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        /// Autor: Miguel Mora
        /// Fecha: 10/08/2021
        /// Version: 1.0
        /// <summary>
        /// Obtiene [Id, Valor] de Retencion segun el pais
        /// </summary>
        /// <param name=”idDePais”>Identificador de la tabla T_Pais</param>
        /// <returns>List<FiltroDTO></returns>
        public List<FiltroDTO> ObtenerValorRetencionPorPais(int idDePais)
        {
            try
            {
                List<FiltroDTO> listaRetencion = new List<FiltroDTO>();
                var query = string.Empty;
                query = "SELECT  IdRetencion as Id, ValorRetencion as Nombre FROM [fin].[V_ObtenerRetencionAsociadoPais] WHERE IdPais =" + idDePais;
                var datosRetencion = _dapper.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(datosRetencion) && !datosRetencion.Contains("[]"))
                {
                    listaRetencion = JsonConvert.DeserializeObject<List<FiltroDTO>>(datosRetencion);
                }
                return listaRetencion;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// Autor: Miguel Mora
        /// Fecha: 10/08/2021
        /// Version: 1.0
        /// <summary>
        /// Obtiene todas las retenciones validas
        /// </summary>
        /// <returns>List<RetencionDTO></returns>
        public List<RetencionDTO> ObtenerRetenciones()
        {
            try
            {
                List<RetencionDTO> listaRetencion = new List<RetencionDTO>();
                var queryText = string.Empty;
                queryText = "SELECT  R.Id,R.Nombre,R.Descripcion,IdPais,R.Valor,NombrePais AS Pais  FROM fin.T_Retencion AS R INNER JOIN conf.T_Pais AS p ON P.id=R.IdPais WHERE R.Estado = 1";
                var datosRetencion = _dapper.QueryDapper(queryText, null);
                if (!string.IsNullOrEmpty(datosRetencion) && !datosRetencion.Contains("[]"))
                {
                    listaRetencion = JsonConvert.DeserializeObject<List<RetencionDTO>>(datosRetencion);
                }
                return listaRetencion;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


    }
}
