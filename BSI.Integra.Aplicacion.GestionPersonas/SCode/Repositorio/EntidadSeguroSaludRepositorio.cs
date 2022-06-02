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

namespace BSI.Integra.Aplicacion.GestionPersonas.SCode.Repositorio
{
    /// Repositorio: EntidadSeguroSaludRepositorio
    /// Autor: Britsel Calluchi - Luis Huallpa.
    /// Fecha: 16/06/2021
    /// <summary>
    /// Repositorio para de tabla T_EntidadSeguroSalud
    /// </summary>
    public class EntidadSeguroSaludRepositorio : BaseRepository<TEntidadSeguroSalud, EntidadSeguroSaludBO>
    {
        #region Metodos Base
        public EntidadSeguroSaludRepositorio() : base()
        {
        }
        public EntidadSeguroSaludRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<EntidadSeguroSaludBO> GetBy(Expression<Func<TEntidadSeguroSalud, bool>> filter)
        {
            IEnumerable<TEntidadSeguroSalud> listado = base.GetBy(filter);
            List<EntidadSeguroSaludBO> listadoBO = new List<EntidadSeguroSaludBO>();
            foreach (var itemEntidad in listado)
            {
                EntidadSeguroSaludBO objetoBO = Mapper.Map<TEntidadSeguroSalud, EntidadSeguroSaludBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public EntidadSeguroSaludBO FirstById(int id)
        {
            try
            {
                TEntidadSeguroSalud entidad = base.FirstById(id);
                EntidadSeguroSaludBO objetoBO = new EntidadSeguroSaludBO();
                Mapper.Map<TEntidadSeguroSalud, EntidadSeguroSaludBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public EntidadSeguroSaludBO FirstBy(Expression<Func<TEntidadSeguroSalud, bool>> filter)
        {
            try
            {
                TEntidadSeguroSalud entidad = base.FirstBy(filter);
                EntidadSeguroSaludBO objetoBO = Mapper.Map<TEntidadSeguroSalud, EntidadSeguroSaludBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(EntidadSeguroSaludBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TEntidadSeguroSalud entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<EntidadSeguroSaludBO> listadoBO)
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

        public bool Update(EntidadSeguroSaludBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TEntidadSeguroSalud entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<EntidadSeguroSaludBO> listadoBO)
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
        private void AsignacionId(TEntidadSeguroSalud entidad, EntidadSeguroSaludBO objetoBO)
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

        private TEntidadSeguroSalud MapeoEntidad(EntidadSeguroSaludBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TEntidadSeguroSalud entidad = new TEntidadSeguroSalud();
                entidad = Mapper.Map<EntidadSeguroSaludBO, TEntidadSeguroSalud>(objetoBO,
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
        /// Repositorio: EntidadSeguroSaludRepositorio
        /// Autor: 
        /// Fecha: 16/06/2021
        /// <summary>
        /// Retorna lista de entidad de seguro de salud para combos
        /// </summary>
        /// <returns> List<FiltroIdNombreDTO> </returns>
        public List<FiltroIdNombreDTO> ObtenerListaEntidadSeguroSalud()
		{
			try
			{
				return this.GetBy(x => true).Select(x => new FiltroIdNombreDTO { Id = x.Id, Nombre = x.Nombre }).ToList();
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
    }
}
