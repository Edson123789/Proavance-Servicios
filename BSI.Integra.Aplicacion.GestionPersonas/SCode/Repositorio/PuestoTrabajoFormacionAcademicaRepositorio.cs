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
    /// PuestoTrabajoFormacionAcademicaRepositorio
    /// Autor: Luis H., Edgar S.
    /// Fecha: 29/01/2021
    /// <summary>
    /// Gestión de Experiencia Requerida de Perfil de Puestos de Trabajo
    /// </summary>
    public class PuestoTrabajoFormacionAcademicaRepositorio : BaseRepository<TPuestoTrabajoFormacionAcademica, PuestoTrabajoFormacionAcademicaBO>
    {
        #region Metodos Base
        public PuestoTrabajoFormacionAcademicaRepositorio() : base()
        {
        }
        public PuestoTrabajoFormacionAcademicaRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<PuestoTrabajoFormacionAcademicaBO> GetBy(Expression<Func<TPuestoTrabajoFormacionAcademica, bool>> filter)
        {
            IEnumerable<TPuestoTrabajoFormacionAcademica> listado = base.GetBy(filter);
            List<PuestoTrabajoFormacionAcademicaBO> listadoBO = new List<PuestoTrabajoFormacionAcademicaBO>();
            foreach (var itemEntidad in listado)
            {
                PuestoTrabajoFormacionAcademicaBO objetoBO = Mapper.Map<TPuestoTrabajoFormacionAcademica, PuestoTrabajoFormacionAcademicaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public PuestoTrabajoFormacionAcademicaBO FirstById(int id)
        {
            try
            {
                TPuestoTrabajoFormacionAcademica entidad = base.FirstById(id);
                PuestoTrabajoFormacionAcademicaBO objetoBO = new PuestoTrabajoFormacionAcademicaBO();
                Mapper.Map<TPuestoTrabajoFormacionAcademica, PuestoTrabajoFormacionAcademicaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public PuestoTrabajoFormacionAcademicaBO FirstBy(Expression<Func<TPuestoTrabajoFormacionAcademica, bool>> filter)
        {
            try
            {
                TPuestoTrabajoFormacionAcademica entidad = base.FirstBy(filter);
                PuestoTrabajoFormacionAcademicaBO objetoBO = Mapper.Map<TPuestoTrabajoFormacionAcademica, PuestoTrabajoFormacionAcademicaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(PuestoTrabajoFormacionAcademicaBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TPuestoTrabajoFormacionAcademica entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<PuestoTrabajoFormacionAcademicaBO> listadoBO)
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

        public bool Update(PuestoTrabajoFormacionAcademicaBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TPuestoTrabajoFormacionAcademica entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<PuestoTrabajoFormacionAcademicaBO> listadoBO)
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
        private void AsignacionId(TPuestoTrabajoFormacionAcademica entidad, PuestoTrabajoFormacionAcademicaBO objetoBO)
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

        private TPuestoTrabajoFormacionAcademica MapeoEntidad(PuestoTrabajoFormacionAcademicaBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TPuestoTrabajoFormacionAcademica entidad = new TPuestoTrabajoFormacionAcademica();
                entidad = Mapper.Map<PuestoTrabajoFormacionAcademicaBO, TPuestoTrabajoFormacionAcademica>(objetoBO,
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

        /// PuestoTrabajoFormacionAcademicaRepositorio
        /// Autor: Luis H., Edgar S.
        /// Fecha: 29/01/2021
        /// <summary>
        /// Obtiene lista de formacion academica de un determinado puesto de trabajo
        /// </summary>
        /// <returns> Información  de  Formación Académica de personal por Perfil de puesto de trabajo </returns>
        /// <returns> Lista de Objeto DTO : List<PuestoTrabajoFormacionAcademicaFiltroDTO> </returns>
        public List<PuestoTrabajoFormacionAcademicaFiltroDTO> ObtenerPuestoTrabajoFormacionAcademica(int idPerfilPuestoTrabajo)
		{
			try
			{
				List<PuestoTrabajoFormacionAcademicaFiltroDTO> lista = new List<PuestoTrabajoFormacionAcademicaFiltroDTO>();
				var _query = "SELECT Id, IdPerfilPuestoTrabajo, IdTipoFormacion, IdNivelEstudio, IdAreaFormacion, IdCentroEstudio, IdGradoEstudio FROM [gp].[V_TPuestoTrabajoFormacionAcademica_ObtenerListaFormacionAcademica] WHERE Estado = 1 AND IdPerfilPuestoTrabajo = @IdPerfilPuestoTrabajo";
				var res = _dapper.QueryDapper(_query, new { IdPerfilPuestoTrabajo = idPerfilPuestoTrabajo });
				if (!res.Contains("[]") && !string.IsNullOrEmpty(res))
				{
					lista = JsonConvert.DeserializeObject<List<PuestoTrabajoFormacionAcademicaFiltroDTO>>(res);
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
