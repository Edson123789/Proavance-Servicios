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
    /// Repositorio: PuestoTrabajoFuncionRepositorio
    /// Autor: Luis H., Edgar S.
    /// Fecha: 29/01/2021
    /// <summary>
    /// Gestión de Puestos de Trabajo y Perfil de Puestos de Trabajo
    /// </summary>
    public class PuestoTrabajoFuncionRepositorio : BaseRepository<TPuestoTrabajoFuncion, PuestoTrabajoFuncionBO>
    {
        #region Metodos Base
        public PuestoTrabajoFuncionRepositorio() : base()
        {
        }
        public PuestoTrabajoFuncionRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<PuestoTrabajoFuncionBO> GetBy(Expression<Func<TPuestoTrabajoFuncion, bool>> filter)
        {
            IEnumerable<TPuestoTrabajoFuncion> listado = base.GetBy(filter);
            List<PuestoTrabajoFuncionBO> listadoBO = new List<PuestoTrabajoFuncionBO>();
            foreach (var itemEntidad in listado)
            {
                PuestoTrabajoFuncionBO objetoBO = Mapper.Map<TPuestoTrabajoFuncion, PuestoTrabajoFuncionBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public PuestoTrabajoFuncionBO FirstById(int id)
        {
            try
            {
                TPuestoTrabajoFuncion entidad = base.FirstById(id);
                PuestoTrabajoFuncionBO objetoBO = new PuestoTrabajoFuncionBO();
                Mapper.Map<TPuestoTrabajoFuncion, PuestoTrabajoFuncionBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public PuestoTrabajoFuncionBO FirstBy(Expression<Func<TPuestoTrabajoFuncion, bool>> filter)
        {
            try
            {
                TPuestoTrabajoFuncion entidad = base.FirstBy(filter);
                PuestoTrabajoFuncionBO objetoBO = Mapper.Map<TPuestoTrabajoFuncion, PuestoTrabajoFuncionBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(PuestoTrabajoFuncionBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TPuestoTrabajoFuncion entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<PuestoTrabajoFuncionBO> listadoBO)
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

        public bool Update(PuestoTrabajoFuncionBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TPuestoTrabajoFuncion entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<PuestoTrabajoFuncionBO> listadoBO)
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
        private void AsignacionId(TPuestoTrabajoFuncion entidad, PuestoTrabajoFuncionBO objetoBO)
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

        private TPuestoTrabajoFuncion MapeoEntidad(PuestoTrabajoFuncionBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TPuestoTrabajoFuncion entidad = new TPuestoTrabajoFuncion();
                entidad = Mapper.Map<PuestoTrabajoFuncionBO, TPuestoTrabajoFuncion>(objetoBO,
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

        ///Repositorio: PuestoTrabajoFuncionRepositorio
        ///Autor: Edgar S.
        ///Fecha: 25/01/2021
        /// <summary>
        /// Obtiene lista de funciones de un determinado puesto de trabajo
        /// </summary>
        /// <returns> Retorna lista de Funciones por Perfil de Puesto Trabajo </returns>
        /// <returns> Lista de objetos DTO: List<PuestoTrabajoFuncionDTO> </returns>
        public List<PuestoTrabajoFuncionDTO> ObtenerPuestoTrabajoFuncion(int idPerfilPuestoTrabajo)
		{
			try
			{
				List<PuestoTrabajoFuncionDTO> lista = new List<PuestoTrabajoFuncionDTO>();
				var query = "SELECT Id, IdPerfilPuestoTrabajo, NroOrden, Funcion, IdPersonalTipoFuncion, PersonalTipoFuncion, IdFrecuenciaPuestoTrabajo, FrecuenciaPuestoTrabajo FROM [gp].[V_TPuestoTrabajoFuncion_ObtenerPuestoTrabajoFuncion] WHERE Estado = 1  AND IdPerfilPuestoTrabajo = @IdPerfilPuestoTrabajo";
				var res = _dapper.QueryDapper(query, new { IdPerfilPuestoTrabajo = idPerfilPuestoTrabajo });
				if (!res.Contains("[]") && !string.IsNullOrEmpty(res))
				{
					lista = JsonConvert.DeserializeObject<List<PuestoTrabajoFuncionDTO>>(res);
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
