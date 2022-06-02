using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.GestionPersonas.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace BSI.Integra.Aplicacion.GestionPersonas.Repositorio
{
    /// Repositorio: CentilRepositorio
    /// Autor: Britsel C., Luis H., Edgar S.
    /// Fecha: 29/01/2021
    /// <summary>
    /// Gestión de Centiles
    /// </summary>
    public class CentilRepositorio : BaseRepository<TCentil, CentilBO>
    {
        #region Metodos Base
        public CentilRepositorio() : base()
        {
        }
        public CentilRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<CentilBO> GetBy(Expression<Func<TCentil, bool>> filter)
        {
            IEnumerable<TCentil> listado = base.GetBy(filter);
            List<CentilBO> listadoBO = new List<CentilBO>();
            foreach (var itemEntidad in listado)
            {
                CentilBO objetoBO = Mapper.Map<TCentil, CentilBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public CentilBO FirstById(int id)
        {
            try
            {
                TCentil entidad = base.FirstById(id);
                CentilBO objetoBO = new CentilBO();
                Mapper.Map<TCentil, CentilBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public CentilBO FirstBy(Expression<Func<TCentil, bool>> filter)
        {
            try
            {
                TCentil entidad = base.FirstBy(filter);
                CentilBO objetoBO = Mapper.Map<TCentil, CentilBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(CentilBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TCentil entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<CentilBO> listadoBO)
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

        public bool Update(CentilBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TCentil entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<CentilBO> listadoBO)
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
        private void AsignacionId(TCentil entidad, CentilBO objetoBO)
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

        private TCentil MapeoEntidad(CentilBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TCentil entidad = new TCentil();
                entidad = Mapper.Map<CentilBO, TCentil>(objetoBO,
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
		/// Obtiene centiles asignados a un componente de evaluacion
		/// </summary>
		/// <param name="idExamen"></param>
		/// <returns></returns>
		public List<CentilDTO> ObtenerCentilesAsignados(int idExamen)
		{
			try
			{
				List<CentilDTO> Centiles = new List<CentilDTO>();
				var campos = "Id, IdExamen,IdExamenTest,IdGrupoComponenteEvaluacion,IdSexo,ValorMinimo,ValorMaximo,Centil,CentilLetra,UsuarioModificacion";

				var _query = "SELECT " + campos + " FROM  gp.V_ObtenerCentiles WHERE Estado=1 AND IdExamen=@IdExamen";
				var listaCentilDB = _dapper.QueryDapper(_query, new { IdExamen = idExamen });
				if (!listaCentilDB.Contains("[]") && !string.IsNullOrEmpty(listaCentilDB))
				{
					Centiles = JsonConvert.DeserializeObject<List<CentilDTO>>(listaCentilDB);
				}
				return Centiles;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		/// <summary>
		/// Obtiene centiles asignados a una evaluacion
		/// </summary>
		/// <param name="idExamenTest"></param>
		/// <returns></returns>
		public List<CentilDTO> ObtenerCentilesEvaluacion(int idExamenTest)
		{
			try
			{
				List<CentilDTO> Centiles = new List<CentilDTO>();
				var campos = "Id,IdExamen,IdExamenTest,IdGrupoComponenteEvaluacion,IdSexo,ValorMinimo,ValorMaximo,Centil,CentilLetra,UsuarioModificacion";

				var _query = "SELECT " + campos + " FROM  gp.V_ObtenerCentiles WHERE Estado=1 AND IdExamenTest=@idExamenTest";
				var listaCentilDB = _dapper.QueryDapper(_query, new { IdExamenTest = idExamenTest });
				if (!listaCentilDB.Contains("[]") && !string.IsNullOrEmpty(listaCentilDB))
				{
					Centiles = JsonConvert.DeserializeObject<List<CentilDTO>>(listaCentilDB);
				}
				return Centiles;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		/// <summary>
		/// Obtiene centiles asignados a un grupo de evaluacion
		/// </summary>
		/// <param name="idExamenTest"></param>
		/// <returns></returns>
		public List<CentilDTO> ObtenerCentilesGrupoEvaluacion(int idGrupoComponenteEvaluacion)
		{
			try
			{
				List<CentilDTO> Centiles = new List<CentilDTO>();
				var campos = "Id, IdExamen,IdExamenTest,IdGrupoComponenteEvaluacion,IdSexo,ValorMinimo,ValorMaximo,Centil,CentilLetra,UsuarioModificacion";

				var _query = "SELECT " + campos + " FROM  gp.V_ObtenerCentiles WHERE Estado=1 AND IdGrupoComponenteEvaluacion = @IdGrupoComponenteEvaluacion";
				var listaCentilDB = _dapper.QueryDapper(_query, new { IdGrupoComponenteEvaluacion = idGrupoComponenteEvaluacion });
				if (!listaCentilDB.Contains("[]") && !string.IsNullOrEmpty(listaCentilDB))
				{
					Centiles = JsonConvert.DeserializeObject<List<CentilDTO>>(listaCentilDB);
				}
				return Centiles;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
	}
}
