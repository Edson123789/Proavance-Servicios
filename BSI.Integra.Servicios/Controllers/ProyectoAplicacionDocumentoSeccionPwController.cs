using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
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
    [Route("api/ProyectoAplicacionDocumentoSeccionPw")]
    [ApiController]
    public class ProyectoAplicacionDocumentoSeccionPwController : Controller
    {
        private readonly integraDBContext _integraDBContext;
        public ProyectoAplicacionDocumentoSeccionPwController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
        }

        [Route("[Action]/{IdAlumno}")]
        [HttpPost]
        public ActionResult ObtenerEnviarProyectoAplicacion(int IdAlumno)
        {
            try
            {
                ProyectoAplicacionDocumentoSeccionPwRepositorio _repProyectoAplicacionDocumentoSeccionPw = new ProyectoAplicacionDocumentoSeccionPwRepositorio();
                return Ok(_repProyectoAplicacionDocumentoSeccionPw.ObtenerEnviarProyectoAplicacionPorAlumno(IdAlumno));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult InsertarProyectoAplicacionDocumentoSeccionPw(ProyectoAplicacionDocumentoSeccionPwInsertarDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ProyectoAplicacionDocumentoSeccionPwRepositorio ProyectoAplicacionDocumentoSeccionPwRepositorio2 = new ProyectoAplicacionDocumentoSeccionPwRepositorio(_integraDBContext);
                var idMatriculaCabecera = ProyectoAplicacionDocumentoSeccionPwRepositorio2.ObtenerIdMatricula(Json.IdAlumnoPEspecifico); 
                var IdProyectoAplicacionEntregaVersionPw = ProyectoAplicacionDocumentoSeccionPwRepositorio2.ObtenerIdProyectoAplicacionEntregaVersionPw(idMatriculaCabecera);

                foreach (var item in Json.ListaSecciones)
                {
                    ProyectoAplicacionDocumentoSeccionPwRepositorio ProyectoAplicacionDocumentoSeccionPwRepositorio = new ProyectoAplicacionDocumentoSeccionPwRepositorio(_integraDBContext);

                    string nombreSeccionTipoDetallePw;
                    int IdDocumentoSeccionPw;

                    if (item.NumeroFila != 0)
                    {
                        var partes = item.Clave.Split('_');
                        var IdSeccionTipoDetalle = Int32.Parse(partes[1]);
                        IdDocumentoSeccionPw = ProyectoAplicacionDocumentoSeccionPwRepositorio.ObtenerIdDocumentoSeccionPw(Json.IdPlantillaPw, Json.IdDocumentoPw, Json.IdSeccionPw, IdSeccionTipoDetalle, item.NumeroFila.Value);
                        nombreSeccionTipoDetallePw = ProyectoAplicacionDocumentoSeccionPwRepositorio.ObtenerNombreSeccionTipoDetallePw(IdSeccionTipoDetalle);
                    }
                    else
                    {
                        IdDocumentoSeccionPw = ProyectoAplicacionDocumentoSeccionPwRepositorio.ObtenerIdDocumentoSeccionPorTituloPw(Json.IdPlantillaPw, Json.IdDocumentoPw, item.Clave);
                        nombreSeccionTipoDetallePw = item.Clave;
                    }

                    if (nombreSeccionTipoDetallePw == "Nota" || nombreSeccionTipoDetallePw == "Estado" || nombreSeccionTipoDetallePw == "Feed Back")
                    {
                        ProyectoAplicacionDocumentoSeccionPwBO ProyectoAplicacionDocumentoSeccionPwBO = new ProyectoAplicacionDocumentoSeccionPwBO();
                        ProyectoAplicacionDocumentoSeccionPwBO.IdDocumentoSeccionPw = IdDocumentoSeccionPw;
                        ProyectoAplicacionDocumentoSeccionPwBO.IdMatriculaCabecera = idMatriculaCabecera;
                        ProyectoAplicacionDocumentoSeccionPwBO.Valor = item.Valor;
                        ProyectoAplicacionDocumentoSeccionPwBO.IdPlantillaPw = Json.IdPlantillaPw;
                        ProyectoAplicacionDocumentoSeccionPwBO.IdDocumentoPw = Json.IdDocumentoPw;
                        ProyectoAplicacionDocumentoSeccionPwBO.IdProyectoAplicacionEntregaVersionPw = IdProyectoAplicacionEntregaVersionPw;
                        ProyectoAplicacionDocumentoSeccionPwBO.Estado = true;
                        ProyectoAplicacionDocumentoSeccionPwBO.UsuarioCreacion = Json.Usuario;
                        ProyectoAplicacionDocumentoSeccionPwBO.UsuarioModificacion = Json.Usuario;
                        ProyectoAplicacionDocumentoSeccionPwBO.FechaCreacion = DateTime.Now;
                        ProyectoAplicacionDocumentoSeccionPwBO.FechaModificacion = DateTime.Now;
                        ProyectoAplicacionDocumentoSeccionPwBO.FechaCalificacion = DateTime.Now;

                        ProyectoAplicacionDocumentoSeccionPwRepositorio.Insert(ProyectoAplicacionDocumentoSeccionPwBO);
                    }
                                        
                }
               

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpPut]
        public ActionResult ActualizarProyectoAplicacionDocumentoSeccionPw(ProyectoAplicacionDocumentoSeccionPwDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ProyectoAplicacionDocumentoSeccionPwRepositorio ProyectoAplicacionDocumentoSeccionPwRepositorio = new ProyectoAplicacionDocumentoSeccionPwRepositorio(_integraDBContext);

                ProyectoAplicacionDocumentoSeccionPwBO ProyectoAplicacionDocumentoSeccionPwBO = ProyectoAplicacionDocumentoSeccionPwRepositorio.FirstById(Json.Id);
                ProyectoAplicacionDocumentoSeccionPwBO.IdDocumentoSeccionPw = Json.IdDocumentoSeccionPw;
                ProyectoAplicacionDocumentoSeccionPwBO.IdMatriculaCabecera = Json.IdMatriculaCabecera;
                ProyectoAplicacionDocumentoSeccionPwBO.Valor = Json.Valor;
                ProyectoAplicacionDocumentoSeccionPwBO.IdPlantillaPw = Json.IdPlantillaPw;
                ProyectoAplicacionDocumentoSeccionPwBO.IdDocumentoPw = Json.IdDocumentoPw;
                ProyectoAplicacionDocumentoSeccionPwBO.IdProyectoAplicacionEntregaVersionPw = Json.IdProyectoAplicacionEntregaVersionPw;
                ProyectoAplicacionDocumentoSeccionPwBO.IdMatriculaCabecera = Json.IdMatriculaCabecera;
                ProyectoAplicacionDocumentoSeccionPwBO.UsuarioModificacion = Json.Usuario;
                ProyectoAplicacionDocumentoSeccionPwBO.FechaModificacion = DateTime.Now;

                return Ok(ProyectoAplicacionDocumentoSeccionPwRepositorio.Update(ProyectoAplicacionDocumentoSeccionPwBO));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpDelete]
        public ActionResult EliminarProyectoAplicacionDocumentoSeccionPw(ProyectoAplicacionDocumentoSeccionPwDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ProyectoAplicacionDocumentoSeccionPwRepositorio ProyectoAplicacionDocumentoSeccionPwRepositorio = new ProyectoAplicacionDocumentoSeccionPwRepositorio();
                bool estadoEliminacion = false;
                using (TransactionScope scope = new TransactionScope())
                {
                    if (ProyectoAplicacionDocumentoSeccionPwRepositorio.Exist(Json.Id))
                    {
                        estadoEliminacion = ProyectoAplicacionDocumentoSeccionPwRepositorio.Delete(Json.Id, Json.Usuario);
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
