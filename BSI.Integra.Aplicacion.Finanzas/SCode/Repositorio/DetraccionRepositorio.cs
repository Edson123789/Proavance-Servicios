using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Finanzas.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Finanzas.Repositorio
{
    public class DetraccionRepositorio : BaseRepository<TDetraccion, DetraccionBO>
    {
        #region Metodos Base
        public DetraccionRepositorio() : base()
        {
        }
        public DetraccionRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<DetraccionBO> GetBy(Expression<Func<TDetraccion, bool>> filter)
        {
            IEnumerable<TDetraccion> listado = base.GetBy(filter);
            List<DetraccionBO> listadoBO = new List<DetraccionBO>();
            foreach (var itemEntidad in listado)
            {
                DetraccionBO objetoBO = Mapper.Map<TDetraccion, DetraccionBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public DetraccionBO FirstById(int id)
        {
            try
            {
                TDetraccion entidad = base.FirstById(id);
                DetraccionBO objetoBO = new DetraccionBO();
                Mapper.Map<TDetraccion, DetraccionBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public DetraccionBO FirstBy(Expression<Func<TDetraccion, bool>> filter)
        {
            try
            {
                TDetraccion entidad = base.FirstBy(filter);
                DetraccionBO objetoBO = Mapper.Map<TDetraccion, DetraccionBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(DetraccionBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TDetraccion entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<DetraccionBO> listadoBO)
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

        public bool Update(DetraccionBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TDetraccion entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<DetraccionBO> listadoBO)
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
        private void AsignacionId(TDetraccion entidad, DetraccionBO objetoBO)
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

        private TDetraccion MapeoEntidad(DetraccionBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TDetraccion entidad = new TDetraccion();
                entidad = Mapper.Map<DetraccionBO, TDetraccion>(objetoBO,
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
        /// Obtiene [Id, Valor] de las Detracciones existentes en una lista 
        /// para ser mostradas en un ComboBox (utilizado en CRUD 'RendicionRequerimientos')
        /// </summary>
        /// <returns></returns>
        public List<FiltroDTO> ObtenerListaDetraccion()
        {
            try
            {
                List<FiltroDTO> detracciones = new List<FiltroDTO>();
                var _query = string.Empty;
                _query = "SELECT Id, Valor as Nombre FROM fin.T_Detraccion WHERE Estado = 1";
                var detraccionesDB = _dapper.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(detraccionesDB) && !detraccionesDB.Contains("[]"))
                {
                    detracciones = JsonConvert.DeserializeObject<List<FiltroDTO>>(detraccionesDB);
                }
                return detracciones;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Obtiene [Id, Valor] de Detraccion segun el pais
        /// </summary>
        /// <returns></returns>
        public List<FiltroDTO> ObtenerValorDetraccionPorPais(int IdPais)
        {
            try
            {
                List<FiltroDTO> Retencion = new List<FiltroDTO>();
                var _query = string.Empty;
                _query = "SELECT  IdDetraccion as Id, ValorDetraccion as Nombre FROM [fin].[V_ObtenerDetraccionAsociadoPais] WHERE IdPais =" + IdPais;
                var RetencionDB = _dapper.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(RetencionDB) && !RetencionDB.Contains("[]"))
                {
                    Retencion = JsonConvert.DeserializeObject<List<FiltroDTO>>(RetencionDB);
                }
                return Retencion;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public List<ReporteDetraccionDTO> ObtenerReporteDetraccion(String IdsOrigen, string FechaEmision, string FechaVencimiento)
        {
            try
            {
                List<ReporteDetraccionDTO> detracciones = new List<ReporteDetraccionDTO>();
                var _query = string.Empty;
                if (FechaEmision.Trim().Equals("") && FechaVencimiento.Trim().Equals("")) 
                    _query = "SELECT Empresa, NroDocIdentidad, NombreProveedor, NumeroComprobante,NombreMoneda,MontoBruto,MontoIgv,MontoNeto,PorcentajeDetraccion,MontoDetraccion,FechaEmision,FechaVencimiento,PeriodoTributario FROM fin.V_ReporteDetraccion where IdSede IN (" + IdsOrigen + ")  order by FechaEmision asc";
                else if (FechaVencimiento.Trim().Equals(""))
                    _query = "SELECT Empresa, NroDocIdentidad, NombreProveedor, NumeroComprobante,NombreMoneda,MontoBruto,MontoIgv,MontoNeto,PorcentajeDetraccion,MontoDetraccion,FechaEmision,FechaVencimiento,PeriodoTributario FROM fin.V_ReporteDetraccion where IdSede IN (" + IdsOrigen + ") AND " + "FechaEmision >= '" + FechaEmision + "'  order by FechaEmision asc";
                else if (FechaEmision.Trim().Equals(""))
                    _query = "SELECT Empresa, NroDocIdentidad, NombreProveedor, NumeroComprobante,NombreMoneda,MontoBruto,MontoIgv,MontoNeto,PorcentajeDetraccion,MontoDetraccion,FechaEmision,FechaVencimiento,PeriodoTributario FROM fin.V_ReporteDetraccion where IdSede IN (" + IdsOrigen + ") AND  FechaVencimiento <='" + FechaVencimiento + "' order by FechaEmision asc";
                else
                    _query = "SELECT Empresa, NroDocIdentidad, NombreProveedor, NumeroComprobante,NombreMoneda,MontoBruto,MontoIgv,MontoNeto,PorcentajeDetraccion,MontoDetraccion,FechaEmision,FechaVencimiento,PeriodoTributario FROM fin.V_ReporteDetraccion where IdSede IN (" + IdsOrigen + ") AND " + "FechaEmision >= '" + FechaEmision + "'  and FechaVencimiento <='" + FechaVencimiento + "' order by FechaEmision asc";


                var detraccionesDB = _dapper.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(detraccionesDB) && !detraccionesDB.Contains("[]"))
                {
                    detracciones = JsonConvert.DeserializeObject<List<ReporteDetraccionDTO>>(detraccionesDB);
                }
                return detracciones;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
