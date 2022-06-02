using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.DTOs.Operaciones;
using BSI.Integra.Aplicacion.Operaciones.BO;
using BSI.Integra.Aplicacion.Operaciones.Repositorio;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/ProyectoAplicacionEntregaVersionPw")]
    [ApiController]
    public class ProyectoAplicacionEntregaVersionPwController : Controller
    {
        private readonly integraDBContext _integraDBContext;
        private readonly MaterialVersionRepositorio _repMaterialVersion;
        private readonly ProyectoAplicacionEntregaVersionPwRepositorio _repProyectoAplicacionEntregaVersionPw;
        public ProyectoAplicacionEntregaVersionPwController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
        }

        [Route("[Action]/{IdAlumno}")]
        [HttpPost]
        public ActionResult ObtenerEnviarProyectoAplicacion(int IdAlumno)
        {
            try
            {
                ProyectoAplicacionEntregaVersionPwRepositorio _repProyectoAplicacionEntregaVersionPw = new ProyectoAplicacionEntregaVersionPwRepositorio();
                return Ok(_repProyectoAplicacionEntregaVersionPw.ObtenerEnviarProyectoAplicacionPorAlumno(IdAlumno));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        //[Route("[action]")]
        //[HttpPost]
        //public ActionResult SubirDocumentosProyecto(ProyectoAplicacionEntregaVersionPwDTO ProyectoAplicacion, [FromForm] IList<IFormFile> Files)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }
        //    try
        //    {

        //        string NombreArchivotemp = "";
        //        string ContentType = "";
        //        var urlArchivoRepositorio = "";

        //        if (Files != null)
        //        {
        //            foreach (var file in Files)
        //            {
        //                ContentType = file.ContentType;
        //                NombreArchivotemp = file.FileName;
        //                NombreArchivotemp = string.Concat(NombreArchivotemp);
        //                urlArchivoRepositorio = _repMaterialVersion.SubirDocumentosProyectoAplicacionRepositorio(file.ConvertToByte(), file.ContentType, NombreArchivotemp);
        //            }
        //        }


        //        var ProyectoAplicacionEntregaVersionPw = new ProyectoAplicacionEntregaVersionPwBO
        //        {

        //            NombreArchivo = NombreArchivotemp,
        //            RutaArchivo = urlArchivoRepositorio,
        //            Version = ProyectoAplicacion.Version,
        //            FechaEnvio = ProyectoAplicacion.FechaEnvio,
        //            IdMatriculaCabecera = ProyectoAplicacion.IdMatriculaCabecera,
        //            FechaCreacion = DateTime.Now,
        //            FechaModificacion = DateTime.Now,
        //            UsuarioCreacion = ProyectoAplicacion.Usuario,
        //            UsuarioModificacion = ProyectoAplicacion.Usuario,
        //            Estado = true
        //        };

        //        var result = _repProyectoAplicacionEntregaVersionPw.Insert(ProyectoAplicacionEntregaVersionPw);
               
        //        return Ok(urlArchivoRepositorio);
        //    }
        //    catch (Exception e)
        //    {
        //        return BadRequest(e.Message);
        //    }
        //}


        [Route("[action]")]
        [HttpPost]
        public ActionResult InsertarProyectoAplicacionEntregaVersionPw(ProyectoAplicacionEntregaVersionPwDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ProyectoAplicacionEntregaVersionPwRepositorio proyectoAplicacionEntregaVersionPwRepositorio = new ProyectoAplicacionEntregaVersionPwRepositorio(_integraDBContext);

                ProyectoAplicacionEntregaVersionPwBO proyectoAplicacionEntregaVersionPwBO = new ProyectoAplicacionEntregaVersionPwBO();
                proyectoAplicacionEntregaVersionPwBO.NombreArchivo = Json.EnlaceProyecto;
                proyectoAplicacionEntregaVersionPwBO.Version = Json.Version;
                proyectoAplicacionEntregaVersionPwBO.FechaEnvio = Json.FechaEnvio;
                proyectoAplicacionEntregaVersionPwBO.IdMatriculaCabecera = Json.IdMatriculaCabecera;
                proyectoAplicacionEntregaVersionPwBO.Estado = true;
                proyectoAplicacionEntregaVersionPwBO.UsuarioCreacion = Json.Usuario;
                proyectoAplicacionEntregaVersionPwBO.UsuarioModificacion = Json.Usuario;
                proyectoAplicacionEntregaVersionPwBO.FechaCreacion = DateTime.Now;
                proyectoAplicacionEntregaVersionPwBO.FechaModificacion = DateTime.Now;

                return Ok(proyectoAplicacionEntregaVersionPwRepositorio.Insert(proyectoAplicacionEntregaVersionPwBO));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpPut]
        public ActionResult ActualizarProyectoAplicacionEntregaVersionPw(ProyectoAplicacionEntregaVersionPwDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ProyectoAplicacionEntregaVersionPwRepositorio proyectoAplicacionEntregaVersionPwRepositorio = new ProyectoAplicacionEntregaVersionPwRepositorio(_integraDBContext);

                ProyectoAplicacionEntregaVersionPwBO proyectoAplicacionEntregaVersionPwBO = proyectoAplicacionEntregaVersionPwRepositorio.FirstById(Json.Id);
                proyectoAplicacionEntregaVersionPwBO.NombreArchivo = Json.EnlaceProyecto;
                proyectoAplicacionEntregaVersionPwBO.Version = Json.Version;
                proyectoAplicacionEntregaVersionPwBO.FechaEnvio = Json.FechaEnvio;
                proyectoAplicacionEntregaVersionPwBO.IdMatriculaCabecera = Json.IdMatriculaCabecera;
                proyectoAplicacionEntregaVersionPwBO.UsuarioModificacion = Json.Usuario;
                proyectoAplicacionEntregaVersionPwBO.FechaModificacion = DateTime.Now;

                return Ok(proyectoAplicacionEntregaVersionPwRepositorio.Update(proyectoAplicacionEntregaVersionPwBO));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpDelete]
        public ActionResult EliminarProyectoAplicacionEntregaVersionPw(ProyectoAplicacionEntregaVersionPwDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ProyectoAplicacionEntregaVersionPwRepositorio proyectoAplicacionEntregaVersionPwRepositorio = new ProyectoAplicacionEntregaVersionPwRepositorio();
                bool estadoEliminacion = false;
                using (TransactionScope scope = new TransactionScope())
                {
                    if (proyectoAplicacionEntregaVersionPwRepositorio.Exist(Json.Id))
                    {
                        estadoEliminacion = proyectoAplicacionEntregaVersionPwRepositorio.Delete(Json.Id, Json.Usuario);
                    }

                    scope.Complete();
                }
                return Ok(estadoEliminacion);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


    }
}
