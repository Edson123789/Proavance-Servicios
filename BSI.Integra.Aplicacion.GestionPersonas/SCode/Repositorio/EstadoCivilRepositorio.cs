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
    /// Repositorio: EstadoCivilRepositorio
    /// Autor: Nelson Huaman - Ansoli Espinoza - Edgar Serruto.
    /// Fecha: 29/01/2021
    /// <summary>
    /// Gestión de registros de Estado Civil
    /// </summary>
    public class EstadoCivilRepositorio : BaseRepository<TEstadoCivil, EstadoCivilBO>
    {
        #region Metodos Base
        public EstadoCivilRepositorio() : base()
        {
        }
        public EstadoCivilRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        //public IEnumerable<EstadoCivilBO> GetBy(Expression<Func<TEstadoCivil, bool>> filter)
        //{
        //    IEnumerable<TEstadoCivil> listado = base.GetBy(filter);
        //    List<EstadoCivilBO> listadoBO = new List<EstadoCivilBO>();
        //    foreach (var itemEntidad in listado)
        //    {
        //        EstadoCivilBO objetoBO = Mapper.Map<TEstadoCivil, EstadoCivilBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
        //        listadoBO.Add(objetoBO);
        //    }

        //    return listadoBO;
        //}
        public EstadoCivilBO FirstById(int id)
        {
            try
            {
                TEstadoCivil entidad = base.FirstById(id);
                EstadoCivilBO objetoBO = new EstadoCivilBO();
                Mapper.Map<TEstadoCivil, EstadoCivilBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public EstadoCivilBO FirstBy(Expression<Func<TEstadoCivil, bool>> filter)
        {
            try
            {
                TEstadoCivil entidad = base.FirstBy(filter);
                EstadoCivilBO objetoBO = Mapper.Map<TEstadoCivil, EstadoCivilBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(EstadoCivilBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TEstadoCivil entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<EstadoCivilBO> listadoBO)
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

        public bool Update(EstadoCivilBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TEstadoCivil entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<EstadoCivilBO> listadoBO)
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
        private void AsignacionId(TEstadoCivil entidad, EstadoCivilBO objetoBO)
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

        private TEstadoCivil MapeoEntidad(EstadoCivilBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TEstadoCivil entidad = new TEstadoCivil();
                entidad = Mapper.Map<EstadoCivilBO, TEstadoCivil>(objetoBO,
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
        /// Repositorio: EstadoCivilRepositorio
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
