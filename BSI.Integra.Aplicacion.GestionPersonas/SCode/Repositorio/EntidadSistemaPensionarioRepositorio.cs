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
    /// Repositorio: EntidadSistemaPensionarioRepositorio
    /// Autor: Ansoli Espinoza
    /// Fecha: 16/06/2021
    /// <summary>
    /// Repositorio para de tabla T_EntidadSistemaPensionario
    /// </summary>
    public class EntidadSistemaPensionarioRepositorio : BaseRepository<TEntidadSistemaPensionario, EntidadSistemaPensionarioBO>
    {
        #region Metodos Base
        public EntidadSistemaPensionarioRepositorio() : base()
        {
        }
        public EntidadSistemaPensionarioRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<EntidadSistemaPensionarioBO> GetBy(Expression<Func<TEntidadSistemaPensionario, bool>> filter)
        {
            IEnumerable<TEntidadSistemaPensionario> listado = base.GetBy(filter);
            List<EntidadSistemaPensionarioBO> listadoBO = new List<EntidadSistemaPensionarioBO>();
            foreach (var itemEntidad in listado)
            {
                EntidadSistemaPensionarioBO objetoBO = Mapper.Map<TEntidadSistemaPensionario, EntidadSistemaPensionarioBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public EntidadSistemaPensionarioBO FirstById(int id)
        {
            try
            {
                TEntidadSistemaPensionario entidad = base.FirstById(id);
                EntidadSistemaPensionarioBO objetoBO = new EntidadSistemaPensionarioBO();
                Mapper.Map<TEntidadSistemaPensionario, EntidadSistemaPensionarioBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public EntidadSistemaPensionarioBO FirstBy(Expression<Func<TEntidadSistemaPensionario, bool>> filter)
        {
            try
            {
                TEntidadSistemaPensionario entidad = base.FirstBy(filter);
                EntidadSistemaPensionarioBO objetoBO = Mapper.Map<TEntidadSistemaPensionario, EntidadSistemaPensionarioBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(EntidadSistemaPensionarioBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TEntidadSistemaPensionario entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<EntidadSistemaPensionarioBO> listadoBO)
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

        public bool Update(EntidadSistemaPensionarioBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TEntidadSistemaPensionario entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<EntidadSistemaPensionarioBO> listadoBO)
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
        private void AsignacionId(TEntidadSistemaPensionario entidad, EntidadSistemaPensionarioBO objetoBO)
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

        private TEntidadSistemaPensionario MapeoEntidad(EntidadSistemaPensionarioBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TEntidadSistemaPensionario entidad = new TEntidadSistemaPensionario();
                entidad = Mapper.Map<EntidadSistemaPensionarioBO, TEntidadSistemaPensionario>(objetoBO,
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
        /// Repositorio: EntidadSistemaPensionarioRepositorio
        /// Autor: 
        /// Fecha: 16/06/2021
        /// <summary>
        /// Obtiene el Id y Nombre para ComboBox
        /// </summary>
        /// <returns> List<EntidadSistemaPensionarioFiltroDTO> </returns>
        public List<EntidadSistemaPensionarioFiltroDTO> GetFiltroIdNombre()
        {
            var lista = GetBy(x => true, y => new EntidadSistemaPensionarioFiltroDTO
            {
                Id = y.Id,
                Nombre = y.Nombre,
                IdSistemaPensionario = y.IdSistemaPensionario
            }).ToList();
            return lista;
        }
    }
}
