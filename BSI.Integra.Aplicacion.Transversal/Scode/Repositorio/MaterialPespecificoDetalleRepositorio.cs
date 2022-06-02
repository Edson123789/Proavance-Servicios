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
    /// Repositorio: Transversal/MaterialPespecificoDetalle
    /// Autor: Wilber Choque - Luis Huallpa - Gian Miranda
    /// Fecha: 11/07/2021
    /// <summary>
    /// Repositorio para operaciones con la tabla ope.T_MaterialPEspecificoDetalle
    /// </summary>
    public class MaterialPespecificoDetalleRepositorio : BaseRepository<TMaterialPespecificoDetalle, MaterialPespecificoDetalleBO>
    {
        #region Metodos Base
        public MaterialPespecificoDetalleRepositorio() : base()
        {
        }
        public MaterialPespecificoDetalleRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<MaterialPespecificoDetalleBO> GetBy(Expression<Func<TMaterialPespecificoDetalle, bool>> filter)
        {
            IEnumerable<TMaterialPespecificoDetalle> listado = base.GetBy(filter);
            List<MaterialPespecificoDetalleBO> listadoBO = new List<MaterialPespecificoDetalleBO>();
            foreach (var itemEntidad in listado)
            {
                MaterialPespecificoDetalleBO objetoBO = Mapper.Map<TMaterialPespecificoDetalle, MaterialPespecificoDetalleBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public MaterialPespecificoDetalleBO FirstById(int id)
        {
            try
            {
                TMaterialPespecificoDetalle entidad = base.FirstById(id);
                MaterialPespecificoDetalleBO objetoBO = new MaterialPespecificoDetalleBO();
                Mapper.Map<TMaterialPespecificoDetalle, MaterialPespecificoDetalleBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public MaterialPespecificoDetalleBO FirstBy(Expression<Func<TMaterialPespecificoDetalle, bool>> filter)
        {
            try
            {
                TMaterialPespecificoDetalle entidad = base.FirstBy(filter);
                MaterialPespecificoDetalleBO objetoBO = Mapper.Map<TMaterialPespecificoDetalle, MaterialPespecificoDetalleBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(MaterialPespecificoDetalleBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TMaterialPespecificoDetalle entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<MaterialPespecificoDetalleBO> listadoBO)
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

        public bool Update(MaterialPespecificoDetalleBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TMaterialPespecificoDetalle entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<MaterialPespecificoDetalleBO> listadoBO)
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
        private void AsignacionId(TMaterialPespecificoDetalle entidad, MaterialPespecificoDetalleBO objetoBO)
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

        private TMaterialPespecificoDetalle MapeoEntidad(MaterialPespecificoDetalleBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TMaterialPespecificoDetalle entidad = new TMaterialPespecificoDetalle();
                entidad = Mapper.Map<MaterialPespecificoDetalleBO, TMaterialPespecificoDetalle>(objetoBO,
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
        /// Se obtiene las versiones de material asociadas a un material 
        /// </summary>
        /// <param name="idMaterialPEspecifico">Id del material PEspecifico (PK de la tabla opt.T_MaterialPEspecifico)</param>
        /// <returns>Lista de objetos de clase MaterialPespecificoDetalleBO</returns>
        public List<MaterialPespecificoDetalleBO> ObtenerPorMaterialPEspecifico(int idMaterialPEspecifico, List<int> listaIdMaterialEstado) {
            try
            {
                return this.GetBy(x => x.IdMaterialPespecifico == idMaterialPEspecifico && listaIdMaterialEstado.Contains(x.IdMaterialEstado) ).ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
        }


		/// <summary>
		/// Obtiene detalle de fur de un materialpespecifico detalle
		/// </summary>
		/// <param name="idMaterialPEspecificoDetalle"></param>
		/// <returns></returns>
		public MaterialPEspecificoDetalleFurDTO ObtenerDetalleFur(int idMaterialPEspecificoDetalle)
		{
			try
			{
				MaterialPEspecificoDetalleFurDTO lista = new MaterialPEspecificoDetalleFurDTO();
				var query = "SELECT IdMaterialPEspecificoDetalle,IdFur,IdProveedor,IdProducto,Monto,Cantidad,NombrePlural,Simbolo,FechaEntrega,DireccionEntrega FROM ope.V_TMaterialPEspecificoDetalle_ObtenerDetalleFur WHERE Estado = 1 AND IdMaterialPEspecificoDetalle = @IdMaterialPEspecificoDetalle";
				var resultadoDB = _dapper.FirstOrDefault(query, new { IdMaterialPEspecificoDetalle = idMaterialPEspecificoDetalle  });
				if (!string.IsNullOrEmpty(resultadoDB) && !resultadoDB.Contains("[]"))
				{
					lista = JsonConvert.DeserializeObject<MaterialPEspecificoDetalleFurDTO>(resultadoDB);
				}
				return lista;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

        /// <summary>
        /// Obtiene detalle de materialpespecifico
        /// </summary>
        /// <param name="idMaterialPEspecifico">Id del material del PEspecifico (PK de la tabla ope.T_MaterialPEspecifico)</param>
        /// <param name="idMaterialAccion">Id de la accion material(PK de la tabla ope.T_MaterialAccion)</param>
        /// <param name="idMaterialVersion">Id de la version del material (PK de la tabla ope.T_MaterialVersion)</param>
        /// <returns>Lista de objetos de clase MaterialPEspecificoDetalleCriteriosDTO</returns>
        public List<MaterialPEspecificoDetalleCriteriosDTO> ObtenerDetalleMaterialPEspecifico(int idMaterialPEspecifico, int idMaterialAccion, int idMaterialVersion)
		{
			try
			{
				List<MaterialPEspecificoDetalleCriteriosDTO> lista = new List<MaterialPEspecificoDetalleCriteriosDTO>();
				var query = "SELECT IdMaterialPEspecificoDetalle,IdMaterialPEspecifico,IdMaterialAccion,IdMaterialVersion FROM ope.V_TMaterialPEspecificoDetalle_ObtenerMaterialPEsécificoDetalle WHERE Estado = 1 AND IdMaterialPEspecifico = @IdMaterialPEspecifico AND IdMaterialAccion = @IdMaterialAccion AND IdMaterialVersion = @IdMaterialVersion";
				var resultadoDB = _dapper.QueryDapper(query, new { IdMaterialPEspecifico = idMaterialPEspecifico, IdMaterialAccion = idMaterialAccion, IdMaterialVersion = idMaterialVersion });
				if (!string.IsNullOrEmpty(resultadoDB) && !resultadoDB.Contains("[]"))
				{
					lista = JsonConvert.DeserializeObject<List<MaterialPEspecificoDetalleCriteriosDTO>>(resultadoDB);
				}
				return lista;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

        /// <summary>
        /// Obtiene detalle de materialpespecifico
        /// </summary>
        /// <param name="idMaterialPEspecificoDetalle"></param>
        /// <returns></returns>
        public MaterialPEspecificoDetalleEnvioProveedorDTO ObtenerDetalleMaterialPEspecificoEnviarProveedor(int id)
        {
            try
            {
                MaterialPEspecificoDetalleEnvioProveedorDTO valor = new MaterialPEspecificoDetalleEnvioProveedorDTO();
                var query = "ope.SP_ObtenerDetalleEnvioProveedorImpresion";
                var resultadoDB = _dapper.QuerySPFirstOrDefault(query, new { IdMaterialPEspecificoDetalle = id });
                if (!string.IsNullOrEmpty(resultadoDB) && !resultadoDB.Contains("[]"))
                {
                    valor = JsonConvert.DeserializeObject<MaterialPEspecificoDetalleEnvioProveedorDTO>(resultadoDB);
                }
                return valor;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

	}
}
