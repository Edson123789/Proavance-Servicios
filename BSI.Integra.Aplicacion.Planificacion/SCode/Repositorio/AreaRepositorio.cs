using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.Planificacion.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using BSI.Integra.Aplicacion.DTOs;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Planificacion.Repositorio
{
    public class AreaRepositorio : BaseRepository<TArea, AreaBO>
    {
        #region Metodos Base
        public AreaRepositorio() : base()
        {
        }
        public AreaRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<AreaBO> GetBy(Expression<Func<TArea, bool>> filter)
        {
            IEnumerable<TArea> listado = base.GetBy(filter);
            List<AreaBO> listadoBO = new List<AreaBO>();
            foreach (var itemEntidad in listado)
            {
                AreaBO objetoBO = Mapper.Map<TArea, AreaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public AreaBO FirstById(int id)
        {
            try
            {
                TArea entidad = base.FirstById(id);
                AreaBO objetoBO = new AreaBO();
                Mapper.Map<TArea, AreaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public AreaBO FirstBy(Expression<Func<TArea, bool>> filter)
        {
            try
            {
                TArea entidad = base.FirstBy(filter);
                AreaBO objetoBO = Mapper.Map<TArea, AreaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(AreaBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TArea entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<AreaBO> listadoBO)
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

        public bool Update(AreaBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TArea entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<AreaBO> listadoBO)
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
        private void AsignacionId(TArea entidad, AreaBO objetoBO)
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

        private TArea MapeoEntidad(AreaBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TArea entidad = new TArea();
                entidad = Mapper.Map<AreaBO, TArea>(objetoBO,
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
        /// Obtiene lista de areas
        /// </summary>
        /// <returns></returns>
        public List<FiltroDTO> ObtenerAreas()
        {
            try
            {
                string _queryAreas = string.Empty;
                _queryAreas = "SELECT Id,Nombre FROM pla.V_TArea_IdNombre WHERE Estado=1";
                var Areas = _dapper.QueryDapper(_queryAreas, null);
                return JsonConvert.DeserializeObject<List<FiltroDTO>>(Areas);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
