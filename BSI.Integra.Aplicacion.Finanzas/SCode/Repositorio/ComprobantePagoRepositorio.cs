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
    public class ComprobantePagoRepositorio : BaseRepository<TComprobantePago, ComprobantePagoBO>
    {
        #region Metodos Base
        public ComprobantePagoRepositorio() : base()
        {
        }
        public ComprobantePagoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<ComprobantePagoBO> GetBy(Expression<Func<TComprobantePago, bool>> filter)
        {
            IEnumerable<TComprobantePago> listado = base.GetBy(filter);
            List<ComprobantePagoBO> listadoBO = new List<ComprobantePagoBO>();
            foreach (var itemEntidad in listado)
            {
                ComprobantePagoBO objetoBO = Mapper.Map<TComprobantePago, ComprobantePagoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public ComprobantePagoBO FirstById(int id)
        {
            try
            {
                TComprobantePago entidad = base.FirstById(id);
                ComprobantePagoBO objetoBO = new ComprobantePagoBO();
                Mapper.Map<TComprobantePago, ComprobantePagoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public ComprobantePagoBO FirstBy(Expression<Func<TComprobantePago, bool>> filter)
        {
            try
            {
                TComprobantePago entidad = base.FirstBy(filter);
                ComprobantePagoBO objetoBO = Mapper.Map<TComprobantePago, ComprobantePagoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(ComprobantePagoBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TComprobantePago entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<ComprobantePagoBO> listadoBO)
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

        public bool Update(ComprobantePagoBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TComprobantePago entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<ComprobantePagoBO> listadoBO)
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
        private void AsignacionId(TComprobantePago entidad, ComprobantePagoBO objetoBO)
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

        private TComprobantePago MapeoEntidad(ComprobantePagoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TComprobantePago entidad = new TComprobantePago();
                entidad = Mapper.Map<ComprobantePagoBO, TComprobantePago>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //Mapea los hijos
                if (objetoBO.ComprobantePagoPorFur != null && objetoBO.ComprobantePagoPorFur.Count > 0)
                {
                    foreach (var hijo in objetoBO.ComprobantePagoPorFur)
                    {
                        TComprobantePagoPorFur entidadHijo = new TComprobantePagoPorFur();
                        entidadHijo = Mapper.Map<ComprobantePagoPorFurBO, TComprobantePagoPorFur>(hijo,
                            opt => opt.ConfigureMap(MemberList.None));
                        entidad.TComprobantePagoPorFur.Add(entidadHijo);
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
        /// Se obtiene todos los registros de comprobantes filtrados por el idFur
        /// </summary>
        /// <param name="idFur"></param>
        /// <returns></returns>
        public List<ComprobantePagoDTO> ObtenerComprobantePagoPorFur(int idFur)
        {
            try
            {
                var _query = "SELECT  Id, IdDocumentoPago, NombreDocumento, SerieComprobante, NumeroComprobante, Monto, IdMoneda, NombreMoneda, FechaEmision, FechaProgramacion, IdPais, NombrePais, IdIgv, ValorIGV, IdRetencion, ValorRetencion, IdDetraccion, ValorDetraccion, IdProveedor, NombreProveedor,CodigoFur, MontoAfecto,MontoInafecto,OtraTazaContribucion,AjusteMontoBruto FROM fin.V_ObtenerComprobantes WHERE IdFur = @idFur AND Estado = 1";
                var comprobantePagoDB = _dapper.QueryDapper(_query, new { idFur });
                return JsonConvert.DeserializeObject<List<ComprobantePagoDTO>>(comprobantePagoDB);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// Obtiene todos los furs asociados a un comprobante
        /// </summary>
        /// <param name="idFur"></param>
        /// <returns></returns>
        public List<ComprobantePagoPorFurCodigoDTO> ObtenerFursAsociados(int IdComprobante)
        {
            try
            {
                var _query = "SELECT Id,Codigo,MontoFur,IdComprobantePagoPorFur,IdComprobantePago FROM fin.V_ObtenerFursAsociados WHERE IdComprobantePago = @IdComprobante";
                var comprobantePagoDB = _dapper.QueryDapper(_query, new { IdComprobante });
                return JsonConvert.DeserializeObject<List<ComprobantePagoPorFurCodigoDTO>>(comprobantePagoDB);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// Obtiene la lista de Comprobantes asociados a un determinado Proveedor
        /// </summary>
        /// <param name="RucParcial"></param>
        /// <returns></returns>
        public List<RucSerieNumeroComprobanteDTO> ObtenerComprobantePorRuc(string RucParcial)
        {
            try
            {
                var _query = "SELECT  Id, Comprobante, MontoNeto, IdMoneda,IdTipoImpuesto,IdRetencion,IdDetraccion,IdPais FROM fin.V_ObtenerRucSerieNumeroComprobante WHERE NroDocIdentidad like '%"+RucParcial+"%' ";
                var comprobantePagoDB = _dapper.QueryDapper(_query, null);
                return JsonConvert.DeserializeObject<List<RucSerieNumeroComprobanteDTO>>(comprobantePagoDB);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<ComprobanteDTO> ObtenerComprobanteAutocomplete(string RucComprobanteParcial)
        {
            try
            {
                var _query = "SELECT  Id, IdProveedor,Ruc, Proveedor, Serie,Numero,IdSunatDocumento,SunatDocumento,concat(Proveedor,' ',Comprobante) as Comprobante, MontoBruto,FechaEmision FROM fin.V_ObtenerDatosComprobante WHERE concat(Proveedor,' ',Ruc,' ',Comprobante) like '%" + RucComprobanteParcial + "%' ";
                var comprobantePagoDB = _dapper.QueryDapper(_query, null);
                return JsonConvert.DeserializeObject<List<ComprobanteDTO>>(comprobantePagoDB);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<ComprobantePagoDatosComboDTO> ObtenerComprobantePago()
        {
            try
            {
                var _query = "SELECT  Id, IdProveedor,NumeroComprobante,MontoNeto FROM fin.V_ObtenerComprobantePago ";
                var comprobantePagoDB = _dapper.QueryDapper(_query, null);
                return JsonConvert.DeserializeObject<List<ComprobantePagoDatosComboDTO>>(comprobantePagoDB);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene Todos Los Comprobantes Asociados que tengan una asociacion a un Fur por medio de ComprobantePagoPorFur
        /// </summary>
        /// <param name="IdFur"></param>
        /// <returns></returns>
        public List<ComprobantePagoBO> ObtenerComprobantesAsociadosAFur(int IdFur)
        {
            try
            {
                List<ComprobantePagoBO> Comprobantes = new List<ComprobantePagoBO>();
                var _query = "exec [fin].[SP_BuscarComprobanteAsociadoAFur]  " + IdFur;
                var ComprobantesDB = _dapper.QueryDapper(_query, null);
                if (!ComprobantesDB.Contains("[]") && !string.IsNullOrEmpty(ComprobantesDB))
                {
                    Comprobantes = JsonConvert.DeserializeObject<List<ComprobantePagoBO>>(ComprobantesDB);
                }
                return Comprobantes;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
