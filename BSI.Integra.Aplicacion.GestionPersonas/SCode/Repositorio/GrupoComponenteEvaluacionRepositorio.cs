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
    /// Repositorio: GrupoComponenteEvaluacionRepositorio
    /// Autor: Britsel C., Luis H., Edgar S.
    /// Fecha: 29/01/2021
    /// <summary>
    /// Gestión de Componentes de Evalución
    /// </summary>
    public class GrupoComponenteEvaluacionRepositorio : BaseRepository<TGrupoComponenteEvaluacion, GrupoComponenteEvaluacionBO>
    {
        #region Metodos Base
        public GrupoComponenteEvaluacionRepositorio() : base()
        {
        }
        public GrupoComponenteEvaluacionRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<GrupoComponenteEvaluacionBO> GetBy(Expression<Func<TGrupoComponenteEvaluacion, bool>> filter)
        {
            IEnumerable<TGrupoComponenteEvaluacion> listado = base.GetBy(filter);
            List<GrupoComponenteEvaluacionBO> listadoBO = new List<GrupoComponenteEvaluacionBO>();
            foreach (var itemEntidad in listado)
            {
                GrupoComponenteEvaluacionBO objetoBO = Mapper.Map<TGrupoComponenteEvaluacion, GrupoComponenteEvaluacionBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public GrupoComponenteEvaluacionBO FirstById(int id)
        {
            try
            {
                TGrupoComponenteEvaluacion entidad = base.FirstById(id);
                GrupoComponenteEvaluacionBO objetoBO = new GrupoComponenteEvaluacionBO();
                Mapper.Map<TGrupoComponenteEvaluacion, GrupoComponenteEvaluacionBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public GrupoComponenteEvaluacionBO FirstBy(Expression<Func<TGrupoComponenteEvaluacion, bool>> filter)
        {
            try
            {
                TGrupoComponenteEvaluacion entidad = base.FirstBy(filter);
                GrupoComponenteEvaluacionBO objetoBO = Mapper.Map<TGrupoComponenteEvaluacion, GrupoComponenteEvaluacionBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(GrupoComponenteEvaluacionBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TGrupoComponenteEvaluacion entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<GrupoComponenteEvaluacionBO> listadoBO)
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

        public bool Update(GrupoComponenteEvaluacionBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TGrupoComponenteEvaluacion entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<GrupoComponenteEvaluacionBO> listadoBO)
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
        private void AsignacionId(TGrupoComponenteEvaluacion entidad, GrupoComponenteEvaluacionBO objetoBO)
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

        private TGrupoComponenteEvaluacion MapeoEntidad(GrupoComponenteEvaluacionBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TGrupoComponenteEvaluacion entidad = new TGrupoComponenteEvaluacion();
                entidad = Mapper.Map<GrupoComponenteEvaluacionBO, TGrupoComponenteEvaluacion>(objetoBO,
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

        public List<GrupoEvaluacionDTO> ObtenerGrupoEvaluacion(int IdEvaluacion)
        {
            try
            {
                List<GrupoEvaluacionDTO> EvaluacionGrupo = new List<GrupoEvaluacionDTO>();
                var campos = "Id,Nombre,IdEvaluacion,IdExamen,NombreExamen,IdFormula,Factor,RequiereCentil ";

                var _query = "SELECT " + campos + " FROM  gp.V_ObtenerGrupoComponenteEvaluacion where IdEvaluacion=" + IdEvaluacion ;
                var dataDB = _dapper.QueryDapper(_query, null);
                if (!dataDB.Contains("[]") && !string.IsNullOrEmpty(dataDB))
                {
                    EvaluacionGrupo = JsonConvert.DeserializeObject<List<GrupoEvaluacionDTO>>(dataDB);
                }
                return EvaluacionGrupo;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

		public List<GrupoComponenteDTO> ObtenerGrupoEvaluacionDesglosadoPorComponente(int IdEvaluacion)
		{
			try
			{
				List<GrupoComponenteDTO> EvaluacionGrupo = new List<GrupoComponenteDTO>();
				var campos = "Id,Nombre,IdEvaluacion,IdExamen,NombreExamen,FactorComponente,RequiereCentil ";

				var _query = "SELECT " + campos + " FROM  gp.V_ObtenerGrupoComponenteEvaluacion where IdEvaluacion=" + IdEvaluacion;
				var dataDB = _dapper.QueryDapper(_query, null);
				if (!dataDB.Contains("[]") && !string.IsNullOrEmpty(dataDB))
				{
					EvaluacionGrupo = JsonConvert.DeserializeObject<List<GrupoComponenteDTO>>(dataDB);
				}
				return EvaluacionGrupo;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
	}
}
