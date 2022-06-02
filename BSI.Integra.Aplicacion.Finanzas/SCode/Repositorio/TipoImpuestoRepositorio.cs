using System;
using System.Collections.Generic;
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
    public class TipoImpuestoRepositorio : BaseRepository<TTipoImpuesto, TipoImpuestoBO>
    {
        #region Metodos Base
        public TipoImpuestoRepositorio() : base()
        {
        }
        public TipoImpuestoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<TipoImpuestoBO> GetBy(Expression<Func<TTipoImpuesto, bool>> filter)
        {
            IEnumerable<TTipoImpuesto> listado = base.GetBy(filter);
            List<TipoImpuestoBO> listadoBO = new List<TipoImpuestoBO>();
            foreach (var itemEntidad in listado)
            {
                TipoImpuestoBO objetoBO = Mapper.Map<TTipoImpuesto, TipoImpuestoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public TipoImpuestoBO FirstById(int id)
        {
            try
            {
                TTipoImpuesto entidad = base.FirstById(id);
                TipoImpuestoBO objetoBO = new TipoImpuestoBO();
                Mapper.Map<TTipoImpuesto, TipoImpuestoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public TipoImpuestoBO FirstBy(Expression<Func<TTipoImpuesto, bool>> filter)
        {
            try
            {
                TTipoImpuesto entidad = base.FirstBy(filter);
                TipoImpuestoBO objetoBO = Mapper.Map<TTipoImpuesto, TipoImpuestoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(TipoImpuestoBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TTipoImpuesto entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<TipoImpuestoBO> listadoBO)
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

        public bool Update(TipoImpuestoBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TTipoImpuesto entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<TipoImpuestoBO> listadoBO)
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
        private void AsignacionId(TTipoImpuesto entidad, TipoImpuestoBO objetoBO)
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

        private TTipoImpuesto MapeoEntidad(TipoImpuestoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TTipoImpuesto entidad = new TTipoImpuesto();
                entidad = Mapper.Map<TipoImpuestoBO, TTipoImpuesto>(objetoBO,
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
        /// Obtiene [Id, Valor] del IGV 
        /// (utilizado en CRUD 'RendicionRequerimientos')
        /// </summary>
        /// <returns></returns>
        public List<FiltroDTO> ObtenerValorIgv()
        {
            try
            {
                List<FiltroDTO> Igv = new List<FiltroDTO>();
                var _query = string.Empty;
                _query = "SELECT Id, Valor as Nombre FROM fin.T_TipoImpuesto WHERE Estado = 1 AND Nombre='IGV-EGRESO'";
                var IgvDB = _dapper.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(IgvDB) && !IgvDB.Contains("[]"))
                {
                    Igv = JsonConvert.DeserializeObject<List<FiltroDTO>>(IgvDB);
                }
                return Igv;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Obtiene [Id, Valor] del IGV segun el pais
        /// </summary>
        /// <returns></returns>
        public List<FiltroDTO> ObtenerValorIgvPorPais(int IdPais)
        {
            try
            {
                List<FiltroDTO> Igv = new List<FiltroDTO>();
                var _query = string.Empty;
                _query = "SELECT TOP 1 IdImpuesto as Id, ValorImpuesto as Nombre FROM [fin].[V_ObtenerImpuestoAsociadoPais] WHERE IdPais =" + IdPais;
                var IgvDB = _dapper.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(IgvDB) && !IgvDB.Contains("[]"))
                {
                    Igv = JsonConvert.DeserializeObject<List<FiltroDTO>>(IgvDB);
                }
                return Igv;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
