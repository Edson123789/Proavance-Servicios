using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    public class AmbienteRepositorio : BaseRepository<TAmbiente, AmbienteBO>
    {
        #region Metodos Base
        public AmbienteRepositorio() : base()
        {
        }
        public AmbienteRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<AmbienteBO> GetBy(Expression<Func<TAmbiente, bool>> filter)
        {
            IEnumerable<TAmbiente> listado = base.GetBy(filter);
            List<AmbienteBO> listadoBO = new List<AmbienteBO>();
            foreach (var itemEntidad in listado)
            {
                AmbienteBO objetoBO = Mapper.Map<TAmbiente, AmbienteBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public AmbienteBO FirstById(int id)
        {
            try
            {
                TAmbiente entidad = base.FirstById(id);
                AmbienteBO objetoBO = new AmbienteBO();
                Mapper.Map<TAmbiente, AmbienteBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public AmbienteBO FirstBy(Expression<Func<TAmbiente, bool>> filter)
        {
            try
            {
                TAmbiente entidad = base.FirstBy(filter);
                AmbienteBO objetoBO = Mapper.Map<TAmbiente, AmbienteBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(AmbienteBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TAmbiente entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<AmbienteBO> listadoBO)
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

        public bool Update(AmbienteBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TAmbiente entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<AmbienteBO> listadoBO)
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
        private void AsignacionId(TAmbiente entidad, AmbienteBO objetoBO)
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

        private TAmbiente MapeoEntidad(AmbienteBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TAmbiente entidad = new TAmbiente();
                entidad = Mapper.Map<AmbienteBO, TAmbiente>(objetoBO,
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
        /// Obtener Lista de ambientes para filtro
        /// </summary>
        /// <returns></returns>
        public List<AmbienteParaFiltroDTO> ObtenerAmbientes()
        {
            try
            {
                string _queryAmbiente = "SELECT Id,Nombre,IdLocacion FROM pla.V_TAmbiente_ParaFiltro WHERE Estado=1";
                var Ambiente = _dapper.QueryDapper(_queryAmbiente, null);
                return JsonConvert.DeserializeObject<List<AmbienteParaFiltroDTO>>(Ambiente);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// Obtiene id y nombre de Ambiente Por IdAmbiente
        /// </summary>
        /// <param name="IdAmbiente"></param>
        /// <returns></returns>
        public List<FiltroDTO> ObtenerAmbientePorId(int IdAmbiente)
        {
            try
            {
                string _queryAmbiente = "SELECT Id,Nombre FROM pla.V_TAmbientePorId WHERE Estado=1";
                var queryAmbiente = _dapper.QueryDapper(_queryAmbiente, null);
                return JsonConvert.DeserializeObject<List<FiltroDTO>>(queryAmbiente);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            
        }
        /// <summary>
        /// Obtiene un Lista de Ambientes
        /// </summary>
        /// <returns></returns>
        public List<AmbienteFiltroDTO> ObtenerAmbiente()
        {
            try
            {
                string _queryAmbiente = "SELECT Id,Nombre,IdLocacion FROM pla.V_TAmbiente_ParaFiltro WHERE Estado=1";
                var queryAmbiente = _dapper.QueryDapper(_queryAmbiente, null);
                return JsonConvert.DeserializeObject<List<AmbienteFiltroDTO>>(queryAmbiente);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            
        }
        /// <summary>
        /// Obtiene campo Virtual de Ambiente Por id
        /// </summary>
        /// <param name="idAmbiente"></param>
        /// <returns></returns>
        public DatosAmbienteDTO ObtenerVirtualDeAmbiente (int? idAmbiente)
        {
            try
            {
                string _queryAmbiente = "Select Virtual From pla.V_TAmbientePorId Where Estado=1 and Id=@IdAmbiente";
                var queryAmbiente = _dapper.FirstOrDefault(_queryAmbiente, new { IdAmbiente = idAmbiente });
                return JsonConvert.DeserializeObject<DatosAmbienteDTO>(queryAmbiente);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        /// <summary>
        /// Obtiene todos los registros 
        /// </summary>
        /// <returns></returns>
        public List<AmbienteDatosFiltroDTO> ObtenerTodoGrid()
        {
            try
            {
                var listaAmbiente = GetBy(x => true, y => new AmbienteDatosFiltroDTO
                {
                    Id = y.Id,
                    Nombre = y.Nombre,
                    IdLocacion = y.IdLocacion,
                    IdTipoAmbiente = y.IdTipoAmbiente,
                    Capacidad = y.Capacidad,
                    Virtual = y.Virtual,
                }).OrderByDescending(x => x.Id).ToList();

                return listaAmbiente;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

		public List<AmbienteCiudadFiltroDTO> ObtenerAmbientesCiudad()
		{
			try
			{
				var query = "SELECT Id, Nombre, IdLocacion, IdCiudad FROM pla.V_AmbienteCiudad_ParaFiltro WHERE Estado = 1";
				var res = _dapper.QueryDapper(query,null);
				var ambientes = JsonConvert.DeserializeObject<List<AmbienteCiudadFiltroDTO>>(res);
				return ambientes;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
    }
}
