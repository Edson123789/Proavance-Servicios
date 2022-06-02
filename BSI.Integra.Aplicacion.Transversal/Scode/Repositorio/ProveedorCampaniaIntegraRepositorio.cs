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
    public class ProveedorCampaniaIntegraRepositorio : BaseRepository<TProveedorCampaniaIntegra, ProveedorCampaniaIntegraBO>
    {
        #region Metodos Base
        public ProveedorCampaniaIntegraRepositorio() : base()
        {
        }
        public ProveedorCampaniaIntegraRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<ProveedorCampaniaIntegraBO> GetBy(Expression<Func<TProveedorCampaniaIntegra, bool>> filter)
        {
            IEnumerable<TProveedorCampaniaIntegra> listado = base.GetBy(filter);
            List<ProveedorCampaniaIntegraBO> listadoBO = new List<ProveedorCampaniaIntegraBO>();
            foreach (var itemEntidad in listado)
            {
                ProveedorCampaniaIntegraBO objetoBO = Mapper.Map<TProveedorCampaniaIntegra, ProveedorCampaniaIntegraBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public ProveedorCampaniaIntegraBO FirstById(int id)
        {
            try
            {
                TProveedorCampaniaIntegra entidad = base.FirstById(id);
                ProveedorCampaniaIntegraBO objetoBO = new ProveedorCampaniaIntegraBO();
                Mapper.Map<TProveedorCampaniaIntegra, ProveedorCampaniaIntegraBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public ProveedorCampaniaIntegraBO FirstBy(Expression<Func<TProveedorCampaniaIntegra, bool>> filter)
        {
            try
            {
                TProveedorCampaniaIntegra entidad = base.FirstBy(filter);
                ProveedorCampaniaIntegraBO objetoBO = Mapper.Map<TProveedorCampaniaIntegra, ProveedorCampaniaIntegraBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(ProveedorCampaniaIntegraBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TProveedorCampaniaIntegra entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<ProveedorCampaniaIntegraBO> listadoBO)
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

        public bool Update(ProveedorCampaniaIntegraBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TProveedorCampaniaIntegra entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<ProveedorCampaniaIntegraBO> listadoBO)
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
        private void AsignacionId(TProveedorCampaniaIntegra entidad, ProveedorCampaniaIntegraBO objetoBO)
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

        private TProveedorCampaniaIntegra MapeoEntidad(ProveedorCampaniaIntegraBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TProveedorCampaniaIntegra entidad = new TProveedorCampaniaIntegra();
                entidad = Mapper.Map<ProveedorCampaniaIntegraBO, TProveedorCampaniaIntegra>(objetoBO,
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
        /// Obtiene todos los registros sin los campos de auditoría.
        /// </summary>
        /// <returns></returns>
        public List<ProveedorCampaniaIntegraDatosDTO> ObtenerTodoGrid()
        {
            try
            {
                var lista = GetBy(x => true, y => new ProveedorCampaniaIntegraDatosDTO
                {
                    Id = y.Id,
                    Nombre = y.Nombre,
                    PorDefecto = y.PorDefecto,
                }).OrderByDescending(x => x.Id).ToList();

                return lista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

    }

}
