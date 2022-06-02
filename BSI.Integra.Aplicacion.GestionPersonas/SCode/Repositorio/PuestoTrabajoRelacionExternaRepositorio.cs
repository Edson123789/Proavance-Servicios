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
    public class PuestoTrabajoRelacionExternaRepositorio : BaseRepository<TPuestoTrabajoRelacionExterna, PuestoTrabajoRelacionExternaBO>
    {
        #region Metodos Base
        public PuestoTrabajoRelacionExternaRepositorio() : base()
        {
        }
        public PuestoTrabajoRelacionExternaRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<PuestoTrabajoRelacionExternaBO> GetBy(Expression<Func<TPuestoTrabajoRelacionExterna, bool>> filter)
        {
            IEnumerable<TPuestoTrabajoRelacionExterna> listado = base.GetBy(filter);
            List<PuestoTrabajoRelacionExternaBO> listadoBO = new List<PuestoTrabajoRelacionExternaBO>();
            foreach (var itemEntidad in listado)
            {
                PuestoTrabajoRelacionExternaBO objetoBO = Mapper.Map<TPuestoTrabajoRelacionExterna, PuestoTrabajoRelacionExternaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public PuestoTrabajoRelacionExternaBO FirstById(int id)
        {
            try
            {
                TPuestoTrabajoRelacionExterna entidad = base.FirstById(id);
                PuestoTrabajoRelacionExternaBO objetoBO = new PuestoTrabajoRelacionExternaBO();
                Mapper.Map<TPuestoTrabajoRelacionExterna, PuestoTrabajoRelacionExternaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public PuestoTrabajoRelacionExternaBO FirstBy(Expression<Func<TPuestoTrabajoRelacionExterna, bool>> filter)
        {
            try
            {
                TPuestoTrabajoRelacionExterna entidad = base.FirstBy(filter);
                PuestoTrabajoRelacionExternaBO objetoBO = Mapper.Map<TPuestoTrabajoRelacionExterna, PuestoTrabajoRelacionExternaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(PuestoTrabajoRelacionExternaBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TPuestoTrabajoRelacionExterna entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<PuestoTrabajoRelacionExternaBO> listadoBO)
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

        public bool Update(PuestoTrabajoRelacionExternaBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TPuestoTrabajoRelacionExterna entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<PuestoTrabajoRelacionExternaBO> listadoBO)
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
        private void AsignacionId(TPuestoTrabajoRelacionExterna entidad, PuestoTrabajoRelacionExternaBO objetoBO)
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

        private TPuestoTrabajoRelacionExterna MapeoEntidad(PuestoTrabajoRelacionExternaBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TPuestoTrabajoRelacionExterna entidad = new TPuestoTrabajoRelacionExterna();
                entidad = Mapper.Map<PuestoTrabajoRelacionExternaBO, TPuestoTrabajoRelacionExterna>(objetoBO,
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
