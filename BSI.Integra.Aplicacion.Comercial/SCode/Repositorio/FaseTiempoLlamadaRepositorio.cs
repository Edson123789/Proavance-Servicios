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
    public class FaseTiempoLlamadaRepositorio : BaseRepository<TFaseTiempoLlamada, FaseTiempoLlamadaBO>
    {
        #region Metodos Base
        public FaseTiempoLlamadaRepositorio() : base()
        {
        }
        public FaseTiempoLlamadaRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<FaseTiempoLlamadaBO> GetBy(Expression<Func<TFaseTiempoLlamada, bool>> filter)
        {
            IEnumerable<TFaseTiempoLlamada> listado = base.GetBy(filter);
            List<FaseTiempoLlamadaBO> listadoBO = new List<FaseTiempoLlamadaBO>();
            foreach (var itemEntidad in listado)
            {
                FaseTiempoLlamadaBO objetoBO = Mapper.Map<TFaseTiempoLlamada, FaseTiempoLlamadaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public FaseTiempoLlamadaBO FirstById(int id)
        {
            try
            {
                TFaseTiempoLlamada entidad = base.FirstById(id);
                FaseTiempoLlamadaBO objetoBO = new FaseTiempoLlamadaBO();
                Mapper.Map<TFaseTiempoLlamada, FaseTiempoLlamadaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public FaseTiempoLlamadaBO FirstBy(Expression<Func<TFaseTiempoLlamada, bool>> filter)
        {
            try
            {
                TFaseTiempoLlamada entidad = base.FirstBy(filter);
                FaseTiempoLlamadaBO objetoBO = Mapper.Map<TFaseTiempoLlamada, FaseTiempoLlamadaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(FaseTiempoLlamadaBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TFaseTiempoLlamada entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<FaseTiempoLlamadaBO> listadoBO)
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

        public bool Update(FaseTiempoLlamadaBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TFaseTiempoLlamada entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<FaseTiempoLlamadaBO> listadoBO)
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
        private void AsignacionId(TFaseTiempoLlamada entidad, FaseTiempoLlamadaBO objetoBO)
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

        private TFaseTiempoLlamada MapeoEntidad(FaseTiempoLlamadaBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TFaseTiempoLlamada entidad = new TFaseTiempoLlamada();
                entidad = Mapper.Map<FaseTiempoLlamadaBO, TFaseTiempoLlamada>(objetoBO,
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
        /// Obtiene todas las FaseTiempoLlamada (para mostrarlas en comboboxes)
        /// </summary>
        /// <returns></returns>
        public List<FaseTiempoLlamadaBO> ObtenerTodasFases()
        {
            try
            {
                List<FaseTiempoLlamadaBO> fases = new List<FaseTiempoLlamadaBO>();
                string _query = "SELECT Id, Nombre, Codigo FROM mkt.T_FaseTiempoLlamada WHERE Estado=1";
                var fasesDB = _dapper.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(fasesDB) && !fasesDB.Contains("[]"))
                {
                    fases = JsonConvert.DeserializeObject<List<FaseTiempoLlamadaBO>>(fasesDB);
                }
                return fases;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
    }


}
