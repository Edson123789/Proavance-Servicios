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
    /// PuestoTrabajoCursoComplementarioRepositorio
    /// Autor: Luis H., Edgar S.
    /// Fecha: 29/01/2021
    /// <summary>
    /// Gestión de Cursos Complementarios de Perfil de Puestos de Trabajo
    /// </summary>
    public class PuestoTrabajoCursoComplementarioRepositorio : BaseRepository<TPuestoTrabajoCursoComplementario, PuestoTrabajoCursoComplementarioBO>
    {
        #region Metodos Base
        public PuestoTrabajoCursoComplementarioRepositorio() : base()
        {
        }
        public PuestoTrabajoCursoComplementarioRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<PuestoTrabajoCursoComplementarioBO> GetBy(Expression<Func<TPuestoTrabajoCursoComplementario, bool>> filter)
        {
            IEnumerable<TPuestoTrabajoCursoComplementario> listado = base.GetBy(filter);
            List<PuestoTrabajoCursoComplementarioBO> listadoBO = new List<PuestoTrabajoCursoComplementarioBO>();
            foreach (var itemEntidad in listado)
            {
                PuestoTrabajoCursoComplementarioBO objetoBO = Mapper.Map<TPuestoTrabajoCursoComplementario, PuestoTrabajoCursoComplementarioBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public PuestoTrabajoCursoComplementarioBO FirstById(int id)
        {
            try
            {
                TPuestoTrabajoCursoComplementario entidad = base.FirstById(id);
                PuestoTrabajoCursoComplementarioBO objetoBO = new PuestoTrabajoCursoComplementarioBO();
                Mapper.Map<TPuestoTrabajoCursoComplementario, PuestoTrabajoCursoComplementarioBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public PuestoTrabajoCursoComplementarioBO FirstBy(Expression<Func<TPuestoTrabajoCursoComplementario, bool>> filter)
        {
            try
            {
                TPuestoTrabajoCursoComplementario entidad = base.FirstBy(filter);
                PuestoTrabajoCursoComplementarioBO objetoBO = Mapper.Map<TPuestoTrabajoCursoComplementario, PuestoTrabajoCursoComplementarioBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(PuestoTrabajoCursoComplementarioBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TPuestoTrabajoCursoComplementario entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<PuestoTrabajoCursoComplementarioBO> listadoBO)
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

        public bool Update(PuestoTrabajoCursoComplementarioBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TPuestoTrabajoCursoComplementario entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<PuestoTrabajoCursoComplementarioBO> listadoBO)
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
        private void AsignacionId(TPuestoTrabajoCursoComplementario entidad, PuestoTrabajoCursoComplementarioBO objetoBO)
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

        private TPuestoTrabajoCursoComplementario MapeoEntidad(PuestoTrabajoCursoComplementarioBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TPuestoTrabajoCursoComplementario entidad = new TPuestoTrabajoCursoComplementario();
                entidad = Mapper.Map<PuestoTrabajoCursoComplementarioBO, TPuestoTrabajoCursoComplementario>(objetoBO,
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

        /// PuestoTrabajoCursoComplementarioRepositorio
        /// Autor: Luis H., Edgar S.
        /// Fecha: 29/01/2021
        /// <summary>
        /// Obtiene lista de curso complementario de un determinado puesto de trabajo
        /// </summary>
        /// <returns> Lista de Objeto DTO: List<PuestoTrabajoCursoComplementarioDTO </returns>
        public List<PuestoTrabajoCursoComplementarioDTO> ObtenerPuestoTrabajoCursoComplementario(int idPerfilPuestoTrabajo)
		{
			try
			{
				List<PuestoTrabajoCursoComplementarioDTO> lista = new List<PuestoTrabajoCursoComplementarioDTO>();
				var _query = "SELECT Id, IdPerfilPuestoTrabajo, IdTipoCompetenciaTecnica, IdCompetenciaTecnica, IdNivelCompetenciaTecnica, TipoCompetenciaTecnica, CompetenciaTecnica, NivelCompetenciaTecnica FROM [gp].[V_TPuestoTrabajoCursoComplementario_ObtenerListaCursosComplementarios] WHERE Estado = 1 AND IdPerfilPuestoTrabajo = @IdPerfilPuestoTrabajo";
				var res = _dapper.QueryDapper(_query, new { IdPerfilPuestoTrabajo = idPerfilPuestoTrabajo });
				if (!res.Contains("[]") && !string.IsNullOrEmpty(res))
				{
					lista = JsonConvert.DeserializeObject<List<PuestoTrabajoCursoComplementarioDTO>>(res);
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
