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
    /// Repositorio: SexoRepositorio
    /// Autor: Ansoli Espinoza - Edgar Serruto.
    /// Fecha: 29/01/2021
    /// <summary>
    /// Gestión de Género
    /// </summary>
    public class SexoRepositorio : BaseRepository<TSexo, SexoBO>
    {
        #region Metodos Base
        public SexoRepositorio() : base()
        {
        }
        public SexoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<SexoBO> GetBy(Expression<Func<TSexo, bool>> filter)
        {
            IEnumerable<TSexo> listado = base.GetBy(filter);
            List<SexoBO> listadoBO = new List<SexoBO>();
            foreach (var itemEntidad in listado)
            {
                SexoBO objetoBO = Mapper.Map<TSexo, SexoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public SexoBO FirstById(int id)
        {
            try
            {
                TSexo entidad = base.FirstById(id);
                SexoBO objetoBO = new SexoBO();
                Mapper.Map<TSexo, SexoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public SexoBO FirstBy(Expression<Func<TSexo, bool>> filter)
        {
            try
            {
                TSexo entidad = base.FirstBy(filter);
                SexoBO objetoBO = Mapper.Map<TSexo, SexoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(SexoBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TSexo entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<SexoBO> listadoBO)
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

        public bool Update(SexoBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TSexo entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<SexoBO> listadoBO)
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
        private void AsignacionId(TSexo entidad, SexoBO objetoBO)
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

        private TSexo MapeoEntidad(SexoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TSexo entidad = new TSexo();
                entidad = Mapper.Map<SexoBO, TSexo>(objetoBO,
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
        /// Repositorio: SexoRepositorio
        /// Autor: Edgar S.
        /// Fecha: 29/01/2021
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
