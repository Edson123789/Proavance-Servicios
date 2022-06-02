using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    /// Repositorio: SedeTrabajoRepositorio
    /// Autor: Luis Huallpa - Wilber Choque
    /// Fecha: 16/06/2021
    /// <summary>
    /// Repositorio para de tabla T_SedeTrabajo
    /// </summary>
    public class SedeTrabajoRepositorio : BaseRepository<TSedeTrabajo, SedeTrabajoBO>
    {
        #region Metodos Base
        public SedeTrabajoRepositorio() : base()
        {
        }
        public SedeTrabajoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<SedeTrabajoBO> GetBy(Expression<Func<TSedeTrabajo, bool>> filter)
        {
            IEnumerable<TSedeTrabajo> listado = base.GetBy(filter);
            List<SedeTrabajoBO> listadoBO = new List<SedeTrabajoBO>();
            foreach (var itemEntidad in listado)
            {
                SedeTrabajoBO objetoBO = Mapper.Map<TSedeTrabajo, SedeTrabajoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public SedeTrabajoBO FirstById(int id)
        {
            try
            {
                TSedeTrabajo entidad = base.FirstById(id);
                SedeTrabajoBO objetoBO = new SedeTrabajoBO();
                Mapper.Map<TSedeTrabajo, SedeTrabajoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public SedeTrabajoBO FirstBy(Expression<Func<TSedeTrabajo, bool>> filter)
        {
            try
            {
                TSedeTrabajo entidad = base.FirstBy(filter);
                SedeTrabajoBO objetoBO = Mapper.Map<TSedeTrabajo, SedeTrabajoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(SedeTrabajoBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TSedeTrabajo entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<SedeTrabajoBO> listadoBO)
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

        public bool Update(SedeTrabajoBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TSedeTrabajo entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<SedeTrabajoBO> listadoBO)
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
        private void AsignacionId(TSedeTrabajo entidad, SedeTrabajoBO objetoBO)
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

        private TSedeTrabajo MapeoEntidad(SedeTrabajoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TSedeTrabajo entidad = new TSedeTrabajo();
                entidad = Mapper.Map<SedeTrabajoBO, TSedeTrabajo>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos

                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<SedeTrabajoBO> GetFiltered<KProperty>(IEnumerable<Expression<Func<TSedeTrabajo, bool>>> filters, Expression<Func<TSedeTrabajo, KProperty>> orderBy, bool ascending)
        {
            IEnumerable<TSedeTrabajo> listado = base.GetFiltered(filters, orderBy, ascending);
            List<SedeTrabajoBO> listadoBO = new List<SedeTrabajoBO>();

            foreach (var itemEntidad in listado)
            {
                SedeTrabajoBO objetoBO = Mapper.Map<TSedeTrabajo, SedeTrabajoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }
            return listadoBO;
        }
        #endregion
        /// Repositorio: SedeTrabajoRepositorio
        /// Autor: 
        /// Fecha: 16/06/2021
        /// <summary>
        /// Obtiene el Id y Nombre para ComboBox
        /// </summary>
        /// <returns> List<FiltroIdNombreDTO> </returns>
        public List<FiltroIdNombreDTO> GetFiltroIdNombre()
		{
			var lista = GetBy(x => x.Estado == true, y => new FiltroIdNombreDTO
			{
				Id = y.Id,
				Nombre = y.Nombre
			}).ToList();
			return lista;
		}
        /// <summary>
        /// Se obtiene para filtro
        /// </summary>
        /// <returns></returns>
        public List<FiltroDTO> ObtenerTodoFiltro()
        {
            try
            {
                return this.GetBy(x => x.Estado == true, x => new FiltroDTO { Id = x.Id, Nombre = x.Nombre }).ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
