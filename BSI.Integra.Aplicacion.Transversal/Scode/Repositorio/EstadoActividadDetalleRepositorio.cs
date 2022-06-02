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

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    public class EstadoActividadDetalleRepositorio : BaseRepository<TEstadoActividadDetalle, EstadoActividadDetalleBO>
    {
        #region Metodos Base
        public EstadoActividadDetalleRepositorio() : base()
        {
        }
        public EstadoActividadDetalleRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<EstadoActividadDetalleBO> GetBy(Expression<Func<TEstadoActividadDetalle, bool>> filter)
        {
            IEnumerable<TEstadoActividadDetalle> listado = base.GetBy(filter);
            List<EstadoActividadDetalleBO> listadoBO = new List<EstadoActividadDetalleBO>();
            foreach (var itemEntidad in listado)
            {
                EstadoActividadDetalleBO objetoBO = Mapper.Map<TEstadoActividadDetalle, EstadoActividadDetalleBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public EstadoActividadDetalleBO FirstById(int id)
        {
            try
            {
                TEstadoActividadDetalle entidad = base.FirstById(id);
                EstadoActividadDetalleBO objetoBO = new EstadoActividadDetalleBO();
                Mapper.Map<TEstadoActividadDetalle, EstadoActividadDetalleBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public EstadoActividadDetalleBO FirstBy(Expression<Func<TEstadoActividadDetalle, bool>> filter)
        {
            try
            {
                TEstadoActividadDetalle entidad = base.FirstBy(filter);
                EstadoActividadDetalleBO objetoBO = Mapper.Map<TEstadoActividadDetalle, EstadoActividadDetalleBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(EstadoActividadDetalleBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TEstadoActividadDetalle entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<EstadoActividadDetalleBO> listadoBO)
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

        public bool Update(EstadoActividadDetalleBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TEstadoActividadDetalle entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<EstadoActividadDetalleBO> listadoBO)
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
        private void AsignacionId(TEstadoActividadDetalle entidad, EstadoActividadDetalleBO objetoBO)
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

        private TEstadoActividadDetalle MapeoEntidad(EstadoActividadDetalleBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TEstadoActividadDetalle entidad = new TEstadoActividadDetalle();
                entidad = Mapper.Map<EstadoActividadDetalleBO, TEstadoActividadDetalle>(objetoBO,
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
		/// Obtiene todos los detalles de actividad para combobox
		/// </summary>
		/// <returns></returns>
		public List<EstadoActividadDetalleFiltroDTO> ObtenerDetalleActividadFiltroCodigo()
        {
            try
            {
                return GetBy(x => x.Estado == true, x => new EstadoActividadDetalleFiltroDTO { Id = x.Id, Nombre = x.Nombre }).ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
