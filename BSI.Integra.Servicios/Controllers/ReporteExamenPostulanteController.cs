using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.GestionPersonas.BO;
using BSI.Integra.Aplicacion.GestionPersonas.Repositorio;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: ReporteExamenPostulanteController
    /// Autor: Britsel C., Luis H., Edgar S.
    /// Fecha: 29/01/2021
    /// <summary>
    /// Gestión de Actualización de Estados y Etapas de Postulantes por Proceso de Selección
    /// </summary>
    [Route("api/ReporteExamenPostulante")]
    [ApiController]
    public class ReporteExamenPostulanteController : Controller
    {
        private readonly integraDBContext _integraDBContext;
        public ReporteExamenPostulanteController()
        {
            _integraDBContext = new integraDBContext();
        }

        /// TipoFuncion: POST
		/// Autor: Luis H, Edgar S.
		/// Fecha: 25/01/2021
		/// Versión: 1.0
		/// <summary>
		/// Genera reporte de etapas y calificación de postulantes por Filtro
		/// </summary>
		/// <returns>Lista de Etapas y calificaciones de postulantes</returns>
        /// <returns>Lista de Etapas y calificaciones de postulantes</returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult GenerarReporte([FromBody] ReporteExamenPostulanteDTO Filtro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                PostulanteComparacionRepositorio repPostulanteComparacionRep = new PostulanteComparacionRepositorio();
                EtapaProcesoSeleccionCalificadoRepositorio repEtapaCalificacionRep = new EtapaProcesoSeleccionCalificadoRepositorio();

                List<int> listaPostulanteComparacion = new List<int>();
                if (Filtro.IdGrupoComparacion != null && Filtro.IdGrupoComparacion != 0)
                {
                    listaPostulanteComparacion = repPostulanteComparacionRep.GetBy(x => x.Estado == true && x.IdGrupoComparacionProcesoSeleccion == Filtro.IdGrupoComparacion).Select(x=>x.IdPostulante.Value).ToList();
                    foreach (var item in listaPostulanteComparacion)
                    {
                        Filtro.Postulante = String.Concat(Filtro.Postulante, ",", item);
                    }
                }
                ReporteExamenProcesoSeleccionBO reporteExamenProcesoSeleccion = new ReporteExamenProcesoSeleccionBO(_integraDBContext);

                if (Filtro.IdSede == "_")
                {
                    Filtro.IdSede = "1,2";
                }
                if (Filtro.IdPuesto == "0") {
                    Filtro.IdPuesto = null;
                }


                var reporte = reporteExamenProcesoSeleccion.ObtenerReporteExamenes(Filtro);

                var datosAgrupado = (from p in reporte orderby p.OrdenReal
                                     group p by new { p.OrdenReal } into grupo
                                     select new { g = grupo.Key, l = grupo }).ToList();

                var postulantes = (from p in reporte
                                   group p by new { p.IdPostulante, p.Postulante, p.Edad } into grupo
                                   select new { IdPostulante = grupo.Key.IdPostulante, Postulante = grupo.Key.Postulante, Edad = grupo.Key.Edad }).ToList();

                if (listaPostulanteComparacion.Count > 0) {
                    reporte = reporte.Where(x => !listaPostulanteComparacion.Contains(x.IdPostulante)).ToList();
                }
                //agrupa de cada postulante sus evaluaciones, contando si es aprobado o no
                var listaEtapas = reporte.GroupBy(u => new { u.IdPostulante, u.Postulante, u.IdProcesoSeleccion, u.ProcesoSeleccion, u.IdEvaluacion }).
                                    Select(group =>
                                    new DatosEvaluacionAprobadaDTO
                                    {
                                        IdProcesoSeleccion= group.Key.IdProcesoSeleccion
                                        ,ProcesoSeleccion=group.Key.ProcesoSeleccion
                                        ,IdPostulante =group.Key.IdPostulante
                                        ,Postulante = group.Key.Postulante
                                        ,IdEtapa=group.Select(x=>x.IdEtapa).FirstOrDefault()
                                        ,Etapa=group.Select(x => x.Etapa).FirstOrDefault()
                                        ,IdEvaluacion = group.Key.IdEvaluacion
                                        ,ContadorAprobado= group.Where(x => x.EsAprobado == true).Count()
                                        ,ContadorTotal= group.Select(x => x.EsAprobado).Count()
                                        ,dividido= (Convert.ToDecimal(group.Select(x => x.EsAprobado).Count(), null) / 2)
                                        ,EsAprobado = Convert.ToDecimal(group.Where(x => x.EsAprobado==true).Count(),null)>= (Convert.ToDecimal(group.Select(x => x.EsAprobado).Count(),null)/2)
                                        ,IdEstadoEtapa= Convert.ToDecimal(group.Where(x => x.EsAprobado == true).Count(), null) >= (Convert.ToDecimal(group.Select(x => x.EsAprobado).Count(), null) / 2) == true ? 1:2
                                    }
                                    ).ToList();

                // De todas las Evaluaciones anteriores, calcula si la etapa ha sido aprobada.
                var etapasPostulante= listaEtapas.GroupBy(u => new { u.IdPostulante, u.Postulante, u.IdProcesoSeleccion, u.ProcesoSeleccion, u.IdEtapa, u.Etapa }).
                                         Select(group =>
                                    new DatosEvaluacionAprobadaDTO
                                    {
                                        IdProcesoSeleccion= group.Key.IdProcesoSeleccion
                                        ,ProcesoSeleccion=group.Key.ProcesoSeleccion
                                        ,IdPostulante =group.Key.IdPostulante
                                        ,Postulante = group.Key.Postulante
                                        ,IdEtapa=group.Key.IdEtapa
                                        ,Etapa=group.Key.Etapa
                                        ,IdEvaluacion = 0
                                        ,ContadorAprobado= group.Where(x => x.EsAprobado == true).Count()
                                        ,ContadorTotal= group.Select(x => x.EsAprobado).Count()
                                        ,dividido= (Convert.ToDecimal(group.Select(x => x.EsAprobado).Count(), null) / 2)
                                        ,EsAprobado = Convert.ToDecimal(group.Where(x => x.EsAprobado==true).Count(),null)>= (Convert.ToDecimal(group.Select(x => x.EsAprobado).Count(),null)/2)
                                        ,IdEstadoEtapa = Convert.ToDecimal(group.Where(x => x.EsAprobado == true).Count(), null) >= (Convert.ToDecimal(group.Select(x => x.EsAprobado).Count(), null) / 2)==true?1:2
                                    }
                                    ).ToList();

                // Se cambia los estados de las Etapas Aprobadas o desaprobadas que ya existen en la BD
                foreach (var item in etapasPostulante) {
                    if (item.IdEtapa != null && item.IdPostulante != null) {
                        var etapaCalificada = repEtapaCalificacionRep.FirstBy(x => x.IdPostulante == item.IdPostulante && x.IdProcesoSeleccionEtapa == item.IdEtapa);
                        if (etapaCalificada != null) {
                            item.EsAprobado = etapaCalificada.EsEtapaAprobada;
                            item.IdEstadoEtapa = etapaCalificada.IdEstadoEtapaProcesoSeleccion.Value;
                        }
                    }
                }

                var Etapas= etapasPostulante.GroupBy(u => new { u.IdPostulante, u.Postulante }).
                                            Select(group=>
                                            new ReportePruebaDTO {
                                                IdPostulante=group.Key.IdPostulante
                                                ,Postulante=group.Key.Postulante
                                                ,Etapas=group.Select(x=>new ReportePruebaDetalleDTO {
                                                    IdProcesoSeleccion=x.IdProcesoSeleccion
                                                    ,ProcesoSeleccion=x.ProcesoSeleccion
                                                    ,IdEtapa=x.IdEtapa
                                                    ,Etapa=x.Etapa
                                                    ,EstadoEtapa=x.EsAprobado==true?1:0
                                                    ,IdEstadoEtapaProceso = x.IdEstadoEtapa
                                                }).ToList()
                                            }
                                            ).ToList();

                return Ok(new { DatosAgrupado = datosAgrupado, Postulantes = postulantes, Estado = true,datosEtapaAprobada= Etapas });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// TipoFuncion: POST
		/// Autor: Luis H, Edgar S.
		/// Fecha: 25/01/2021
		/// Versión: 1.0
		/// <summary>
		/// ??
		/// </summary>
		/// <returns>??</returns>
        [Route("[action]")]
		[HttpPost]
		public ActionResult ReporteEtapas()
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				List<ReportePruebaDTO> agrupado = new List<ReportePruebaDTO>();
				List<ReportePruebaDetalleDTO> detalle1 = new List<ReportePruebaDetalleDTO>();
				List<ReportePruebaDetalleDTO> detalle2 = new List<ReportePruebaDetalleDTO>();
				List<ReportePruebaDetalleDTO> detalle3 = new List<ReportePruebaDetalleDTO>();
				ReportePruebaDetalleDTO det1 = new ReportePruebaDetalleDTO()
				{
					IdProcesoSeleccion = 1,
					ProcesoSeleccion = "Practicante de sistemas",
					IdEtapa = 1,
					Etapa = "Etapa 1",
					EstadoEtapa = 1
				};
				ReportePruebaDetalleDTO det2 = new ReportePruebaDetalleDTO()
				{
					IdProcesoSeleccion = 1,
					ProcesoSeleccion = "Practicante de sistemas",
					IdEtapa = 2,
					Etapa = "Etapa 2",
					EstadoEtapa = 1
				};
				ReportePruebaDetalleDTO det3 = new ReportePruebaDetalleDTO()
				{
					IdProcesoSeleccion = 1,
					ProcesoSeleccion = "Practicante de sistemas",
					IdEtapa = 3,
					Etapa = "Etapa 3",
					EstadoEtapa = 0
				};

				ReportePruebaDetalleDTO det4 = new ReportePruebaDetalleDTO()
				{
					IdProcesoSeleccion = 2,
					ProcesoSeleccion = "Practicante de seguridad y salud en el trabajo",
					IdEtapa = 4,
					Etapa = "Etapa 1",
					EstadoEtapa = 1
				};
				ReportePruebaDetalleDTO det5 = new ReportePruebaDetalleDTO()
				{
					IdProcesoSeleccion = 2,
					ProcesoSeleccion = "Practicante de seguridad y salud en el trabajo",
					IdEtapa = 5,
					Etapa = "Etapa 2",
					EstadoEtapa = 0
				};
				ReportePruebaDetalleDTO det6 = new ReportePruebaDetalleDTO()
				{
					IdProcesoSeleccion = 2,
					ProcesoSeleccion = "Practicante de seguridad y salud en el trabajo",
					IdEtapa = 6,
					Etapa = "Etapa 3",
					EstadoEtapa = 1
				};

				ReportePruebaDetalleDTO det7 = new ReportePruebaDetalleDTO()
				{
					IdProcesoSeleccion = 1,
					ProcesoSeleccion = "Practicante de sistemas",
					IdEtapa = 1,
					Etapa = "Etapa 1",
					EstadoEtapa = 1
				};
				ReportePruebaDetalleDTO det8 = new ReportePruebaDetalleDTO()
				{
					IdProcesoSeleccion = 1,
					ProcesoSeleccion = "Practicante de sistemas",
					IdEtapa = 2,
					Etapa = "Etapa 2",
					EstadoEtapa = 1
				};
				ReportePruebaDetalleDTO det9 = new ReportePruebaDetalleDTO()
				{
					IdProcesoSeleccion = 1,
					ProcesoSeleccion = "Practicante de sistemas",
					IdEtapa = 3,
					Etapa = "Etapa 3",
					EstadoEtapa = 1
				};


				detalle1.Add(det1);
				detalle1.Add(det2);
				detalle1.Add(det3);

				detalle2.Add(det4);
				detalle2.Add(det5);
				detalle2.Add(det6);

				detalle3.Add(det7);
				detalle3.Add(det8);
				detalle3.Add(det9);


				ReportePruebaDTO obj1 = new ReportePruebaDTO()
				{
					IdPostulante = 1,
					Postulante = "Luis David Huallpa Tapia",
					Etapas = detalle1
				};
				ReportePruebaDTO obj2 = new ReportePruebaDTO()
				{
					IdPostulante = 2,
					Postulante = "Mishell Salvatierra Ccaihuari",
					Etapas = detalle2
				};
				ReportePruebaDTO obj3 = new ReportePruebaDTO()
				{
					IdPostulante = 3,
					Postulante = "Rafaela Luciana Huallpa Salvatierra",
					Etapas = detalle3
				};



				agrupado.Add(obj1);
				agrupado.Add(obj2);
				agrupado.Add(obj3);

				//var columnas = agrupado.GroupBy(x => new { x.IdAlumno, x.Alumno })
				return Ok(agrupado);

			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

        /// TipoFuncion: POST
		/// Autor: Luis H, Edgar S.
		/// Fecha: 25/01/2021
		/// Versión: 1.0
		/// <summary>
		/// Inserta o actualiza los estados de cada etapa de un proceso de selección
		/// </summary>
		/// <returns>Confirmación de actualización o inserción</returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult InsetarActualizarEstadoEtapaAprobado([FromBody] CalificacionEtapaConsolidadoDTO Filtro) {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                EtapaProcesoSeleccionCalificadoRepositorio repEtapaCalificacionRep = new EtapaProcesoSeleccionCalificadoRepositorio();
                var listaPostulante = Filtro.listaEtapaCalificada.GroupBy(u => (u.IdPostulante))
                                    .Select(group => 
                                    new CalificacionEtapaConsolidadoDTO
                                    {
                                        IdPostulante = group.Key
                                    , listaEtapaCalificada = group.Select(x=> 
                                    new CalificacionEtapaDTO
                                    { IdPostulante = x.IdPostulante
                                    , IdProcesoSeleccion = x.IdProcesoSeleccion
                                    , IdEtapa = x.IdEtapa
                                    , EsEtapaAprobada = x.EsEtapaAprobada
                                    , IdEstadoEtapaProcesoSeleccion = x.IdEstadoEtapaProcesoSeleccion
                                    , EtapaContactado = x.EtapaContactado
                                    , EsEtapaActual = x.EsEtapaActual
                                    , OrdenEtapa = x.OrdenEtapa}).ToList() }).ToList();
                using (TransactionScope scope = new TransactionScope())
                {

                    foreach (var item in listaPostulante)
                    {
                        item.listaEtapaCalificada = item.listaEtapaCalificada.Where(x => x.IdEtapa != null && x.IdPostulante != null && (x.IdEstadoEtapaProcesoSeleccion != null && x.IdEstadoEtapaProcesoSeleccion != 0)).OrderBy(x => x.OrdenEtapa).ToList();
                        for (int i = 0; i < item.listaEtapaCalificada.Count; i++)
                        {
                            item.listaEtapaCalificada[i].EsEtapaActual = false;
                            if (i != item.listaEtapaCalificada.Count - 1 && item.listaEtapaCalificada[i].IdEstadoEtapaProcesoSeleccion != 9 && item.listaEtapaCalificada[i + 1].IdEstadoEtapaProcesoSeleccion == 9)
                            {
                                item.listaEtapaCalificada[i].EsEtapaActual = true;
                            }
                            if (i == item.listaEtapaCalificada.Count - 1 && item.listaEtapaCalificada[i].IdEstadoEtapaProcesoSeleccion != 9)
                            {
                                item.listaEtapaCalificada[i].EsEtapaActual = true;
                            }

                            var etapaCalificada = repEtapaCalificacionRep.FirstBy(x => x.IdProcesoSeleccionEtapa == item.listaEtapaCalificada[i].IdEtapa && x.IdPostulante == item.listaEtapaCalificada[i].IdPostulante);
                            if (etapaCalificada != null)
                            {
                                etapaCalificada.EsEtapaAprobada = item.listaEtapaCalificada[i].EsEtapaAprobada;
                                etapaCalificada.IdEstadoEtapaProcesoSeleccion = item.listaEtapaCalificada[i].IdEstadoEtapaProcesoSeleccion; //Estado en Proceso
                                etapaCalificada.EsContactado = item.listaEtapaCalificada[i].EtapaContactado;
                                etapaCalificada.EsEtapaActual = item.listaEtapaCalificada[i].EsEtapaActual.Value;
                                etapaCalificada.UsuarioModificacion = Filtro.Usuario;
                                etapaCalificada.FechaModificacion = DateTime.Now;
                                repEtapaCalificacionRep.Update(etapaCalificada);
                            }
                            else
                            {
                                etapaCalificada = new EtapaProcesoSeleccionCalificadoBO();
                                etapaCalificada.EsEtapaAprobada = item.listaEtapaCalificada[i].EsEtapaAprobada;
                                etapaCalificada.IdEstadoEtapaProcesoSeleccion = item.listaEtapaCalificada[i].IdEstadoEtapaProcesoSeleccion; //Estado en Proceso
                                etapaCalificada.EsContactado = item.listaEtapaCalificada[i].EtapaContactado;
                                etapaCalificada.IdPostulante = item.listaEtapaCalificada[i].IdPostulante;
                                etapaCalificada.IdProcesoSeleccionEtapa = item.listaEtapaCalificada[i].IdEtapa.Value;
                                etapaCalificada.EsEtapaActual = item.listaEtapaCalificada[i].EsEtapaActual.Value;
                                etapaCalificada.Estado = true;
                                etapaCalificada.UsuarioCreacion = Filtro.Usuario;
                                etapaCalificada.UsuarioModificacion = Filtro.Usuario;
                                etapaCalificada.FechaCreacion = DateTime.Now;
                                etapaCalificada.FechaModificacion = DateTime.Now;
                                repEtapaCalificacionRep.Insert(etapaCalificada);
                            }
                        }
                    }
                    scope.Complete();
                }
                return Ok(true);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}