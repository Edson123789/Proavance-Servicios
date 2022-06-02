using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Transactions;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.GestionPersonas.BO;
using BSI.Integra.Aplicacion.GestionPersonas.Repositorio;
using BSI.Integra.Persistencia.Models;
using Newtonsoft.Json;
using System.Text;

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/MaestroExamenTest")]
    public class MaestroExamenTestController : Controller
    {
        private readonly integraDBContext _integraDBContext;
        public MaestroExamenTestController()
        {
            _integraDBContext = new integraDBContext();
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerExamenTest()
        {
            try
            {
                ExamenTestRepositorio repExamenTestRep = new ExamenTestRepositorio();

                var listaRespuesta = repExamenTestRep.GetBy(x => x.Estado == true, x => new { Id = x.Id, Nombre = x.Nombre,NombreAbreviado=x.NombreAbreviado, EsCalificadoPorPostulante = x.EsCalificadoPorPostulante, MostrarEvaluacionAgrupado=x.MostrarEvaluacionAgrupado, MostrarEvaluacionPorGrupo = x.MostrarEvaluacionPorGrupo, MostrarEvaluacionPorComponente = x.MostrarEvaluacionPorComponente, RequiereCentil =x.RequiereCentil,IdFormulaPuntaje=x.IdFormulaPuntaje,CalificarEvaluacion=x.CalificarEvaluacion, EsCalificacionAgrupada=x.EsCalificacionAgrupada,Factor=x.Factor, IdEvaluacionCategoria = x.IdEvaluacionCategoria }).OrderByDescending(w => w.Id).ToList();
                                
                return Ok(listaRespuesta);
                
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerEvaluacionEditar(int IdEvaluacion)
        {
            try
            {
                ExamenTestRepositorio repExamenTestRep = new ExamenTestRepositorio();
                ExamenRepositorio repExamenRep = new ExamenRepositorio();
                GrupoComponenteEvaluacionRepositorio repGrupoComponenteEvaluacionRep = new GrupoComponenteEvaluacionRepositorio();
                var listaIdGrupo = repExamenRep.GetBy(x => x.Estado == true && x.IdExamenTest == IdEvaluacion, x => new { Id = x.IdGrupoComponenteEvaluacion }).Select(x => x.Id).ToList();
                var ListaGrupo = repGrupoComponenteEvaluacionRep.GetBy(x => listaIdGrupo.Contains(x.Id) && !listaIdGrupo.Contains(null), x => new { Id = x.Id, Nombre = x.Nombre });


                var ListaComponentes = repExamenRep.GetBy(x => x.Estado == true && x.IdExamenTest == IdEvaluacion, x => new { Id = x.Id,Nombre=x.Nombre }).ToList();

                var listaExamenes = repExamenTestRep.ObtenerEvaluacionAgrupado(IdEvaluacion);

                return Ok(new { ListaExamenes = listaExamenes , ListaGrupo = ListaGrupo , ListaComponentes = ListaComponentes });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [Route("[action]")]
        [HttpPost]
        public ActionResult ObtenerEvaluacionEditarGrupo([FromBody]int IdEvaluacion)
        {
            try
            {
                ExamenTestRepositorio repExamenTestRep = new ExamenTestRepositorio();
                ExamenRepositorio repExamenRep = new ExamenRepositorio();
                GrupoComponenteEvaluacionRepositorio repGrupoComponenteEvaluacionRep = new GrupoComponenteEvaluacionRepositorio();
                var listaIdGrupo = repExamenRep.GetBy(x => x.Estado == true && x.IdExamenTest == IdEvaluacion && x.IdGrupoComponenteEvaluacion!=null, x => new { Id = x.IdGrupoComponenteEvaluacion }).Select(x => x.Id).ToList();
                listaIdGrupo = listaIdGrupo.Distinct().ToList();
                var ListaGrupo = repGrupoComponenteEvaluacionRep.GetBy(x => listaIdGrupo.Contains(x.Id) && !listaIdGrupo.Contains(null), x => new { Id = x.Id, Nombre = x.Nombre });
                var ListaComponentes = repExamenRep.GetBy(x => x.Estado == true && x.IdExamenTest == IdEvaluacion, x => new { Id = x.Id, Nombre = x.Nombre }).ToList();

                var listaGruposComponente=repGrupoComponenteEvaluacionRep.ObtenerGrupoEvaluacion(IdEvaluacion); // GrupoComponenteEvaluacionDTO

                List<GrupoComponenteEvaluacionDTO> ListaGrupoComponenteCompleta = new List<GrupoComponenteEvaluacionDTO>();
                ListaGrupoComponenteCompleta = listaGruposComponente.GroupBy(u => (u.Id, u.Nombre))
                                   .Select(group =>
                                   new GrupoComponenteEvaluacionDTO
                                   {
                                       Id = group.Key.Id                                      
                                       ,Nombre = group.Key.Nombre
                                       ,IdFormula= group.Select(x => x.IdFormula).FirstOrDefault()
                                       ,RequiereCentil= group.Select(x => x.RequiereCentil).FirstOrDefault()
                                       ,Factor= group.Select(x => x.Factor).FirstOrDefault()
                                       ,ListaComponentes = group.Select(x => new GrupoComponentesDTO { Id= x.IdExamen, Nombre = x.NombreExamen}).ToArray()
                                   }).ToList();


                return Ok(new { ListaGrupo = ListaGrupo, ListaComponentes = ListaComponentes,ListaGrupoComponente= ListaGrupoComponenteCompleta });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

		[Route("[action]")]
		[HttpPost]
		public ActionResult ObtenerEvaluacionEditarGrupoGrilla([FromBody]int IdEvaluacion)
		{
			try
			{
				List<GrupoComponenteEvaluacionDTO> ListaGrupoComponenteCompleta;
				if (IdEvaluacion > 0)
				{
					GrupoComponenteEvaluacionRepositorio repGrupoComponenteEvaluacionRep = new GrupoComponenteEvaluacionRepositorio();

					var listaGruposComponente = repGrupoComponenteEvaluacionRep.ObtenerGrupoEvaluacion(IdEvaluacion); // GrupoComponenteEvaluacionDTO

					ListaGrupoComponenteCompleta = listaGruposComponente.GroupBy(u => (u.Id, u.Nombre)).Select(group =>
									  new GrupoComponenteEvaluacionDTO
									  {
										  Id = group.Key.Id
										  ,
										  Nombre = group.Key.Nombre
										  ,
										  IdFormula = group.Select(x => x.IdFormula).FirstOrDefault()
										  ,
										  RequiereCentil = group.Select(x => x.RequiereCentil).FirstOrDefault()
										  ,
										  Factor = group.Select(x => x.Factor).FirstOrDefault()
										  ,
										  ListaComponentes = group.Select(x => new GrupoComponentesDTO { Id = x.IdExamen, Nombre = x.NombreExamen }).ToArray()
									  }).ToList();
				}
				else
				{
					ListaGrupoComponenteCompleta = new List<GrupoComponenteEvaluacionDTO>();
				}
				return Ok(ListaGrupoComponenteCompleta);

			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[Route("[action]")]
        [HttpPost]
        public ActionResult EliminarExamenTest([FromBody] EliminarDTO objeto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                integraDBContext _integraDBContext = new integraDBContext();
                CentilRepositorio repCentilRep = new CentilRepositorio(_integraDBContext);
                ExamenTestRepositorio repExamenTestRep = new ExamenTestRepositorio(_integraDBContext);
                var listaCentiles = repCentilRep.ObtenerCentilesEvaluacion(objeto.Id);
                using (TransactionScope scope = new TransactionScope())
                {
                    
                    foreach (var item in listaCentiles)
                    {
                        repCentilRep.Delete(item.Id, objeto.NombreUsuario);
                    }
                    if (repExamenTestRep.Exist(objeto.Id))                    
                        repExamenTestRep.Delete(objeto.Id, objeto.NombreUsuario);                     
                                       
                    
                    scope.Complete();
                    
                }
                return Ok(true);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult InsertarExamenTest([FromBody] ExamenTestDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ExamenTestRepositorio repExamenTestRep = new ExamenTestRepositorio(_integraDBContext);
                ExamenTestBO examenTest = new ExamenTestBO();
                CentilRepositorio repCentilRep = new CentilRepositorio(_integraDBContext);

                using (TransactionScope scope = new TransactionScope())
                {

                    examenTest.Nombre = Json.Nombre;
                    examenTest.NombreAbreviado = Json.NombreAbreviado;
                    examenTest.EsCalificadoPorPostulante = Json.EsCalificadoPorPostulante;
                    examenTest.MostrarEvaluacionAgrupado = Json.MostrarEvaluacionAgrupado;
                    examenTest.MostrarEvaluacionPorGrupo = Json.MostrarEvaluacionPorGrupo;
                    examenTest.MostrarEvaluacionPorComponente = Json.MostrarEvaluacionPorComponente;
                    examenTest.RequiereCentil = Json.RequiereCentil;
                    examenTest.IdFormulaPuntaje = Json.IdFormulaPuntaje;
                    examenTest.CalificarEvaluacion = Json.CalificarEvaluacion;
                    examenTest.EsCalificacionAgrupada = Json.EsCalificacionAgrupada;
                    examenTest.Factor = Json.Factor;

                    examenTest.Estado = true;
                    examenTest.UsuarioCreacion = Json.Usuario;
                    examenTest.FechaCreacion = DateTime.Now;
                    examenTest.UsuarioModificacion = Json.Usuario;
                    examenTest.FechaModificacion = DateTime.Now;
					examenTest.IdEvaluacionCategoria = Json.IdEvaluacionCategoria;

					repExamenTestRep.Insert(examenTest);
                    Json.Id = examenTest.Id;
                    foreach (var item in Json.ListaCentilEvaluacion)
                    {
                        CentilBO centil = new CentilBO();
                        centil.IdExamenTest = Json.Id;
                        centil.Centil = item.Centil;
                        centil.ValorMinimo = item.ValorMinimo;
                        centil.ValorMaximo = item.ValorMaximo;
                        centil.CentilLetra = item.CentilLetra;
                        centil.IdSexo = item.IdSexo;
                        centil.UsuarioCreacion = Json.Usuario;
                        centil.UsuarioModificacion = Json.Usuario;
                        centil.Estado = true;
                        centil.FechaCreacion = DateTime.Now;
                        centil.FechaModificacion = DateTime.Now;
                        repCentilRep.Insert(centil);
                    }

                    scope.Complete();
                }
                return Ok(Json);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public IActionResult ActualizarExamenTest([FromBody] ExamenTestDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                CentilRepositorio repCentilRep = new CentilRepositorio(_integraDBContext);
                AsignacionPreguntaExamenRepositorio repAsignacionPreguntaExamenRep = new AsignacionPreguntaExamenRepositorio(_integraDBContext);
                ExamenTestRepositorio repExamenTestRep = new ExamenTestRepositorio(_integraDBContext);
                ExamenTestBO examenTest = new ExamenTestBO();
                examenTest = repExamenTestRep.FirstById(Json.Id);

                using (TransactionScope scope = new TransactionScope())
                {
                    examenTest.Nombre = Json.Nombre;
                    examenTest.NombreAbreviado = Json.NombreAbreviado;
                    examenTest.EsCalificadoPorPostulante = Json.EsCalificadoPorPostulante;
                    examenTest.MostrarEvaluacionAgrupado = Json.MostrarEvaluacionAgrupado;
                    examenTest.MostrarEvaluacionPorGrupo = Json.MostrarEvaluacionPorGrupo;
                    examenTest.MostrarEvaluacionPorComponente = Json.MostrarEvaluacionPorComponente;
                    examenTest.RequiereCentil = Json.RequiereCentil;
                    examenTest.IdFormulaPuntaje = Json.IdFormulaPuntaje;
                    examenTest.CalificarEvaluacion = Json.CalificarEvaluacion;
                    examenTest.EsCalificacionAgrupada = Json.EsCalificacionAgrupada;
                    examenTest.Factor = Json.Factor;
                    examenTest.UsuarioModificacion = Json.Usuario;
                    examenTest.FechaModificacion = DateTime.Now;
					examenTest.IdEvaluacionCategoria = Json.IdEvaluacionCategoria;
					repExamenTestRep.Update(examenTest);
                    scope.Complete();
                }

                foreach (var examenV in Json.ListaExamenVisualizacion) {
                    AsignacionPreguntaExamenBO asignado = repAsignacionPreguntaExamenRep.FirstById(examenV.IdAsignacionPreguntaExamen);
                    if (asignado != null && asignado.Id!=0 && examenV.NroOrden!=asignado.NroOrden) {
                        asignado.NroOrden = examenV.NroOrden;
                        asignado.UsuarioModificacion = Json.Usuario;
                        asignado.FechaModificacion = DateTime.Now;
                        repAsignacionPreguntaExamenRep.Update(asignado);
                    }
                }

                var listaCentiles = repCentilRep.ObtenerCentilesEvaluacion(Json.Id);
                foreach (var item in listaCentiles)
                {                    
                    if (!Json.ListaCentilEvaluacion.Any(x => x.Id == item.Id))
                    {
                        repCentilRep.Delete(item.Id, Json.Usuario);
                    }
                }
                foreach (var item in Json.ListaCentilEvaluacion)
                {
                    if (item.Id > 0)
                    {
                        var centil = repCentilRep.FirstById(item.Id);
                        centil.IdExamenTest = Json.Id;
                        centil.Centil = item.Centil;
                        centil.ValorMinimo = item.ValorMinimo;
                        centil.ValorMaximo = item.ValorMaximo;
                        centil.CentilLetra = item.CentilLetra;
                        centil.IdSexo = item.IdSexo;
                        centil.UsuarioModificacion = Json.Usuario;
                        centil.FechaModificacion = DateTime.Now;
                        repCentilRep.Update(centil);
                    }
                    else
                    {
                        CentilBO centil = new CentilBO();
                        centil.IdExamenTest = Json.Id;
                        centil.Centil = item.Centil;
                        centil.ValorMinimo = item.ValorMinimo;
                        centil.ValorMaximo = item.ValorMaximo;
                        centil.CentilLetra = item.CentilLetra;
                        centil.IdSexo = item.IdSexo;
                        centil.UsuarioCreacion = Json.Usuario;
                        centil.UsuarioModificacion = Json.Usuario;
                        centil.Estado = true;
                        centil.FechaCreacion = DateTime.Now;
                        centil.FechaModificacion = DateTime.Now;
                        repCentilRep.Insert(centil);
                    }
                }

                string rpta = "ACTUALIZADO CORRECTAMENTE";
                return Ok(new { rpta });

            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerCombosExamenTest()
        {
            try
            {
                FormulaPuntajeRepositorio repFormulaPuntajeRep = new FormulaPuntajeRepositorio();
				EvaluacionCategoriaRepositorio _repEvaluacionCategoria = new EvaluacionCategoriaRepositorio(_integraDBContext);
                var listaFormula= repFormulaPuntajeRep.GetBy(x => x.Estado == true, x => new { Id = x.Id, Nombre = x.Nombre }).ToList();
                ExamenTestRepositorio repExamenTestRep = new ExamenTestRepositorio(_integraDBContext);
                SexoRepositorio repSexo = new SexoRepositorio(_integraDBContext);
				var evaluacionCategoria = _repEvaluacionCategoria.ObtenerCategoriasEvaluacionRegistradas();
				var listaSexo = repSexo.GetFiltroIdNombre();

                return Ok(new { ListaFormula = listaFormula, listaSexo= listaSexo, EvaluacionCategoria = evaluacionCategoria });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [Route("[action]")]
        [HttpPost]
        public ActionResult ObtenerCentilEvaluacion([FromBody] int IdExamenTest)
        {
            try
            {
                CentilRepositorio centilRep = new CentilRepositorio();

                var listaCentil = centilRep.ObtenerCentilesEvaluacion(IdExamenTest);

                return Ok(listaCentil);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerAsignacionEvaluaciones(int IdEvaluacion)
        {
            try
            {
                ExamenRepositorio repExamenRep = new ExamenRepositorio();
                var ExamenesNoAsignados = repExamenRep.GetBy(x => x.Estado == true && x.IdExamenTest == null, x => new { Id = x.Id,NombreComponente=x.Nombre+"("+x.Titulo+")" }).ToList();
                var ExamenesAsignados = repExamenRep.GetBy(x =>x.Estado==true && x.IdExamenTest == IdEvaluacion , x => new { Id = x.Id, NombreComponente = x.Nombre + "(" + x.Titulo + ")" }).ToList();
                                
                return Ok(new { ExamenesAsignados = ExamenesAsignados, ExamenesNoAsignados = ExamenesNoAsignados });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public IActionResult ActualizarAsignacionComponenteAEvaluacion([FromBody] AsignacionComponenteEvaluacionDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ExamenRepositorio repExamenRep = new ExamenRepositorio(_integraDBContext);
                
                using (TransactionScope scope = new TransactionScope())
                {
                    foreach (var NoAsignado in Json.ListaComponenteNoAsignado) {
                        ExamenBO examen = new ExamenBO();
                        examen = repExamenRep.FirstById(NoAsignado.Id);
                        if (examen.Id != 0 && examen.IdExamenTest != null) {

                            examen.IdExamenTest = null;
                            examen.UsuarioModificacion = Json.Usuario;
                            examen.FechaModificacion = DateTime.Now;
                            repExamenRep.Update(examen);
                        }
                    }

                    foreach (var Asignado in Json.ListaComponenteAsignado)
                    {
                        ExamenBO examen = new ExamenBO();
                        examen = repExamenRep.FirstById(Asignado.Id);
                        if (examen.Id != 0 && examen.IdExamenTest != Json.IdEvaluacion)
                        {

                            examen.IdExamenTest = Json.IdEvaluacion;
                            examen.UsuarioModificacion = Json.Usuario;
                            examen.FechaModificacion = DateTime.Now;
                            repExamenRep.Update(examen);
                        }
                    }
                    scope.Complete();
                }

                
                string rpta = "ACTUALIZADO CORRECTAMENTE";
                return Ok(new { rpta });

            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        /// TipoFuncion: POST
        /// Autor: Edgar S.
        /// Fecha: 21/05/2021
        /// Versión: 1.0
        /// <summary>
        /// Recibe valor encriptado de configuración de evaluación
        /// </summary>
        /// <param name="InformacionExamenEncriptado"> Información de configuración de Evaluación Encriptado </param>
        /// <returns> Confirmación de actualización : String </returns>
        [Route("[action]")]
        [HttpPost]
        public IActionResult ActualizarExamenTestEncriptar([FromBody] ExamenTestEncriptadoDTO InformacionExamenEncriptado)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ExamenTestDTO configuracionExamenTest = JsonConvert.DeserializeObject<ExamenTestDTO>(Encoding.UTF8.GetString(Convert.FromBase64String(InformacionExamenEncriptado.DatosEncriptados)));
                CentilRepositorio _repCentilRep = new CentilRepositorio(_integraDBContext);
                AsignacionPreguntaExamenRepositorio _repAsignacionPreguntaExamenRep = new AsignacionPreguntaExamenRepositorio(_integraDBContext);
                ExamenTestRepositorio _repExamenTestRep = new ExamenTestRepositorio(_integraDBContext);
                ExamenTestBO examenTest = new ExamenTestBO();
                examenTest = _repExamenTestRep.FirstById(configuracionExamenTest.Id);

                using (TransactionScope scope = new TransactionScope())
                {
                    examenTest.Nombre = configuracionExamenTest.Nombre;
                    examenTest.NombreAbreviado = configuracionExamenTest.NombreAbreviado;
                    examenTest.EsCalificadoPorPostulante = configuracionExamenTest.EsCalificadoPorPostulante;
                    examenTest.MostrarEvaluacionAgrupado = configuracionExamenTest.MostrarEvaluacionAgrupado;
                    examenTest.MostrarEvaluacionPorGrupo = configuracionExamenTest.MostrarEvaluacionPorGrupo;
                    examenTest.MostrarEvaluacionPorComponente = configuracionExamenTest.MostrarEvaluacionPorComponente;
                    examenTest.RequiereCentil = configuracionExamenTest.RequiereCentil;
                    examenTest.IdFormulaPuntaje = configuracionExamenTest.IdFormulaPuntaje;
                    examenTest.CalificarEvaluacion = configuracionExamenTest.CalificarEvaluacion;
                    examenTest.EsCalificacionAgrupada = configuracionExamenTest.EsCalificacionAgrupada;
                    examenTest.Factor = configuracionExamenTest.Factor;
                    examenTest.UsuarioModificacion = configuracionExamenTest.Usuario;
                    examenTest.FechaModificacion = DateTime.Now;
                    examenTest.IdEvaluacionCategoria = configuracionExamenTest.IdEvaluacionCategoria;
                    _repExamenTestRep.Update(examenTest);
                    scope.Complete();
                }

                if(configuracionExamenTest.ListaExamenVisualizacion == null)
                {
                    List<EvaluacionAgrupadaComponenteDTO> auxiliar = new List<EvaluacionAgrupadaComponenteDTO>();
                    configuracionExamenTest.ListaExamenVisualizacion = auxiliar;
                }
                if (configuracionExamenTest.ListaCentilEvaluacion == null)
                {
                    List<CentilDTO> auxiliar = new List<CentilDTO>();
                    configuracionExamenTest.ListaCentilEvaluacion = auxiliar;
                }

                if (configuracionExamenTest.ListaExamenVisualizacion != null && configuracionExamenTest.ListaExamenVisualizacion.Count > 0)
                {
                    foreach (var examenV in configuracionExamenTest.ListaExamenVisualizacion)
                    {
                        AsignacionPreguntaExamenBO asignado = _repAsignacionPreguntaExamenRep.FirstById(examenV.IdAsignacionPreguntaExamen);
                        if (asignado != null && asignado.Id != 0 && examenV.NroOrden != asignado.NroOrden)
                        {
                            asignado.NroOrden = examenV.NroOrden;
                            asignado.UsuarioModificacion = configuracionExamenTest.Usuario;
                            asignado.FechaModificacion = DateTime.Now;
                            _repAsignacionPreguntaExamenRep.Update(asignado);
                        }
                    }
                }
                var listaCentiles = _repCentilRep.ObtenerCentilesEvaluacion(configuracionExamenTest.Id);
                if (listaCentiles != null && listaCentiles.Count > 0)
                {
                    foreach (var item in listaCentiles)
                    {
                        if (!configuracionExamenTest.ListaCentilEvaluacion.Any(x => x.Id == item.Id))
                        {
                            _repCentilRep.Delete(item.Id, configuracionExamenTest.Usuario);
                        }
                    }
                }                
                if (configuracionExamenTest.ListaCentilEvaluacion != null && configuracionExamenTest.ListaCentilEvaluacion.Count > 0)
                {
                    foreach (var item in configuracionExamenTest.ListaCentilEvaluacion)
                    {
                        if (item.Id > 0)
                        {
                            var centil = _repCentilRep.FirstById(item.Id);
                            centil.IdExamenTest = configuracionExamenTest.Id;
                            centil.Centil = item.Centil;
                            centil.ValorMinimo = item.ValorMinimo;
                            centil.ValorMaximo = item.ValorMaximo;
                            centil.CentilLetra = item.CentilLetra;
                            centil.IdSexo = item.IdSexo;
                            centil.UsuarioModificacion = configuracionExamenTest.Usuario;
                            centil.FechaModificacion = DateTime.Now;
                            _repCentilRep.Update(centil);
                        }
                        else
                        {
                            CentilBO centil = new CentilBO();
                            centil.IdExamenTest = configuracionExamenTest.Id;
                            centil.Centil = item.Centil;
                            centil.ValorMinimo = item.ValorMinimo;
                            centil.ValorMaximo = item.ValorMaximo;
                            centil.CentilLetra = item.CentilLetra;
                            centil.IdSexo = item.IdSexo;
                            centil.UsuarioCreacion = configuracionExamenTest.Usuario;
                            centil.UsuarioModificacion = configuracionExamenTest.Usuario;
                            centil.Estado = true;
                            centil.FechaCreacion = DateTime.Now;
                            centil.FechaModificacion = DateTime.Now;
                            _repCentilRep.Insert(centil);
                        }
                    }
                }
                string rpta = "ACTUALIZADO CORRECTAMENTE";
                return Ok(new { rpta });

            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }
    }
}