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
    public class MaterialTipoEntregaRepositorio : BaseRepository<TMaterialTipoEntrega, MaterialTipoEntregaBO>
    {
        #region Metodos Base
        public MaterialTipoEntregaRepositorio() : base()
        {
        }
        public MaterialTipoEntregaRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<MaterialTipoEntregaBO> GetBy(Expression<Func<TMaterialTipoEntrega, bool>> filter)
        {
            IEnumerable<TMaterialTipoEntrega> listado = base.GetBy(filter);
            List<MaterialTipoEntregaBO> listadoBO = new List<MaterialTipoEntregaBO>();
            foreach (var itemEntidad in listado)
            {
                MaterialTipoEntregaBO objetoBO = Mapper.Map<TMaterialTipoEntrega, MaterialTipoEntregaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public MaterialTipoEntregaBO FirstById(int id)
        {
            try
            {
                TMaterialTipoEntrega entidad = base.FirstById(id);
                MaterialTipoEntregaBO objetoBO = new MaterialTipoEntregaBO();
                Mapper.Map<TMaterialTipoEntrega, MaterialTipoEntregaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public MaterialTipoEntregaBO FirstBy(Expression<Func<TMaterialTipoEntrega, bool>> filter)
        {
            try
            {
                TMaterialTipoEntrega entidad = base.FirstBy(filter);
                MaterialTipoEntregaBO objetoBO = Mapper.Map<TMaterialTipoEntrega, MaterialTipoEntregaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(MaterialTipoEntregaBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TMaterialTipoEntrega entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<MaterialTipoEntregaBO> listadoBO)
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

        public bool Update(MaterialTipoEntregaBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TMaterialTipoEntrega entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<MaterialTipoEntregaBO> listadoBO)
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
        private void AsignacionId(TMaterialTipoEntrega entidad, MaterialTipoEntregaBO objetoBO)
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

        private TMaterialTipoEntrega MapeoEntidad(MaterialTipoEntregaBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TMaterialTipoEntrega entidad = new TMaterialTipoEntrega();
                entidad = Mapper.Map<MaterialTipoEntregaBO, TMaterialTipoEntrega>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos

                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<MaterialTipoEntregaBO> GetFiltered<KProperty>(IEnumerable<Expression<Func<TMaterialTipoEntrega, bool>>> filters, Expression<Func<TMaterialTipoEntrega, KProperty>> orderBy, bool ascending)
        {
            IEnumerable<TMaterialTipoEntrega> listado = base.GetFiltered(filters, orderBy, ascending);
            List<MaterialTipoEntregaBO> listadoBO = new List<MaterialTipoEntregaBO>();

            foreach (var itemEntidad in listado)
            {
                MaterialTipoEntregaBO objetoBO = Mapper.Map<TMaterialTipoEntrega, MaterialTipoEntregaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }
            return listadoBO;
        }
        #endregion

        /// <summary>
        /// Obtiene todos los registros para filtro
        /// </summary>
        /// <returns></returns>
        public List<FiltroDTO> ObtenerTodoFiltro()
        {
            try
            {
                return this.GetBy(x => x.Estado, x => new FiltroDTO { Id = x.Id, Nombre = x.Nombre }).ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
