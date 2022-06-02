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
    public class PuestoTrabajoIdiomaRepositorio : BaseRepository<TPuestoTrabajoIdioma, PuestoTrabajoIdiomaBO>
    {

        #region Metodos Base
        public PuestoTrabajoIdiomaRepositorio() : base()
        {
        }
        public PuestoTrabajoIdiomaRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<PuestoTrabajoIdiomaBO> GetBy(Expression<Func<TPuestoTrabajoIdioma, bool>> filter)
        {
            IEnumerable<TPuestoTrabajoIdioma> listado = base.GetBy(filter);
            List<PuestoTrabajoIdiomaBO> listadoBO = new List<PuestoTrabajoIdiomaBO>();
            foreach (var itemEntidad in listado)
            {
                PuestoTrabajoIdiomaBO objetoBO = Mapper.Map<TPuestoTrabajoIdioma, PuestoTrabajoIdiomaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public PuestoTrabajoIdiomaBO FirstById(int id)
        {
            try
            {
                TPuestoTrabajoIdioma entidad = base.FirstById(id);
                PuestoTrabajoIdiomaBO objetoBO = new PuestoTrabajoIdiomaBO();
                Mapper.Map<TPuestoTrabajoIdioma, PuestoTrabajoIdiomaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public PuestoTrabajoIdiomaBO FirstBy(Expression<Func<TPuestoTrabajoIdioma, bool>> filter)
        {
            try
            {
                TPuestoTrabajoIdioma entidad = base.FirstBy(filter);
                PuestoTrabajoIdiomaBO objetoBO = Mapper.Map<TPuestoTrabajoIdioma, PuestoTrabajoIdiomaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(PuestoTrabajoIdiomaBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TPuestoTrabajoIdioma entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<PuestoTrabajoIdiomaBO> listadoBO)
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

        public bool Update(PuestoTrabajoIdiomaBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TPuestoTrabajoIdioma entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<PuestoTrabajoIdiomaBO> listadoBO)
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
        private void AsignacionId(TPuestoTrabajoIdioma entidad, PuestoTrabajoIdiomaBO objetoBO)
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

        private TPuestoTrabajoIdioma MapeoEntidad(PuestoTrabajoIdiomaBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TPuestoTrabajoIdioma entidad = new TPuestoTrabajoIdioma();
                entidad = Mapper.Map<PuestoTrabajoIdiomaBO, TPuestoTrabajoIdioma>(objetoBO,
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
