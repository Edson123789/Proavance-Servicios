using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/Empresa")]
    public class EmpresaController : Controller
    {
        [Route("[action]/{id}")]
        [HttpGet]
        public ActionResult GetEmpresaPorId(int id)
        {
            try
            {
                EmpresaRepositorio _repEmpresa = new EmpresaRepositorio();
				return Ok(_repEmpresa.ObtenerFiltroPorId(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

		[Route("[action]")]
		[HttpGet]
		public ActionResult Obtener()
		{
			try
			{
				EmpresaRepositorio _repEmpresa = new EmpresaRepositorio();
				var data = _repEmpresa.GetBy(x => x.Estado == true, x => new { x.Id, x.Nombre, x.Ruc, x.Direccion, x.IdTipoIdentificador, x.Telefono, x.PaginaWeb, x.Email, x.Trabajadores, x.NivelFacturacion, x.IdPais,x.IdRegion, x.IdCiudad, x.IdCodigoCiiuIndustria, x.IdTamanio, x.IdTipoEmpresa,x.FechaCreacion}).OrderByDescending(w=> w.FechaCreacion);
				return Ok(data);
			}
			catch (Exception ex)
			{
				return BadRequest(ex);
			}
		}

        [Route("[action]")]
		[HttpPost]
		public ActionResult InsertarEmpresa([FromBody]EmpresaDTO Objeto)
		{
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
			{
                EmpresaRepositorio _repEmpresa = new EmpresaRepositorio();
                EmpresaBO empresa = new EmpresaBO();

                empresa.Nombre = Objeto.Nombre;
                empresa.Ruc = Objeto.Ruc;
                empresa.IdTipoIdentificador = Objeto.IdTipoIdentificador;
                empresa.Direccion = Objeto.Direccion;
                empresa.Telefono = Objeto.Telefono;
                empresa.PaginaWeb = Objeto.PaginaWeb;
                empresa.Email = Objeto.Email;
                empresa.Trabajadores = Objeto.Trabajadores;
                empresa.NivelFacturacion = Objeto.NivelFacturacion;
                empresa.IdPais = Objeto.IdPais;
                empresa.IdRegion = Objeto.IdRegion;
                empresa.IdCiudad = Objeto.IdCiudad;
                empresa.IdIndustria = Objeto.IdIndustria;
                empresa.IdTipoEmpresa = Objeto.IdTipoEmpresa;
                empresa.IdTamanio = Objeto.IdTamanio;
                empresa.Ciiu = Objeto.Ciiu;
                empresa.IdCodigoCiiuIndustria = Objeto.IdCodigoCiiuIndustria;
                empresa.Estado = true;
                empresa.FechaCreacion = DateTime.Now;
                empresa.FechaModificacion = DateTime.Now;
                empresa.UsuarioCreacion = Objeto.usuario;
                empresa.UsuarioModificacion = Objeto.usuario;

                if (!empresa.HasErrors)
                {
                    var rpta = _repEmpresa.Insert(empresa);
                }
                else
                {
                    return BadRequest(empresa.GetErrors(null));
                }

                return Ok(empresa);
			}
			catch (Exception ex)
			{
				return BadRequest(ex);
			}
		}

        [Route("[action]")]
		[HttpPost]
		public ActionResult ActualizarEmpresa([FromBody]EmpresaDTO Objeto)
		{
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
			{
                EmpresaRepositorio _repEmpresa = new EmpresaRepositorio();
                EmpresaBO empresa = _repEmpresa.FirstById(Objeto.Id);

                empresa.Nombre = Objeto.Nombre;
                empresa.Ruc = Objeto.Ruc;
                empresa.IdTipoIdentificador = Objeto.IdTipoIdentificador;
                empresa.Direccion = Objeto.Direccion;
                empresa.Telefono = Objeto.Telefono;
                empresa.PaginaWeb = Objeto.PaginaWeb;
                empresa.Email = Objeto.Email;
                empresa.Trabajadores = Objeto.Trabajadores;
                empresa.NivelFacturacion = Objeto.NivelFacturacion;
                empresa.IdPais = Objeto.IdPais;
                empresa.IdRegion = Objeto.IdRegion;
                empresa.IdCiudad = Objeto.IdCiudad;
                empresa.IdIndustria = Objeto.IdIndustria;
                empresa.IdTipoEmpresa = Objeto.IdTipoEmpresa;
                empresa.IdTamanio = Objeto.IdTamanio;
                empresa.Ciiu = Objeto.Ciiu;
                empresa.IdCodigoCiiuIndustria = Objeto.IdCodigoCiiuIndustria;
                empresa.FechaModificacion = DateTime.Now;
                empresa.UsuarioModificacion = Objeto.usuario;

                if (!empresa.HasErrors)
                {
                    var rpta = _repEmpresa.Update(empresa);
                }
                else
                {
                    return BadRequest(empresa.GetErrors(null));
                }

                return Ok(empresa);
			}
			catch (Exception ex)
			{
				return BadRequest(ex);
			}
		}

        [Route("[action]")]
		[HttpPost]
		public ActionResult EliminarEmpresa([FromBody] EliminarDTO Objeto)
		{
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
			{
                EmpresaRepositorio _repEmpresa = new EmpresaRepositorio();
                _repEmpresa.Delete(Objeto.Id, Objeto.NombreUsuario);
                return Ok(true);
			}
			catch (Exception ex)
			{
				return BadRequest(ex);
			}
		}

        [Route("[action]/{Id}")]
		[HttpGet]
		public ActionResult ObtenerNombreCodigoCIIUPorId(int Id)
		{
            
            try
			{
                CodigoCiiuIndustriaRepositorio _repCodigoCiiuIndustria = new CodigoCiiuIndustriaRepositorio();
                //CodigoCiiuIndustriaDTO codigoCIIU = new CodigoCiiuIndustriaDTO();

                var codigoCIIU = _repCodigoCiiuIndustria.FirstById(Id);

                return Ok(codigoCIIU);
			}
			catch (Exception ex)
			{
				return BadRequest(ex);
			}
		}

        [Route("[action]")]
		[HttpPost]
		public ActionResult ObtenerNombreCodigoCIIUPorFiltro([FromBody] Dictionary<string, string> Filtros)
		{
            if (!ModelState.IsValid)
            {
                return Ok();
            }
            if (Filtros.Count<= 0 && Filtros != null)
            {
                return Ok();
            }
            try
			{
                CodigoCiiuIndustriaRepositorio _repCodigoCiiuIndustria = new CodigoCiiuIndustriaRepositorio();
                //CodigoCiiuIndustriaDTO codigoCIIU = new CodigoCiiuIndustriaDTO();
                string filtro = Filtros["Valor"].ToString();
                var codigoCIIU = _repCodigoCiiuIndustria.GetBy(w=> w.Nombre.Contains(filtro)).Select(x=> new { x.Id, x.Nombre }).ToList();

                return Ok(codigoCIIU);
			}
			catch (Exception ex)
			{
				return BadRequest(ex);
			}
		}









	}
}
