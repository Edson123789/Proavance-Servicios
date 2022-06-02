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
    /// PuestoTrabajoCaracteristicaPersonalRepositorio
    /// Autor: Luis H., Edgar S.
    /// Fecha: 29/01/2021
    /// <summary>
    /// Gestión de Experiencia Requerida de Perfil de Puestos de Trabajo
    /// </summary>
    public class PuestoTrabajoCaracteristicaPersonalRepositorio : BaseRepository<TPuestoTrabajoCaracteristicaPersonal, PuestoTrabajoCaracteristicaPersonalBO>
    {
        #region Metodos Base
        public PuestoTrabajoCaracteristicaPersonalRepositorio() : base()
        {
        }
        public PuestoTrabajoCaracteristicaPersonalRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<PuestoTrabajoCaracteristicaPersonalBO> GetBy(Expression<Func<TPuestoTrabajoCaracteristicaPersonal, bool>> filter)
        {
            IEnumerable<TPuestoTrabajoCaracteristicaPersonal> listado = base.GetBy(filter);
            List<PuestoTrabajoCaracteristicaPersonalBO> listadoBO = new List<PuestoTrabajoCaracteristicaPersonalBO>();
            foreach (var itemEntidad in listado)
            {
                PuestoTrabajoCaracteristicaPersonalBO objetoBO = Mapper.Map<TPuestoTrabajoCaracteristicaPersonal, PuestoTrabajoCaracteristicaPersonalBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public PuestoTrabajoCaracteristicaPersonalBO FirstById(int id)
        {
            try
            {
                TPuestoTrabajoCaracteristicaPersonal entidad = base.FirstById(id);
                PuestoTrabajoCaracteristicaPersonalBO objetoBO = new PuestoTrabajoCaracteristicaPersonalBO();
                Mapper.Map<TPuestoTrabajoCaracteristicaPersonal, PuestoTrabajoCaracteristicaPersonalBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public PuestoTrabajoCaracteristicaPersonalBO FirstBy(Expression<Func<TPuestoTrabajoCaracteristicaPersonal, bool>> filter)
        {
            try
            {
                TPuestoTrabajoCaracteristicaPersonal entidad = base.FirstBy(filter);
                PuestoTrabajoCaracteristicaPersonalBO objetoBO = Mapper.Map<TPuestoTrabajoCaracteristicaPersonal, PuestoTrabajoCaracteristicaPersonalBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(PuestoTrabajoCaracteristicaPersonalBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TPuestoTrabajoCaracteristicaPersonal entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<PuestoTrabajoCaracteristicaPersonalBO> listadoBO)
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

        public bool Update(PuestoTrabajoCaracteristicaPersonalBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TPuestoTrabajoCaracteristicaPersonal entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<PuestoTrabajoCaracteristicaPersonalBO> listadoBO)
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
        private void AsignacionId(TPuestoTrabajoCaracteristicaPersonal entidad, PuestoTrabajoCaracteristicaPersonalBO objetoBO)
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

        private TPuestoTrabajoCaracteristicaPersonal MapeoEntidad(PuestoTrabajoCaracteristicaPersonalBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TPuestoTrabajoCaracteristicaPersonal entidad = new TPuestoTrabajoCaracteristicaPersonal();
                entidad = Mapper.Map<PuestoTrabajoCaracteristicaPersonalBO, TPuestoTrabajoCaracteristicaPersonal>(objetoBO,
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

        /// PuestoTrabajoCaracteristicaPersonalRepositorio
        /// Autor: Luis H., Edgar S.
        /// Fecha: 29/01/2021
        /// <summary>
        /// Obtiene lista de caracteristicas personales de un determinado puesto de trabajo
        /// </summary>
        /// <returns> Lista de Carateríticas de Personal Por Perfil de Puesto de Trabajo </returns>
        /// <returns> Lista de Objeto DTO :  List<PuestoTrabajoCaracteristicaPersonalDTO> </returns>
        public List<PuestoTrabajoCaracteristicaPersonalDTO> ObtenerPuestoTrabajoCaracteristicaPersonal(int idPerfilPuestoTrabajo)
		{
			try
			{
				List<PuestoTrabajoCaracteristicaPersonalDTO> lista = new List<PuestoTrabajoCaracteristicaPersonalDTO>();
				var _query = "SELECT Id, IdPerfilPuestoTrabajo, EdadMinima, EdadMaxima, IdSexo, IdEstadoCivil, Sexo, EstadoCivil FROM [gp].[V_TPuestoTrabajoCaracteristicaPersonal_ObtenerListaCaracteristicaPersonal] WHERE Estado = 1 AND IdPerfilPuestoTrabajo = @IdPerfilPuestoTrabajo";
				var res = _dapper.QueryDapper(_query, new { IdPerfilPuestoTrabajo = idPerfilPuestoTrabajo });
				if (!res.Contains("[]") && !string.IsNullOrEmpty(res))
				{
					lista = JsonConvert.DeserializeObject<List<PuestoTrabajoCaracteristicaPersonalDTO>>(res);
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
