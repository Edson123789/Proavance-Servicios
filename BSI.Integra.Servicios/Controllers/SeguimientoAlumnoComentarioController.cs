using System;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/[controller]")]
    public class SeguimientoAlumnoComentarioController : Controller
    {
        private readonly integraDBContext _integraDBContext;
        private readonly SeguimientoAlumnoComentarioRepositorio _repSeguimientoAlumnoComentario;
        public SeguimientoAlumnoComentarioController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
            _repSeguimientoAlumnoComentario = new SeguimientoAlumnoComentarioRepositorio(_integraDBContext);
        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult Insertar([FromBody] SeguimientoAlumnoComentarioDTO Objeto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                if (Objeto.IdSeguimientoAlumnoCategoriaPago != null)
                {
                    SeguimientoAlumnoComentarioBO seguimientoAlumnoComentarioPago = new SeguimientoAlumnoComentarioBO
                    {
                        IdMatriculaCabecera = Objeto.IdMatriculaCabecera,
                        NroCuota = Objeto.NroCuota,
                        NroSubCuota = Objeto.NroSubCuota,
                        IdSeguimientoAlumnoCategoria = Objeto.IdSeguimientoAlumnoCategoriaPago.Value,
                        IdPersonal = Objeto.IdPersonal,
                        IdOportunidad = Objeto.IdOportunidad,
                        Comentario = Objeto.ComentarioPago,
                        FechaCompromiso = null,//Objeto.FechaCompromiso,
                        Estado = true,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        UsuarioCreacion = Objeto.Usuario,
                        UsuarioModificacion = Objeto.Usuario
                    };
                    if (!seguimientoAlumnoComentarioPago.HasErrors)
                    {
                        _repSeguimientoAlumnoComentario.Insert(seguimientoAlumnoComentarioPago);
                    }
                }

                if (Objeto.IdSeguimientoAlumnoCategoriaAcademico != null)
                {
                    SeguimientoAlumnoComentarioBO seguimientoAlumnoComentarioAcademico = new SeguimientoAlumnoComentarioBO
                    {
                        IdMatriculaCabecera = Objeto.IdMatriculaCabecera,
                        NroCuota = Objeto.NroCuota,
                        NroSubCuota = Objeto.NroSubCuota,
                        IdSeguimientoAlumnoCategoria = Objeto.IdSeguimientoAlumnoCategoriaAcademico.Value,
                        IdPersonal = Objeto.IdPersonal,
                        IdOportunidad = Objeto.IdOportunidad,
                        Comentario = Objeto.ComentarioAcademico,
                        FechaCompromiso = null,//Objeto.FechaCompromiso,
                        Estado = true,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        UsuarioCreacion = Objeto.Usuario,
                        UsuarioModificacion = Objeto.Usuario
                    };
                    if (!seguimientoAlumnoComentarioAcademico.HasErrors)
                    {
                        _repSeguimientoAlumnoComentario.Insert(seguimientoAlumnoComentarioAcademico);
                    }
                }

                
                return Ok(Objeto);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        //[Route("[Action]")]
        //[HttpPost]
        //public ActionResult Actualizar([FromBody] SeguimientoAlumnoComentarioDTO Objeto)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }
        //    try
        //    {
        //        if (!_repSeguimientoAlumnoComentario.Exist(Objeto.Id))
        //        {
        //            return BadRequest("El registro no existe!");
        //        }
        //        var seguimientoAlumnoComentario = _repSeguimientoAlumnoComentario.FirstById(Objeto.Id);
        //        seguimientoAlumnoComentario.IdSeguimientoAlumnoCategoria = Objeto.IdSeguimientoAlumnoCategoria;
        //        seguimientoAlumnoComentario.Comentario = Objeto.Comentario;
        //        seguimientoAlumnoComentario.FechaCompromiso = null;// Objeto.FechaCompromiso;
        //        seguimientoAlumnoComentario.FechaModificacion = DateTime.Now;
        //        seguimientoAlumnoComentario.UsuarioModificacion = Objeto.Usuario;
        //        if (!seguimientoAlumnoComentario.HasErrors)
        //        {
        //            _repSeguimientoAlumnoComentario.Update(seguimientoAlumnoComentario);
        //        }
        //        else
        //        {
        //            return Ok(seguimientoAlumnoComentario.GetErrors());
        //        }
        //        return Ok(seguimientoAlumnoComentario);
        //    }
        //    catch (Exception e)
        //    {
        //        return BadRequest(e.Message);
        //    }
        //}

        [Route("[Action]")]
        [HttpPost]
        public ActionResult Eliminar([FromBody] EliminarDTO Objeto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if (!_repSeguimientoAlumnoComentario.Exist(Objeto.Id))
                {
                    return BadRequest("No existe el registro");
                }
                return Ok(_repSeguimientoAlumnoComentario.Delete(Objeto.Id, Objeto.NombreUsuario));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
