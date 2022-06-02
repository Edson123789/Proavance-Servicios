using BSI.Integra.Aplicacion.Comercial.BO;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Finanzas.BO;
using BSI.Integra.Aplicacion.Finanzas.Repositorio;
using BSI.Integra.Aplicacion.GestionPersonas.Repositorio;
using BSI.Integra.Aplicacion.Operaciones.BO;
using BSI.Integra.Aplicacion.Operaciones.Repositorio;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Aplicacion.Transversal.Helper;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using MatriculaDetalleBO = BSI.Integra.Aplicacion.Finanzas.BO.MatriculaDetalleBO;

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/MatriculaManual")]
    [ApiController]
    public class MatriculaManualController : ControllerBase
    {
        [Route("[action]")]
        [HttpPost]
        public ActionResult ObtenerCoordinadorPorApellidos([FromBody] Dictionary<string, string> Valor)
        {
            try
            {
                if (Valor != null && Valor.Count > 0)
                {
                    PersonalRepositorio _repPersonal = new PersonalRepositorio();
                    if (Valor["filtro"] == "ninguno")
                    {
                        return Ok(_repPersonal.GetBy(x => x.Id == 126, x => new { x.Id, NombreCompleto = x.Nombres + " " + x.Apellidos }));
                    }
                    else
                    {
                        return Ok(_repPersonal.GetBy(x => x.TipoPersonal.Contains("coor") && x.Apellidos.Contains(Valor["filtro"]), x => new { x.Id, NombreCompleto = x.Nombres + " " + x.Apellidos }));
                    }
                }
                else
                {
                    return Ok();
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult ObtenerAsesorPorApellidos([FromBody] Dictionary<string, string> Valor)
        {
            try
            {
                if (Valor != null & Valor.Count > 0)
                {
                    PersonalRepositorio _repPersonal = new PersonalRepositorio();
                    var personal = _repPersonal.GetBy(x => x.Rol == "Ventas" && x.Apellidos.Contains(Valor["filtro"]), x => new { x.Id, x.Nombres, x.Apellidos, x.Email, NombreCompleto = x.Nombres + " " + x.Apellidos });
                    return Ok(personal);
                }
                else
                {
                    return Ok();
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult ObtenerProgramaEspecificoPorCentroCosto([FromBody] Dictionary<string, string> Valor)
        {
            try
            {
                if (Valor != null && Valor.Count > 0)
                {
                    PespecificoRepositorio _repPespecifico = new PespecificoRepositorio();
                    return Ok(_repPespecifico.ObtenerPEspecificoPorCentroCosto(Valor["filtro"]));
                }
                else
                {
                    return Ok();
                }
            }
            catch (Exception e)
            {
                return BadRequest(ModelState);
            }
        }

        [Route("[action]/{IdPEspecifico}")]
        [HttpGet]
        public ActionResult ObtenerDatosDelCentrodeCosto(int IdPEspecifico)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                CentroCostoRepositorio _repCentroCosto = new CentroCostoRepositorio();
                var listaRpta = _repCentroCosto.ObtenerDatosCentroCostos(IdPEspecifico);

                PespecificoRepositorio _repPEspecifico = new PespecificoRepositorio();
                var modalidad = _repPEspecifico.GetBy(x => x.Id == IdPEspecifico, x => new { x.Tipo }).FirstOrDefault();
                CriterioDocRepositorio _repCriterioDoc = new CriterioDocRepositorio();
                List<CriterioDocDTO> listaDocumentos = new List<CriterioDocDTO>();

                if (modalidad.Tipo == "Presencial")
                {
                    var tempData = _repCriterioDoc.GetBy(x => x.ModalidadPresencial == true, x => new { IdCriterioDocs = x.Id, NombreDocumento = x.Nombre });
                    foreach (var item in tempData)
                    {
                        var temp = new CriterioDocDTO()
                        {
                            IdCriterioDocs = item.IdCriterioDocs,
                            NombreDocumento = item.NombreDocumento
                        };
                        listaDocumentos.Add(temp);
                    }
                }
                if (modalidad.Tipo == "Online Asincronica")
                {
                    var tempData = _repCriterioDoc.GetBy(x => x.ModalidadAonline == true, x => new { IdCriterioDocs = x.Id, NombreDocumento = x.Nombre });
                    foreach (var item in tempData)
                    {
                        var temp = new CriterioDocDTO()
                        {
                            IdCriterioDocs = item.IdCriterioDocs,
                            NombreDocumento = item.NombreDocumento
                        };
                        listaDocumentos.Add(temp);
                    }
                }
                if (modalidad.Tipo == "Online Sincronica")
                {
                    var tempData = _repCriterioDoc.GetBy(x => x.ModalidadOnline == true, x => new { IdCriterioDocs = x.Id, NombreDocumento = x.Nombre });
                    foreach (var item in tempData)
                    {
                        var temp = new CriterioDocDTO()
                        {
                            IdCriterioDocs = item.IdCriterioDocs,
                            NombreDocumento = item.NombreDocumento
                        };
                        listaDocumentos.Add(temp);
                    }
                }
                var listadoDocumentos = listaDocumentos;

                PespecificoRepositorio _repCursosCentroCosto = new PespecificoRepositorio();
                var listaCursos = _repCursosCentroCosto.ObtenerCursosCentroCosto(IdPEspecifico);

                return Ok(new { listaRpta, listaDocumentos, listaCursos });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]/{TipoCambio}")]
        [HttpGet]
        public ActionResult ObtenerTipoCambioFecha(int TipoCambio)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                TipoCambioRepositorio _repTipoCambio = new TipoCambioRepositorio();
                return Ok(_repTipoCambio.ObtenerTipoCambio(TipoCambio));
            }
            catch (Exception e)
            {
                return BadRequest(ModelState);
            }
        }

        [Route("[action]/{CodigoMatricula}")]
        [HttpGet]
        public ActionResult ObtenerCronogramaPagoPorCodigoMatricula(string CodigoMatricula)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                MatriculaCabeceraRepositorio _repMatriculaCabecera = new MatriculaCabeceraRepositorio();
                var idMatriculaCabeceraTemp = _repMatriculaCabecera.GetBy(x => x.CodigoMatricula == CodigoMatricula, x => new { x.Id }).FirstOrDefault();

                CronogramaPagoDetalleRepositorio _repCronogramaPagoDetalle = new CronogramaPagoDetalleRepositorio();
                return Ok(_repCronogramaPagoDetalle.GetBy(x => x.IdMatriculaCabecera == idMatriculaCabeceraTemp.Id, x => new { x.Id, x.IdMatriculaCabecera, x.NroCuota, x.FechaVencimiento, x.TotalPagar, x.Cuota, x.Saldo, x.TipoCuota, x.Moneda }));
            }
            catch (Exception e)
            {
                return BadRequest(ModelState);
            }
        }

        [Route("[action]/{CodigoMatricula}/{IdPEspecifico}")]
        [HttpGet]
        public ActionResult CargarMatricula(string CodigoMatricula, int IdPEspecifico)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                MatriculaCabeceraRepositorio _repMatriculaCabecera = new MatriculaCabeceraRepositorio();
                var listaRpta = _repMatriculaCabecera.ObtenerDatosMatriculaManual(CodigoMatricula);

                CentroCostoRepositorio _repCentroCosto = new CentroCostoRepositorio();
                var listaDatosCentroCosto = _repCentroCosto.ObtenerDatosCentroCostos(IdPEspecifico);

                PespecificoRepositorio _repCursosCentroCosto = new PespecificoRepositorio();
                var listaCursos = _repCursosCentroCosto.ObtenerCursosCentroCosto(IdPEspecifico);

                return Ok(new { listaRpta, listaDatosCentroCosto, listaCursos });
            }
            catch (Exception e)
            {
                return BadRequest(ModelState);
            }
        }

        [Route("[action]/{CodigoMatricula}")]
        [HttpGet]
        public ActionResult ObtenerCronogramaBusqueda(string CodigoMatricula)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                MatriculaCabeceraRepositorio _repMatriculaCabecera = new MatriculaCabeceraRepositorio();
                CronogramaPagoDetalleOriginalRepositorio _repCronogramaPagoDetalleOriginal = new CronogramaPagoDetalleOriginalRepositorio();
                CronogramaPagoDetalleFinalRepositorio _repCronogramaPagoDetalleFinal = new CronogramaPagoDetalleFinalRepositorio();
                //obtenemos el idMatriculaCabecera
                var matriculaCabecera = _repMatriculaCabecera.FirstBy(x => x.CodigoMatricula == CodigoMatricula, x => new { x.Id });
                var existe = _repMatriculaCabecera.GetBy(x => x.Id == matriculaCabecera.Id).Count();
                var count = _repCronogramaPagoDetalleOriginal.GetBy(x => x.IdMatriculaCabecera == matriculaCabecera.Id).Count();
                var count2 = _repCronogramaPagoDetalleFinal.GetBy(x => x.IdMatriculaCabecera == matriculaCabecera.Id).Count();
                int valor;
                if (existe == 1)
                {
                    if (count == 0 && count2 == 0)
                    {
                        valor = _repMatriculaCabecera.FirstBy(x => x.Id == matriculaCabecera.Id, x => new { x.IdPespecifico }).IdPespecifico;
                    }
                    else {
                        valor = 0;
                    }
                }
                else {
                    valor = -1;
                }
                return Ok(valor);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [Route("[action]")]
        [HttpPost]
        public ActionResult GenerarMatriculaCabecera(MatriculaCronogramaDTO MatriculaCronogramaDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                integraDBContext _integraDBContext = new integraDBContext();
                MatriculaCabeceraRepositorio _repMatriculaCabecera = new MatriculaCabeceraRepositorio(_integraDBContext);
                PespecificoRepositorio _repPespecificoRepositorio = new PespecificoRepositorio(_integraDBContext);
                MatriculaDetalleRepositorio _repMatriculaDetalle = new MatriculaDetalleRepositorio(_integraDBContext);
                CronogramaPagoDetalleRepositorio _repCronogramaPagoDetalle = new CronogramaPagoDetalleRepositorio(_integraDBContext);
                ControlDocAlumnoRepositorio _repControlDocAlumno = new ControlDocAlumnoRepositorio(_integraDBContext);
                CronogramaPagoRepositorio _repCronogramaPago = new CronogramaPagoRepositorio(_integraDBContext);
                ControlDocRepositorio _repControlDocumento = new ControlDocRepositorio(_integraDBContext);
				MonedaRepositorio _repMoneda = new MonedaRepositorio(_integraDBContext);
				TipoCambioRepositorio _repTipoCambio = new TipoCambioRepositorio(_integraDBContext);
				TipoCambioColRepositorio _repTipoCambioCol = new TipoCambioColRepositorio(_integraDBContext);
				TipoCambioMonedaRepositorio _repTipoCambioMoneda = new TipoCambioMonedaRepositorio(_integraDBContext);
                PEspecificoMatriculaAlumnoRepositorio _repPEspecificoMatriculaAlumno = new PEspecificoMatriculaAlumnoRepositorio(_integraDBContext);

                PEspecificoMatriculaAlumnoRepositorio pEspecificoMatriculaAlumnoRepositorio = new PEspecificoMatriculaAlumnoRepositorio(_integraDBContext);
                List<PespecificoPadrePespecificoHijoDTO> listaPEspecificoPadrePespecificoHijo = new List<PespecificoPadrePespecificoHijoDTO>();

				using (TransactionScope scope = new TransactionScope())
                {

                    var listRpta = "";
                    bool CadaNDias = false;
                    int DiaPago = MatriculaCronogramaDTO.FechaInicioPago.Day;
                    var existe = _repMatriculaCabecera.ExisteMatriculaCabecera(MatriculaCronogramaDTO.IdAlumno, MatriculaCronogramaDTO.IdPespecifico);
                    var codigoMatricula = string.Empty;
                    int idMatriculaCabecera = 0;

                    if (existe)
                    {
                        codigoMatricula = "0";
                    }
                    else
                    {
                        var codigoBanco = _repPespecificoRepositorio.FirstBy(x => x.Id == MatriculaCronogramaDTO.IdPespecifico, x => new { x.CodigoBanco }).CodigoBanco;

                        MatriculaCabeceraBO matriculaCabeceraBO = new MatriculaCabeceraBO()
                        {
                            CodigoMatricula = string.Concat(MatriculaCronogramaDTO.IdAlumno, codigoBanco),
                            IdAlumno = MatriculaCronogramaDTO.IdAlumno,
                            IdPespecifico = MatriculaCronogramaDTO.IdPespecifico,
                            UsuarioCreacion = MatriculaCronogramaDTO.NombreUsuario,
                            FechaCreacion = DateTime.Now,
                            UsuarioModificacion = MatriculaCronogramaDTO.NombreUsuario,
                            FechaModificacion = DateTime.Now,
                            Estado = true,
                            IdEstadoPagoMatricula = ValorEstatico.IdEstadoPagoMatriculaPorMatricular,
                            EstadoMatricula = "pormatricular",
                            FechaMatricula = DateTime.Now,
                            IdCoordinador = MatriculaCronogramaDTO.IdCoordinador,
                            IdAsesor = MatriculaCronogramaDTO.IdAsesor,
                            IdEstado_matricula = 1,
                            UsuarioCoordinadorAcademico = "0",
                            ObservacionGeneralOperaciones = "",
                            IdPaquete=0
                        };
                        _repMatriculaCabecera.Insert(matriculaCabeceraBO);
                        codigoMatricula = matriculaCabeceraBO.CodigoMatricula;
                        idMatriculaCabecera = matriculaCabeceraBO.Id;

                        //Verificar si es hijo o si es padre
                        listaPEspecificoPadrePespecificoHijo = pEspecificoMatriculaAlumnoRepositorio.ListaPespecificoPadrePespecificoHijo(MatriculaCronogramaDTO.IdPespecifico);

                        //IdUsuarioMoodle
                        var IdUsuarioMoodle = pEspecificoMatriculaAlumnoRepositorio.IdUsuarioMoodle(MatriculaCronogramaDTO.IdAlumno);
                        //var IdUsuarioMoodle = Int32.Parse(IdUsuario);

                        
                        //var idmatriculacabecera = _repMatriculaCabecera.FirstBy(x => x.CodigoMatricula == codigoMatricula);
                        //
                        if (listaPEspecificoPadrePespecificoHijo.Count > 0)
                        {

                            foreach (var item in listaPEspecificoPadrePespecificoHijo)
                            {
                                //IdCursoMoodle
                                var IdCursoMoodle = pEspecificoMatriculaAlumnoRepositorio.IdCursoMoodle(item.PEspecificoHijoId);

                                PEspecificoMatriculaAlumnoBO matriculaBO = new PEspecificoMatriculaAlumnoBO();
                                matriculaBO.IdMatriculaCabecera = idMatriculaCabecera;
                                matriculaBO.IdPespecifico = item.PEspecificoHijoId;
                                matriculaBO.IdPespecificoTipoMatricula = 1;
                                matriculaBO.IdCursoMoodle = IdCursoMoodle;
                                matriculaBO.IdUsuarioMoodle = IdUsuarioMoodle;
                                matriculaBO.Grupo = 1;
                                matriculaBO.AplicaNuevaAulaVirtual = _repPEspecificoMatriculaAlumno.ExisteNuevaAulaVirtual(item.PEspecificoHijoId);
                                matriculaBO.UsuarioCreacion = MatriculaCronogramaDTO.NombreUsuario;
                                matriculaBO.FechaCreacion = DateTime.Now;
                                matriculaBO.UsuarioModificacion = MatriculaCronogramaDTO.NombreUsuario;
                                matriculaBO.FechaModificacion = DateTime.Now;
                                matriculaBO.Estado = true;

                                pEspecificoMatriculaAlumnoRepositorio.Insert(matriculaBO);
                            }
                            
                        }
                        else
                        {
                            //IdCursoMoodle
                            var IdCursoMoodle = pEspecificoMatriculaAlumnoRepositorio.IdCursoMoodle(MatriculaCronogramaDTO.IdPespecifico);

                            PEspecificoMatriculaAlumnoBO matriculaBO = new PEspecificoMatriculaAlumnoBO();
                            matriculaBO.IdMatriculaCabecera = idMatriculaCabecera;
                            matriculaBO.IdPespecifico = MatriculaCronogramaDTO.IdPespecifico;
                            matriculaBO.IdPespecificoTipoMatricula = 1;
                            matriculaBO.IdCursoMoodle = IdCursoMoodle;
                            matriculaBO.IdUsuarioMoodle = IdUsuarioMoodle;
                            matriculaBO.Grupo = 1;
                            matriculaBO.AplicaNuevaAulaVirtual = _repPEspecificoMatriculaAlumno.ExisteNuevaAulaVirtual(MatriculaCronogramaDTO.IdPespecifico);
                            matriculaBO.UsuarioCreacion = MatriculaCronogramaDTO.NombreUsuario;
                            matriculaBO.FechaCreacion = DateTime.Now;
                            matriculaBO.UsuarioModificacion = MatriculaCronogramaDTO.NombreUsuario;
                            matriculaBO.FechaModificacion = DateTime.Now;
                            matriculaBO.Estado = true;
                            pEspecificoMatriculaAlumnoRepositorio.Insert(matriculaBO);
                        }
                    }

                    if (codigoMatricula == "0")
                    {
                        listRpta = "0";
                    }
                    else
                    {
                        for (int i = 0; i < MatriculaCronogramaDTO.CursosMatriculados.Length; i++)
                        {
                            MatriculaDetalleBO matriculaDetalleBO = new MatriculaDetalleBO()
                            {
                                IdMatriculaCabecera = idMatriculaCabecera,
                                IdCursoPespecifico = MatriculaCronogramaDTO.CursosMatriculados[i],
                                UsuarioCreacion = MatriculaCronogramaDTO.NombreUsuario,
                                FechaCreacion = DateTime.Now,
                                UsuarioModificacion = MatriculaCronogramaDTO.NombreUsuario,
                                FechaModificacion = DateTime.Now,
                                Estado = true,
                            };
                            _repMatriculaDetalle.Insert(matriculaDetalleBO);
                        }
                        if (MatriculaCronogramaDTO.OpcionPagoNDias == "Ndias")
                        {
                            CadaNDias = true;
                        }

						//Generacion de campos WEB para el portal web
						var monedaFormulario = MatriculaCronogramaDTO.Moneda == "1" ? "soles" : "dolares";
						var moneda = _repMoneda.FirstBy(x => x.NombrePlural.Equals(monedaFormulario));

						var webMoneda = moneda.DigitoFinanzas;
						var tipoCambioSol = _repTipoCambio.GetBy(x => x.Estado == true).OrderByDescending(x => x.FechaCreacion).FirstOrDefault();
						var tipoCambioCol = _repTipoCambioCol.GetBy(x => x.Estado == true).OrderByDescending(x => x.FechaCreacion).FirstOrDefault();
						var tipoCambioGeneral = _repTipoCambioMoneda.GetBy(x => x.IdMoneda == moneda.Id).OrderByDescending(x => x.FechaCreacion).FirstOrDefault();

						double? webTipoCambio = 0.0;
						if (moneda.Id == 20) //Soles
						{
							webTipoCambio = tipoCambioSol.SolesDolares;
						}
						else if(moneda.Id == 10) // Colombiano
						{
							webTipoCambio = tipoCambioCol.PesosDolares;
						}
						else if(moneda.Id == 19)// Otros
						{
							webTipoCambio = null;
						}
						else
						{
							webTipoCambio = tipoCambioGeneral.MonedaAdolar;
						}

						var webTotalPagar = MatriculaCronogramaDTO.TotalPagar;

						CronogramaPagoBO cronogramaPagoBO = new CronogramaPagoBO()
                        {
                            IdMatriculaCabecera = idMatriculaCabecera,
                            IdAlumno = MatriculaCronogramaDTO.IdAlumno,
                            IdPespecifico = MatriculaCronogramaDTO.IdPespecifico,
                            Periodo = MatriculaCronogramaDTO.Periodo,
                            Moneda = MatriculaCronogramaDTO.Moneda == "1" ? "soles" : "dolares",
                            AcuerdoPago = MatriculaCronogramaDTO.AcuerdoPago,
                            TipoCambio = MatriculaCronogramaDTO.TipoCambio,
                            TotalPagar = MatriculaCronogramaDTO.TotalPagar,
                            NroCuotas = MatriculaCronogramaDTO.NroCuotas,
                            FechaIniPago = MatriculaCronogramaDTO.FechaInicioPago,
                            ConCuotaInicial = false,
                            CuotaInicial = 0,
                            CadaNdias = CadaNDias,
                            Ndias = MatriculaCronogramaDTO.Ndias,
                            UsuarioCreacion = MatriculaCronogramaDTO.NombreUsuario,
                            FechaCreacion = DateTime.Now,
                            UsuarioModificacion = MatriculaCronogramaDTO.NombreUsuario,
                            FechaModificacion = DateTime.Now,
                            Estado = true,
							WebMoneda = webMoneda.ToString(),
							WebTipoCambio = webTipoCambio,
							WebTotalPagar = webTotalPagar,
							WebTotalPagarConv = webTotalPagar
							
                        };

                        _repCronogramaPago.Insert(cronogramaPagoBO);

                        double MontoCuota = MatriculaCronogramaDTO.TotalPagar / MatriculaCronogramaDTO.NroCuotas;
                        MontoCuota = Math.Round(MontoCuota, 2);
                        double Saldo = MatriculaCronogramaDTO.TotalPagar;
                        for (int x = 1; x <= MatriculaCronogramaDTO.NroCuotas; x++)
                        {
                            double Total = Saldo;
                            Saldo = Total - MontoCuota;

                            if (CadaNDias == true)
                            {
                                MatriculaCronogramaDTO.FechaInicioPago = MatriculaCronogramaDTO.FechaInicioPago.AddDays(Convert.ToDouble(MatriculaCronogramaDTO.Ndias));
                            }

                            CronogramaPagoDetalleBO cronogramaPagoDetalleBO = new CronogramaPagoDetalleBO()
                            {
                                IdMatriculaCabecera = idMatriculaCabecera,
                                NroCuota = x,
                                FechaVencimiento = MatriculaCronogramaDTO.FechaInicioPago,
                                TotalPagar = Convert.ToDecimal(Total),
                                Cuota = Convert.ToDecimal(MontoCuota),
                                Saldo = Convert.ToDecimal(Saldo),
                                TipoCuota = "CUOTA",
                                Moneda = MatriculaCronogramaDTO.Moneda == "1" ? "soles" : "dolares",
                                UsuarioCreacion = MatriculaCronogramaDTO.NombreUsuario,
                                FechaCreacion = DateTime.Now,
                                UsuarioModificacion = MatriculaCronogramaDTO.NombreUsuario,
                                FechaModificacion = DateTime.Now,
                                Cancelado = false,
                                Estado = true
                            };
                            _repCronogramaPagoDetalle.Insert(cronogramaPagoDetalleBO);
                            if (CadaNDias == false)
                            {
                                try
                                {
                                    DateTime FechaPago = new DateTime(MatriculaCronogramaDTO.FechaInicioPago.Year, MatriculaCronogramaDTO.FechaInicioPago.Month + 1, DiaPago);
                                    MatriculaCronogramaDTO.FechaInicioPago = FechaPago;
                                }
                                catch (ArgumentOutOfRangeException)
                                {
                                    MatriculaCronogramaDTO.FechaInicioPago = MatriculaCronogramaDTO.FechaInicioPago.AddMonths(1);
                                }
                            }
                        }

                        ControlDocAlumnoBO controlDocAlumnoBO = new ControlDocAlumnoBO()
                        {
                            IdMatriculaCabecera = idMatriculaCabecera,
                            IdCriterioCalificacion = 0,
                            QuienEntrego = "",
                            FechaEntregaDocumento = null,
                            Observaciones = "",
                            ComisionableEditable = "Ninguno",
                            MontoComisionable = 0,
                            ObservacionesComisionable = "",
                            PagadoComisionable = 0,
                            UsuarioCreacion = MatriculaCronogramaDTO.NombreUsuario,
                            FechaCreacion = DateTime.Now,
                            UsuarioModificacion = MatriculaCronogramaDTO.NombreUsuario,
                            FechaModificacion = DateTime.Now,
                            Estado = true
                        };
                        _repControlDocAlumno.Insert(controlDocAlumnoBO);
                        for (int x = 0; x < MatriculaCronogramaDTO.ListaIdDocumento.Length; x++)
                        {
                            ControlDocBO controlDocBO = new ControlDocBO()
                            {
                                IdMatriculaCabecera = idMatriculaCabecera,
                                IdCriterioDoc = MatriculaCronogramaDTO.ListaIdDocumento[x],
                                EstadoDocumento = false,
                                UsuarioCreacion = MatriculaCronogramaDTO.NombreUsuario,
                                FechaCreacion = DateTime.Now,
                                UsuarioModificacion = MatriculaCronogramaDTO.NombreUsuario,
                                FechaModificacion = DateTime.Now,
                                Estado = true
                            };
                            _repControlDocumento.Insert(controlDocBO);
                        }
                        listRpta = codigoMatricula;
                    }
                    scope.Complete();
                    return Ok(new { listRpta });
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult ActualizarCronogramaPago(CronogramaPagoAlumnoDTO CronogramaPago)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                integraDBContext _integraDBContext = new integraDBContext();
				CronogramaPagoRepositorio _repCronogramaPago = new CronogramaPagoRepositorio(_integraDBContext);
                CronogramaPagoDetalleRepositorio _repCronogramaPagoDetalle = new CronogramaPagoDetalleRepositorio(_integraDBContext);
                CronogramaPagoDetalleModLogFinalRepositorio _repCronogramaPagoDetalleModLogFinal = new CronogramaPagoDetalleModLogFinalRepositorio(_integraDBContext);
                CronogramaPagoDetalleFinalRepositorio _repCronogramaPagoDetalleFinalRepositorio = new CronogramaPagoDetalleFinalRepositorio(_integraDBContext);
                CronogramaPagoDetalleOriginalRepositorio _repCronogramaPagoDetalleOriginal = new CronogramaPagoDetalleOriginalRepositorio(_integraDBContext);

                if (CronogramaPago.TipoCambio == "") CronogramaPago.TipoCambio = "0";
                if (CronogramaPago.Moneda == "1") CronogramaPago.Moneda = "soles";
                if (CronogramaPago.Moneda == "2") CronogramaPago.Moneda = "dolares";

                var idsCronogramaPagoDetalle = _repCronogramaPagoDetalle.GetBy(x => x.IdMatriculaCabecera == CronogramaPago.ListaCronogramaDetallePago.FirstOrDefault().IdMatriculaCabecera).Select(x => x.Id).ToList();
                var idMatriculaCabecera = CronogramaPago.ListaCronogramaDetallePago.FirstOrDefault().IdMatriculaCabecera;
                var listaRpta = _repCronogramaPagoDetalle.Delete(idsCronogramaPagoDetalle, CronogramaPago.NombreUsuario);

                for (int i = 0; i < CronogramaPago.ListaCronogramaDetallePago.Count; i++)
                {
                    CronogramaPagoDetalleModLogFinalBO cronogramaPagoDetalleModLogFinalBO = new CronogramaPagoDetalleModLogFinalBO()
                    {
                        IdMatriculaCabecera = CronogramaPago.ListaCronogramaDetallePago[i].IdMatriculaCabecera,
                        Fecha = DateTime.Now,
                        NroCuota = CronogramaPago.ListaCronogramaDetallePago[i].NroCuota,
                        NroSubCuota = 1,
                        FechaVencimiento = CronogramaPago.ListaCronogramaDetallePago[i].FechaVencimiento,
                        TotalPagar = CronogramaPago.ListaCronogramaDetallePago[i].TotalPagar,
                        Cuota = CronogramaPago.ListaCronogramaDetallePago[i].Cuota,
                        Mora = 0,
                        MontoPagado = 0,
                        Saldo = CronogramaPago.ListaCronogramaDetallePago[i].Saldo,
                        Cancelado = false,
                        TipoCuota = CronogramaPago.ListaCronogramaDetallePago[i].TipoCuota,
                        Moneda = CronogramaPago.ListaCronogramaDetallePago[i].Moneda,
                        //objCroFinal.TipoCambio = Convert.ToDecimal(tipocambio);
                        FechaPago = null,
                        IdFormaPago = null,
                        FechaPagoBanco = null,
                        Ultimo = false,
                        Observaciones = null,
                        IdDocumentoPago = null,
                        NroDocumento = null,
                        MonedaPago = null,
                        TipoCambio = Convert.ToDecimal(CronogramaPago.TipoCambio),
                        MensajeSistema = null,
                        FechaProcesoPago = null,
                        EstadoPrimerLog = "1",
                        Version = 0,
                        Aprobado = true,
                        Estado2 = true,
                        UsuarioCreacion = CronogramaPago.NombreUsuario,
                        FechaCreacion = DateTime.Now,
                        UsuarioModificacion = CronogramaPago.NombreUsuario,
                        FechaModificacion = DateTime.Now,
                        Estado = true
                    };

                    //var objCroDetalle = new TPW_MontoPagoCronogramasDetalleDTO();
                    //objCroDetalle.numeroCuota = listObjCronograma[i].nroCuota;
                    //objCroDetalle.montoCuota = System.Convert.ToDouble(listObjCronograma[i].cuota);
                    //objCroDetalle.fechapago = listObjCronograma[i].fechaVencimiento;
                    //objCroDetalle.montoCuotaDescuento = System.Convert.ToDouble(listObjCronograma[i].cuota);
                    //objCroDetalle.ispagado = false;
                    //objCroDetalle.es_matricula = true;
                    //objCroDetalle.cuotaDescripcion = "Prueba Crep" + i.ToString();

                    //tCronogramaPagosDetalle_Final
                    CronogramaPagoDetalleFinalBO cronogramaPagoDetalleFinalBO = new CronogramaPagoDetalleFinalBO()
                    {

                        //var objCroFinal = new tCronogramaPagosDetalle_FinalDTO();
                        //Id = "00000000-0000-0000-0000-000000000000";
                        IdMatriculaCabecera = CronogramaPago.ListaCronogramaDetallePago[i].IdMatriculaCabecera,
                        NroCuota = CronogramaPago.ListaCronogramaDetallePago[i].NroCuota,
                        NroSubCuota = 1,
                        FechaVencimiento = CronogramaPago.ListaCronogramaDetallePago[i].FechaVencimiento,
                        TotalPagar = CronogramaPago.ListaCronogramaDetallePago[i].TotalPagar,
                        Cuota = CronogramaPago.ListaCronogramaDetallePago[i].Cuota,
                        Saldo = CronogramaPago.ListaCronogramaDetallePago[i].Saldo,
                        MontoPagado = 0,
                        Cancelado = Convert.ToBoolean(0),
                        TipoCuota = CronogramaPago.ListaCronogramaDetallePago[i].TipoCuota,
                        Moneda = CronogramaPago.ListaCronogramaDetallePago[i].Moneda,
                        Mora = 0,
                        //objCroFinal.TipoCambio = Convert.ToDecimal(tipocambio);
                        Version = 0,
                        Enviado=false,
                        Aprobado=true,                        
                        UsuarioCreacion = CronogramaPago.NombreUsuario,
                        FechaCreacion = DateTime.Now,
                        UsuarioModificacion = CronogramaPago.NombreUsuario,
                        FechaModificacion = DateTime.Now,
                        Estado = true
                    };

                    //cronograma.Add(objCroDetalle);

                    //var crod = _tcrm_CentroCostoService.InsertCroPagDetalle(listObjCronograma[i].matId, listObjCronograma[i].nroCuota, listObjCronograma[i].fechaVencimiento, listObjCronograma[i].totalPagar, listObjCronograma[i].cuota, listObjCronograma[i].saldo, listObjCronograma[i].tipocuota, listObjCronograma[i].moneda, User.Identity.Name);

                    CronogramaPagoDetalleBO cronogramaPagoDetalleBO = new CronogramaPagoDetalleBO()
                    {
                        IdMatriculaCabecera = CronogramaPago.ListaCronogramaDetallePago[i].IdMatriculaCabecera,
                        NroCuota = CronogramaPago.ListaCronogramaDetallePago[i].NroCuota,
                        FechaVencimiento = CronogramaPago.ListaCronogramaDetallePago[i].FechaVencimiento,
                        TotalPagar = CronogramaPago.ListaCronogramaDetallePago[i].TotalPagar,
                        Cuota = CronogramaPago.ListaCronogramaDetallePago[i].Cuota,
                        Saldo = CronogramaPago.ListaCronogramaDetallePago[i].Saldo,
                        TipoCuota = CronogramaPago.ListaCronogramaDetallePago[i].TipoCuota,
                        Moneda = CronogramaPago.ListaCronogramaDetallePago[i].Moneda,
                        Cancelado=false,
                        UsuarioCreacion = CronogramaPago.NombreUsuario,
                        FechaCreacion = DateTime.Now,
                        UsuarioModificacion = CronogramaPago.NombreUsuario,
                        FechaModificacion = DateTime.Now,
                        Estado = true
                    };
                    _repCronogramaPagoDetalle.Insert(cronogramaPagoDetalleBO);

                    //var original = _tcrm_CentroCostoService.IngresarCronogramaDetOriginal(listObjCronograma[i].matId, listObjCronograma[i].nroCuota, 1, 
                    //listObjCronograma[i].fechaVencimiento, listObjCronograma[i].totalPagar, listObjCronograma[i].cuota,
                    //listObjCronograma[i].saldo, false, listObjCronograma[i].tipocuota, (moneda == "1" ? "soles" : "dolares"), Convert.ToDecimal(tipocambio));

                    var cantidad = _repCronogramaPagoDetalleOriginal.GetBy(x=>x.IdMatriculaCabecera == CronogramaPago.ListaCronogramaDetallePago[i].IdMatriculaCabecera && x.NroCuota == CronogramaPago.ListaCronogramaDetallePago[i].NroCuota).Count();
                    if (cantidad == 0)
                    {
                        CronogramaPagoDetalleOriginalBO cronogramaPagoDetalleOriginalBO = new CronogramaPagoDetalleOriginalBO()
                        {
                            IdMatriculaCabecera = CronogramaPago.ListaCronogramaDetallePago[i].IdMatriculaCabecera,
                            NroCuota = CronogramaPago.ListaCronogramaDetallePago[i].NroCuota,
                            NroSubCuota = 1,
                            FechaVencimiento = CronogramaPago.ListaCronogramaDetallePago[i].FechaVencimiento,
                            TotalPagar = CronogramaPago.ListaCronogramaDetallePago[i].TotalPagar,
                            Cuota = CronogramaPago.ListaCronogramaDetallePago[i].Cuota,
                            Saldo = CronogramaPago.ListaCronogramaDetallePago[i].Saldo,
                            Cancelado = false,
                            TipoCuota = CronogramaPago.ListaCronogramaDetallePago[i].TipoCuota,
                            Moneda = CronogramaPago.Moneda == "1" ? "soles" : "dolares",
                            TipocCambio = Convert.ToDecimal(CronogramaPago.TipoCambio),
                            UsuarioCreacion = CronogramaPago.NombreUsuario,
                            FechaCreacion = DateTime.Now,
                            UsuarioModificacion = CronogramaPago.NombreUsuario,
                            FechaModificacion = DateTime.Now,
                            Estado = true
                        };
                        _repCronogramaPagoDetalleOriginal.Insert(cronogramaPagoDetalleOriginalBO);
                    }

                    //var final = _tcrm_CentroCostoService.InserTCroDetFinal(objCroFinal, User.Identity.Name);
                    _repCronogramaPagoDetalleFinalRepositorio.Insert(cronogramaPagoDetalleFinalBO);
                    //var finalLog = _tCronogramaPagosDetalle_Mod_Log_FinalService.Insert(objCroLogFinal);
                    _repCronogramaPagoDetalleModLogFinal.Insert(cronogramaPagoDetalleModLogFinalBO);
                }
                /*var ObjCro = new TPW_MontoPagoCronogramasDTO();
                ObjCro.is_aprobado = true;
                ObjCro.mp_precioDescuento = System.Convert.ToDouble(listObjCronograma[0].totalPagar);
                ObjCro.NombrePlural = moneda;
                ObjCro.Firma = "";*/

                //var alumnoBD = _tAlumnoService.Get(new TAlumnoFiltroDTO() { id = Convert.ToInt32(idAlum) });
                //var tpespecifico = _tpLA_PEspecificoService.GetById(Convert.ToInt32(idPespecifico));

                //  _MontoPagoCronogramasController.archivo_crepCronograma(alumnoBD, tpespecifico, ObjCro, cronograma, "Nuevo");

                //var jsonResult = Json(new { Result = "OK"/*, Records = listRpta*/ }, JsonRequestBehavior.AllowGet);
                //jsonResult.MaxJsonLength = int.MaxValue;
                //return jsonResult;
                bool afirmacion = true;
				var cronogramaPago = _repCronogramaPago.FirstBy(x => x.IdMatriculaCabecera == CronogramaPago.ListaCronogramaDetallePago[0].IdMatriculaCabecera);
				var version = _repCronogramaPagoDetalleFinalRepositorio.ObtenerMaximaVersionCronograma(CronogramaPago.ListaCronogramaDetallePago[0].IdMatriculaCabecera);
				var cronogramaPagoDetFin = _repCronogramaPagoDetalleFinalRepositorio.GetBy(x => x.IdMatriculaCabecera == CronogramaPago.ListaCronogramaDetallePago[0].IdMatriculaCabecera && x.Version == version).OrderBy(x => x.NroCuota ).FirstOrDefault();
				cronogramaPago.ConCuotaInicial = true;
				cronogramaPago.CuotaInicial = cronogramaPagoDetFin.Cuota;
				cronogramaPago.FechaModificacion = DateTime.Now;
				cronogramaPago.UsuarioModificacion = CronogramaPago.NombreUsuario;
				_repCronogramaPago.Update(cronogramaPago);

				return Ok(new {afirmacion,idMatriculaCabecera});
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
