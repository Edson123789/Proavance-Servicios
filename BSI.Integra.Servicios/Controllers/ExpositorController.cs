using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Operaciones.BO;
using BSI.Integra.Aplicacion.Operaciones.Repositorio;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/Expositor")]
    [ApiController]
    public class ExpositorController : Controller
    {
        private readonly integraDBContext _integraDBContext;
        public ExpositorController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
        }

        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerParaFiltro()
        {
            try
            {
                ExpositorRepositorio _repExpositor = new ExpositorRepositorio();
                return Ok(_repExpositor.ObtenerListado().Select(x => new { x.Id, NombreCompleto = string.Concat(x.Nombres, x.Apellidos) }).ToList());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerTodo()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ExpositorRepositorio _repExpositorPorArea = new ExpositorRepositorio();
                return Ok(_repExpositorPorArea.ObtenerTodoGrid());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult Insertar([FromBody] ExpositorDTO Expositor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ExpositorRepositorio _repExpositor = new ExpositorRepositorio(_integraDBContext);
                ExpositorBO expositorBO = new ExpositorBO();
                PersonaBO _persona = new PersonaBO(_integraDBContext);

                byte[] dataHojaVida = Convert.FromBase64String(Expositor.HojaVidaResumidaPerfil);
                var decodedHojaVida = Encoding.UTF8.GetString(dataHojaVida);

                var IdExpositorRepetido = _repExpositor.ObtenerExpositorEliminadoEmailRepetido(Expositor.Email1);
                if (IdExpositorRepetido == null || IdExpositorRepetido == 0)
                {
                    expositorBO.IdTipoDocumento = Expositor.IdTipoDocumento;
                    expositorBO.NroDocumento = Expositor.NroDocumento;
                    expositorBO.PrimerNombre = Expositor.PrimerNombre;
                    expositorBO.SegundoNombre = Expositor.SegundoNombre;
                    expositorBO.ApellidoPaterno = Expositor.ApellidoPaterno;
                    expositorBO.ApellidoMaterno = Expositor.ApellidoMaterno;
                    expositorBO.FechaNacimiento = Expositor.FechaNacimiento;
                    expositorBO.IdPaisProcedencia = Expositor.IdPaisProcedencia;
                    expositorBO.IdCiudadProcedencia = Expositor.IdCiudadProcedencia;
                    expositorBO.IdReferidoPor = Expositor.IdReferidoPor;
                    expositorBO.TelfCelular1 = Expositor.TelfCelular1;
                    expositorBO.TelfCelular2 = Expositor.TelfCelular2;
                    expositorBO.TelfCelular3 = Expositor.TelfCelular3;
                    expositorBO.Email1 = Expositor.Email1;
                    expositorBO.Email2 = Expositor.Email2;
                    expositorBO.Email3 = Expositor.Email3;
                    expositorBO.Domicilio = Expositor.Domicilio;
                    expositorBO.IdPaisDomicilio = Expositor.IdPaisDomicilio;
                    expositorBO.IdCiudadDomicilio = Expositor.IdCiudadDomicilio;
                    expositorBO.LugarTrabajo = Expositor.LugarTrabajo;
                    expositorBO.IdPaisLugarTrabajo = Expositor.IdPaisLugarTrabajo;
                    expositorBO.IdCiudadLugarTrabajo = Expositor.IdCiudadLugarTrabajo;
                    expositorBO.AsistenteNombre = Expositor.AsistenteNombre;
                    expositorBO.AsistenteTelefono = Expositor.AsistenteTelefono;
                    expositorBO.AsistenteCelular = Expositor.AsistenteCelular;
                    expositorBO.HojaVidaResumidaPerfil = decodedHojaVida;
                    expositorBO.HojaVidaResumidaSpeech = Expositor.HojaVidaResumidaSpeech;
                    expositorBO.FormacionAcademica = Expositor.FormacionAcademica;
                    expositorBO.ExperienciaProfesional = Expositor.ExperienciaProfesional;
                    expositorBO.Publicaciones = Expositor.Publicaciones;
                    expositorBO.PremiosDistinciones = Expositor.PremiosDistinciones;
                    expositorBO.OtraInformacion = Expositor.OtraInformacion;
                    expositorBO.Estado = true;
                    expositorBO.UsuarioCreacion = Expositor.Usuario;
                    expositorBO.UsuarioModificacion = Expositor.Usuario;
                    expositorBO.FechaCreacion = DateTime.Now;
                    expositorBO.FechaModificacion = DateTime.Now;
                    expositorBO.IdPersonalAsignado = Expositor.IdPersonalAsignado;

                    if (!string.IsNullOrEmpty(Expositor.FotoDocente))
                    {
                        expositorBO.FotoDocente = Expositor.FotoDocente;
                        expositorBO.UrlFotoDocente = "https://repositorioweb.blob.core.windows.net/repositorioweb/img/docentes/" + Expositor.UrlFotoDocente.Replace(" ", "%20");
                    }
                    

                    _repExpositor.Insert(expositorBO);

                    var InsertarClaPersona = _persona.InsertarPersona(expositorBO.Id, Aplicacion.Base.Enums.Enums.TipoPersona.Docente, Expositor.Usuario);

                    if (InsertarClaPersona != null)
                    {
                        return Ok(expositorBO);
                    }
                    else
                    {
                        var nombreTablaV3 = "tPLA_expositor";
                        var nombreTablaV4 = "pla.T_Expositor";
                        var resultado = _repExpositor.EliminarFisicaExpositor(nombreTablaV3, nombreTablaV4, expositorBO.Id, null, null);
                        if (resultado == true)
                        {
                            throw new Exception("Se elimino el docente");
                        }
                        else
                        {
                            throw new Exception("No se elimino docente");
                        }
                        //throw new Exception("ocurrio un error NO se pudo Insertar el docente");
                    }
                }
                else
                {
                    throw new Exception("El correo" + Expositor.Email1 + "es repetido");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult Actualizar([FromBody] ExpositorDTO Expositor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ExpositorRepositorio _repExpositor = new ExpositorRepositorio(_integraDBContext);
                byte[] dataHojaVida = Convert.FromBase64String(Expositor.HojaVidaResumidaPerfil);
                var decodedHojaVida = Encoding.UTF8.GetString(dataHojaVida);

                ExpositorBO expositorBO = _repExpositor.FirstById(Expositor.Id);
				expositorBO.IdTipoDocumento = Expositor.IdTipoDocumento;
				expositorBO.NroDocumento = Expositor.NroDocumento;
				expositorBO.PrimerNombre = Expositor.PrimerNombre;
				expositorBO.SegundoNombre = Expositor.SegundoNombre;
				expositorBO.ApellidoPaterno = Expositor.ApellidoPaterno;
				expositorBO.ApellidoMaterno = Expositor.ApellidoMaterno;
				expositorBO.FechaNacimiento = Expositor.FechaNacimiento;
				expositorBO.IdPaisProcedencia = Expositor.IdPaisProcedencia;
				expositorBO.IdCiudadProcedencia = Expositor.IdCiudadProcedencia;
				expositorBO.IdReferidoPor = Expositor.IdReferidoPor;
				expositorBO.TelfCelular1 = Expositor.TelfCelular1;
				expositorBO.TelfCelular2 = Expositor.TelfCelular2;
				expositorBO.TelfCelular3 = Expositor.TelfCelular3;
				expositorBO.Email1 = Expositor.Email1;
				expositorBO.Email2 = Expositor.Email2;
				expositorBO.Email3 = Expositor.Email3;
				expositorBO.Domicilio = Expositor.Domicilio;
				expositorBO.IdPaisDomicilio = Expositor.IdPaisDomicilio;
				expositorBO.IdCiudadDomicilio = Expositor.IdCiudadDomicilio;
				expositorBO.LugarTrabajo = Expositor.LugarTrabajo;
				expositorBO.IdPaisLugarTrabajo = Expositor.IdPaisLugarTrabajo;
				expositorBO.IdCiudadLugarTrabajo = Expositor.IdCiudadLugarTrabajo;
				expositorBO.AsistenteNombre = Expositor.AsistenteNombre;
				expositorBO.AsistenteTelefono = Expositor.AsistenteTelefono;
				expositorBO.AsistenteCelular = Expositor.AsistenteCelular;
				expositorBO.HojaVidaResumidaPerfil = decodedHojaVida;
				expositorBO.HojaVidaResumidaSpeech = Expositor.HojaVidaResumidaSpeech;
				expositorBO.FormacionAcademica = Expositor.FormacionAcademica;
				expositorBO.ExperienciaProfesional = Expositor.ExperienciaProfesional;
				expositorBO.Publicaciones = Expositor.Publicaciones;
				expositorBO.PremiosDistinciones = Expositor.PremiosDistinciones;
				expositorBO.OtraInformacion = Expositor.OtraInformacion;
				expositorBO.Estado = true;
				expositorBO.UsuarioModificacion = Expositor.Usuario;
				expositorBO.FechaModificacion = DateTime.Now;
                expositorBO.IdPersonalAsignado = Expositor.IdPersonalAsignado;

                if (!string.IsNullOrEmpty(Expositor.FotoDocente))
                {
                    expositorBO.FotoDocente = Expositor.FotoDocente;
                    expositorBO.UrlFotoDocente = "https://repositorioweb.blob.core.windows.net/repositorioweb/img/docentes/" + Expositor.UrlFotoDocente.Replace(" ", "%20");
                }

                _repExpositor.Update(expositorBO);

				return Ok(expositorBO);
                
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult registrarArchivoFotoExpositor([FromForm] IFormFile Files)
        {
            try
            {
                string respuesta = string.Empty;

                ExpositorRepositorio _repExpositor = new ExpositorRepositorio(_integraDBContext);

                using (var ms = new MemoryStream())
                {
                    Files.CopyTo(ms);
                    var fileBytes = ms.ToArray();
                    respuesta = _repExpositor.guardarArchivos(fileBytes, Files.ContentType, Files.FileName);
                }

                if (string.IsNullOrEmpty(respuesta))
                {
                    return Ok(new { Resultado = "Error" });
                }
                else
                {
                    return Ok(new { Resultado = "Ok", UrlArchivo = respuesta, NombreArchivo = Files.FileName });
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult Eliminar([FromBody]  ExpositorDTO Expositor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    ExpositorRepositorio _repExpositor = new ExpositorRepositorio();

                    if (_repExpositor.Exist(Expositor.Id))
                    {
                        _repExpositor.Delete(Expositor.Id, Expositor.Usuario);
                        scope.Complete();
                        return Ok(true);
                    }
                    else
                    {
                        return BadRequest("Registro no existente");
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerTodoTipoDocumento()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                TipoDocumentoRepositorio _repTipoDocumento = new TipoDocumentoRepositorio();
                return Ok(_repTipoDocumento.ObtenerTodoTipoDocumento());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerTodoPais()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                PaisRepositorio _repPais = new PaisRepositorio();
                return Ok(_repPais.ObtenerTodoPaises());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerTodoCoordinador()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                CoordinadoraRepositorio _repCoordinadora = new CoordinadoraRepositorio();
                return Ok(_repCoordinadora.ObtenerTodoCoordinadoresDocentes());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerTodoCiudad()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                CiudadRepositorio _repCiudad = new CiudadRepositorio();
                return Ok(_repCiudad.ObtenerTodoCiudades());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerTodoExpositor()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ExpositorRepositorio _repExpositor = new ExpositorRepositorio();
                return Ok(_repExpositor.ObtenerExpositoresFiltro());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerTodoPaisCiudad()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                PaisRepositorio _repPais = new PaisRepositorio();
                CiudadRepositorio _repCiudad = new CiudadRepositorio();

                var listaPais = _repPais.ObtenerListaPais();
                var listaCiudad = _repCiudad.ObtenerCiudadFiltro();

                return Ok(new { listaPais, listaCiudad });
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


    }
}
