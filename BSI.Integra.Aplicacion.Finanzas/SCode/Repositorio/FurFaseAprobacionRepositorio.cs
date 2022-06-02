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
    public class FurFaseAprobacionRepositorio : BaseRepository<TFurFaseAprobacion, FurFaseAprobacionBO>
    {
        #region Metodos Base
        public FurFaseAprobacionRepositorio() : base()
        {
        }
        public FurFaseAprobacionRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<FurFaseAprobacionBO> GetBy(Expression<Func<TFurFaseAprobacion, bool>> filter)
        {
            IEnumerable<TFurFaseAprobacion> listado = base.GetBy(filter);
            List<FurFaseAprobacionBO> listadoBO = new List<FurFaseAprobacionBO>();
            foreach (var itemEntidad in listado)
            {
                FurFaseAprobacionBO objetoBO = Mapper.Map<TFurFaseAprobacion, FurFaseAprobacionBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public FurFaseAprobacionBO FirstById(int id)
        {
            try
            {
                TFurFaseAprobacion entidad = base.FirstById(id);
                FurFaseAprobacionBO objetoBO = new FurFaseAprobacionBO();
                Mapper.Map<TFurFaseAprobacion, FurFaseAprobacionBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public FurFaseAprobacionBO FirstBy(Expression<Func<TFurFaseAprobacion, bool>> filter)
        {
            try
            {
                TFurFaseAprobacion entidad = base.FirstBy(filter);
                FurFaseAprobacionBO objetoBO = Mapper.Map<TFurFaseAprobacion, FurFaseAprobacionBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(FurFaseAprobacionBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TFurFaseAprobacion entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<FurFaseAprobacionBO> listadoBO)
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

        public bool Update(FurFaseAprobacionBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TFurFaseAprobacion entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<FurFaseAprobacionBO> listadoBO)
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
        private void AsignacionId(TFurFaseAprobacion entidad, FurFaseAprobacionBO objetoBO)
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

        private TFurFaseAprobacion MapeoEntidad(FurFaseAprobacionBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TFurFaseAprobacion entidad = new TFurFaseAprobacion();
                entidad = Mapper.Map<FurFaseAprobacionBO, TFurFaseAprobacion>(objetoBO,
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
        /// Obtiene una Lista de FurFaseAprobacion (utilizado para combobox)
        /// </summary>
        /// <returns></returns>
        public List<FiltroDTO> ObtenerListaFurFaseEstado()
        {
            try
            {
                List<FiltroDTO> lista = new List<FiltroDTO>();
                var _query = "SELECT Id, Nombre AS Nombre FROM fin.T_FurFaseAprobacion  where Estado=1";
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
