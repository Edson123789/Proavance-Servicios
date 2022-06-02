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
    /// Repositorio: Transversal/MaterialPEspecifico
    /// Autor: Wilber Choque - Luis Huallpa - Gian Miranda
    /// Fecha: 11/07/2021
    /// <summary>
    /// Repositorio para operaciones con la tabla ope.T_MaterialPEspecifico
    /// </summary>
    public class MaterialPespecificoRepositorio : BaseRepository<TMaterialPespecifico, MaterialPespecificoBO>
    {
        #region Metodos Base
        public MaterialPespecificoRepositorio() : base()
        {
        }
        public MaterialPespecificoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<MaterialPespecificoBO> GetBy(Expression<Func<TMaterialPespecifico, bool>> filter)
        {
            IEnumerable<TMaterialPespecifico> listado = base.GetBy(filter);
            List<MaterialPespecificoBO> listadoBO = new List<MaterialPespecificoBO>();
            foreach (var itemEntidad in listado)
            {
                MaterialPespecificoBO objetoBO = Mapper.Map<TMaterialPespecifico, MaterialPespecificoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public MaterialPespecificoBO FirstById(int id)
        {
            try
            {
                TMaterialPespecifico entidad = base.FirstById(id);
                MaterialPespecificoBO objetoBO = new MaterialPespecificoBO();
                Mapper.Map<TMaterialPespecifico, MaterialPespecificoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public MaterialPespecificoBO FirstBy(Expression<Func<TMaterialPespecifico, bool>> filter)
        {
            try
            {
                TMaterialPespecifico entidad = base.FirstBy(filter);
                MaterialPespecificoBO objetoBO = Mapper.Map<TMaterialPespecifico, MaterialPespecificoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(MaterialPespecificoBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TMaterialPespecifico entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<MaterialPespecificoBO> listadoBO)
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

        public bool Update(MaterialPespecificoBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TMaterialPespecifico entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<MaterialPespecificoBO> listadoBO)
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
        private void AsignacionId(TMaterialPespecifico entidad, MaterialPespecificoBO objetoBO)
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

        private TMaterialPespecifico MapeoEntidad(MaterialPespecificoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TMaterialPespecifico entidad = new TMaterialPespecifico();
                entidad = Mapper.Map<MaterialPespecificoBO, TMaterialPespecifico>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos
                if (objetoBO.ListaMaterialPespecificoDetalle != null && objetoBO.ListaMaterialPespecificoDetalle.Count > 0)
                {
                    foreach (var hijo in objetoBO.ListaMaterialPespecificoDetalle)
                    {
                        TMaterialPespecificoDetalle entidadHijo = new TMaterialPespecificoDetalle();
                        entidadHijo = Mapper.Map<MaterialPespecificoDetalleBO, TMaterialPespecificoDetalle>(hijo,
                            opt => opt.ConfigureMap(MemberList.None));
                        entidad.TMaterialPespecificoDetalle.Add(entidadHijo);
                    }
                }
                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        #endregion

        /// <summary>
        /// Obtiene los materiales por sesion
        /// </summary>
        /// <param name="idPEspecifico"></param>
        /// <returns></returns>
        public List<MaterialPespecificoBO> ObtenerPorPEspecifico(int idPEspecifico)
        {
            try
            {
                return this.GetBy(x => x.Estado && x.IdPespecifico == idPEspecifico).OrderBy(x => x.GrupoEdicion).ThenBy(x => x.GrupoEdicionOrden).ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Obtiene los furs asociados aun programa especifico
        /// </summary>
        /// <param name="idPEspecifico"></param>
        /// <returns></returns>
        public List<PEspecificoFurDetalleDTO> ObtenerFursAsociadosPorPEspecifico(int idPEspecifico)
        {
            try
            {
                var _resultado = new List<PEspecificoFurDetalleDTO>();
                var query = $@"ope.SP_ObtenerFursAsociadosProgramaEspecifico";
                var resultado = _dapper.QuerySPDapper(query, new { IdPEspecifico = idPEspecifico });

                if (!string.IsNullOrEmpty(resultado))
                {
                    _resultado = JsonConvert.DeserializeObject<List<PEspecificoFurDetalleDTO>>(resultado);
                }
                return _resultado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene el fur asociado
        /// </summary>
        /// <param name="idMaterialPEspecificoDetalle"></param>
        /// <returns></returns>
        public AsociarActualizarFurMaterialVersionDTO ObtenerFurAsociadoPorMaterialPEspecificoDetalle(int idMaterialPEspecificoDetalle)
        {
            try
            {
                var _resultado = new AsociarActualizarFurMaterialVersionDTO();
                var query = $@"ope.SP_ObtenerFurAsociadoMaterialProgramaEspecifico";
                var resultado = _dapper.QuerySPFirstOrDefault(query, new { IdMaterialPEspecificoDetalle = idMaterialPEspecificoDetalle });

                if (!string.IsNullOrEmpty(resultado))
                {
                    _resultado = JsonConvert.DeserializeObject<AsociarActualizarFurMaterialVersionDTO>(resultado);
                }
                return _resultado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene el maximo grupo de edicion
        /// </summary>
        /// <param name="idPEspecifico">Id del PEspecifico (PK de la tabla pla.T_PEspecifico)</param>
        /// <returns>Entero</returns>
        public int ObtenerMaximoGrupoEdicion(int idPEspecifico)
        {
            try
            {
                var _resultado = new ValorIntDTO();
                var query = $@"ope.SP_ObtenerMaximoGrupoEdicionMaterialPEspecifico";
                var resultado = _dapper.QuerySPFirstOrDefault(query, new { IdPEspecifico = idPEspecifico });

                if (!string.IsNullOrEmpty(resultado))
                {
                    _resultado = JsonConvert.DeserializeObject<ValorIntDTO>(resultado);
                }
                return _resultado.Valor;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene el proximo grupo de edicion
        /// </summary>
        /// <param name="idPEspecifico">Id del PEspecifico (PK de la tabla pla.T_PEspecifico)</param>
        /// <returns>Entero</returns>
        public int ObtenerProximoGrupoEdicion(int idPEspecifico)
        {
            try
            {
                return this.ObtenerMaximoGrupoEdicion(idPEspecifico) + 1;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Obtiene todos programas especificos hijos
        /// </summary>
        /// <returns></returns>
        public List<MaterialPEspecificoGrupoDetalleDTO> ObtenerPorProgramaEspecificoGrupo(int idPEspecifico, int grupo, List<int> listaIdMaterialEstado)
        {
            try
            {
                var lista = new List<MaterialPEspecificoGrupoDetalleDTO>();
                var query = $@"ope.SP_ObtenerMaterialProgramaEspecificoGrupo";
                var resultadoDB = _dapper.QuerySPDapper(query, new { IdPEspecifico = idPEspecifico, Grupo = grupo, ListaMaterialEstado = string.Join(",", listaIdMaterialEstado.Select(x => x)) });
                if (!string.IsNullOrEmpty(resultadoDB) && !resultadoDB.Contains("[]"))
                {
                    lista = JsonConvert.DeserializeObject<List<MaterialPEspecificoGrupoDetalleDTO>>(resultadoDB);
                }
                return lista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene todos programas especificos hijos
        /// </summary>
        /// <param name="filtro">Objeto de clase FiltroMaterialDTO</param>
        /// <returns>Lista de objetos de clase ResultadoMaterialPEspecificoDetalleDTO</returns>
        public List<ResultadoMaterialPEspecificoDetalleDTO> ObtenerMateriales(FiltroMaterialDTO filtro)
        {
            try
            {
                var lista = new List<ResultadoMaterialPEspecificoDetalleDTO>();
                var query = $@"ope.SP_ObtenerMateriales";
                var filtros = new
                {
                    ListaArea = string.Join(",", filtro.Area.Select(x => x)),
                    ListaSubArea = string.Join(",", filtro.SubArea.Select(x => x)),
                    ListaProgramaGeneral = string.Join(",", filtro.ProgramaGeneral.Select(x => x)),
                    ListaProgramaEspecificoPadreIndividual = string.Join(",", filtro.ProgramaEspecificoPadreIndividual.Select(x => x)),
                    ListaProgramaEspecificoCurso = string.Join(",", filtro.ProgramaEspecificoCurso.Select(x => x)),
                    ListaEstadoProgramaEspecifico = string.Join(",", filtro.EstadoProgramaEspecifico.Select(x => x)),
                    ListaCiudad = string.Join(",", filtro.Ciudad.Select(x => x)),
                    ListaModalidad = string.Join(",", filtro.Modalidad.Select(x => x)),
                    ListaMaterialEstado = string.Join(",", filtro.ListaMaterialEstado.Select(x => x)),
                    ListaGrupo = string.Join(",", filtro.Grupo.Select(x => x)),
                    ListaMaterialVersion = ""
                };
                var resultadoDB = _dapper.QuerySPDapper(query, filtros);
                if (!string.IsNullOrEmpty(resultadoDB) && !resultadoDB.Contains("[]"))
                {
                    lista = JsonConvert.DeserializeObject<List<ResultadoMaterialPEspecificoDetalleDTO>>(resultadoDB);
                }
                return lista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene todos programas especificos hijos
        /// </summary>
        /// <returns></returns>
        public List<ResultadoMaterialPEspecificoDetalleDTO> ObtenerMaterialesGestionEnvio(FiltroMaterialDTO filtro)
        {
            try
            {
                var lista = new List<ResultadoMaterialPEspecificoDetalleDTO>();
                var query = $@"ope.SP_ObtenerMaterialesGestionEnvio";
                var filtros = new
                {
                    ListaArea = string.Join(",", filtro.Area.Select(x => x)),
                    ListaSubArea = string.Join(",", filtro.SubArea.Select(x => x)),
                    ListaProgramaGeneral = string.Join(",", filtro.ProgramaGeneral.Select(x => x)),
                    ListaProgramaEspecificoPadreIndividual = string.Join(",", filtro.ProgramaEspecificoPadreIndividual.Select(x => x)),
                    ListaProgramaEspecificoCurso = string.Join(",", filtro.ProgramaEspecificoCurso.Select(x => x)),
                    ListaEstadoProgramaEspecifico = string.Join(",", filtro.EstadoProgramaEspecifico.Select(x => x)),
                    ListaCiudad = string.Join(",", filtro.Ciudad.Select(x => x)),
                    ListaModalidad = string.Join(",", filtro.Modalidad.Select(x => x)),
                    ListaMaterialEstado = string.Join(",", filtro.ListaMaterialEstado.Select(x => x)),
                    ListaMaterialVersion = ""
                };
                var resultadoDB = _dapper.QuerySPDapper(query, filtros);
                if (!string.IsNullOrEmpty(resultadoDB) && !resultadoDB.Contains("[]"))
                {
                    lista = JsonConvert.DeserializeObject<List<ResultadoMaterialPEspecificoDetalleDTO>>(resultadoDB);
                }
                return lista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene las acciones asociadas a un tipo de material
        /// </summary>
        /// <param name="idMaterialTipo"></param>
        /// <returns></returns>
        public List<FiltroDTO> ObtenerMaterialAccionPorMaterialTipo(int idMaterialTipo)
        {
            try
            {
                var lista = new List<FiltroDTO>();
                var query = $@"ope.SP_ObtenerMaterialAccionPorMaterialTipo";
                var filtros = new
                {
                    IdMaterialTipo = idMaterialTipo
                };
                var resultadoDB = _dapper.QuerySPDapper(query, filtros);
                if (!string.IsNullOrEmpty(resultadoDB) && !resultadoDB.Contains("[]"))
                {
                    lista = JsonConvert.DeserializeObject<List<FiltroDTO>>(resultadoDB);
                }
                return lista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene todos materiales segun programa especifico y grupo, material accion y material version
        /// </summary>
        /// <returns></returns>
        public List<MaterialPEspecificoEntregaDTO> ObtenerMaterialesPorProgramaEspecificoGrupo(int idPEspecifico, int grupo)
		{
			try
			{
				var lista = new List<MaterialPEspecificoEntregaDTO>();
				var query = $@"
                     SELECT Id, 
						   Grupo, 
						   IdProgramaEspecifico, 
						   ProgramaEspecifico, 
						   IdMaterialTipo, 
						   MaterialTipo, 
						   IdMaterialAccion, 
						   MaterialAccion, 
						   IdMaterialPEspecificoDetalle, 
						   IdMaterialVersion, 
						   MaterialVersion, 
						   IdMaterialEstado, 
						   IdEstadoRegistroMaterial, 
						   EstadoRegistroMaterial,
						   IdFur
					FROM ope.V_TMaterialPEspecifico_ObtenerMaterialProgramaEspecifico
					WHERE Estado = 1
						  AND IdProgramaEspecifico = @IdPEspecifico
						  AND Grupo = @Grupo
						  AND IdMaterialVersion = 2
						  AND IdMaterialAccion = 2
						  AND IdMaterialEstado = 5
                ";
				var resultadoDB = _dapper.QueryDapper(query, new { IdPEspecifico = idPEspecifico, Grupo = grupo });
				if (!string.IsNullOrEmpty(resultadoDB) && !resultadoDB.Contains("[]"))
				{
					lista = JsonConvert.DeserializeObject<List<MaterialPEspecificoEntregaDTO>>(resultadoDB);
				}
				return lista;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		/// <summary>
		/// Obtiene todos materiales segun programa especifico y grupo, material accion y material version
		/// </summary>
		/// <returns></returns>
		/// <summary>
		/// Obtiene todos programas especificos hijos
		/// </summary>
		/// <returns></returns>
		public List<MaterialPEspecificoEntregaDTO> ObtenerMaterialesRegistroEntrega(FiltroMaterialDTO filtro)
		{
			try
			{
				var lista = new List<MaterialPEspecificoEntregaDTO>();
				var query = $@"ope.SP_ObtenerMateriales_Alterno";
				var filtros = new
				{
					ListaArea = string.Join(",", filtro.Area.Select(x => x)),
					ListaSubArea = string.Join(",", filtro.SubArea.Select(x => x)),
					ListaProgramaGeneral = string.Join(",", filtro.ProgramaGeneral.Select(x => x)),
					ListaProgramaEspecificoPadreIndividual = string.Join(",", filtro.ProgramaEspecificoPadreIndividual.Select(x => x)),
					ListaProgramaEspecificoCurso = string.Join(",", filtro.ProgramaEspecificoCurso.Select(x => x)),
					ListaEstadoProgramaEspecifico = string.Join(",", filtro.EstadoProgramaEspecifico.Select(x => x)),
					ListaCiudad = string.Join(",", filtro.Ciudad.Select(x => x)),
					ListaModalidad = string.Join(",", filtro.Modalidad.Select(x => x)),
					ListaMaterialEstado = "5",
					ListaMaterialVersion = "2",
					ListaMaterialAccion = "2"
				};
				var resultadoDB = _dapper.QuerySPDapper(query, filtros);
				if (!string.IsNullOrEmpty(resultadoDB) && !resultadoDB.Contains("[]"))
				{
					lista = JsonConvert.DeserializeObject<List<MaterialPEspecificoEntregaDTO>>(resultadoDB);
				}
				return lista;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}


		/// <summary>
		/// Obtiene todos materiales segun programa especifico y grupo, material accion y material version
		/// </summary>
		/// <returns></returns>
		/// <summary>
		/// Obtiene todos programas especificos hijos
		/// </summary>
		/// <returns></returns>
		public List<MaterialPEspecificoEntregaAlumnoDTO> ObtenerMaterialesRegistroEntregaAlumno(FiltroMaterialDTO filtro)
		{
			try
			{
				var lista = new List<MaterialPEspecificoEntregaAlumnoDTO>();
				var query = $@"ope.SP_ObtenerMateriales_AlternoAlumnos";
				var filtros = new
				{
					ListaArea = string.Join(",", filtro.Area.Select(x => x)),
					ListaSubArea = string.Join(",", filtro.SubArea.Select(x => x)),
					ListaProgramaGeneral = string.Join(",", filtro.ProgramaGeneral.Select(x => x)),
					ListaProgramaEspecificoPadreIndividual = string.Join(",", filtro.ProgramaEspecificoPadreIndividual.Select(x => x)),
					ListaProgramaEspecificoCurso = string.Join(",", filtro.ProgramaEspecificoCurso.Select(x => x)),
					ListaEstadoProgramaEspecifico = string.Join(",", filtro.EstadoProgramaEspecifico.Select(x => x)),
					ListaCiudad = string.Join(",", filtro.Ciudad.Select(x => x)),
					ListaModalidad = string.Join(",", filtro.Modalidad.Select(x => x)),
					ListaMaterialEstado = "5",
					ListaMaterialVersion = "2",
					ListaMaterialAccion = "2",
					ListaEstadoRegistroMaterial = "1"
				};
				var resultadoDB = _dapper.QuerySPDapper(query, filtros);
				if (!string.IsNullOrEmpty(resultadoDB) && !resultadoDB.Contains("[]"))
				{
					lista = JsonConvert.DeserializeObject<List<MaterialPEspecificoEntregaAlumnoDTO>>(resultadoDB);
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
