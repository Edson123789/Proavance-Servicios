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
    /// Repositorio: ProcesoSeleccionRangoRepositorio
    /// Autor: Edgar S.
    /// Fecha: 29/01/2021
    /// <summary>
    /// Gestión de Rango de Proceso de Selección
    /// </summary>
    public class ProcesoSeleccionRangoRepositorio : BaseRepository<TProcesoSeleccionRango, ProcesoSeleccionRangoBO>
    {
        #region Metodos Base
        public ProcesoSeleccionRangoRepositorio() : base()
        {
        }
        public ProcesoSeleccionRangoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<ProcesoSeleccionRangoBO> GetBy(Expression<Func<TProcesoSeleccionRango, bool>> filter)
        {
            IEnumerable<TProcesoSeleccionRango> listado = base.GetBy(filter);
            List<ProcesoSeleccionRangoBO> listadoBO = new List<ProcesoSeleccionRangoBO>();
            foreach (var itemEntidad in listado)
            {
                ProcesoSeleccionRangoBO objetoBO = Mapper.Map<TProcesoSeleccionRango, ProcesoSeleccionRangoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public ProcesoSeleccionRangoBO FirstById(int id)
        {
            try
            {
                TProcesoSeleccionRango entidad = base.FirstById(id);
                ProcesoSeleccionRangoBO objetoBO = new ProcesoSeleccionRangoBO();
                Mapper.Map<TProcesoSeleccionRango, ProcesoSeleccionRangoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public ProcesoSeleccionRangoBO FirstBy(Expression<Func<TProcesoSeleccionRango, bool>> filter)
        {
            try
            {
                TProcesoSeleccionRango entidad = base.FirstBy(filter);
                ProcesoSeleccionRangoBO objetoBO = Mapper.Map<TProcesoSeleccionRango, ProcesoSeleccionRangoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(ProcesoSeleccionRangoBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TProcesoSeleccionRango entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<ProcesoSeleccionRangoBO> listadoBO)
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

        public bool Update(ProcesoSeleccionRangoBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TProcesoSeleccionRango entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<ProcesoSeleccionRangoBO> listadoBO)
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
        private void AsignacionId(TProcesoSeleccionRango entidad, ProcesoSeleccionRangoBO objetoBO)
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

        private TProcesoSeleccionRango MapeoEntidad(ProcesoSeleccionRangoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TProcesoSeleccionRango entidad = new TProcesoSeleccionRango();
                entidad = Mapper.Map<ProcesoSeleccionRangoBO, TProcesoSeleccionRango>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos

                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<ProcesoSeleccionRangoBO> GetFiltered<KProperty>(IEnumerable<Expression<Func<TProcesoSeleccionRango, bool>>> filters, Expression<Func<TProcesoSeleccionRango, KProperty>> orderBy, bool ascending)
        {
            IEnumerable<TProcesoSeleccionRango> listado = base.GetFiltered(filters, orderBy, ascending);
            List<ProcesoSeleccionRangoBO> listadoBO = new List<ProcesoSeleccionRangoBO>();

            foreach (var itemEntidad in listado)
            {
                ProcesoSeleccionRangoBO objetoBO = Mapper.Map<TProcesoSeleccionRango, ProcesoSeleccionRangoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }
            return listadoBO;
        }
        #endregion

        /// Repositorio: ProcesoSeleccionRangoRepositorio
        /// Autor: Edgar S.
        /// Fecha: 29/01/2021
        /// <summary>
        /// Obtiene lista de proceso seleccion rango para combos
        /// </summary>
        /// <returns> Lista de Id, Nombre de Rango de Proceso de Selección Registrados </returns>
        /// <returns> Lista de Objeto DTO : List<FiltroIdNombreDTO> </returns>
        public List<FiltroIdNombreDTO> ObtenerListaParaCombo()
		{
			try
			{
				return this.GetBy(x => x.Estado == true, x => new FiltroIdNombreDTO { Id = x.Id, Nombre = x.Nombre }).ToList();
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
    }
}
