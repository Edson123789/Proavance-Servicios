using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Marketing.Repositorio;
using BSI.Integra.Aplicacion.Planificacion.BO;
using BSI.Integra.Aplicacion.Planificacion.Repositorio;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/Etiqueta")]
    [ApiController]
    public class EtiquetaController : ControllerBase
    {

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerTodo()
        {
            try
            {
                EtiquetaRepositorio etiquetaRepositorio = new EtiquetaRepositorio();
                return Ok(etiquetaRepositorio.ObtenerTodoGrid());
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerEtiquetaPadre()
        {
            try
            {
                EtiquetaRepositorio etiquetaRepositorio = new EtiquetaRepositorio();
                return Ok(etiquetaRepositorio.ObtenerEtiquetaPadre());
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerTipoEtiqueta()
        {
            try
            {
                TipoEtiquetaRepositorio _repTipoEtiqueta = new TipoEtiquetaRepositorio();
                return Ok(_repTipoEtiqueta.ObtenerParaFiltro());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerCampoEtiqueta()
        {
            try
            {
                AreaCampoEtiquetaRepositorio areaCampoEtiquetaRepositorio = new AreaCampoEtiquetaRepositorio();
                SubAreaCampoEtiquetaRepositorio subAreaCampoEtiquetaRepositorio = new SubAreaCampoEtiquetaRepositorio();
                CampoEtiquetaRepositorio campoEtiquetaRepositorio = new CampoEtiquetaRepositorio();
                List<AreaCampoEtiquetaDTO> areaCampoEtiquetaDTO = areaCampoEtiquetaRepositorio.ObtenerAreas();

                foreach (var area in areaCampoEtiquetaDTO)
                {
                    area.subAreas = subAreaCampoEtiquetaRepositorio.ObtenerSubAreaPorArea(area.Id);
                    foreach(var subarea in area.subAreas)
                    {
                        subarea.listaCampoEtiqueta = campoEtiquetaRepositorio.ObtenerCampoEtiquetaPorSubArea(subarea.Id);
                    }
                }
                return Ok(areaCampoEtiquetaDTO);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult Insertar(EtiquetaDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                EtiquetaRepositorio _repEtiqueta = new EtiquetaRepositorio();
                EtiquetaBO etiqueta = new EtiquetaBO
                {
                    Nombre = Json.Nombre,
                    IdTipoEtiqueta = Json.IdTipoEtiqueta,
                    Descripcion = Json.Descripcion,
                    CampoDb = Json.CampoDb,
                    NodoPadre = Json.NodoPadre,
                    IdNodoPadre = Json.IdNodoPadre,
                    Estado = true,
                    UsuarioCreacion = Json.Usuario,
                    UsuarioModificacion = Json.Usuario,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now
                };

                if (Json.EtiquetaBotonReemplazo != null)
                {
                    var etiquetaBotonReemplazo = new EtiquetaBotonReemplazoBO()
                    {
                        Estilos = Json.EtiquetaBotonReemplazo.Estilos,
                        Url = Json.EtiquetaBotonReemplazo.Url,
                        AbrirEnNuevoTab = Json.EtiquetaBotonReemplazo.AbrirEnNuevoTab,
                        Texto = Json.EtiquetaBotonReemplazo.Texto,
                        Estado = true,
                        UsuarioCreacion = Json.Usuario,
                        UsuarioModificacion = Json.Usuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now
                    };
                    etiqueta.ListaEtiquetaBotonReemplazo.Add(etiquetaBotonReemplazo);
                }
                return Ok(_repEtiqueta.Insert(etiqueta));
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [Route("[action]")]
        [HttpPut]
        public ActionResult Actualizar(EtiquetaDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                EtiquetaRepositorio _repEtiqueta = new EtiquetaRepositorio();
                EtiquetaBotonReemplazoRepositorio _repEtiquetaBotonReemplazo = new EtiquetaBotonReemplazoRepositorio();

                if (!_repEtiqueta.Exist(Json.Id))
                {
                    return BadRequest("Registro no existente!");
                }

                EtiquetaBO etiqueta = _repEtiqueta.FirstById(Json.Id);
                etiqueta.Nombre = Json.Nombre;
                etiqueta.Descripcion = Json.Descripcion;
                etiqueta.CampoDb = Json.CampoDb;
                etiqueta.NodoPadre = Json.NodoPadre;
                etiqueta.IdNodoPadre = Json.IdNodoPadre;
                etiqueta.UsuarioModificacion = Json.Usuario;
                etiqueta.FechaModificacion = DateTime.Now;

                //eliminar
                var botonReemplazo = _repEtiquetaBotonReemplazo.GetBy(x => x.IdEtiqueta == Json.Id);
                _repEtiquetaBotonReemplazo.Delete(botonReemplazo.Select(x => x.Id), Json.Usuario);

                if (Json.EtiquetaBotonReemplazo != null)
                {
                    var etiquetaBotonReemplazo = new EtiquetaBotonReemplazoBO()
                    {
                        Estilos = Json.EtiquetaBotonReemplazo.Estilos,
                        Url = Json.EtiquetaBotonReemplazo.Url,
                        AbrirEnNuevoTab = Json.EtiquetaBotonReemplazo.AbrirEnNuevoTab,
                        Texto = Json.EtiquetaBotonReemplazo.Texto,
                        Estado = true,
                        UsuarioCreacion = Json.Usuario,
                        UsuarioModificacion = Json.Usuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now
                    };
                    etiqueta.ListaEtiquetaBotonReemplazo.Add(etiquetaBotonReemplazo);
                }

                return Ok(_repEtiqueta.Update(etiqueta));
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [Route("[action]")]
        [HttpDelete]
        public ActionResult Eliminar(EtiquetaDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                EtiquetaRepositorio _repEtiqueta = new EtiquetaRepositorio();
                EtiquetaBotonReemplazoRepositorio _repEtiquetaBotonReemplazo = new EtiquetaBotonReemplazoRepositorio();
                bool estadoEliminacion = false;
                using (TransactionScope scope = new TransactionScope())
                {
                    if (Json.NodoPadre)
                    {
                        var listaHijos = _repEtiqueta.GetBy(x => x.IdNodoPadre == Json.Id);
                        foreach (var item in listaHijos)
                        {
                            var listaBotonReemplazo = _repEtiquetaBotonReemplazo.GetBy(x => x.IdEtiqueta == item.Id).ToList();
                            _repEtiquetaBotonReemplazo.Delete(listaBotonReemplazo.Select(x => x.Id), Json.Usuario);
                            _repEtiqueta.Delete(item.Id, Json.Usuario);
                        }
                    }
                    estadoEliminacion = _repEtiqueta.Delete(Json.Id, Json.Usuario);
                    scope.Complete();
                }

                return Ok(estadoEliminacion);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}
