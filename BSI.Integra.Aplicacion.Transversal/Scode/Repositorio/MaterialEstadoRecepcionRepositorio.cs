using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    public class MaterialEstadoRecepcionRepositorio : BaseRepository<TMaterialEstadoRecepcion, MaterialEstadoRecepcionBO>
    {
        #region Metodos Base
        public MaterialEstadoRecepcionRepositorio() : base()
        {
        }
        public MaterialEstadoRecepcionRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<MaterialEstadoRecepcionBO> GetBy(Expression<Func<TMaterialEstadoRecepcion, bool>> filter)
        {
            IEnumerable<TMaterialEstadoRecepcion> listado = base.GetBy(filter);
            List<MaterialEstadoRecepcionBO> listadoBO = new List<MaterialEstadoRecepcionBO>();
            foreach (var itemEntidad in listado)
            {
                MaterialEstadoRecepcionBO objetoBO = Mapper.Map<TMaterialEstadoRecepcion, MaterialEstadoRecepcionBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public MaterialEstadoRecepcionBO FirstById(int id)
        {
            try
            {
                TMaterialEstadoRecepcion entidad = base.FirstById(id);
                MaterialEstadoRecepcionBO objetoBO = new MaterialEstadoRecepcionBO();
                Mapper.Map<TMaterialEstadoRecepcion, MaterialEstadoRecepcionBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public MaterialEstadoRecepcionBO FirstBy(Expression<Func<TMaterialEstadoRecepcion, bool>> filter)
        {
            try
            {
                TMaterialEstadoRecepcion entidad = base.FirstBy(filter);
                MaterialEstadoRecepcionBO objetoBO = Mapper.Map<TMaterialEstadoRecepcion, MaterialEstadoRecepcionBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(MaterialEstadoRecepcionBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TMaterialEstadoRecepcion entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<MaterialEstadoRecepcionBO> listadoBO)
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

        public bool Update(MaterialEstadoRecepcionBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TMaterialEstadoRecepcion entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<MaterialEstadoRecepcionBO> listadoBO)
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
        private void AsignacionId(TMaterialEstadoRecepcion entidad, MaterialEstadoRecepcionBO objetoBO)
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

        private TMaterialEstadoRecepcion MapeoEntidad(MaterialEstadoRecepcionBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TMaterialEstadoRecepcion entidad = new TMaterialEstadoRecepcion();
                entidad = Mapper.Map<MaterialEstadoRecepcionBO, TMaterialEstadoRecepcion>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos

                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<MaterialEstadoRecepcionBO> GetFiltered<KProperty>(IEnumerable<Expression<Func<TMaterialEstadoRecepcion, bool>>> filters, Expression<Func<TMaterialEstadoRecepcion, KProperty>> orderBy, bool ascending)
        {
            IEnumerable<TMaterialEstadoRecepcion> listado = base.GetFiltered(filters, orderBy, ascending);
            List<MaterialEstadoRecepcionBO> listadoBO = new List<MaterialEstadoRecepcionBO>();

            foreach (var itemEntidad in listado)
            {
                MaterialEstadoRecepcionBO objetoBO = Mapper.Map<TMaterialEstadoRecepcion, MaterialEstadoRecepcionBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
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
