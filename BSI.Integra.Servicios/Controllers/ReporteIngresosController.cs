using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Finanzas.Repositorio;
using BSI.Integra.Aplicacion.GestionPersonas.Repositorio;
using BSI.Integra.Aplicacion.Marketing.Repositorio;
using BSI.Integra.Aplicacion.Planificacion.Repositorio;
using BSI.Integra.Aplicacion.Reportes.Comercial.Repositorio;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/ReporteIngresos")]
    public class ReporteIngresosController : ControllerBase
    {
        private readonly integraDBContext _integraDBContext;
        public ReporteIngresosController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
        }
            
        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerPeriodos()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                PeriodoRepositorio _repPeriodo = new PeriodoRepositorio();
                return Ok(_repPeriodo.ObtenerPeriodos());               
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [Route("[action]")]
        [HttpPost]
        public ActionResult GenerarReporteIngresos([FromBody] ReporteIngresosFiltroDTO FiltroIngresos)
        {
            try
            {
                ReportesRepositorio reportesRepositorio = new ReportesRepositorio();
                FeriadoRepositorio repFeriadoRep = new FeriadoRepositorio();
                FeriadoEspecialRepositorio repFeriadoEspecialRepo = new FeriadoEspecialRepositorio();

                decimal tipocambiomesanteriorcolombia = reportesRepositorio.ObtenerTipoCambioPromedioMesAnteriorColombianos(FiltroIngresos);

                var result = reportesRepositorio.ReporteIngresoMatriculasNuevas(FiltroIngresos).ToList();
                var resultP2 = reportesRepositorio.ReporteIngresoPagos(FiltroIngresos).ToList();
                var resultP3 = reportesRepositorio.ReporteIngresoInHouse(FiltroIngresos).ToList();
                var resultP4 = reportesRepositorio.ReporteIngresoPorHoras(FiltroIngresos).ToList();
                var resultP5 = reportesRepositorio.ReporteIngresoOtrosIngresos(FiltroIngresos).ToList();//0 //Otros Ingresos                                         
                var resultP6 = reportesRepositorio.ReporteIngresoAbonoCuenta(FiltroIngresos);//1 //Abonos de cuentas                                        
                var resultP7 = reportesRepositorio.ReporteIngresoOtrosIntereses(FiltroIngresos);//1 //Otros intereces        

                var ListFeriados = repFeriadoRep.GetAll();

                var ListFeriadosEspeciales = repFeriadoEspecialRepo.GetAll().ToList();
                //foreach (var item in ListFeriadosEspeciales)
                //{
                //    Planificacion.Dominio.Entidades.tPLA_Feriados feriado = new Planificacion.Dominio.Entidades.tPLA_Feriados()
                //    {
                //        //Id = item.Id,
                //        Dia=item.Dia,
                //        idCiudad=item.idCiudad,
                //        Motivo=item.Motivo
                //    };

                //    ListFeriados.Add(feriado);
                //}

                List<ReporteIngresosFinalFinanzasDTO> ListaFinal = new List<ReporteIngresosFinalFinanzasDTO>();
                ReporteIngresosFinalFinanzasDTO ItemLista = new ReporteIngresosFinalFinanzasDTO();
                //AQUI PROCESO LOS DATOS 

                foreach (var item in result)
                {
                    if (item.FechaDepositaron != null)//si ya tien una fecha de deposito
                    {
                        continue;
                    }

                    if (item.WebMoneda == "2")//si es colombiano
                    {
                        decimal montocolombianos = item.Mes * item.WebTipoCambio;//para tener el monto en colombianos
                        item.Mes = montocolombianos / tipocambiomesanteriorcolombia;
                        item.MesTotal = item.Mes;
                    }

                    var listaferiadosbyformapago = new List<FeriadoBO>();

                    listaferiadosbyformapago.AddRange(ListFeriados);//agrego los feriados calendario

                    if (item.CuentaFeriadosEstatales == true)
                    {
                        foreach (var itemferiado in ListFeriadosEspeciales)
                        {
                            FeriadoBO feriado = new FeriadoBO()
                            {
                                //Id = item.Id,
                                Dia = itemferiado.Dia,
                                IdTroncalCiudad = itemferiado.IdCiudad,
                                Motivo = itemferiado.Motivo
                            };

                            listaferiadosbyformapago.Add(feriado);
                        }
                    }


                    item.Mes = item.Mes * (item.PorcentajeCobro / 100);

                    if (item.IdCiudad == 57)//si el pais es colombia se el asignacion idciudad 6 que es de bogota
                    {
                        item.IdCiudad = 6;
                    }
                    else//en cualquier otro caso (peru,ecuador,etc) se le asigna el id 2 que es de lima
                    {
                        item.IdCiudad = 2;
                    }


                    if (item.FechaPagoReal != null)//me salto todo
                    {
                        if (item.CuentaFeriados == false && item.ConsiderarDiasHabilesLV == false && item.ConsiderarDiasHabilesLS == false)
                        {
                            item.FechaDepositaron = item.FechaPagoReal;
                            item.FechaDisponible = item.FechaPagoReal;
                            item.FechaDepositaron = item.FechaDepositaron.Value.AddDays(item.DiasDeposito);
                            item.FechaDisponible = item.FechaDisponible.Value.AddDays(item.DiasDisponible);
                        }
                        else
                        {
                            //deposito
                            if (item.DiasDeposito == 0)
                            {
                                bool validadorferiado = true;
                                bool validadorhabiles = true;
                                item.FechaDepositaron = item.FechaPago;
                                while (validadorferiado || validadorhabiles)
                                {
                                    if ((listaferiadosbyformapago.Where(w => w.Dia == item.FechaDepositaron.Value.Date && w.IdTroncalCiudad == item.IdCiudad).FirstOrDefault() != null) && item.CuentaFeriados == true)
                                    {
                                        item.FechaDepositaron = item.FechaDepositaron.Value.AddDays(1);
                                        validadorferiado = true;
                                    }
                                    else
                                    {
                                        validadorferiado = false;
                                        if ((item.FechaDepositaron.Value.DayOfWeek.ToString() == "Sunday" && item.ConsiderarDiasHabilesLS == true) || ((item.FechaDepositaron.Value.DayOfWeek.ToString() == "Saturday" || item.FechaDepositaron.Value.DayOfWeek.ToString() == "Sunday") && item.ConsiderarDiasHabilesLV == true))
                                        {
                                            item.FechaDepositaron = item.FechaDepositaron.Value.AddDays(1);
                                            validadorhabiles = true;
                                        }
                                        else
                                            validadorhabiles = false;//lo mando a falso porque no le importa que caiga feriado
                                    }
                                }
                            }
                            else
                            {
                                item.FechaDepositaron = item.FechaPagoReal;
                                while (item.DiasDeposito > 0)
                                {
                                    item.FechaDepositaron = item.FechaDepositaron.Value.AddDays(1);

                                    if ((listaferiadosbyformapago.Where(w => w.Dia == item.FechaDepositaron.Value.Date && w.IdTroncalCiudad == item.IdCiudad).FirstOrDefault() != null))//significa que es feriado entonces ahora valido si considera feriados
                                    {
                                        if (item.CuentaFeriados == true)
                                            item.DiasDeposito = item.DiasDeposito;//sigue igual los dias deposito porque me salto el feriado
                                        else
                                            item.DiasDeposito = item.DiasDeposito - 1;
                                    }
                                    else
                                    {
                                        if (item.FechaDepositaron.Value.DayOfWeek.ToString() == "Sunday" || item.FechaDepositaron.Value.DayOfWeek.ToString() == "Saturday")//significa que es domingo entonces ahora valido si considera dias habiles
                                        {
                                            if ((item.ConsiderarDiasHabilesLV == true && (item.FechaDepositaron.Value.DayOfWeek.ToString() == "Sunday" || item.FechaDepositaron.Value.DayOfWeek.ToString() == "Saturday")) || (item.ConsiderarDiasHabilesLS == true && item.FechaDepositaron.Value.DayOfWeek.ToString() == "Sunday"))
                                                item.DiasDeposito = item.DiasDeposito;//sigue igual los dias deposito porque me salto el domingo o sabado
                                            else
                                                item.DiasDeposito = item.DiasDeposito - 1;
                                        }
                                        else
                                            item.DiasDeposito = item.DiasDeposito - 1;
                                    }

                                }
                            }

                            //disponible
                            if (item.DiasDisponible == 0)
                            {
                                bool validadorferiado = true;
                                bool validadorhabiles = true;
                                item.FechaDisponible = item.FechaPago;
                                while (validadorferiado || validadorhabiles)
                                {
                                    if ((listaferiadosbyformapago.Where(w => w.Dia == item.FechaDisponible.Value.Date && w.IdTroncalCiudad == item.IdCiudad).FirstOrDefault() != null) && item.CuentaFeriados == true)
                                    {
                                        item.FechaDisponible = item.FechaDisponible.Value.AddDays(1);
                                        validadorferiado = true;
                                    }
                                    else
                                    {
                                        validadorferiado = false;
                                        if ((item.FechaDisponible.Value.DayOfWeek.ToString() == "Sunday" && item.ConsiderarDiasHabilesLS == true) || ((item.FechaDisponible.Value.DayOfWeek.ToString() == "Saturday" || item.FechaDisponible.Value.DayOfWeek.ToString() == "Sunday") && item.ConsiderarDiasHabilesLV == true))
                                        {
                                            item.FechaDisponible = item.FechaDisponible.Value.AddDays(1);
                                            validadorhabiles = true;
                                        }
                                        else
                                            validadorhabiles = false;//lo mando a falso porque no le importa que caiga feriado
                                    }
                                }
                            }
                            else
                            {
                                item.FechaDisponible = item.FechaPagoReal;
                                while (item.DiasDisponible > 0)
                                {
                                    item.FechaDisponible = item.FechaDisponible.Value.AddDays(1);

                                    if ((listaferiadosbyformapago.Where(w => w.Dia == item.FechaDisponible.Value.Date && w.IdTroncalCiudad == item.IdCiudad).FirstOrDefault() != null))//significa que es feriado entonces ahora valido si considera feriados
                                    {
                                        if (item.CuentaFeriados == true)
                                            item.DiasDisponible = item.DiasDisponible;//sigue igual los dias deposito porque me salto el feriado
                                        else
                                            item.DiasDisponible = item.DiasDisponible - 1;
                                    }
                                    else
                                    {
                                        if (item.FechaDisponible.Value.DayOfWeek.ToString() == "Sunday" || item.FechaDisponible.Value.DayOfWeek.ToString() == "Saturday")//significa que es domingo entonces ahora valido si considera dias habiles
                                        {
                                            if ((item.ConsiderarDiasHabilesLV == true && (item.FechaDisponible.Value.DayOfWeek.ToString() == "Sunday" || item.FechaDisponible.Value.DayOfWeek.ToString() == "Saturday")) || (item.ConsiderarDiasHabilesLS == true && item.FechaDisponible.Value.DayOfWeek.ToString() == "Sunday"))
                                                item.DiasDisponible = item.DiasDisponible;//sigue igual los dias deposito porque me salto el domingo
                                            else
                                                item.DiasDisponible = item.DiasDisponible - 1;
                                        }
                                        else
                                            item.DiasDisponible = item.DiasDisponible - 1;
                                    }

                                }
                            }

                        }


                        if (DateTime.Now.Date >= item.FechaDisponible.Value.Date)
                            item.EstadoEfectivo = "Disponible";
                        else
                            if (DateTime.Now.Date >= item.FechaDepositaron.Value.Date)
                            item.EstadoEfectivo = "Depositado";
                    }
                    else
                    {
                        //no hago nada
                    }



                }
                //agregado para calcular los siguientes campos
                foreach (var itempP2 in resultP2)
                {
                    if (itempP2.FechaDepositaron != null)//si ya tien una fecha de deposito
                    {
                        continue;
                    }
                    if (itempP2.WebMoneda == "2")//si es colombiano
                    {
                        decimal montocolombianos = itempP2.Mes * itempP2.WebTipoCambio;//para tener el monto en colombianos
                        itempP2.Mes = montocolombianos / tipocambiomesanteriorcolombia;
                        itempP2.MesTotal = itempP2.Mes;
                    }
                    var listaferiadosbyformapago = new List<FeriadoBO>();

                    listaferiadosbyformapago.AddRange(ListFeriados);//agrego los feriados calendario

                    if (itempP2.CuentaFeriadosEstatales == true)
                    {
                        foreach (var itemferiado in ListFeriadosEspeciales)
                        {
                            FeriadoBO feriado = new FeriadoBO()
                            {
                                //Id = item.Id,
                                Dia = itemferiado.Dia,
                                IdTroncalCiudad = itemferiado.IdCiudad,
                                Motivo = itemferiado.Motivo
                            };

                            listaferiadosbyformapago.Add(feriado);
                        }
                    }

                    itempP2.Mes = itempP2.Mes * (itempP2.PorcentajeCobro / 100);
                    if (itempP2.IdCiudad == 57)//si el pais es colombia se el asignacion idciudad 6 que es de bogota
                    {
                        itempP2.IdCiudad = 6;
                    }
                    else//en cualquier otro caso (peru,ecuador,etc) se le asigna el id 2 que es de lima
                    {
                        itempP2.IdCiudad = 2;
                    }
                    if (itempP2.FechaPagoReal != null)//me salto todo
                    {
                        if (itempP2.CuentaFeriados == false && itempP2.ConsiderarDiasHabilesLV == false && itempP2.ConsiderarDiasHabilesLS == false)
                        {
                            itempP2.FechaDepositaron = itempP2.FechaPagoReal;
                            itempP2.FechaDisponible = itempP2.FechaPagoReal;
                            itempP2.FechaDepositaron = itempP2.FechaDepositaron.Value.AddDays(itempP2.DiasDeposito);
                            itempP2.FechaDisponible = itempP2.FechaDisponible.Value.AddDays(itempP2.DiasDisponible);
                        }
                        else
                        {
                            //deposito
                            if (itempP2.DiasDeposito == 0)
                            {
                                bool validadorferiado = true;
                                bool validadorhabiles = true;
                                itempP2.FechaDepositaron = itempP2.FechaPago;
                                while (validadorferiado || validadorhabiles)
                                {
                                    if ((listaferiadosbyformapago.Where(w => w.Dia == itempP2.FechaDepositaron.Value.Date && w.IdTroncalCiudad == itempP2.IdCiudad).FirstOrDefault() != null) && itempP2.CuentaFeriados == true)
                                    {
                                        itempP2.FechaDepositaron = itempP2.FechaDepositaron.Value.AddDays(1);
                                        validadorferiado = true;
                                    }
                                    else
                                    {
                                        validadorferiado = false;
                                        if ((itempP2.FechaDepositaron.Value.DayOfWeek.ToString() == "Sunday" && itempP2.ConsiderarDiasHabilesLS == true) || ((itempP2.FechaDepositaron.Value.DayOfWeek.ToString() == "Saturday" || itempP2.FechaDepositaron.Value.DayOfWeek.ToString() == "Sunday") && itempP2.ConsiderarDiasHabilesLV == true))
                                        {
                                            itempP2.FechaDepositaron = itempP2.FechaDepositaron.Value.AddDays(1);
                                            validadorhabiles = true;
                                        }
                                        else
                                            validadorhabiles = false;//lo mando a falso porque no le importa que caiga feriado
                                    }
                                }
                            }
                            else
                            {
                                itempP2.FechaDepositaron = itempP2.FechaPagoReal;
                                while (itempP2.DiasDeposito > 0)
                                {
                                    itempP2.FechaDepositaron = itempP2.FechaDepositaron.Value.AddDays(1);

                                    if ((listaferiadosbyformapago.Where(w => w.Dia == itempP2.FechaDepositaron.Value.Date && w.IdTroncalCiudad == itempP2.IdCiudad).FirstOrDefault() != null))//significa que es feriado entonces ahora valido si considera feriados
                                    {
                                        if (itempP2.CuentaFeriados == true)
                                            itempP2.DiasDeposito = itempP2.DiasDeposito;//sigue igual los dias deposito porque me salto el feriado
                                        else
                                            itempP2.DiasDeposito = itempP2.DiasDeposito - 1;
                                    }
                                    else
                                    {
                                        if (itempP2.FechaDepositaron.Value.DayOfWeek.ToString() == "Sunday" || itempP2.FechaDepositaron.Value.DayOfWeek.ToString() == "Saturday")//significa que es domingo entonces ahora valido si considera dias habiles
                                        {
                                            if ((itempP2.ConsiderarDiasHabilesLV == true && (itempP2.FechaDepositaron.Value.DayOfWeek.ToString() == "Sunday" || itempP2.FechaDepositaron.Value.DayOfWeek.ToString() == "Saturday")) || (itempP2.ConsiderarDiasHabilesLS == true && itempP2.FechaDepositaron.Value.DayOfWeek.ToString() == "Sunday"))
                                                itempP2.DiasDeposito = itempP2.DiasDeposito;//sigue igual los dias deposito porque me salto el domingo o sabado
                                            else
                                                itempP2.DiasDeposito = itempP2.DiasDeposito - 1;
                                        }
                                        else
                                            itempP2.DiasDeposito = itempP2.DiasDeposito - 1;
                                    }

                                }
                            }

                            //disponible
                            if (itempP2.DiasDisponible == 0)
                            {
                                bool validadorferiado = true;
                                bool validadorhabiles = true;
                                itempP2.FechaDisponible = itempP2.FechaPago;
                                while (validadorferiado || validadorhabiles)
                                {
                                    if ((listaferiadosbyformapago.Where(w => w.Dia == itempP2.FechaDisponible.Value.Date && w.IdTroncalCiudad == itempP2.IdCiudad).FirstOrDefault() != null) && itempP2.CuentaFeriados == true)
                                    {
                                        itempP2.FechaDisponible = itempP2.FechaDisponible.Value.AddDays(1);
                                        validadorferiado = true;
                                    }
                                    else
                                    {
                                        validadorferiado = false;
                                        if ((itempP2.FechaDisponible.Value.DayOfWeek.ToString() == "Sunday" && itempP2.ConsiderarDiasHabilesLS == true) || ((itempP2.FechaDisponible.Value.DayOfWeek.ToString() == "Saturday" || itempP2.FechaDisponible.Value.DayOfWeek.ToString() == "Sunday") && itempP2.ConsiderarDiasHabilesLV == true))
                                        {
                                            itempP2.FechaDisponible = itempP2.FechaDisponible.Value.AddDays(1);
                                            validadorhabiles = true;
                                        }
                                        else
                                            validadorhabiles = false;//lo mando a falso porque no le importa que caiga feriado
                                    }
                                }
                            }
                            else
                            {
                                itempP2.FechaDisponible = itempP2.FechaPagoReal;
                                while (itempP2.DiasDisponible > 0)
                                {
                                    itempP2.FechaDisponible = itempP2.FechaDisponible.Value.AddDays(1);

                                    if ((listaferiadosbyformapago.Where(w => w.Dia == itempP2.FechaDisponible.Value.Date && w.IdTroncalCiudad == itempP2.IdCiudad).FirstOrDefault() != null))//significa que es feriado entonces ahora valido si considera feriados
                                    {
                                        if (itempP2.CuentaFeriados == true)
                                            itempP2.DiasDisponible = itempP2.DiasDisponible;//sigue igual los dias deposito porque me salto el feriado
                                        else
                                            itempP2.DiasDisponible = itempP2.DiasDisponible - 1;
                                    }
                                    else
                                    {
                                        if (itempP2.FechaDisponible.Value.DayOfWeek.ToString() == "Sunday" || itempP2.FechaDisponible.Value.DayOfWeek.ToString() == "Saturday")//significa que es domingo entonces ahora valido si considera dias habiles
                                        {
                                            if ((itempP2.ConsiderarDiasHabilesLV == true && (itempP2.FechaDisponible.Value.DayOfWeek.ToString() == "Sunday" || itempP2.FechaDisponible.Value.DayOfWeek.ToString() == "Saturday")) || (itempP2.ConsiderarDiasHabilesLS == true && itempP2.FechaDisponible.Value.DayOfWeek.ToString() == "Sunday"))
                                                itempP2.DiasDisponible = itempP2.DiasDisponible;//sigue igual los dias deposito porque me salto el domingo
                                            else
                                                itempP2.DiasDisponible = itempP2.DiasDisponible - 1;
                                        }
                                        else
                                            itempP2.DiasDisponible = itempP2.DiasDisponible - 1;
                                    }

                                }
                            }

                        }


                        if (DateTime.Now.Date >= itempP2.FechaDisponible.Value.Date)
                            itempP2.EstadoEfectivo = "Disponible";
                        else
                            if (DateTime.Now.Date >= itempP2.FechaDepositaron.Value.Date)
                            itempP2.EstadoEfectivo = "Depositado";
                    }
                    else
                    {
                        //no hago nada
                    }



                }
                foreach (var itempP3 in resultP3)
                {
                    if (itempP3.FechaDepositaron != null)//si ya tien una fecha de deposito
                    {
                        continue;
                    }
                    if (itempP3.WebMoneda == "2")//si es colombiano
                    {
                        decimal montocolombianos = itempP3.Mes * itempP3.WebTipoCambio;//para tener el monto en colombianos
                        itempP3.Mes = montocolombianos / tipocambiomesanteriorcolombia;
                        itempP3.MesTotal = itempP3.Mes;
                    }

                    var listaferiadosbyformapago = new List<FeriadoBO>();

                    listaferiadosbyformapago.AddRange(ListFeriados);//agrego los feriados calendario

                    if (itempP3.CuentaFeriadosEstatales == true)
                    {
                        foreach (var itemferiado in ListFeriadosEspeciales)
                        {
                            FeriadoBO feriado = new FeriadoBO()
                            {
                                //Id = item.Id,
                                Dia = itemferiado.Dia,
                                IdTroncalCiudad = itemferiado.IdCiudad,
                                Motivo = itemferiado.Motivo
                            };

                            listaferiadosbyformapago.Add(feriado);
                        }
                    }


                    if (itempP3.FechaPagoReal != null)//me salto todo
                    {
                        if (itempP3.CuentaFeriados == false && itempP3.ConsiderarDiasHabilesLV == false && itempP3.ConsiderarDiasHabilesLS == false)
                        {
                            itempP3.FechaDepositaron = itempP3.FechaPagoReal;
                            itempP3.FechaDisponible = itempP3.FechaPagoReal;
                            itempP3.FechaDepositaron = itempP3.FechaDepositaron.Value.AddDays(itempP3.DiasDeposito);
                            itempP3.FechaDisponible = itempP3.FechaDisponible.Value.AddDays(itempP3.DiasDisponible);
                        }
                        else
                        {
                            //deposito
                            if (itempP3.DiasDeposito == 0)
                            {
                                bool validadorferiado = true;
                                bool validadorhabiles = true;
                                itempP3.FechaDepositaron = itempP3.FechaPago;
                                while (validadorferiado || validadorhabiles)
                                {
                                    if ((listaferiadosbyformapago.Where(w => w.Dia == itempP3.FechaDepositaron.Value.Date && w.IdTroncalCiudad == itempP3.IdCiudad).FirstOrDefault() != null) && itempP3.CuentaFeriados == true)
                                    {
                                        itempP3.FechaDepositaron = itempP3.FechaDepositaron.Value.AddDays(1);
                                        validadorferiado = true;
                                    }
                                    else
                                    {
                                        validadorferiado = false;
                                        if ((itempP3.FechaDepositaron.Value.DayOfWeek.ToString() == "Sunday" && itempP3.ConsiderarDiasHabilesLS == true) || ((itempP3.FechaDepositaron.Value.DayOfWeek.ToString() == "Saturday" || itempP3.FechaDepositaron.Value.DayOfWeek.ToString() == "Sunday") && itempP3.ConsiderarDiasHabilesLV == true))
                                        {
                                            itempP3.FechaDepositaron = itempP3.FechaDepositaron.Value.AddDays(1);
                                            validadorhabiles = true;
                                        }
                                        else
                                            validadorhabiles = false;//lo mando a falso porque no le importa que caiga feriado
                                    }
                                }
                            }
                            else
                            {
                                itempP3.FechaDepositaron = itempP3.FechaPagoReal;
                                while (itempP3.DiasDeposito > 0)
                                {
                                    itempP3.FechaDepositaron = itempP3.FechaDepositaron.Value.AddDays(1);

                                    if ((listaferiadosbyformapago.Where(w => w.Dia == itempP3.FechaDepositaron.Value.Date && w.IdTroncalCiudad == itempP3.IdCiudad).FirstOrDefault() != null))//significa que es feriado entonces ahora valido si considera feriados
                                    {
                                        if (itempP3.CuentaFeriados == true)
                                            itempP3.DiasDeposito = itempP3.DiasDeposito;//sigue igual los dias deposito porque me salto el feriado
                                        else
                                            itempP3.DiasDeposito = itempP3.DiasDeposito - 1;
                                    }
                                    else
                                    {
                                        if (itempP3.FechaDepositaron.Value.DayOfWeek.ToString() == "Sunday" || itempP3.FechaDepositaron.Value.DayOfWeek.ToString() == "Saturday")//significa que es domingo entonces ahora valido si considera dias habiles
                                        {
                                            if ((itempP3.ConsiderarDiasHabilesLV == true && (itempP3.FechaDepositaron.Value.DayOfWeek.ToString() == "Sunday" || itempP3.FechaDepositaron.Value.DayOfWeek.ToString() == "Saturday")) || (itempP3.ConsiderarDiasHabilesLS == true && itempP3.FechaDepositaron.Value.DayOfWeek.ToString() == "Sunday"))
                                                itempP3.DiasDeposito = itempP3.DiasDeposito;//sigue igual los dias deposito porque me salto el domingo o sabado
                                            else
                                                itempP3.DiasDeposito = itempP3.DiasDeposito - 1;
                                        }
                                        else
                                            itempP3.DiasDeposito = itempP3.DiasDeposito - 1;
                                    }

                                }
                            }

                            //disponible
                            if (itempP3.DiasDisponible == 0)
                            {
                                bool validadorferiado = true;
                                bool validadorhabiles = true;
                                itempP3.FechaDisponible = itempP3.FechaPago;
                                while (validadorferiado || validadorhabiles)
                                {
                                    if ((listaferiadosbyformapago.Where(w => w.Dia == itempP3.FechaDisponible.Value.Date && w.IdTroncalCiudad == itempP3.IdCiudad).FirstOrDefault() != null) && itempP3.CuentaFeriados == true)
                                    {
                                        itempP3.FechaDisponible = itempP3.FechaDisponible.Value.AddDays(1);
                                        validadorferiado = true;
                                    }
                                    else
                                    {
                                        validadorferiado = false;
                                        if ((itempP3.FechaDisponible.Value.DayOfWeek.ToString() == "Sunday" && itempP3.ConsiderarDiasHabilesLS == true) || ((itempP3.FechaDisponible.Value.DayOfWeek.ToString() == "Saturday" || itempP3.FechaDisponible.Value.DayOfWeek.ToString() == "Sunday") && itempP3.ConsiderarDiasHabilesLV == true))
                                        {
                                            itempP3.FechaDisponible = itempP3.FechaDisponible.Value.AddDays(1);
                                            validadorhabiles = true;
                                        }
                                        else
                                            validadorhabiles = false;//lo mando a falso porque no le importa que caiga feriado
                                    }
                                }
                            }
                            else
                            {
                                itempP3.FechaDisponible = itempP3.FechaPagoReal;
                                while (itempP3.DiasDisponible > 0)
                                {
                                    itempP3.FechaDisponible = itempP3.FechaDisponible.Value.AddDays(1);

                                    if ((listaferiadosbyformapago.Where(w => w.Dia == itempP3.FechaDisponible.Value.Date && w.IdTroncalCiudad == itempP3.IdCiudad).FirstOrDefault() != null))//significa que es feriado entonces ahora valido si considera feriados
                                    {
                                        if (itempP3.CuentaFeriados == true)
                                            itempP3.DiasDisponible = itempP3.DiasDisponible;//sigue igual los dias deposito porque me salto el feriado
                                        else
                                            itempP3.DiasDisponible = itempP3.DiasDisponible - 1;
                                    }
                                    else
                                    {
                                        if (itempP3.FechaDisponible.Value.DayOfWeek.ToString() == "Sunday" || itempP3.FechaDisponible.Value.DayOfWeek.ToString() == "Saturday")//significa que es domingo entonces ahora valido si considera dias habiles
                                        {
                                            if ((itempP3.ConsiderarDiasHabilesLV == true && (itempP3.FechaDisponible.Value.DayOfWeek.ToString() == "Sunday" || itempP3.FechaDisponible.Value.DayOfWeek.ToString() == "Saturday")) || (itempP3.ConsiderarDiasHabilesLS == true && itempP3.FechaDisponible.Value.DayOfWeek.ToString() == "Sunday"))
                                                itempP3.DiasDisponible = itempP3.DiasDisponible;//sigue igual los dias deposito porque me salto el domingo
                                            else
                                                itempP3.DiasDisponible = itempP3.DiasDisponible - 1;
                                        }
                                        else
                                            itempP3.DiasDisponible = itempP3.DiasDisponible - 1;
                                    }

                                }
                            }

                        }


                        if (DateTime.Now.Date >= itempP3.FechaDisponible.Value.Date)
                            itempP3.EstadoEfectivo = "Disponible";
                        else
                            if (DateTime.Now.Date >= itempP3.FechaDepositaron.Value.Date)
                            itempP3.EstadoEfectivo = "Depositado";
                    }
                    else
                    {
                        //no hago nada
                    }



                }
                foreach (var itempP4 in resultP4)
                {
                    if (itempP4.FechaDepositaron != null)//si ya tien una fecha de deposito
                    {
                        continue;
                    }
                    if (itempP4.WebMoneda == "2")//si es colombiano
                    {
                        decimal montocolombianos = itempP4.Mes * itempP4.WebTipoCambio;//para tener el monto en colombianos
                        itempP4.Mes = montocolombianos / tipocambiomesanteriorcolombia;
                        itempP4.MesTotal = itempP4.Mes;
                    }

                    var listaferiadosbyformapago = new List<FeriadoBO>();

                    listaferiadosbyformapago.AddRange(ListFeriados);//agrego los feriados calendario

                    if (itempP4.CuentaFeriadosEstatales == true)
                    {
                        foreach (var itemferiado in ListFeriadosEspeciales)
                        {
                            FeriadoBO feriado = new FeriadoBO()
                            {
                                //Id = item.Id,
                                Dia = itemferiado.Dia,
                                IdTroncalCiudad = itemferiado.IdCiudad,
                                Motivo = itemferiado.Motivo
                            };

                            listaferiadosbyformapago.Add(feriado);
                        }
                    }

                    if (itempP4.FechaPagoReal != null)//me salto todo
                    {
                        if (itempP4.CuentaFeriados == false && itempP4.ConsiderarDiasHabilesLV == false && itempP4.ConsiderarDiasHabilesLS == false)
                        {
                            itempP4.FechaDepositaron = itempP4.FechaPagoReal;
                            itempP4.FechaDisponible = itempP4.FechaPagoReal;
                            itempP4.FechaDepositaron = itempP4.FechaDepositaron.Value.AddDays(itempP4.DiasDeposito);
                            itempP4.FechaDisponible = itempP4.FechaDisponible.Value.AddDays(itempP4.DiasDisponible);
                        }
                        else
                        {
                            //deposito
                            if (itempP4.DiasDeposito == 0)
                            {
                                bool validadorferiado = true;
                                bool validadorhabiles = true;
                                itempP4.FechaDepositaron = itempP4.FechaPago;
                                while (validadorferiado || validadorhabiles)
                                {
                                    if ((listaferiadosbyformapago.Where(w => w.Dia == itempP4.FechaDepositaron.Value.Date && w.IdTroncalCiudad == itempP4.IdCiudad).FirstOrDefault() != null) && itempP4.CuentaFeriados == true)
                                    {
                                        itempP4.FechaDepositaron = itempP4.FechaDepositaron.Value.AddDays(1);
                                        validadorferiado = true;
                                    }
                                    else
                                    {
                                        validadorferiado = false;
                                        if ((itempP4.FechaDepositaron.Value.DayOfWeek.ToString() == "Sunday" && itempP4.ConsiderarDiasHabilesLS == true) || ((itempP4.FechaDepositaron.Value.DayOfWeek.ToString() == "Saturday" || itempP4.FechaDepositaron.Value.DayOfWeek.ToString() == "Sunday") && itempP4.ConsiderarDiasHabilesLV == true))
                                        {
                                            itempP4.FechaDepositaron = itempP4.FechaDepositaron.Value.AddDays(1);
                                            validadorhabiles = true;
                                        }
                                        else
                                            validadorhabiles = false;//lo mando a falso porque no le importa que caiga feriado
                                    }
                                }
                            }
                            else
                            {
                                itempP4.FechaDepositaron = itempP4.FechaPagoReal;
                                while (itempP4.DiasDeposito > 0)
                                {
                                    itempP4.FechaDepositaron = itempP4.FechaDepositaron.Value.AddDays(1);

                                    if ((listaferiadosbyformapago.Where(w => w.Dia == itempP4.FechaDepositaron.Value.Date && w.IdTroncalCiudad == itempP4.IdCiudad).FirstOrDefault() != null))//significa que es feriado entonces ahora valido si considera feriados
                                    {
                                        if (itempP4.CuentaFeriados == true)
                                            itempP4.DiasDeposito = itempP4.DiasDeposito;//sigue igual los dias deposito porque me salto el feriado
                                        else
                                            itempP4.DiasDeposito = itempP4.DiasDeposito - 1;
                                    }
                                    else
                                    {
                                        if (itempP4.FechaDepositaron.Value.DayOfWeek.ToString() == "Sunday" || itempP4.FechaDepositaron.Value.DayOfWeek.ToString() == "Saturday")//significa que es domingo entonces ahora valido si considera dias habiles
                                        {
                                            if ((itempP4.ConsiderarDiasHabilesLV == true && (itempP4.FechaDepositaron.Value.DayOfWeek.ToString() == "Sunday" || itempP4.FechaDepositaron.Value.DayOfWeek.ToString() == "Saturday")) || (itempP4.ConsiderarDiasHabilesLS == true && itempP4.FechaDepositaron.Value.DayOfWeek.ToString() == "Sunday"))
                                                itempP4.DiasDeposito = itempP4.DiasDeposito;//sigue igual los dias deposito porque me salto el domingo o sabado
                                            else
                                                itempP4.DiasDeposito = itempP4.DiasDeposito - 1;
                                        }
                                        else
                                            itempP4.DiasDeposito = itempP4.DiasDeposito - 1;
                                    }

                                }
                            }

                            //disponible
                            if (itempP4.DiasDisponible == 0)
                            {
                                bool validadorferiado = true;
                                bool validadorhabiles = true;
                                itempP4.FechaDisponible = itempP4.FechaPago;
                                while (validadorferiado || validadorhabiles)
                                {
                                    if ((listaferiadosbyformapago.Where(w => w.Dia == itempP4.FechaDisponible.Value.Date && w.IdTroncalCiudad == itempP4.IdCiudad).FirstOrDefault() != null) && itempP4.CuentaFeriados == true)
                                    {
                                        itempP4.FechaDisponible = itempP4.FechaDisponible.Value.AddDays(1);
                                        validadorferiado = true;
                                    }
                                    else
                                    {
                                        validadorferiado = false;
                                        if ((itempP4.FechaDisponible.Value.DayOfWeek.ToString() == "Sunday" && itempP4.ConsiderarDiasHabilesLS == true) || ((itempP4.FechaDisponible.Value.DayOfWeek.ToString() == "Saturday" || itempP4.FechaDisponible.Value.DayOfWeek.ToString() == "Sunday") && itempP4.ConsiderarDiasHabilesLV == true))
                                        {
                                            itempP4.FechaDisponible = itempP4.FechaDisponible.Value.AddDays(1);
                                            validadorhabiles = true;
                                        }
                                        else
                                            validadorhabiles = false;//lo mando a falso porque no le importa que caiga feriado
                                    }
                                }
                            }
                            else
                            {
                                itempP4.FechaDisponible = itempP4.FechaPagoReal;
                                while (itempP4.DiasDisponible > 0)
                                {
                                    itempP4.FechaDisponible = itempP4.FechaDisponible.Value.AddDays(1);

                                    if ((listaferiadosbyformapago.Where(w => w.Dia == itempP4.FechaDisponible.Value.Date && w.IdTroncalCiudad == itempP4.IdCiudad).FirstOrDefault() != null))//significa que es feriado entonces ahora valido si considera feriados
                                    {
                                        if (itempP4.CuentaFeriados == true)
                                            itempP4.DiasDisponible = itempP4.DiasDisponible;//sigue igual los dias deposito porque me salto el feriado
                                        else
                                            itempP4.DiasDisponible = itempP4.DiasDisponible - 1;
                                    }
                                    else
                                    {
                                        if (itempP4.FechaDisponible.Value.DayOfWeek.ToString() == "Sunday" || itempP4.FechaDisponible.Value.DayOfWeek.ToString() == "Saturday")//significa que es domingo entonces ahora valido si considera dias habiles
                                        {
                                            if ((itempP4.ConsiderarDiasHabilesLV == true && (itempP4.FechaDisponible.Value.DayOfWeek.ToString() == "Sunday" || itempP4.FechaDisponible.Value.DayOfWeek.ToString() == "Saturday")) || (itempP4.ConsiderarDiasHabilesLS == true && itempP4.FechaDisponible.Value.DayOfWeek.ToString() == "Sunday"))
                                                itempP4.DiasDisponible = itempP4.DiasDisponible;//sigue igual los dias deposito porque me salto el domingo
                                            else
                                                itempP4.DiasDisponible = itempP4.DiasDisponible - 1;
                                        }
                                        else
                                            itempP4.DiasDisponible = itempP4.DiasDisponible - 1;
                                    }

                                }
                            }

                        }


                        if (DateTime.Now.Date >= itempP4.FechaDisponible.Value.Date)
                            itempP4.EstadoEfectivo = "Disponible";
                        else
                            if (DateTime.Now.Date >= itempP4.FechaDepositaron.Value.Date)
                            itempP4.EstadoEfectivo = "Depositado";
                    }
                    else
                    {
                        //no hago nada
                    }



                }
                foreach (var itempP5 in resultP5)
                {
                    if (itempP5.FechaDepositaron != null)//si ya tien una fecha de deposito
                    {
                        continue;
                    }
                    if (itempP5.FechaPagoReal != null)//me salto todo
                    {
                        if (itempP5.CuentaFeriados == false && itempP5.ConsiderarDiasHabilesLV == false && itempP5.ConsiderarDiasHabilesLS == false)
                        {
                            itempP5.FechaDepositaron = itempP5.FechaPagoReal;
                            itempP5.FechaDisponible = itempP5.FechaPagoReal;
                            itempP5.FechaDepositaron = itempP5.FechaDepositaron.Value.AddDays(itempP5.DiasDeposito);
                            itempP5.FechaDisponible = itempP5.FechaDisponible.Value.AddDays(itempP5.DiasDisponible);
                        }
                        else
                        {
                            //deposito
                            if (itempP5.DiasDeposito == 0)
                            {
                                bool validadorferiado = true;
                                bool validadorhabiles = true;
                                itempP5.FechaDepositaron = itempP5.FechaPago;
                                while (validadorferiado || validadorhabiles)
                                {
                                    if ((ListFeriados.Where(w => w.Dia == itempP5.FechaDepositaron.Value.Date && w.IdTroncalCiudad == itempP5.IdCiudad).FirstOrDefault() != null) && itempP5.CuentaFeriados == true)
                                    {
                                        itempP5.FechaDepositaron = itempP5.FechaDepositaron.Value.AddDays(1);
                                        validadorferiado = true;
                                    }
                                    else
                                    {
                                        validadorferiado = false;
                                        if ((itempP5.FechaDepositaron.Value.DayOfWeek.ToString() == "Sunday" && itempP5.ConsiderarDiasHabilesLS == true) || ((itempP5.FechaDepositaron.Value.DayOfWeek.ToString() == "Saturday" || itempP5.FechaDepositaron.Value.DayOfWeek.ToString() == "Sunday") && itempP5.ConsiderarDiasHabilesLV == true))
                                        {
                                            itempP5.FechaDepositaron = itempP5.FechaDepositaron.Value.AddDays(1);
                                            validadorhabiles = true;
                                        }
                                        else
                                            validadorhabiles = false;//lo mando a falso porque no le importa que caiga feriado
                                    }
                                }
                            }
                            else
                            {
                                itempP5.FechaDepositaron = itempP5.FechaPagoReal;
                                while (itempP5.DiasDeposito > 0)
                                {
                                    itempP5.FechaDepositaron = itempP5.FechaDepositaron.Value.AddDays(1);

                                    if ((ListFeriados.Where(w => w.Dia == itempP5.FechaDepositaron.Value.Date && w.IdTroncalCiudad == itempP5.IdCiudad).FirstOrDefault() != null))//significa que es feriado entonces ahora valido si considera feriados
                                    {
                                        if (itempP5.CuentaFeriados == true)
                                            itempP5.DiasDeposito = itempP5.DiasDeposito;//sigue igual los dias deposito porque me salto el feriado
                                        else
                                            itempP5.DiasDeposito = itempP5.DiasDeposito - 1;
                                    }
                                    else
                                    {
                                        if (itempP5.FechaDepositaron.Value.DayOfWeek.ToString() == "Sunday" || itempP5.FechaDepositaron.Value.DayOfWeek.ToString() == "Saturday")//significa que es domingo entonces ahora valido si considera dias habiles
                                        {
                                            if ((itempP5.ConsiderarDiasHabilesLV == true && (itempP5.FechaDepositaron.Value.DayOfWeek.ToString() == "Sunday" || itempP5.FechaDepositaron.Value.DayOfWeek.ToString() == "Saturday")) || (itempP5.ConsiderarDiasHabilesLS == true && itempP5.FechaDepositaron.Value.DayOfWeek.ToString() == "Sunday"))
                                                itempP5.DiasDeposito = itempP5.DiasDeposito;//sigue igual los dias deposito porque me salto el domingo o sabado
                                            else
                                                itempP5.DiasDeposito = itempP5.DiasDeposito - 1;
                                        }
                                        else
                                            itempP5.DiasDeposito = itempP5.DiasDeposito - 1;
                                    }

                                }
                            }

                            //disponible
                            if (itempP5.DiasDisponible == 0)
                            {
                                bool validadorferiado = true;
                                bool validadorhabiles = true;
                                itempP5.FechaDisponible = itempP5.FechaPago;
                                while (validadorferiado || validadorhabiles)
                                {
                                    if ((ListFeriados.Where(w => w.Dia == itempP5.FechaDisponible.Value.Date && w.IdTroncalCiudad == itempP5.IdCiudad).FirstOrDefault() != null) && itempP5.CuentaFeriados == true)
                                    {
                                        itempP5.FechaDisponible = itempP5.FechaDisponible.Value.AddDays(1);
                                        validadorferiado = true;
                                    }
                                    else
                                    {
                                        validadorferiado = false;
                                        if ((itempP5.FechaDisponible.Value.DayOfWeek.ToString() == "Sunday" && itempP5.ConsiderarDiasHabilesLS == true) || ((itempP5.FechaDisponible.Value.DayOfWeek.ToString() == "Saturday" || itempP5.FechaDisponible.Value.DayOfWeek.ToString() == "Sunday") && itempP5.ConsiderarDiasHabilesLV == true))
                                        {
                                            itempP5.FechaDisponible = itempP5.FechaDisponible.Value.AddDays(1);
                                            validadorhabiles = true;
                                        }
                                        else
                                            validadorhabiles = false;//lo mando a falso porque no le importa que caiga feriado
                                    }
                                }
                            }
                            else
                            {
                                itempP5.FechaDisponible = itempP5.FechaPagoReal;
                                while (itempP5.DiasDisponible > 0)
                                {
                                    itempP5.FechaDisponible = itempP5.FechaDisponible.Value.AddDays(1);

                                    if ((ListFeriados.Where(w => w.Dia == itempP5.FechaDisponible.Value.Date && w.IdTroncalCiudad == itempP5.IdCiudad).FirstOrDefault() != null))//significa que es feriado entonces ahora valido si considera feriados
                                    {
                                        if (itempP5.CuentaFeriados == true)
                                            itempP5.DiasDisponible = itempP5.DiasDisponible;//sigue igual los dias deposito porque me salto el feriado
                                        else
                                            itempP5.DiasDisponible = itempP5.DiasDisponible - 1;
                                    }
                                    else
                                    {
                                        if (itempP5.FechaDisponible.Value.DayOfWeek.ToString() == "Sunday" || itempP5.FechaDisponible.Value.DayOfWeek.ToString() == "Saturday")//significa que es domingo entonces ahora valido si considera dias habiles
                                        {
                                            if ((itempP5.ConsiderarDiasHabilesLV == true && (itempP5.FechaDisponible.Value.DayOfWeek.ToString() == "Sunday" || itempP5.FechaDisponible.Value.DayOfWeek.ToString() == "Saturday")) || (itempP5.ConsiderarDiasHabilesLS == true && itempP5.FechaDisponible.Value.DayOfWeek.ToString() == "Sunday"))
                                                itempP5.DiasDisponible = itempP5.DiasDisponible;//sigue igual los dias deposito porque me salto el domingo
                                            else
                                                itempP5.DiasDisponible = itempP5.DiasDisponible - 1;
                                        }
                                        else
                                            itempP5.DiasDisponible = itempP5.DiasDisponible - 1;
                                    }

                                }
                            }

                        }


                        if (DateTime.Now.Date >= itempP5.FechaDisponible.Value.Date)
                            itempP5.EstadoEfectivo = "Disponible";
                        else
                            if (DateTime.Now.Date >= itempP5.FechaDepositaron.Value.Date)
                            itempP5.EstadoEfectivo = "Depositado";
                    }
                    else
                    {
                        //no hago nada
                    }



                }
                //fin add

                DateTime FechainicioOriginalTemp = FiltroIngresos.SeleccionoPeriodo == true ? DateTime.ParseExact(result.FirstOrDefault().FechaInicioPeriodo, "yyyyMMddHHmmss", System.Globalization.CultureInfo.InvariantCulture) : DateTime.ParseExact(result.FirstOrDefault().FechaInicioFiltro, "yyyyMMddHHmmss", System.Globalization.CultureInfo.InvariantCulture);
                DateTime FechaFinOriginalTemp = FiltroIngresos.SeleccionoPeriodo == true ? DateTime.ParseExact(result.FirstOrDefault().FechaFinPeriodo, "yyyyMMddHHmmss", System.Globalization.CultureInfo.InvariantCulture) : DateTime.ParseExact(result.FirstOrDefault().FechaFinFiltro, "yyyyMMddHHmmss", System.Globalization.CultureInfo.InvariantCulture);
                //quitamos nuevas matriculas 
                var matriculas_nuevas = (from p in result
                                         where p.FechaDepositaron >= FechainicioOriginalTemp && p.FechaDepositaron <= FechaFinOriginalTemp

                                         select p).ToList();
                var res = (from c in resultP2
                           where !matriculas_nuevas.Select(o => o.Idmatricula).Equals(c.Idmatricula)
                           select c).ToList();

                var agrupado = (from p in result
                                where DateTime.ParseExact(p.FechaLog, "yyyyMMddHHmmss", System.Globalization.CultureInfo.InvariantCulture) >= FechainicioOriginalTemp && DateTime.ParseExact(p.FechaLog, "yyyyMMddHHmmss", System.Globalization.CultureInfo.InvariantCulture) <= FechaFinOriginalTemp
                                group p by new { p.IdOportunidad, p.CentroCosto } into grupo
                                select new
                                {
                                    g = grupo.Key,
                                    Inscritos_CursosRegulares = grupo.Sum(w => w.Cantidad),
                                    IngresoCursosRegulares = (from x in grupo select (x.FechaDepositaron >= FechainicioOriginalTemp && x.FechaDepositaron <= FechaFinOriginalTemp) ? x.Mes : 0).Sum()
                                }).ToList();



                var atrasos = (from r in res
                               where ((((DateTime)r.FechaVencimiento).Month < ((DateTime)r.FechaPago).Month && ((DateTime)r.FechaVencimiento).Year <= ((DateTime)r.FechaPago).Year) || (((DateTime)r.FechaVencimiento).Month == ((DateTime)r.FechaPago).Month && ((DateTime)r.FechaVencimiento).Year < ((DateTime)r.FechaPago).Year))
                               select r).ToList();

                var adelantos = (from r in res
                                 where ((((DateTime)r.FechaVencimiento).Month > ((DateTime)r.FechaPago).Month && ((DateTime)r.FechaVencimiento).Year >= ((DateTime)r.FechaPago).Year) || (((DateTime)r.FechaVencimiento).Month == ((DateTime)r.FechaPago).Month && ((DateTime)r.FechaVencimiento).Year > ((DateTime)r.FechaPago).Year))
                                 select r).ToList();

                var ingresoMes = (from r in res
                                  where ((DateTime)r.FechaVencimiento).Month == ((DateTime)r.FechaPago).Month && ((DateTime)r.FechaVencimiento).Year == ((DateTime)r.FechaPago).Year
                                  select r).ToList();

                //var carlos = result.Where(p => p.FechaDepositaron >= FechainicioOriginalTemp && p.FechaDepositaron <= FechaFinOriginalTemp).ToList();
                //var carlos2 = carlos.Sum(w => w.mes);

                ///////////////////////////////////////DATOS PARA PATRICIA COMPARAR
                ////INSERT MASIVO 
                //foreach (var item in carlos)
                //{
                //    var insert = _centroCostoRepository.InsertDataPatricia(item).FirstOrDefault();
                //}

                ////var carlosori = (from p in ingresoMes
                ////              where p.fechapagoOriginal >= FechainicioOriginalTemp && p.fechapagoOriginal <= FechaFinOriginalTemp
                ////              select p).ToList();

                /////////////////////////////////////////////FIN DATOS PATRICIA COMPARAR


                var Alquileres = (from r in resultP5
                                  where r.Idmatricula == "Pagos por alquileres"
                                  select r).ToList();
                var Tramites = (from r in resultP5
                                where r.Idmatricula != "Pagos por alquileres"
                                select r).ToList();
                var agrupadoAtrasos = (from p in atrasos
                                       where p.FechaDepositaron >= FechainicioOriginalTemp && p.FechaDepositaron <= FechaFinOriginalTemp
                                       group p by new { p.IdOportunidad, p.CentroCosto } into grupo
                                       select new
                                       {
                                           g = grupo.Key,
                                           Inscritos_CursosRegulares = grupo.Sum(w => w.Cantidad),
                                           IngresoCursosRegulares = (from x in grupo select (x.FechaDepositaron >= FechainicioOriginalTemp && x.FechaDepositaron <= FechaFinOriginalTemp) ? x.Mes : 0).Sum()
                                       }).ToList();
                var agrupadoAdelantos = (from p in adelantos
                                         where p.FechaDepositaron >= FechainicioOriginalTemp && p.FechaDepositaron <= FechaFinOriginalTemp
                                         group p by new { p.IdOportunidad, p.CentroCosto } into grupo
                                         select new
                                         {
                                             g = grupo.Key,
                                             Inscritos_CursosRegulares = grupo.Sum(w => w.Cantidad),
                                             IngresoCursosRegulares = (from x in grupo select (x.FechaDepositaron >= FechainicioOriginalTemp && x.FechaDepositaron <= FechaFinOriginalTemp) ? x.Mes : 0).Sum()
                                         }).ToList();
                var agrupadoIngresoMes = (from p in ingresoMes
                                          where p.FechaDepositaron >= FechainicioOriginalTemp && p.FechaDepositaron <= FechaFinOriginalTemp
                                          group p by new { p.IdOportunidad, p.CentroCosto } into grupo
                                          select new
                                          {
                                              g = grupo.Key,
                                              Inscritos_CursosRegulares = grupo.Sum(w => w.Cantidad),
                                              IngresoCursosRegulares = (from x in grupo select (x.FechaDepositaron >= FechainicioOriginalTemp && x.FechaDepositaron <= FechaFinOriginalTemp) ? x.Mes : 0).Sum()
                                          }).ToList();
                var agrupadoAlquileres = (from p in Alquileres
                                          where p.FechaDepositaron >= FechainicioOriginalTemp && p.FechaDepositaron <= FechaFinOriginalTemp
                                          group p by new { p.IdOportunidad, p.CentroCosto } into grupo
                                          select new
                                          {
                                              g = grupo.Key,
                                              Inscritos_CursosRegulares = grupo.Sum(w => w.Cantidad),
                                              IngresoCursosRegulares = (from x in grupo select (x.FechaDepositaron >= FechainicioOriginalTemp && x.FechaDepositaron <= FechaFinOriginalTemp) ? x.Mes : 0).Sum()
                                          }).ToList();
                var agrupadoTramintes = (from p in Tramites
                                         where p.FechaDepositaron >= FechainicioOriginalTemp && p.FechaDepositaron <= FechaFinOriginalTemp
                                         group p by new { p.IdOportunidad, p.CentroCosto } into grupo
                                         select new
                                         {
                                             g = grupo.Key,
                                             Inscritos_CursosRegulares = grupo.Sum(w => w.Cantidad),
                                             IngresoCursosRegulares = (from x in grupo select (x.FechaDepositaron >= FechainicioOriginalTemp && x.FechaDepositaron <= FechaFinOriginalTemp) ? x.Mes : 0).Sum()
                                         }).ToList();



                var agrupado2 = (from p in result
                                 where p.FechaDepositaron >= FechainicioOriginalTemp && p.FechaDepositaron <= FechaFinOriginalTemp
                                 group p by new { p.IdOportunidad, p.CentroCosto } into grupo
                                 select new
                                 {
                                     g = grupo.Key,
                                     Inscritos_CursosRegulares = grupo.Sum(w => w.Cantidad),
                                     IngresoCursosRegulares = (from x in grupo select (x.FechaDepositaron >= FechainicioOriginalTemp && x.FechaDepositaron <= FechaFinOriginalTemp) ? x.Mes : 0).Sum()
                                 }).ToList();

                var agrupado3 = (from p in resultP3
                                 where p.FechaDepositaron >= FechainicioOriginalTemp && p.FechaDepositaron <= FechaFinOriginalTemp
                                 group p by new { p.IdOportunidad, p.CentroCosto } into grupo
                                 select new
                                 {
                                     g = grupo.Key,
                                     Inscritos_CursosRegulares = grupo.Sum(w => w.Cantidad),
                                     IngresoCursosRegulares = (from x in grupo select (x.FechaDepositaron >= FechainicioOriginalTemp && x.FechaDepositaron <= FechaFinOriginalTemp) ? x.Mes : 0).Sum()
                                 }).ToList();
                var agrupado4 = (from p in resultP4
                                 where p.FechaDepositaron >= FechainicioOriginalTemp && p.FechaDepositaron <= FechaFinOriginalTemp
                                 group p by new { p.IdOportunidad, p.CentroCosto } into grupo
                                 select new
                                 {
                                     g = grupo.Key,
                                     Inscritos_CursosRegulares = grupo.Sum(w => w.Cantidad),
                                     IngresoCursosRegulares = (from x in grupo select (x.FechaDepositaron >= FechainicioOriginalTemp && x.FechaDepositaron <= FechaFinOriginalTemp) ? x.Mes : 0).Sum()
                                 }).ToList();


                var Abonos = resultP6;//.Sum(w => w);
                var OtrosIntereses = resultP7;//.Sum(w => w);
                ItemLista.RangoFechas = FechainicioOriginalTemp.ToString("yyyy/MM/dd") + " AL " + FechaFinOriginalTemp.ToString("yyyy/MM/dd");
                ItemLista.IngresoCursosRegularesMNCuenta = agrupado2.Where(w => w.g.CentroCosto.ToUpper().IndexOf("INSTITUTO") < 0).Sum(w => w.IngresoCursosRegulares);

                ItemLista.IngresoCursosRegularesPAT = agrupadoAtrasos.Where(w => w.g.CentroCosto.ToUpper().IndexOf("INSTITUTO") < 0).Sum(w => w.IngresoCursosRegulares);
                ItemLista.IngresoInstitutoPAT = agrupadoAtrasos.Where(w => w.g.CentroCosto.ToUpper().IndexOf("INSTITUTO") >= 0).Sum(w => w.IngresoCursosRegulares);

                ItemLista.IngresoCursosRegularesPAD = agrupadoAdelantos.Where(w => w.g.CentroCosto.ToUpper().IndexOf("INSTITUTO") < 0).Sum(w => w.IngresoCursosRegulares);
                ItemLista.IngresoInstitutoPAD = agrupadoAdelantos.Where(w => w.g.CentroCosto.ToUpper().IndexOf("INSTITUTO") >= 0).Sum(w => w.IngresoCursosRegulares);

                ItemLista.IngresoCursosRegularesPM = agrupadoIngresoMes.Where(w => w.g.CentroCosto.ToUpper().IndexOf("INSTITUTO") < 0).Sum(w => w.IngresoCursosRegulares) - OtrosIntereses /*- agrupado.Where(w => w.g.centrocosto.ToUpper().IndexOf("INSTITUTO") < 0).Sum(w => w.IngresoCursosRegulares)*/;
                ItemLista.IngresoInstitutoPM = agrupadoIngresoMes.Where(w => w.g.CentroCosto.ToUpper().IndexOf("INSTITUTO") >= 0).Sum(w => w.IngresoCursosRegulares)/* - agrupado.Where(w => w.g.centrocosto.ToUpper().IndexOf("INSTITUTO") >= 0).Sum(w => w.IngresoCursosRegulares)*/;

                ItemLista.IngresosInHouse = agrupado3.Sum(w => w.IngresoCursosRegulares);
                ItemLista.IngresosAlquileres = agrupadoAlquileres.Sum(w => w.IngresoCursosRegulares);
                ItemLista.PagosPorTramites = agrupadoTramintes.Sum(w => w.IngresoCursosRegulares);
                ItemLista.IngresosFinancieros = agrupado4.Sum(w => w.IngresoCursosRegulares) + Abonos;

                //fin add

                ItemLista.InscritosRgulares = agrupado.Count(w => w.g.CentroCosto.ToUpper().IndexOf("INSTITUTO") < 0);
                ItemLista.InscritosInstituto = agrupado.Count(w => w.g.CentroCosto.ToUpper().IndexOf("INSTITUTO") >= 0);

                ItemLista.IngresoCursosRegularesMN = agrupado.Where(w => w.g.CentroCosto.ToUpper().IndexOf("INSTITUTO") < 0).Sum(w => w.IngresoCursosRegulares);
                ItemLista.IngresoInstitutoMN = agrupado.Where(w => w.g.CentroCosto.ToUpper().IndexOf("INSTITUTO") >= 0).Sum(w => w.IngresoCursosRegulares);
                //calculo de totales y porcentajes
                ItemLista.TotalIngresosSinInstituto = ItemLista.IngresoCursosRegularesPAT + ItemLista.IngresoCursosRegularesPAD + ItemLista.IngresoCursosRegularesPM + ItemLista.IngresoCursosRegularesMNCuenta + (ItemLista.IngresosInHouse + ItemLista.IngresosAlquileres + ItemLista.PagosPorTramites + ItemLista.IngresosFinancieros);
                ItemLista.IngresosInstituto = ItemLista.IngresoInstitutoPAT + ItemLista.IngresoInstitutoPAD + ItemLista.IngresoInstitutoPM + ItemLista.IngresoInstitutoMN;
                ItemLista.Total = ItemLista.TotalIngresosSinInstituto + ItemLista.IngresosInstituto;
                ItemLista.PorcentajeIngresoPAT = Decimal.Round(((ItemLista.IngresoCursosRegularesPAT + ItemLista.IngresoInstitutoPAT) / ItemLista.Total), 2);
                ItemLista.PorcentajeIngresoPAD = Decimal.Round(((ItemLista.IngresoCursosRegularesPAD + ItemLista.IngresoInstitutoPAD) / ItemLista.Total), 2);
                ItemLista.PorcentajeIngresoPM = Decimal.Round(((ItemLista.IngresoCursosRegularesPM + ItemLista.IngresoInstitutoPM) / ItemLista.Total), 2);
                ItemLista.PorcentajeIngresoMN = Decimal.Round(((ItemLista.IngresoCursosRegularesMNCuenta + ItemLista.IngresoInstitutoMN) / ItemLista.Total), 2);
                ItemLista.PorcentajeIngresoOI = Decimal.Round(((ItemLista.IngresosInHouse + ItemLista.IngresosAlquileres + ItemLista.PagosPorTramites + ItemLista.IngresosFinancieros) / ItemLista.Total), 2);
                //fin totales y %

                //FIN AQUI PROCESODATOS
                ListaFinal.Add(ItemLista);
                return Ok(ListaFinal);

                //ReportesRepositorio reportesRepositorio = new ReportesRepositorio();
                //var reporteCambios = reportesRepositorio.ObtenerReporteCambios(FiltroCambios);                
                //return Ok(reporteCambios);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


    }
}