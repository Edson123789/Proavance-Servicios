using System;
using System.Collections.Generic;
using System.Linq;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Marketing.Repositorio;
using BSI.Integra.Aplicacion.Operaciones.BO;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/ReporteEmbudo")]
    public class ReporteEmbudoController : Controller
    {
        private readonly integraDBContext _integraDBContext;
        public ReporteEmbudoController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerTodo()
        {
            try
            {
                EmbudoRepositorio _repEmbudo = new EmbudoRepositorio(_integraDBContext);
                return Ok(_repEmbudo);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerFiltros()
        {
            try
            {
                AreaCapacitacionRepositorio _repAreaCapacitacion = new AreaCapacitacionRepositorio(_integraDBContext);
                SubAreaCapacitacionRepositorio _repSubAreaCapacitacion = new SubAreaCapacitacionRepositorio(_integraDBContext);
                PaisRepositorio _repPais = new PaisRepositorio(_integraDBContext);
                TipoCategoriaOrigenRepositorio _repTipoCategoriaOrigen = new TipoCategoriaOrigenRepositorio(_integraDBContext);
                PgeneralRepositorio _repPGeneral = new PgeneralRepositorio(_integraDBContext);
                PespecificoRepositorio _repPEspecifico = new PespecificoRepositorio(_integraDBContext);

                FiltroEmbudoDTO filtroEmbudo = new FiltroEmbudoDTO()
                {
                    ListadoAreaCapacitacion = _repAreaCapacitacion.ObtenerTodoFiltro(),
                    ListadoPais = _repPais.ObtenerListaPais(),
                    ListadoTipoCategoriaOrigen = _repTipoCategoriaOrigen.ObtenerTodoFiltro(),
                    ListadoSubAreaCapacitacion = _repSubAreaCapacitacion.ObtenerTodoFiltro(),
                    ListadoProgramaGeneral = _repPGeneral.ObtenerPorSubArea(),
                    ListadoProgramaEspecifico = _repPEspecifico.ObtenerPorPGeneral()
                };
                return Ok(filtroEmbudo);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerUltimaActualizacion()
        {
            try
            {
                EmbudoRepositorio _repEmbudo = new EmbudoRepositorio(_integraDBContext);

                var fechaUltimaActualizacion = _repEmbudo.ObtenerUltimaActualizacion();

                if (fechaUltimaActualizacion != null)
                {
                    return Ok(new { FechaUltimaActualizacion = fechaUltimaActualizacion });
                }

                DateTime fecha = DateTime.Now;
                fecha = fecha.AddDays(-1);
                return Ok(new { FechaUltimaActualizacion = fecha });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult ObtenerData([FromBody] FiltroReporteEmbudoDTO Filtro)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                EmbudoRepositorio _repEmbudo = new EmbudoRepositorio(_integraDBContext);
                var listaResultado = _repEmbudo.ObtenerFiltrado(Filtro);

                List<EmbudoResultadoAgrupadoDTO> listaEmbudoResultadoAgrupado = new List<EmbudoResultadoAgrupadoDTO>();

                foreach (var item in listaResultado)
                {
                    var _idEmbudoNivel = item.IdEmbudoNivel;
                    var _idEmbudoSubNivel = item.IdEmbudoSubNivel;


                    var inicialAgrupadoSubNivel = listaResultado.Where(x => x.IdEmbudoNivel == _idEmbudoNivel && x.IdEmbudoSubNivel == _idEmbudoSubNivel).Select(x => x.Inicial).Sum();
                    //var entranAgrupadoSubNivel = listaResultado.Where(x => x.IdEmbudoNivel == _idEmbudoNivel && x.IdEmbudoSubNivel == _idEmbudoSubNivel).Select(x => x.Entran).Sum();
                    //var salenAgrupadoSubNivel = listaResultado.Where(x => x.IdEmbudoNivel == _idEmbudoNivel && x.IdEmbudoSubNivel == _idEmbudoSubNivel).Select(x => x.Salen).Sum();
                    //var finalAgrupadoSubNivel = listaResultado.Where(x => x.IdEmbudoNivel == _idEmbudoNivel && x.IdEmbudoSubNivel == _idEmbudoSubNivel).Select(x => x.Final).Sum();

                    //var inicialAgrupadoNivel = listaResultado.Where(x => x.IdEmbudoNivel == _idEmbudoNivel).Select(x => x.Inicial).Sum();
                    //var entranAgrupadoNivel = listaResultado.Where(x => x.IdEmbudoNivel == _idEmbudoNivel).Select(x => x.Entran).Sum();
                    //var salenAgrupadoNivel = listaResultado.Where(x => x.IdEmbudoNivel == _idEmbudoNivel).Select(x => x.Salen).Sum();
                    //var finalAgrupadoNivel = listaResultado.Where(x => x.IdEmbudoNivel == _idEmbudoNivel).Select(x => x.Final).Sum();

                    var _iten = new EmbudoResultadoAgrupadoDTO()
                    {
                        Llave = item.Llave,
                        IdEmbudoNivel = item.IdEmbudoNivel,
                        NombreEmbudoNivel = item.NombreEmbudoNivel,
                        IdEmbudoSubNivel = item.IdEmbudoSubNivel,
                        NombreEmbudoSubNivel = item.NombreEmbudoSubNivel,
                        //IdTipoCategoriaOrigen = item.IdTipoCategoriaOrigen,
                        //NombreTipoCategoriaOrigen = item.NombreTipoCategoriaOrigen,

                        //InicialTipoCategoriaOrigen = item.Inicial,
                        //EntranTipoCategoriaOrigen = item.Entran,
                        //SalenTipoCategoriaOrigen = item.Salen,
                        //FinalTipoCategoriaOrigen = item.Final,

                        //InicialSubNivel = inicialAgrupadoSubNivel,
                        //EntranSubNivel = entranAgrupadoSubNivel,
                        //SalenSubNivel = salenAgrupadoSubNivel,
                        //FinalSubNivel = finalAgrupadoSubNivel,

                        //InicialNivel = inicialAgrupadoNivel,
                        //EntranNivel = entranAgrupadoNivel,
                        //SalenNivel = salenAgrupadoNivel,
                        //FinalNivel = finalAgrupadoNivel
                    };
                    listaEmbudoResultadoAgrupado.Add(_iten);
                }
                return Ok(listaEmbudoResultadoAgrupado);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }



        [Route("[Action]")]
        [HttpGet]
        public ActionResult DescargarResultado(DateTime FechaInicio, DateTime FechaFin, string ListaArea, string ListaSubArea, string ListaProgramaGeneral, string ListaPais, string ListaTipoCategoriaOrigen)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var _listaArea = new List<FiltroValorEmbudoDTO>();
                var _listaSubArea = new List<FiltroValorEmbudoDTO>();
                var _listaPais = new List<FiltroValorEmbudoDTO>();
                var _listaProgramaGeneral = new List<FiltroValorEmbudoDTO>();
                var _listaTipoCategoriaOrigen = new List<FiltroValorEmbudoDTO>();

                if (!string.IsNullOrEmpty(ListaArea))
                {
                    _listaArea = ListaArea.Split(',').Select(x => new FiltroValorEmbudoDTO { Valor = int.Parse(x) }).ToList();
                }
                if (!string.IsNullOrEmpty(ListaSubArea))
                {
                    _listaSubArea = ListaSubArea.Split(',').Select(x => new FiltroValorEmbudoDTO { Valor = int.Parse(x) }).ToList();
                }
                if (!string.IsNullOrEmpty(ListaProgramaGeneral))
                {
                    _listaProgramaGeneral = ListaProgramaGeneral.Split(',').Select(x => new FiltroValorEmbudoDTO { Valor = int.Parse(x) }).ToList();
                }
                if (!string.IsNullOrEmpty(ListaPais))
                {
                    _listaPais = ListaPais.Split(',').Select(x => new FiltroValorEmbudoDTO { Valor = int.Parse(x) }).ToList();
                }
                if (!string.IsNullOrEmpty(ListaTipoCategoriaOrigen))
                {
                    _listaTipoCategoriaOrigen = ListaTipoCategoriaOrigen.Split(',').Select(x => new FiltroValorEmbudoDTO { Valor = int.Parse(x) }).ToList();
                }

                FiltroReporteEmbudoDTO filtros = new FiltroReporteEmbudoDTO()
                {
                    FechaInicio = FechaInicio,
                    FechaFin = FechaFin,
                    ListaArea = _listaArea,
                    ListaSubArea = _listaSubArea,
                    ListaProgramaGeneral = _listaProgramaGeneral,
                    ListaPais = _listaPais,
                    ListaTipoCategoriaOrigen = _listaTipoCategoriaOrigen
                };
                
                EmbudoRepositorio _repEmbudo = new EmbudoRepositorio(_integraDBContext);

                var listaResultado = _repEmbudo.ObtenerFiltrado(filtros);

                var listadoFinal = listaResultado.GroupBy(x =>new { x.NombreEmbudoNivel, x.IdEmbudoNivel })
                    .Select(g => new EmbudoNivel
                        {
                            Id = g.Key.IdEmbudoNivel,
                            Nombre = g.Key.NombreEmbudoNivel,
                            Inicial = g.Select(x => x.Inicial).Sum(),
                            Entran = "",
                            Salen = "",
                            Nuevos = g.Select(x => x.Nuevos).Sum(),
                            Final = g.Select(x => x.Final).Sum(),
                        ListaSubNiveles = g.Select(p => new EmbudoSubNivel {
                                Id = p.IdEmbudoSubNivel,
                                Nombre = p.NombreEmbudoSubNivel,
                                Inicial = p.Inicial,
                                Entran = p.Entran,
                                Salen = p.Salen,
                                Nuevos = p.Nuevos,
                                Final = p.Final,
                            }).ToList()
                        }
                    ).ToList();

                var listaResultadoDetalle = _repEmbudo.ObtenerDetalleFiltrado(filtros);

                foreach (var nivel in listadoFinal)
                {
                    foreach (var subNivel in nivel.ListaSubNiveles)
                    {
                        subNivel.ListaSubSubNivel = listaResultadoDetalle.Where(x => x.IdEmbudoNivel == nivel.Id && x.IdEmbudoSubNivel == subNivel.Id)
                            .Select(x => new EmbudoSubSubNivel
                                        {
                                            Id = x.IdTipoCategoriaOrigen,
                                            Cantidad = x.Cantidad,
                                            Nombre = x.NombreTipoCategoriaOrigen
                                        }
                                    ).ToList();
                    }
                }
                var resultadoFiltro = new EmbudoResumentoDTO()
                {
                    ListaNivel = listadoFinal
                };

                ReporteBO reporte = new ReporteBO();
                var reporteExcel = reporte.ObtenerEmbudo(resultadoFiltro);

                return File(reporteExcel, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Filtro embudo.xlsx");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}