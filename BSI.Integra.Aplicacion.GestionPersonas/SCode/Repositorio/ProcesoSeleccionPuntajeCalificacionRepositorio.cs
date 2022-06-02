using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.GestionPersonas.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.GestionPersonas.Repositorio
{
    /// Repositorio: ProcesoSeleccionPuntajeCalificacionRepositorio
    /// Autor: Britsel C., Luis H., Edgar S.
    /// Fecha: 29/01/2021
    /// <summary>
    /// Gestión de Calificaciones y Puntajes
    /// </summary>
    public class ProcesoSeleccionPuntajeCalificacionRepositorio : BaseRepository<TProcesoSeleccionPuntajeCalificacion, ProcesoSeleccionPuntajeCalificacionBO>
    {
        #region Metodos Base
        public ProcesoSeleccionPuntajeCalificacionRepositorio() : base()
        {
        }
        public ProcesoSeleccionPuntajeCalificacionRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<ProcesoSeleccionPuntajeCalificacionBO> GetBy(Expression<Func<TProcesoSeleccionPuntajeCalificacion, bool>> filter)
        {
            IEnumerable<TProcesoSeleccionPuntajeCalificacion> listado = base.GetBy(filter);
            List<ProcesoSeleccionPuntajeCalificacionBO> listadoBO = new List<ProcesoSeleccionPuntajeCalificacionBO>();
            foreach (var itemEntidad in listado)
            {
                ProcesoSeleccionPuntajeCalificacionBO objetoBO = Mapper.Map<TProcesoSeleccionPuntajeCalificacion, ProcesoSeleccionPuntajeCalificacionBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public ProcesoSeleccionPuntajeCalificacionBO FirstById(int id)
        {
            try
            {
                TProcesoSeleccionPuntajeCalificacion entidad = base.FirstById(id);
                ProcesoSeleccionPuntajeCalificacionBO objetoBO = new ProcesoSeleccionPuntajeCalificacionBO();
                Mapper.Map<TProcesoSeleccionPuntajeCalificacion, ProcesoSeleccionPuntajeCalificacionBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public ProcesoSeleccionPuntajeCalificacionBO FirstBy(Expression<Func<TProcesoSeleccionPuntajeCalificacion, bool>> filter)
        {
            try
            {
                TProcesoSeleccionPuntajeCalificacion entidad = base.FirstBy(filter);
                ProcesoSeleccionPuntajeCalificacionBO objetoBO = Mapper.Map<TProcesoSeleccionPuntajeCalificacion, ProcesoSeleccionPuntajeCalificacionBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(ProcesoSeleccionPuntajeCalificacionBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TProcesoSeleccionPuntajeCalificacion entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<ProcesoSeleccionPuntajeCalificacionBO> listadoBO)
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

        public bool Update(ProcesoSeleccionPuntajeCalificacionBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TProcesoSeleccionPuntajeCalificacion entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<ProcesoSeleccionPuntajeCalificacionBO> listadoBO)
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
        private void AsignacionId(TProcesoSeleccionPuntajeCalificacion entidad, ProcesoSeleccionPuntajeCalificacionBO objetoBO)
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

        private TProcesoSeleccionPuntajeCalificacion MapeoEntidad(ProcesoSeleccionPuntajeCalificacionBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TProcesoSeleccionPuntajeCalificacion entidad = new TProcesoSeleccionPuntajeCalificacion();
                entidad = Mapper.Map<ProcesoSeleccionPuntajeCalificacionBO, TProcesoSeleccionPuntajeCalificacion>(objetoBO,
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
