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
    public class ProveedorCalificacionRepositorio : BaseRepository<TProveedorCalificacion, ProveedorCalificacionBO>
    {
        #region Metodos Base
        public ProveedorCalificacionRepositorio() : base()
        {
        }
        public ProveedorCalificacionRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<ProveedorCalificacionBO> GetBy(Expression<Func<TProveedorCalificacion, bool>> filter)
        {
            IEnumerable<TProveedorCalificacion> listado = base.GetBy(filter);
            List<ProveedorCalificacionBO> listadoBO = new List<ProveedorCalificacionBO>();
            foreach (var itemEntidad in listado)
            {
                ProveedorCalificacionBO objetoBO = Mapper.Map<TProveedorCalificacion, ProveedorCalificacionBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public ProveedorCalificacionBO FirstById(int id)
        {
            try
            {
                TProveedorCalificacion entidad = base.FirstById(id);
                ProveedorCalificacionBO objetoBO = new ProveedorCalificacionBO();
                Mapper.Map<TProveedorCalificacion, ProveedorCalificacionBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public ProveedorCalificacionBO FirstBy(Expression<Func<TProveedorCalificacion, bool>> filter)
        {
            try
            {
                TProveedorCalificacion entidad = base.FirstBy(filter);
                ProveedorCalificacionBO objetoBO = Mapper.Map<TProveedorCalificacion, ProveedorCalificacionBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(ProveedorCalificacionBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TProveedorCalificacion entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<ProveedorCalificacionBO> listadoBO)
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

        public bool Update(ProveedorCalificacionBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TProveedorCalificacion entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<ProveedorCalificacionBO> listadoBO)
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
        private void AsignacionId(TProveedorCalificacion entidad, ProveedorCalificacionBO objetoBO)
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

        private TProveedorCalificacion MapeoEntidad(ProveedorCalificacionBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TProveedorCalificacion entidad = new TProveedorCalificacion();
                entidad = Mapper.Map<ProveedorCalificacionBO, TProveedorCalificacion>(objetoBO,
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
