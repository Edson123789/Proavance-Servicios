using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.GestionPersonas.Repositorio;
using BSI.Integra.Aplicacion.GestionPersonas.BO;
using BSI.Integra.Persistencia.Models;
using System.Transactions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/GrupoComponenteEvaluacion")]
    public class GrupoComponenteEvaluacionController : Controller
    {
        private readonly integraDBContext _integraDBContext;
        public GrupoComponenteEvaluacionController()
        {
            _integraDBContext = new integraDBContext();
        }

		[Route("[action]")]
		[HttpPost]
		public ActionResult ObtenerGruposComponenteDesglosado([FromBody]int IdEvaluacion)
		{
			try
			{
				GrupoComponenteEvaluacionRepositorio repGrupoComponenteEvaluacionRep = new GrupoComponenteEvaluacionRepositorio();
				var listaGruposComponente = repGrupoComponenteEvaluacionRep.ObtenerGrupoEvaluacionDesglosadoPorComponente(IdEvaluacion); // GrupoComponenteEvaluacionDTO
				return Ok(listaGruposComponente);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[Route("[action]")]
        [HttpPost]
        public ActionResult InsertarGrupoComponente([FromBody]GrupoComponenteEvaluacionFormularioDTO Formulario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                GrupoComponenteEvaluacionRepositorio repGrupoComponenteEvaluacionRep = new GrupoComponenteEvaluacionRepositorio(_integraDBContext);
                GrupoComponenteEvaluacionBO grupoComponente = new GrupoComponenteEvaluacionBO();
                ExamenRepositorio _repExamen = new ExamenRepositorio(_integraDBContext);

                using (TransactionScope scope = new TransactionScope())
                {

                    grupoComponente.Nombre = Formulario.GrupoComponenteEvaluacion.Nombre;
                    grupoComponente.NombreAbreviado = Formulario.GrupoComponenteEvaluacion.Nombre;
                    grupoComponente.IdFormulaPuntaje = Formulario.GrupoComponenteEvaluacion.IdFormula;
                    grupoComponente.RequiereCentil = Formulario.GrupoComponenteEvaluacion.RequiereCentil;
                    grupoComponente.Factor = Formulario.GrupoComponenteEvaluacion.Factor;
                    grupoComponente.Estado = true;
                    grupoComponente.UsuarioCreacion = Formulario.Usuario;
                    grupoComponente.FechaCreacion = DateTime.Now;
                    grupoComponente.UsuarioModificacion = Formulario.Usuario;
                    grupoComponente.FechaModificacion = DateTime.Now;

                    repGrupoComponenteEvaluacionRep.Insert(grupoComponente);

                    foreach (var item in Formulario.GrupoComponenteEvaluacion.ListaComponentes) {
                        ExamenBO examen = new ExamenBO();
                        examen = _repExamen.FirstById(item.Id);
                        examen.IdGrupoComponenteEvaluacion = grupoComponente.Id;
                        examen.UsuarioModificacion = Formulario.Usuario;
                        examen.FechaModificacion = DateTime.Now;
                        _repExamen.Update(examen);
                    }
                    scope.Complete();
                }
                Formulario.GrupoComponenteEvaluacion.Id = grupoComponente.Id;
                string rpta = "INSERTADO CORRECTAMENTE";
                return Ok(Formulario.GrupoComponenteEvaluacion);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult ActualizarGrupoComponente([FromBody]GrupoComponenteEvaluacionFormularioDTO Formulario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                GrupoComponenteEvaluacionRepositorio repGrupoComponenteEvaluacionRep = new GrupoComponenteEvaluacionRepositorio(_integraDBContext);
                GrupoComponenteEvaluacionBO grupoComponente = new GrupoComponenteEvaluacionBO();
                ExamenRepositorio _repExamen = new ExamenRepositorio(_integraDBContext);
                grupoComponente = repGrupoComponenteEvaluacionRep.FirstById(Formulario.GrupoComponenteEvaluacion.Id);
                var ListaExamenExistente = _repExamen.GetBy(x => x.IdExamenTest == Formulario.IdEvaluacion && x.IdGrupoComponenteEvaluacion == Formulario.GrupoComponenteEvaluacion.Id).ToList();
                List<int> IdExistente = new List<int>();
                using (TransactionScope scope = new TransactionScope())
                {
                    grupoComponente.Nombre = Formulario.GrupoComponenteEvaluacion.Nombre;
                    grupoComponente.NombreAbreviado = Formulario.GrupoComponenteEvaluacion.Nombre;
                    grupoComponente.IdFormulaPuntaje = Formulario.GrupoComponenteEvaluacion.IdFormula;
                    grupoComponente.RequiereCentil = Formulario.GrupoComponenteEvaluacion.RequiereCentil;
                    grupoComponente.Factor = Formulario.GrupoComponenteEvaluacion.Factor;
                    grupoComponente.Estado = true;
                    grupoComponente.UsuarioModificacion = Formulario.Usuario;
                    grupoComponente.FechaModificacion = DateTime.Now;
                    repGrupoComponenteEvaluacionRep.Update(grupoComponente);

                    foreach (var item in ListaExamenExistente)
                    {
                        ExamenBO examen = new ExamenBO();
                        examen = _repExamen.FirstById(item.Id);
                        examen.IdGrupoComponenteEvaluacion = null;
                        _repExamen.Update(examen);
                    }

                    foreach (var item in Formulario.GrupoComponenteEvaluacion.ListaComponentes)
                    {
                        ExamenBO examen = new ExamenBO();
                        examen = _repExamen.FirstById(item.Id);
                        examen.IdGrupoComponenteEvaluacion = grupoComponente.Id;
                        examen.UsuarioModificacion = Formulario.Usuario;
                        examen.FechaModificacion = DateTime.Now;
                        _repExamen.Update(examen);
                    }

                    scope.Complete();
                }
                string rpta = "ACTUALIZADO CORRECTAMENTE";
                return Ok(Formulario.GrupoComponenteEvaluacion);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult EliminarGrupoComponente([FromBody]EliminarDTO eliminar)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                GrupoComponenteEvaluacionRepositorio _repGrupoComponenteEvaluacion = new GrupoComponenteEvaluacionRepositorio(_integraDBContext);
                ExamenRepositorio _repExamen = new ExamenRepositorio(_integraDBContext);
				CentilRepositorio _repCentil = new CentilRepositorio(_integraDBContext);
                using (TransactionScope scope = new TransactionScope())
                {
                    if (_repGrupoComponenteEvaluacion.Exist(eliminar.Id))
                    {
                        List<ExamenBO> ListaExamenes = _repExamen.GetBy(x => x.IdGrupoComponenteEvaluacion == eliminar.Id).ToList();
                        foreach (var examen in ListaExamenes)
                        {
                            examen.IdGrupoComponenteEvaluacion = null;
                            examen.UsuarioModificacion = eliminar.NombreUsuario;
                            examen.FechaModificacion = DateTime.Now;
                            _repExamen.Update(examen);
                        }
						var listaCentiles = _repCentil.ObtenerCentilesGrupoEvaluacion(eliminar.Id);
						foreach (var item in listaCentiles)
						{
							_repCentil.Delete(item.Id, eliminar.NombreUsuario);
						} 
                        GrupoComponenteEvaluacionBO grupo = new GrupoComponenteEvaluacionBO();
                        grupo = _repGrupoComponenteEvaluacion.FirstById(eliminar.Id);

                        grupo.Estado = false;
                        grupo.UsuarioModificacion =eliminar.NombreUsuario;
                        grupo.FechaModificacion = DateTime.Now;
                        _repGrupoComponenteEvaluacion.Update(grupo);


                        scope.Complete();
                    }
                    else
                    {
                        return BadRequest("El Grupo Componente no existe o ya fue eliminado");
                    }
                }
                return Ok(eliminar);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

		[Route("[action]")]
		public ActionResult ObtenerCentilGrupoComponente([FromBody]int IdGrupoComponenteEvaluacion)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				CentilRepositorio _repCentil = new CentilRepositorio(_integraDBContext);
				var centiles = _repCentil.ObtenerCentilesGrupoEvaluacion(IdGrupoComponenteEvaluacion);
				return Ok(centiles);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[Route("[action]")]
		public ActionResult InsertarCentilGrupoComponente([FromBody]ObjetoCentilCompuestoDTO CentilFormulario)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				CentilRepositorio _repCentil = new CentilRepositorio(_integraDBContext);
				CentilBO centil = new CentilBO()
				{
					IdExamenTest = CentilFormulario.Centil.IdExamenTest == 0 ? null : CentilFormulario.Centil.IdExamenTest,
					IdGrupoComponenteEvaluacion = CentilFormulario.Centil.IdGrupoComponenteEvaluacion == 0 ? null : CentilFormulario.Centil.IdGrupoComponenteEvaluacion,
					IdExamen = CentilFormulario.Centil.IdExamen == 0 ? null : CentilFormulario.Centil.IdExamen,
					IdSexo = CentilFormulario.Centil.IdSexo == 0 ? null : CentilFormulario.Centil.IdSexo,
					ValorMinimo = CentilFormulario.Centil.ValorMinimo,
					ValorMaximo = CentilFormulario.Centil.ValorMaximo,
					Centil = CentilFormulario.Centil.Centil,
					CentilLetra = CentilFormulario.Centil.CentilLetra,
					Estado = true,
					UsuarioCreacion = CentilFormulario.Usuario,
					UsuarioModificacion = CentilFormulario.Usuario,
					FechaCreacion = DateTime.Now,
					FechaModificacion = DateTime.Now
				};
				_repCentil.Insert(centil);
				return Ok(true);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[Route("[action]")]
		public ActionResult ActualizarCentilGrupoComponente([FromBody]ObjetoCentilCompuestoDTO CentilFormulario)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				CentilRepositorio _repCentil = new CentilRepositorio(_integraDBContext);
				var centil = _repCentil.FirstById(CentilFormulario.Centil.Id);
				if(centil != null)
				{
					centil.IdExamenTest = CentilFormulario.Centil.IdExamenTest;
					centil.IdGrupoComponenteEvaluacion = CentilFormulario.Centil.IdGrupoComponenteEvaluacion;
					centil.IdExamen = CentilFormulario.Centil.IdExamen;
					centil.IdSexo = CentilFormulario.Centil.IdSexo;
					centil.ValorMinimo = CentilFormulario.Centil.ValorMinimo;
					centil.ValorMaximo = CentilFormulario.Centil.ValorMaximo;
					centil.Centil = CentilFormulario.Centil.Centil;
					centil.CentilLetra = CentilFormulario.Centil.CentilLetra;
					centil.Estado = true;
					centil.UsuarioModificacion = CentilFormulario.Usuario;
					centil.FechaModificacion = DateTime.Now;
					_repCentil.Update(centil);
				}
				return Ok(true);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[Route("[action]")]
		public ActionResult EliminarCentilGrupoComponente([FromBody]EliminarDTO Centil)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				CentilRepositorio _repCentil = new CentilRepositorio(_integraDBContext);
				if (_repCentil.Exist(Centil.Id))
				{
					_repCentil.Delete(Centil.Id, Centil.NombreUsuario);
				}
				return Ok(true);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}


		[Route("[action]")]
		public ActionResult ActualizarFactorGrupoComponente([FromBody]GrupoComponenteFactorDTO GrupoComponente)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				GrupoComponenteEvaluacionRepositorio _repGrupoComponenteEvaluacion = new GrupoComponenteEvaluacionRepositorio(_integraDBContext);
				var grupoComponenteEvaluacion = _repGrupoComponenteEvaluacion.FirstById(GrupoComponente.IdGrupoComponenteEvaluacion);
				grupoComponenteEvaluacion.Factor = GrupoComponente.Factor;
				grupoComponenteEvaluacion.UsuarioModificacion = GrupoComponente.Usuario;
				grupoComponenteEvaluacion.FechaModificacion = DateTime.Now;
				var res = _repGrupoComponenteEvaluacion.Update(grupoComponenteEvaluacion);
				return Ok(res);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}
	}
}
