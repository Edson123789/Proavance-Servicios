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
    public class PuestoTrabajoGrupoComparacionRepositorio : BaseRepository<TPuestoTrabajoGrupoComparacion, PuestoTrabajoGrupoComparacionBO>
    {
        #region Metodos Base
        public PuestoTrabajoGrupoComparacionRepositorio() : base()
        {
        }
        public PuestoTrabajoGrupoComparacionRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<PuestoTrabajoGrupoComparacionBO> GetBy(Expression<Func<TPuestoTrabajoGrupoComparacion, bool>> filter)
        {
            IEnumerable<TPuestoTrabajoGrupoComparacion> listado = base.GetBy(filter);
            List<PuestoTrabajoGrupoComparacionBO> listadoBO = new List<PuestoTrabajoGrupoComparacionBO>();
            foreach (var itemEntidad in listado)
            {
                PuestoTrabajoGrupoComparacionBO objetoBO = Mapper.Map<TPuestoTrabajoGrupoComparacion, PuestoTrabajoGrupoComparacionBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public PuestoTrabajoGrupoComparacionBO FirstById(int id)
        {
            try
            {
                TPuestoTrabajoGrupoComparacion entidad = base.FirstById(id);
                PuestoTrabajoGrupoComparacionBO objetoBO = new PuestoTrabajoGrupoComparacionBO();
                Mapper.Map<TPuestoTrabajoGrupoComparacion, PuestoTrabajoGrupoComparacionBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public PuestoTrabajoGrupoComparacionBO FirstBy(Expression<Func<TPuestoTrabajoGrupoComparacion, bool>> filter)
        {
            try
            {
                TPuestoTrabajoGrupoComparacion entidad = base.FirstBy(filter);
                PuestoTrabajoGrupoComparacionBO objetoBO = Mapper.Map<TPuestoTrabajoGrupoComparacion, PuestoTrabajoGrupoComparacionBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(PuestoTrabajoGrupoComparacionBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TPuestoTrabajoGrupoComparacion entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<PuestoTrabajoGrupoComparacionBO> listadoBO)
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

        public bool Update(PuestoTrabajoGrupoComparacionBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TPuestoTrabajoGrupoComparacion entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<PuestoTrabajoGrupoComparacionBO> listadoBO)
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
        private void AsignacionId(TPuestoTrabajoGrupoComparacion entidad, PuestoTrabajoGrupoComparacionBO objetoBO)
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

        private TPuestoTrabajoGrupoComparacion MapeoEntidad(PuestoTrabajoGrupoComparacionBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TPuestoTrabajoGrupoComparacion entidad = new TPuestoTrabajoGrupoComparacion();
                entidad = Mapper.Map<PuestoTrabajoGrupoComparacionBO, TPuestoTrabajoGrupoComparacion>(objetoBO,
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
