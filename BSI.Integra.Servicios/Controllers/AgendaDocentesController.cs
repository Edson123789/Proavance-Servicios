using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/AgendaDocentes")]
    [ApiController]
    public class AgendaDocentesController : ControllerBase
    {
        private readonly integraDBContext _integraDBContext;
        public AgendaDocentesController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
        }

        [Route("[Action]/{IdClasificacionPersona}")]
        [HttpGet]
        public ActionResult ObtenerDatosDocente(int IdClasificacionPersona)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ExpositorRepositorio expositorRepositorio = new ExpositorRepositorio(_integraDBContext);
                var expositor = expositorRepositorio.ObtenerExpositorPorClasificacionPersona(IdClasificacionPersona);

                return Ok(expositor);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]/{IdClasificacionPersona}")]
        [HttpGet]
        public ActionResult ObtenerDatosProveedor(int IdClasificacionPersona)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ExpositorRepositorio expositorRepositorio = new ExpositorRepositorio(_integraDBContext);
                var expositor = expositorRepositorio.ObtenerProveedorPorClasificacionPersona(IdClasificacionPersona);

                return Ok(expositor);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult ActualizarExpositor([FromBody] ExpositorAgendaDTO Expositor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var _repExpositor = new ExpositorRepositorio(_integraDBContext);
                var _repExpositorLog = new ExpositorLogRepositorio(_integraDBContext);

                var listaExpositorLog = new List<ExpositorLogBO>();

                ExpositorBO expositorBO = _repExpositor.FirstById(Expositor.Id);

                if (expositorBO.PrimerNombre != Expositor.PrimerNombre) listaExpositorLog.Add(new ExpositorLogBO(expositorBO.Id, "Primer nombre", expositorBO.PrimerNombre ?? "", Expositor.PrimerNombre ?? "", Expositor.Usuario));
                if (expositorBO.SegundoNombre != Expositor.SegundoNombre && (expositorBO.SegundoNombre != null || Expositor.SegundoNombre != "")) listaExpositorLog.Add(new ExpositorLogBO(expositorBO.Id, "Segundo nombre", expositorBO.SegundoNombre ?? "", Expositor.SegundoNombre ?? "", Expositor.Usuario));
                if (expositorBO.ApellidoPaterno != Expositor.ApellidoPaterno) listaExpositorLog.Add(new ExpositorLogBO(expositorBO.Id, "Apellido Paterno", expositorBO.ApellidoPaterno ?? "", expositorBO.ApellidoPaterno ?? "", Expositor.Usuario));
                if (expositorBO.ApellidoMaterno != Expositor.ApellidoMaterno && (expositorBO.ApellidoMaterno != null || Expositor.ApellidoMaterno != "")) listaExpositorLog.Add(new ExpositorLogBO(expositorBO.Id, "Apellido Materno", expositorBO.ApellidoMaterno ?? "", Expositor.ApellidoMaterno ?? "", Expositor.Usuario));
                if (expositorBO.TelfCelular1 != Expositor.TelfCelular1 && (expositorBO.TelfCelular1 != null || Expositor.TelfCelular1 != "")) listaExpositorLog.Add(new ExpositorLogBO(expositorBO.Id, "Telefono celular 1", expositorBO.TelfCelular1 ?? "", Expositor.TelfCelular1 ?? "", Expositor.Usuario));
                if (expositorBO.TelfCelular2 != Expositor.TelfCelular2 && (expositorBO.TelfCelular2 != null || Expositor.TelfCelular2 != "")) listaExpositorLog.Add(new ExpositorLogBO(expositorBO.Id, "Telefono celular 2", expositorBO.TelfCelular2 ?? "", Expositor.TelfCelular2 ?? "", Expositor.Usuario));
                if (expositorBO.Email2 != Expositor.Email2 && (expositorBO.Email2 != null || Expositor.Email2 != "")) listaExpositorLog.Add(new ExpositorLogBO(expositorBO.Id, "Email 2", expositorBO.Email2 ?? "", Expositor.Email2 ?? "", Expositor.Usuario));
                if (expositorBO.NroDocumento != Expositor.NroDocumento && (expositorBO.NroDocumento != null || Expositor.NroDocumento != "")) listaExpositorLog.Add(new ExpositorLogBO(expositorBO.Id, "Nro documento", expositorBO.NroDocumento ?? "", Expositor.NroDocumento ?? "", Expositor.Usuario));
                if (expositorBO.Domicilio != Expositor.Domicilio && (expositorBO.Domicilio != null || Expositor.Domicilio != "")) listaExpositorLog.Add(new ExpositorLogBO(expositorBO.Id, "Domicilio", expositorBO.Domicilio ?? "", Expositor.Domicilio ?? "", Expositor.Usuario));
                //if (expositorBO.IdPaisDomicilio != Expositor.IdPaisDomicilio) listaExpositorLog.Add(new AlumnoLogBO(expositorBO.Id, "Id pais domicilio", expositorBO.IdPaisDomicilio ?? "", Expositor.IdPaisDomicilio ?? "", Expositor.Usuario));
                //if (expositorBO.IdCiudadDomicilio != Expositor.IdCiudadDomicilio && expositorBO.IdCiudadDomicilio != Expositor.IdCiudadDomicilio) listaExpositorLog.Add(new AlumnoLogBO(expositorBO.Id, "Id ciudad domicilio", expositorBO.IdCiudadDomicilio ?? "", Expositor.IdCiudadDomicilio ?? "", Expositor.Usuario));

                expositorBO.PrimerNombre = Expositor.PrimerNombre;
                expositorBO.SegundoNombre = Expositor.SegundoNombre;
                expositorBO.ApellidoPaterno = Expositor.ApellidoPaterno;
                expositorBO.ApellidoMaterno = Expositor.ApellidoMaterno;
                expositorBO.TelfCelular1 = Expositor.TelfCelular1;
                expositorBO.TelfCelular2 = Expositor.TelfCelular2;
                expositorBO.Email2 = Expositor.Email2;
                expositorBO.NroDocumento = Expositor.NroDocumento;

                expositorBO.Domicilio = Expositor.Domicilio;
                expositorBO.IdPaisDomicilio = Expositor.IdPaisDomicilio;
                expositorBO.IdCiudadDomicilio = Expositor.IdCiudadDomicilio;
                expositorBO.UsuarioModificacion = Expositor.Usuario;
                expositorBO.FechaModificacion = DateTime.Now;
              
                if (!expositorBO.HasErrors)
                {
                    _repExpositor.Update(expositorBO);
                    _repExpositorLog.Insert(listaExpositorLog);
                }
                else
                {
                    return BadRequest(expositorBO.GetErrors());
                }
                return Ok(expositorBO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult ActualizarProveedor([FromBody] ProveedorAgendaDTO Expositor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var _repProveedor = new ProveedorRepositorio(_integraDBContext);
                var _repProveedorLog = new ProveedorLogRepositorio(_integraDBContext);

                var listaExpositorLog = new List<ProveedorLogBO>();

                ProveedorBO proveedorBO = _repProveedor.FirstById(Expositor.Id);

                if (proveedorBO.Nombre1 != Expositor.PrimerNombre)
                    listaExpositorLog.Add(new ProveedorLogBO(proveedorBO.Id, "Primer nombre", proveedorBO.Nombre1 ?? "",
                        Expositor.PrimerNombre ?? "", Expositor.Usuario));
                if (proveedorBO.Nombre2 != Expositor.SegundoNombre &&
                    (proveedorBO.Nombre2 != null || Expositor.SegundoNombre != ""))
                    listaExpositorLog.Add(new ProveedorLogBO(proveedorBO.Id, "Segundo nombre",
                        proveedorBO.Nombre2 ?? "", Expositor.SegundoNombre ?? "", Expositor.Usuario));
                if (proveedorBO.ApePaterno != Expositor.ApellidoPaterno)
                    listaExpositorLog.Add(new ProveedorLogBO(proveedorBO.Id, "Apellido Paterno",
                        proveedorBO.ApePaterno ?? "", proveedorBO.ApePaterno ?? "", Expositor.Usuario));
                if (proveedorBO.ApeMaterno != Expositor.ApellidoMaterno &&
                    (proveedorBO.ApeMaterno != null || Expositor.ApellidoMaterno != ""))
                    listaExpositorLog.Add(new ProveedorLogBO(proveedorBO.Id, "Apellido Materno",
                        proveedorBO.ApeMaterno ?? "", Expositor.ApellidoMaterno ?? "", Expositor.Usuario));
                if (proveedorBO.Celular1 != Expositor.TelfCelular1 &&
                    (proveedorBO.Celular1 != null || Expositor.TelfCelular1 != ""))
                    listaExpositorLog.Add(new ProveedorLogBO(proveedorBO.Id, "Telefono celular 1",
                        proveedorBO.Celular1 ?? "", Expositor.TelfCelular1 ?? "", Expositor.Usuario));
                if (proveedorBO.Celular2 != Expositor.TelfCelular2 &&
                    (proveedorBO.Celular2 != null || Expositor.TelfCelular2 != ""))
                    listaExpositorLog.Add(new ProveedorLogBO(proveedorBO.Id, "Telefono celular 2",
                        proveedorBO.Celular2 ?? "", Expositor.TelfCelular2 ?? "", Expositor.Usuario));
                //if (proveedorBO.Email2 != Expositor.Email2 && (proveedorBO.Email2 != null || Expositor.Email2 != "")) listaExpositorLog.Add(new ProveedorLogBO(proveedorBO.Id, "Email 2", proveedorBO.Email2 ?? "", Expositor.Email2 ?? "", Expositor.Usuario));
                if (proveedorBO.NroDocIdentidad != Expositor.NroDocumento &&
                    (proveedorBO.NroDocIdentidad != null || Expositor.NroDocumento != ""))
                    listaExpositorLog.Add(new ProveedorLogBO(proveedorBO.Id, "Nro documento",
                        proveedorBO.NroDocIdentidad ?? "", Expositor.NroDocumento ?? "", Expositor.Usuario));
                if (proveedorBO.Direccion != Expositor.Domicilio &&
                    (proveedorBO.Direccion != null || Expositor.Domicilio != ""))
                    listaExpositorLog.Add(new ProveedorLogBO(proveedorBO.Id, "Domicilio", proveedorBO.Direccion ?? "",
                        Expositor.Domicilio ?? "", Expositor.Usuario));
                //if (expositorBO.IdPaisDomicilio != Expositor.IdPaisDomicilio) listaExpositorLog.Add(new AlumnoLogBO(expositorBO.Id, "Id pais domicilio", expositorBO.IdPaisDomicilio ?? "", Expositor.IdPaisDomicilio ?? "", Expositor.Usuario));
                if (proveedorBO.IdCiudad != Expositor.IdCiudadDomicilio &&
                    proveedorBO.IdCiudad != Expositor.IdCiudadDomicilio)
                    listaExpositorLog.Add(new ProveedorLogBO(proveedorBO.Id, "Id ciudad domicilio",
                        proveedorBO.IdCiudad == null ? "" : proveedorBO.IdCiudad.ToString(),
                        Expositor.IdCiudadDomicilio == null ? "" : Expositor.IdCiudadDomicilio.ToString(),
                        Expositor.Usuario));
                if (proveedorBO.RazonSocial != Expositor.RazonSocial)
                    listaExpositorLog.Add(new ProveedorLogBO(proveedorBO.Id, "Primer nombre", proveedorBO.RazonSocial ?? "",
                        Expositor.RazonSocial ?? "", Expositor.Usuario));

                proveedorBO.Nombre1 = Expositor.PrimerNombre;
                proveedorBO.Nombre2 = Expositor.SegundoNombre;
                proveedorBO.ApePaterno = Expositor.ApellidoPaterno;
                proveedorBO.ApeMaterno = Expositor.ApellidoMaterno;
                proveedorBO.Celular1 = Expositor.TelfCelular1;
                proveedorBO.Celular2 = Expositor.TelfCelular2;
                //proveedorBO.Email2 = Expositor.Email2;
                proveedorBO.NroDocIdentidad = Expositor.NroDocumento;

                proveedorBO.Direccion = Expositor.Domicilio;
                proveedorBO.IdCiudad = Expositor.IdCiudadDomicilio;
                proveedorBO.RazonSocial = string.IsNullOrEmpty(Expositor.RazonSocial) ? "" : Expositor.RazonSocial;

                proveedorBO.UsuarioModificacion = Expositor.Usuario;
                proveedorBO.FechaModificacion = DateTime.Now;

                if (!proveedorBO.HasErrors)
                {
                    _repProveedor.Update(proveedorBO);
                    _repProveedorLog.Insert(listaExpositorLog);
                }
                else
                {
                    return BadRequest(proveedorBO.GetErrors());
                }

                return Ok(proveedorBO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[Action]/{IdOportunidad}")]
        [HttpGet]
        public ActionResult ObtenerHistorialParticipacion(int IdOportunidad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                PespecificoRepositorio pespecificoRepositorio = new PespecificoRepositorio(_integraDBContext);
                var Todos = pespecificoRepositorio.ObtenerHistorialParticipacion(IdOportunidad);
                var Online = Todos.FindAll(x => x.Modalidad == "Online Sincronica");
                var AOnline = Todos.FindAll(x => x.Modalidad == "Online Asincronica");
                var Presencial = Todos.FindAll(x => x.Modalidad == "Presencial");

                return Ok(new { Todos, Online, AOnline, Presencial });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        [Route("[Action]/{IdClasificacionPersona}")]
        [HttpGet]
        public ActionResult ObtenerHistorialParticipacionV2(int IdClasificacionPersona)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                PespecificoRepositorio pespecificoRepositorio = new PespecificoRepositorio(_integraDBContext);
                var Todos = pespecificoRepositorio.ObtenerHistorialParticipacionV2(IdClasificacionPersona);
                var Online = Todos.FindAll(x => x.Modalidad == "Online Sincronica");
                var AOnline = Todos.FindAll(x => x.Modalidad == "Online Asincronica");
                var Presencial = Todos.FindAll(x => x.Modalidad == "Presencial");

                return Ok(new {Todos, Online, AOnline, Presencial});
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]/{IdClasificacionPersona}")]
        [HttpGet]
        public ActionResult ObtenerHistorialParticipacionV3(int IdClasificacionPersona)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                PespecificoRepositorio pespecificoRepositorio = new PespecificoRepositorio(_integraDBContext);
                var Todos = pespecificoRepositorio.ObtenerHistorialParticipacionV3(IdClasificacionPersona);
                var Online = Todos.FindAll(x => x.ModalidadPrograma == "Online Sincronica");
                var AOnline = Todos.FindAll(x => x.ModalidadPrograma == "Online Asincronica");
                var Presencial = Todos.FindAll(x => x.ModalidadPrograma == "Presencial");

                return Ok(new { Todos, Online, AOnline, Presencial });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]/{idExpositor}")]
        [HttpGet]
        public ActionResult ObtenerHistorialParticipacionV3PorExpositorPortal(int idExpositor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                PespecificoRepositorio pespecificoRepositorio = new PespecificoRepositorio(_integraDBContext);
                var listado = pespecificoRepositorio.ObtenerHistorialParticipacionV3PorExpositorPortal(idExpositor);
                
                return Ok(listado);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]/{idProveedor}")]
        [HttpGet]
        public ActionResult ObtenerHistorialParticipacionV3PorProveedorPortal(int idProveedor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                PespecificoRepositorio pespecificoRepositorio = new PespecificoRepositorio(_integraDBContext);
                var listado = pespecificoRepositorio.ObtenerHistorialParticipacionV3PorProveedorPortal(idProveedor);

                return Ok(listado);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]/{idProveedor}")]
        [HttpGet]
        public ActionResult ObtenerHistorialParticipacionV3PorProveedorPortalAonline(int idProveedor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                PespecificoRepositorio pespecificoRepositorio = new PespecificoRepositorio(_integraDBContext);
                var listado = pespecificoRepositorio.ObtenerHistorialParticipacionV3PorProveedorPortalAonline(idProveedor);

                return Ok(listado);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]/{Usuario}/{IdPEspecifico}/{IdExpositor}/{estado}")]
        [HttpGet]
        public ActionResult RegistrarConfirmacionParticipacion(string Usuario, int IdPEspecifico, int IdExpositor, bool estado)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                PespecificoParticipacionDocenteRepositorio participacionRepositorio = new PespecificoParticipacionDocenteRepositorio(_integraDBContext);
                if (participacionRepositorio.Exist(w => w.IdExpositor == IdExpositor && w.IdPespecifico == IdPEspecifico))
                {
                    var bo = participacionRepositorio.FirstBy(w =>
                        w.IdExpositor == IdExpositor && w.IdPespecifico == IdPEspecifico);

                    bo.ConfirmaParticipacion = estado;
                    if(estado)
                        bo.FechaConfirmacion=DateTime.Now;

                    bo.UsuarioCreacion = Usuario;
                    bo.FechaModificacion = DateTime.Now;

                    participacionRepositorio.Update(bo);
                }
                else
                {
                    PespecificoParticipacionDocenteBO bo = new PespecificoParticipacionDocenteBO()
                    {
                        IdExpositor = IdExpositor,
                        IdPespecifico = IdPEspecifico,
                        ConfirmaParticipacion = estado,
                        FechaConfirmacion = estado ? DateTime.Now : (DateTime?) null,
                        Estado = true,
                        UsuarioCreacion = Usuario,
                        UsuarioModificacion = Usuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now
                    };

                    participacionRepositorio.Insert(bo);
                }

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]/{IdClasificacionPersona}")]
        [HttpGet]
        public ActionResult ObtenerHistorialProyectoAplicacion(int IdClasificacionPersona)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                PgeneralProyectoAplicacionRepositorio _repoProyecto = new PgeneralProyectoAplicacionRepositorio();
                var listado = _repoProyecto.ListadoConsolidado_ProyectosRegistrados(IdClasificacionPersona);

                return Ok(listado);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}