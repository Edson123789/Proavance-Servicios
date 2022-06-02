using BSI.Integra.Aplicacion.Classes;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.SCode.IRepository;
using BSI.Integra.Persistencia.SCode.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using BSI.Integra.Aplicacion.Servicios.Builder;
using System.Text;
using SentinelService;
using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.Base.Classes;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Comercial.Repositorio;
using BSI.Integra.Aplicacion.Planificacion.Repositorio;
using BSI.Integra.Aplicacion.Transversal.Repositorio;

namespace BSI.Integra.Aplicacion.Comercial.BO
{
    public class SentinelBO : BaseBO
    {
        public string Dni { get; set; }

        public virtual ICollection<SentinelSdtEstandarItemBO> DniRuc { get; set; }
        public SentinelSdtInfGenBO DatosGenerales { get; set; }
        public List<SentinelSdtRepSbsitemBO> Deuda { get; set; }
        public List<SentinelSdtResVenItemBO> DatosVencidas { get; set; }
        public List<SentinelSdtLincreItemBO> LineaCredito { get; set; }
        public List<SentinelSdtPoshisItemBO> PosicionHistoria { get; set; }
        public List<SentinelRepLegItemBO> Cargo { get; set; }

		public Guid? IdMigracion { get; set; }

		private SentinelSueldoIndividualRepositorio _repSentinelSueldoIndividual;
        private SentinelSueldoPorIndustriaDataTotalRepositorio _repSentinelSueldoPorIndustriaDataTotal;
        private SentinelSueldoPorIndustriaRepositorio _repSentinelSueldoPorIndustria;
        private EmpresaRepositorio _repEmpresa;
        private SentinelSueldoPorIndustriaDataDinamicoRepositorio _repSentinelSueldoPorIndustriaDataDinamico;

        public SentinelBO()
        {
            ActualesErrores = new Dictionary<string, List<ErrorInfo>>();

            _repSentinelSueldoIndividual = new SentinelSueldoIndividualRepositorio();
            _repSentinelSueldoPorIndustriaDataTotal = new SentinelSueldoPorIndustriaDataTotalRepositorio();
            _repSentinelSueldoPorIndustria = new SentinelSueldoPorIndustriaRepositorio();
            _repEmpresa = new EmpresaRepositorio();
            _repSentinelSueldoPorIndustriaDataDinamico = new SentinelSueldoPorIndustriaDataDinamicoRepositorio();
        }

