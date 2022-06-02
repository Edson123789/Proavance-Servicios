using AutoMapper;
using BSI.Integra.Aplicacion.GestionPersonas.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace BSI.Integra.Aplicacion.GestionPersonas.Repositorio
{
    /// Repositorio: ModuloSistemaAccesoRepositorio
    /// Autor: Nelson Huaman - Ansoli Espinoza - Edgar Serruto.
    /// Fecha: 29/01/2021
    /// <summary>
    /// Gestión de Accesos al módulo
    /// </summary>
    public class ModuloSistemaAccesoRepositorio : BaseRepository<TModuloSistemaAcceso, ModuloSistemaAccesoBO>
    {
        #region Metodos Base
        public ModuloSistemaAccesoRepositorio() : base()
        {
        }
        public ModuloSistemaAccesoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<ModuloSistemaAccesoBO> GetBy(Expression<Func<TModuloSistemaAcceso, bool>> filter)
        {
            IEnumerable<TModuloSistemaAcceso> listado = base.GetBy(filter);
            List<ModuloSistemaAccesoBO> listadoBO = new List<ModuloSistemaAccesoBO>();
            foreach (var itemEntidad in listado)
            {
                ModuloSistemaAccesoBO objetoBO = Mapper.Map<TModuloSistemaAcceso, ModuloSistemaAccesoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public ModuloSistemaAccesoBO FirstById(int id)
        {
            try
            {
                TModuloSistemaAcceso entidad = base.FirstById(id);
                ModuloSistemaAccesoBO objetoBO = new ModuloSistemaAccesoBO();
                Mapper.Map<TModuloSistemaAcceso, ModuloSistemaAccesoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public ModuloSistemaAccesoBO FirstBy(Expression<Func<TModuloSistemaAcceso, bool>> filter)
        {
            try
            {
                TModuloSistemaAcceso entidad = base.FirstBy(filter);
                ModuloSistemaAccesoBO objetoBO = Mapper.Map<TModuloSistemaAcceso, ModuloSistemaAccesoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(ModuloSistemaAccesoBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TModuloSistemaAcceso entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<ModuloSistemaAccesoBO> listadoBO)
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

        public bool Update(ModuloSistemaAccesoBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TModuloSistemaAcceso entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<ModuloSistemaAccesoBO> listadoBO)
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
        private void AsignacionId(TModuloSistemaAcceso entidad, ModuloSistemaAccesoBO objetoBO)
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


        //CORREGIR IDMIGRACION
        private TModuloSistemaAcceso MapeoEntidad(ModuloSistemaAccesoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TModuloSistemaAcceso entidad = new TModuloSistemaAcceso();
                entidad = Mapper.Map<ModuloSistemaAccesoBO, TModuloSistemaAcceso>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos

                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public IEnumerable<ModuloSistemaAccesoBO> GetFiltered<KProperty>(IEnumerable<Expression<Func<TModuloSistemaAcceso, bool>>> filters, Expression<Func<TModuloSistemaAcceso, KProperty>> orderBy, bool ascending)
        {
            IEnumerable<TModuloSistemaAcceso> listado = base.GetFiltered(filters, orderBy, ascending);
            List<ModuloSistemaAccesoBO> listadoBO = new List<ModuloSistemaAccesoBO>();

            foreach (var itemEntidad in listado)
            {
                ModuloSistemaAccesoBO objetoBO = Mapper.Map<TModuloSistemaAcceso, ModuloSistemaAccesoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }
            return listadoBO;
        }
        #endregion

    }
}
