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
    public class PuestoTrabajoObjetivoRepositorio : BaseRepository<TPuestoTrabajoObjetivo, PuestoTrabajoObjetivoBO>
    {

        #region Metodos Base
        public PuestoTrabajoObjetivoRepositorio() : base()
        {
        }
        public PuestoTrabajoObjetivoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<PuestoTrabajoObjetivoBO> GetBy(Expression<Func<TPuestoTrabajoObjetivo, bool>> filter)
        {
            IEnumerable<TPuestoTrabajoObjetivo> listado = base.GetBy(filter);
            List<PuestoTrabajoObjetivoBO> listadoBO = new List<PuestoTrabajoObjetivoBO>();
            foreach (var itemEntidad in listado)
            {
                PuestoTrabajoObjetivoBO objetoBO = Mapper.Map<TPuestoTrabajoObjetivo, PuestoTrabajoObjetivoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public PuestoTrabajoObjetivoBO FirstById(int id)
        {
            try
            {
                TPuestoTrabajoObjetivo entidad = base.FirstById(id);
                PuestoTrabajoObjetivoBO objetoBO = new PuestoTrabajoObjetivoBO();
                Mapper.Map<TPuestoTrabajoObjetivo, PuestoTrabajoObjetivoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public PuestoTrabajoObjetivoBO FirstBy(Expression<Func<TPuestoTrabajoObjetivo, bool>> filter)
        {
            try
            {
                TPuestoTrabajoObjetivo entidad = base.FirstBy(filter);
                PuestoTrabajoObjetivoBO objetoBO = Mapper.Map<TPuestoTrabajoObjetivo, PuestoTrabajoObjetivoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(PuestoTrabajoObjetivoBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TPuestoTrabajoObjetivo entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<PuestoTrabajoObjetivoBO> listadoBO)
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

        public bool Update(PuestoTrabajoObjetivoBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TPuestoTrabajoObjetivo entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<PuestoTrabajoObjetivoBO> listadoBO)
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
        private void AsignacionId(TPuestoTrabajoObjetivo entidad, PuestoTrabajoObjetivoBO objetoBO)
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

        private TPuestoTrabajoObjetivo MapeoEntidad(PuestoTrabajoObjetivoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TPuestoTrabajoObjetivo entidad = new TPuestoTrabajoObjetivo();
                entidad = Mapper.Map<PuestoTrabajoObjetivoBO, TPuestoTrabajoObjetivo>(objetoBO,
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
