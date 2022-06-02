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
    public class ConectorOcurrenciaLlamadaRepositorio : BaseRepository<TConectorOcurrenciaLlamada, ConectorOcurrenciaLlamadaBO>
    {
        #region Metodos Base
        public ConectorOcurrenciaLlamadaRepositorio() : base()
        {
        }
        public ConectorOcurrenciaLlamadaRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<ConectorOcurrenciaLlamadaBO> GetBy(Expression<Func<TConectorOcurrenciaLlamada, bool>> filter)
        {
            IEnumerable<TConectorOcurrenciaLlamada> listado = base.GetBy(filter);
            List<ConectorOcurrenciaLlamadaBO> listadoBO = new List<ConectorOcurrenciaLlamadaBO>();
            foreach (var itemEntidad in listado)
            {
                ConectorOcurrenciaLlamadaBO objetoBO = Mapper.Map<TConectorOcurrenciaLlamada, ConectorOcurrenciaLlamadaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public ConectorOcurrenciaLlamadaBO FirstById(int id)
        {
            try
            {
                TConectorOcurrenciaLlamada entidad = base.FirstById(id);
                ConectorOcurrenciaLlamadaBO objetoBO = new ConectorOcurrenciaLlamadaBO();
                Mapper.Map<TConectorOcurrenciaLlamada, ConectorOcurrenciaLlamadaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public ConectorOcurrenciaLlamadaBO FirstBy(Expression<Func<TConectorOcurrenciaLlamada, bool>> filter)
        {
            try
            {
                TConectorOcurrenciaLlamada entidad = base.FirstBy(filter);
                ConectorOcurrenciaLlamadaBO objetoBO = Mapper.Map<TConectorOcurrenciaLlamada, ConectorOcurrenciaLlamadaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(ConectorOcurrenciaLlamadaBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TConectorOcurrenciaLlamada entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<ConectorOcurrenciaLlamadaBO> listadoBO)
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

        public bool Update(ConectorOcurrenciaLlamadaBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TConectorOcurrenciaLlamada entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<ConectorOcurrenciaLlamadaBO> listadoBO)
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
        private void AsignacionId(TConectorOcurrenciaLlamada entidad, ConectorOcurrenciaLlamadaBO objetoBO)
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

        private TConectorOcurrenciaLlamada MapeoEntidad(ConectorOcurrenciaLlamadaBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TConectorOcurrenciaLlamada entidad = new TConectorOcurrenciaLlamada();
                entidad = Mapper.Map<ConectorOcurrenciaLlamadaBO, TConectorOcurrenciaLlamada>(objetoBO,
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
        /// Obtiene todas las ConectorOcurrenciaLlamada (para mostrarlas en comboboxes)
        /// </summary>
        /// <returns></returns>
        public List<ConectorOcurrenciaLlamadaBO> ObtenerTodosConectores()
        {
            try
            {
                List<ConectorOcurrenciaLlamadaBO> conectores = new List<ConectorOcurrenciaLlamadaBO>();
                string _query = "SELECT Id, Nombre FROM mkt.T_ConectorOcurrenciaLlamada WHERE Estado=1";
                var conectoresDB = _dapper.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(conectoresDB) && !conectoresDB.Contains("[]"))
                {
                    conectores = JsonConvert.DeserializeObject<List<ConectorOcurrenciaLlamadaBO>>(conectoresDB);
                }
                return conectores;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

    }


}
