using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using BSI.Integra.Aplicacion.DTOs;
using Newtonsoft.Json;
using BSI.Integra.Aplicacion.DTOs.Scode.DTOs.Transversal;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    /// Repositorio: Finanzas/CronogramaPagoDetalleFinal
    /// Autor: Carlos Crispin - Jose Villena
    /// Fecha: 01/05/2021
    /// <summary>
    /// Repositorio para consultas de fin.T_CronogramaPAgoDetalleFinal
    /// </summary>
    public class CronogramaPagoDetalleFinalRepositorio : BaseRepository<TCronogramaPagoDetalleFinal, CronogramaPagoDetalleFinalBO>
    {
        #region Metodos Base
        public CronogramaPagoDetalleFinalRepositorio() : base()
        {
        }
        public CronogramaPagoDetalleFinalRepositorio(integraDBContext contexto) : base(contexto)
        {
        }

        public IEnumerable<CronogramaPagoDetalleFinalBO> GetBy(Expression<Func<TCronogramaPagoDetalleFinal, bool>> filter)
        {
            try
            {
                IEnumerable<TCronogramaPagoDetalleFinal> listado = base.GetBy(filter);
                List<CronogramaPagoDetalleFinalBO> listadoBO = new List<CronogramaPagoDetalleFinalBO>();
                foreach (var itemEntidad in listado)
                {
                    CronogramaPagoDetalleFinalBO objetoBO = Mapper.Map<TCronogramaPagoDetalleFinal, CronogramaPagoDetalleFinalBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                    listadoBO.Add(objetoBO);
                }

                return listadoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public CronogramaPagoDetalleFinalBO FirstById(int id)
        {
            try
            {
                TCronogramaPagoDetalleFinal entidad = base.FirstById(id);
                CronogramaPagoDetalleFinalBO objetoBO = new CronogramaPagoDetalleFinalBO();
                Mapper.Map<TCronogramaPagoDetalleFinal, CronogramaPagoDetalleFinalBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public CronogramaPagoDetalleFinalBO FirstBy(Expression<Func<TCronogramaPagoDetalleFinal, bool>> filter)
        {
            try
            {
                TCronogramaPagoDetalleFinal entidad = base.FirstBy(filter);
                CronogramaPagoDetalleFinalBO objetoBO = Mapper.Map<TCronogramaPagoDetalleFinal, CronogramaPagoDetalleFinalBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(CronogramaPagoDetalleFinalBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TCronogramaPagoDetalleFinal entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<CronogramaPagoDetalleFinalBO> listadoBO)
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

        public bool Update(CronogramaPagoDetalleFinalBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TCronogramaPagoDetalleFinal entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<CronogramaPagoDetalleFinalBO> listadoBO)
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
        private void AsignacionId(TCronogramaPagoDetalleFinal entidad, CronogramaPagoDetalleFinalBO objetoBO)
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

        private TCronogramaPagoDetalleFinal MapeoEntidad(CronogramaPagoDetalleFinalBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TCronogramaPagoDetalleFinal entidad = new TCronogramaPagoDetalleFinal();
                entidad = Mapper.Map<CronogramaPagoDetalleFinalBO, TCronogramaPagoDetalleFinal>(objetoBO,
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

        public List<ProgramaListaCuotaDTO> ObtenerListaCuotaPrograma(int idMatricula)
        {
            try
            {
                string _queryCronogramaPagoDetalleMod = "Select FechaVencimiento,Cuota,Mora,NroCuota,Moneda,Cancelado,MontoCuotaDescuento From fin.V_TCronogramaPagoDetalleFinal_CuotasVentas where  IdMatriCulaCabecera =@IdMatricula and Estado=1 order by NroCuota";
                var queryCronogramaPagoDetalleMod = _dapper.QueryDapper(_queryCronogramaPagoDetalleMod , new { IdMatricula=idMatricula});
                return JsonConvert.DeserializeObject<List<ProgramaListaCuotaDTO>>(queryCronogramaPagoDetalleMod);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }      
        }

        /// <summary>
        /// Obtiene una lista de cuotas crep
        /// </summary>
        /// <param name="idMatriculaCabecera"></param>
        /// <param name="version"></param>
        /// <returns></returns>
        public List<ListadoCuotaCrepDTO> ObtenerCuotasCrepPorCodigoMatricula(int idMatriculaCabecera, int? version)
        {
            try
            {
                List<ListadoCuotaCrepDTO> listadoCuotaCreps = new List<ListadoCuotaCrepDTO>();
                var _query = "SELECT Id, NroCuota, NroSubCuota,FechaVencimiento,moneda,Cuota,Mora, Total, Enviado,fin.F_ObtenerFechaAnteriorCuota(@idMatriculaCabecera, Nrocuota, NroSubCuota) AS FechaAnterior, Adicional FROM fin.V_TCronogramaPagoDetalleFinal_ObtenerTodo WHERE IdMatriculaCabecera = @idMatriculaCabecera AND version = @version AND ( Cancelado = 0  OR IdFormaPago IN ( 1, 2, 6, 7 ) ) ORDER  BY NroCuota, NroSubCuota";
                var listadoCuotaCrepsDB = _dapper.QueryDapper(_query,new { idMatriculaCabecera, version});

                if (!string.IsNullOrEmpty(listadoCuotaCrepsDB) && !listadoCuotaCrepsDB.Contains("[]"))
                {
                    listadoCuotaCreps = JsonConvert.DeserializeObject<List<ListadoCuotaCrepDTO>>(listadoCuotaCrepsDB);
                }
                return listadoCuotaCreps;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="idMatriculaCabecera"></param>
        /// <param name="version"></param>
        /// <returns></returns>
        public List<ListadoCuotasModificadasDTO> ObtenerCuotas(int idMatriculaCabecera,int? version)
        {
            try
            {
                List<ListadoCuotasModificadasDTO> listadoCuotaCreps = new List<ListadoCuotasModificadasDTO>();
                var _query = "SELECT case(mora) when 0.00 then '1' + right('0' + convert(varchar,NroCuota),2) + right('0' + convert(varchar,NroSubCuota),2) + 'XXXXXX'  else  '2' + right('0' + convert(varchar,NroCuota),2) + right('0' + convert(varchar,NroSubCuota),2) + right('000000' + replace(convert(varchar,Mora),'.',''),6) end CodigoEspecial,NroCuota,NroSubCuota,round(Cuota,2) + ROUND(Mora, 2) Cuota, convert(varchar,FechaVencimiento,103) FechaVencimiento, fin.F_ObtenerFechaAnteriorCuota(@idMatriculaCabecera, Nrocuota, NroSubCuota) AS FechaAnterior, Enviado FROM fin.V_TCronogramaPagoDetalleFinal_ObtenerTodo WHERE IdMatriculaCabecera = @idMatriculaCabecera AND version = @version AND ( Cancelado = 0  OR IdFormaPago IN ( 1, 2, 6, 7 ) )";
                var listadoCuotaCrepsDB = _dapper.QueryDapper(_query, new { idMatriculaCabecera, version });

                if (!string.IsNullOrEmpty(listadoCuotaCrepsDB) && !listadoCuotaCrepsDB.Contains("[]"))
                {
                    listadoCuotaCreps = JsonConvert.DeserializeObject<List<ListadoCuotasModificadasDTO>>(listadoCuotaCrepsDB);
                }
                return listadoCuotaCreps;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public int ActualizarEnviado(string CodigoMatricula,int NroCuota,int NroSubCuota)
        {
            try
            {
                var registroDB = _dapper.QuerySPFirstOrDefault("fin.SP_ActualizarEnviadoCronograma", new { CodigoMatricula, NroCuota, NroSubCuota });
                var valor = JsonConvert.DeserializeObject<ResultadoDTO>(registroDB);
                return valor.Resultado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        ///Repositorio: CronogramaPagoDetalleFinalRepositorio
        ///Autor: Jose Villena.
        ///Fecha: 01/05/2021
        /// <summary>
        /// Obtiene versiones de Fecha de Compromiso
        /// </summary>
        /// <param name="IdCuota"> Id de la cuota </param>
        /// <returns> Lista de Personal por nombre Registrados : List<ResultadoFechaCompromiso> </returns>
        public List<ResultadoFechaCompromiso> ObtenerVersionesFechaCompromiso(int IdCuota)
        {
            try
            {
                var registroDB = _dapper.QuerySPDapper("fin.SP_ObtenerVersionesFechaCompromiso", new { IdCuota });
                var valor = JsonConvert.DeserializeObject<List<ResultadoFechaCompromiso>>(registroDB);
                return valor;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public int ActualizarUltimo(string CodigoMatricula, int NroCuota, int NroSubCuota)
        {
            try
            {
                var registroDB = _dapper.QuerySPFirstOrDefault("fin.SP_ActualizarUltimoCronograma", new { CodigoMatricula, NroCuota, NroSubCuota });
                var valor = JsonConvert.DeserializeObject<ResultadoDTO>(registroDB);
                return valor.Resultado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public int PagarCuotaCDPG_CtoFinal(string idMat, int NroCuota, int NroSubCuota, DateTime FechaPago, double MontoPagado, double MoraBanco, string MonedaPago, string NroDoc, int IdPeriodo, string UsuarioMod)
        {
            try
            {
                var registroDB = _dapper.QuerySPFirstOrDefault("fin.SP_PagarCuotaCDPG_CtoFinal", new { idMat,NroCuota, NroSubCuota, FechaPago, MontoPagado, MoraBanco, MonedaPago, NroDoc, IdPeriodo, UsuarioMod });
                var valor = JsonConvert.DeserializeObject<ResultadoDTO>(registroDB);
                return valor.Resultado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        public FechaVencimientoCuotaDTO ObtenerCuotaVencidaPorOportunidad(int idOportunidad)
        {
            try
            {
                FechaVencimientoCuotaDTO fechaVencimientoCuotaDTO = new FechaVencimientoCuotaDTO();

                var _query = @"SELECT TOP 1 FechaVencimiento
                                FROM ope.V_ObtenerCronogramaPagoVencidoPorOportunidad
                                WHERE IdOportunidad = @idOportunidad
                                ORDER BY Version DESC, NroCuota ASC, NroSubCuota ASC";

                var cuotaVencida = _dapper.FirstOrDefault(_query, new { idOportunidad });

                if (!string.IsNullOrEmpty(cuotaVencida) && !cuotaVencida.Contains("[]"))
                {
                    fechaVencimientoCuotaDTO = JsonConvert.DeserializeObject<FechaVencimientoCuotaDTO>(cuotaVencida);
                    return fechaVencimientoCuotaDTO;
                }
                return null;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        ///Repositorio: CronogramaPagoDetalleFinal
        ///Autor: Jose Villena
        ///Fecha: 01/05/2021
        /// <summary>
        /// Obtener Cronograma Finanzas del Alumno
        /// </summary>
        /// <param name="Version"> Version </param>
        /// <param name="IdMatriculaCabecera"> Id de la Matricula </param>
        /// <returns> Lista Cronograma del Alumno : List<CronogramaPagoDetalleFinalDTO> </returns>
        public List<CronogramaPagoDetalleFinalDTO> ObtenerCronogramaFinanzas(int Version, int IdMatriculaCabecera)
        {
            try
            {
                List<CronogramaPagoDetalleFinalDTO> cronogramaPagoDetalleFinals = new List<CronogramaPagoDetalleFinalDTO>();
                //string _query = "Select Id,IdMatriculaCabecera,NroCuota,NroSubCuota,FechaVencimiento,TotalPagar,case when WebMoneda=2 then round(Cuota*isnull(WebTipoCambio,1),-2) else Cuota end as Cuota,Saldo" +
                //                "  ,Mora,MontoPagado,Cancelado,TipoCuota,case when WebMoneda=2 then 'Pesos Colombianos' else Moneda end as Moneda,FechaPago,IdFormaPago,NombreFormaPago,IdCuenta," +
                //                "FechaPagoBanco,Enviado,Observaciones,IdDocumentoPago,NroDocumento,MonedaPago,TipoCambio,CuotaDolares,FechaProcesoPago,Version,Aprobado,FechaDeposito,WebMoneda,WebTipoCambio " +
                //                " From fin.V_ObtenerCronogramaPagodetalleFinal Where version=@Version and IdMatriculaCabecera=@IdMatriculaCabecera order by NroCuota,NroSubCuota asc";
                string _query = "SELECT preFinal.* ,  comp.FechaCompromiso, comp.FechaGeneracionCompromiso AS FechaGeneradoCompromiso, comp.Monto AS MontoCompromiso "
                    + "FROM( "
                    +"SELECT CPDF.Id,CPDF.IdMatriculaCabecera,CPDF.NroCuota,CPDF.NroSubCuota,CPDF.FechaVencimiento,CPDF.TotalPagar,CASE WHEN CPDF.WebMoneda = 2 THEN round(CPDF.Cuota* isnull(CPDF.WebTipoCambio,1),-2) ELSE CPDF.Cuota END AS Cuota,CPDF.Saldo "
                    +", CPDF.Mora,CPDF.MontoPagado,CPDF.Cancelado,CPDF.TipoCuota,CASE WHEN CPDF.WebMoneda = 2 THEN 'Pesos Colombianos' ELSE CPDF.Moneda END AS Moneda, CPDF.FechaPago,CPDF.IdFormaPago,CPDF.NombreFormaPago,CPDF.IdCuenta "
                    +",CPDF.FechaPagoBanco,CPDF.Enviado,CPDF.Observaciones,CPDF.IdDocumentoPago,CPDF.NroDocumento,CPDF.MonedaPago,CPDF.TipoCambio,CPDF.CuotaDolares,CPDF.FechaProcesoPago,CPDF.Version,CPDF.Aprobado,CPDF.FechaDeposito,CPDF.WebMoneda,CPDF.WebTipoCambio, "
                    +"CASE "
                    +"    WHEN CPDF.WebMoneda = 0 THEN TARD.MontoPeru "
                    +"    WHEN CPDF.WebMoneda = 1 THEN TARD.MontoExtranjero "
                    +"    WHEN CPDF.WebMoneda = 2 THEN TARD.MontoColombia "
                    +"    WHEN CPDF.WebMoneda = 3 THEN TARD.MontoBolivia "
                    +" END AS MoraTarifario "
                    +" , MAX(compromiso.Version) AS VersionCompromiso "
                    +" From fin.V_ObtenerCronogramaPagodetalleFinal CPDF "
                    +" LEFT JOIN ope.T_OportunidadClasificacionOperaciones OPE ON CPDF.IdMatriculaCabecera = OPE.IdMatriculaCabecera "
                    +" LEFT JOIN mkt.T_Tarifario TAR ON OPE.idtarifario = TAR.Id "
                    +" LEFT JOIN mkt.T_TarifarioDetalle TARD ON TAR.Id = TARD.IdTarifario AND TARD.AplicaCuota = 1 "
                    +" LEFT JOIN fin.T_CompromisoAlumno AS compromiso ON CPDF.Id = compromiso.IdCronogramaPagoDetalleFinal "
                    +" WHERE CPDF.version = @Version AND CPDF.IdMatriculaCabecera = @IdMatriculaCabecera " 
                    +" GROUP BY CPDF.Id,CPDF.IdMatriculaCabecera,CPDF.NroCuota,CPDF.NroSubCuota,CPDF.FechaVencimiento,CPDF.TotalPagar, "
                    +" CASE "
                    +"    WHEN CPDF.WebMoneda = 2 THEN round(CPDF.Cuota* isnull(CPDF.WebTipoCambio,1),-2) "
                    +"	ELSE CPDF.Cuota "
                    +"END "
                    +",CPDF.Saldo "
                    +",CPDF.Mora,CPDF.MontoPagado,CPDF.Cancelado,CPDF.TipoCuota, "
                    +"CASE "
                    +"    WHEN CPDF.WebMoneda = 2 "
                    +"    THEN 'Pesos Colombianos' ELSE CPDF.Moneda "
                    +"END, "
                    +"CPDF.FechaPago,CPDF.IdFormaPago,CPDF.NombreFormaPago,CPDF.IdCuenta "
                    +",CPDF.FechaPagoBanco,CPDF.Enviado,CPDF.Observaciones,CPDF.IdDocumentoPago,CPDF.NroDocumento,CPDF.MonedaPago,CPDF.TipoCambio,CPDF.CuotaDolares,CPDF.FechaProcesoPago,CPDF.Version,CPDF.Aprobado,CPDF.FechaDeposito,CPDF.WebMoneda,CPDF.WebTipoCambio,  "
                    +"CASE "
                    +"    WHEN CPDF.WebMoneda = 0 THEN TARD.MontoPeru "
                    +"    WHEN CPDF.WebMoneda = 1 THEN TARD.MontoExtranjero "
                    +"    WHEN CPDF.WebMoneda = 2 THEN TARD.MontoColombia "
                    +"    WHEN CPDF.WebMoneda = 3 THEN TARD.MontoBolivia "
                    +" END "
                    +") AS preFinal "
                    +"LEFT JOIN fin.T_CompromisoAlumno AS comp ON preFinal.Id = comp.IdCronogramaPagoDetalleFinal  AND comp.Version = preFinal.VersionCompromiso "
                    +"ORDER BY preFinal.NroCuota, preFinal.NroSubCuota ";

                var registroDB = _dapper.QueryDapper(_query, new { Version, IdMatriculaCabecera});

                if (!registroDB.Contains("[]"))
                {
                    cronogramaPagoDetalleFinals = JsonConvert.DeserializeObject<List<CronogramaPagoDetalleFinalDTO>>(registroDB);
                }              

                return cronogramaPagoDetalleFinals;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene la maxima version del cronograma
        /// </summary>
        /// <param name="idMatriculaCabecera"></param>
        /// <returns></returns>
        public int ObtenerMaximaVersionCronograma(int idMatriculaCabecera) {
            try
            {
                var _resultado = new ValorIntDTO();
                var query = $@"fin.SP_ObtenerVersionMaximaCronograma";
                var resultado = _dapper.QuerySPFirstOrDefault(query, new { IdMatriculaCabecera = idMatriculaCabecera });

                if (!string.IsNullOrEmpty(resultado))
                {
                    _resultado = JsonConvert.DeserializeObject<ValorIntDTO>(resultado);
                }
                return _resultado.Valor;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
