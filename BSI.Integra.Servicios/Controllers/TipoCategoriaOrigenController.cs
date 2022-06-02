using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.Comercial.BO;
using BSI.Integra.Aplicacion.Comercial.Repositorio;
using BSI.Integra.Aplicacion.Planificacion.BO;
using Microsoft.AspNetCore.Mvc;
using BSI.Integra.Aplicacion.GestionPersonas.BO;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Marketing.Repositorio;
using BSI.Integra.Aplicacion.Transversal.Helper;
using BSI.Integra.Persistencia.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/TipoCategoriaOrigen")]
    public class TipoCategoriaOrigenController : ControllerBase
    {
        private readonly integraDBContext _integraDBContext;
        public TipoCategoriaOrigenController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
        }

        

        [Route("[action]")]
        [HttpGet]
        public ActionResult VisualizarTipoCategoriaOrigen()
        {
            

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                TipoCategoriaOrigenRepositorio categoria = new TipoCategoriaOrigenRepositorio(_integraDBContext);
                
                var rpta = categoria.GetAll();

                TipoCategoriaOrigenRepositorio _repTipoCategoriaOrigen = new TipoCategoriaOrigenRepositorio(_integraDBContext);
                var listaTipoCategoriaOrigen = _repTipoCategoriaOrigen.ObtenerListaTodoTipoCategoriaOrigen();

                return Ok(listaTipoCategoriaOrigen);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


       


        [Route("[action]")]
        [HttpPost]
        public ActionResult InsertarTipoCategoriaOrigen([FromBody] TipoCategoriaOrigenListaDTO ObjetoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                TipoCategoriaOrigenBO nuevoTipoCategoriaOrigen = new TipoCategoriaOrigenBO();
                nuevoTipoCategoriaOrigen.Id = 0;
                nuevoTipoCategoriaOrigen.Nombre = ObjetoDTO.Nombre;
                nuevoTipoCategoriaOrigen.Descripcion = ObjetoDTO.Descripcion;
                nuevoTipoCategoriaOrigen.Meta = ObjetoDTO.Meta;

                nuevoTipoCategoriaOrigen.OportunidadMaxima =   int.Parse( Math.Round((1.0/(ObjetoDTO.Meta/100.0))).ToString());
                nuevoTipoCategoriaOrigen.Estado =  true;
                nuevoTipoCategoriaOrigen.UsuarioCreacion  = ObjetoDTO.Usuario;
                nuevoTipoCategoriaOrigen.UsuarioModificacion  = ObjetoDTO.Usuario;
                nuevoTipoCategoriaOrigen.FechaCreacion = DateTime.Now; 
                nuevoTipoCategoriaOrigen.FechaModificacion  = DateTime.Now;
             
                
                //nuevoTipoCategoriaOrigen.RowVersion  =  new byte[] {0x20,0x10};


                TipoCategoriaOrigenRepositorio categoria = new TipoCategoriaOrigenRepositorio(_integraDBContext);
                categoria.Insert(nuevoTipoCategoriaOrigen);
                return Ok(nuevoTipoCategoriaOrigen);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        

        [Route("[action]")]
        [HttpPost]
        public ActionResult ActualizarTipoCategoriaOrigen([FromBody] TipoCategoriaOrigenListaDTO ObjetoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            { 
                
                TipoCategoriaOrigenRepositorio repo_categoria = new TipoCategoriaOrigenRepositorio(_integraDBContext);
                TipoCategoriaOrigenBO categoria = repo_categoria.FirstById(ObjetoDTO.Id);

                categoria.Id = ObjetoDTO.Id;
                categoria.Nombre = ObjetoDTO.Nombre;
                categoria.Descripcion = ObjetoDTO.Descripcion;
                categoria.UsuarioModificacion = ObjetoDTO.Usuario;
                categoria.FechaModificacion = DateTime.Now;
                categoria.Meta = ObjetoDTO.Meta;
                categoria.OportunidadMaxima = int.Parse(Math.Round((1.0 / (ObjetoDTO.Meta / 100.0))).ToString());

                repo_categoria.Update(categoria);

                return Ok(categoria);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        


        [Route("[action]")]
        [HttpPost]
        public ActionResult EliminarTipoCategoriaOrigen([FromBody] EliminarDTO Eliminar)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                TipoCategoriaOrigenRepositorio repo_categoria = new TipoCategoriaOrigenRepositorio(_integraDBContext);
                TipoCategoriaOrigenBO categoria = repo_categoria.FirstById(Eliminar.Id);
                categoria.UsuarioModificacion = Eliminar.NombreUsuario;
                categoria.FechaModificacion = DateTime.Now;



                repo_categoria.Delete(Eliminar.Id, Eliminar.NombreUsuario);
                return Ok(true);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}
