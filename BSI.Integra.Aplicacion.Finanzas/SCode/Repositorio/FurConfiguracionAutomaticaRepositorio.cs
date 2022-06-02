using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Finanzas.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using BSI.Integra.Persistencia.SCode.Repository;
using Newtonsoft.Json;


namespace BSI.Integra.Aplicacion.Finanzas.Repositorio
{
    public class FurConfiguracionAutomaticaRepositorio : BaseRepository<TFurConfiguracionAutomatica, FurConfiguracionAutomaticaBO>
    {
        #region Metodos Base
        public FurConfiguracionAutomaticaRepositorio() : base()
        {
        }
        public FurConfiguracionAutomaticaRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<FurConfiguracionAutomaticaBO> GetBy(Expression<Func<TFurConfiguracionAutomatica, bool>> filter)
        {
            IEnumerable<TFurConfiguracionAutomatica> listado = base.GetBy(filter);
            List<FurConfiguracionAutomaticaBO> listadoBO = new List<FurConfiguracionAutomaticaBO>();
            foreach (var itemEntidad in listado)
            {
                FurConfiguracionAutomaticaBO objetoBO = Mapper.Map<TFurConfiguracionAutomatica, FurConfiguracionAutomaticaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public FurConfiguracionAutomaticaBO FirstById(int id)
        {
            try
            {
                TFurConfiguracionAutomatica entidad = base.FirstById(id);
                FurConfiguracionAutomaticaBO objetoBO = new FurConfiguracionAutomaticaBO();
                Mapper.Map<TFurConfiguracionAutomatica, FurConfiguracionAutomaticaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public FurConfiguracionAutomaticaBO FirstBy(Expression<Func<TFurConfiguracionAutomatica, bool>> filter)
        {
            try
            {
                TFurConfiguracionAutomatica entidad = base.FirstBy(filter);
                FurConfiguracionAutomaticaBO objetoBO = Mapper.Map<TFurConfiguracionAutomatica, FurConfiguracionAutomaticaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(FurConfiguracionAutomaticaBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TFurConfiguracionAutomatica entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<FurConfiguracionAutomaticaBO> listadoBO)
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

        public bool Update(FurConfiguracionAutomaticaBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TFurConfiguracionAutomatica entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<FurConfiguracionAutomaticaBO> listadoBO)
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
        private void AsignacionId(TFurConfiguracionAutomatica entidad, FurConfiguracionAutomaticaBO objetoBO)
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

        private TFurConfiguracionAutomatica MapeoEntidad(FurConfiguracionAutomaticaBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TFurConfiguracionAutomatica entidad = new TFurConfiguracionAutomatica();
                entidad = Mapper.Map<FurConfiguracionAutomaticaBO, TFurConfiguracionAutomatica>(objetoBO,
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
        /// Obtiene  registros con Estado=1 de fin.T_FurConfiguracionAutomatica (para llenado de grilla)
        /// </summary>
        /// <returns></returns>
        public List<FurConfiguracionAutomaticaConProductoDTO> ObtenerRegistrosFurConfiguracionAutomatica()
        {
            try
            {
           

                List<FurConfiguracionAutomaticaConProductoDTO> FurConfiguraciones = new List<FurConfiguracionAutomaticaConProductoDTO>();
                var _query = "SELECT Id, IdSede,NombreSede, IdFurTipoPedido,NombreFurTipoPedido,IdPersonalAreaTrabajo,NombrePersonalAreaTrabajo,Cantidad,IdMonedaPagoReal,NombreMonedaPagoReal,AjusteNumeroSemana,RucProveedor,"+ 
                    "NombreProveedor,NombreProducto, IdFrecuencia,NombreFrecuencia,NombreCentroCosto,Descripcion, FechaGeneracionFur,FechaInicioConfiguracion, FechaFinConfiguracion, " +
                    "IdProducto, IdProveedor, IdProductoPresentacion, ISNULL(IdCentroCosto,0) AS IdCentroCosto, PrecioUnitario, MontoProyectado, Usuario, NombreFurTipoSolicitud,IdEmpresa,RazonSocial FROM fin.V_ObtenerConfiguracionAutomaticaConProveedorYProducto WHERE Estado = 1";
					
				var FurConfiguracionesDB = _dapper.QueryDapper(_query, null);
                if (!FurConfiguracionesDB.Contains("[]") && !string.IsNullOrEmpty(FurConfiguracionesDB))
                {
                    FurConfiguraciones = JsonConvert.DeserializeObject<List<FurConfiguracionAutomaticaConProductoDTO>>(FurConfiguracionesDB);
                }
                return FurConfiguraciones;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene  un (1) unico registro con  dado un Id de fin.T_FurConfiguracionAutomatica (para actualizar una fila de una grilla)
        /// </summary>
        /// <returns></returns>
        public List<FurConfiguracionAutomaticaConProductoDTO> ObtenerRegistrosFurConfiguracionAutomaticaPorId(int IdConfiguracion)
        {
            try
            {
                List<FurConfiguracionAutomaticaConProductoDTO> FurConfiguraciones = new List<FurConfiguracionAutomaticaConProductoDTO>();
                var _query = "SELECT Id, IdSede,NombreSede, IdFurTipoPedido,NombreFurTipoPedido,IdPersonalAreaTrabajo,NombrePersonalAreaTrabajo,Cantidad,IdMonedaPagoReal,NombreMonedaPagoReal,AjusteNumeroSemana,RucProveedor," +
                    "NombreProveedor,NombreProducto, IdFrecuencia,NombreFrecuencia,NombreCentroCosto,Descripcion, FechaGeneracionFur,FechaInicioConfiguracion, FechaFinConfiguracion, " +
                    "IdProducto, IdProveedor, IdProductoPresentacion, IdCentroCosto, PrecioUnitario, MontoProyectado, Usuario FROM fin.V_ObtenerConfiguracionAutomaticaConProveedorYProducto where Estado=1 AND Id=" + IdConfiguracion;
                var FurConfiguracionesDB = _dapper.QueryDapper(_query, null);
                if (!FurConfiguracionesDB.Contains("[]") && !string.IsNullOrEmpty(FurConfiguracionesDB))
                {
                    FurConfiguraciones = JsonConvert.DeserializeObject<List<FurConfiguracionAutomaticaConProductoDTO>>(FurConfiguracionesDB);
                }
                return FurConfiguraciones;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        /// <summary>
        /// Llama al store procedure '[fin].[sp_ActualizarFURCOMPROBANTE]' para actualizar el estado 'IdFurSubEstadoFaseAprobacion' en el fur (o en el comprobante si esque tubiera uno asociado)
        /// </summary>
        /// <param name="Sedes"></param>
        /// <param name="Anio"></param>
        /// <returns></returns>
        public bool EjecutarActualizacionEstadoFurOComprobante()
        {
            try
            {
                List<ReporteEgresoPorRubroDTO> gastosRubro = new List<ReporteEgresoPorRubroDTO>();
                DapperRepository _dapper2 = new DapperRepository();
                var query = _dapper2.QuerySPDapper("fin.sp_ActualizarFURCOMPROBANTE", null);

               
                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    } 
}
