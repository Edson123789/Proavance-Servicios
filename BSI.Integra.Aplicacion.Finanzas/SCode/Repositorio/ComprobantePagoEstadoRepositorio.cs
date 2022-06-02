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
    public class ComprobantePagoEstadoRepositorio : BaseRepository<TComprobantePagoEstado, ComprobantePagoEstadoBO>
    {
        #region Metodos Base
        public ComprobantePagoEstadoRepositorio() : base()
        {
        }
        public ComprobantePagoEstadoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<ComprobantePagoEstadoBO> GetBy(Expression<Func<TComprobantePagoEstado, bool>> filter)
        {
            IEnumerable<TComprobantePagoEstado> listado = base.GetBy(filter);
            List<ComprobantePagoEstadoBO> listadoBO = new List<ComprobantePagoEstadoBO>();
            foreach (var itemEntidad in listado)
            {
                ComprobantePagoEstadoBO objetoBO = Mapper.Map<TComprobantePagoEstado, ComprobantePagoEstadoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public ComprobantePagoEstadoBO FirstById(int id)
        {
            try
            {
                TComprobantePagoEstado entidad = base.FirstById(id);
                ComprobantePagoEstadoBO objetoBO = new ComprobantePagoEstadoBO();
                Mapper.Map<TComprobantePagoEstado, ComprobantePagoEstadoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public ComprobantePagoEstadoBO FirstBy(Expression<Func<TComprobantePagoEstado, bool>> filter)
        {
            try
            {
                TComprobantePagoEstado entidad = base.FirstBy(filter);
                ComprobantePagoEstadoBO objetoBO = Mapper.Map<TComprobantePagoEstado, ComprobantePagoEstadoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(ComprobantePagoEstadoBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TComprobantePagoEstado entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<ComprobantePagoEstadoBO> listadoBO)
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

        public bool Update(ComprobantePagoEstadoBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TComprobantePagoEstado entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<ComprobantePagoEstadoBO> listadoBO)
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
        private void AsignacionId(TComprobantePagoEstado entidad, ComprobantePagoEstadoBO objetoBO)
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

        private TComprobantePagoEstado MapeoEntidad(ComprobantePagoEstadoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TComprobantePagoEstado entidad = new TComprobantePagoEstado();
                entidad = Mapper.Map<ComprobantePagoEstadoBO, TComprobantePagoEstado>(objetoBO,
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
        /// Obtiene una Lista de ComprobantePagoEstado (utilizado para combobox)
        /// </summary>
        /// <returns></returns>
        public List<FiltroDTO> ObtenerListaComprobantePagoEstado()
        {
            try
            {
                List<FiltroDTO> lista = new List<FiltroDTO>();
                var _query = "SELECT Id, Nombre AS Nombre FROM fin.T_ComprobantePagoEstado  where Estado=1";
                var listaDB = _dapper.QueryDapper(_query, null);
                if (!listaDB.Contains("[]") && !string.IsNullOrEmpty(listaDB))
                {
                    lista = JsonConvert.DeserializeObject<List<FiltroDTO>>(listaDB);
                }
                return lista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
