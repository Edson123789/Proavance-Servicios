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
    public class CondicionOcurrenciaLlamadaRepositorio : BaseRepository<TCondicionOcurrenciaLlamada, CondicionOcurrenciaLlamadaBO>
    {
        #region Metodos Base
        public CondicionOcurrenciaLlamadaRepositorio() : base()
        {
        }
        public CondicionOcurrenciaLlamadaRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<CondicionOcurrenciaLlamadaBO> GetBy(Expression<Func<TCondicionOcurrenciaLlamada, bool>> filter)
        {
            IEnumerable<TCondicionOcurrenciaLlamada> listado = base.GetBy(filter);
            List<CondicionOcurrenciaLlamadaBO> listadoBO = new List<CondicionOcurrenciaLlamadaBO>();
            foreach (var itemEntidad in listado)
            {
                CondicionOcurrenciaLlamadaBO objetoBO = Mapper.Map<TCondicionOcurrenciaLlamada, CondicionOcurrenciaLlamadaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public CondicionOcurrenciaLlamadaBO FirstById(int id)
        {
            try
            {
                TCondicionOcurrenciaLlamada entidad = base.FirstById(id);
                CondicionOcurrenciaLlamadaBO objetoBO = new CondicionOcurrenciaLlamadaBO();
                Mapper.Map<TCondicionOcurrenciaLlamada, CondicionOcurrenciaLlamadaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public CondicionOcurrenciaLlamadaBO FirstBy(Expression<Func<TCondicionOcurrenciaLlamada, bool>> filter)
        {
            try
            {
                TCondicionOcurrenciaLlamada entidad = base.FirstBy(filter);
                CondicionOcurrenciaLlamadaBO objetoBO = Mapper.Map<TCondicionOcurrenciaLlamada, CondicionOcurrenciaLlamadaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(CondicionOcurrenciaLlamadaBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TCondicionOcurrenciaLlamada entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<CondicionOcurrenciaLlamadaBO> listadoBO)
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

        public bool Update(CondicionOcurrenciaLlamadaBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TCondicionOcurrenciaLlamada entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<CondicionOcurrenciaLlamadaBO> listadoBO)
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
        private void AsignacionId(TCondicionOcurrenciaLlamada entidad, CondicionOcurrenciaLlamadaBO objetoBO)
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

        private TCondicionOcurrenciaLlamada MapeoEntidad(CondicionOcurrenciaLlamadaBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TCondicionOcurrenciaLlamada entidad = new TCondicionOcurrenciaLlamada();
                entidad = Mapper.Map<CondicionOcurrenciaLlamadaBO, TCondicionOcurrenciaLlamada>(objetoBO,
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
        /// Obtiene todas las CondicionOcurrenciaLlamada (para mostrarlas en comboboxes)
        /// </summary>
        /// <returns></returns>
        public List<CondicionOcurrenciaLlamadaBO> ObtenerTodasCondiciones()
        {
            try
            {
                List<CondicionOcurrenciaLlamadaBO> condiciones = new List<CondicionOcurrenciaLlamadaBO>();
                string _query = "SELECT Id, Nombre FROM mkt.T_CondicionOcurrenciaLLamada WHERE Estado=1";
                var condicionesDB = _dapper.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(condicionesDB) && !condicionesDB.Contains("[]"))
                {
                    condiciones = JsonConvert.DeserializeObject<List<CondicionOcurrenciaLlamadaBO>>(condicionesDB);
                }
                return condiciones;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

    }

}
