using System;
using System.Collections.Generic;
using System.Linq;
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
    public class ProveedorSubCriterioCalificacionRepositorio : BaseRepository<TProveedorSubCriterioCalificacion, ProveedorSubCriterioCalificacionBO>
    {
        #region Metodos Base
        public ProveedorSubCriterioCalificacionRepositorio() : base()
        {
        }
        public ProveedorSubCriterioCalificacionRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<ProveedorSubCriterioCalificacionBO> GetBy(Expression<Func<TProveedorSubCriterioCalificacion, bool>> filter)
        {
            IEnumerable<TProveedorSubCriterioCalificacion> listado = base.GetBy(filter);
            List<ProveedorSubCriterioCalificacionBO> listadoBO = new List<ProveedorSubCriterioCalificacionBO>();
            foreach (var itemEntidad in listado)
            {
                ProveedorSubCriterioCalificacionBO objetoBO = Mapper.Map<TProveedorSubCriterioCalificacion, ProveedorSubCriterioCalificacionBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public ProveedorSubCriterioCalificacionBO FirstById(int id)
        {
            try
            {
                TProveedorSubCriterioCalificacion entidad = base.FirstById(id);
                ProveedorSubCriterioCalificacionBO objetoBO = new ProveedorSubCriterioCalificacionBO();
                Mapper.Map<TProveedorSubCriterioCalificacion, ProveedorSubCriterioCalificacionBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public ProveedorSubCriterioCalificacionBO FirstBy(Expression<Func<TProveedorSubCriterioCalificacion, bool>> filter)
        {
            try
            {
                TProveedorSubCriterioCalificacion entidad = base.FirstBy(filter);
                ProveedorSubCriterioCalificacionBO objetoBO = Mapper.Map<TProveedorSubCriterioCalificacion, ProveedorSubCriterioCalificacionBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(ProveedorSubCriterioCalificacionBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TProveedorSubCriterioCalificacion entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<ProveedorSubCriterioCalificacionBO> listadoBO)
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

        public bool Update(ProveedorSubCriterioCalificacionBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TProveedorSubCriterioCalificacion entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<ProveedorSubCriterioCalificacionBO> listadoBO)
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
        private void AsignacionId(TProveedorSubCriterioCalificacion entidad, ProveedorSubCriterioCalificacionBO objetoBO)
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

        private TProveedorSubCriterioCalificacion MapeoEntidad(ProveedorSubCriterioCalificacionBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TProveedorSubCriterioCalificacion entidad = new TProveedorSubCriterioCalificacion();
                entidad = Mapper.Map<ProveedorSubCriterioCalificacionBO, TProveedorSubCriterioCalificacion>(objetoBO,
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
        /// Obtiene el id y idcriterioCalificacion de la tabla ProveedorSubCriterioCalificacion.
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        public object ObtenerSubCriterioCalificacion()
        {
            try
            {
                var listaSubCriterioProveedor = this.GetBy(x => x.Estado == true , x => new { Id = x.Id, IdCriterioCalificacion=x.IdProveedorCriterioCalificacion,Nombre = x.Nombre+" ["+x.Puntaje+" ptos]",Puntaje=x.Puntaje }).ToList();
                return listaSubCriterioProveedor;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
