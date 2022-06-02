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
    /// Repositorio: MotivoCeseRepositorio
    /// Autor: Ansoli Espinoza.
    /// Fecha: 16/06/2021
    /// <summary>
    /// Repositorio para de tabla T_MotivoCese
    /// </summary>
    public class MotivoCeseRepositorio : BaseRepository<TMotivoCese, MotivoCeseBO>
    {
        #region Metodos Base
        public MotivoCeseRepositorio() : base()
        {
        }
        public MotivoCeseRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<MotivoCeseBO> GetBy(Expression<Func<TMotivoCese, bool>> filter)
        {
            IEnumerable<TMotivoCese> listado = base.GetBy(filter);
            List<MotivoCeseBO> listadoBO = new List<MotivoCeseBO>();
            foreach (var itemEntidad in listado)
            {
                MotivoCeseBO objetoBO = Mapper.Map<TMotivoCese, MotivoCeseBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public MotivoCeseBO FirstById(int id)
        {
            try
            {
                TMotivoCese entidad = base.FirstById(id);
                MotivoCeseBO objetoBO = new MotivoCeseBO();
                Mapper.Map<TMotivoCese, MotivoCeseBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public MotivoCeseBO FirstBy(Expression<Func<TMotivoCese, bool>> filter)
        {
            try
            {
                TMotivoCese entidad = base.FirstBy(filter);
                MotivoCeseBO objetoBO = Mapper.Map<TMotivoCese, MotivoCeseBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(MotivoCeseBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TMotivoCese entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<MotivoCeseBO> listadoBO)
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

        public bool Update(MotivoCeseBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TMotivoCese entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<MotivoCeseBO> listadoBO)
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
        private void AsignacionId(TMotivoCese entidad, MotivoCeseBO objetoBO)
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

        private TMotivoCese MapeoEntidad(MotivoCeseBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TMotivoCese entidad = new TMotivoCese();
                entidad = Mapper.Map<MotivoCeseBO, TMotivoCese>(objetoBO,
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
        /// Repositorio: MotivoCeseRepositorio
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
