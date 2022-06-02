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
    public class PuestoTrabajoLineaCarreraRepositorio : BaseRepository<TPuestoTrabajoLineaCarrera, PuestoTrabajoLineaCarreraBO>
    {

        #region Metodos Base
        public PuestoTrabajoLineaCarreraRepositorio() : base()
        {
        }
        public PuestoTrabajoLineaCarreraRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<PuestoTrabajoLineaCarreraBO> GetBy(Expression<Func<TPuestoTrabajoLineaCarrera, bool>> filter)
        {
            IEnumerable<TPuestoTrabajoLineaCarrera> listado = base.GetBy(filter);
            List<PuestoTrabajoLineaCarreraBO> listadoBO = new List<PuestoTrabajoLineaCarreraBO>();
            foreach (var itemEntidad in listado)
            {
                PuestoTrabajoLineaCarreraBO objetoBO = Mapper.Map<TPuestoTrabajoLineaCarrera, PuestoTrabajoLineaCarreraBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public PuestoTrabajoLineaCarreraBO FirstById(int id)
        {
            try
            {
                TPuestoTrabajoLineaCarrera entidad = base.FirstById(id);
                PuestoTrabajoLineaCarreraBO objetoBO = new PuestoTrabajoLineaCarreraBO();
                Mapper.Map<TPuestoTrabajoLineaCarrera, PuestoTrabajoLineaCarreraBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public PuestoTrabajoLineaCarreraBO FirstBy(Expression<Func<TPuestoTrabajoLineaCarrera, bool>> filter)
        {
            try
            {
                TPuestoTrabajoLineaCarrera entidad = base.FirstBy(filter);
                PuestoTrabajoLineaCarreraBO objetoBO = Mapper.Map<TPuestoTrabajoLineaCarrera, PuestoTrabajoLineaCarreraBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(PuestoTrabajoLineaCarreraBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TPuestoTrabajoLineaCarrera entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<PuestoTrabajoLineaCarreraBO> listadoBO)
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

        public bool Update(PuestoTrabajoLineaCarreraBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TPuestoTrabajoLineaCarrera entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<PuestoTrabajoLineaCarreraBO> listadoBO)
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
        private void AsignacionId(TPuestoTrabajoLineaCarrera entidad, PuestoTrabajoLineaCarreraBO objetoBO)
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

        private TPuestoTrabajoLineaCarrera MapeoEntidad(PuestoTrabajoLineaCarreraBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TPuestoTrabajoLineaCarrera entidad = new TPuestoTrabajoLineaCarrera();
                entidad = Mapper.Map<PuestoTrabajoLineaCarreraBO, TPuestoTrabajoLineaCarrera>(objetoBO,
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
