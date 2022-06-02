using System;
using System.Collections.Generic;
using System.Linq;
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
    public class ExpositorPorAreaRepositorio : BaseRepository<TExpositorPorArea, ExpositorPorAreaBO>
    {
        #region Metodos Base
        public ExpositorPorAreaRepositorio() : base()
        {
        }
        public ExpositorPorAreaRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<ExpositorPorAreaBO> GetBy(Expression<Func<TExpositorPorArea, bool>> filter)
        {
            IEnumerable<TExpositorPorArea> listado = base.GetBy(filter);
            List<ExpositorPorAreaBO> listadoBO = new List<ExpositorPorAreaBO>();
            foreach (var itemEntidad in listado)
            {
                ExpositorPorAreaBO objetoBO = Mapper.Map<TExpositorPorArea, ExpositorPorAreaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public ExpositorPorAreaBO FirstById(int id)
        {
            try
            {
                TExpositorPorArea entidad = base.FirstById(id);
                ExpositorPorAreaBO objetoBO = new ExpositorPorAreaBO();
                Mapper.Map<TExpositorPorArea, ExpositorPorAreaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public ExpositorPorAreaBO FirstBy(Expression<Func<TExpositorPorArea, bool>> filter)
        {
            try
            {
                TExpositorPorArea entidad = base.FirstBy(filter);
                ExpositorPorAreaBO objetoBO = Mapper.Map<TExpositorPorArea, ExpositorPorAreaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(ExpositorPorAreaBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TExpositorPorArea entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<ExpositorPorAreaBO> listadoBO)
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

        public bool Update(ExpositorPorAreaBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TExpositorPorArea entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<ExpositorPorAreaBO> listadoBO)
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
        private void AsignacionId(TExpositorPorArea entidad, ExpositorPorAreaBO objetoBO)
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

        private TExpositorPorArea MapeoEntidad(ExpositorPorAreaBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TExpositorPorArea entidad = new TExpositorPorArea();
                entidad = Mapper.Map<ExpositorPorAreaBO, TExpositorPorArea>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos

                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<ExpositorPorAreaBO> GetFiltered<KProperty>(IEnumerable<Expression<Func<TExpositorPorArea, bool>>> filters, Expression<Func<TExpositorPorArea, KProperty>> orderBy, bool ascending)
        {
            IEnumerable<TExpositorPorArea> listado = base.GetFiltered(filters, orderBy, ascending);
            List<ExpositorPorAreaBO> listadoBO = new List<ExpositorPorAreaBO>();

            foreach (var itemEntidad in listado)
            {
                ExpositorPorAreaBO objetoBO = Mapper.Map<TExpositorPorArea, ExpositorPorAreaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }
            return listadoBO;
        }
        #endregion

        /// <summary>
        /// Obtiene todos los registros sin los campos de auditoria.
        /// </summary>
        /// <returns></returns>
        public List<ExpositorPorAreaListadoDTO> ObtenerTodoExpositorPorArea()
        {
            try
            {
                List<ExpositorPorAreaListadoDTO> obtenerExpositorPorArea = new List<ExpositorPorAreaListadoDTO>();
                var _query = "SELECT Id, IdExpositor, NombreExpositor, IdArea, NombreArea FROM pla.V_ObtenerTodoExpositorPorArea WHERE Estado = 1 ORDER BY Id DESC";
                var obtenerExpositorPorAreaDB = _dapper.QueryDapper(_query, new { });
                if (!string.IsNullOrEmpty(obtenerExpositorPorAreaDB) && !obtenerExpositorPorAreaDB.Contains("[]"))
                {
                    obtenerExpositorPorArea = JsonConvert.DeserializeObject<List<ExpositorPorAreaListadoDTO>>(obtenerExpositorPorAreaDB);
                }
                return obtenerExpositorPorArea;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
