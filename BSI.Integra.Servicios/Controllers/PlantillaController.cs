using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.GestionPersonas.Repositorio;
using BSI.Integra.Aplicacion.Marketing.BO;
using BSI.Integra.Aplicacion.Marketing.Repositorio;
using BSI.Integra.Aplicacion.Planificacion.Repositorio;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Aplicacion.Transversal.DTO;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.SCode.IRepository;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/Plantilla")]
    public class PlantillaController: Controller
    {
        private readonly integraDBContext _integraDBContext;
        private readonly PlantillaRepositorio _repPlantilla;
        private readonly PlantillaBaseRepositorio _repPlantillaBase;
        private readonly EtiquetaRepositorio _repEtiqueta;
        private readonly PlantillaClaveValorRepositorio _repPlantillaClaveValor;
        private readonly FaseOportunidadRepositorio _repFaseOportunidad;
        private readonly FaseByPlantillaRepositorio _repFaseByPlantilla;
        private readonly PlantillaAsociacionModuloSistemaRepositorio _repPlantillaAsociacionModuloSistema;

        public PlantillaController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
            _repPlantilla = new PlantillaRepositorio(_integraDBContext);
            _repPlantillaBase = new PlantillaBaseRepositorio(_integraDBContext);
            _repEtiqueta = new EtiquetaRepositorio(_integraDBContext);
            _repPlantillaClaveValor = new PlantillaClaveValorRepositorio(_integraDBContext);
            _repFaseOportunidad = new FaseOportunidadRepositorio(_integraDBContext);
            _repFaseByPlantilla = new FaseByPlantillaRepositorio(_integraDBContext);
            _repPlantillaAsociacionModuloSistema = new PlantillaAsociacionModuloSistemaRepositorio(_integraDBContext);
        }

        [Route("[action]")]
        [HttpGet]
        public IActionResult ObtenerPlantillaPanel()
        {
            try
            {
                var listaPlantilla = _repPlantilla.ObtenerListarPlantilla();
                var listaPlantillaAsociacionModuloSistema = _repPlantillaAsociacionModuloSistema.ObtenerPorPlantlla(listaPlantilla.Select(x => x.Id).ToList());

                foreach (var item in listaPlantilla)
                {
                    //item.ListaPlantillaAsociacionModuloSistema = _repPlantillaAsociacionModuloSistema.ObtenerPorPlantlla(item.Id);
                    item.ListaPlantillaAsociacionModuloSistema = listaPlantillaAsociacionModuloSistema.Where(x => x.IdPlantilla == item.Id).ToList();
                }
                return Ok(listaPlantilla);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpGet]
        public IActionResult ObtenerCombosPlantilla()
        {
            try
            {
                return Ok(_repPlantillaBase.ObtenerPlantillaBase());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]/{id}")]
        [HttpGet]
        public IActionResult ObtenerBasePlantilla(int id)
        {
            try
            {
                return Ok(_repPlantillaBase.ObtenerPlantillaBasePorId(id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [Route("[action]")]
        [HttpPost]
        public IActionResult ObtenerCategorias([FromBody]CategoriasPlantillaDTO Json)
        {
            try
            {
                List <EtiquetaDTO> categorias = new List<EtiquetaDTO>();
                int padre = 1;

                if (!(Json.Id == 0))
                {
                    categorias = _repEtiqueta.ObtenerCategoriasPorIdPadre(Json.Id);
                }
                else
                    {
                    categorias = _repEtiqueta.ObtenerCategoriasPorIdPadre(padre);
                }

                return Ok(categorias);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]/{id}")]
        [HttpGet]
        public IActionResult ObtenerClaveValor(int id)
        {
            try
            {
                return Ok(_repPlantillaClaveValor.ObtenerClaveValorPorPlantilla(id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpGet]
        public IActionResult ObtenerTodasFasesOportunidad()
        {
            try
            {
                List<FaseOportunidadDTO> listaFases = new List<FaseOportunidadDTO>();
                listaFases = _repFaseOportunidad.ObtenerFasesOportunidad();
                listaFases.Add( new FaseOportunidadDTO(){ Id = 0, Codigo = "Todas las fases de oportunidad ..."  });
                return Ok(listaFases);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]/{id}")]
        [HttpGet]
        public IActionResult ObtenerFasesPorPlantilla(int id)
        {
            try
            {
                return Ok(_repFaseByPlantilla.ObtenerFasesPlantilla(id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]

        public ActionResult Insertar([FromBody]CompuestoPlantillaDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                using (TransactionScope scope = new TransactionScope())
                {
                    PlantillaBO plantilla = new PlantillaBO
                    {
                        Id = Json.DatosPlantilla.Id,
                        Nombre = Json.DatosPlantilla.Nombre,
                        Descripcion = Json.DatosPlantilla.Descripcion,
                        IdPlantillaBase = Json.DatosPlantilla.IdPlantillaBase,
                        EstadoAgenda = Json.DatosPlantilla.EstadoAgenda,
                        Documento = Json.DatosPlantilla.Documento,
                        UsuarioCreacion = Json.Usuario,
                        UsuarioModificacion = Json.Usuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        Estado = true,
                        IdPersonalAreaTrabajo = Json.DatosPlantilla.IdPersonalAreaTrabajo,
                    };

                    foreach (var item in Json.PlantillaClaveValor)
                    {
                        byte[] _base64 = Convert.FromBase64String(item.Valor);
                        var _valor = Encoding.UTF8.GetString(_base64);
                        var plantillaClaveValor = new PlantillaClaveValorBO
                        {
                            Clave = item.Clave,
                            Valor = _valor,
                            UsuarioCreacion = Json.Usuario,
                            UsuarioModificacion = Json.Usuario,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            Estado = true
                        };
                        plantilla.PlantillaClaveValor.Add(plantillaClaveValor);
                    }

                    foreach (var item in Json.FasesPlantilla)
                    {
                        var fasePlantilla = new FaseByPlantillaBO
                        {
                            idFaseOrigen = item,
                            NombreFase = " ",
                            UsuarioCreacion = Json.Usuario,
                            UsuarioModificacion = Json.Usuario,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            Estado = true
                        };
                        plantilla.FaseByPlantilla.Add(fasePlantilla);
                    }

                    foreach (var item in Json.ListaPlantillaAsociacionModuloSistema)
                    {
                        var plantillaAsociacionModuloSistema = new PlantillaAsociacionModuloSistemaBO
                        {
                            IdModuloSistema = item.IdModuloSistema,
                            UsuarioCreacion = Json.Usuario,
                            UsuarioModificacion = Json.Usuario,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            Estado = true
                        };
                        plantilla.ListaPlantillaAsociacionModuloSistema.Add(plantillaAsociacionModuloSistema);
                    }
                    _repPlantilla.Insert(plantilla);
                    scope.Complete();
                    return Ok(plantilla);
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult Actualizar([FromBody]CompuestoPlantillaDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                using (TransactionScope scope = new TransactionScope())
                {
                    if (!_repPlantilla.Exist(Json.DatosPlantilla.Id))
                    {
                        return BadRequest("Registro no existente");
                    }
                    else { 
                        _repPlantillaClaveValor.EliminacionLogicoPorPlantilla(Json.DatosPlantilla.Id, Json.Usuario, Json.PlantillaClaveValor);
                        _repFaseByPlantilla.EliminacionLogicoPorPlantilla(Json.DatosPlantilla.Id, Json.Usuario, Json.FasesPlantilla);
                        _repPlantillaAsociacionModuloSistema.EliminacionLogicoPorPlantilla(Json.DatosPlantilla.Id, Json.Usuario, Json.ListaPlantillaAsociacionModuloSistema.Select(x => x.IdModuloSistema).ToList());

                        var plantilla = _repPlantilla.FirstById(Json.DatosPlantilla.Id);
                        plantilla.Nombre = Json.DatosPlantilla.Nombre;
                        plantilla.Descripcion = Json.DatosPlantilla.Descripcion;
                        plantilla.IdPlantillaBase = Json.DatosPlantilla.IdPlantillaBase;
                        plantilla.EstadoAgenda = Json.DatosPlantilla.EstadoAgenda;
                        plantilla.Documento = Json.DatosPlantilla.Documento;
                        plantilla.UsuarioModificacion = Json.Usuario;
                        plantilla.FechaModificacion = DateTime.Now;
                        plantilla.Estado = Json.Estado;
                        plantilla.IdPersonalAreaTrabajo = Json.DatosPlantilla.IdPersonalAreaTrabajo;

                        foreach (var item in Json.PlantillaClaveValor)
                        {
                            byte[] _base64 = Convert.FromBase64String(item.Valor);
                            var _valor = Encoding.UTF8.GetString(_base64);
                            PlantillaClaveValorBO plantillaClaveValor;
                            if (_repPlantillaClaveValor.Exist(x => x.Clave == item.Clave && x.IdPlantilla == Json.IdPlantilla))
                            {
                                plantillaClaveValor = _repPlantillaClaveValor.FirstBy(x => x.Clave == item.Clave &&  x.IdPlantilla == Json.IdPlantilla);
                                plantillaClaveValor.Clave = item.Clave;
                                plantillaClaveValor.Valor = _valor;
                                plantillaClaveValor.UsuarioModificacion = Json.Usuario;
                                plantillaClaveValor.FechaModificacion = DateTime.Now;

                            }
                            else
                            {
                                plantillaClaveValor = new PlantillaClaveValorBO
                                {
                                    Clave = item.Clave,
                                    Valor = _valor,
                                    UsuarioCreacion = Json.Usuario,
                                    UsuarioModificacion = Json.Usuario,
                                    FechaCreacion = DateTime.Now,
                                    FechaModificacion = DateTime.Now,
                                    Estado = true
                                };
                            }
                            plantilla.PlantillaClaveValor.Add(plantillaClaveValor);
                        }

                        foreach (var item in Json.FasesPlantilla)
                        {
                            FaseByPlantillaBO fasesPlantilla;
                            if (_repFaseByPlantilla.Exist(x => x.IdFaseOrigen == item && x.IdPlantilla == Json.IdPlantilla))
                            {
                                fasesPlantilla = _repFaseByPlantilla.FirstBy(x => x.IdFaseOrigen == item &&  x.IdPlantilla == Json.IdPlantilla);
                                fasesPlantilla.idFaseOrigen = item;
                                fasesPlantilla.NombreFase = " ";
                                fasesPlantilla.UsuarioModificacion = Json.Usuario;
                                fasesPlantilla.FechaModificacion = DateTime.Now;
                            }
                            else
                            {
                                fasesPlantilla = new FaseByPlantillaBO
                                {
                                    idFaseOrigen = item,
                                    NombreFase = " ",
                                    UsuarioCreacion = Json.Usuario,
                                    UsuarioModificacion = Json.Usuario,
                                    FechaCreacion = DateTime.Now,
                                    FechaModificacion = DateTime.Now,
                                    Estado = true
                                };
                            }
                            plantilla.FaseByPlantilla.Add(fasesPlantilla);
                        }

                        foreach (var item in Json.ListaPlantillaAsociacionModuloSistema)
                        {
                            PlantillaAsociacionModuloSistemaBO plantillaAsociacionModuloSistema;
                            if (_repPlantillaAsociacionModuloSistema.Exist(x => x.IdModuloSistema == item.IdModuloSistema && x.IdPlantilla == Json.IdPlantilla))
                            {
                                plantillaAsociacionModuloSistema = _repPlantillaAsociacionModuloSistema.FirstBy(x => x.IdModuloSistema == item.IdModuloSistema && x.IdPlantilla == Json.IdPlantilla);
                                plantillaAsociacionModuloSistema.IdModuloSistema = item.IdModuloSistema;
                                plantillaAsociacionModuloSistema.UsuarioModificacion = Json.Usuario;
                                plantillaAsociacionModuloSistema.FechaModificacion = DateTime.Now;
                            }
                            else
                            {
                                plantillaAsociacionModuloSistema = new PlantillaAsociacionModuloSistemaBO
                                {
                                    IdModuloSistema = item.IdModuloSistema,
                                    UsuarioCreacion = Json.Usuario,
                                    UsuarioModificacion = Json.Usuario,
                                    FechaCreacion = DateTime.Now,
                                    FechaModificacion = DateTime.Now,
                                    Estado = true
                                };
                            }
                            plantilla.ListaPlantillaAsociacionModuloSistema.Add(plantillaAsociacionModuloSistema);
                        }
                        _repPlantilla.Update(plantilla);
                        scope.Complete();
                    }
                }
                return Ok(true);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]/{Id}/{Usuario}")]
        [HttpGet]
        public ActionResult Eliminar(int Id, string Usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if (!_repPlantilla.Exist(Id))
                {
                    return BadRequest("Registro no existente!");
                }
                else { 
                    _repPlantilla.Delete(Id, Usuario);
                    _repPlantillaClaveValor.Delete(_repPlantillaClaveValor.GetBy(x => x.IdPlantilla == Id).Select(x => x.Id), Usuario);
                    _repFaseByPlantilla.Delete(_repFaseByPlantilla.GetBy(x => x.IdPlantilla == Id).Select(x => x.Id), Usuario);
                    _repPlantillaAsociacionModuloSistema.Delete(_repPlantillaAsociacionModuloSistema.GetBy(x => x.IdPlantilla == Id).Select(x => x.Id), Usuario);
                }
                return Ok(true);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerTodoFiltro() 
        {
            try
            {
                PlantillaRepositorio _repPlantilla = new PlantillaRepositorio();
                return Ok(_repPlantilla.ObtenerTodoFiltro());
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
        [Route("[action]")]
        [HttpGet]
        public ActionResult CalcularEtiquetas()
        {
            try
            {

                PlantillaRepositorio _repPlantilla = new PlantillaRepositorio();
                PlantillaClaveValorRepositorio _repPlantillaClaveValor = new PlantillaClaveValorRepositorio();
                var listaPlantillasDisponibles = _repPlantilla.ObtenerPlantillaCalcularEtiqueta();

                var etiquetas = "";
                foreach (var item in listaPlantillasDisponibles)
                {
                    var listaPlantillaClaveValor = _repPlantilla.ObtenerPlantillaClaveValor(item.Id);

                    foreach (var plantillaClaveValor in listaPlantillaClaveValor)
                    {
                        var listaEtiqueta = plantillaClaveValor.Valor.Split(new string[] { "{" }, StringSplitOptions.None).Where(o => o.Contains("}")).Select(o => o.Split(new string[] { "}" }, StringSplitOptions.None).First()).ToList();

                        var _plantillaClaveValor = _repPlantillaClaveValor.FirstById(plantillaClaveValor.Id);

                        if (listaEtiqueta != null && listaEtiqueta.Count() >= 1)
                        {
                            var lastItem = listaEtiqueta.Last();
                            etiquetas = "";
                            foreach (var etiqueta in listaEtiqueta)
                            {
                                etiquetas += string.Concat("{", etiqueta, "}");

                                if (lastItem != etiqueta)
                                {
                                    etiquetas += ",";
                                }
                            }
                            _plantillaClaveValor.Etiquetas = etiquetas;
                            _repPlantillaClaveValor.Update(_plantillaClaveValor);
                        }
                    }
                  
                }
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
        /// TipoFuncion: GET
        /// Autor: Edgar Serruto
        /// Fecha: 15/07/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene plantillas de mensajes para chat
        /// </summary>
        /// <param name="IdPlantilla">Id de Plantilla</param>
        /// <param name="UsuarioNombre">Nombre de Usuario de Interfaz</param>
        /// <returns> List<ChatDetalleIntegraBO> </returns>
        [Route("[action]/{IdPlantilla}/{UsuarioNombre}")]
        [HttpGet]
        public ActionResult GenerarSpeechChatSoporte(int IdPlantilla, string UsuarioNombre)
        {
            try
            {
                IntegraAspNetUsersRepositorio _repIntegraAspNetUsers = new IntegraAspNetUsersRepositorio();
                var reemplazoEtiquetaPlantilla = new ReemplazoEtiquetaPlantillaBO(_integraDBContext)
                {
                    IdPlantilla = IdPlantilla,
                };
                var emailPersonal = _repIntegraAspNetUsers.GetBy(x => x.UserName == UsuarioNombre).OrderByDescending(x => x.Id).FirstOrDefault();
                reemplazoEtiquetaPlantilla.ReemplazarSpeechChatSoporte(emailPersonal.Email, emailPersonal.PerId);
                var datosMensaje = reemplazoEtiquetaPlantilla.EmailReemplazado;
                return Ok(datosMensaje.CuerpoHTML);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
