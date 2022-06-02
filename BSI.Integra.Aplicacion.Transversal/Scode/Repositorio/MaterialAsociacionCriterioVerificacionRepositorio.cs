using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    public class MaterialAsociacionCriterioVerificacionRepositorio : BaseRepository<TMaterialAsociacionCriterioVerificacion, MaterialAsociacionCriterioVerificacionBO>
    {
        #region Metodos Base
        public MaterialAsociacionCriterioVerificacionRepositorio() : base()
        {
        }
        public MaterialAsociacionCriterioVerificacionRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<MaterialAsociacionCriterioVerificacionBO> GetBy(Expression<Func<TMaterialAsociacionCriterioVerificacion, bool>> filter)
        {
            IEnumerable<TMaterialAsociacionCriterioVerificacion> listado = base.GetBy(filter);
            List<MaterialAsociacionCriterioVerificacionBO> listadoBO = new List<MaterialAsociacionCriterioVerificacionBO>();
            foreach (var itemEntidad in listado)
            {
                MaterialAsociacionCriterioVerificacionBO objetoBO = Mapper.Map<TMaterialAsociacionCriterioVerificacion, MaterialAsociacionCriterioVerificacionBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public MaterialAsociacionCriterioVerificacionBO FirstById(int id)
        {
            try
            {
                TMaterialAsociacionCriterioVerificacion entidad = base.FirstById(id);
                MaterialAsociacionCriterioVerificacionBO objetoBO = new MaterialAsociacionCriterioVerificacionBO();
                Mapper.Map<TMaterialAsociacionCriterioVerificacion, MaterialAsociacionCriterioVerificacionBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public MaterialAsociacionCriterioVerificacionBO FirstBy(Expression<Func<TMaterialAsociacionCriterioVerificacion, bool>> filter)
        {
            try
            {
                TMaterialAsociacionCriterioVerificacion entidad = base.FirstBy(filter);
                MaterialAsociacionCriterioVerificacionBO objetoBO = Mapper.Map<TMaterialAsociacionCriterioVerificacion, MaterialAsociacionCriterioVerificacionBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(MaterialAsociacionCriterioVerificacionBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TMaterialAsociacionCriterioVerificacion entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<MaterialAsociacionCriterioVerificacionBO> listadoBO)
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

        public bool Update(MaterialAsociacionCriterioVerificacionBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TMaterialAsociacionCriterioVerificacion entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<MaterialAsociacionCriterioVerificacionBO> listadoBO)
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
        private void AsignacionId(TMaterialAsociacionCriterioVerificacion entidad, MaterialAsociacionCriterioVerificacionBO objetoBO)
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

        private TMaterialAsociacionCriterioVerificacion MapeoEntidad(MaterialAsociacionCriterioVerificacionBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TMaterialAsociacionCriterioVerificacion entidad = new TMaterialAsociacionCriterioVerificacion();
                entidad = Mapper.Map<MaterialAsociacionCriterioVerificacionBO, TMaterialAsociacionCriterioVerificacion>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos

                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<MaterialAsociacionCriterioVerificacionBO> GetFiltered<KProperty>(IEnumerable<Expression<Func<TMaterialAsociacionCriterioVerificacion, bool>>> filters, Expression<Func<TMaterialAsociacionCriterioVerificacion, KProperty>> orderBy, bool ascending)
        {
            IEnumerable<TMaterialAsociacionCriterioVerificacion> listado = base.GetFiltered(filters, orderBy, ascending);
            List<MaterialAsociacionCriterioVerificacionBO> listadoBO = new List<MaterialAsociacionCriterioVerificacionBO>();

            foreach (var itemEntidad in listado)
            {
                MaterialAsociacionCriterioVerificacionBO objetoBO = Mapper.Map<TMaterialAsociacionCriterioVerificacion, MaterialAsociacionCriterioVerificacionBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }
            return listadoBO;
        }
        #endregion

        /// <summary>
        /// Elimina de forma fisica los registros asociados
        /// </summary>
        /// <param name="idMaterialTipo"></param>
        /// <param name="nombreUsuario"></param>
        /// <param name="nuevos"></param>
        public void EliminacionLogicoPorMaterialTipo(int idMaterialTipo, string nombreUsuario, List<int> nuevos)
        {
            try
            {
                var listaBorrar = GetBy(x => x.IdMaterialTipo == idMaterialTipo && x.Estado == true).ToList();
                listaBorrar.RemoveAll(x => nuevos.Any(y => y == x.IdMaterialCriterioVerificacion));
                foreach (var item in listaBorrar)
                {
                    Delete(item.Id, nombreUsuario);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

		/// <summary>
		/// Obtiene lista de criterios de verificacion con idmaterialtipo
		/// </summary>
		/// <returns></returns>
		public List<CriterioVerificacionDTO> ObtenerCriteriosDeVerificacion()
		{
			try
			{
				List<CriterioVerificacionDTO> lista = new List<CriterioVerificacionDTO>();
				var query = "SELECT IdMaterialCriterioVerificacion, MaterialCriterioVerificacion, IdMaterialTipo FROM ope.V_TMaterialCriterioVerificacion_ObtenerListaCriterios WHERE Estado = 1";
				var resultadoDB = _dapper.QueryDapper(query, null);
				if (!string.IsNullOrEmpty(resultadoDB) && !resultadoDB.Contains("[]"))
				{
					lista = JsonConvert.DeserializeObject<List<CriterioVerificacionDTO>> (resultadoDB);
				}
				return lista;
			}
			catch (Exception e)
			{
				throw e;
			}
		}

		/// <summary>
		/// Obtiene lista de criterios de verificacion asociados a un material pespecifico detalle
		/// </summary>
		/// <param name="idMaterialPEspecificoDetalle"></param>
		/// <returns></returns>
		public List<MaterialDetalleCriterioVerificacionDTO> ObtenerCriteriosVerificacionPorMaterialDetalle(int idMaterialPEspecificoDetalle)
		{
			try
			{
				List<MaterialDetalleCriterioVerificacionDTO> lista = new List<MaterialDetalleCriterioVerificacionDTO>();
				var query = "SELECT Id, IdMaterialPEspecificoDetalle, IdMaterialCriterioVerificacion, MaterialCriterioVerificacion, EsAprobado FROM ope.V_TMaterialCriterioVerificacionDetalle_ObtenerCriterios WHERE IdMaterialPEspecificoDetalle = @IdMaterialPEspecificoDetalle AND Estado = 1";
				var resultadoDB = _dapper.QueryDapper(query, new { IdMaterialPEspecificoDetalle  = idMaterialPEspecificoDetalle });
				if (!string.IsNullOrEmpty(resultadoDB) && !resultadoDB.Contains("[]"))
				{
					lista = JsonConvert.DeserializeObject<List<MaterialDetalleCriterioVerificacionDTO>>(resultadoDB);
				}
				return lista;
			}
			catch (Exception e)
			{
				throw e;
			}
		}

		//public List<AlumnoMatriculaPEspecificoDTO> ObtenerAlumnosPorProgramaEspecificoGrupo(int idPEspecifico, int grupo)
		//{
		//	try
		//	{
		//		var lista = new List<AlumnoMatriculaPEspecificoDTO>();
		//		var query = $@"
  //                   SELECT IdAlumno, 
		//				   Alumno, 
		//				   IdPEspecifico, 
		//				   Grupo
		//			FROM [ope].[V_TPEspecificoMatriculaAlumno_ObtenerAlumnosPEspecificoMatriculados]
		//			WHERE Estado = 1
		//				  AND IdPEspecifico = @IdPEspecifico
		//				  AND Grupo = @Grupo
  //              ";
		//		var resultadoDB = _dapper.QueryDapper(query, new { IdPEspecifico = idPEspecifico, Grupo = grupo });
		//		if (!string.IsNullOrEmpty(resultadoDB) && !resultadoDB.Contains("[]"))
		//		{
		//			lista = JsonConvert.DeserializeObject<List<AlumnoMatriculaPEspecificoDTO>>(resultadoDB);
		//		}
		//		return lista;
		//	}
		//	catch (Exception e)
		//	{
		//		throw new Exception(e.Message);
		//	}
		//}
	}
}
