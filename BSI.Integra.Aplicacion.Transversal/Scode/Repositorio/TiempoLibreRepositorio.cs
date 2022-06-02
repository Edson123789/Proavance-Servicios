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
    public class TiempoLibreRepositorio : BaseRepository<TTiempoLibre, TiempoLibreBO>
    {
        #region Metodos Base
        public TiempoLibreRepositorio() : base()
        {
        }
        public TiempoLibreRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<TiempoLibreBO> GetBy(Expression<Func<TTiempoLibre, bool>> filter)
        {
            IEnumerable<TTiempoLibre> listado = base.GetBy(filter);
            List<TiempoLibreBO> listadoBO = new List<TiempoLibreBO>();
            foreach (var itemEntidad in listado)
            {
                TiempoLibreBO objetoBO = Mapper.Map<TTiempoLibre, TiempoLibreBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public TiempoLibreBO FirstById(int id)
        {
            try
            {
                TTiempoLibre entidad = base.FirstById(id);
                TiempoLibreBO objetoBO = new TiempoLibreBO();
                Mapper.Map<TTiempoLibre, TiempoLibreBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public TiempoLibreBO FirstBy(Expression<Func<TTiempoLibre, bool>> filter)
        {
            try
            {
                TTiempoLibre entidad = base.FirstBy(filter);
                TiempoLibreBO objetoBO = Mapper.Map<TTiempoLibre, TiempoLibreBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(TiempoLibreBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TTiempoLibre entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<TiempoLibreBO> listadoBO)
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

        public bool Update(TiempoLibreBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TTiempoLibre entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<TiempoLibreBO> listadoBO)
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
        private void AsignacionId(TTiempoLibre entidad, TiempoLibreBO objetoBO)
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

        private TTiempoLibre MapeoEntidad(TiempoLibreBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TTiempoLibre entidad = new TTiempoLibre();
                entidad = Mapper.Map<TiempoLibreBO, TTiempoLibre>(objetoBO,
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
        /// Obtiene Cantidad de minutos libre de Entrada
        /// </summary>
        /// <returns></returns>
        public TiempoLibreRADTO ObtenerTiempoLibreTipoUno()
        {
            try
            {
                string _queryLibreEntrada = "select  TiempoMin from com.V_TTiempoLibre_FechaProgramacionAutomatica Where Tipo=1";
                var queryLibreEntrada = _dapper.FirstOrDefault(_queryLibreEntrada, null);
                TiempoLibreRADTO libreEntrada = JsonConvert.DeserializeObject<TiempoLibreRADTO>(queryLibreEntrada);
                return libreEntrada;

            }
            catch(Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
        /// <summary>
        /// Obtiene Cantidad de minutos libres de Almuerzo ObtenerAllTiempoLibre
        /// </summary>
        /// <returns></returns>
        public TiempoLibreRADTO ObtenerTiempoLibreTipoDos()
        {
            try
            {
                string _queryLibreEntrada = "select  TiempoMin from com.V_TTiempoLibre_FechaProgramacionAutomatica Where Tipo=2";
                var queryLibreEntrada = _dapper.FirstOrDefault(_queryLibreEntrada,new { });
                TiempoLibreRADTO libreEntrada = JsonConvert.DeserializeObject<TiempoLibreRADTO>(queryLibreEntrada);
                return libreEntrada;

            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        /// <summary>
        /// Obtiene los datos de tiempo libre para ser llenados en grilla (CRUD PROPIO)
        /// </summary>
        /// <returns></returns>
        public List<TiempoLibreDTO> ObtenerAllTiempoLibre()
        {
            try
            {
                string _queryLibreEntrada = "SELECT Id, TiempoMin, Tipo FROM com.T_TiempoLibre WHERE Estado=1";
                var queryLibreEntrada = _dapper.QueryDapper(_queryLibreEntrada, null);
                var libreEntrada = JsonConvert.DeserializeObject<List<TiempoLibreDTO>>(queryLibreEntrada);
                return libreEntrada;

            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
    }
}
