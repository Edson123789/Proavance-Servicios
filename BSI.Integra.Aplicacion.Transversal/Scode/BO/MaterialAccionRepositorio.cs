using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    public class MaterialAccionRepositorio : BaseRepository<TMaterialAccion, MaterialAccionBO>
    {
        #region Metodos Base
        public MaterialAccionRepositorio() : base()
        {
        }
        public MaterialAccionRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<MaterialAccionBO> GetBy(Expression<Func<TMaterialAccion, bool>> filter)
        {
            IEnumerable<TMaterialAccion> listado = base.GetBy(filter);
            List<MaterialAccionBO> listadoBO = new List<MaterialAccionBO>();
            foreach (var itemEntidad in listado)
            {
                MaterialAccionBO objetoBO = Mapper.Map<TMaterialAccion, MaterialAccionBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public MaterialAccionBO FirstById(int id)
        {
            try
            {
                TMaterialAccion entidad = base.FirstById(id);
                MaterialAccionBO objetoBO = new MaterialAccionBO();
                Mapper.Map<TMaterialAccion, MaterialAccionBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public MaterialAccionBO FirstBy(Expression<Func<TMaterialAccion, bool>> filter)
        {
            try
            {
                TMaterialAccion entidad = base.FirstBy(filter);
                MaterialAccionBO objetoBO = Mapper.Map<TMaterialAccion, MaterialAccionBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(MaterialAccionBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TMaterialAccion entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<MaterialAccionBO> listadoBO)
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

        public bool Update(MaterialAccionBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TMaterialAccion entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<MaterialAccionBO> listadoBO)
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
        private void AsignacionId(TMaterialAccion entidad, MaterialAccionBO objetoBO)
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

        private TMaterialAccion MapeoEntidad(MaterialAccionBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TMaterialAccion entidad = new TMaterialAccion();
                entidad = Mapper.Map<MaterialAccionBO, TMaterialAccion>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos

                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<MaterialAccionBO> GetFiltered<KProperty>(IEnumerable<Expression<Func<TMaterialAccion, bool>>> filters, Expression<Func<TMaterialAccion, KProperty>> orderBy, bool ascending)
        {
            IEnumerable<TMaterialAccion> listado = base.GetFiltered(filters, orderBy, ascending);
            List<MaterialAccionBO> listadoBO = new List<MaterialAccionBO>();

            foreach (var itemEntidad in listado)
            {
                MaterialAccionBO objetoBO = Mapper.Map<TMaterialAccion, MaterialAccionBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }
            return listadoBO;
        }
        #endregion

        /// <summary>
        /// Obtiene para filtro
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
        /// <summary>
        /// Obtiene la lista 
        /// </summary>
        /// <returns></returns>
        public List<MaterialAccionBO> Obtener()
        {
            try
            {
                return this.GetBy(x => x.Estado).ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
