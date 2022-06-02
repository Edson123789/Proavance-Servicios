using System;
using System.Collections.Generic;
using System.Linq;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Finanzas.Repositorio;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/ReporteEgresoPorRubro")]
    public class ReporteEgresoPorRubroController : Controller
    {
        private readonly integraDBContext _integraDBContext;

        public ReporteEgresoPorRubroController(integraDBContext integraDBContext) {
            _integraDBContext = integraDBContext;
        }


        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerListaSedes()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                SedeRepositorio _repoSede = new SedeRepositorio(_integraDBContext);
                var Lista = _repoSede.ObtenerListaSedesSegunFur();
                return Ok(Lista);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [Route("[Action]")]
        [HttpPost]
        public ActionResult VizualizarReporteEgresoPorRubro([FromBody] ReporteEgresoPorRubroSedesAnioDTO Filtro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ComprobantePagoPorFurRepositorio _repoComprobantePagoPorFur = new ComprobantePagoPorFurRepositorio(_integraDBContext);

                ReporteEgresoPorRubroDTO GastoFinanciero = new ReporteEgresoPorRubroDTO();
                GastoFinanciero.Rubro = "Gastos Financieros";
                GastoFinanciero.EmpresaSede = "Desconocido";

                ReporteEgresoPorRubroDTO Desconocido = new ReporteEgresoPorRubroDTO();
                Desconocido.Rubro = "Desconocido";
                Desconocido.EmpresaSede = "Desconocido";

                ReporteEgresoPorRubroDTO Inversiones = new ReporteEgresoPorRubroDTO();
                Inversiones.Rubro = "Inversiones";
                Inversiones.EmpresaSede = "Desconocido";

                ReporteEgresoPorRubroDTO CostosFijos = new ReporteEgresoPorRubroDTO();
                CostosFijos.Rubro = "Costos Fijos";
                CostosFijos.EmpresaSede = "Desconocido";

                ReporteEgresoPorRubroDTO Planilla = new ReporteEgresoPorRubroDTO();
                Planilla.Rubro = "Planilla";
                Planilla.EmpresaSede = "Desconocido";

                ReporteEgresoPorRubroDTO CostosVariables = new ReporteEgresoPorRubroDTO();
                CostosVariables.Rubro = "Costos Variables";
                CostosVariables.EmpresaSede = "Desconocido";

                ReporteEgresoPorRubroDTO Dividendos = new ReporteEgresoPorRubroDTO();
                Dividendos.Rubro = "Dividendos";
                Dividendos.EmpresaSede = "Desconocido";

                ReporteEgresoPorRubroDTO Participaciones = new ReporteEgresoPorRubroDTO();
                Participaciones.Rubro = "Participaciones";
                Participaciones.EmpresaSede = "Desconocido";

                ReporteEgresoPorRubroDTO Impuestos = new ReporteEgresoPorRubroDTO();
                Impuestos.Rubro = "Impuestos";
                Impuestos.EmpresaSede = "Desconocido";

                ReporteEgresoPorRubroDTO Publicidad = new ReporteEgresoPorRubroDTO();
                Publicidad.Rubro = "Publicidad";
                Publicidad.EmpresaSede = "Desconocido";

                ReporteEgresoPorRubroDTO Devoluciones = new ReporteEgresoPorRubroDTO();
                Devoluciones.Rubro = "Devoluciones";
                Devoluciones.EmpresaSede = "Desconocido";


                if (Filtro.Sedes.Equals(""))
                {
                    SedeRepositorio _repoSede = new SedeRepositorio(_integraDBContext);
                    var ListaSedes = _repoSede.ObtenerListaSedesSegunFur();

                    for (int i = 0; i < ListaSedes.Count; ++i)
                        if (i == 0) Filtro.Sedes += ListaSedes[i].Id;
                        else Filtro.Sedes += ("," + ListaSedes[i].Id);

                }

                var ListaDesdeDB = _repoComprobantePagoPorFur.ObtenerDatosReporteEgresosPorRubro(Filtro.Sedes, Filtro.Anio);
                for (int i = 0; i < ListaDesdeDB.Count; ++i)
                {
                    switch (ListaDesdeDB[i].IdRubro)
                    {
                        case 1:
                            CopiarValores(ListaDesdeDB[i], GastoFinanciero);
                            break;
                        case 2:
                            CopiarValores(ListaDesdeDB[i], Inversiones);
                            break;
                        case 3:
                            CopiarValores(ListaDesdeDB[i], CostosFijos);
                            break;
                        case 4:
                            CopiarValores(ListaDesdeDB[i], Planilla);
                            break;
                        case 5:
                            CopiarValores(ListaDesdeDB[i], CostosVariables);
                            break;
                        case 6:
                            CopiarValores(ListaDesdeDB[i], Dividendos);
                            break;
                        case 7:
                            CopiarValores(ListaDesdeDB[i], Participaciones);
                            break;
                        case 8:
                            CopiarValores(ListaDesdeDB[i], Impuestos);
                            break;
                        case 9:
                            CopiarValores(ListaDesdeDB[i], Publicidad);
                            break;
                        case 10:
                            CopiarValores(ListaDesdeDB[i], Devoluciones);
                            break;
                        case 0:
                            CopiarValores(ListaDesdeDB[i], Desconocido);
                            break;
                        default:
                            throw new Exception("Rubro Id=" + ListaDesdeDB[i].IdRubro + " No Considerado, agregar nuevo case en switch");
                    }
                }



                List<ReporteEgresoPorRubroDTO> ReporteEgresoPorRubro = new List<ReporteEgresoPorRubroDTO>();
                ReporteEgresoPorRubro.Add(GastoFinanciero);
                ReporteEgresoPorRubro.Add(Inversiones);
                ReporteEgresoPorRubro.Add(CostosFijos);
                ReporteEgresoPorRubro.Add(Planilla);
                ReporteEgresoPorRubro.Add(CostosVariables);
                ReporteEgresoPorRubro.Add(Dividendos);
                ReporteEgresoPorRubro.Add(Participaciones);
                ReporteEgresoPorRubro.Add(Impuestos);
                ReporteEgresoPorRubro.Add(Publicidad);
                ReporteEgresoPorRubro.Add(Devoluciones);
                ReporteEgresoPorRubro.Add(Desconocido);


                ReporteEgresoPorRubroDTO Total = new ReporteEgresoPorRubroDTO();
                Total.Rubro = "Total por Mes";

                for (int i = 0; i < ReporteEgresoPorRubro.Count; ++i)
                {
                    Total.Enero += ReporteEgresoPorRubro[i].Enero;
                    Total.Febrero += ReporteEgresoPorRubro[i].Febrero;
                    Total.Marzo += ReporteEgresoPorRubro[i].Marzo;
                    Total.Abril += ReporteEgresoPorRubro[i].Abril;
                    Total.Mayo += ReporteEgresoPorRubro[i].Mayo;
                    Total.Junio += ReporteEgresoPorRubro[i].Junio;
                    Total.Julio += ReporteEgresoPorRubro[i].Julio;
                    Total.Agosto += ReporteEgresoPorRubro[i].Agosto;
                    Total.Septiembre += ReporteEgresoPorRubro[i].Septiembre;
                    Total.Octubre += ReporteEgresoPorRubro[i].Octubre;
                    Total.Noviembre += ReporteEgresoPorRubro[i].Noviembre;
                    Total.Diciembre += ReporteEgresoPorRubro[i].Diciembre;
                    Total.Total += ReporteEgresoPorRubro[i].Total;
                }
                ReporteEgresoPorRubro.Add(Total);

                return Ok(ReporteEgresoPorRubro);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [Route("[Action]")]
        [HttpPost]
        public ActionResult VizualizarDesgloseReporteEgresoPorRubro([FromBody] FiltroDesgloseReporteEgresoPorRubroDTO Filtro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ComprobantePagoPorFurRepositorio _repoComprobantePagoPorFur = new ComprobantePagoPorFurRepositorio(_integraDBContext);
                var ListaDesdeDB = _repoComprobantePagoPorFur.ObtenerDesgloceReporteEgresosPorRubro(Filtro.Sedes, Filtro.Anio, Filtro.IdRubro);
                return Ok(ListaDesdeDB);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        private void CopiarValores(ReporteEgresoPorRubroDTO origen, ReporteEgresoPorRubroDTO destino)
        {
            destino.Enero = origen.Enero;
            destino.Febrero = origen.Febrero;
            destino.Marzo = origen.Marzo;
            destino.Abril = origen.Abril;
            destino.Mayo = origen.Mayo;
            destino.Junio = origen.Junio;
            destino.Julio = origen.Julio;
            destino.Agosto = origen.Agosto;
            destino.Septiembre = origen.Septiembre;
            destino.Octubre = origen.Octubre;
            destino.Noviembre = origen.Noviembre;
            destino.Diciembre = origen.Diciembre;
            destino.IdRubro =  origen.IdRubro;
            destino.IdSede = origen.IdSede;
            destino.EmpresaSede = origen.EmpresaSede;
            destino.Total = origen.Total;
        }
    }
}
