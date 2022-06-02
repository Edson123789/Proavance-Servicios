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
    public class PuestoTrabajoPremioRepositorio : BaseRepository<TPuestoTrabajoPremio, PuestoTrabajoPremioBO>
    {
        #region Metodos Base
        public PuestoTrabajoPremioRepositorio() : base()
        {
        }
        public PuestoTrabajoPremioRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<PuestoTrabajoPremioBO> GetBy(Expression<Func<TPuestoTrabajoPremio, bool>> filter)
        {
            IEnumerable<TPuestoTrabajoPremio> listado = base.GetBy(filter);
            List<PuestoTrabajoPremioBO> listadoBO = new List<PuestoTrabajoPremioBO>();
            foreach (var itemEntidad in listado)
            {
                PuestoTrabajoPremioBO objetoBO = Mapper.Map<TPuestoTrabajoPremio, PuestoTrabajoPremioBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public PuestoTrabajoPremioBO FirstById(int id)
        {
            try
            {
                TPuestoTrabajoPremio entidad = base.FirstById(id);
                PuestoTrabajoPremioBO objetoBO = new PuestoTrabajoPremioBO();
                Mapper.Map<TPuestoTrabajoPremio, PuestoTrabajoPremioBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public PuestoTrabajoPremioBO FirstBy(Expression<Func<TPuestoTrabajoPremio, bool>> filter)
        {
            try
            {
                TPuestoTrabajoPremio entidad = base.FirstBy(filter);
                PuestoTrabajoPremioBO objetoBO = Mapper.Map<TPuestoTrabajoPremio, PuestoTrabajoPremioBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(PuestoTrabajoPremioBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TPuestoTrabajoPremio entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<PuestoTrabajoPremioBO> listadoBO)
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

        public bool Update(PuestoTrabajoPremioBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TPuestoTrabajoPremio entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<PuestoTrabajoPremioBO> listadoBO)
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
        private void AsignacionId(TPuestoTrabajoPremio entidad, PuestoTrabajoPremioBO objetoBO)
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

        private TPuestoTrabajoPremio MapeoEntidad(PuestoTrabajoPremioBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TPuestoTrabajoPremio entidad = new TPuestoTrabajoPremio();
                entidad = Mapper.Map<PuestoTrabajoPremioBO, TPuestoTrabajoPremio>(objetoBO,
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
