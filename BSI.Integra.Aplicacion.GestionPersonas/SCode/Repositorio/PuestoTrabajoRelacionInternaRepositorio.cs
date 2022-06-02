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
    public class PuestoTrabajoRelacionInternaRepositorio : BaseRepository<TPuestoTrabajoRelacionInterna, PuestoTrabajoRelacionInternaBO>
    {
        #region Metodos Base
        public PuestoTrabajoRelacionInternaRepositorio() : base()
        {
        }
        public PuestoTrabajoRelacionInternaRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<PuestoTrabajoRelacionInternaBO> GetBy(Expression<Func<TPuestoTrabajoRelacionInterna, bool>> filter)
        {
            IEnumerable<TPuestoTrabajoRelacionInterna> listado = base.GetBy(filter);
            List<PuestoTrabajoRelacionInternaBO> listadoBO = new List<PuestoTrabajoRelacionInternaBO>();
            foreach (var itemEntidad in listado)
            {
                PuestoTrabajoRelacionInternaBO objetoBO = Mapper.Map<TPuestoTrabajoRelacionInterna, PuestoTrabajoRelacionInternaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public PuestoTrabajoRelacionInternaBO FirstById(int id)
        {
            try
            {
                TPuestoTrabajoRelacionInterna entidad = base.FirstById(id);
                PuestoTrabajoRelacionInternaBO objetoBO = new PuestoTrabajoRelacionInternaBO();
                Mapper.Map<TPuestoTrabajoRelacionInterna, PuestoTrabajoRelacionInternaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public PuestoTrabajoRelacionInternaBO FirstBy(Expression<Func<TPuestoTrabajoRelacionInterna, bool>> filter)
        {
            try
            {
                TPuestoTrabajoRelacionInterna entidad = base.FirstBy(filter);
                PuestoTrabajoRelacionInternaBO objetoBO = Mapper.Map<TPuestoTrabajoRelacionInterna, PuestoTrabajoRelacionInternaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(PuestoTrabajoRelacionInternaBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TPuestoTrabajoRelacionInterna entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<PuestoTrabajoRelacionInternaBO> listadoBO)
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

        public bool Update(PuestoTrabajoRelacionInternaBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TPuestoTrabajoRelacionInterna entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<PuestoTrabajoRelacionInternaBO> listadoBO)
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
        private void AsignacionId(TPuestoTrabajoRelacionInterna entidad, PuestoTrabajoRelacionInternaBO objetoBO)
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

        private TPuestoTrabajoRelacionInterna MapeoEntidad(PuestoTrabajoRelacionInternaBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TPuestoTrabajoRelacionInterna entidad = new TPuestoTrabajoRelacionInterna();
                entidad = Mapper.Map<PuestoTrabajoRelacionInternaBO, TPuestoTrabajoRelacionInterna>(objetoBO,
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
		/// Obtiene lista de relaciones internas de un determinado puesto de trabajo
		/// </summary>
		/// <returns></returns>
		public List<PuestoTrabajoRelacionInternaDTO> ObtenerPuestoTrabajoRelacionInterna(int idPerfilPuestoTrabajo)
		{
			try
			{
				List<PuestoTrabajoRelacionInternaDTO> lista = new List<PuestoTrabajoRelacionInternaDTO>();
				var _query = "SELECT IdPuestoTrabajoRelacionInterna, PuestoTrabajoRelacionInterna FROM [gp].[V_TPuestoTrabajoRelacionInterna_ObtenerPuestoRelacionInterna] WHERE Estado = 1  AND IdPerfilPuestoTrabajo = @IdPerfilPuestoTrabajo";
				var res = _dapper.QueryDapper(_query, new { IdPerfilPuestoTrabajo = idPerfilPuestoTrabajo });
				if (!res.Contains("[]") && !string.IsNullOrEmpty(res))
				{
					lista = JsonConvert.DeserializeObject<List<PuestoTrabajoRelacionInternaDTO>>(res);
				}
				return lista;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
	}
}
