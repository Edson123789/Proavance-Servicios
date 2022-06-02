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
    /// Repositorio: SistemaPensionario
    /// Autor: Ansoli Espinoza.
    /// Fecha: 16/06/2021
    /// <summary>
    /// Gestión de Registros de T_SistemaPensionario
    /// </summary>
    public class SistemaPensionarioRepositorio : BaseRepository<TSistemaPensionario, SistemaPensionarioBO>
    {
        #region Metodos Base
        public SistemaPensionarioRepositorio() : base()
        {
        }
        public SistemaPensionarioRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<SistemaPensionarioBO> GetBy(Expression<Func<TSistemaPensionario, bool>> filter)
        {
            IEnumerable<TSistemaPensionario> listado = base.GetBy(filter);
            List<SistemaPensionarioBO> listadoBO = new List<SistemaPensionarioBO>();
            foreach (var itemEntidad in listado)
            {
                SistemaPensionarioBO objetoBO = Mapper.Map<TSistemaPensionario, SistemaPensionarioBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public SistemaPensionarioBO FirstById(int id)
        {
            try
            {
                TSistemaPensionario entidad = base.FirstById(id);
                SistemaPensionarioBO objetoBO = new SistemaPensionarioBO();
                Mapper.Map<TSistemaPensionario, SistemaPensionarioBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public SistemaPensionarioBO FirstBy(Expression<Func<TSistemaPensionario, bool>> filter)
        {
            try
            {
                TSistemaPensionario entidad = base.FirstBy(filter);
                SistemaPensionarioBO objetoBO = Mapper.Map<TSistemaPensionario, SistemaPensionarioBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(SistemaPensionarioBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TSistemaPensionario entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<SistemaPensionarioBO> listadoBO)
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

        public bool Update(SistemaPensionarioBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TSistemaPensionario entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<SistemaPensionarioBO> listadoBO)
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
        private void AsignacionId(TSistemaPensionario entidad, SistemaPensionarioBO objetoBO)
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

        private TSistemaPensionario MapeoEntidad(SistemaPensionarioBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TSistemaPensionario entidad = new TSistemaPensionario();
                entidad = Mapper.Map<SistemaPensionarioBO, TSistemaPensionario>(objetoBO,
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
        /// Repositorio: SistemaPensionarioRepositorio
        /// Autor: 
        /// Fecha: 16/06/2021
        /// <summary>
        /// Obtiene el Id y Nombre para ComboBox
        /// </summary>
        /// <returns> List<FiltroIdNombreDTO> </returns>
        public List<FiltroIdNombreDTO> GetFiltroIdNombre()
        {
            var lista = GetBy(x => true, y => new FiltroIdNombreDTO
            {
                Id = y.Id,
                Nombre = y.Nombre
            }).ToList();
            return lista;
        }
    }
}
