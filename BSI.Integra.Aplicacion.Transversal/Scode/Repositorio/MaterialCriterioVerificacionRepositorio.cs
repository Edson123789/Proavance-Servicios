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
    public class MaterialCriterioVerificacionRepositorio : BaseRepository<TMaterialCriterioVerificacion, MaterialCriterioVerificacionBO>
    {
        #region Metodos Base
        public MaterialCriterioVerificacionRepositorio() : base()
        {
        }
        public MaterialCriterioVerificacionRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<MaterialCriterioVerificacionBO> GetBy(Expression<Func<TMaterialCriterioVerificacion, bool>> filter)
        {
            IEnumerable<TMaterialCriterioVerificacion> listado = base.GetBy(filter);
            List<MaterialCriterioVerificacionBO> listadoBO = new List<MaterialCriterioVerificacionBO>();
            foreach (var itemEntidad in listado)
            {
                MaterialCriterioVerificacionBO objetoBO = Mapper.Map<TMaterialCriterioVerificacion, MaterialCriterioVerificacionBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public MaterialCriterioVerificacionBO FirstById(int id)
        {
            try
            {
                TMaterialCriterioVerificacion entidad = base.FirstById(id);
                MaterialCriterioVerificacionBO objetoBO = new MaterialCriterioVerificacionBO();
                Mapper.Map<TMaterialCriterioVerificacion, MaterialCriterioVerificacionBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public MaterialCriterioVerificacionBO FirstBy(Expression<Func<TMaterialCriterioVerificacion, bool>> filter)
        {
            try
            {
                TMaterialCriterioVerificacion entidad = base.FirstBy(filter);
                MaterialCriterioVerificacionBO objetoBO = Mapper.Map<TMaterialCriterioVerificacion, MaterialCriterioVerificacionBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(MaterialCriterioVerificacionBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TMaterialCriterioVerificacion entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<MaterialCriterioVerificacionBO> listadoBO)
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

        public bool Update(MaterialCriterioVerificacionBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TMaterialCriterioVerificacion entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<MaterialCriterioVerificacionBO> listadoBO)
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
        private void AsignacionId(TMaterialCriterioVerificacion entidad, MaterialCriterioVerificacionBO objetoBO)
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

        private TMaterialCriterioVerificacion MapeoEntidad(MaterialCriterioVerificacionBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TMaterialCriterioVerificacion entidad = new TMaterialCriterioVerificacion();
                entidad = Mapper.Map<MaterialCriterioVerificacionBO, TMaterialCriterioVerificacion>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos

                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<MaterialCriterioVerificacionBO> GetFiltered<KProperty>(IEnumerable<Expression<Func<TMaterialCriterioVerificacion, bool>>> filters, Expression<Func<TMaterialCriterioVerificacion, KProperty>> orderBy, bool ascending)
        {
            IEnumerable<TMaterialCriterioVerificacion> listado = base.GetFiltered(filters, orderBy, ascending);
            List<MaterialCriterioVerificacionBO> listadoBO = new List<MaterialCriterioVerificacionBO>();

            foreach (var itemEntidad in listado)
            {
                MaterialCriterioVerificacionBO objetoBO = Mapper.Map<TMaterialCriterioVerificacion, MaterialCriterioVerificacionBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
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
        public List<MaterialCriterioVerificacionBO> Obtener()
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
