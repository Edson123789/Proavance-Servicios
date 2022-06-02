using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/ProgramaGeneralMaterialEstudioAdicional")]
    [ApiController]
    public class ProgramaGeneralMaterialEstudioAdicionalController : Controller
    {
        private readonly integraDBContext _integraDBContext;

        public ProgramaGeneralMaterialEstudioAdicionalController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
        }

        [Route("[action]")]
        [HttpPost]
        public IActionResult ObtenerListaRegistroMaterialPrograma()
        {
            try
            {
                ProgramaGeneralMaterialEstudioAdicionalRepositorio _repfiltros = new ProgramaGeneralMaterialEstudioAdicionalRepositorio();
                var _respuesta = _repfiltros.ListaProgramaGeneralMaterialEstudioAdicional();

                return Ok(_respuesta);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]/{IdPGeneral}")]
        [HttpPost]
        public IActionResult ListaRegistroProgramaGeneralMaterialEstudioAdicional(int IdPGeneral)
        {
            try
            {
                ProgramaGeneralMaterialEstudioAdicionalRepositorio _repfiltros = new ProgramaGeneralMaterialEstudioAdicionalRepositorio();
                ProgramaGeneralMaterialEstudioAdicionalEspecificosRepositorio _repLista = new ProgramaGeneralMaterialEstudioAdicionalEspecificosRepositorio();

                datosMaterialAdicionalBO _registro = new datosMaterialAdicionalBO();
                var _respuesta = _repfiltros.RegistroProgramaGeneralMaterialEstudioAdicional(IdPGeneral);
                var _lista = _repLista.RegistroProgramaGeneralMaterialEstudioAdicional(IdPGeneral);

                if (_respuesta != null )
                {
                    _registro.MaterialAdicional = _respuesta;
                }
                if (_lista != null && _lista.Count > 0)
                {
                    _registro.listaEspecifico = _lista.Select(x=>x.IdPEspecifico).ToList();
                }

                return Ok(_registro);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult InsertarProgramaGeneralMaterialEstudioAdicional([FromBody] MaterialEstudioAdicionalDTO objeto)
        {
            try
            {
                ProgramaGeneralMaterialEstudioAdicionalRepositorio _repfiltros = new ProgramaGeneralMaterialEstudioAdicionalRepositorio();
                ProgramaGeneralMaterialEstudioAdicionalEspecificosRepositorio _repLista = new ProgramaGeneralMaterialEstudioAdicionalEspecificosRepositorio();

                var _nuevos = objeto.MaterialRegistro.Where(x => x.Id == 0).ToList();
                if (_nuevos != null && _nuevos.Count > 0)
                {
                    foreach (var itemnuevo in _nuevos)
                    {
                        ProgramaGeneralMaterialEstudioAdicionalBO _reg = new ProgramaGeneralMaterialEstudioAdicionalBO();
                        _reg.IdPGeneral = itemnuevo.IdPGeneral;
                        _reg.NombreArchivo = itemnuevo.NombreArchivo;
                        _reg.EnlaceArchivo = itemnuevo.EnlaceArchivo;
                        _reg.EsEnlace = itemnuevo.EsEnlace;
                        _reg.Estado = true;
                        _reg.UsuarioCreacion = objeto.Usuario;
                        _reg.UsuarioModificacion = objeto.Usuario;
                        _reg.FechaCreacion = DateTime.Now;
                        _reg.FechaModificacion = DateTime.Now;

                        _repfiltros.Insert(_reg);
                    }
                }
                

                var _editados = objeto.MaterialRegistro.Where(x => x.Id != 0).ToList();
                if (_editados != null && _editados.Count > 0)
                {
                    foreach (var itemeditar in _editados)
                    {
                        var _buqueda = _repfiltros.FirstById(itemeditar.Id);
                        if (_buqueda != null)
                        {
                            _buqueda.IdPGeneral = itemeditar.IdPGeneral;
                            _buqueda.NombreArchivo = itemeditar.NombreArchivo;
                            _buqueda.EnlaceArchivo = itemeditar.EnlaceArchivo;
                            _buqueda.EsEnlace = itemeditar.EsEnlace;
                            _buqueda.UsuarioModificacion = objeto.Usuario;
                            _buqueda.FechaModificacion = DateTime.Now;

                            _repfiltros.Update(_buqueda);
                        }
                    }
                }

                if (objeto.MaterialEliminado != null)
                {
                    foreach (var item in objeto.MaterialEliminado)
                    {
                        var _rpta = _repfiltros.Delete(item.Id, objeto.Usuario);
                    }
                }

                if (objeto.ProgramaEspecifico != null && objeto.ProgramaEspecifico.Count > 0)
                {
                    var _registrados = _repLista.GetBy(x => x.MaterialEstudioAdicionalPorPgeneralId == objeto.IdPGeneral).ToList();
                    foreach (var item in _registrados)
                    {
                        var _rpta = _repfiltros.Delete(item.Id, objeto.Usuario);
                    }

                    foreach (var itemnuevo in objeto.ProgramaEspecifico)
                    {
                        ProgramaGeneralMaterialEstudioAdicionalEspecificosBO _reg = new ProgramaGeneralMaterialEstudioAdicionalEspecificosBO();
                        _reg.MaterialEstudioAdicionalPorPgeneralId = objeto.IdPGeneral;
                        _reg.IdPEspecifico = itemnuevo;
                        _reg.Estado = true;
                        _reg.UsuarioCreacion = objeto.Usuario;
                        _reg.UsuarioModificacion = objeto.Usuario;
                        _reg.FechaCreacion = DateTime.Now;
                        _reg.FechaModificacion = DateTime.Now;

                        _repLista.Insert(_reg);
                    }
                }

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult EliminarRegistro([FromBody] EliminarMaterialEstudioAdicionalDTO objeto)
        {
            try
            {
                ProgramaGeneralMaterialEstudioAdicionalRepositorio _repfiltros = new ProgramaGeneralMaterialEstudioAdicionalRepositorio();

                var _rpta = _repfiltros.EliminarProgramaGeneralMaterialEstudioAdicional(objeto.IdPGeneral, objeto.Usuario, DateTime.Now);

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
