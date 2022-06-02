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
    public class FurSubFaseAprobacionRepositorio : BaseRepository<TFurSubFaseAprobacion, FurSubFaseAprobacionBO>
    {
        #region Metodos Base
        public FurSubFaseAprobacionRepositorio() : base()
        {
        }
        public FurSubFaseAprobacionRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<FurSubFaseAprobacionBO> GetBy(Expression<Func<TFurSubFaseAprobacion, bool>> filter)
        {
            IEnumerable<TFurSubFaseAprobacion> listado = base.GetBy(filter);
            List<FurSubFaseAprobacionBO> listadoBO = new List<FurSubFaseAprobacionBO>();
            foreach (var itemEntidad in listado)
            {
                FurSubFaseAprobacionBO objetoBO = Mapper.Map<TFurSubFaseAprobacion, FurSubFaseAprobacionBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public FurSubFaseAprobacionBO FirstById(int id)
        {
            try
            {
                TFurSubFaseAprobacion entidad = base.FirstById(id);
                FurSubFaseAprobacionBO objetoBO = new FurSubFaseAprobacionBO();
                Mapper.Map<TFurSubFaseAprobacion, FurSubFaseAprobacionBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public FurSubFaseAprobacionBO FirstBy(Expression<Func<TFurSubFaseAprobacion, bool>> filter)
        {
            try
            {
                TFurSubFaseAprobacion entidad = base.FirstBy(filter);
                FurSubFaseAprobacionBO objetoBO = Mapper.Map<TFurSubFaseAprobacion, FurSubFaseAprobacionBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(FurSubFaseAprobacionBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TFurSubFaseAprobacion entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<FurSubFaseAprobacionBO> listadoBO)
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

        public bool Update(FurSubFaseAprobacionBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TFurSubFaseAprobacion entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<FurSubFaseAprobacionBO> listadoBO)
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
        private void AsignacionId(TFurSubFaseAprobacion entidad, FurSubFaseAprobacionBO objetoBO)
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

        private TFurSubFaseAprobacion MapeoEntidad(FurSubFaseAprobacionBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TFurSubFaseAprobacion entidad = new TFurSubFaseAprobacion();
                entidad = Mapper.Map<FurSubFaseAprobacionBO, TFurSubFaseAprobacion>(objetoBO,
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
        /// Obtiene una Lista de FurSubFaseAprobacion (utilizado para combobox)
        /// </summary>
        /// <returns></returns>
        public List<FiltroDTO> ObtenerListaFurSubFaseEstado()
        {
            try
            {
                List<FiltroDTO> lista = new List<FiltroDTO>();
                var _query = "SELECT Id, Nombre AS Nombre FROM fin.T_FurSubFaseAprobacion  where Estado=1";
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
