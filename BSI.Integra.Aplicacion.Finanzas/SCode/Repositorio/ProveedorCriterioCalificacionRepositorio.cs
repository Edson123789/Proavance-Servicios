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
    public class ProveedorCriterioCalificacionRepositorio : BaseRepository<TProveedorCriterioCalificacion, ProveedorCriterioCalificacionBO>
    {
        #region Metodos Base
        public ProveedorCriterioCalificacionRepositorio() : base()
        {
        }
        public ProveedorCriterioCalificacionRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<ProveedorCriterioCalificacionBO> GetBy(Expression<Func<TProveedorCriterioCalificacion, bool>> filter)
        {
            IEnumerable<TProveedorCriterioCalificacion> listado = base.GetBy(filter);
            List<ProveedorCriterioCalificacionBO> listadoBO = new List<ProveedorCriterioCalificacionBO>();
            foreach (var itemEntidad in listado)
            {
                ProveedorCriterioCalificacionBO objetoBO = Mapper.Map<TProveedorCriterioCalificacion, ProveedorCriterioCalificacionBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public ProveedorCriterioCalificacionBO FirstById(int id)
        {
            try
            {
                TProveedorCriterioCalificacion entidad = base.FirstById(id);
                ProveedorCriterioCalificacionBO objetoBO = new ProveedorCriterioCalificacionBO();
                Mapper.Map<TProveedorCriterioCalificacion, ProveedorCriterioCalificacionBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public ProveedorCriterioCalificacionBO FirstBy(Expression<Func<TProveedorCriterioCalificacion, bool>> filter)
        {
            try
            {
                TProveedorCriterioCalificacion entidad = base.FirstBy(filter);
                ProveedorCriterioCalificacionBO objetoBO = Mapper.Map<TProveedorCriterioCalificacion, ProveedorCriterioCalificacionBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(ProveedorCriterioCalificacionBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TProveedorCriterioCalificacion entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<ProveedorCriterioCalificacionBO> listadoBO)
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

        public bool Update(ProveedorCriterioCalificacionBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TProveedorCriterioCalificacion entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<ProveedorCriterioCalificacionBO> listadoBO)
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
        private void AsignacionId(TProveedorCriterioCalificacion entidad, ProveedorCriterioCalificacionBO objetoBO)
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

        private TProveedorCriterioCalificacion MapeoEntidad(ProveedorCriterioCalificacionBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TProveedorCriterioCalificacion entidad = new TProveedorCriterioCalificacion();
                entidad = Mapper.Map<ProveedorCriterioCalificacionBO, TProveedorCriterioCalificacion>(objetoBO,
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
    }
}