        /// Autor: Ficher V.
        /// Fecha: 31/09/2019
        /// Version: 1.0
        /// <summary>
        /// Consulta al servicio de Sentinel por DNI  del Alumno y guarda la informacion retornada
        /// asociada al Id del Alumno en Integra
        /// </summary>
        /// <param name="dni">dni de alumno</param>
        /// <param name="usuario">usuario que genera la consulta</param>
        /// <returns></returns>
        public void ActualizarSentinelAlumno(string dni, string usuario)
        {
            try
            {
                WS_BSGrupoSoapPortClient client = new WS_BSGrupoSoapPortClient();

                SDT_IC_EstandarSDT_IC_EstandarItem[] sdt_bsgrupo_estandar;
                SDT_IC_RepSBSSDT_IC_RepSBSItem[] sdt_bsgrupo_repsbs;
                SDT_IC_LinCreItem[] sdt_bsgrupo_lincre;
                SDT_IC_ResVenSDT_IC_ResVenItem[] sdt_bsgrupo_resven;
                SDT_IC_InfGen sdt_bsgrupo_infgen;
                SDT_IC_RepLegSDT_IC_RepLegItem[] sdt_bsgrupo_repleg;
                SDT_IC_PosHisSDT_IC_PosHisItem[] sdt_bsgrupo_poshis;

                //Consultamos al servicio de Sentinel//Antigua Contraseña:Empres@grup09
                client.Execute("41170772", "Arequip@2022BSG", 1438, "D", Dni, out sdt_bsgrupo_estandar, out sdt_bsgrupo_repsbs,
                    out sdt_bsgrupo_lincre, out sdt_bsgrupo_resven, out sdt_bsgrupo_infgen, out sdt_bsgrupo_repleg, out sdt_bsgrupo_poshis);

                var sdt_bsgrupo_estandar_dtos = BuilderOrquestaSentinel_SDT_BSGrupo_EstandarItemDTO.builderListEntityDTO(sdt_bsgrupo_estandar.ToList());
                var sdt_bsgrupo_repsbs_dtos = BuilderOrquestaSentinel_SDT_BSGrupo_RepSBSItemDTO.builderListEntityDTO(sdt_bsgrupo_repsbs.ToList());
                var sdt_bsgrupo_lincre_dtos = BuilderOrquestaSentinel_SDT_BSGrupo_LinCreItemDTO.builderListEntityDTO(sdt_bsgrupo_lincre.ToList());
                var sdt_bsgrupo_resven_dtos = BuilderOrquestaSentinel_SDT_BSGrupo_ResVenItemDTO.builderListEntityDTO(sdt_bsgrupo_resven.ToList());
                var sdt_bsgrupo_infgen_dto = BuilderOrquestaSentinel_SDT_BSGrupo_InfGenDTO.builderEntityDTO(sdt_bsgrupo_infgen);
                var sdt_bsgrupo_repleg_dtos = BuilderOrquestaSentinel_SDT_BSGrupo_RepLegItemDTO.builderListEntityDTO(sdt_bsgrupo_repleg.ToList());
                var sdt_bsgrupo_poshis_dtos = BuilderOrquestaSentinel_SDT_BSGrupo_PosHisItemDTO.builderListEntityDTO(sdt_bsgrupo_poshis.ToList());

                DniRuc = new List<SentinelSdtEstandarItemBO>();
                Deuda = new List<SentinelSdtRepSbsitemBO>();
                LineaCredito = new List<SentinelSdtLincreItemBO>();
                DatosVencidas = new List<SentinelSdtResVenItemBO>();
                Cargo = new List<SentinelRepLegItemBO>();
                PosicionHistoria = new List<SentinelSdtPoshisItemBO>();

                foreach (var entity in sdt_bsgrupo_estandar_dtos)
                {                    
                    SentinelSdtEstandarItemBO rpta = new SentinelSdtEstandarItemBO() ;
                    rpta.TipoDocumento = entity.TipoDocumento;
                    rpta.Documento = entity.Documento;
                    rpta.RazonSocial = entity.RazonSocial;
                    rpta.FechaProceso = entity.FechaProceso;
                    rpta.Semaforos = entity.Semaforos;
                    rpta.Score = entity.Score;
                    rpta.NroBancos = entity.NroBancos;
                    rpta.DeudaTotal = entity.DeudaTotal;
                    rpta.VencidoBanco = entity.VencidoBanco;
                    rpta.Calificativo = entity.Calificativo;
                    rpta.Veces24m = entity.Veces24m;
                    rpta.SemanaActual = entity.SemanaActual;
                    rpta.SemanaPrevio = entity.SemanaPrevio;
                    rpta.SemanaPeorMejor = entity.SemanaPeorMejor;
                    rpta.Documento2 = entity.Documento2;
                    rpta.EstadoDomicilio = entity.EstadoDomicilio;
                    rpta.CondicionDomicilio = entity.CondicionDomicilio;
                    rpta.DeudaTributaria = entity.DeudaTributaria;
                    rpta.DeudaLaboral = entity.DeudaLaboral;
                    rpta.DeudaImpagable = entity.DeudaImpagable;
                    rpta.DeudaProtestos = entity.DeudaProtestos;
                    rpta.DeudaSbs = entity.DeudaSbs;
                    rpta.CuentasTarjetas = entity.CuentasTarjetas;
                    rpta.ReporteNegativo = entity.ReporteNegativo;
                    rpta.TipoActividad = entity.TipoActividad;
                    rpta.FechaInicioActividad = entity.FechaInicioActividad;
                    rpta.DeudaDirecta = entity.DeudaDirecta;
                    rpta.DeudaIndirecta = entity.DeudaIndirecta;
                    rpta.DeudaCastigada = entity.DeudaCastigada;
                    rpta.LineaCreditoNoUtilizada = entity.LineaCreditoNoUtilizada;
                    rpta.TotalRiesgo = entity.TotalRiesgo;
                    rpta.PeorCalificacion = entity.PeorCalificacion;
                    rpta.PorcentajeCalificacionNormal = entity.PorcentajeCalificacionNormal;
                    rpta.Estado = true;
                    rpta.FechaCreacion = DateTime.Now;
                    rpta.FechaModificacion = DateTime.Now;
                    rpta.UsuarioCreacion = usuario;
                    rpta.UsuarioModificacion = usuario;

                    DniRuc.Add(rpta);
                    
                }

                foreach (var entity in sdt_bsgrupo_repsbs_dtos)
                {                    
                    SentinelSdtRepSbsitemBO rpta = new SentinelSdtRepSbsitemBO();
                    rpta.TipoDoc = entity.TipoDoc;
                    rpta.NroDoc = entity.NroDoc;
                    rpta.NombreRazonSocial = entity.NombreRazonSocial;
                    rpta.Calificacion = entity.Calificacion;
                    rpta.MontoDeuda = entity.MontoDeuda;
                    rpta.DiasVencidos = entity.DiasVencidos;
                    rpta.FechaReporte = entity.FechaReporte;
                    rpta.Estado = true;
                    rpta.FechaCreacion = DateTime.Now;
                    rpta.FechaModificacion = DateTime.Now;
                    rpta.UsuarioCreacion = usuario;
                    rpta.UsuarioModificacion = usuario;

                    Deuda.Add(rpta);
                }

                foreach (var entity in sdt_bsgrupo_lincre_dtos)
                {                    
                    SentinelSdtLincreItemBO rpta = new SentinelSdtLincreItemBO();
                    rpta.TipoDocumento = entity.TipoDocumento;
                    rpta.NumeroDocumento = entity.NumeroDocumento;
                    rpta.TipoCuenta = entity.TipoCuenta;
                    rpta.LineaCredito = entity.LineaCredito;
                    rpta.LineaCreditoNoUtil = entity.LineaCreditoNoUtil;
                    rpta.LineaUtil = entity.LineaUtil;
                    rpta.CnsEntNomRazLn = entity.CnsEntNomRazLn;
                    rpta.Estado = true;
                    rpta.FechaCreacion = DateTime.Now;
                    rpta.FechaModificacion = DateTime.Now;
                    rpta.UsuarioCreacion = usuario;
                    rpta.UsuarioModificacion = usuario;

                    LineaCredito.Add(rpta);
                }

                foreach (var entity in sdt_bsgrupo_resven_dtos)
                {                    
                    SentinelSdtResVenItemBO rpta = new SentinelSdtResVenItemBO();
                    rpta.TipoDocumento = entity.TipoDocumento;
                    rpta.NroDocumento = entity.NroDocumento;
                    rpta.CantidadDocs = entity.CantidadDocs;
                    rpta.Fuente = entity.Fuente;
                    rpta.Entidad = entity.Entidad;
                    rpta.Monto = entity.Monto;
                    rpta.Cantidad = entity.Cantidad;
                    rpta.DiasVencidos = entity.DiasVencidos;
                    rpta.Estado = true;
                    rpta.FechaCreacion = DateTime.Now;
                    rpta.FechaModificacion = DateTime.Now;
                    rpta.UsuarioCreacion = usuario;
                    rpta.UsuarioModificacion = usuario;

                    DatosVencidas.Add(rpta);
                }

                if (sdt_bsgrupo_infgen_dto != null)
                {
                    DatosGenerales = new SentinelSdtInfGenBO();
                    DatosGenerales.Dni = sdt_bsgrupo_infgen_dto.Dni;
                    DatosGenerales.FechaNacimiento = sdt_bsgrupo_infgen_dto.FechaNacimiento;
                    DatosGenerales.Sexo = sdt_bsgrupo_infgen_dto.Sexo;
                    DatosGenerales.Digito = sdt_bsgrupo_infgen_dto.Digito;
                    DatosGenerales.DigitoAnterior = sdt_bsgrupo_infgen_dto.DigitoAnterior;
                    DatosGenerales.Ruc = sdt_bsgrupo_infgen_dto.Ruc;
                    DatosGenerales.RazonSocial = sdt_bsgrupo_infgen_dto.RazonSocial;
                    DatosGenerales.NombreComercial = sdt_bsgrupo_infgen_dto.NombreComercial;
                    DatosGenerales.FechaBaja = sdt_bsgrupo_infgen_dto.FechaBaja;
                    //rpta.FechaBaja = entity.FechaBaja == "" ? "" : Convert.ToDateTime(entity.FechaBaja);
                    DatosGenerales.TipoContribuyente = sdt_bsgrupo_infgen_dto.TipoContribuyente;
                    DatosGenerales.CodigoTipoContribuyente = sdt_bsgrupo_infgen_dto.CodigoTipoContribuyente;
                    DatosGenerales.EstadoContribuyente = sdt_bsgrupo_infgen_dto.EstadoContribuyente;
                    DatosGenerales.CodigoEstadoContribuyente = sdt_bsgrupo_infgen_dto.CodigoEstadoContribuyente;
                    DatosGenerales.CondicionContribuyente = sdt_bsgrupo_infgen_dto.CondicionContribuyente;
                    DatosGenerales.CodigoCondicionContribuyente = sdt_bsgrupo_infgen_dto.CodigoCondicionContribuyente;
                    DatosGenerales.ActividadEconomica = sdt_bsgrupo_infgen_dto.ActividadEconomica;
                    DatosGenerales.Ciiu = sdt_bsgrupo_infgen_dto.Ciiu;
                    DatosGenerales.ActividadEconomica2 = sdt_bsgrupo_infgen_dto.ActividadEconomica2;
                    DatosGenerales.Ciiu2 = sdt_bsgrupo_infgen_dto.Ciiu2;
                    DatosGenerales.ActividadEconomica3 = sdt_bsgrupo_infgen_dto.ActividadEconomica3;
                    DatosGenerales.Ciiu3 = sdt_bsgrupo_infgen_dto.Ciiu3;
                    DatosGenerales.FechaActividad = sdt_bsgrupo_infgen_dto.FechaActividad;
                    DatosGenerales.Direccion = sdt_bsgrupo_infgen_dto.Direccion;
                    DatosGenerales.Referencia = sdt_bsgrupo_infgen_dto.Referencia;
                    DatosGenerales.Departamento = sdt_bsgrupo_infgen_dto.Departamento;
                    DatosGenerales.Provincia = sdt_bsgrupo_infgen_dto.Provincia;
                    DatosGenerales.Distrito = sdt_bsgrupo_infgen_dto.Distrito;
                    DatosGenerales.Ubigeo = sdt_bsgrupo_infgen_dto.Ubigeo;
                    DatosGenerales.FechaConstitucion = sdt_bsgrupo_infgen_dto.FechaConstitucion;
                    //rpta.FechaConstitucion = Convert.ToDateTime(entity.FConstitucion);
                    DatosGenerales.ActvidadComercioExterior = sdt_bsgrupo_infgen_dto.ActvidadComercioExterior;
                    DatosGenerales.CodigoActividadComerExt = sdt_bsgrupo_infgen_dto.CodigoActividadComerExt;
                    DatosGenerales.CodigoDependencia = sdt_bsgrupo_infgen_dto.CodigoDependencia;
                    DatosGenerales.Dependencia = sdt_bsgrupo_infgen_dto.Dependencia;
                    DatosGenerales.Folio = sdt_bsgrupo_infgen_dto.Folio;
                    DatosGenerales.Asiento = sdt_bsgrupo_infgen_dto.Asiento;
                    DatosGenerales.Tomo = sdt_bsgrupo_infgen_dto.Tomo;
                    DatosGenerales.PartidaReg = sdt_bsgrupo_infgen_dto.PartidaReg;
                    DatosGenerales.Patron = sdt_bsgrupo_infgen_dto.Patron;
                    DatosGenerales.Estado = true;
                    DatosGenerales.FechaCreacion = DateTime.Now;
                    DatosGenerales.FechaModificacion = DateTime.Now;
                    DatosGenerales.UsuarioCreacion = usuario;
                    DatosGenerales.UsuarioModificacion = usuario;
                }
                else
                {
                    DatosGenerales = null;
                }

                foreach (var entity in sdt_bsgrupo_repleg_dtos)
                {                    
                    SentinelRepLegItemBO rpta = new SentinelRepLegItemBO();
                    rpta.TipoDocumento = entity.TipoDocumento;
                    rpta.NumeroDocumento = entity.NumeroDocumento;
                    rpta.Nombres = entity.Nombres;
                    rpta.ApellidoPaterno = entity.ApellidoPaterno;
                    rpta.ApellidoMaterno = entity.ApellidoMaterno;
                    rpta.RazonSocial = entity.RazonSocial;
                    rpta.Cargo = entity.Cargo;
                    rpta.SemaforoActual = entity.SemaforoActual;
                    rpta.Estado = true;
                    rpta.FechaCreacion = DateTime.Now;
                    rpta.FechaModificacion = DateTime.Now;
                    rpta.UsuarioCreacion = usuario;
                    rpta.UsuarioModificacion = usuario;

                    Cargo.Add(rpta);
                }

                foreach (var entity in sdt_bsgrupo_poshis_dtos)
                {                    
                    SentinelSdtPoshisItemBO rpta = new SentinelSdtPoshisItemBO();
                    rpta.TipoDocumento = entity.TipoDocumento;
                    rpta.NumeroDocumento = entity.NumeroDocumento;
                    rpta.FechaProceso = entity.FechaProceso;
                    rpta.SemanaActual = entity.SemanaActual;
                    rpta.DescripcionSemaforo = entity.DescripcionSemaforo;
                    rpta.Score = entity.Score;
                    rpta.CodigoVariacion = entity.CodigoVariacion;
                    rpta.DescripcionVariacion = entity.DescripcionVariacion;
                    rpta.NumeroEntidades = entity.NumeroEntidades;
                    rpta.DeudaTotal = entity.DeudaTotal;
                    rpta.PorcentajeCalificacion = entity.PorcentajeCalificacion;
                    rpta.PeorCalificacion = entity.PeorCalificacion;
                    rpta.PeroCalificacionDescripcion = entity.PeroCalificacionDescripcion;
                    rpta.MontoSbs = entity.MontoSbs;
                    rpta.ProgresoRegistro = entity.ProgresoRegistro;
                    rpta.DocImpuesto = entity.DocImpuesto;
                    rpta.DeudaTributaria = entity.DeudaTributaria;
                    rpta.Afp = entity.Afp;
                    rpta.TarjetaCredito = entity.TarjetaCredito;
                    rpta.CuentaCorriente = entity.CuentaCorriente;
                    rpta.ReporteNegativo = entity.ReporteNegativo;
                    rpta.DeudaDirecta = entity.DeudaDirecta;
                    rpta.DeudaIndirecta = entity.DeudaIndirecta;
                    rpta.LineaCreditoNoUtilizada = entity.LineaCreditoNoUtilizada;
                    rpta.DeudaCastigada = entity.DeudaCastigada;
                    rpta.Estado = true;
                    rpta.FechaCreacion = DateTime.Now;
                    rpta.FechaModificacion = DateTime.Now;
                    rpta.UsuarioCreacion = usuario;
                    rpta.UsuarioModificacion = usuario;

                    PosicionHistoria.Add(rpta);
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }

        /// <summary>
        /// Obtiene El Sueldo Promedio de un Contacto
        /// </summary>
        /// <param name="idEmpresa"></param>
        /// <param name="dni"></param>
        /// <param name="idCargo"></param>
        /// <param name="idIndustria"></param>
        /// <returns></returns>
        public SueldosDescripcionDTO GetPromedioSueldo(int? idEmpresa, string dni, int? idCargo, int? idIndustria)
        {
            SueldosDescripcionDTO promedio = new SueldosDescripcionDTO();

            promedio.valor = null;
            promedio.descripcion = "SD";
            //// si tiene DNI 
            var sentinelDNI = _repSentinelSueldoIndividual.ObtenerSentinelSueldoPromedio(dni);
            if (sentinelDNI != null)
            {//si existe en los sueldos individuales
                int? pro = (int?)sentinelDNI.SePromedio; 

                promedio.valor = pro;
                promedio.descripcion = "SP";
                return promedio;
            }
            /////////////////////////////// si no tiene DNI se aplica la logica//////////////////////////////////////////////////////////////////////////////
            if (idIndustria == 1)
            {
                var proProm = _repSentinelSueldoPorIndustriaDataTotal.ObtenerValorDeSueldoPorInsustria(idCargo.Value, idIndustria.Value);
                if (proProm != null)
                {
                    promedio.valor = proProm.Valor;
                    promedio.descripcion = "PM";
                }
                return promedio;//PM
            }

            var entidad = _repSentinelSueldoPorIndustria.ObtenerSueldoPorIndustria(idCargo.Value, idIndustria.Value);

            if (entidad != null)
            {
                switch (entidad.Tipo)
                {
                    case 1://solo mercado
                        var promMercado = _repSentinelSueldoPorIndustriaDataTotal.ObtenerValorDeSueldoPorInsustria(idCargo.Value, idIndustria.Value);
                        if (promMercado != null)
                        {
                            //promedio = promMercado.valor;//SM
                            promedio.valor = promMercado.Valor;
                            promedio.descripcion = "SM";

                            //return promedio;
                        }
                        break;
                    case 2://solo industria

                        var solomercado = _repSentinelSueldoPorIndustriaDataTotal.ObtenerValorDeSueldoPorInsustria(idCargo.Value, idIndustria.Value);
                        if (solomercado != null)
                        {
                            // promedio = solomercado.valor;//SI
                            promedio.valor = solomercado.Valor;
                            promedio.descripcion = "SI";
                        }
                        break;
                    case 3:// MERCADO por categoria
                        if (idEmpresa == null)
                        {
                            //si no tiene empresa se imprime promedio del mercado
                            var promIndustria = _repSentinelSueldoPorIndustriaDataTotal.ObtenerValorDeSueldoPorInsustria(idCargo.Value, idIndustria.Value);
                            if (promIndustria != null)
                            {
                                // promedio = promIndustria.valor;//PM
                                promedio.valor = promIndustria.Valor;
                                promedio.descripcion = "PM";
                            }
                        }
                        else
                        {//tiene empresa imprime el mercado de acuerdo a su categoria
                            var empresa = _repEmpresa.ObtenerTamanioEmpresa(idEmpresa.Value);
                            if (empresa != null && empresa.IdTamanio != null)
                            {
                                var industria = _repSentinelSueldoPorIndustriaDataDinamico.ObtenerValorSueldoIndustria(idCargo.Value, idIndustria.Value, empresa.IdTamanio == null ? 0 : empresa.IdTamanio.Value);
                                if (industria != null)
                                {
                                    // promedio = industria.valor; //MC
                                    promedio.valor = industria.Valor;
                                    promedio.descripcion = "MC";
                                }
                            }
                            else
                            { //si no tiene categoria ,debe imprimir promedio de mercado
                                var promIndustria = _repSentinelSueldoPorIndustriaDataTotal.ObtenerValorDeSueldoPorInsustria(idCargo.Value, idIndustria.Value);
                                if (promIndustria != null)
                                {
                                    // promedio = promIndustria.valor;//PM
                                    promedio.valor = promIndustria.Valor;
                                    promedio.descripcion = "PM";
                                }
                            }
                        }

                        break;

                    case 4://INDUSTRIA por categoria
                        if (idEmpresa == null)
                        {//si no tiene empresa se imprime promedio de la industria
                            var industria = _repSentinelSueldoPorIndustriaDataDinamico.ObtenerValorSueldoIndustria(idCargo.Value, idIndustria.Value, idIndustria.Value);
                            if (industria != null)
                            {
                                //promedio = industria.valor;//PI
                                promedio.valor = industria.Valor;
                                promedio.descripcion = "PI";
                            }
                        }
                        else
                        {//tiene empresa imprime de acuerdo a su categoria de la industria
                            var empresa = _repEmpresa.ObtenerTamanioEmpresa(idEmpresa.Value);
                            if (empresa != null && empresa.IdTamanio != null)
                            {
                                var industria = _repSentinelSueldoPorIndustriaDataDinamico.ObtenerValorSueldoIndustria(idCargo.Value, idIndustria.Value, empresa.IdTamanio==null ? 0 : empresa.IdTamanio.Value);
                                if (industria != null)
                                {
                                    //promedio = industria.valor; //IC
                                    promedio.valor = industria.Valor;
                                    promedio.descripcion = "IC";
                                }
                            }
                            else
                            { //debe imprimir promedio de la industria
                                var industria = _repSentinelSueldoPorIndustriaDataDinamico.ObtenerValorSueldoIndustria(idCargo.Value, idIndustria.Value, empresa.IdTamanio == null ? 0 : empresa.IdTamanio.Value);
                                if (industria != null)
                                {
                                    //promedio = industria.valor; //PI
                                    promedio.valor = industria.Valor;
                                    promedio.descripcion = "PI";
                                }
                            }
                        }
                        break;
                }
            }
            else
            {//caso que solo tenga cargo no industria 
                var promMercado = _repSentinelSueldoPorIndustriaDataTotal.ObtenerValorDeSueldoPorInsustria(idCargo.Value, idIndustria.Value);
                if (promMercado != null)
                {
                    //promedio = promMercado.valor;//PM
                    promedio.valor = promMercado.Valor;
                    promedio.descripcion = "PM";
                }
            }


            return promedio;
        }
    }
}
