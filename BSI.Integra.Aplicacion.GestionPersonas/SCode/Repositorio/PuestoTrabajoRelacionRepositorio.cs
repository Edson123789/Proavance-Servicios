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
    public class PuestoTrabajoRelacionRepositorio : BaseRepository<TPuestoTrabajoRelacion, PuestoTrabajoRelacionBO>
    {
        #region Metodos Base
        public PuestoTrabajoRelacionRepositorio() : base()
        {
        }
        public PuestoTrabajoRelacionRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<PuestoTrabajoRelacionBO> GetBy(Expression<Func<TPuestoTrabajoRelacion, bool>> filter)
        {
            IEnumerable<TPuestoTrabajoRelacion> listado = base.GetBy(filter);
            List<PuestoTrabajoRelacionBO> listadoBO = new List<PuestoTrabajoRelacionBO>();
            foreach (var itemEntidad in listado)
            {
                PuestoTrabajoRelacionBO objetoBO = Mapper.Map<TPuestoTrabajoRelacion, PuestoTrabajoRelacionBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public PuestoTrabajoRelacionBO FirstById(int id)
        {
            try
            {
                TPuestoTrabajoRelacion entidad = base.FirstById(id);
                PuestoTrabajoRelacionBO objetoBO = new PuestoTrabajoRelacionBO();
                Mapper.Map<TPuestoTrabajoRelacion, PuestoTrabajoRelacionBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public PuestoTrabajoRelacionBO FirstBy(Expression<Func<TPuestoTrabajoRelacion, bool>> filter)
        {
            try
            {
                TPuestoTrabajoRelacion entidad = base.FirstBy(filter);
                PuestoTrabajoRelacionBO objetoBO = Mapper.Map<TPuestoTrabajoRelacion, PuestoTrabajoRelacionBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(PuestoTrabajoRelacionBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TPuestoTrabajoRelacion entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<PuestoTrabajoRelacionBO> listadoBO)
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

        public bool Update(PuestoTrabajoRelacionBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TPuestoTrabajoRelacion entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<PuestoTrabajoRelacionBO> listadoBO)
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
        private void AsignacionId(TPuestoTrabajoRelacion entidad, PuestoTrabajoRelacionBO objetoBO)
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

        private TPuestoTrabajoRelacion MapeoEntidad(PuestoTrabajoRelacionBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TPuestoTrabajoRelacion entidad = new TPuestoTrabajoRelacion();
                entidad = Mapper.Map<PuestoTrabajoRelacionBO, TPuestoTrabajoRelacion>(objetoBO,
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
