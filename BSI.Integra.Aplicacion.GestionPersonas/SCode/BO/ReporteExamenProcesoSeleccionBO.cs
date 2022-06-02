using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.GestionPersonas.Repositorio;
using BSI.Integra.Persistencia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BSI.Integra.Aplicacion.GestionPersonas.BO
{
    /// BO: ReporteExamenProcesoSeleccionBO
    /// Autor: Luis Huallpa - Britsel Calluchi - Edgar Serruto.
    /// Fecha: 27/08/2021
    /// <summary>
    /// Gestiona funciones de Reporte de Examen de Proceso de Selección
    /// </summary>
    public class ReporteExamenProcesoSeleccionBO
    {
        /// Propiedades		            Significado
        /// -------------	            ----------------------------------------------
        /// RedondeoGeneral             Variable de redondeo de puntajes para interfaz
        private ExamenRepositorio _repExamen;
        private integraDBContext _integraDBContext;
        private int RedondeoGeneral = 0;

        public ReporteExamenProcesoSeleccionBO()
        {

        }
        public ReporteExamenProcesoSeleccionBO(integraDBContext IntegraDBContext)
        {
            _integraDBContext = IntegraDBContext;
            _repExamen = new ExamenRepositorio(_integraDBContext);
        }
        public List<ProcesoSelecionGmatPmaDTO> ObtenerReporteGmatPma(ReporteProcesoSeleccionFiltroDTO filtro)
        {
            try
            {
                List<ReporteGmatPmaDTO> reporte;
                if (filtro.EstadoFiltro.Equals("1"))
                {
                    reporte = _repExamen.ObtenerReporteGmat(filtro);
                }
                else
                {
                    reporte = _repExamen.ObtenerReporteGmatPmaPostulante(filtro);
                }

                var reporteDTO = (from x in reporte
                                  select new ProcesoSelecionGmatPmaDTO
                                  {
                                      IdPostulante = x.IdPostulante,
                                      Postulante = x.Postulante,
                                      Edad = x.Edad,
                                      Examen = x.Examen,
                                      Titulo = x.Titulo,
                                      Orden = x.Orden,
                                      Nombre = x.Nombre,
                                      Registro = x.Registro
                                  }).ToList();
                return reporteDTO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<ProcesoSelecionPsicologicoDTO> ObtenerReporteIsraOptimismoNeopir(ReporteProcesoSeleccionFiltroDTO filtro)
        {
            try
            {
                List<ProcesoSelecionPsicologicoDTO> reporte;
                if (filtro.EstadoFiltro.Equals("1"))
                {
                    reporte = _repExamen.ObtenerReporteIsraOptimismoNeopir(filtro);
                }
                else
                {
                    reporte = _repExamen.ObtenerReporteIsraOptimismoNeopirPostulante(filtro);
                }

                var reporteDTO = (from x in reporte
                                  select new ProcesoSelecionPsicologicoDTO
                                  {
                                      IdPostulante = x.IdPostulante,
                                      Postulante = x.Postulante,
                                      Edad = x.Edad,
                                      Titulo = x.Titulo,
                                      NombreDimension = x.NombreDimension,
                                      Registro = x.Registro,
                                      Valor = x.Valor,
                                      Orden = x.Orden
                                  }).ToList();
                return reporteDTO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<ProcesoSelecionExamenesCompletosDTO> ObtenerReporteExamenes(ReporteExamenPostulanteDTO filtro)
        {
            try
            {
                FormulaPuntajeRepositorio repFormulaPuntajeRep = new FormulaPuntajeRepositorio(_integraDBContext);
                ExamenTestRepositorio repExamenTestRep = new ExamenTestRepositorio(_integraDBContext);
                ProcesoSeleccionPuntajeCalificacionRepositorio repProcesoSeleccionPuntajeCalificacionRep = new ProcesoSeleccionPuntajeCalificacionRepositorio(_integraDBContext);
                GrupoComponenteEvaluacionRepositorio repGrupoComponenteEvaluacionRep = new GrupoComponenteEvaluacionRepositorio(_integraDBContext);
                AsignacionPreguntaExamenRepositorio repAsignacionExamenRep = new AsignacionPreguntaExamenRepositorio(_integraDBContext);
                ExamenRepositorio repExamenRep = new ExamenRepositorio(_integraDBContext);
                CentilRepositorio repCentilRep = new CentilRepositorio(_integraDBContext);

                List<DatosExamenPostulanteDTO> reporte;

                if (filtro.EstadoFiltro.Equals("1"))
                {
                    filtro.Postulante = null;
                    reporte = _repExamen.ObtenerReporteExamenPostulante(filtro);
                }
                else
                {
                    filtro.FechaFin = DateTime.Now;
                    filtro.FechaInicio = new DateTime(1900, 12, 31);
                    filtro.IdPuesto = null;
                    filtro.IdSede = null;
                    reporte = _repExamen.ObtenerReporteExamenPostulante(filtro);
                }

                var listaPostulante = reporte.GroupBy(u => new { u.Postulante, u.NombreProceso, u.IdPostulante })
                    .Select(group =>
                    new DatosNotaPorPostulanteDTO
                    {
                        IdPostulante = group.Key.IdPostulante
                        ,
                        Postulante = group.Key.Postulante
                        ,
                        NombreProceso = group.Key.NombreProceso
                        ,
                        ListaNotas = group.Select(x => new NotaPostulanteDTO { IdProceso = x.IdProceso, ProcesoSeleccion = x.NombreProceso, IdSexo = x.IdSexo, IdEvaluacion = x.IdEvaluacion, IdGrupo = x.IdGrupo, IdExamen = x.IdExamen, NombreEvaluacion = x.NombreEvaluacion, NombreExamen = x.NombreExamen, NombreGrupo = x.NombreGrupo, Puntaje = x.Puntaje, IdCategoria = x.IdCategoria, IdEtapa = x.IdEtapa, NombreCategoria = x.NombreCategoria, NombreEtapa = x.NombreEtapa, FactorComponente = x.FactorComponente, FactorEvaluacion = x.FactorEvaluacion, FactorGrupo = x.FactorGrupo, IdFormulaComponente = x.IdFormulaComponente, IdFormulaEvaluacion = x.IdFormulaEvaluacion, IdFormulaGrupo = x.IdFormulaGrupo }).ToList()
                    }).ToList();

                List<ProcesoSelecionExamenesCompletosDTO> listaNotasProcesoSeleccion = new List<ProcesoSelecionExamenesCompletosDTO>();
                Decimal count = 0.00M;

                foreach (var postulante in listaPostulante)
                {
                    count = 0.00M;
                    List<int?> ListaEvaluacion = postulante.ListaNotas.Where(x => x.IdEvaluacion != null).Select(x => x.IdEvaluacion).Distinct().ToList();

                    var grupoEvaluacion = postulante.ListaNotas.GroupBy(u => new { u.IdEvaluacion, u.NombreEvaluacion })
                        .Select(group => new EvaluacionPostulanteDTO
                        {
                            IdEvaluacion = group.Key.IdEvaluacion
                            ,
                            ListaComponentesEvaluacion = group.Select(x => new NotaPostulanteDTO { IdProceso = x.IdProceso, ProcesoSeleccion = x.ProcesoSeleccion, IdSexo = x.IdSexo, IdEvaluacion = x.IdEvaluacion, IdGrupo = x.IdGrupo, IdExamen = x.IdExamen, NombreEvaluacion = x.NombreEvaluacion, NombreExamen = x.NombreExamen, NombreGrupo = x.NombreGrupo, Puntaje = x.Puntaje, IdCategoria = x.IdCategoria, IdEtapa = x.IdEtapa, NombreCategoria = x.NombreCategoria, NombreEtapa = x.NombreEtapa, FactorComponente = x.FactorComponente, FactorEvaluacion = x.FactorEvaluacion, FactorGrupo = x.FactorGrupo, IdFormulaComponente = x.IdFormulaComponente, IdFormulaEvaluacion = x.IdFormulaEvaluacion, IdFormulaGrupo = x.IdFormulaGrupo }).ToList()
                        }
                        ).ToList();
                    foreach (var Evaluacion in grupoEvaluacion)
                    {
                        var CalificacionTotal = repExamenTestRep.GetBy(x => x.Estado == true && x.Id == Evaluacion.IdEvaluacion).FirstOrDefault();
                        bool EsAgrupada = Evaluacion.ListaComponentesEvaluacion.Where(x => x.IdGrupo != null).Count() >= 1;
                        ProcesoSelecionExamenesCompletosDTO eval = new ProcesoSelecionExamenesCompletosDTO();

                        //Inserta la Nota por Evaluacion, su nota total y calculada
                        if (CalificacionTotal != null && CalificacionTotal.CalificarEvaluacion == true)
                        {
                            var FormulaPuntajeEvaluacion = repFormulaPuntajeRep.FirstById(CalificacionTotal.IdFormulaPuntaje.Value);
                            if (FormulaPuntajeEvaluacion.Nombre.Contains("(Puntaje componentes)"))
                            {
                                eval = new ProcesoSelecionExamenesCompletosDTO
                                {
                                    IdProcesoSeleccion = Evaluacion.ListaComponentesEvaluacion[0].IdProceso,
                                    ProcesoSeleccion = Evaluacion.ListaComponentesEvaluacion[0].ProcesoSeleccion,
                                    IdPostulante = postulante.IdPostulante,
                                    Postulante = postulante.Postulante,
                                    Edad = 24,
                                    Examen = null,
                                    IdCategoria = Evaluacion.ListaComponentesEvaluacion[0].IdCategoria,
                                    Categoria = Evaluacion.ListaComponentesEvaluacion[0].NombreCategoria,
                                    IdExamen = 0,
                                    IdGrupo = 0,
                                    IdEvaluacion = Evaluacion.ListaComponentesEvaluacion[0].IdEvaluacion,
                                    Evaluacion = Evaluacion.ListaComponentesEvaluacion[0].NombreEvaluacion,
                                    Grupo = null,
                                    IdEtapa = Evaluacion.ListaComponentesEvaluacion[0].IdEtapa,
                                    Etapa = Evaluacion.ListaComponentesEvaluacion[0].NombreEtapa,
                                    Orden = Convert.ToInt32(String.Concat(Evaluacion.ListaComponentesEvaluacion[0].IdEvaluacion.ToString(), "0", "0")),
                                    Registro = FormulaPuntajeEvaluacion.Nombre.ToUpper().Contains("SUMAR") ? Evaluacion.ListaComponentesEvaluacion.Sum(x => x.Puntaje * x.FactorComponente).ToString() : (Evaluacion.ListaComponentesEvaluacion.Sum(x => x.Puntaje * x.FactorComponente) / Evaluacion.ListaComponentesEvaluacion.Count()).ToString(),
                                    EsAprobado = false,
                                    CalificaPorCentil = false,
                                    IdSexo = Evaluacion.ListaComponentesEvaluacion[0].IdSexo
                                };
                                count = count + 1M;
                                eval.OrdenReal = count;
                                listaNotasProcesoSeleccion.Add(eval);
                            }
                            else
                            {
                                List<ProcesoSelecionExamenesCompletosDTO> listaGrupo = new List<ProcesoSelecionExamenesCompletosDTO>();
                                foreach (var componente in Evaluacion.ListaComponentesEvaluacion)
                                {
                                    var eval1 = listaGrupo.Where(x => x.IdPostulante == postulante.IdPostulante && x.IdEvaluacion == componente.IdEvaluacion && x.IdGrupo == componente.IdGrupo).FirstOrDefault();

                                    if (eval1 == null)
                                    {
                                        eval = new ProcesoSelecionExamenesCompletosDTO
                                        {
                                            IdProcesoSeleccion = Evaluacion.ListaComponentesEvaluacion[0].IdProceso,
                                            ProcesoSeleccion = Evaluacion.ListaComponentesEvaluacion[0].ProcesoSeleccion,
                                            IdPostulante = postulante.IdPostulante,
                                            Postulante = postulante.Postulante,
                                            Edad = 24,
                                            Examen = null,
                                            IdExamen = 0,
                                            IdGrupo = componente.IdGrupo,
                                            IdEvaluacion = componente.IdEvaluacion,
                                            Evaluacion = componente.NombreEvaluacion,
                                            Grupo = componente.FactorComponente.ToString(),
                                            Orden = Convert.ToInt32(String.Concat(componente.IdEvaluacion, componente.IdGrupo == null ? "0" : componente.IdGrupo.ToString(), "0")),
                                            Registro = componente.Puntaje.ToString(),
                                            EsAprobado = false,
                                            CalificaPorCentil = false
                                        };
                                        listaGrupo.Add(eval);
                                    }
                                    else
                                    {
                                        listaGrupo.Where(x => x.IdPostulante == postulante.IdPostulante && x.IdEvaluacion == componente.IdEvaluacion && x.IdGrupo == componente.IdGrupo).FirstOrDefault().Registro = (Convert.ToDecimal(eval1.Registro, null) + componente.Puntaje).ToString();
                                    }

                                }
                                eval = new ProcesoSelecionExamenesCompletosDTO
                                {
                                    IdProcesoSeleccion = Evaluacion.ListaComponentesEvaluacion[0].IdProceso,
                                    ProcesoSeleccion = Evaluacion.ListaComponentesEvaluacion[0].ProcesoSeleccion,
                                    IdPostulante = postulante.IdPostulante,
                                    Postulante = postulante.Postulante,
                                    Edad = 24,
                                    Examen = null,
                                    Categoria = Evaluacion.ListaComponentesEvaluacion[0].NombreCategoria,
                                    IdCategoria = Evaluacion.ListaComponentesEvaluacion[0].IdCategoria,
                                    IdExamen = 0,
                                    IdGrupo = 0,
                                    IdEvaluacion = Evaluacion.ListaComponentesEvaluacion[0].IdEvaluacion,
                                    Evaluacion = Evaluacion.ListaComponentesEvaluacion[0].NombreEvaluacion,
                                    Grupo = null,
                                    Etapa = Evaluacion.ListaComponentesEvaluacion[0].NombreEtapa,
                                    IdEtapa = Evaluacion.ListaComponentesEvaluacion[0].IdEtapa,
                                    Orden = Convert.ToInt32(String.Concat(Evaluacion.ListaComponentesEvaluacion[0].IdEvaluacion.ToString(), "0", "0")),
                                    Registro = FormulaPuntajeEvaluacion.Nombre.ToUpper().Contains("SUMAR") ? listaGrupo.Sum(x => Convert.ToDecimal(x.Registro, null) * Convert.ToDecimal(x.Grupo, null)).ToString() : (listaGrupo.Sum(x => Convert.ToDecimal(x.Registro, null) * Convert.ToDecimal(x.Grupo, null)) / Convert.ToDecimal(listaGrupo.Count(), null)).ToString(),
                                    EsAprobado = false,
                                    CalificaPorCentil = false,
                                    IdSexo = Evaluacion.ListaComponentesEvaluacion[0].IdSexo
                                };
                                count = count + 1M;
                                eval.OrdenReal = count;
                                listaNotasProcesoSeleccion.Add(eval);
                            }

                        }
                        else
                        {
                            foreach (var componente in Evaluacion.ListaComponentesEvaluacion)
                            {
                                //Inserta los Grupos de Componentes con su Calificacion
                                if (EsAgrupada == true)
                                {
                                    var eval1 = listaNotasProcesoSeleccion.Where(x => x.IdPostulante == postulante.IdPostulante && x.IdEvaluacion == componente.IdEvaluacion && x.IdGrupo == componente.IdGrupo).FirstOrDefault();

                                    if (eval1 == null)
                                    {
                                        eval = new ProcesoSelecionExamenesCompletosDTO
                                        {
                                            IdProcesoSeleccion = componente.IdProceso,
                                            ProcesoSeleccion = componente.ProcesoSeleccion,
                                            IdPostulante = postulante.IdPostulante,
                                            Postulante = postulante.Postulante,
                                            Edad = 24,
                                            Examen = null,
                                            IdCategoria = componente.IdCategoria,
                                            Categoria = componente.NombreCategoria,
                                            IdExamen = 0,
                                            IdGrupo = componente.IdGrupo,
                                            IdEvaluacion = componente.IdEvaluacion,
                                            Evaluacion = componente.NombreEvaluacion,
                                            Grupo = componente.NombreGrupo,
                                            Etapa = componente.NombreEtapa,
                                            IdEtapa = componente.IdEtapa,
                                            Orden = Convert.ToInt32(String.Concat(componente.IdEvaluacion, componente.IdGrupo == null ? "0" : componente.IdGrupo.ToString(), "0")),
                                            Registro = (componente.Puntaje * componente.FactorComponente).ToString(),
                                            //componente.Puntaje.ToString(),
                                            EsAprobado = false,
                                            CalificaPorCentil = false,
                                            IdSexo = componente.IdSexo,
                                            OrdenReal = count++
                                        };
                                        count = count + 1M;
                                        eval.OrdenReal = count;
                                        listaNotasProcesoSeleccion.Add(eval);
                                    }
                                    else
                                    {
                                        listaNotasProcesoSeleccion.Where(x => x.IdPostulante == postulante.IdPostulante && x.IdEvaluacion == componente.IdEvaluacion && x.IdGrupo == componente.IdGrupo).FirstOrDefault().Registro = (Convert.ToDecimal(eval1.Registro, null) + (componente.Puntaje * componente.FactorComponente)).ToString();
                                    }
                                }
                                //Inserta los Componentes con su Calificacion respectiva
                                else
                                {
                                    eval = new ProcesoSelecionExamenesCompletosDTO
                                    {
                                        IdProcesoSeleccion = componente.IdProceso,
                                        ProcesoSeleccion = componente.ProcesoSeleccion,
                                        IdPostulante = postulante.IdPostulante,
                                        Postulante = postulante.Postulante,
                                        Edad = 24,
                                        Examen = componente.NombreExamen,
                                        IdCategoria = componente.IdCategoria,
                                        Categoria = componente.NombreCategoria,
                                        IdExamen = componente.IdExamen,
                                        IdGrupo = componente.IdGrupo == null ? 0 : componente.IdGrupo,
                                        IdEvaluacion = componente.IdEvaluacion,
                                        Evaluacion = componente.NombreEvaluacion,
                                        Grupo = componente.NombreGrupo,
                                        Etapa = componente.NombreEtapa,
                                        IdEtapa = componente.IdEtapa,
                                        Orden = Convert.ToInt32(String.Concat(componente.IdEvaluacion, componente.IdGrupo == null ? "0" : componente.IdGrupo.ToString(), componente.IdExamen == null ? "0" : componente.IdExamen.ToString())),
                                        Registro = componente.Puntaje.ToString(),
                                        EsAprobado = false,
                                        CalificaPorCentil = false,
                                        IdSexo = componente.IdSexo
                                    };
                                    count = count + 1M;
                                    eval.OrdenReal = count;
                                    listaNotasProcesoSeleccion.Add(eval);
                                }
                            }
                        }

                    }
                }


                foreach (var item in listaNotasProcesoSeleccion)
                {
                    item.OrdenReal = item.Orden;
                    var puntaje = Convert.ToDecimal(item.Registro, null);
                    if (item.Evaluacion != null && (item.IdEvaluacion == 53 || item.Evaluacion.Equals("GRADUATE MANAGEMENT ADMISSION TEST V2")))
                    {
                        var countPregunta = Convert.ToDecimal(repAsignacionExamenRep.GetBy(x => x.IdExamen == item.IdExamen).Count(), null);
                        item.Registro = Math.Round((puntaje * 100.0M) / countPregunta).ToString();
                    }

                    // Cambia los puntajes de las Evaluaciones
                    if (item.IdEvaluacion != 0 && item.IdGrupo == 0 && item.IdExamen == 0)
                    {
                        var calificacion = repProcesoSeleccionPuntajeCalificacionRep.FirstBy(x => x.Estado == true && x.IdExamenTest == item.IdEvaluacion && x.IdGrupoComponenteEvaluacion == null && x.IdExamen == null && x.IdProcesoSeleccion == item.IdProcesoSeleccion);

                        if (calificacion == null)
                        {
                            item.EsAprobado = false;
                            item.NotaAprobatoria = "N.A";
                        }
                        else
                        {
                            // La Calificacion debe de ser Igual a Valor minimo del componente o evaluacion
                            item.NotaAprobatoria = calificacion.PuntajeMinimo.ToString();
                            if (calificacion.IdProcesoSeleccionRango == 1)
                            {
                                item.Simbolo = "=";
                                if (calificacion.CalificaPorCentil)
                                {
                                    item.CalificaPorCentil = true;
                                    var centil = repCentilRep.FirstBy(x => x.Estado == true && x.IdExamenTest == item.IdEvaluacion && x.IdSexo == (x.IdSexo == null ? null : item.IdSexo) && (puntaje >= x.ValorMinimo && puntaje <= x.ValorMaximo));
                                    if (centil == null)
                                    {
                                        item.Registro = "SIN CENTIL";
                                        item.EsAprobado = false;
                                    }
                                    else
                                    {
                                        item.Registro = centil.Centil.ToString();
                                        if (calificacion.PuntajeMinimo == Convert.ToDecimal(item.Registro, null))
                                        {
                                            item.EsAprobado = true;
                                        }
                                        if (calificacion.EsCalificable == false)
                                        {
                                            item.EsAprobado = false;
                                            item.NotaAprobatoria = "N.A";
                                            item.Simbolo = "";
                                        }
                                    }
                                }
                                else
                                {
                                    if (calificacion.PuntajeMinimo == Convert.ToDecimal(item.Registro, null))
                                    {
                                        item.EsAprobado = true;
                                        item.CalificaPorCentil = false;
                                    }
                                    if (calificacion.EsCalificable == false)
                                    {
                                        item.EsAprobado = false;
                                        item.NotaAprobatoria = "N.A";
                                        item.Simbolo = "";
                                    }
                                }
                            }

                            // La Calificacion debe de ser Mayor a Valor Minimo
                            if (calificacion.IdProcesoSeleccionRango == 2)
                            {
                                item.Simbolo = ">";
                                if (calificacion.CalificaPorCentil)
                                {
                                    item.CalificaPorCentil = true;
                                    var centil = repCentilRep.FirstBy(x => x.Estado == true && x.IdExamenTest == item.IdEvaluacion && x.IdSexo == (x.IdSexo == null ? null : item.IdSexo) && (puntaje >= x.ValorMinimo && puntaje <= x.ValorMaximo));
                                    if (centil == null)
                                    {
                                        item.Registro = "SIN CENTIL";
                                        item.EsAprobado = false;
                                    }
                                    else
                                    {
                                        item.Registro = centil.Centil.ToString();
                                        if (Convert.ToDecimal(item.Registro, null) > calificacion.PuntajeMinimo)
                                        {
                                            item.EsAprobado = true;
                                        }
                                        if (calificacion.EsCalificable == false)
                                        {
                                            item.EsAprobado = false;
                                            item.NotaAprobatoria = "N.A";
                                            item.Simbolo = "";
                                        }
                                    }
                                }
                                else
                                {
                                    if (Convert.ToDecimal(item.Registro, null) > calificacion.PuntajeMinimo)
                                    {
                                        item.EsAprobado = true;
                                        item.CalificaPorCentil = false;
                                    }
                                    if (calificacion.EsCalificable == false)
                                    {
                                        item.EsAprobado = false;
                                        item.NotaAprobatoria = "N.A";
                                        item.Simbolo = "";
                                    }
                                }
                            }

                            // La Calificacion debe de ser Menor a Valor Minimo
                            if (calificacion.IdProcesoSeleccionRango == 9)
                            {
                                item.Simbolo = "<";
                                if (calificacion.CalificaPorCentil)
                                {
                                    item.CalificaPorCentil = true;
                                    var centil = repCentilRep.FirstBy(x => x.Estado == true && x.IdExamenTest == item.IdEvaluacion && x.IdSexo == (x.IdSexo == null ? null : item.IdSexo) && (puntaje >= x.ValorMinimo && puntaje <= x.ValorMaximo));
                                    if (centil == null)
                                    {
                                        item.Registro = "SIN CENTIL";
                                        item.EsAprobado = false;
                                    }
                                    else
                                    {
                                        item.Registro = centil.Centil.ToString();
                                        if (Convert.ToDecimal(item.Registro, null) < calificacion.PuntajeMinimo)
                                        {
                                            item.EsAprobado = true;
                                        }
                                        if (calificacion.EsCalificable == false)
                                        {
                                            item.EsAprobado = false;
                                            item.NotaAprobatoria = "N.A";
                                            item.Simbolo = "";
                                        }
                                    }
                                }
                                else
                                {
                                    if (Convert.ToDecimal(item.Registro, null) < calificacion.PuntajeMinimo)
                                    {
                                        item.EsAprobado = true;
                                        item.CalificaPorCentil = false;
                                    }
                                    if (calificacion.EsCalificable == false)
                                    {
                                        item.EsAprobado = false;
                                        item.NotaAprobatoria = "N.A";
                                        item.Simbolo = "";
                                    }
                                }
                            }

                            // La Calificacion debe de ser Mayor Igual a Valor Minimo
                            if (calificacion.IdProcesoSeleccionRango == 12)
                            {
                                item.Simbolo = ">=";
                                if (calificacion.CalificaPorCentil)
                                {
                                    item.CalificaPorCentil = true;
                                    var centil = repCentilRep.FirstBy(x => x.Estado == true && x.IdExamenTest == item.IdEvaluacion && x.IdSexo == (x.IdSexo == null ? null : item.IdSexo) && (puntaje >= x.ValorMinimo && puntaje <= x.ValorMaximo));
                                    if (centil == null)
                                    {
                                        item.Registro = "SIN CENTIL";
                                        item.EsAprobado = false;
                                    }
                                    else
                                    {
                                        item.Registro = centil.Centil.ToString();
                                        if (Convert.ToDecimal(item.Registro, null) >= calificacion.PuntajeMinimo)
                                        {
                                            item.EsAprobado = true;
                                        }
                                        if (calificacion.EsCalificable == false)
                                        {
                                            item.EsAprobado = false;
                                            item.NotaAprobatoria = "N.A";
                                            item.Simbolo = "";
                                        }
                                    }
                                }
                                else
                                {
                                    if (Convert.ToDecimal(item.Registro, null) >= calificacion.PuntajeMinimo)
                                    {
                                        item.EsAprobado = true;
                                        item.CalificaPorCentil = false;
                                    }
                                    if (calificacion.EsCalificable == false)
                                    {
                                        item.EsAprobado = false;
                                        item.NotaAprobatoria = "N.A";
                                        item.Simbolo = "";
                                    }
                                }
                            }

                            // La Calificacion debe de ser Menor Igual a Valor Minimo
                            if (calificacion.IdProcesoSeleccionRango == 13)
                            {
                                item.Simbolo = "<=";
                                if (calificacion.CalificaPorCentil)
                                {
                                    item.CalificaPorCentil = true;
                                    var centil = repCentilRep.FirstBy(x => x.Estado == true && x.IdExamenTest == item.IdEvaluacion && x.IdSexo == (x.IdSexo == null ? null : item.IdSexo) && (puntaje >= x.ValorMinimo && puntaje <= x.ValorMaximo));
                                    if (centil == null)
                                    {
                                        item.Registro = "SIN CENTIL";
                                        item.EsAprobado = false;
                                    }
                                    else
                                    {
                                        item.Registro = centil.Centil.ToString();
                                        if (Convert.ToDecimal(item.Registro, null) <= calificacion.PuntajeMinimo)
                                        {
                                            item.EsAprobado = true;
                                        }
                                        if (calificacion.EsCalificable == false)
                                        {
                                            item.EsAprobado = false;
                                            item.NotaAprobatoria = "N.A";
                                            item.Simbolo = "";
                                        }
                                    }
                                }
                                else
                                {
                                    if (Convert.ToDecimal(item.Registro, null) <= calificacion.PuntajeMinimo)
                                    {
                                        item.EsAprobado = true;
                                        item.CalificaPorCentil = false;
                                    }
                                    if (calificacion.EsCalificable == false)
                                    {
                                        item.EsAprobado = false;
                                        item.NotaAprobatoria = "N.A";
                                        item.Simbolo = "";
                                    }
                                }
                            }


                            if (calificacion.IdProcesoSeleccionRango == null || calificacion.IdProcesoSeleccionRango == 0)
                            {
                                if (calificacion.CalificaPorCentil)
                                {
                                    item.CalificaPorCentil = true;

                                    var centil = repCentilRep.FirstBy(x => x.Estado == true && x.IdExamenTest == item.IdEvaluacion && x.IdSexo == (x.IdSexo == null ? null : item.IdSexo) && (puntaje >= x.ValorMinimo && puntaje <= x.ValorMaximo));

                                    if (centil == null)
                                    {
                                        item.Registro = "SIN CENTIL";
                                        item.EsAprobado = false;
                                    }
                                    else
                                    {
                                        item.Registro = centil.Centil.ToString();
                                        if (Convert.ToDecimal(item.Registro, null) >= (calificacion.PuntajeMinimo == null ? 0 : calificacion.PuntajeMinimo))
                                        {
                                            item.EsAprobado = true;
                                        }
                                        if (calificacion.EsCalificable == false)
                                        {
                                            item.EsAprobado = false;
                                            item.NotaAprobatoria = "N.A";
                                            item.Simbolo = "";
                                        }
                                    }
                                }
                                else
                                {
                                    if (Convert.ToDecimal(item.Registro, null) >= (calificacion.PuntajeMinimo == null ? 0 : calificacion.PuntajeMinimo))
                                    {
                                        item.EsAprobado = true;
                                        item.CalificaPorCentil = false;
                                    }
                                    if (calificacion.EsCalificable == false)
                                    {
                                        item.EsAprobado = false;
                                        item.NotaAprobatoria = "N.A";
                                        item.Simbolo = "";
                                    }
                                }
                            }
                        }
                    }


                    // Cambia los puntajes de las Grupos
                    if (item.IdEvaluacion != 0 && item.IdGrupo != 0 && item.IdExamen == 0)
                    {
                        var calificacion = repProcesoSeleccionPuntajeCalificacionRep.FirstBy(x => x.Estado == true && x.IdExamenTest == item.IdEvaluacion
                                        && x.IdGrupoComponenteEvaluacion == item.IdGrupo && x.IdExamen == null && x.IdProcesoSeleccion == item.IdProcesoSeleccion);

                        if (calificacion == null)
                        {
                            item.EsAprobado = false;
                            item.NotaAprobatoria = "N.A";
                        }
                        else
                        {

                            // La Calificacion debe de ser Igual a Valor minimo del componente o evaluacion
                            item.NotaAprobatoria = calificacion.PuntajeMinimo.ToString();
                            if (calificacion.IdProcesoSeleccionRango == 1)
                            {
                                item.Simbolo = "=";
                                if (calificacion.CalificaPorCentil)
                                {
                                    item.CalificaPorCentil = true;
                                    var centil = repCentilRep.FirstBy(x => x.Estado == true && x.IdGrupoComponenteEvaluacion == item.IdGrupo && x.IdSexo == (x.IdSexo == null ? null : item.IdSexo) && (puntaje >= x.ValorMinimo && puntaje <= x.ValorMaximo));
                                    if (centil == null)
                                    {
                                        item.Registro = "SIN CENTIL";
                                        item.EsAprobado = false;
                                    }
                                    else
                                    {
                                        item.Registro = centil.Centil.ToString();
                                        if (Convert.ToDecimal(item.Registro, null) == calificacion.PuntajeMinimo)
                                        {
                                            item.EsAprobado = true;
                                        }
                                        if (calificacion.EsCalificable == false)
                                        {
                                            item.EsAprobado = false;
                                            item.NotaAprobatoria = "N.A";
                                            item.Simbolo = "";
                                        }
                                    }
                                }
                                else
                                {
                                    if (Convert.ToDecimal(item.Registro, null) == calificacion.PuntajeMinimo)
                                    {
                                        item.EsAprobado = true;
                                        item.CalificaPorCentil = false;
                                    }
                                    if (calificacion.EsCalificable == false)
                                    {
                                        item.EsAprobado = false;
                                        item.NotaAprobatoria = "N.A";
                                        item.Simbolo = "";
                                    }
                                }
                            }

                            // La Calificacion debe de ser Mayor a Valor Minimo
                            if (calificacion.IdProcesoSeleccionRango == 2)
                            {
                                item.Simbolo = ">";
                                if (calificacion.CalificaPorCentil)
                                {
                                    item.CalificaPorCentil = true;
                                    var centil = repCentilRep.FirstBy(x => x.Estado == true && x.IdGrupoComponenteEvaluacion == item.IdGrupo && x.IdSexo == (x.IdSexo == null ? null : item.IdSexo) && (puntaje >= x.ValorMinimo && puntaje <= x.ValorMaximo));
                                    if (centil == null)
                                    {
                                        item.Registro = "SIN CENTIL";
                                        item.EsAprobado = false;
                                    }
                                    else
                                    {
                                        item.Registro = centil.Centil.ToString();
                                        if (Convert.ToDecimal(item.Registro, null) > calificacion.PuntajeMinimo)
                                        {
                                            item.EsAprobado = true;
                                        }
                                        if (calificacion.EsCalificable == false)
                                        {
                                            item.EsAprobado = false;
                                            item.NotaAprobatoria = "N.A";
                                            item.Simbolo = "";
                                        }
                                    }
                                }
                                else
                                {
                                    if (Convert.ToDecimal(item.Registro, null) > calificacion.PuntajeMinimo)
                                    {
                                        item.EsAprobado = true;
                                        item.CalificaPorCentil = false;
                                    }
                                    if (calificacion.EsCalificable == false)
                                    {
                                        item.EsAprobado = false;
                                        item.NotaAprobatoria = "N.A";
                                        item.Simbolo = "";
                                    }
                                }
                            }

                            // La Calificacion debe de ser Menor a Valor Minimo
                            if (calificacion.IdProcesoSeleccionRango == 9)
                            {
                                item.Simbolo = "<";
                                if (calificacion.CalificaPorCentil)
                                {
                                    item.CalificaPorCentil = true;
                                    var centil = repCentilRep.FirstBy(x => x.Estado == true && x.IdGrupoComponenteEvaluacion == item.IdGrupo && x.IdSexo == (x.IdSexo == null ? null : item.IdSexo) && (puntaje >= x.ValorMinimo && puntaje <= x.ValorMaximo));
                                    if (centil == null)
                                    {
                                        item.Registro = "SIN CENTIL";
                                        item.EsAprobado = false;
                                    }
                                    else
                                    {
                                        item.Registro = centil.Centil.ToString();
                                        if (Convert.ToDecimal(item.Registro, null) < calificacion.PuntajeMinimo)
                                        {
                                            item.EsAprobado = true;
                                        }
                                        if (calificacion.EsCalificable == false)
                                        {
                                            item.EsAprobado = false;
                                            item.NotaAprobatoria = "N.A";
                                            item.Simbolo = "";
                                        }
                                    }
                                }
                                else
                                {
                                    if (Convert.ToDecimal(item.Registro, null) < calificacion.PuntajeMinimo)
                                    {
                                        item.EsAprobado = true;
                                        item.CalificaPorCentil = false;
                                    }
                                    if (calificacion.EsCalificable == false)
                                    {
                                        item.EsAprobado = false;
                                        item.NotaAprobatoria = "N.A";
                                        item.Simbolo = "";
                                    }
                                }
                            }

                            // La Calificacion debe de ser Mayor Igual a Valor Minimo
                            if (calificacion.IdProcesoSeleccionRango == 12)
                            {
                                item.Simbolo = ">=";
                                if (calificacion.CalificaPorCentil)
                                {
                                    item.CalificaPorCentil = true;
                                    var centil = repCentilRep.FirstBy(x => x.Estado == true && x.IdGrupoComponenteEvaluacion == item.IdGrupo && x.IdSexo == (x.IdSexo == null ? null : item.IdSexo) && (puntaje >= x.ValorMinimo && puntaje <= x.ValorMaximo));
                                    if (centil == null)
                                    {
                                        item.Registro = "SIN CENTIL";
                                        item.EsAprobado = false;
                                    }
                                    else
                                    {
                                        item.Registro = centil.Centil.ToString();
                                        if (Convert.ToDecimal(item.Registro, null) >= calificacion.PuntajeMinimo)
                                        {
                                            item.EsAprobado = true;
                                        }
                                        if (calificacion.EsCalificable == false)
                                        {
                                            item.EsAprobado = false;
                                            item.NotaAprobatoria = "N.A";
                                            item.Simbolo = "";
                                        }
                                    }
                                }
                                else
                                {
                                    if (Convert.ToDecimal(item.Registro, null) >= calificacion.PuntajeMinimo)
                                    {
                                        item.EsAprobado = true;
                                        item.CalificaPorCentil = false;
                                    }
                                    if (calificacion.EsCalificable == false)
                                    {
                                        item.EsAprobado = false;
                                        item.NotaAprobatoria = "N.A";
                                        item.Simbolo = "";
                                    }
                                }
                            }

                            // La Calificacion debe de ser Menor Igual a Valor Minimo
                            if (calificacion.IdProcesoSeleccionRango == 13)
                            {
                                item.Simbolo = "<=";
                                if (calificacion.CalificaPorCentil)
                                {
                                    item.CalificaPorCentil = true;
                                    var centil = repCentilRep.FirstBy(x => x.Estado == true && x.IdGrupoComponenteEvaluacion == item.IdGrupo && x.IdSexo == (x.IdSexo == null ? null : item.IdSexo) && (puntaje >= x.ValorMinimo && puntaje <= x.ValorMaximo));
                                    if (centil == null)
                                    {
                                        item.Registro = "SIN CENTIL";
                                        item.EsAprobado = false;
                                    }
                                    else
                                    {
                                        item.Registro = centil.Centil.ToString();
                                        if (Convert.ToDecimal(item.Registro, null) <= calificacion.PuntajeMinimo)
                                        {
                                            item.EsAprobado = true;
                                        }
                                        if (calificacion.EsCalificable == false)
                                        {
                                            item.EsAprobado = false;
                                            item.NotaAprobatoria = "N.A";
                                            item.Simbolo = "";
                                        }
                                    }
                                }
                                else
                                {
                                    if (Convert.ToDecimal(item.Registro, null) <= calificacion.PuntajeMinimo)
                                    {
                                        item.EsAprobado = true;
                                        item.CalificaPorCentil = false;
                                    }
                                    if (calificacion.EsCalificable == false)
                                    {
                                        item.EsAprobado = false;
                                        item.NotaAprobatoria = "N.A";
                                        item.Simbolo = "";
                                    }
                                }
                            }

                            if (calificacion.IdProcesoSeleccionRango == null || calificacion.IdProcesoSeleccionRango == 0)
                            {
                                if (calificacion.CalificaPorCentil)
                                {
                                    item.CalificaPorCentil = true;

                                    var centil = repCentilRep.FirstBy(x => x.Estado == true && x.IdGrupoComponenteEvaluacion == item.IdGrupo && x.IdSexo == (x.IdSexo == null ? null : item.IdSexo) && (puntaje >= x.ValorMinimo && puntaje <= x.ValorMaximo));

                                    if (centil == null)
                                    {
                                        item.Registro = "SIN CENTIL";
                                        item.EsAprobado = false;
                                    }
                                    else
                                    {
                                        item.Registro = centil.Centil.ToString();
                                        if (Convert.ToDecimal(item.Registro, null) >= (calificacion.PuntajeMinimo == null ? 0 : calificacion.PuntajeMinimo))
                                        {
                                            item.EsAprobado = true;
                                        }
                                        if (calificacion.EsCalificable == false)
                                        {
                                            item.EsAprobado = false;
                                            item.NotaAprobatoria = "N.A";
                                            item.Simbolo = "";
                                        }
                                    }
                                }
                                else
                                {
                                    if (Convert.ToDecimal(item.Registro, null) >= (calificacion.PuntajeMinimo == null ? 0 : calificacion.PuntajeMinimo))
                                    {
                                        item.EsAprobado = true;
                                        item.CalificaPorCentil = false;
                                    }
                                    if (calificacion.EsCalificable == false)
                                    {
                                        item.EsAprobado = false;
                                        item.NotaAprobatoria = "N.A";
                                        item.Simbolo = "";
                                    }
                                }
                            }

                        }
                    }


                    // Cambia los puntajes de los componentes
                    if (item.IdEvaluacion != 0 && item.IdGrupo == 0 && item.IdExamen != 0)
                    {
                        var calificacion = repProcesoSeleccionPuntajeCalificacionRep.FirstBy(x => x.Estado == true && x.IdExamenTest == item.IdEvaluacion && x.IdGrupoComponenteEvaluacion == null && x.IdExamen == item.IdExamen && x.IdProcesoSeleccion == item.IdProcesoSeleccion);

                        if (calificacion == null)
                        {
                            item.EsAprobado = false;
                            item.NotaAprobatoria = "N.A";
                        }
                        else
                        {
                            // La Calificacion debe de ser Igual a Valor minimo del componente o evaluacion
                            item.NotaAprobatoria = calificacion.PuntajeMinimo.ToString();
                            if (calificacion.IdProcesoSeleccionRango == 1)
                            {
                                item.Simbolo = "=";
                                if (calificacion.CalificaPorCentil)
                                {
                                    item.CalificaPorCentil = true;
                                    var centil = repCentilRep.FirstBy(x => x.Estado == true && x.IdExamen == item.IdExamen
                                                    && x.IdSexo == (x.IdSexo == null ? null : item.IdSexo) && (puntaje >= x.ValorMinimo && puntaje <= x.ValorMaximo));
                                    if (centil == null)
                                    {
                                        item.Registro = "SIN CENTIL";
                                        item.EsAprobado = false;
                                    }
                                    else
                                    {
                                        item.Registro = centil.Centil.ToString();
                                        if (Convert.ToDecimal(item.Registro, null) == calificacion.PuntajeMinimo)
                                        {
                                            item.EsAprobado = true;
                                        }
                                        if (calificacion.EsCalificable == false)
                                        {
                                            item.EsAprobado = false;
                                            item.NotaAprobatoria = "N.A";
                                            item.Simbolo = "";
                                        }
                                    }
                                }
                                else
                                {
                                    if (Convert.ToDecimal(item.Registro, null) == calificacion.PuntajeMinimo)
                                    {
                                        item.EsAprobado = true;
                                        item.CalificaPorCentil = false;
                                    }
                                    if (calificacion.EsCalificable == false)
                                    {
                                        item.EsAprobado = false;
                                        item.NotaAprobatoria = "N.A";
                                        item.Simbolo = "";
                                    }
                                }
                            }

                            // La Calificacion debe de ser Mayor a Valor Minimo
                            if (calificacion.IdProcesoSeleccionRango == 2)
                            {
                                item.Simbolo = ">";
                                if (calificacion.CalificaPorCentil)
                                {
                                    item.CalificaPorCentil = true;
                                    var centil = repCentilRep.FirstBy(x => x.Estado == true && x.IdExamen == item.IdExamen
                                                    && x.IdSexo == (x.IdSexo == null ? null : item.IdSexo) && (puntaje >= x.ValorMinimo && puntaje <= x.ValorMaximo));
                                    if (centil == null)
                                    {
                                        item.Registro = "SIN CENTIL";
                                        item.EsAprobado = false;
                                    }
                                    else
                                    {
                                        item.Registro = centil.Centil.ToString();
                                        if (Convert.ToDecimal(item.Registro, null) > calificacion.PuntajeMinimo)
                                        {
                                            item.EsAprobado = true;
                                        }
                                        if (calificacion.EsCalificable == false)
                                        {
                                            item.EsAprobado = false;
                                            item.NotaAprobatoria = "N.A";
                                            item.Simbolo = "";
                                        }
                                    }
                                }
                                else
                                {
                                    if (Convert.ToDecimal(item.Registro, null) > calificacion.PuntajeMinimo)
                                    {
                                        item.EsAprobado = true;
                                        item.CalificaPorCentil = false;
                                    }
                                    if (calificacion.EsCalificable == false)
                                    {
                                        item.EsAprobado = false;
                                        item.NotaAprobatoria = "N.A";
                                        item.Simbolo = "";
                                    }
                                }
                            }

                            // La Calificacion debe de ser Menor a Valor Minimo
                            if (calificacion.IdProcesoSeleccionRango == 9)
                            {
                                item.Simbolo = "<";
                                if (calificacion.CalificaPorCentil)
                                {
                                    item.CalificaPorCentil = true;
                                    var centil = repCentilRep.FirstBy(x => x.Estado == true && x.IdExamen == item.IdExamen
                                                    && x.IdSexo == (x.IdSexo == null ? null : item.IdSexo) && (puntaje >= x.ValorMinimo && puntaje <= x.ValorMaximo));
                                    if (centil == null)
                                    {
                                        item.Registro = "SIN CENTIL";
                                        item.EsAprobado = false;
                                    }
                                    else
                                    {
                                        item.Registro = centil.Centil.ToString();
                                        if (Convert.ToDecimal(item.Registro, null) < calificacion.PuntajeMinimo)
                                        {
                                            item.EsAprobado = true;
                                        }
                                        if (calificacion.EsCalificable == false)
                                        {
                                            item.EsAprobado = false;
                                            item.NotaAprobatoria = "N.A";
                                            item.Simbolo = "";
                                        }
                                    }
                                }
                                else
                                {
                                    if (Convert.ToDecimal(item.Registro, null) < calificacion.PuntajeMinimo)
                                    {
                                        item.EsAprobado = true;
                                        item.CalificaPorCentil = false;
                                    }
                                    if (calificacion.EsCalificable == false)
                                    {
                                        item.EsAprobado = false;
                                        item.NotaAprobatoria = "N.A";
                                        item.Simbolo = "";
                                    }
                                }
                            }

                            // La Calificacion debe de ser Mayor Igual a Valor Minimo
                            if (calificacion.IdProcesoSeleccionRango == 12)
                            {
                                item.Simbolo = ">=";
                                if (calificacion.CalificaPorCentil)
                                {
                                    item.CalificaPorCentil = true;
                                    var centil = repCentilRep.FirstBy(x => x.Estado == true && x.IdExamen == item.IdExamen
                                                    && x.IdSexo == (x.IdSexo == null ? null : item.IdSexo) && (puntaje >= x.ValorMinimo && puntaje <= x.ValorMaximo));
                                    if (centil == null)
                                    {
                                        item.Registro = "SIN CENTIL";
                                        item.EsAprobado = false;
                                    }
                                    else
                                    {
                                        item.Registro = centil.Centil.ToString();
                                        if (Convert.ToDecimal(item.Registro, null) >= calificacion.PuntajeMinimo)
                                        {
                                            item.EsAprobado = true;
                                        }
                                        if (calificacion.EsCalificable == false)
                                        {
                                            item.EsAprobado = false;
                                            item.NotaAprobatoria = "N.A";
                                            item.Simbolo = "";
                                        }
                                    }
                                }
                                else
                                {
                                    if (Convert.ToDecimal(item.Registro, null) >= calificacion.PuntajeMinimo)
                                    {
                                        item.EsAprobado = true;
                                        item.CalificaPorCentil = false;
                                    }
                                    if (calificacion.EsCalificable == false)
                                    {
                                        item.EsAprobado = false;
                                        item.NotaAprobatoria = "N.A";
                                        item.Simbolo = "";
                                    }
                                }
                            }

                            // La Calificacion debe de ser Menor Igual a Valor Minimo
                            if (calificacion.IdProcesoSeleccionRango == 13)
                            {
                                item.Simbolo = "<=";
                                if (calificacion.CalificaPorCentil)
                                {
                                    item.CalificaPorCentil = true;
                                    var centil = repCentilRep.FirstBy(x => x.Estado == true && x.IdExamen == item.IdExamen
                                                    && x.IdSexo == (x.IdSexo == null ? null : item.IdSexo) && (puntaje >= x.ValorMinimo && puntaje <= x.ValorMaximo));
                                    if (centil == null)
                                    {
                                        item.Registro = "SIN CENTIL";
                                        item.EsAprobado = false;
                                    }
                                    else
                                    {
                                        item.Registro = centil.Centil.ToString();
                                        if (Convert.ToDecimal(item.Registro, null) <= calificacion.PuntajeMinimo)
                                        {
                                            item.EsAprobado = true;
                                        }
                                        if (calificacion.EsCalificable == false)
                                        {
                                            item.EsAprobado = false;
                                            item.NotaAprobatoria = "N.A";
                                            item.Simbolo = "";
                                        }
                                    }
                                }
                                else
                                {
                                    if (Convert.ToDecimal(item.Registro, null) <= calificacion.PuntajeMinimo)
                                    {
                                        item.EsAprobado = true;
                                        item.CalificaPorCentil = false;
                                    }
                                    if (calificacion.EsCalificable == false)
                                    {
                                        item.EsAprobado = false;
                                        item.NotaAprobatoria = "N.A";
                                        item.Simbolo = "";
                                    }
                                }
                            }

                            if (calificacion.IdProcesoSeleccionRango == null || calificacion.IdProcesoSeleccionRango == 0)
                            {
                                if (calificacion.CalificaPorCentil)
                                {
                                    item.CalificaPorCentil = true;
                                    var centil = repCentilRep.FirstBy(x => x.Estado == true && x.IdExamen == item.IdExamen
                                                    && x.IdSexo == (x.IdSexo == null ? null : item.IdSexo) && (puntaje >= x.ValorMinimo && puntaje <= x.ValorMaximo));
                                    if (centil == null)
                                    {
                                        item.Registro = "SIN CENTIL";
                                        item.EsAprobado = false;
                                    }
                                    else
                                    {
                                        item.Registro = centil.Centil.ToString();
                                        if (Convert.ToDecimal(item.Registro, null) >= (calificacion.PuntajeMinimo == null ? 0 : calificacion.PuntajeMinimo))
                                        {
                                            item.EsAprobado = true;
                                        }
                                        if (calificacion.EsCalificable == false)
                                        {
                                            item.EsAprobado = false;
                                            item.NotaAprobatoria = "N.A";
                                            item.Simbolo = "";
                                        }
                                    }
                                }
                                else
                                {
                                    if (Convert.ToDecimal(item.Registro, null) >= (calificacion.PuntajeMinimo == null ? 0 : calificacion.PuntajeMinimo))
                                    {
                                        item.EsAprobado = true;
                                        item.CalificaPorCentil = false;
                                    }
                                    if (calificacion.EsCalificable == false)
                                    {
                                        item.EsAprobado = false;
                                        item.NotaAprobatoria = "N.A";
                                        item.Simbolo = "";
                                    }
                                }
                            }

                        }
                    }

                    if (!item.Registro.Equals("SIN CENTIL"))
                    {

                        item.Registro = (Math.Round(Convert.ToDecimal(item.Registro, null))).ToString();
                        if (item.Evaluacion != null && (item.IdEvaluacion == 53 || item.Evaluacion.Equals("GRADUATE MANAGEMENT ADMISSION TEST V2")))
                        {
                            item.Registro = item.Registro + "%";
                        }

                    }
                }

                List<ProcesoSelecionExamenesCompletosDTO> listaNotasProcesoSeleccionComplemento = new List<ProcesoSelecionExamenesCompletosDTO>();
                //Obtiene solo las evaluaciones con calificacion Total
                var Evaluaciones2 = listaNotasProcesoSeleccion.Where(x => x.IdExamen == 0 && x.IdGrupo == 0).Select(x => new ProcesoSelecionExamenesCompletosComplementoDTO { IdProcesoSeleccion = x.IdProcesoSeleccion, IdEtapa = x.IdEtapa, IdEvaluacion = x.IdEvaluacion, IdGrupo = x.IdGrupo, IdExamen = x.IdExamen, EsAprobado = x.EsAprobado, CalificaPorCentil = x.CalificaPorCentil, Orden = x.Orden, OrdenReal = x.OrdenReal }).ToList();
                var Evaluaciones = Evaluaciones2
                    .GroupBy(x => new { IdProcesoSeleccion = x.IdProcesoSeleccion, IdEtapa = x.IdEtapa, IdEvaluacion = x.IdEvaluacion, IdGrupo = x.IdGrupo, IdExamen = x.IdExamen, EsAprobado = x.EsAprobado, CalificaPorCentil = x.CalificaPorCentil, Orden = x.Orden, OrdenReal = x.OrdenReal })
                    .Select(group =>
                    new ProcesoSelecionExamenesCompletosComplementoDTO
                    {
                        IdProcesoSeleccion = group.Key.IdProcesoSeleccion
                        ,
                        IdEtapa = group.Key.IdEtapa
                        ,
                        IdEvaluacion = group.Key.IdEvaluacion
                        ,
                        IdGrupo = group.Key.IdGrupo
                        ,
                        IdExamen = group.Key.IdExamen
                        ,
                        EsAprobado = group.Key.EsAprobado
                        ,
                        CalificaPorCentil = group.Key.CalificaPorCentil
                        ,
                        Orden = group.Key.Orden
                        ,
                        OrdenReal = group.Key.OrdenReal
                    })
                    .ToList();
                // Obtiene solo las evaluaciones con calificacion agrupada
                var GrupoComponente2 = listaNotasProcesoSeleccion.Where(x => x.IdExamen == 0 && x.IdGrupo != 0).Select(x => new ProcesoSelecionExamenesCompletosComplementoDTO { IdProcesoSeleccion = x.IdProcesoSeleccion, IdEtapa = x.IdEtapa, IdEvaluacion = x.IdEvaluacion, IdGrupo = x.IdGrupo, IdExamen = x.IdExamen, EsAprobado = x.EsAprobado, CalificaPorCentil = x.CalificaPorCentil, Orden = x.Orden, OrdenReal = x.OrdenReal }).Distinct().ToList();
                var GrupoComponente = GrupoComponente2
                    .GroupBy(x => new { IdProcesoSeleccion = x.IdProcesoSeleccion, IdEtapa = x.IdEtapa, IdEvaluacion = x.IdEvaluacion, IdGrupo = x.IdGrupo, IdExamen = x.IdExamen, EsAprobado = x.EsAprobado, CalificaPorCentil = x.CalificaPorCentil, Orden = x.Orden, OrdenReal = x.OrdenReal })
                    .Select(group =>
                    new ProcesoSelecionExamenesCompletosComplementoDTO
                    {
                        IdProcesoSeleccion = group.Key.IdProcesoSeleccion
                        ,
                        IdEtapa = group.Key.IdEtapa
                        ,
                        IdEvaluacion = group.Key.IdEvaluacion
                        ,
                        IdGrupo = group.Key.IdGrupo
                        ,
                        IdExamen = group.Key.IdExamen
                        ,
                        EsAprobado = group.Key.EsAprobado
                        ,
                        CalificaPorCentil = group.Key.CalificaPorCentil
                        ,
                        Orden = group.Key.Orden
                        ,
                        OrdenReal = group.Key.OrdenReal
                    })
                    .ToList();
                foreach (var item in Evaluaciones)
                {
                    count = 0.00M;
                    var EvaluacionesGrupo = reporte.Where(x => x.IdGrupo == null && x.IdEvaluacion == item.IdEvaluacion).ToList();
                    foreach (var componente in EvaluacionesGrupo)
                    {
                        if (componente.Titulo.Equals("N1: Ansiedad"))
                        {
                            var c = 323;
                        }
                        var existe = listaNotasProcesoSeleccionComplemento.Where(x => x.IdEvaluacion == componente.IdEvaluacion && x.IdExamen == componente.IdExamen /*&&x.IdProcesoSeleccion==componente.IdProceso && x.IdEtapa==componente.IdEtapa*/).FirstOrDefault();
                        var eval = new ProcesoSelecionExamenesCompletosDTO
                        {
                            IdProcesoSeleccion = componente.IdProceso,
                            ProcesoSeleccion = componente.NombreProceso,
                            IdPostulante = componente.IdPostulante,
                            Postulante = componente.Postulante,
                            Edad = 24,
                            Examen = componente.NombreExamen,
                            IdCategoria = componente.IdCategoria,
                            Categoria = componente.NombreCategoria,
                            IdExamen = componente.IdExamen,
                            IdGrupo = componente.IdGrupo == null ? 0 : componente.IdGrupo,
                            IdEvaluacion = componente.IdEvaluacion,
                            Evaluacion = componente.NombreEvaluacion,
                            Grupo = componente.NombreGrupo,
                            Etapa = componente.NombreEtapa,
                            IdEtapa = componente.IdEtapa,
                            Orden = item.Orden,
                            Registro = componente.Puntaje.ToString(),
                            EsAprobado = item.EsAprobado,
                            CalificaPorCentil = item.CalificaPorCentil,
                            IdSexo = componente.IdSexo,
                            NotaAprobatoria = "N.A",
                            Simbolo = "",
                        };
                        if (existe != null)
                        {
                            eval.OrdenReal = existe.OrdenReal;
                        }
                        else
                        {
                            count = count + 0.01M;
                            eval.OrdenReal = item.OrdenReal + count;
                        }
                        listaNotasProcesoSeleccionComplemento.Add(eval);
                    }

                }
                foreach (var item in GrupoComponente)
                {
                    count = 0.00M;
                    var grupos = reporte.Where(x => x.IdGrupo == item.IdGrupo && x.IdEvaluacion == item.IdEvaluacion).ToList();
                    foreach (var componente in grupos)
                    {
                        if (componente.Titulo.Equals("N1: Ansiedad"))
                        {
                            var c = 323;
                        }
                        var existe = listaNotasProcesoSeleccionComplemento.Where(x => x.IdEvaluacion == componente.IdEvaluacion && x.IdExamen == componente.IdExamen /*&& x.IdProcesoSeleccion == componente.IdProceso && x.IdEtapa == componente.IdEtapa*/).FirstOrDefault();
                        var eval = new ProcesoSelecionExamenesCompletosDTO
                        {
                            IdProcesoSeleccion = componente.IdProceso,
                            ProcesoSeleccion = componente.NombreProceso,
                            IdPostulante = componente.IdPostulante,
                            Postulante = componente.Postulante,
                            Edad = 24,
                            Examen = componente.NombreExamen,
                            IdCategoria = componente.IdCategoria,
                            Categoria = componente.NombreCategoria,
                            IdExamen = componente.IdExamen,
                            IdGrupo = componente.IdGrupo == null ? 0 : componente.IdGrupo,
                            IdEvaluacion = componente.IdEvaluacion,
                            Evaluacion = componente.NombreEvaluacion,
                            Grupo = componente.NombreGrupo,
                            Etapa = componente.NombreEtapa,
                            IdEtapa = componente.IdEtapa,
                            Orden = item.Orden,
                            Registro = componente.Puntaje.ToString(),
                            EsAprobado = item.EsAprobado,
                            CalificaPorCentil = item.CalificaPorCentil,
                            IdSexo = componente.IdSexo,
                            NotaAprobatoria = "N.A",
                            Simbolo = ""
                        };
                        if (existe != null)
                        {
                            eval.OrdenReal = existe.OrdenReal;
                        }
                        else
                        {

                            count = count + 0.01M;
                            eval.OrdenReal = item.OrdenReal + count;
                        }
                        listaNotasProcesoSeleccionComplemento.Add(eval);
                    }

                }
                foreach (var item in listaNotasProcesoSeleccionComplemento)
                {

                    var puntaje = Convert.ToDecimal(item.Registro, null);

                    if (item.CalificaPorCentil)
                    {
                        item.CalificaPorCentil = true;
                        var centil = repCentilRep.FirstBy(x => x.Estado == true && x.IdExamen == item.IdExamen
                                        && x.IdSexo == (x.IdSexo == null ? null : item.IdSexo) && (puntaje >= x.ValorMinimo && puntaje <= x.ValorMaximo));
                        if (centil == null)
                        {
                            item.Registro = "SIN CENTIL";
                            item.EsAprobado = false;
                        }
                        else
                        {
                            item.Registro = centil.Centil.ToString();
                        }
                    }
                    if (!item.Registro.Equals("SIN CENTIL") && item.IdEvaluacion != 53)
                    {
                        var puntaje2 = Convert.ToDecimal(item.Registro, null);
                        item.Registro = Math.Round(puntaje2).ToString();
                    }
                }

                listaNotasProcesoSeleccion = listaNotasProcesoSeleccion.Concat(listaNotasProcesoSeleccionComplemento).ToList();
                return listaNotasProcesoSeleccion.OrderBy(x => x.OrdenReal.Value).ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Edgar S .
        /// Fecha: 12/05/2021
        /// Version: 1.0
        /// <summary>
        /// Obtiene puntaje de examen para su calificación segpun configuración de evaluaciones, grupos y componentes
        /// </summary>
        /// <param name="filtro"> Valores de búsqueda para recolección de información </param>
        /// <returns> List<ProcesoSelecionExamenesCompletosDTO> </returns>
        public List<ProcesoSelecionExamenesCompletosDTO> ObtenerReporteExamenesNuevaVersion(ReportePostulanteDTO filtro)
        {
            try
            {
                FormulaPuntajeRepositorio _repFormulaPuntaje = new FormulaPuntajeRepositorio(_integraDBContext);
                ExamenTestRepositorio _repExamenTest = new ExamenTestRepositorio(_integraDBContext);
                ProcesoSeleccionPuntajeCalificacionRepositorio _repProcesoSeleccionPuntajeCalificacion = new ProcesoSeleccionPuntajeCalificacionRepositorio(_integraDBContext);
                GrupoComponenteEvaluacionRepositorio _repGrupoComponenteEvaluacion = new GrupoComponenteEvaluacionRepositorio(_integraDBContext);
                AsignacionPreguntaExamenRepositorio _repAsignacionPreguntaExamen = new AsignacionPreguntaExamenRepositorio(_integraDBContext);
                ExamenRepositorio _repExamen = new ExamenRepositorio(_integraDBContext);
                CentilRepositorio _repCentil = new CentilRepositorio(_integraDBContext);

                List<DatosExamenPostulanteDTO> reporte;

                reporte = _repExamen.ObtenerReporteExamenPostulante_V2(filtro);
                if (reporte == null || reporte.Count == 0)
                {
                    List<ProcesoSelecionExamenesCompletosDTO> siVacio = new List<ProcesoSelecionExamenesCompletosDTO>();
                    return siVacio;
                }

                //Obtenemos la configuración de fórmulas de calificación de componentes, grupos y evaluaciones
                var configuracionExamen = _repExamen.ObtenerConfiguracionPuntaje();

                //Asignamos el puntaje real al puntaje de cada componente unitario                
                foreach (var componente in reporte)
                {
                    //Cambiamos el puntaje de Curso a decimal redondeado
                    if (componente.PuntajeCurso !=  null) componente.PuntajeCurso = FuncionRedondeo(componente.PuntajeCurso.GetValueOrDefault(), RedondeoGeneral);
                    //Obtenemos la cantidad de preguntas para realizar el promedio en caso fuera necesario
                    var cantidadPreguntaComponenteConfigurado = configuracionExamen.Where(x => x.IdExamen == componente.IdExamen).FirstOrDefault();
                    var formulaComponente = componente.IdFormulaComponente.GetValueOrDefault();
                    if (formulaComponente > 0 && cantidadPreguntaComponenteConfigurado != null && cantidadPreguntaComponenteConfigurado.CantidadPreguntas > 0)
                    {
                        // 3 = PROMEDIAR(Puntaje preguntas)
                        if (formulaComponente == 3) componente.Puntaje = (componente.Puntaje * componente.FactorComponente) / cantidadPreguntaComponenteConfigurado.CantidadPreguntas;
                        else componente.Puntaje = componente.Puntaje * componente.FactorComponente;
                    }
                    else componente.Puntaje = componente.Puntaje * componente.FactorComponente;
                }

                //Se agrupan las notas o calificaciones por postulante para que de esta manera podamos calcular su calificacion.
                var listaPostulante = reporte.GroupBy(u => new { u.Postulante, u.NombreProceso, u.IdPostulante })
                    .Select(group =>
                    new DatosNotaPorPostulanteDTO
                    {
                        IdPostulante = group.Key.IdPostulante,
                        Postulante = group.Key.Postulante,
                        NombreProceso = group.Key.NombreProceso,
                        ListaNotas = group.Select(x => new NotaPostulanteDTO { 
                            IdProceso = x.IdProceso, 
                            ProcesoSeleccion = x.NombreProceso, 
                            IdSexo = x.IdSexo, 
                            IdEvaluacion = x.IdEvaluacion, 
                            IdGrupo = x.IdGrupo, 
                            IdExamen = x.IdExamen, 
                            NombreEvaluacion = x.NombreEvaluacion, 
                            NombreExamen = x.NombreExamen, 
                            NombreGrupo = x.NombreGrupo, 
                            Puntaje = x.Puntaje, 
                            IdCategoria = x.IdCategoria, 
                            IdEtapa = x.IdEtapa, 
                            NombreCategoria = x.NombreCategoria, 
                            NombreEtapa = x.NombreEtapa, 
                            FactorComponente = x.FactorComponente, 
                            FactorEvaluacion = x.FactorEvaluacion, 
                            FactorGrupo = x.FactorGrupo, 
                            IdFormulaComponente = x.IdFormulaComponente, 
                            IdFormulaEvaluacion = x.IdFormulaEvaluacion, 
                            IdFormulaGrupo = x.IdFormulaGrupo, 
                            EstadoAcceso = x.EstadoAcceso, 
                            CantidadConfigurado = x.CantidadConfigurado, 
                            CantidadResuelto = x.CantidadResuelto, 
                            PuntajeCurso = x.PuntajeCurso 
                        }).ToList()
                    }).ToList();

                List<ProcesoSelecionExamenesCompletosDTO> listaNotasProcesoSeleccion = new List<ProcesoSelecionExamenesCompletosDTO>();
                Decimal count = 0.00M;

                foreach (var postulante in listaPostulante)
                {                    
                    count = 0.00M;
                    //Se obtiene en una lista todas las evaluaciones de un postulante
                    List<int?> ListaEvaluacion = postulante.ListaNotas.Where(x => x.IdEvaluacion != null).Select(x => x.IdEvaluacion).Distinct().ToList();

                    //Se agrupan las evaluaciones segun sus notas obtenidas por componentes, recordar que una evaluacion puede tener mas de un componente
                    var grupoEvaluacion = postulante.ListaNotas.GroupBy(u => new { u.IdEvaluacion, u.NombreEvaluacion })
                        .Select(group => new EvaluacionPostulanteDTO
                        {
                            IdEvaluacion = group.Key.IdEvaluacion,
                            ListaComponentesEvaluacion = group.Select(x => new NotaPostulanteDTO { 
                                IdProceso = x.IdProceso, 
                                ProcesoSeleccion = x.ProcesoSeleccion, 
                                IdSexo = x.IdSexo, 
                                IdEvaluacion = x.IdEvaluacion, 
                                IdGrupo = x.IdGrupo, 
                                IdExamen = x.IdExamen,
                                NombreEvaluacion = x.NombreEvaluacion, 
                                NombreExamen = x.NombreExamen, 
                                NombreGrupo = x.NombreGrupo, 
                                Puntaje = x.Puntaje, 
                                IdCategoria = x.IdCategoria, 
                                IdEtapa = x.IdEtapa, 
                                NombreCategoria = x.NombreCategoria, 
                                NombreEtapa = x.NombreEtapa, 
                                FactorComponente = x.FactorComponente, 
                                FactorEvaluacion = x.FactorEvaluacion, 
                                FactorGrupo = x.FactorGrupo, 
                                IdFormulaComponente = x.IdFormulaComponente, 
                                IdFormulaEvaluacion = x.IdFormulaEvaluacion, 
                                IdFormulaGrupo = x.IdFormulaGrupo, 
                                EstadoAcceso = x.EstadoAcceso,
                                CantidadConfigurado = x.CantidadConfigurado,
                                CantidadResuelto = x.CantidadResuelto,
                                PuntajeCurso = x.PuntajeCurso
                            }).ToList()
                        }
                        ).ToList();

                    //Se recorre las evaluaciones de un postulante
                    foreach (var evaluacion in grupoEvaluacion)
                    {
                        var calificacionTotal = _repExamenTest.GetBy(x => x.Estado == true && x.Id == evaluacion.IdEvaluacion).FirstOrDefault();// se obtiene informacion de cada evaluacion del postulante
                        bool esAgrupada = evaluacion.ListaComponentesEvaluacion.Where(x => x.IdGrupo != null).Count() >= 1; // Verifica si las evaluaciones tienen al menos un grupo de Evaluacion para considerar la calificacion por Grupos
                        ProcesoSelecionExamenesCompletosDTO eval = new ProcesoSelecionExamenesCompletosDTO();

                        //Inserta la Nota por Evaluacion, su nota total y calculada
                        // el campo de CalificarEvaluacion indica si se califica por Evaluacion en caso sea true 
                        if (calificacionTotal != null && calificacionTotal.CalificarEvaluacion == true)
                        {
                            var formulaPuntajeEvaluacion = _repFormulaPuntaje.FirstById(calificacionTotal.IdFormulaPuntaje.Value); // extrae el valor de como se va a calificar la evaluacion
                            if (formulaPuntajeEvaluacion.Nombre.Contains("(Puntaje componentes)")) // Si se califica por Puntaje de Componentes, de todos los componentes de una evaluacion suma o promedia sus puntajes y esa seria la calificacion de la Evaluacion
                            {
                                var cantidadComponentes = configuracionExamen.Where(x => x.IdExamenTest == evaluacion.IdEvaluacion).Count();

                                eval = new ProcesoSelecionExamenesCompletosDTO
                                {
                                    IdProcesoSeleccion = evaluacion.ListaComponentesEvaluacion[0].IdProceso,
                                    ProcesoSeleccion = evaluacion.ListaComponentesEvaluacion[0].ProcesoSeleccion,
                                    IdPostulante = postulante.IdPostulante,
                                    Postulante = postulante.Postulante,
                                    Edad = 24,
                                    Examen = null,
                                    IdCategoria = evaluacion.ListaComponentesEvaluacion[0].IdCategoria,
                                    Categoria = evaluacion.ListaComponentesEvaluacion[0].NombreCategoria,
                                    IdExamen = 0,
                                    IdGrupo = 0,
                                    IdEvaluacion = evaluacion.ListaComponentesEvaluacion[0].IdEvaluacion,
                                    Evaluacion = evaluacion.ListaComponentesEvaluacion[0].NombreEvaluacion,
                                    Grupo = null,
                                    IdEtapa = evaluacion.ListaComponentesEvaluacion[0].IdEtapa,
                                    Etapa = evaluacion.ListaComponentesEvaluacion[0].NombreEtapa,
                                    Orden = Convert.ToInt32(String.Concat(evaluacion.ListaComponentesEvaluacion[0].IdEvaluacion.ToString(), "0", "0")),
                                    Registro = formulaPuntajeEvaluacion.Nombre.ToUpper().Contains("SUMAR") ? evaluacion.ListaComponentesEvaluacion.Sum(x => x.Puntaje).ToString() : PromediarListaPuntaje(evaluacion.ListaComponentesEvaluacion.Select(x => x.Puntaje).ToList(), cantidadComponentes, 1.00M).ToString(),
                                    EsAprobado = false,
                                    CalificaPorCentil = false,
                                    IdSexo = evaluacion.ListaComponentesEvaluacion[0].IdSexo
                                };
                                count = count + 1M;
                                eval.OrdenReal = count; //Esta parte se utilizara para darle un orden a cada componente.
                                listaNotasProcesoSeleccion.Add(eval); // añade la Nota por Evaluacion a la listafinal
                            }
                            else // de lo contrario la calificacion de la Evaluacion se calcula mediante la suma o promedio de GRUPO DE COMPONENTES
                            {
                                List<ProcesoSelecionExamenesCompletosDTO> listaGrupo = new List<ProcesoSelecionExamenesCompletosDTO>();
                                foreach (var componente in evaluacion.ListaComponentesEvaluacion)
                                {
                                    // Verifica si se insertado el componente al Grupo de componentes listaGrupo
                                    var eval1 = listaGrupo.Where(x => x.IdPostulante == postulante.IdPostulante && x.IdEvaluacion == componente.IdEvaluacion && x.IdGrupo == componente.IdGrupo).FirstOrDefault();
                                    if (eval1 == null) // aun no existe el grupo de componentes en  listaGrupo por eso se inserta
                                    {
                                        eval = new ProcesoSelecionExamenesCompletosDTO
                                        {
                                            IdProcesoSeleccion = evaluacion.ListaComponentesEvaluacion[0].IdProceso,
                                            ProcesoSeleccion = evaluacion.ListaComponentesEvaluacion[0].ProcesoSeleccion,
                                            IdPostulante = postulante.IdPostulante,
                                            Postulante = postulante.Postulante,
                                            Edad = 24,
                                            Examen = null,
                                            IdExamen = 0,
                                            IdGrupo = componente.IdGrupo,
                                            IdFormulaGrupo = componente.IdFormulaGrupo,
                                            IdEvaluacion = componente.IdEvaluacion,
                                            Evaluacion = componente.NombreEvaluacion,
                                            Grupo = componente.FactorComponente.ToString(),
                                            Orden = Convert.ToInt32(String.Concat(componente.IdEvaluacion, componente.IdGrupo == null ? "0" : componente.IdGrupo.ToString(), "0")),
                                            Registro = componente.Puntaje.ToString(),
                                            EsAprobado = false,
                                            CalificaPorCentil = false,
                                            EstadoAcceso = componente.EstadoAcceso,
                                            CantidadConfigurado = componente.CantidadConfigurado,
                                            CantidadResuelto = componente.CantidadResuelto,
                                            PuntajeCurso = componente.PuntajeCurso
                                        };
                                        listaGrupo.Add(eval);
                                    }
                                    else
                                    {
                                        //si el grupo ya existe, se suma el puntaje anterior obtenido de la consulta eval1
                                        listaGrupo.Where(x => x.IdPostulante == postulante.IdPostulante && x.IdEvaluacion == componente.IdEvaluacion && x.IdGrupo == componente.IdGrupo).FirstOrDefault().Registro = (Convert.ToDecimal(eval1.Registro, null) + componente.Puntaje).ToString();
                                    }
                                }

                                // Ahora se obtiene los puntajes obtenidos en el grupo y se aplica la fórmula configurada para el grupo
                                foreach (var grupo in listaGrupo)
                                {
                                    //Obtenemos la cantidad de grupos para realizar el promedio en caso fuera necesario
                                    var cantidadGrupoConfigurado = configuracionExamen.Where(x => x.IdExamenTest == evaluacion.IdEvaluacion && x.IdGrupo == grupo.IdGrupo).ToList();
                                    var formulaGrupo = grupo.IdFormulaGrupo.GetValueOrDefault();
                                    if (formulaGrupo > 0 && cantidadGrupoConfigurado.Count > 0)
                                    {                                        
                                        if (formulaGrupo == 6) // 6 = PROMEDIAR(Puntaje componentes)
                                        {
                                            grupo.Registro = (PromediarPuntaje(Convert.ToDecimal(grupo.Registro), cantidadGrupoConfigurado.Count, 1.00M)).ToString();
                                        }
                                    }
                                }

                                // se obtiene el puntaje de la evaluacion de acuerdo a los grupos de componentes que se encuentran almacenadoes en listaGrupo
                                eval = new ProcesoSelecionExamenesCompletosDTO
                                {
                                    IdProcesoSeleccion = evaluacion.ListaComponentesEvaluacion[0].IdProceso,
                                    ProcesoSeleccion = evaluacion.ListaComponentesEvaluacion[0].ProcesoSeleccion,
                                    IdPostulante = postulante.IdPostulante,
                                    Postulante = postulante.Postulante,
                                    Edad = 24,
                                    Examen = null,
                                    Categoria = evaluacion.ListaComponentesEvaluacion[0].NombreCategoria,
                                    IdCategoria = evaluacion.ListaComponentesEvaluacion[0].IdCategoria,
                                    IdExamen = 0,
                                    IdGrupo = 0,
                                    IdEvaluacion = evaluacion.ListaComponentesEvaluacion[0].IdEvaluacion,
                                    Evaluacion = evaluacion.ListaComponentesEvaluacion[0].NombreEvaluacion,
                                    Grupo = null,
                                    Etapa = evaluacion.ListaComponentesEvaluacion[0].NombreEtapa,
                                    IdEtapa = evaluacion.ListaComponentesEvaluacion[0].IdEtapa,
                                    Orden = Convert.ToInt32(String.Concat(evaluacion.ListaComponentesEvaluacion[0].IdEvaluacion.ToString(), "0", "0")),

                                    //Verifica si la formula de la evaluacion es Suma de Grupo de Componentes o Promedio de los mismos
                                    Registro = formulaPuntajeEvaluacion.Nombre.ToUpper().Contains("SUMAR") ? listaGrupo.Sum(x => Convert.ToDecimal(x.Registro, null) * Convert.ToDecimal(x.Grupo, null)).ToString() : (listaGrupo.Sum(x => Convert.ToDecimal(x.Registro, null) * Convert.ToDecimal(x.Grupo, null)) / Convert.ToDecimal(listaGrupo.Count(), null)).ToString(),
                                    EsAprobado = false,
                                    CalificaPorCentil = false,
                                    IdSexo = evaluacion.ListaComponentesEvaluacion[0].IdSexo,
                                };
                                count = count + 1M;
                                eval.OrdenReal = count;
                                listaNotasProcesoSeleccion.Add(eval);
                            }
                        }
                        else
                        {
                            //listaNotasProcesoSeleccion
                            List<ProcesoSelecionExamenesCompletosDTO> listaAuxiliarGrupo = new List<ProcesoSelecionExamenesCompletosDTO>();

                            //Si la calificacion no es por Evaluacion entonces puede ser por Grupo de Componentes o componentes.
                            foreach (var componente in evaluacion.ListaComponentesEvaluacion)
                            {
                                //Inserta los Grupos de Componentes con su Calificacion
                                if (esAgrupada == true)
                                {
                                    var eval1 = listaAuxiliarGrupo.Where(x => x.IdPostulante == postulante.IdPostulante && x.IdEvaluacion == componente.IdEvaluacion && x.IdGrupo == componente.IdGrupo).FirstOrDefault();

                                    if (eval1 == null)
                                    {
                                        eval = new ProcesoSelecionExamenesCompletosDTO
                                        {
                                            IdProcesoSeleccion = componente.IdProceso,
                                            ProcesoSeleccion = componente.ProcesoSeleccion,
                                            IdPostulante = postulante.IdPostulante,
                                            Postulante = postulante.Postulante,
                                            Edad = 24,
                                            Examen = null,
                                            IdCategoria = componente.IdCategoria,
                                            Categoria = componente.NombreCategoria,
                                            IdExamen = 0,
                                            IdGrupo = componente.IdGrupo,
                                            IdEvaluacion = componente.IdEvaluacion,
                                            Evaluacion = componente.NombreEvaluacion,
                                            IdFormulaGrupo = componente.IdFormulaGrupo,
                                            Grupo = componente.NombreGrupo,
                                            Etapa = componente.NombreEtapa,
                                            IdEtapa = componente.IdEtapa,
                                            Orden = Convert.ToInt32(String.Concat(componente.IdEvaluacion, componente.IdGrupo == null ? "0" : componente.IdGrupo.ToString(), "0")),
                                            Registro = componente.Puntaje.ToString(),
                                            EsAprobado = false,
                                            CalificaPorCentil = false,
                                            IdSexo = componente.IdSexo,
                                            OrdenReal = count++,
                                            EstadoAcceso = componente.EstadoAcceso,
                                            CantidadConfigurado = componente.CantidadConfigurado,
                                            CantidadResuelto = componente.CantidadResuelto,
                                            PuntajeCurso = componente.PuntajeCurso
                                        };
                                        count = count + 1M;
                                        eval.OrdenReal = count;
                                        listaAuxiliarGrupo.Add(eval);
                                    }
                                    else
                                    {
                                        listaAuxiliarGrupo.Where(x => x.IdPostulante == postulante.IdPostulante && x.IdEvaluacion == componente.IdEvaluacion && x.IdGrupo == componente.IdGrupo).FirstOrDefault().Registro = (Convert.ToDecimal(eval1.Registro, null) + (componente.Puntaje)).ToString();
                                    }
                                }
                                //Inserta los Componentes con su Calificacion respectiva, es decir no realiza ni un calculo solo inserta lo componentes que se obtuvieron en la vista principal
                                else
                                {
                                    eval = new ProcesoSelecionExamenesCompletosDTO
                                    {
                                        IdProcesoSeleccion = componente.IdProceso,
                                        ProcesoSeleccion = componente.ProcesoSeleccion,
                                        IdPostulante = postulante.IdPostulante,
                                        Postulante = postulante.Postulante,
                                        Edad = 24,
                                        Examen = componente.NombreExamen,
                                        IdCategoria = componente.IdCategoria,
                                        Categoria = componente.NombreCategoria,
                                        IdExamen = componente.IdExamen,
                                        IdGrupo = componente.IdGrupo == null ? 0 : componente.IdGrupo,
                                        IdEvaluacion = componente.IdEvaluacion,
                                        Evaluacion = componente.NombreEvaluacion,
                                        Grupo = componente.NombreGrupo,
                                        IdFormulaGrupo = componente.IdFormulaGrupo,
                                        Etapa = componente.NombreEtapa,
                                        IdEtapa = componente.IdEtapa,
                                        Orden = Convert.ToInt32(String.Concat(componente.IdEvaluacion, componente.IdGrupo == null ? "0" : componente.IdGrupo.ToString(), componente.IdExamen == null ? "0" : componente.IdExamen.ToString())),
                                        Registro = componente.Puntaje.ToString(),
                                        EsAprobado = false,
                                        CalificaPorCentil = false,
                                        IdSexo = componente.IdSexo,
                                        EstadoAcceso = componente.EstadoAcceso,
                                        CantidadConfigurado = componente.CantidadConfigurado,
                                        CantidadResuelto = componente.CantidadResuelto,
                                        PuntajeCurso = componente.PuntajeCurso
                                    };
                                    count = count + 1M;
                                    eval.OrdenReal = count;
                                    listaAuxiliarGrupo.Add(eval); // inserta la calificacion de los grupos de componentes 
                                }
                            }

                            // Ahora se obtiene los puntajes obtenidos en el grupo y se aplica la fórmula configurada para el grupo
                            foreach (var grupo in listaAuxiliarGrupo)
                            {
                                //Obtenemos la cantidad de grupos para realizar el promedio en caso fuera necesario
                                var cantidadGrupoConfigurado = configuracionExamen.Where(x => x.IdExamenTest == evaluacion.IdEvaluacion && x.IdGrupo == grupo.IdGrupo).ToList();
                                var formulaGrupo = grupo.IdFormulaGrupo.GetValueOrDefault();
                                if (formulaGrupo > 0 && cantidadGrupoConfigurado.Count > 0)
                                {
                                    if (formulaGrupo == 6) // 6 = PROMEDIAR(Puntaje componentes)
                                    {
                                        grupo.Registro = (PromediarPuntaje(Convert.ToDecimal(grupo.Registro), cantidadGrupoConfigurado.Count, 1.00M)).ToString();
                                    }
                                }
                            }
                            listaNotasProcesoSeleccion.AddRange(listaAuxiliarGrupo);
                        }
                    }
                }

                var informacionCentilCalificacion = new List<ObtenerCalificacionCentilDTO>();
                if (filtro.ListaProcesoSeleccion != null)
                {
                    var listaProcesoSeleccion = filtro.ListaProcesoSeleccion.GetValueOrDefault().ToString();
                    informacionCentilCalificacion = _repExamen.ObtenerInformacionCentilPorProcesoSeleccion(listaProcesoSeleccion);
                }
                else
                {
                    var pruebaCalificacion = reporte.Select(x => x.IdProceso).Distinct().ToList();
                    if (pruebaCalificacion != null && pruebaCalificacion.Count > 0)
                    {
                        var hallarCalificacion = "";
                        for (var i = 0; pruebaCalificacion.Count > i; i++)
                        {
                            if (i == 0) hallarCalificacion = pruebaCalificacion[i] + "";
                            else hallarCalificacion = hallarCalificacion + "," + pruebaCalificacion[i];
                        }
                        informacionCentilCalificacion = _repExamen.ObtenerInformacionCentilPorProcesoSeleccion(hallarCalificacion);
                    }
                }

                List<CentilBO> centilesCompletos = new List<CentilBO>();
                var informacionCentilCalificacionAuxiliar = _repCentil.GetBy(x => x.IdExamenTest == null).ToList();
                centilesCompletos.AddRange(informacionCentilCalificacionAuxiliar);
                List<CentilBO> centilesAsociados = new List<CentilBO>();
                if (informacionCentilCalificacion.Count > 0)
                {
                    var prueba = informacionCentilCalificacion.Where(x => x.IdExamen != null).Select(x => x.IdExamen).Distinct().ToList();
                    if (prueba.Count > 0)
                    {
                        var hallarCentilesAsociados = "";
                        for (var i = 0; prueba.Count > i; i++)
                        {
                            if (i == 0) hallarCentilesAsociados = prueba[i] + "";
                            else hallarCentilesAsociados = hallarCentilesAsociados + "," + prueba[i];
                        }
                        centilesAsociados = _repExamen.ObtenerCentilesAsociados(hallarCentilesAsociados);
                        centilesCompletos.AddRange(centilesAsociados);
                    }
                }

                // En esta parte se usa la configuracion del proceso para saber si se califica por centil o en forma directa, en caso se califica por centil se va a la tabla de centiles y se busca su calificacion
                //tambien segun lo parametros configurados, se calcula si un postulante aprueba o no
                foreach (var item in listaNotasProcesoSeleccion)
                {
                    item.OrdenReal = item.Orden;
                    var puntaje = Convert.ToDecimal(item.Registro, null);
                    if (item.Evaluacion != null && (item.IdEvaluacion == 53 || item.Evaluacion.Equals("GRADUATE MANAGEMENT ADMISSION TEST V2")))
                    {
                        var countPregunta = Convert.ToDecimal(_repAsignacionPreguntaExamen.GetBy(x => x.IdExamen == item.IdExamen).Count(), null);
                        item.Registro = FuncionRedondeo((puntaje * 100.0M / countPregunta), RedondeoGeneral).ToString();
                    }

                    // Cambia los puntajes de las Evaluaciones
                    if (item.IdEvaluacion != 0 && item.IdGrupo == 0 && item.IdExamen == 0)
                    {
                        //var calificacionOriginal = repProcesoSeleccionPuntajeCalificacionRep.FirstBy(x => x.Estado == true && x.IdExamenTest == item.IdEvaluacion && x.IdGrupoComponenteEvaluacion == null && x.IdExamen == null && x.IdProcesoSeleccion == item.IdProcesoSeleccion);
                        var calificacion = informacionCentilCalificacion.Where(x => x.IdExamenTest == item.IdEvaluacion && x.IdGrupoComponenteEvaluacion == null && x.IdExamen == null && x.IdProcesoSeleccion == item.IdProcesoSeleccion).FirstOrDefault();

                        if (calificacion == null)
                        {
                            item.EsAprobado = false;
                            item.NotaAprobatoria = "N.A";
                        }
                        else
                        {
                            // La Calificacion debe de ser Igual a Valor minimo del componente o evaluacion
                            item.NotaAprobatoria = calificacion.PuntajeMinimo.ToString();
                            if (calificacion.IdProcesoSeleccionRango == 1)
                            {
                                item.Simbolo = "=";
                                if (calificacion.CalificaPorCentil)
                                {
                                    item.CalificaPorCentil = true;
                                    var centil = centilesCompletos.Where(x => x.IdExamenTest == item.IdEvaluacion && x.IdSexo == (x.IdSexo == null ? null : item.IdSexo) && (puntaje >= x.ValorMinimo && puntaje <= x.ValorMaximo)).FirstOrDefault();

                                    if (centil == null)
                                    {
                                        item.Registro = "SIN CENTIL";
                                        item.EsAprobado = false;
                                    }
                                    else
                                    {
                                        item.Registro = centil.Centil.ToString();
                                        if (calificacion.PuntajeMinimo == Convert.ToDecimal(item.Registro, null))
                                        {
                                            item.EsAprobado = true;
                                        }
                                        if (calificacion.EsCalificable == false)
                                        {
                                            item.EsAprobado = false;
                                            item.NotaAprobatoria = "N.A";
                                            item.Simbolo = "";
                                        }
                                    }
                                }
                                else
                                {
                                    if (calificacion.PuntajeMinimo == Convert.ToDecimal(item.Registro, null))
                                    {
                                        item.EsAprobado = true;
                                        item.CalificaPorCentil = false;
                                    }
                                    if (calificacion.EsCalificable == false)
                                    {
                                        item.EsAprobado = false;
                                        item.NotaAprobatoria = "N.A";
                                        item.Simbolo = "";
                                    }
                                }
                            }

                            // La Calificacion debe de ser Mayor a Valor Minimo
                            if (calificacion.IdProcesoSeleccionRango == 2)
                            {
                                item.Simbolo = ">";
                                if (calificacion.CalificaPorCentil)
                                {
                                    item.CalificaPorCentil = true;
                                    var centil = centilesCompletos.Where(x => x.IdExamenTest == item.IdEvaluacion && x.IdSexo == (x.IdSexo == null ? null : item.IdSexo) && (puntaje >= x.ValorMinimo && puntaje <= x.ValorMaximo)).FirstOrDefault();

                                    if (centil == null)
                                    {
                                        item.Registro = "SIN CENTIL";
                                        item.EsAprobado = false;
                                    }
                                    else
                                    {
                                        item.Registro = centil.Centil.ToString();
                                        if (Convert.ToDecimal(item.Registro, null) > calificacion.PuntajeMinimo)
                                        {
                                            item.EsAprobado = true;
                                        }
                                        if (calificacion.EsCalificable == false)
                                        {
                                            item.EsAprobado = false;
                                            item.NotaAprobatoria = "N.A";
                                            item.Simbolo = "";
                                        }
                                    }
                                }
                                else
                                {
                                    if (Convert.ToDecimal(item.Registro, null) > calificacion.PuntajeMinimo)
                                    {
                                        item.EsAprobado = true;
                                        item.CalificaPorCentil = false;
                                    }
                                    if (calificacion.EsCalificable == false)
                                    {
                                        item.EsAprobado = false;
                                        item.NotaAprobatoria = "N.A";
                                        item.Simbolo = "";
                                    }
                                }
                            }

                            // La Calificacion debe de ser Menor a Valor Minimo
                            if (calificacion.IdProcesoSeleccionRango == 9)
                            {
                                item.Simbolo = "<";
                                if (calificacion.CalificaPorCentil)
                                {
                                    item.CalificaPorCentil = true;
                                    var centil = centilesCompletos.Where(x => x.IdExamenTest == item.IdEvaluacion && x.IdSexo == (x.IdSexo == null ? null : item.IdSexo) && (puntaje >= x.ValorMinimo && puntaje <= x.ValorMaximo)).FirstOrDefault();

                                    if (centil == null)
                                    {
                                        item.Registro = "SIN CENTIL";
                                        item.EsAprobado = false;
                                    }
                                    else
                                    {
                                        item.Registro = centil.Centil.ToString();
                                        if (Convert.ToDecimal(item.Registro, null) < calificacion.PuntajeMinimo)
                                        {
                                            item.EsAprobado = true;
                                        }
                                        if (calificacion.EsCalificable == false)
                                        {
                                            item.EsAprobado = false;
                                            item.NotaAprobatoria = "N.A";
                                            item.Simbolo = "";
                                        }
                                    }
                                }
                                else
                                {
                                    if (Convert.ToDecimal(item.Registro, null) < calificacion.PuntajeMinimo)
                                    {
                                        item.EsAprobado = true;
                                        item.CalificaPorCentil = false;
                                    }
                                    if (calificacion.EsCalificable == false)
                                    {
                                        item.EsAprobado = false;
                                        item.NotaAprobatoria = "N.A";
                                        item.Simbolo = "";
                                    }
                                }
                            }

                            // La Calificacion debe de ser Mayor Igual a Valor Minimo
                            if (calificacion.IdProcesoSeleccionRango == 12)
                            {
                                item.Simbolo = ">=";
                                if (calificacion.CalificaPorCentil)
                                {
                                    item.CalificaPorCentil = true;
                                    var centil = centilesCompletos.Where(x => x.IdExamenTest == item.IdEvaluacion && x.IdSexo == (x.IdSexo == null ? null : item.IdSexo) && (puntaje >= x.ValorMinimo && puntaje <= x.ValorMaximo)).FirstOrDefault();

                                    if (centil == null)
                                    {
                                        item.Registro = "SIN CENTIL";
                                        item.EsAprobado = false;
                                    }
                                    else
                                    {
                                        item.Registro = centil.Centil.ToString();
                                        if (Convert.ToDecimal(item.Registro, null) >= calificacion.PuntajeMinimo)
                                        {
                                            item.EsAprobado = true;
                                        }
                                        if (calificacion.EsCalificable == false)
                                        {
                                            item.EsAprobado = false;
                                            item.NotaAprobatoria = "N.A";
                                            item.Simbolo = "";
                                        }
                                    }
                                }
                                else
                                {
                                    if (Convert.ToDecimal(item.Registro, null) >= calificacion.PuntajeMinimo)
                                    {
                                        item.EsAprobado = true;
                                        item.CalificaPorCentil = false;
                                    }
                                    if (calificacion.EsCalificable == false)
                                    {
                                        item.EsAprobado = false;
                                        item.NotaAprobatoria = "N.A";
                                        item.Simbolo = "";
                                    }
                                }
                            }

                            // La Calificacion debe de ser Menor Igual a Valor Minimo
                            if (calificacion.IdProcesoSeleccionRango == 13)
                            {
                                item.Simbolo = "<=";
                                if (calificacion.CalificaPorCentil)
                                {
                                    item.CalificaPorCentil = true;
                                    var centil = centilesCompletos.Where(x => x.IdExamenTest == item.IdEvaluacion && x.IdSexo == (x.IdSexo == null ? null : item.IdSexo) && (puntaje >= x.ValorMinimo && puntaje <= x.ValorMaximo)).FirstOrDefault();

                                    if (centil == null)
                                    {
                                        item.Registro = "SIN CENTIL";
                                        item.EsAprobado = false;
                                    }
                                    else
                                    {
                                        item.Registro = centil.Centil.ToString();
                                        if (Convert.ToDecimal(item.Registro, null) <= calificacion.PuntajeMinimo)
                                        {
                                            item.EsAprobado = true;
                                        }
                                        if (calificacion.EsCalificable == false)
                                        {
                                            item.EsAprobado = false;
                                            item.NotaAprobatoria = "N.A";
                                            item.Simbolo = "";
                                        }
                                    }
                                }
                                else
                                {
                                    if (Convert.ToDecimal(item.Registro, null) <= calificacion.PuntajeMinimo)
                                    {
                                        item.EsAprobado = true;
                                        item.CalificaPorCentil = false;
                                    }
                                    if (calificacion.EsCalificable == false)
                                    {
                                        item.EsAprobado = false;
                                        item.NotaAprobatoria = "N.A";
                                        item.Simbolo = "";
                                    }
                                }
                            }

                            if (calificacion.IdProcesoSeleccionRango == null || calificacion.IdProcesoSeleccionRango == 0)
                            {
                                if (calificacion.CalificaPorCentil)
                                {
                                    item.CalificaPorCentil = true;
                                    var centil = centilesCompletos.Where(x => x.IdExamenTest == item.IdEvaluacion && x.IdSexo == (x.IdSexo == null ? null : item.IdSexo) && (puntaje >= x.ValorMinimo && puntaje <= x.ValorMaximo)).FirstOrDefault();

                                    if (centil == null)
                                    {
                                        item.Registro = "SIN CENTIL";
                                        item.EsAprobado = false;
                                    }
                                    else
                                    {
                                        item.Registro = centil.Centil.ToString();
                                        if (Convert.ToDecimal(item.Registro, null) >= (calificacion.PuntajeMinimo == null ? 0 : calificacion.PuntajeMinimo))
                                        {
                                            item.EsAprobado = true;
                                        }
                                        if (calificacion.EsCalificable == false)
                                        {
                                            item.EsAprobado = false;
                                            item.NotaAprobatoria = "N.A";
                                            item.Simbolo = "";
                                        }
                                    }
                                }
                                else
                                {
                                    if (Convert.ToDecimal(item.Registro, null) >= (calificacion.PuntajeMinimo == null ? 0 : calificacion.PuntajeMinimo))
                                    {
                                        item.EsAprobado = true;
                                        item.CalificaPorCentil = false;
                                    }
                                    if (calificacion.EsCalificable == false)
                                    {
                                        item.EsAprobado = false;
                                        item.NotaAprobatoria = "N.A";
                                        item.Simbolo = "";
                                    }
                                }
                            }

                        }
                    }


                    // Cambia los puntajes de las Grupos
                    if (item.IdEvaluacion != 0 && item.IdGrupo != 0 && item.IdExamen == 0)
                    {
                        var calificacion = informacionCentilCalificacion.Where(x => x.IdExamenTest == item.IdEvaluacion && x.IdGrupoComponenteEvaluacion == item.IdGrupo && x.IdExamen == null && x.IdProcesoSeleccion == item.IdProcesoSeleccion).FirstOrDefault();

                        if (calificacion == null)
                        {
                            item.EsAprobado = false;
                            item.NotaAprobatoria = "N.A";
                        }
                        else
                        {

                            // La Calificacion debe de ser Igual a Valor minimo del componente o evaluacion
                            item.NotaAprobatoria = calificacion.PuntajeMinimo.ToString();
                            if (calificacion.IdProcesoSeleccionRango == 1)
                            {
                                item.Simbolo = "=";
                                if (calificacion.CalificaPorCentil)
                                {
                                    item.CalificaPorCentil = true;
                                    var centil = centilesCompletos.Where(x => x.IdGrupoComponenteEvaluacion == item.IdGrupo && x.IdSexo == (x.IdSexo == null ? null : item.IdSexo) && (puntaje >= x.ValorMinimo && puntaje <= x.ValorMaximo)).FirstOrDefault();

                                    if (centil == null)
                                    {
                                        item.Registro = "SIN CENTIL";
                                        item.EsAprobado = false;
                                    }
                                    else
                                    {
                                        item.Registro = centil.Centil.ToString();
                                        if (Convert.ToDecimal(item.Registro, null) == calificacion.PuntajeMinimo)
                                        {
                                            item.EsAprobado = true;
                                        }
                                        if (calificacion.EsCalificable == false)
                                        {
                                            item.EsAprobado = false;
                                            item.NotaAprobatoria = "N.A";
                                            item.Simbolo = "";
                                        }
                                    }
                                }
                                else
                                {
                                    if (Convert.ToDecimal(item.Registro, null) == calificacion.PuntajeMinimo)
                                    {
                                        item.EsAprobado = true;
                                        item.CalificaPorCentil = false;
                                    }
                                    if (calificacion.EsCalificable == false)
                                    {
                                        item.EsAprobado = false;
                                        item.NotaAprobatoria = "N.A";
                                        item.Simbolo = "";
                                    }
                                }
                            }

                            // La Calificacion debe de ser Mayor a Valor Minimo
                            if (calificacion.IdProcesoSeleccionRango == 2)
                            {
                                item.Simbolo = ">";
                                if (calificacion.CalificaPorCentil)
                                {
                                    item.CalificaPorCentil = true;
                                    var centil = centilesCompletos.Where(x => x.IdGrupoComponenteEvaluacion == item.IdGrupo && x.IdSexo == (x.IdSexo == null ? null : item.IdSexo) && (puntaje >= x.ValorMinimo && puntaje <= x.ValorMaximo)).FirstOrDefault();

                                    if (centil == null)
                                    {
                                        item.Registro = "SIN CENTIL";
                                        item.EsAprobado = false;
                                    }
                                    else
                                    {
                                        item.Registro = centil.Centil.ToString();
                                        if (Convert.ToDecimal(item.Registro, null) > calificacion.PuntajeMinimo)
                                        {
                                            item.EsAprobado = true;
                                        }
                                        if (calificacion.EsCalificable == false)
                                        {
                                            item.EsAprobado = false;
                                            item.NotaAprobatoria = "N.A";
                                            item.Simbolo = "";
                                        }
                                    }
                                }
                                else
                                {
                                    if (Convert.ToDecimal(item.Registro, null) > calificacion.PuntajeMinimo)
                                    {
                                        item.EsAprobado = true;
                                        item.CalificaPorCentil = false;
                                    }
                                    if (calificacion.EsCalificable == false)
                                    {
                                        item.EsAprobado = false;
                                        item.NotaAprobatoria = "N.A";
                                        item.Simbolo = "";
                                    }
                                }
                            }

                            // La Calificacion debe de ser Menor a Valor Minimo
                            if (calificacion.IdProcesoSeleccionRango == 9)
                            {
                                item.Simbolo = "<";
                                if (calificacion.CalificaPorCentil)
                                {
                                    item.CalificaPorCentil = true;
                                    var centil = centilesCompletos.Where(x => x.IdGrupoComponenteEvaluacion == item.IdGrupo && x.IdSexo == (x.IdSexo == null ? null : item.IdSexo) && (puntaje >= x.ValorMinimo && puntaje <= x.ValorMaximo)).FirstOrDefault();

                                    if (centil == null)
                                    {
                                        item.Registro = "SIN CENTIL";
                                        item.EsAprobado = false;
                                    }
                                    else
                                    {
                                        item.Registro = centil.Centil.ToString();
                                        if (Convert.ToDecimal(item.Registro, null) < calificacion.PuntajeMinimo)
                                        {
                                            item.EsAprobado = true;
                                        }
                                        if (calificacion.EsCalificable == false)
                                        {
                                            item.EsAprobado = false;
                                            item.NotaAprobatoria = "N.A";
                                            item.Simbolo = "";
                                        }
                                    }
                                }
                                else
                                {
                                    if (Convert.ToDecimal(item.Registro, null) < calificacion.PuntajeMinimo)
                                    {
                                        item.EsAprobado = true;
                                        item.CalificaPorCentil = false;
                                    }
                                    if (calificacion.EsCalificable == false)
                                    {
                                        item.EsAprobado = false;
                                        item.NotaAprobatoria = "N.A";
                                        item.Simbolo = "";
                                    }
                                }
                            }

                            // La Calificacion debe de ser Mayor Igual a Valor Minimo
                            if (calificacion.IdProcesoSeleccionRango == 12)
                            {
                                item.Simbolo = ">=";
                                if (calificacion.CalificaPorCentil)
                                {
                                    item.CalificaPorCentil = true;
                                    var centil = centilesCompletos.Where(x => x.IdGrupoComponenteEvaluacion == item.IdGrupo && x.IdSexo == (x.IdSexo == null ? null : item.IdSexo) && (puntaje >= x.ValorMinimo && puntaje <= x.ValorMaximo)).FirstOrDefault();

                                    if (centil == null)
                                    {
                                        item.Registro = "SIN CENTIL";
                                        item.EsAprobado = false;
                                    }
                                    else
                                    {
                                        item.Registro = centil.Centil.ToString();
                                        if (Convert.ToDecimal(item.Registro, null) >= calificacion.PuntajeMinimo)
                                        {
                                            item.EsAprobado = true;
                                        }
                                        if (calificacion.EsCalificable == false)
                                        {
                                            item.EsAprobado = false;
                                            item.NotaAprobatoria = "N.A";
                                            item.Simbolo = "";
                                        }
                                    }
                                }
                                else
                                {
                                    if (Convert.ToDecimal(item.Registro, null) >= calificacion.PuntajeMinimo)
                                    {
                                        item.EsAprobado = true;
                                        item.CalificaPorCentil = false;
                                    }
                                    if (calificacion.EsCalificable == false)
                                    {
                                        item.EsAprobado = false;
                                        item.NotaAprobatoria = "N.A";
                                        item.Simbolo = "";
                                    }
                                }
                            }

                            // La Calificacion debe de ser Menor Igual a Valor Minimo
                            if (calificacion.IdProcesoSeleccionRango == 13)
                            {
                                item.Simbolo = "<=";
                                if (calificacion.CalificaPorCentil)
                                {
                                    item.CalificaPorCentil = true;
                                    var centil = centilesCompletos.Where(x => x.IdGrupoComponenteEvaluacion == item.IdGrupo && x.IdSexo == (x.IdSexo == null ? null : item.IdSexo) && (puntaje >= x.ValorMinimo && puntaje <= x.ValorMaximo)).FirstOrDefault();

                                    if (centil == null)
                                    {
                                        item.Registro = "SIN CENTIL";
                                        item.EsAprobado = false;
                                    }
                                    else
                                    {
                                        item.Registro = centil.Centil.ToString();
                                        if (Convert.ToDecimal(item.Registro, null) <= calificacion.PuntajeMinimo)
                                        {
                                            item.EsAprobado = true;
                                        }
                                        if (calificacion.EsCalificable == false)
                                        {
                                            item.EsAprobado = false;
                                            item.NotaAprobatoria = "N.A";
                                            item.Simbolo = "";
                                        }
                                    }
                                }
                                else
                                {
                                    if (Convert.ToDecimal(item.Registro, null) <= calificacion.PuntajeMinimo)
                                    {
                                        item.EsAprobado = true;
                                        item.CalificaPorCentil = false;
                                    }
                                    if (calificacion.EsCalificable == false)
                                    {
                                        item.EsAprobado = false;
                                        item.NotaAprobatoria = "N.A";
                                        item.Simbolo = "";
                                    }
                                }
                            }

                            if (calificacion.IdProcesoSeleccionRango == null || calificacion.IdProcesoSeleccionRango == 0)
                            {
                                if (calificacion.CalificaPorCentil)
                                {
                                    item.CalificaPorCentil = true;

                                    var centil = centilesCompletos.Where(x => x.IdGrupoComponenteEvaluacion == item.IdGrupo && x.IdSexo == (x.IdSexo == null ? null : item.IdSexo) && (puntaje >= x.ValorMinimo && puntaje <= x.ValorMaximo)).FirstOrDefault();

                                    if (centil == null)
                                    {
                                        item.Registro = "SIN CENTIL";
                                        item.EsAprobado = false;
                                    }
                                    else
                                    {
                                        item.Registro = centil.Centil.ToString();
                                        if (Convert.ToDecimal(item.Registro, null) >= (calificacion.PuntajeMinimo == null ? 0 : calificacion.PuntajeMinimo))
                                        {
                                            item.EsAprobado = true;
                                        }
                                        if (calificacion.EsCalificable == false)
                                        {
                                            item.EsAprobado = false;
                                            item.NotaAprobatoria = "N.A";
                                            item.Simbolo = "";
                                        }
                                    }
                                }
                                else
                                {
                                    if (Convert.ToDecimal(item.Registro, null) >= (calificacion.PuntajeMinimo == null ? 0 : calificacion.PuntajeMinimo))
                                    {
                                        item.EsAprobado = true;
                                        item.CalificaPorCentil = false;
                                    }
                                    if (calificacion.EsCalificable == false)
                                    {
                                        item.EsAprobado = false;
                                        item.NotaAprobatoria = "N.A";
                                        item.Simbolo = "";
                                    }
                                }
                            }
                        }
                    }


                    // Cambia los puntajes de los componentes
                    if (item.IdEvaluacion != 0 && item.IdGrupo == 0 && item.IdExamen != 0)
                    {
                        //var calificacionOriginal = repProcesoSeleccionPuntajeCalificacionRep.FirstBy(x => x.Estado == true && x.IdExamenTest == item.IdEvaluacion && x.IdGrupoComponenteEvaluacion == null && x.IdExamen == item.IdExamen && x.IdProcesoSeleccion == item.IdProcesoSeleccion);
                        var calificacion = informacionCentilCalificacion.Where(x => x.IdExamenTest == item.IdEvaluacion && x.IdGrupoComponenteEvaluacion == null && x.IdExamen == item.IdExamen && x.IdProcesoSeleccion == item.IdProcesoSeleccion).FirstOrDefault();

                        if (calificacion == null)
                        {
                            item.EsAprobado = false;
                            item.NotaAprobatoria = "N.A";
                        }
                        else
                        {
                            // La Calificacion debe de ser Igual a Valor minimo del componente o evaluacion
                            item.NotaAprobatoria = calificacion.PuntajeMinimo.ToString();
                            if (calificacion.IdProcesoSeleccionRango == 1)
                            {
                                item.Simbolo = "=";
                                if (calificacion.CalificaPorCentil)
                                {
                                    item.CalificaPorCentil = true;
                                    var centil = centilesCompletos.Where(x => x.IdExamen == item.IdExamen && x.IdSexo == (x.IdSexo == null ? null : item.IdSexo) && (puntaje >= x.ValorMinimo && puntaje <= x.ValorMaximo)).FirstOrDefault();

                                    if (centil == null)
                                    {
                                        item.Registro = "SIN CENTIL";
                                        item.EsAprobado = false;
                                    }
                                    else
                                    {
                                        item.Registro = centil.Centil.ToString();
                                        if (Convert.ToDecimal(item.Registro, null) == calificacion.PuntajeMinimo)
                                        {
                                            item.EsAprobado = true;
                                        }
                                        if (calificacion.EsCalificable == false)
                                        {
                                            item.EsAprobado = false;
                                            item.NotaAprobatoria = "N.A";
                                            item.Simbolo = "";
                                        }
                                    }
                                }
                                else
                                {
                                    if (Convert.ToDecimal(item.Registro, null) == calificacion.PuntajeMinimo)
                                    {
                                        item.EsAprobado = true;
                                        item.CalificaPorCentil = false;
                                    }
                                    if (calificacion.EsCalificable == false)
                                    {
                                        item.EsAprobado = false;
                                        item.NotaAprobatoria = "N.A";
                                        item.Simbolo = "";
                                    }
                                }
                            }

                            // La Calificacion debe de ser Mayor a Valor Minimo
                            if (calificacion.IdProcesoSeleccionRango == 2)
                            {
                                item.Simbolo = ">";
                                if (calificacion.CalificaPorCentil)
                                {
                                    item.CalificaPorCentil = true;
                                    var centil = centilesCompletos.Where(x => x.IdExamen == item.IdExamen && x.IdSexo == (x.IdSexo == null ? null : item.IdSexo) && (puntaje >= x.ValorMinimo && puntaje <= x.ValorMaximo)).FirstOrDefault();


                                    if (centil == null)
                                    {
                                        item.Registro = "SIN CENTIL";
                                        item.EsAprobado = false;
                                    }
                                    else
                                    {
                                        item.Registro = centil.Centil.ToString();
                                        if (Convert.ToDecimal(item.Registro, null) > calificacion.PuntajeMinimo)
                                        {
                                            item.EsAprobado = true;
                                        }
                                        if (calificacion.EsCalificable == false)
                                        {
                                            item.EsAprobado = false;
                                            item.NotaAprobatoria = "N.A";
                                            item.Simbolo = "";
                                        }
                                    }
                                }
                                else
                                {
                                    if (Convert.ToDecimal(item.Registro, null) > calificacion.PuntajeMinimo)
                                    {
                                        item.EsAprobado = true;
                                        item.CalificaPorCentil = false;
                                    }
                                    if (calificacion.EsCalificable == false)
                                    {
                                        item.EsAprobado = false;
                                        item.NotaAprobatoria = "N.A";
                                        item.Simbolo = "";
                                    }
                                }
                            }

                            // La Calificacion debe de ser Menor a Valor Minimo
                            if (calificacion.IdProcesoSeleccionRango == 9)
                            {
                                item.Simbolo = "<";
                                if (calificacion.CalificaPorCentil)
                                {
                                    item.CalificaPorCentil = true;
                                    var centil = centilesCompletos.Where(x => x.IdExamen == item.IdExamen && x.IdSexo == (x.IdSexo == null ? null : item.IdSexo) && (puntaje >= x.ValorMinimo && puntaje <= x.ValorMaximo)).FirstOrDefault();


                                    if (centil == null)
                                    {
                                        item.Registro = "SIN CENTIL";
                                        item.EsAprobado = false;
                                    }
                                    else
                                    {
                                        item.Registro = centil.Centil.ToString();
                                        if (Convert.ToDecimal(item.Registro, null) < calificacion.PuntajeMinimo)
                                        {
                                            item.EsAprobado = true;
                                        }
                                        if (calificacion.EsCalificable == false)
                                        {
                                            item.EsAprobado = false;
                                            item.NotaAprobatoria = "N.A";
                                            item.Simbolo = "";
                                        }
                                    }
                                }
                                else
                                {
                                    if (Convert.ToDecimal(item.Registro, null) < calificacion.PuntajeMinimo)
                                    {
                                        item.EsAprobado = true;
                                        item.CalificaPorCentil = false;
                                    }
                                    if (calificacion.EsCalificable == false)
                                    {
                                        item.EsAprobado = false;
                                        item.NotaAprobatoria = "N.A";
                                        item.Simbolo = "";
                                    }
                                }
                            }

                            // La Calificacion debe de ser Mayor Igual a Valor Minimo
                            if (calificacion.IdProcesoSeleccionRango == 12)
                            {
                                item.Simbolo = ">=";
                                if (calificacion.CalificaPorCentil)
                                {
                                    item.CalificaPorCentil = true;
                                    var centil = centilesCompletos.Where(x => x.IdExamen == item.IdExamen && x.IdSexo == (x.IdSexo == null ? null : item.IdSexo) && (puntaje >= x.ValorMinimo && puntaje <= x.ValorMaximo)).FirstOrDefault();

                                    if (centil == null)
                                    {
                                        item.Registro = "SIN CENTIL";
                                        item.EsAprobado = false;
                                    }
                                    else
                                    {
                                        item.Registro = centil.Centil.ToString();
                                        if (Convert.ToDecimal(item.Registro, null) >= calificacion.PuntajeMinimo)
                                        {
                                            item.EsAprobado = true;
                                        }
                                        if (calificacion.EsCalificable == false)
                                        {
                                            item.EsAprobado = false;
                                            item.NotaAprobatoria = "N.A";
                                            item.Simbolo = "";
                                        }
                                    }
                                }
                                else
                                {
                                    if (Convert.ToDecimal(item.Registro, null) >= calificacion.PuntajeMinimo)
                                    {
                                        item.EsAprobado = true;
                                        item.CalificaPorCentil = false;
                                    }
                                    if (calificacion.EsCalificable == false)
                                    {
                                        item.EsAprobado = false;
                                        item.NotaAprobatoria = "N.A";
                                        item.Simbolo = "";
                                    }
                                }
                            }

                            // La Calificacion debe de ser Menor Igual a Valor Minimo
                            if (calificacion.IdProcesoSeleccionRango == 13)
                            {
                                item.Simbolo = "<=";
                                if (calificacion.CalificaPorCentil)
                                {
                                    item.CalificaPorCentil = true;
                                    var centil = centilesCompletos.Where(x => x.IdExamen == item.IdExamen && x.IdSexo == (x.IdSexo == null ? null : item.IdSexo) && (puntaje >= x.ValorMinimo && puntaje <= x.ValorMaximo)).FirstOrDefault();


                                    if (centil == null)
                                    {
                                        item.Registro = "SIN CENTIL";
                                        item.EsAprobado = false;
                                    }
                                    else
                                    {
                                        item.Registro = centil.Centil.ToString();
                                        if (Convert.ToDecimal(item.Registro, null) <= calificacion.PuntajeMinimo)
                                        {
                                            item.EsAprobado = true;
                                        }
                                        if (calificacion.EsCalificable == false)
                                        {
                                            item.EsAprobado = false;
                                            item.NotaAprobatoria = "N.A";
                                            item.Simbolo = "";
                                        }
                                    }
                                }
                                else
                                {
                                    if (Convert.ToDecimal(item.Registro, null) <= calificacion.PuntajeMinimo)
                                    {
                                        item.EsAprobado = true;
                                        item.CalificaPorCentil = false;
                                    }
                                    if (calificacion.EsCalificable == false)
                                    {
                                        item.EsAprobado = false;
                                        item.NotaAprobatoria = "N.A";
                                        item.Simbolo = "";
                                    }
                                }
                            }

                            if (calificacion.IdProcesoSeleccionRango == null || calificacion.IdProcesoSeleccionRango == 0)
                            {
                                if (calificacion.CalificaPorCentil)
                                {
                                    item.CalificaPorCentil = true;
                                    var centil = centilesCompletos.Where(x => x.IdExamen == item.IdExamen && x.IdSexo == (x.IdSexo == null ? null : item.IdSexo) && (puntaje >= x.ValorMinimo && puntaje <= x.ValorMaximo)).FirstOrDefault();


                                    if (centil == null)
                                    {
                                        item.Registro = "SIN CENTIL";
                                        item.EsAprobado = false;
                                    }
                                    else
                                    {
                                        item.Registro = centil.Centil.ToString();
                                        if (Convert.ToDecimal(item.Registro, null) >= (calificacion.PuntajeMinimo == null ? 0 : calificacion.PuntajeMinimo))
                                        {
                                            item.EsAprobado = true;
                                        }
                                        if (calificacion.EsCalificable == false)
                                        {
                                            item.EsAprobado = false;
                                            item.NotaAprobatoria = "N.A";
                                            item.Simbolo = "";
                                        }
                                    }
                                }
                                else
                                {
                                    if (Convert.ToDecimal(item.Registro, null) >= (calificacion.PuntajeMinimo == null ? 0 : calificacion.PuntajeMinimo))
                                    {
                                        item.EsAprobado = true;
                                        item.CalificaPorCentil = false;
                                    }
                                    if (calificacion.EsCalificable == false)
                                    {
                                        item.EsAprobado = false;
                                        item.NotaAprobatoria = "N.A";
                                        item.Simbolo = "";
                                    }
                                }
                            }

                        }
                    }

                    if (!item.Registro.Equals("SIN CENTIL"))
                    {

                        item.Registro = FuncionRedondeo(Convert.ToDecimal(item.Registro), RedondeoGeneral).ToString();
                        if (item.Evaluacion != null && (item.IdEvaluacion == 53 || item.Evaluacion.Equals("GRADUATE MANAGEMENT ADMISSION TEST V2")))
                        {
                            item.Registro = item.Registro + "%";
                        }

                    }

                    item.OrdenReal = item.Orden;
                    // Esta parte se agrego para poder ordenar la evaluacion de Aptitudes a como el señor juan carlos lo requeria. el codigo de orden real esta compuesto por IdEvaluacion IdGrupo IdComponente, lo que forma el codigo de OrdenReal
                    switch (item.OrdenReal)
                    {
                        case 53052:
                            item.OrdenReal = 53051;
                            break;
                        case 53053:
                            item.OrdenReal = 53052;
                            break;
                        case 53051:
                            item.OrdenReal = 53053;
                            break;
                        case 53054:
                            item.OrdenReal = 53054;
                            break;
                    }
                }

                List<ProcesoSelecionExamenesCompletosDTO> listaNotasProcesoSeleccionComplemento = new List<ProcesoSelecionExamenesCompletosDTO>();

                // Esta parte es para que siempre muestre las calificaciones de los componentes asi sea por CalificacionTotal o CalificacionAgrupada.

                //Obtiene solo las evaluaciones con calificacion Total
                var Evaluaciones2 = listaNotasProcesoSeleccion.Where(x => x.IdExamen == 0 && x.IdGrupo == 0).Select(x => new ProcesoSelecionExamenesCompletosComplementoDTO { IdProcesoSeleccion = x.IdProcesoSeleccion, IdEtapa = x.IdEtapa, IdEvaluacion = x.IdEvaluacion, IdGrupo = x.IdGrupo, IdExamen = x.IdExamen, EsAprobado = x.EsAprobado, CalificaPorCentil = x.CalificaPorCentil, Orden = x.Orden, OrdenReal = x.OrdenReal }).ToList();
                var Evaluaciones = Evaluaciones2
                    .GroupBy(x => new { IdProcesoSeleccion = x.IdProcesoSeleccion, IdEtapa = x.IdEtapa, IdEvaluacion = x.IdEvaluacion, IdGrupo = x.IdGrupo, IdExamen = x.IdExamen, EsAprobado = x.EsAprobado, CalificaPorCentil = x.CalificaPorCentil, Orden = x.Orden, OrdenReal = x.OrdenReal })
                    .Select(group =>
                    new ProcesoSelecionExamenesCompletosComplementoDTO
                    {
                        IdProcesoSeleccion = group.Key.IdProcesoSeleccion
                        ,
                        IdEtapa = group.Key.IdEtapa
                        ,
                        IdEvaluacion = group.Key.IdEvaluacion
                        ,
                        IdGrupo = group.Key.IdGrupo
                        ,
                        IdExamen = group.Key.IdExamen
                        ,
                        EsAprobado = group.Key.EsAprobado
                        ,
                        CalificaPorCentil = group.Key.CalificaPorCentil
                        ,
                        Orden = group.Key.Orden
                        ,
                        OrdenReal = group.Key.OrdenReal
                    })
                    .ToList();
                // Obtiene solo las evaluaciones con calificacion agrupada
                var GrupoComponente2 = listaNotasProcesoSeleccion.Where(x => x.IdExamen == 0 && x.IdGrupo != 0).Select(x => new ProcesoSelecionExamenesCompletosComplementoDTO { IdProcesoSeleccion = x.IdProcesoSeleccion, IdEtapa = x.IdEtapa, IdEvaluacion = x.IdEvaluacion, IdGrupo = x.IdGrupo, IdExamen = x.IdExamen, EsAprobado = x.EsAprobado, CalificaPorCentil = x.CalificaPorCentil, Orden = x.Orden, OrdenReal = x.OrdenReal }).Distinct().ToList();
                var GrupoComponente = GrupoComponente2
                    .GroupBy(x => new { IdProcesoSeleccion = x.IdProcesoSeleccion, IdEtapa = x.IdEtapa, IdEvaluacion = x.IdEvaluacion, IdGrupo = x.IdGrupo, IdExamen = x.IdExamen, EsAprobado = x.EsAprobado, CalificaPorCentil = x.CalificaPorCentil, Orden = x.Orden, OrdenReal = x.OrdenReal })
                    .Select(group =>
                    new ProcesoSelecionExamenesCompletosComplementoDTO
                    {
                        IdProcesoSeleccion = group.Key.IdProcesoSeleccion
                        ,
                        IdEtapa = group.Key.IdEtapa
                        ,
                        IdEvaluacion = group.Key.IdEvaluacion
                        ,
                        IdGrupo = group.Key.IdGrupo
                        ,
                        IdExamen = group.Key.IdExamen
                        ,
                        EsAprobado = group.Key.EsAprobado
                        ,
                        CalificaPorCentil = group.Key.CalificaPorCentil
                        ,
                        Orden = group.Key.Orden
                        ,
                        OrdenReal = group.Key.OrdenReal
                    })
                    .ToList();
                foreach (var item in Evaluaciones)
                {
                    count = 0.00M;
                    var EvaluacionesGrupo = reporte.Where(x => x.IdGrupo == null && x.IdEvaluacion == item.IdEvaluacion).ToList();
                    foreach (var componente in EvaluacionesGrupo)
                    {
                        if (componente.Titulo.Equals("N1: Ansiedad"))
                        {
                            var c = 323;
                        }
                        var existe = listaNotasProcesoSeleccionComplemento.Where(x => x.IdEvaluacion == componente.IdEvaluacion && x.IdExamen == componente.IdExamen /*&&x.IdProcesoSeleccion==componente.IdProceso && x.IdEtapa==componente.IdEtapa*/).FirstOrDefault();
                        var eval = new ProcesoSelecionExamenesCompletosDTO
                        {
                            IdProcesoSeleccion = componente.IdProceso,
                            ProcesoSeleccion = componente.NombreProceso,
                            IdPostulante = componente.IdPostulante,
                            Postulante = componente.Postulante,
                            Edad = 24,
                            Examen = componente.NombreExamen,
                            IdCategoria = componente.IdCategoria,
                            Categoria = componente.NombreCategoria,
                            IdExamen = componente.IdExamen,
                            IdGrupo = componente.IdGrupo == null ? 0 : componente.IdGrupo,
                            IdEvaluacion = componente.IdEvaluacion,
                            Evaluacion = componente.NombreEvaluacion,
                            Grupo = componente.NombreGrupo,
                            Etapa = componente.NombreEtapa,
                            IdEtapa = componente.IdEtapa,
                            Orden = item.Orden,
                            Registro = componente.Puntaje.ToString(),
                            EsAprobado = item.EsAprobado,
                            CalificaPorCentil = item.CalificaPorCentil,
                            IdSexo = componente.IdSexo,
                            NotaAprobatoria = "N.A",
                            Simbolo = "",
                            EstadoAcceso = componente.EstadoAcceso,
                            CantidadConfigurado = componente.CantidadConfigurado,
                            CantidadResuelto = componente.CantidadResuelto,
                            PuntajeCurso = componente.PuntajeCurso
                        };
                        if (existe != null)
                        {
                            eval.OrdenReal = existe.OrdenReal;
                        }
                        else
                        {
                            count = count + 0.01M; // esta parte interviene en el orden se le pone como decimal para que los componentes siempre esten dentro de la evaluacion o Grupo de Componentes.
                            eval.OrdenReal = item.OrdenReal + count;
                        }
                        listaNotasProcesoSeleccionComplemento.Add(eval);
                    }

                }
                foreach (var item in GrupoComponente)
                {
                    count = 0.00M;
                    var grupos = reporte.Where(x => x.IdGrupo == item.IdGrupo && x.IdEvaluacion == item.IdEvaluacion).ToList();
                    foreach (var componente in grupos)
                    {
                        if (componente.Titulo.Equals("N1: Ansiedad"))
                        {
                            var c = 323;
                        }
                        var existe = listaNotasProcesoSeleccionComplemento.Where(x => x.IdEvaluacion == componente.IdEvaluacion && x.IdExamen == componente.IdExamen /*&& x.IdProcesoSeleccion == componente.IdProceso && x.IdEtapa == componente.IdEtapa*/).FirstOrDefault();
                        var eval = new ProcesoSelecionExamenesCompletosDTO
                        {
                            IdProcesoSeleccion = componente.IdProceso,
                            ProcesoSeleccion = componente.NombreProceso,
                            IdPostulante = componente.IdPostulante,
                            Postulante = componente.Postulante,
                            Edad = 24,
                            Examen = componente.NombreExamen,
                            IdCategoria = componente.IdCategoria,
                            Categoria = componente.NombreCategoria,
                            IdExamen = componente.IdExamen,
                            IdGrupo = componente.IdGrupo == null ? 0 : componente.IdGrupo,
                            IdEvaluacion = componente.IdEvaluacion,
                            Evaluacion = componente.NombreEvaluacion,
                            Grupo = componente.NombreGrupo,
                            Etapa = componente.NombreEtapa,
                            IdEtapa = componente.IdEtapa,
                            Orden = item.Orden,
                            Registro = componente.Puntaje.ToString(),
                            EsAprobado = item.EsAprobado,
                            CalificaPorCentil = item.CalificaPorCentil,
                            IdSexo = componente.IdSexo,
                            NotaAprobatoria = "N.A",
                            Simbolo = "",
                            EstadoAcceso = componente.EstadoAcceso,
                            CantidadConfigurado = componente.CantidadConfigurado,
                            CantidadResuelto = componente.CantidadResuelto,
                            PuntajeCurso = componente.PuntajeCurso
                        };
                        if (existe != null)
                        {
                            eval.OrdenReal = existe.OrdenReal;
                        }
                        else
                        {

                            count = count + 0.01M;
                            eval.OrdenReal = item.OrdenReal + count;
                        }
                        listaNotasProcesoSeleccionComplemento.Add(eval);
                    }

                }

                // Aqui a las calificaciones de componentes de las Evaluaciones y Grupo de Componentes se le calcula su puntaje por centil y no se evalua si esta aprobado
                //o no ya que en la configuracion el que deberia de tener la calificacion es la Evaluacion o el Grupo de Componentes que faltaban obtener 
                foreach (var item in listaNotasProcesoSeleccionComplemento)
                {

                    var puntaje = Convert.ToDecimal(item.Registro, null);

                    if (item.CalificaPorCentil)
                    {
                        item.CalificaPorCentil = true;
                        var centil = centilesCompletos.Where(x => x.IdExamen == item.IdExamen && x.IdSexo == (x.IdSexo == null ? null : item.IdSexo) && (puntaje >= x.ValorMinimo && puntaje <= x.ValorMaximo)).FirstOrDefault();

                        if (centil == null)
                        {
                            item.Registro = "SIN CENTIL";
                            item.EsAprobado = false;
                        }
                        else
                        {
                            item.Registro = centil.Centil.ToString();
                        }
                    }
                    if (!item.Registro.Equals("SIN CENTIL") && item.IdEvaluacion != 53)
                    {
                        var puntaje2 = Convert.ToDecimal(item.Registro, null);
                        item.Registro = FuncionRedondeo(puntaje2, RedondeoGeneral).ToString();
                    }
                }

                //finalmente se concatenan las dos listas para obtener el resultado final
                listaNotasProcesoSeleccion = listaNotasProcesoSeleccion.Concat(listaNotasProcesoSeleccionComplemento).ToList();
                return listaNotasProcesoSeleccion.OrderBy(x => x.OrdenReal.Value).ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Autor: Edgar S .
        /// Fecha: 12/05/2021
        /// Version: 1.0
        /// <summary>
        /// Promedia lista de Puntajes para componente, grupo o evaluación
        /// </summary>
        /// <param name="listaPuntaje"> Lista de puntajes recibidos </param>
        /// <returns> int </returns>
        public decimal PromediarListaPuntaje(List<decimal?> listaPuntaje, int denominador, decimal factor)
        {
            decimal resultado = 0;
            if (denominador > 0) resultado = (decimal)(listaPuntaje.Sum() / Convert.ToDecimal(denominador));
            return resultado * factor;
        }

        /// Autor: Edgar S .
        /// Fecha: 12/05/2021
        /// Version: 1.0
        /// <summary>
        /// Redondea determinado valor en decimal según la cantidad de decimales configurados
        /// </summary>
        /// <param name="numero"> Número no redondeado </param>
        /// <param name="cantidadDecimales"> Número no redondeado </param>
        /// <returns> decimal </returns>
        public decimal FuncionRedondeo(decimal numero, int cantidadDecimales)
        {
            return Math.Round(numero, cantidadDecimales, MidpointRounding.AwayFromZero);
        }

        /// Autor: Edgar S .
        /// Fecha: 12/05/2021
        /// Version: 1.0
        /// <summary>
        /// Promedia lista de Puntajes para componente, grupo o evaluación
        /// </summary>
        /// <param name="listaPuntaje"> Lista de puntajes recibidos </param>
        /// <returns> int </returns>
        public decimal PromediarPuntaje(decimal Puntaje, int denominador, decimal factor)
        {
            decimal resultado = 0;
            if (denominador > 0) resultado = (decimal)(Puntaje / Convert.ToDecimal(denominador));
            return resultado * factor;
        }

        /// Autor: Edgar S .
        /// Fecha: 12/05/2021
        /// Version: 1.0
        /// <summary>
        /// Obtiene puntaje de examen para su calificación
        /// </summary>
        /// <param name="filtro"> Valores de búsqueda para recolección de información </param>
        /// <returns> AgrupacionDetalleEvaluacionDTO </returns>
        public AgrupacionDetalleEvaluacionDTO CalificarAutomaticamenteNuevaVersion(CalificacionAutomaticaDTO filtro)
        {
            try
            {
                FormulaPuntajeRepositorio _repFormulaPuntaje = new FormulaPuntajeRepositorio(_integraDBContext);
                ExamenTestRepositorio _repExamenTest = new ExamenTestRepositorio(_integraDBContext);
                ProcesoSeleccionPuntajeCalificacionRepositorio _repProcesoSeleccionPuntajeCalificacion = new ProcesoSeleccionPuntajeCalificacionRepositorio(_integraDBContext);
                GrupoComponenteEvaluacionRepositorio _repGrupoComponenteEvaluacion = new GrupoComponenteEvaluacionRepositorio(_integraDBContext);
                AsignacionPreguntaExamenRepositorio _repAsignacionPreguntaExamen = new AsignacionPreguntaExamenRepositorio(_integraDBContext);
                ExamenRepositorio _repExamen = new ExamenRepositorio(_integraDBContext);
                CentilRepositorio _repCentil = new CentilRepositorio(_integraDBContext);

                List<DatosExamenPostulanteDTO> reporte;

                reporte = _repExamen.ObtenerPuntajeExamenTest(filtro);
                if (reporte == null || reporte.Count == 0)
                {
                    AgrupacionDetalleEvaluacionDTO siVacio = new AgrupacionDetalleEvaluacionDTO();
                    return siVacio;
                }

                //Obtenemos la configuración de fórmulas de calificación de componentes, grupos y evaluaciones
                var configuracionExamen = _repExamen.ObtenerConfiguracionPuntaje();

                //Asignamos el puntaje real al puntaje de cada componente unitario                
                foreach (var componente in reporte)
                {
                    //Obtenemos la cantidad de preguntas para realizar el promedio en caso fuera necesario
                    var cantidadPreguntaComponenteConfigurado = configuracionExamen.Where(x => x.IdExamen == componente.IdExamen).FirstOrDefault();
                    var formulaComponente = componente.IdFormulaComponente.GetValueOrDefault();
                    if (formulaComponente > 0 && cantidadPreguntaComponenteConfigurado != null && cantidadPreguntaComponenteConfigurado.CantidadPreguntas > 0)
                    {
                        // 3 = PROMEDIAR(Puntaje preguntas)
                        if (formulaComponente == 3) componente.Puntaje = componente.Puntaje * componente.FactorComponente / cantidadPreguntaComponenteConfigurado.CantidadPreguntas;
                        else componente.Puntaje = componente.Puntaje * componente.FactorComponente;
                    }
                    else componente.Puntaje = componente.Puntaje * componente.FactorComponente;
                }

                //Se agrupan las notas o calificaciones por postulante para que de esta manera podamos calcular su calificacion.
                var listaPostulante = reporte.GroupBy(u => new { u.Postulante, u.NombreProceso, u.IdPostulante })
                    .Select(group =>
                    new DatosNotaPorPostulanteDTO
                    {
                        IdPostulante = group.Key.IdPostulante,
                        Postulante = group.Key.Postulante,
                        NombreProceso = group.Key.NombreProceso,
                        ListaNotas = group.Select(x => new NotaPostulanteDTO { IdProceso = x.IdProceso, ProcesoSeleccion = x.NombreProceso, IdSexo = x.IdSexo, IdEvaluacion = x.IdEvaluacion, IdGrupo = x.IdGrupo, IdExamen = x.IdExamen, NombreEvaluacion = x.NombreEvaluacion, NombreExamen = x.NombreExamen, NombreGrupo = x.NombreGrupo, Puntaje = x.Puntaje, IdCategoria = x.IdCategoria, IdEtapa = x.IdEtapa, NombreCategoria = x.NombreCategoria, NombreEtapa = x.NombreEtapa, FactorComponente = x.FactorComponente, FactorEvaluacion = x.FactorEvaluacion, FactorGrupo = x.FactorGrupo, IdFormulaComponente = x.IdFormulaComponente, IdFormulaEvaluacion = x.IdFormulaEvaluacion, IdFormulaGrupo = x.IdFormulaGrupo }).ToList()
                    }).ToList();

                List<ProcesoSelecionExamenesCompletosDTO> listaNotasProcesoSeleccion = new List<ProcesoSelecionExamenesCompletosDTO>();
                Decimal count = 0.00M;

                foreach (var postulante in listaPostulante)
                {
                    count = 0.00M;
                    //Se obtiene en una lista todas las evaluaciones de un postulante
                    List<int?> ListaEvaluacion = postulante.ListaNotas.Where(x => x.IdEvaluacion != null).Select(x => x.IdEvaluacion).Distinct().ToList();

                    //Se agrupan las evaluaciones segun sus notas obtenidas por componentes, recordar que una evaluacion puede tener mas de un componente
                    var grupoEvaluacion = postulante.ListaNotas.GroupBy(u => new { u.IdEvaluacion, u.NombreEvaluacion })
                        .Select(group => new EvaluacionPostulanteDTO
                        {
                            IdEvaluacion = group.Key.IdEvaluacion,
                            ListaComponentesEvaluacion = group.Select(x => new NotaPostulanteDTO { IdProceso = x.IdProceso, ProcesoSeleccion = x.ProcesoSeleccion, IdSexo = x.IdSexo, IdEvaluacion = x.IdEvaluacion, IdGrupo = x.IdGrupo, IdExamen = x.IdExamen, NombreEvaluacion = x.NombreEvaluacion, NombreExamen = x.NombreExamen, NombreGrupo = x.NombreGrupo, Puntaje = x.Puntaje, IdCategoria = x.IdCategoria, IdEtapa = x.IdEtapa, NombreCategoria = x.NombreCategoria, NombreEtapa = x.NombreEtapa, FactorComponente = x.FactorComponente, FactorEvaluacion = x.FactorEvaluacion, FactorGrupo = x.FactorGrupo, IdFormulaComponente = x.IdFormulaComponente, IdFormulaEvaluacion = x.IdFormulaEvaluacion, IdFormulaGrupo = x.IdFormulaGrupo }).ToList()
                        }
                        ).ToList();

                    //Se recorre las evaluaciones de un postulante
                    foreach (var evaluacion in grupoEvaluacion)
                    {
                        var calificacionTotal = _repExamenTest.GetBy(x => x.Estado == true && x.Id == evaluacion.IdEvaluacion).FirstOrDefault();// se obtiene informacion de cada evaluacion del postulante
                        bool esAgrupada = evaluacion.ListaComponentesEvaluacion.Where(x => x.IdGrupo != null).Count() >= 1; // Verifica si las evaluaciones tienen al menos un grupo de Evaluacion para considerar la calificacion por Grupos
                        ProcesoSelecionExamenesCompletosDTO eval = new ProcesoSelecionExamenesCompletosDTO();

                        //Inserta la Nota por Evaluacion, su nota total y calculada
                        // el campo de CalificarEvaluacion indica si se califica por Evaluacion en caso sea true 
                        if (calificacionTotal != null && calificacionTotal.CalificarEvaluacion == true)
                        {
                            var formulaPuntajeEvaluacion = _repFormulaPuntaje.FirstById(calificacionTotal.IdFormulaPuntaje.Value); // extrae el valor de como se va a calificar la evaluacion
                            if (formulaPuntajeEvaluacion.Nombre.Contains("(Puntaje componentes)")) // Si se califica por Puntaje de Componentes, de todos los componentes de una evaluacion suma o promedia sus puntajes y esa seria la calificacion de la Evaluacion
                            {
                                var cantidadComponentes = configuracionExamen.Where(x => x.IdExamenTest == evaluacion.IdEvaluacion).Count();

                                eval = new ProcesoSelecionExamenesCompletosDTO
                                {
                                    IdProcesoSeleccion = evaluacion.ListaComponentesEvaluacion[0].IdProceso,
                                    ProcesoSeleccion = evaluacion.ListaComponentesEvaluacion[0].ProcesoSeleccion,
                                    IdPostulante = postulante.IdPostulante,
                                    Postulante = postulante.Postulante,
                                    Edad = 24,
                                    Examen = null,
                                    IdCategoria = evaluacion.ListaComponentesEvaluacion[0].IdCategoria,
                                    Categoria = evaluacion.ListaComponentesEvaluacion[0].NombreCategoria,
                                    IdExamen = 0,
                                    IdGrupo = 0,
                                    IdEvaluacion = evaluacion.ListaComponentesEvaluacion[0].IdEvaluacion,
                                    Evaluacion = evaluacion.ListaComponentesEvaluacion[0].NombreEvaluacion,
                                    Grupo = null,
                                    IdEtapa = evaluacion.ListaComponentesEvaluacion[0].IdEtapa,
                                    Etapa = evaluacion.ListaComponentesEvaluacion[0].NombreEtapa,
                                    Orden = Convert.ToInt32(String.Concat(evaluacion.ListaComponentesEvaluacion[0].IdEvaluacion.ToString(), "0", "0")),
                                    Registro = formulaPuntajeEvaluacion.Nombre.ToUpper().Contains("SUMAR") ? evaluacion.ListaComponentesEvaluacion.Sum(x => x.Puntaje).ToString() : PromediarListaPuntaje(evaluacion.ListaComponentesEvaluacion.Select(x => x.Puntaje).ToList(), cantidadComponentes, 1.00M).ToString(),
                                    EsAprobado = false,
                                    CalificaPorCentil = false,
                                    IdSexo = evaluacion.ListaComponentesEvaluacion[0].IdSexo
                                };
                                count = count + 1M;
                                eval.OrdenReal = count; //Esta parte se utilizara para darle un orden a cada componente.
                                listaNotasProcesoSeleccion.Add(eval); // añade la Nota por Evaluacion a la listafinal
                            }
                            else // de lo contrario la calificacion de la Evaluacion se calcula mediante la suma o promedio de GRUPO DE COMPONENTES
                            {
                                List<ProcesoSelecionExamenesCompletosDTO> listaGrupo = new List<ProcesoSelecionExamenesCompletosDTO>();
                                foreach (var componente in evaluacion.ListaComponentesEvaluacion)
                                {
                                    // Verifica si se insertado el componente al Grupo de componentes listaGrupo
                                    var eval1 = listaGrupo.Where(x => x.IdPostulante == postulante.IdPostulante && x.IdEvaluacion == componente.IdEvaluacion && x.IdGrupo == componente.IdGrupo).FirstOrDefault();
                                    if (eval1 == null) // aun no existe el grupo de componentes en  listaGrupo por eso se inserta
                                    {
                                        eval = new ProcesoSelecionExamenesCompletosDTO
                                        {
                                            IdProcesoSeleccion = evaluacion.ListaComponentesEvaluacion[0].IdProceso,
                                            ProcesoSeleccion = evaluacion.ListaComponentesEvaluacion[0].ProcesoSeleccion,
                                            IdPostulante = postulante.IdPostulante,
                                            Postulante = postulante.Postulante,
                                            Edad = 24,
                                            Examen = null,
                                            IdExamen = 0,
                                            IdGrupo = componente.IdGrupo,
                                            IdFormulaGrupo = componente.IdFormulaGrupo,
                                            IdEvaluacion = componente.IdEvaluacion,
                                            Evaluacion = componente.NombreEvaluacion,
                                            Grupo = componente.FactorComponente.ToString(),
                                            Orden = Convert.ToInt32(String.Concat(componente.IdEvaluacion, componente.IdGrupo == null ? "0" : componente.IdGrupo.ToString(), "0")),
                                            Registro = componente.Puntaje.ToString(),
                                            EsAprobado = false,
                                            CalificaPorCentil = false
                                        };
                                        listaGrupo.Add(eval);
                                    }
                                    else
                                    {
                                        //si el grupo ya existe, se suma el puntaje anterior obtenido de la consulta eval1
                                        listaGrupo.Where(x => x.IdPostulante == postulante.IdPostulante && x.IdEvaluacion == componente.IdEvaluacion && x.IdGrupo == componente.IdGrupo).FirstOrDefault().Registro = (Convert.ToDecimal(eval1.Registro, null) + componente.Puntaje).ToString();
                                    }
                                }

                                // Ahora se obtiene los puntajes obtenidos en el grupo y se aplica la fórmula configurada para el grupo
                                foreach (var grupo in listaGrupo)
                                {
                                    //Obtenemos la cantidad de grupos para realizar el promedio en caso fuera necesario
                                    var cantidadGrupoConfigurado = configuracionExamen.Where(x => x.IdExamenTest == evaluacion.IdEvaluacion && x.IdGrupo == grupo.IdGrupo).ToList();
                                    var formulaGrupo = grupo.IdFormulaGrupo.GetValueOrDefault();
                                    if (formulaGrupo > 0 && cantidadGrupoConfigurado.Count > 0)
                                    {
                                        if (formulaGrupo == 6) // 6 = PROMEDIAR(Puntaje componentes)
                                        {
                                            grupo.Registro = (PromediarPuntaje(Convert.ToDecimal(grupo.Registro), cantidadGrupoConfigurado.Count, 1.00M)).ToString();
                                        }
                                    }
                                }

                                // se obtiene el puntaje de la evaluacion de acuerdo a los grupos de componentes que se encuentran almacenadoes en listaGrupo
                                eval = new ProcesoSelecionExamenesCompletosDTO
                                {
                                    IdProcesoSeleccion = evaluacion.ListaComponentesEvaluacion[0].IdProceso,
                                    ProcesoSeleccion = evaluacion.ListaComponentesEvaluacion[0].ProcesoSeleccion,
                                    IdPostulante = postulante.IdPostulante,
                                    Postulante = postulante.Postulante,
                                    Edad = 24,
                                    Examen = null,
                                    Categoria = evaluacion.ListaComponentesEvaluacion[0].NombreCategoria,
                                    IdCategoria = evaluacion.ListaComponentesEvaluacion[0].IdCategoria,
                                    IdExamen = 0,
                                    IdGrupo = 0,
                                    IdEvaluacion = evaluacion.ListaComponentesEvaluacion[0].IdEvaluacion,
                                    Evaluacion = evaluacion.ListaComponentesEvaluacion[0].NombreEvaluacion,
                                    Grupo = null,
                                    Etapa = evaluacion.ListaComponentesEvaluacion[0].NombreEtapa,
                                    IdEtapa = evaluacion.ListaComponentesEvaluacion[0].IdEtapa,
                                    Orden = Convert.ToInt32(String.Concat(evaluacion.ListaComponentesEvaluacion[0].IdEvaluacion.ToString(), "0", "0")),

                                    //Verifica si la formula de la evaluacion es Suma de Grupo de Componentes o Promedio de los mismos
                                    Registro = formulaPuntajeEvaluacion.Nombre.ToUpper().Contains("SUMAR") ? listaGrupo.Sum(x => Convert.ToDecimal(x.Registro, null) * Convert.ToDecimal(x.Grupo, null)).ToString() : (listaGrupo.Sum(x => Convert.ToDecimal(x.Registro, null) * Convert.ToDecimal(x.Grupo, null)) / Convert.ToDecimal(listaGrupo.Count(), null)).ToString(),
                                    EsAprobado = false,
                                    CalificaPorCentil = false,
                                    IdSexo = evaluacion.ListaComponentesEvaluacion[0].IdSexo
                                };
                                count = count + 1M;
                                eval.OrdenReal = count;
                                listaNotasProcesoSeleccion.Add(eval);
                            }
                        }
                        else
                        {
                            //listaNotasProcesoSeleccion
                            List<ProcesoSelecionExamenesCompletosDTO> listaAuxiliarGrupo = new List<ProcesoSelecionExamenesCompletosDTO>();

                            //Si la calificacion no es por Evaluacion entonces puede ser por Grupo de Componentes o componentes.
                            foreach (var componente in evaluacion.ListaComponentesEvaluacion)
                            {
                                //Inserta los Grupos de Componentes con su Calificacion
                                if (esAgrupada == true)
                                {
                                    var eval1 = listaAuxiliarGrupo.Where(x => x.IdPostulante == postulante.IdPostulante && x.IdEvaluacion == componente.IdEvaluacion && x.IdGrupo == componente.IdGrupo).FirstOrDefault();

                                    if (eval1 == null)
                                    {
                                        eval = new ProcesoSelecionExamenesCompletosDTO
                                        {
                                            IdProcesoSeleccion = componente.IdProceso,
                                            ProcesoSeleccion = componente.ProcesoSeleccion,
                                            IdPostulante = postulante.IdPostulante,
                                            Postulante = postulante.Postulante,
                                            Edad = 24,
                                            Examen = null,
                                            IdCategoria = componente.IdCategoria,
                                            Categoria = componente.NombreCategoria,
                                            IdExamen = 0,
                                            IdGrupo = componente.IdGrupo,
                                            IdEvaluacion = componente.IdEvaluacion,
                                            Evaluacion = componente.NombreEvaluacion,
                                            IdFormulaGrupo = componente.IdFormulaGrupo,
                                            Grupo = componente.NombreGrupo,
                                            Etapa = componente.NombreEtapa,
                                            IdEtapa = componente.IdEtapa,
                                            Orden = Convert.ToInt32(String.Concat(componente.IdEvaluacion, componente.IdGrupo == null ? "0" : componente.IdGrupo.ToString(), "0")),
                                            Registro = componente.Puntaje.ToString(),
                                            EsAprobado = false,
                                            CalificaPorCentil = false,
                                            IdSexo = componente.IdSexo,
                                            OrdenReal = count++
                                        };
                                        count = count + 1M;
                                        eval.OrdenReal = count;
                                        listaAuxiliarGrupo.Add(eval);
                                    }
                                    else
                                    {
                                        listaAuxiliarGrupo.Where(x => x.IdPostulante == postulante.IdPostulante && x.IdEvaluacion == componente.IdEvaluacion && x.IdGrupo == componente.IdGrupo).FirstOrDefault().Registro = (Convert.ToDecimal(eval1.Registro, null) + (componente.Puntaje)).ToString();
                                    }
                                }
                                //Inserta los Componentes con su Calificacion respectiva, es decir no realiza ni un calculo solo inserta lo componentes que se obtuvieron en la vista principal
                                else
                                {
                                    eval = new ProcesoSelecionExamenesCompletosDTO
                                    {
                                        IdProcesoSeleccion = componente.IdProceso,
                                        ProcesoSeleccion = componente.ProcesoSeleccion,
                                        IdPostulante = postulante.IdPostulante,
                                        Postulante = postulante.Postulante,
                                        Edad = 24,
                                        Examen = componente.NombreExamen,
                                        IdCategoria = componente.IdCategoria,
                                        Categoria = componente.NombreCategoria,
                                        IdExamen = componente.IdExamen,
                                        IdGrupo = componente.IdGrupo == null ? 0 : componente.IdGrupo,
                                        IdEvaluacion = componente.IdEvaluacion,
                                        Evaluacion = componente.NombreEvaluacion,
                                        Grupo = componente.NombreGrupo,
                                        IdFormulaGrupo = componente.IdFormulaGrupo,
                                        Etapa = componente.NombreEtapa,
                                        IdEtapa = componente.IdEtapa,
                                        Orden = Convert.ToInt32(String.Concat(componente.IdEvaluacion, componente.IdGrupo == null ? "0" : componente.IdGrupo.ToString(), componente.IdExamen == null ? "0" : componente.IdExamen.ToString())),
                                        Registro = componente.Puntaje.ToString(),
                                        EsAprobado = false,
                                        CalificaPorCentil = false,
                                        IdSexo = componente.IdSexo
                                    };
                                    count = count + 1M;
                                    eval.OrdenReal = count;
                                    listaAuxiliarGrupo.Add(eval); // inserta la calificacion de los grupos de componentes 
                                }
                            }

                            // Ahora se obtiene los puntajes obtenidos en el grupo y se aplica la fórmula configurada para el grupo
                            foreach (var grupo in listaAuxiliarGrupo)
                            {
                                //Obtenemos la cantidad de grupos para realizar el promedio en caso fuera necesario
                                var cantidadGrupoConfigurado = configuracionExamen.Where(x => x.IdExamenTest == evaluacion.IdEvaluacion && x.IdGrupo == grupo.IdGrupo).ToList();
                                var formulaGrupo = grupo.IdFormulaGrupo.GetValueOrDefault();
                                if (formulaGrupo > 0 && cantidadGrupoConfigurado.Count > 0)
                                {
                                    if (formulaGrupo == 6) // 6 = PROMEDIAR(Puntaje componentes)
                                    {
                                        grupo.Registro = (PromediarPuntaje(Convert.ToDecimal(grupo.Registro), cantidadGrupoConfigurado.Count, 1.00M)).ToString();
                                    }
                                }
                            }

                            listaNotasProcesoSeleccion.AddRange(listaAuxiliarGrupo);
                        }
                    }
                }

                var informacionCentilCalificacion = new List<ObtenerCalificacionCentilDTO>();
                if (filtro.IdProcesoSeleccion != null && filtro.IdProcesoSeleccion > 0)
                {
                    var listaProcesoSeleccion = filtro.IdProcesoSeleccion.ToString();
                    informacionCentilCalificacion = _repExamen.ObtenerInformacionCentilPorProcesoSeleccion(listaProcesoSeleccion);
                }
                else
                {
                    var pruebaCalificacion = reporte.Select(x => x.IdProceso).Distinct().ToList();
                    if (pruebaCalificacion != null && pruebaCalificacion.Count > 0)
                    {
                        var hallarCalificacion = "";
                        for (var i = 0; pruebaCalificacion.Count > i; i++)
                        {
                            if (i == 0) hallarCalificacion = pruebaCalificacion[i] + "";
                            else hallarCalificacion = hallarCalificacion + "," + pruebaCalificacion[i];
                        }
                        informacionCentilCalificacion = _repExamen.ObtenerInformacionCentilPorProcesoSeleccion(hallarCalificacion);
                    }
                }

                List<CentilBO> centilesCompletos = new List<CentilBO>();
                var informacionCentilCalificacionAuxiliar = _repCentil.GetBy(x => x.IdExamenTest == null).ToList();
                centilesCompletos.AddRange(informacionCentilCalificacionAuxiliar);
                List<CentilBO> centilesAsociados = new List<CentilBO>();
                if (informacionCentilCalificacion.Count > 0)
                {
                    var prueba = informacionCentilCalificacion.Where(x => x.IdExamen != null).Select(x => x.IdExamen).Distinct().ToList();
                    if (prueba.Count > 0)
                    {
                        var hallarCentilesAsociados = "";
                        for (var i = 0; prueba.Count > i; i++)
                        {
                            if (i == 0) hallarCentilesAsociados = prueba[i] + "";
                            else hallarCentilesAsociados = hallarCentilesAsociados + "," + prueba[i];
                        }
                        centilesAsociados = _repExamen.ObtenerCentilesAsociados(hallarCentilesAsociados);
                        centilesCompletos.AddRange(centilesAsociados);
                    }
                }

                // En esta parte se usa la configuracion del proceso para saber si se califica por centil o en forma directa, en caso se califica por centil se va a la tabla de centiles y se busca su calificacion
                //tambien segun lo parametros configurados, se calcula si un postulante aprueba o no
                foreach (var item in listaNotasProcesoSeleccion)
                {
                    item.OrdenReal = item.Orden;
                    var puntaje = Convert.ToDecimal(item.Registro, null);
                    if (item.Evaluacion != null && (item.IdEvaluacion == 53 || item.Evaluacion.Equals("GRADUATE MANAGEMENT ADMISSION TEST V2")))
                    {
                        var countPregunta = Convert.ToDecimal(_repAsignacionPreguntaExamen.GetBy(x => x.IdExamen == item.IdExamen).Count(), null);
                        item.Registro = FuncionRedondeo((puntaje * 100.0M) / countPregunta, RedondeoGeneral).ToString();
                    }

                    // Cambia los puntajes de las Evaluaciones
                    if (item.IdEvaluacion != 0 && item.IdGrupo == 0 && item.IdExamen == 0)
                    {
                        //var calificacionOriginal = repProcesoSeleccionPuntajeCalificacionRep.FirstBy(x => x.Estado == true && x.IdExamenTest == item.IdEvaluacion && x.IdGrupoComponenteEvaluacion == null && x.IdExamen == null && x.IdProcesoSeleccion == item.IdProcesoSeleccion);
                        var calificacion = informacionCentilCalificacion.Where(x => x.IdExamenTest == item.IdEvaluacion && x.IdGrupoComponenteEvaluacion == null && x.IdExamen == null && x.IdProcesoSeleccion == item.IdProcesoSeleccion).FirstOrDefault();

                        if (calificacion == null)
                        {
                            item.EsAprobado = false;
                            item.NotaAprobatoria = "N.A";
                        }
                        else
                        {
                            // La Calificacion debe de ser Igual a Valor minimo del componente o evaluacion
                            item.NotaAprobatoria = calificacion.PuntajeMinimo.ToString();
                            if (calificacion.IdProcesoSeleccionRango == 1)
                            {
                                item.Simbolo = "=";
                                if (calificacion.CalificaPorCentil)
                                {
                                    item.CalificaPorCentil = true;
                                    var centil = centilesCompletos.Where(x => x.IdExamenTest == item.IdEvaluacion && x.IdSexo == (x.IdSexo == null ? null : item.IdSexo) && (puntaje >= x.ValorMinimo && puntaje <= x.ValorMaximo)).FirstOrDefault();

                                    if (centil == null)
                                    {
                                        item.Registro = "SIN CENTIL";
                                        item.EsAprobado = false;
                                    }
                                    else
                                    {
                                        item.Registro = centil.Centil.ToString();
                                        if (calificacion.PuntajeMinimo == Convert.ToDecimal(item.Registro, null))
                                        {
                                            item.EsAprobado = true;
                                        }
                                        if (calificacion.EsCalificable == false)
                                        {
                                            item.EsAprobado = false;
                                            item.NotaAprobatoria = "N.A";
                                            item.Simbolo = "";
                                        }
                                    }
                                }
                                else
                                {
                                    if (calificacion.PuntajeMinimo == Convert.ToDecimal(item.Registro, null))
                                    {
                                        item.EsAprobado = true;
                                        item.CalificaPorCentil = false;
                                    }
                                    if (calificacion.EsCalificable == false)
                                    {
                                        item.EsAprobado = false;
                                        item.NotaAprobatoria = "N.A";
                                        item.Simbolo = "";
                                    }
                                }
                            }

                            // La Calificacion debe de ser Mayor a Valor Minimo
                            if (calificacion.IdProcesoSeleccionRango == 2)
                            {
                                item.Simbolo = ">";
                                if (calificacion.CalificaPorCentil)
                                {
                                    item.CalificaPorCentil = true;
                                    var centil = centilesCompletos.Where(x => x.IdExamenTest == item.IdEvaluacion && x.IdSexo == (x.IdSexo == null ? null : item.IdSexo) && (puntaje >= x.ValorMinimo && puntaje <= x.ValorMaximo)).FirstOrDefault();

                                    if (centil == null)
                                    {
                                        item.Registro = "SIN CENTIL";
                                        item.EsAprobado = false;
                                    }
                                    else
                                    {
                                        item.Registro = centil.Centil.ToString();
                                        if (Convert.ToDecimal(item.Registro, null) > calificacion.PuntajeMinimo)
                                        {
                                            item.EsAprobado = true;
                                        }
                                        if (calificacion.EsCalificable == false)
                                        {
                                            item.EsAprobado = false;
                                            item.NotaAprobatoria = "N.A";
                                            item.Simbolo = "";
                                        }
                                    }
                                }
                                else
                                {
                                    if (Convert.ToDecimal(item.Registro, null) > calificacion.PuntajeMinimo)
                                    {
                                        item.EsAprobado = true;
                                        item.CalificaPorCentil = false;
                                    }
                                    if (calificacion.EsCalificable == false)
                                    {
                                        item.EsAprobado = false;
                                        item.NotaAprobatoria = "N.A";
                                        item.Simbolo = "";
                                    }
                                }
                            }

                            // La Calificacion debe de ser Menor a Valor Minimo
                            if (calificacion.IdProcesoSeleccionRango == 9)
                            {
                                item.Simbolo = "<";
                                if (calificacion.CalificaPorCentil)
                                {
                                    item.CalificaPorCentil = true;
                                    var centil = centilesCompletos.Where(x => x.IdExamenTest == item.IdEvaluacion && x.IdSexo == (x.IdSexo == null ? null : item.IdSexo) && (puntaje >= x.ValorMinimo && puntaje <= x.ValorMaximo)).FirstOrDefault();

                                    if (centil == null)
                                    {
                                        item.Registro = "SIN CENTIL";
                                        item.EsAprobado = false;
                                    }
                                    else
                                    {
                                        item.Registro = centil.Centil.ToString();
                                        if (Convert.ToDecimal(item.Registro, null) < calificacion.PuntajeMinimo)
                                        {
                                            item.EsAprobado = true;
                                        }
                                        if (calificacion.EsCalificable == false)
                                        {
                                            item.EsAprobado = false;
                                            item.NotaAprobatoria = "N.A";
                                            item.Simbolo = "";
                                        }
                                    }
                                }
                                else
                                {
                                    if (Convert.ToDecimal(item.Registro, null) < calificacion.PuntajeMinimo)
                                    {
                                        item.EsAprobado = true;
                                        item.CalificaPorCentil = false;
                                    }
                                    if (calificacion.EsCalificable == false)
                                    {
                                        item.EsAprobado = false;
                                        item.NotaAprobatoria = "N.A";
                                        item.Simbolo = "";
                                    }
                                }
                            }

                            // La Calificacion debe de ser Mayor Igual a Valor Minimo
                            if (calificacion.IdProcesoSeleccionRango == 12)
                            {
                                item.Simbolo = ">=";
                                if (calificacion.CalificaPorCentil)
                                {
                                    item.CalificaPorCentil = true;
                                    var centil = centilesCompletos.Where(x => x.IdExamenTest == item.IdEvaluacion && x.IdSexo == (x.IdSexo == null ? null : item.IdSexo) && (puntaje >= x.ValorMinimo && puntaje <= x.ValorMaximo)).FirstOrDefault();

                                    if (centil == null)
                                    {
                                        item.Registro = "SIN CENTIL";
                                        item.EsAprobado = false;
                                    }
                                    else
                                    {
                                        item.Registro = centil.Centil.ToString();
                                        if (Convert.ToDecimal(item.Registro, null) >= calificacion.PuntajeMinimo)
                                        {
                                            item.EsAprobado = true;
                                        }
                                        if (calificacion.EsCalificable == false)
                                        {
                                            item.EsAprobado = false;
                                            item.NotaAprobatoria = "N.A";
                                            item.Simbolo = "";
                                        }
                                    }
                                }
                                else
                                {
                                    if (Convert.ToDecimal(item.Registro, null) >= calificacion.PuntajeMinimo)
                                    {
                                        item.EsAprobado = true;
                                        item.CalificaPorCentil = false;
                                    }
                                    if (calificacion.EsCalificable == false)
                                    {
                                        item.EsAprobado = false;
                                        item.NotaAprobatoria = "N.A";
                                        item.Simbolo = "";
                                    }
                                }
                            }

                            // La Calificacion debe de ser Menor Igual a Valor Minimo
                            if (calificacion.IdProcesoSeleccionRango == 13)
                            {
                                item.Simbolo = "<=";
                                if (calificacion.CalificaPorCentil)
                                {
                                    item.CalificaPorCentil = true;
                                    var centil = centilesCompletos.Where(x => x.IdExamenTest == item.IdEvaluacion && x.IdSexo == (x.IdSexo == null ? null : item.IdSexo) && (puntaje >= x.ValorMinimo && puntaje <= x.ValorMaximo)).FirstOrDefault();

                                    if (centil == null)
                                    {
                                        item.Registro = "SIN CENTIL";
                                        item.EsAprobado = false;
                                    }
                                    else
                                    {
                                        item.Registro = centil.Centil.ToString();
                                        if (Convert.ToDecimal(item.Registro, null) <= calificacion.PuntajeMinimo)
                                        {
                                            item.EsAprobado = true;
                                        }
                                        if (calificacion.EsCalificable == false)
                                        {
                                            item.EsAprobado = false;
                                            item.NotaAprobatoria = "N.A";
                                            item.Simbolo = "";
                                        }
                                    }
                                }
                                else
                                {
                                    if (Convert.ToDecimal(item.Registro, null) <= calificacion.PuntajeMinimo)
                                    {
                                        item.EsAprobado = true;
                                        item.CalificaPorCentil = false;
                                    }
                                    if (calificacion.EsCalificable == false)
                                    {
                                        item.EsAprobado = false;
                                        item.NotaAprobatoria = "N.A";
                                        item.Simbolo = "";
                                    }
                                }
                            }

                            if (calificacion.IdProcesoSeleccionRango == null || calificacion.IdProcesoSeleccionRango == 0)
                            {
                                if (calificacion.CalificaPorCentil)
                                {
                                    item.CalificaPorCentil = true;
                                    var centil = centilesCompletos.Where(x => x.IdExamenTest == item.IdEvaluacion && x.IdSexo == (x.IdSexo == null ? null : item.IdSexo) && (puntaje >= x.ValorMinimo && puntaje <= x.ValorMaximo)).FirstOrDefault();

                                    if (centil == null)
                                    {
                                        item.Registro = "SIN CENTIL";
                                        item.EsAprobado = false;
                                    }
                                    else
                                    {
                                        item.Registro = centil.Centil.ToString();
                                        if (Convert.ToDecimal(item.Registro, null) >= (calificacion.PuntajeMinimo == null ? 0 : calificacion.PuntajeMinimo))
                                        {
                                            item.EsAprobado = true;
                                        }
                                        if (calificacion.EsCalificable == false)
                                        {
                                            item.EsAprobado = false;
                                            item.NotaAprobatoria = "N.A";
                                            item.Simbolo = "";
                                        }
                                    }
                                }
                                else
                                {
                                    if (Convert.ToDecimal(item.Registro, null) >= (calificacion.PuntajeMinimo == null ? 0 : calificacion.PuntajeMinimo))
                                    {
                                        item.EsAprobado = true;
                                        item.CalificaPorCentil = false;
                                    }
                                    if (calificacion.EsCalificable == false)
                                    {
                                        item.EsAprobado = false;
                                        item.NotaAprobatoria = "N.A";
                                        item.Simbolo = "";
                                    }
                                }
                            }

                        }
                    }


                    // Cambia los puntajes de las Grupos
                    if (item.IdEvaluacion != 0 && item.IdGrupo != 0 && item.IdExamen == 0)
                    {
                        var calificacion = informacionCentilCalificacion.Where(x => x.IdExamenTest == item.IdEvaluacion && x.IdGrupoComponenteEvaluacion == item.IdGrupo && x.IdExamen == null && x.IdProcesoSeleccion == item.IdProcesoSeleccion).FirstOrDefault();

                        if (calificacion == null)
                        {
                            item.EsAprobado = false;
                            item.NotaAprobatoria = "N.A";
                        }
                        else
                        {

                            // La Calificacion debe de ser Igual a Valor minimo del componente o evaluacion
                            item.NotaAprobatoria = calificacion.PuntajeMinimo.ToString();
                            if (calificacion.IdProcesoSeleccionRango == 1)
                            {
                                item.Simbolo = "=";
                                if (calificacion.CalificaPorCentil)
                                {
                                    item.CalificaPorCentil = true;
                                    var centil = centilesCompletos.Where(x => x.IdGrupoComponenteEvaluacion == item.IdGrupo && x.IdSexo == (x.IdSexo == null ? null : item.IdSexo) && (puntaje >= x.ValorMinimo && puntaje <= x.ValorMaximo)).FirstOrDefault();

                                    if (centil == null)
                                    {
                                        item.Registro = "SIN CENTIL";
                                        item.EsAprobado = false;
                                    }
                                    else
                                    {
                                        item.Registro = centil.Centil.ToString();
                                        if (Convert.ToDecimal(item.Registro, null) == calificacion.PuntajeMinimo)
                                        {
                                            item.EsAprobado = true;
                                        }
                                        if (calificacion.EsCalificable == false)
                                        {
                                            item.EsAprobado = false;
                                            item.NotaAprobatoria = "N.A";
                                            item.Simbolo = "";
                                        }
                                    }
                                }
                                else
                                {
                                    if (Convert.ToDecimal(item.Registro, null) == calificacion.PuntajeMinimo)
                                    {
                                        item.EsAprobado = true;
                                        item.CalificaPorCentil = false;
                                    }
                                    if (calificacion.EsCalificable == false)
                                    {
                                        item.EsAprobado = false;
                                        item.NotaAprobatoria = "N.A";
                                        item.Simbolo = "";
                                    }
                                }
                            }

                            // La Calificacion debe de ser Mayor a Valor Minimo
                            if (calificacion.IdProcesoSeleccionRango == 2)
                            {
                                item.Simbolo = ">";
                                if (calificacion.CalificaPorCentil)
                                {
                                    item.CalificaPorCentil = true;
                                    var centil = centilesCompletos.Where(x => x.IdGrupoComponenteEvaluacion == item.IdGrupo && x.IdSexo == (x.IdSexo == null ? null : item.IdSexo) && (puntaje >= x.ValorMinimo && puntaje <= x.ValorMaximo)).FirstOrDefault();

                                    if (centil == null)
                                    {
                                        item.Registro = "SIN CENTIL";
                                        item.EsAprobado = false;
                                    }
                                    else
                                    {
                                        item.Registro = centil.Centil.ToString();
                                        if (Convert.ToDecimal(item.Registro, null) > calificacion.PuntajeMinimo)
                                        {
                                            item.EsAprobado = true;
                                        }
                                        if (calificacion.EsCalificable == false)
                                        {
                                            item.EsAprobado = false;
                                            item.NotaAprobatoria = "N.A";
                                            item.Simbolo = "";
                                        }
                                    }
                                }
                                else
                                {
                                    if (Convert.ToDecimal(item.Registro, null) > calificacion.PuntajeMinimo)
                                    {
                                        item.EsAprobado = true;
                                        item.CalificaPorCentil = false;
                                    }
                                    if (calificacion.EsCalificable == false)
                                    {
                                        item.EsAprobado = false;
                                        item.NotaAprobatoria = "N.A";
                                        item.Simbolo = "";
                                    }
                                }
                            }

                            // La Calificacion debe de ser Menor a Valor Minimo
                            if (calificacion.IdProcesoSeleccionRango == 9)
                            {
                                item.Simbolo = "<";
                                if (calificacion.CalificaPorCentil)
                                {
                                    item.CalificaPorCentil = true;
                                    var centil = centilesCompletos.Where(x => x.IdGrupoComponenteEvaluacion == item.IdGrupo && x.IdSexo == (x.IdSexo == null ? null : item.IdSexo) && (puntaje >= x.ValorMinimo && puntaje <= x.ValorMaximo)).FirstOrDefault();

                                    if (centil == null)
                                    {
                                        item.Registro = "SIN CENTIL";
                                        item.EsAprobado = false;
                                    }
                                    else
                                    {
                                        item.Registro = centil.Centil.ToString();
                                        if (Convert.ToDecimal(item.Registro, null) < calificacion.PuntajeMinimo)
                                        {
                                            item.EsAprobado = true;
                                        }
                                        if (calificacion.EsCalificable == false)
                                        {
                                            item.EsAprobado = false;
                                            item.NotaAprobatoria = "N.A";
                                            item.Simbolo = "";
                                        }
                                    }
                                }
                                else
                                {
                                    if (Convert.ToDecimal(item.Registro, null) < calificacion.PuntajeMinimo)
                                    {
                                        item.EsAprobado = true;
                                        item.CalificaPorCentil = false;
                                    }
                                    if (calificacion.EsCalificable == false)
                                    {
                                        item.EsAprobado = false;
                                        item.NotaAprobatoria = "N.A";
                                        item.Simbolo = "";
                                    }
                                }
                            }

                            // La Calificacion debe de ser Mayor Igual a Valor Minimo
                            if (calificacion.IdProcesoSeleccionRango == 12)
                            {
                                item.Simbolo = ">=";
                                if (calificacion.CalificaPorCentil)
                                {
                                    item.CalificaPorCentil = true;
                                    var centil = centilesCompletos.Where(x => x.IdGrupoComponenteEvaluacion == item.IdGrupo && x.IdSexo == (x.IdSexo == null ? null : item.IdSexo) && (puntaje >= x.ValorMinimo && puntaje <= x.ValorMaximo)).FirstOrDefault();

                                    if (centil == null)
                                    {
                                        item.Registro = "SIN CENTIL";
                                        item.EsAprobado = false;
                                    }
                                    else
                                    {
                                        item.Registro = centil.Centil.ToString();
                                        if (Convert.ToDecimal(item.Registro, null) >= calificacion.PuntajeMinimo)
                                        {
                                            item.EsAprobado = true;
                                        }
                                        if (calificacion.EsCalificable == false)
                                        {
                                            item.EsAprobado = false;
                                            item.NotaAprobatoria = "N.A";
                                            item.Simbolo = "";
                                        }
                                    }
                                }
                                else
                                {
                                    if (Convert.ToDecimal(item.Registro, null) >= calificacion.PuntajeMinimo)
                                    {
                                        item.EsAprobado = true;
                                        item.CalificaPorCentil = false;
                                    }
                                    if (calificacion.EsCalificable == false)
                                    {
                                        item.EsAprobado = false;
                                        item.NotaAprobatoria = "N.A";
                                        item.Simbolo = "";
                                    }
                                }
                            }

                            // La Calificacion debe de ser Menor Igual a Valor Minimo
                            if (calificacion.IdProcesoSeleccionRango == 13)
                            {
                                item.Simbolo = "<=";
                                if (calificacion.CalificaPorCentil)
                                {
                                    item.CalificaPorCentil = true;
                                    var centil = centilesCompletos.Where(x => x.IdGrupoComponenteEvaluacion == item.IdGrupo && x.IdSexo == (x.IdSexo == null ? null : item.IdSexo) && (puntaje >= x.ValorMinimo && puntaje <= x.ValorMaximo)).FirstOrDefault();

                                    if (centil == null)
                                    {
                                        item.Registro = "SIN CENTIL";
                                        item.EsAprobado = false;
                                    }
                                    else
                                    {
                                        item.Registro = centil.Centil.ToString();
                                        if (Convert.ToDecimal(item.Registro, null) <= calificacion.PuntajeMinimo)
                                        {
                                            item.EsAprobado = true;
                                        }
                                        if (calificacion.EsCalificable == false)
                                        {
                                            item.EsAprobado = false;
                                            item.NotaAprobatoria = "N.A";
                                            item.Simbolo = "";
                                        }
                                    }
                                }
                                else
                                {
                                    if (Convert.ToDecimal(item.Registro, null) <= calificacion.PuntajeMinimo)
                                    {
                                        item.EsAprobado = true;
                                        item.CalificaPorCentil = false;
                                    }
                                    if (calificacion.EsCalificable == false)
                                    {
                                        item.EsAprobado = false;
                                        item.NotaAprobatoria = "N.A";
                                        item.Simbolo = "";
                                    }
                                }
                            }

                            if (calificacion.IdProcesoSeleccionRango == null || calificacion.IdProcesoSeleccionRango == 0)
                            {
                                if (calificacion.CalificaPorCentil)
                                {
                                    item.CalificaPorCentil = true;

                                    var centil = centilesCompletos.Where(x => x.IdGrupoComponenteEvaluacion == item.IdGrupo && x.IdSexo == (x.IdSexo == null ? null : item.IdSexo) && (puntaje >= x.ValorMinimo && puntaje <= x.ValorMaximo)).FirstOrDefault();

                                    if (centil == null)
                                    {
                                        item.Registro = "SIN CENTIL";
                                        item.EsAprobado = false;
                                    }
                                    else
                                    {
                                        item.Registro = centil.Centil.ToString();
                                        if (Convert.ToDecimal(item.Registro, null) >= (calificacion.PuntajeMinimo == null ? 0 : calificacion.PuntajeMinimo))
                                        {
                                            item.EsAprobado = true;
                                        }
                                        if (calificacion.EsCalificable == false)
                                        {
                                            item.EsAprobado = false;
                                            item.NotaAprobatoria = "N.A";
                                            item.Simbolo = "";
                                        }
                                    }
                                }
                                else
                                {
                                    if (Convert.ToDecimal(item.Registro, null) >= (calificacion.PuntajeMinimo == null ? 0 : calificacion.PuntajeMinimo))
                                    {
                                        item.EsAprobado = true;
                                        item.CalificaPorCentil = false;
                                    }
                                    if (calificacion.EsCalificable == false)
                                    {
                                        item.EsAprobado = false;
                                        item.NotaAprobatoria = "N.A";
                                        item.Simbolo = "";
                                    }
                                }
                            }
                        }
                    }


                    // Cambia los puntajes de los componentes
                    if (item.IdEvaluacion != 0 && item.IdGrupo == 0 && item.IdExamen != 0)
                    {
                        //var calificacionOriginal = repProcesoSeleccionPuntajeCalificacionRep.FirstBy(x => x.Estado == true && x.IdExamenTest == item.IdEvaluacion && x.IdGrupoComponenteEvaluacion == null && x.IdExamen == item.IdExamen && x.IdProcesoSeleccion == item.IdProcesoSeleccion);
                        var calificacion = informacionCentilCalificacion.Where(x => x.IdExamenTest == item.IdEvaluacion && x.IdGrupoComponenteEvaluacion == null && x.IdExamen == item.IdExamen && x.IdProcesoSeleccion == item.IdProcesoSeleccion).FirstOrDefault();

                        if (calificacion == null)
                        {
                            item.EsAprobado = false;
                            item.NotaAprobatoria = "N.A";
                        }
                        else
                        {
                            // La Calificacion debe de ser Igual a Valor minimo del componente o evaluacion
                            item.NotaAprobatoria = calificacion.PuntajeMinimo.ToString();
                            if (calificacion.IdProcesoSeleccionRango == 1)
                            {
                                item.Simbolo = "=";
                                if (calificacion.CalificaPorCentil)
                                {
                                    item.CalificaPorCentil = true;
                                    var centil = centilesCompletos.Where(x => x.IdExamen == item.IdExamen && x.IdSexo == (x.IdSexo == null ? null : item.IdSexo) && (puntaje >= x.ValorMinimo && puntaje <= x.ValorMaximo)).FirstOrDefault();

                                    if (centil == null)
                                    {
                                        item.Registro = "SIN CENTIL";
                                        item.EsAprobado = false;
                                    }
                                    else
                                    {
                                        item.Registro = centil.Centil.ToString();
                                        if (Convert.ToDecimal(item.Registro, null) == calificacion.PuntajeMinimo)
                                        {
                                            item.EsAprobado = true;
                                        }
                                        if (calificacion.EsCalificable == false)
                                        {
                                            item.EsAprobado = false;
                                            item.NotaAprobatoria = "N.A";
                                            item.Simbolo = "";
                                        }
                                    }
                                }
                                else
                                {
                                    if (Convert.ToDecimal(item.Registro, null) == calificacion.PuntajeMinimo)
                                    {
                                        item.EsAprobado = true;
                                        item.CalificaPorCentil = false;
                                    }
                                    if (calificacion.EsCalificable == false)
                                    {
                                        item.EsAprobado = false;
                                        item.NotaAprobatoria = "N.A";
                                        item.Simbolo = "";
                                    }
                                }
                            }

                            // La Calificacion debe de ser Mayor a Valor Minimo
                            if (calificacion.IdProcesoSeleccionRango == 2)
                            {
                                item.Simbolo = ">";
                                if (calificacion.CalificaPorCentil)
                                {
                                    item.CalificaPorCentil = true;
                                    var centil = centilesCompletos.Where(x => x.IdExamen == item.IdExamen && x.IdSexo == (x.IdSexo == null ? null : item.IdSexo) && (puntaje >= x.ValorMinimo && puntaje <= x.ValorMaximo)).FirstOrDefault();


                                    if (centil == null)
                                    {
                                        item.Registro = "SIN CENTIL";
                                        item.EsAprobado = false;
                                    }
                                    else
                                    {
                                        item.Registro = centil.Centil.ToString();
                                        if (Convert.ToDecimal(item.Registro, null) > calificacion.PuntajeMinimo)
                                        {
                                            item.EsAprobado = true;
                                        }
                                        if (calificacion.EsCalificable == false)
                                        {
                                            item.EsAprobado = false;
                                            item.NotaAprobatoria = "N.A";
                                            item.Simbolo = "";
                                        }
                                    }
                                }
                                else
                                {
                                    if (Convert.ToDecimal(item.Registro, null) > calificacion.PuntajeMinimo)
                                    {
                                        item.EsAprobado = true;
                                        item.CalificaPorCentil = false;
                                    }
                                    if (calificacion.EsCalificable == false)
                                    {
                                        item.EsAprobado = false;
                                        item.NotaAprobatoria = "N.A";
                                        item.Simbolo = "";
                                    }
                                }
                            }

                            // La Calificacion debe de ser Menor a Valor Minimo
                            if (calificacion.IdProcesoSeleccionRango == 9)
                            {
                                item.Simbolo = "<";
                                if (calificacion.CalificaPorCentil)
                                {
                                    item.CalificaPorCentil = true;
                                    var centil = centilesCompletos.Where(x => x.IdExamen == item.IdExamen && x.IdSexo == (x.IdSexo == null ? null : item.IdSexo) && (puntaje >= x.ValorMinimo && puntaje <= x.ValorMaximo)).FirstOrDefault();


                                    if (centil == null)
                                    {
                                        item.Registro = "SIN CENTIL";
                                        item.EsAprobado = false;
                                    }
                                    else
                                    {
                                        item.Registro = centil.Centil.ToString();
                                        if (Convert.ToDecimal(item.Registro, null) < calificacion.PuntajeMinimo)
                                        {
                                            item.EsAprobado = true;
                                        }
                                        if (calificacion.EsCalificable == false)
                                        {
                                            item.EsAprobado = false;
                                            item.NotaAprobatoria = "N.A";
                                            item.Simbolo = "";
                                        }
                                    }
                                }
                                else
                                {
                                    if (Convert.ToDecimal(item.Registro, null) < calificacion.PuntajeMinimo)
                                    {
                                        item.EsAprobado = true;
                                        item.CalificaPorCentil = false;
                                    }
                                    if (calificacion.EsCalificable == false)
                                    {
                                        item.EsAprobado = false;
                                        item.NotaAprobatoria = "N.A";
                                        item.Simbolo = "";
                                    }
                                }
                            }

                            // La Calificacion debe de ser Mayor Igual a Valor Minimo
                            if (calificacion.IdProcesoSeleccionRango == 12)
                            {
                                item.Simbolo = ">=";
                                if (calificacion.CalificaPorCentil)
                                {
                                    item.CalificaPorCentil = true;
                                    var centil = centilesCompletos.Where(x => x.IdExamen == item.IdExamen && x.IdSexo == (x.IdSexo == null ? null : item.IdSexo) && (puntaje >= x.ValorMinimo && puntaje <= x.ValorMaximo)).FirstOrDefault();

                                    if (centil == null)
                                    {
                                        item.Registro = "SIN CENTIL";
                                        item.EsAprobado = false;
                                    }
                                    else
                                    {
                                        item.Registro = centil.Centil.ToString();
                                        if (Convert.ToDecimal(item.Registro, null) >= calificacion.PuntajeMinimo)
                                        {
                                            item.EsAprobado = true;
                                        }
                                        if (calificacion.EsCalificable == false)
                                        {
                                            item.EsAprobado = false;
                                            item.NotaAprobatoria = "N.A";
                                            item.Simbolo = "";
                                        }
                                    }
                                }
                                else
                                {
                                    if (Convert.ToDecimal(item.Registro, null) >= calificacion.PuntajeMinimo)
                                    {
                                        item.EsAprobado = true;
                                        item.CalificaPorCentil = false;
                                    }
                                    if (calificacion.EsCalificable == false)
                                    {
                                        item.EsAprobado = false;
                                        item.NotaAprobatoria = "N.A";
                                        item.Simbolo = "";
                                    }
                                }
                            }

                            // La Calificacion debe de ser Menor Igual a Valor Minimo
                            if (calificacion.IdProcesoSeleccionRango == 13)
                            {
                                item.Simbolo = "<=";
                                if (calificacion.CalificaPorCentil)
                                {
                                    item.CalificaPorCentil = true;
                                    var centil = centilesCompletos.Where(x => x.IdExamen == item.IdExamen && x.IdSexo == (x.IdSexo == null ? null : item.IdSexo) && (puntaje >= x.ValorMinimo && puntaje <= x.ValorMaximo)).FirstOrDefault();


                                    if (centil == null)
                                    {
                                        item.Registro = "SIN CENTIL";
                                        item.EsAprobado = false;
                                    }
                                    else
                                    {
                                        item.Registro = centil.Centil.ToString();
                                        if (Convert.ToDecimal(item.Registro, null) <= calificacion.PuntajeMinimo)
                                        {
                                            item.EsAprobado = true;
                                        }
                                        if (calificacion.EsCalificable == false)
                                        {
                                            item.EsAprobado = false;
                                            item.NotaAprobatoria = "N.A";
                                            item.Simbolo = "";
                                        }
                                    }
                                }
                                else
                                {
                                    if (Convert.ToDecimal(item.Registro, null) <= calificacion.PuntajeMinimo)
                                    {
                                        item.EsAprobado = true;
                                        item.CalificaPorCentil = false;
                                    }
                                    if (calificacion.EsCalificable == false)
                                    {
                                        item.EsAprobado = false;
                                        item.NotaAprobatoria = "N.A";
                                        item.Simbolo = "";
                                    }
                                }
                            }

                            if (calificacion.IdProcesoSeleccionRango == null || calificacion.IdProcesoSeleccionRango == 0)
                            {
                                if (calificacion.CalificaPorCentil)
                                {
                                    item.CalificaPorCentil = true;
                                    var centil = centilesCompletos.Where(x => x.IdExamen == item.IdExamen && x.IdSexo == (x.IdSexo == null ? null : item.IdSexo) && (puntaje >= x.ValorMinimo && puntaje <= x.ValorMaximo)).FirstOrDefault();


                                    if (centil == null)
                                    {
                                        item.Registro = "SIN CENTIL";
                                        item.EsAprobado = false;
                                    }
                                    else
                                    {
                                        item.Registro = centil.Centil.ToString();
                                        if (Convert.ToDecimal(item.Registro, null) >= (calificacion.PuntajeMinimo == null ? 0 : calificacion.PuntajeMinimo))
                                        {
                                            item.EsAprobado = true;
                                        }
                                        if (calificacion.EsCalificable == false)
                                        {
                                            item.EsAprobado = false;
                                            item.NotaAprobatoria = "N.A";
                                            item.Simbolo = "";
                                        }
                                    }
                                }
                                else
                                {
                                    if (Convert.ToDecimal(item.Registro, null) >= (calificacion.PuntajeMinimo == null ? 0 : calificacion.PuntajeMinimo))
                                    {
                                        item.EsAprobado = true;
                                        item.CalificaPorCentil = false;
                                    }
                                    if (calificacion.EsCalificable == false)
                                    {
                                        item.EsAprobado = false;
                                        item.NotaAprobatoria = "N.A";
                                        item.Simbolo = "";
                                    }
                                }
                            }

                        }
                    }

                    if (!item.Registro.Equals("SIN CENTIL"))
                    {
                        item.Registro = FuncionRedondeo(Convert.ToDecimal(item.Registro, null), RedondeoGeneral).ToString();
                        if (item.Evaluacion != null && (item.IdEvaluacion == 53 || item.Evaluacion.Equals("GRADUATE MANAGEMENT ADMISSION TEST V2")))
                        {
                            item.Registro = item.Registro + "%";
                        }
                    }

                    item.OrdenReal = item.Orden;
                    // Esta parte se agrego para poder ordenar la evaluacion de Aptitudes a como el señor juan carlos lo requeria. el codigo de orden real esta compuesto por IdEvaluacion IdGrupo IdComponente, lo que forma el codigo de OrdenReal
                    switch (item.OrdenReal)
                    {
                        case 53052:
                            item.OrdenReal = 53051;
                            break;
                        case 53053:
                            item.OrdenReal = 53052;
                            break;
                        case 53051:
                            item.OrdenReal = 53053;
                            break;
                        case 53054:
                            item.OrdenReal = 53054;
                            break;
                    }
                }

                List<ProcesoSelecionExamenesCompletosDTO> listaNotasProcesoSeleccionComplemento = new List<ProcesoSelecionExamenesCompletosDTO>();

                // Esta parte es para que siempre muestre las calificaciones de los componentes asi sea por CalificacionTotal o CalificacionAgrupada.

                //Obtiene solo las evaluaciones con calificacion Total
                var Evaluaciones2 = listaNotasProcesoSeleccion.Where(x => x.IdExamen == 0 && x.IdGrupo == 0).Select(x => new ProcesoSelecionExamenesCompletosComplementoDTO { IdProcesoSeleccion = x.IdProcesoSeleccion, IdEtapa = x.IdEtapa, IdEvaluacion = x.IdEvaluacion, IdGrupo = x.IdGrupo, IdExamen = x.IdExamen, EsAprobado = x.EsAprobado, CalificaPorCentil = x.CalificaPorCentil, Orden = x.Orden, OrdenReal = x.OrdenReal }).ToList();
                var Evaluaciones = Evaluaciones2
                    .GroupBy(x => new { IdProcesoSeleccion = x.IdProcesoSeleccion, IdEtapa = x.IdEtapa, IdEvaluacion = x.IdEvaluacion, IdGrupo = x.IdGrupo, IdExamen = x.IdExamen, EsAprobado = x.EsAprobado, CalificaPorCentil = x.CalificaPorCentil, Orden = x.Orden, OrdenReal = x.OrdenReal })
                    .Select(group =>
                    new ProcesoSelecionExamenesCompletosComplementoDTO
                    {
                        IdProcesoSeleccion = group.Key.IdProcesoSeleccion
                        ,
                        IdEtapa = group.Key.IdEtapa
                        ,
                        IdEvaluacion = group.Key.IdEvaluacion
                        ,
                        IdGrupo = group.Key.IdGrupo
                        ,
                        IdExamen = group.Key.IdExamen
                        ,
                        EsAprobado = group.Key.EsAprobado
                        ,
                        CalificaPorCentil = group.Key.CalificaPorCentil
                        ,
                        Orden = group.Key.Orden
                        ,
                        OrdenReal = group.Key.OrdenReal
                    })
                    .ToList();
                // Obtiene solo las evaluaciones con calificacion agrupada
                var GrupoComponente2 = listaNotasProcesoSeleccion.Where(x => x.IdExamen == 0 && x.IdGrupo != 0).Select(x => new ProcesoSelecionExamenesCompletosComplementoDTO { IdProcesoSeleccion = x.IdProcesoSeleccion, IdEtapa = x.IdEtapa, IdEvaluacion = x.IdEvaluacion, IdGrupo = x.IdGrupo, IdExamen = x.IdExamen, EsAprobado = x.EsAprobado, CalificaPorCentil = x.CalificaPorCentil, Orden = x.Orden, OrdenReal = x.OrdenReal }).Distinct().ToList();
                var GrupoComponente = GrupoComponente2
                    .GroupBy(x => new { IdProcesoSeleccion = x.IdProcesoSeleccion, IdEtapa = x.IdEtapa, IdEvaluacion = x.IdEvaluacion, IdGrupo = x.IdGrupo, IdExamen = x.IdExamen, EsAprobado = x.EsAprobado, CalificaPorCentil = x.CalificaPorCentil, Orden = x.Orden, OrdenReal = x.OrdenReal })
                    .Select(group =>
                    new ProcesoSelecionExamenesCompletosComplementoDTO
                    {
                        IdProcesoSeleccion = group.Key.IdProcesoSeleccion
                        ,
                        IdEtapa = group.Key.IdEtapa
                        ,
                        IdEvaluacion = group.Key.IdEvaluacion
                        ,
                        IdGrupo = group.Key.IdGrupo
                        ,
                        IdExamen = group.Key.IdExamen
                        ,
                        EsAprobado = group.Key.EsAprobado
                        ,
                        CalificaPorCentil = group.Key.CalificaPorCentil
                        ,
                        Orden = group.Key.Orden
                        ,
                        OrdenReal = group.Key.OrdenReal
                    })
                    .ToList();
                foreach (var item in Evaluaciones)
                {
                    count = 0.00M;
                    var EvaluacionesGrupo = reporte.Where(x => x.IdGrupo == null && x.IdEvaluacion == item.IdEvaluacion).ToList();
                    foreach (var componente in EvaluacionesGrupo)
                    {
                        if (componente.Titulo.Equals("N1: Ansiedad"))
                        {
                            var c = 323;
                        }
                        var existe = listaNotasProcesoSeleccionComplemento.Where(x => x.IdEvaluacion == componente.IdEvaluacion && x.IdExamen == componente.IdExamen /*&&x.IdProcesoSeleccion==componente.IdProceso && x.IdEtapa==componente.IdEtapa*/).FirstOrDefault();
                        var eval = new ProcesoSelecionExamenesCompletosDTO
                        {
                            IdProcesoSeleccion = componente.IdProceso,
                            ProcesoSeleccion = componente.NombreProceso,
                            IdPostulante = componente.IdPostulante,
                            Postulante = componente.Postulante,
                            Edad = 24,
                            Examen = componente.NombreExamen,
                            IdCategoria = componente.IdCategoria,
                            Categoria = componente.NombreCategoria,
                            IdExamen = componente.IdExamen,
                            IdGrupo = componente.IdGrupo == null ? 0 : componente.IdGrupo,
                            IdEvaluacion = componente.IdEvaluacion,
                            Evaluacion = componente.NombreEvaluacion,
                            Grupo = componente.NombreGrupo,
                            Etapa = componente.NombreEtapa,
                            IdEtapa = componente.IdEtapa,
                            Orden = item.Orden,
                            Registro = componente.Puntaje.ToString(),
                            EsAprobado = item.EsAprobado,
                            CalificaPorCentil = item.CalificaPorCentil,
                            IdSexo = componente.IdSexo,
                            NotaAprobatoria = "N.A",
                            Simbolo = "",
                        };
                        if (existe != null)
                        {
                            eval.OrdenReal = existe.OrdenReal;
                        }
                        else
                        {
                            count = count + 0.01M; // esta parte interviene en el orden se le pone como decimal para que los componentes siempre esten dentro de la evaluacion o Grupo de Componentes.
                            eval.OrdenReal = item.OrdenReal + count;
                        }
                        listaNotasProcesoSeleccionComplemento.Add(eval);
                    }

                }
                foreach (var item in GrupoComponente)
                {
                    count = 0.00M;
                    var grupos = reporte.Where(x => x.IdGrupo == item.IdGrupo && x.IdEvaluacion == item.IdEvaluacion).ToList();
                    foreach (var componente in grupos)
                    {
                        if (componente.Titulo.Equals("N1: Ansiedad"))
                        {
                            var c = 323;
                        }
                        var existe = listaNotasProcesoSeleccionComplemento.Where(x => x.IdEvaluacion == componente.IdEvaluacion && x.IdExamen == componente.IdExamen /*&& x.IdProcesoSeleccion == componente.IdProceso && x.IdEtapa == componente.IdEtapa*/).FirstOrDefault();
                        var eval = new ProcesoSelecionExamenesCompletosDTO
                        {
                            IdProcesoSeleccion = componente.IdProceso,
                            ProcesoSeleccion = componente.NombreProceso,
                            IdPostulante = componente.IdPostulante,
                            Postulante = componente.Postulante,
                            Edad = 24,
                            Examen = componente.NombreExamen,
                            IdCategoria = componente.IdCategoria,
                            Categoria = componente.NombreCategoria,
                            IdExamen = componente.IdExamen,
                            IdGrupo = componente.IdGrupo == null ? 0 : componente.IdGrupo,
                            IdEvaluacion = componente.IdEvaluacion,
                            Evaluacion = componente.NombreEvaluacion,
                            Grupo = componente.NombreGrupo,
                            Etapa = componente.NombreEtapa,
                            IdEtapa = componente.IdEtapa,
                            Orden = item.Orden,
                            Registro = componente.Puntaje.ToString(),
                            EsAprobado = item.EsAprobado,
                            CalificaPorCentil = item.CalificaPorCentil,
                            IdSexo = componente.IdSexo,
                            NotaAprobatoria = "N.A",
                            Simbolo = ""
                        };
                        if (existe != null)
                        {
                            eval.OrdenReal = existe.OrdenReal;
                        }
                        else
                        {

                            count = count + 0.01M;
                            eval.OrdenReal = item.OrdenReal + count;
                        }
                        listaNotasProcesoSeleccionComplemento.Add(eval);
                    }

                }

                // Aqui a las calificaciones de componentes de las Evaluaciones y Grupo de Componentes se le calcula su puntaje por centil y no se evalua si esta aprobado
                //o no ya que en la configuracion el que deberia de tener la calificacion es la Evaluacion o el Grupo de Componentes que faltaban obtener 
                foreach (var item in listaNotasProcesoSeleccionComplemento)
                {

                    var puntaje = Convert.ToDecimal(item.Registro, null);

                    if (item.CalificaPorCentil)
                    {
                        item.CalificaPorCentil = true;
                        var centil = centilesCompletos.Where(x => x.IdExamen == item.IdExamen && x.IdSexo == (x.IdSexo == null ? null : item.IdSexo) && (puntaje >= x.ValorMinimo && puntaje <= x.ValorMaximo)).FirstOrDefault();

                        if (centil == null)
                        {
                            item.Registro = "SIN CENTIL";
                            item.EsAprobado = false;
                        }
                        else
                        {
                            item.Registro = centil.Centil.ToString();
                        }
                    }
                    if (!item.Registro.Equals("SIN CENTIL") && item.IdEvaluacion != 53)
                    {
                        var puntaje2 = Convert.ToDecimal(item.Registro, null);
                        item.Registro = FuncionRedondeo(puntaje2, RedondeoGeneral).ToString();
                    }
                }

                //finalamente se concatenan las dos listas para obtener el resultado final
                AgrupacionDetalleEvaluacionDTO retorna = new AgrupacionDetalleEvaluacionDTO();
                retorna.Agrupado = listaNotasProcesoSeleccion.OrderBy(x => x.OrdenReal.Value).ToList(); ;
                retorna.Detalle = listaNotasProcesoSeleccionComplemento.OrderBy(x => x.OrdenReal.Value).ToList(); ;
                return retorna;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
