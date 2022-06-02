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
using BSI.Integra.Aplicacion.Marketing.BO;
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/ListaPlantilla")]
    public class ListaPlantillaController : Controller
    {
        public ListaPlantillaController()
        {
        }

        

        [Route("[action]")]
        [HttpGet]
        public ActionResult VisualizarPlantilla()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                
                ListaPlantillaRepositorio repo_plantilla = new ListaPlantillaRepositorio();
                var TodoListaPlantilla = repo_plantilla.ObtenerTodasListaPlantilla();

                return Ok(TodoListaPlantilla);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


       


        [Route("[action]")]
        [HttpPost]
        public ActionResult InsertarPlantilla([FromBody] ListaPlantillaDTO ObjetoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                
                byte[] dataDisenho = Convert.FromBase64String(ObjetoDTO.Disenho);
                var decodedDisenho = Encoding.UTF8.GetString(dataDisenho);



                ListaPlantillaBO plantilla = new ListaPlantillaBO();
                plantilla.Id = 0;
                plantilla.Nombre = ObjetoDTO.Nombre;
                plantilla.Disenho = decodedDisenho;
                
                plantilla.Estado =  true;
                plantilla.UsuarioCreacion  = ObjetoDTO.Usuario;
                plantilla.UsuarioModificacion  = ObjetoDTO.Usuario;
                plantilla.FechaCreacion = DateTime.Now;
                plantilla.FechaModificacion  = DateTime.Now;
             
                ListaPlantillaRepositorio plantillaRepo = new ListaPlantillaRepositorio();
                plantillaRepo.Insert(plantilla);
                return Ok(plantilla);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        

        [Route("[action]")]
        [HttpPost]
        public ActionResult ActualizarPlantilla([FromBody] ListaPlantillaDTO ObjetoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                
                byte[] dataDisenho = Convert.FromBase64String(ObjetoDTO.Disenho);
                var decodedDisenho = Encoding.UTF8.GetString(dataDisenho);


                ListaPlantillaRepositorio repo_categoria = new ListaPlantillaRepositorio();
                ListaPlantillaBO categoria = repo_categoria.FirstById(ObjetoDTO.Id);

                categoria.Id = ObjetoDTO.Id;
                categoria.Nombre = ObjetoDTO.Nombre;
                categoria.Disenho = decodedDisenho;
                categoria.UsuarioModificacion = ObjetoDTO.Usuario;
                categoria.FechaModificacion = DateTime.Now;


                repo_categoria.Update(categoria);

                return Ok(categoria);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}
