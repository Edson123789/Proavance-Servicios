using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Operaciones.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;

namespace BSI.Integra.Aplicacion.Operaciones.Repositorio
{
    public class RaHistorialCambioAlumnoTipoRepositorio : BaseRepository<TRaHistorialCambioAlumnoTipo, RaHistorialCambioAlumnoTipoBO>
    {
        #region Metodos Base
        public RaHistorialCambioAlumnoTipoRepositorio() : base()
        {
        }
        public RaHistorialCambioAlumnoTipoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<RaHistorialCambioAlumnoTipoBO> GetBy(Expression<Func<TRaHistorialCambioAlumnoTipo, bool>> filter)
        {
            IEnumerable<TRaHistorialCambioAlumnoTipo> listado = base.GetBy(filter);
            List<RaHistorialCambioAlumnoTipoBO> listadoBO = new List<RaHistorialCambioAlumnoTipoBO>();
            foreach (var itemEntidad in listado)
            {
                RaHistorialCambioAlumnoTipoBO objetoBO = Mapper.Map<TRaHistorialCambioAlumnoTipo, RaHistorialCambioAlumnoTipoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public RaHistorialCambioAlumnoTipoBO FirstById(int id)
        {
            try
            {
                TRaHistorialCambioAlumnoTipo entidad = base.FirstById(id);
                RaHistorialCambioAlumnoTipoBO objetoBO = new RaHistorialCambioAlumnoTipoBO();
                Mapper.Map<TRaHistorialCambioAlumnoTipo, RaHistorialCambioAlumnoTipoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public RaHistorialCambioAlumnoTipoBO FirstBy(Expression<Func<TRaHistorialCambioAlumnoTipo, bool>> filter)
        {
            try
            {
                TRaHistorialCambioAlumnoTipo entidad = base.FirstBy(filter);
                RaHistorialCambioAlumnoTipoBO objetoBO = Mapper.Map<TRaHistorialCambioAlumnoTipo, RaHistorialCambioAlumnoTipoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(RaHistorialCambioAlumnoTipoBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TRaHistorialCambioAlumnoTipo entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<RaHistorialCambioAlumnoTipoBO> listadoBO)
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

        public bool Update(RaHistorialCambioAlumnoTipoBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TRaHistorialCambioAlumnoTipo entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<RaHistorialCambioAlumnoTipoBO> listadoBO)
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
        private void AsignacionId(TRaHistorialCambioAlumnoTipo entidad, RaHistorialCambioAlumnoTipoBO objetoBO)
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

        private TRaHistorialCambioAlumnoTipo MapeoEntidad(RaHistorialCambioAlumnoTipoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TRaHistorialCambioAlumnoTipo entidad = new TRaHistorialCambioAlumnoTipo();
                entidad = Mapper.Map<RaHistorialCambioAlumnoTipoBO, TRaHistorialCambioAlumnoTipo>(objetoBO,
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
        /// Obtiene una lista de filtro dto
        /// </summary>
        /// <returns></returns>
        public List<FiltroDTO> ObtenerFiltro() {
            try
            {
                return this.GetBy(x => x.Estado, x => new FiltroDTO { Id = x.Id, Nombre = x.Nombre }).ToList(); ;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message); ;
            }
        }
    }
}
