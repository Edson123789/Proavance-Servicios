using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: MaterialAdicionalAulaVirtualController
	/// Autor: Lourdes Priscila.
	/// Fecha: 19/06/2021
	/// <summary>
	/// Controlador para el modulo de materialadicionalaulavirtual
	/// </summary>
    [Route("api/MaterialAdicionalAulaVirtual")]
    [ApiController]
    public class MaterialAdicionalAulaVirtualController : ControllerBase
    {
        private readonly integraDBContext _integraDBContext;

        public MaterialAdicionalAulaVirtualController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
        }
        /// TipoFuncion: POST
		/// Autor: Lourdes Priscila Pacsi Gamboa
		/// Fecha: 19/06/2021
		/// Version: 1.0
		/// <summary>
		/// Funcion que trae enlista los programas generales que ya tienen materiales adicionales
		/// </summary>
		/// <returns>Retorma una lista del tipo PEspecificoFiltroPGeneralDTO </returns>
        [Route("[action]")]
        [HttpPost]
        public IActionResult ObtenerListaRegistroMaterialPrograma()
        {
            try
            {
                MaterialAdicionalAulaVirtualRepositorio _repfiltros = new MaterialAdicionalAulaVirtualRepositorio();
                var _respuesta = _repfiltros.ListaMaterialAdicionalAulaVirtualProgramaGeneral();

                return Ok(_respuesta);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// TipoFuncion: POST
		/// Autor: Lourdes Priscila Pacsi Gamboa
		/// Fecha: 19/06/2021
		/// Version: 1.0
		/// <summary>
		/// Funcion que trae toda la configuracion de un registro en especifico de la tabla T_MaterialAdicionalAulaVirtual
		/// </summary>
		/// <param name="IdMaterialAdicional">Id de la configuracion</param>
		/// <returns>Retorma un tipo de datos DatosMaterialAdicionalAulaVirtualDTO </returns>
        [Route("[action]/{IdMaterialAdicional}")]
        [HttpPost]
        public IActionResult ListaRegistroMaterialAdicionalAulaVirtual(int IdMaterialAdicional)
        {
            try
            {
                MaterialAdicionalAulaVirtualRepositorio _repMaterialAdicionalAulaVirtual = new MaterialAdicionalAulaVirtualRepositorio();
                MaterialAdicionalAulaVirtualPEspecificoRepositorio _repMaterialAdicionalAulaVirtualPEspecifico = new MaterialAdicionalAulaVirtualPEspecificoRepositorio();
                MaterialAdicionalAulaVirtualRegistroRepositorio _repMaterialAdicionalAulaVirtualRegistro = new MaterialAdicionalAulaVirtualRegistroRepositorio();

                DatosMaterialAdicionalAulaVirtualDTO datosMaterialAdicional = new DatosMaterialAdicionalAulaVirtualDTO();
                var materialAdicional = _repMaterialAdicionalAulaVirtual.DatosMaterialAdicional(IdMaterialAdicional);
                var registroMaterialAdicional = _repMaterialAdicionalAulaVirtualRegistro.ListaMaterialAdicionalAulaVirtualRegistro(IdMaterialAdicional);
                var pEspecificoMaterialAdicional = _repMaterialAdicionalAulaVirtualPEspecifico.ListaMaterialAdicionalAulaVirtualPEspecifico(IdMaterialAdicional);                

                if (materialAdicional != null)
                {
                    datosMaterialAdicional.MaterialAdicional = materialAdicional;
                }
                if (registroMaterialAdicional != null && registroMaterialAdicional.Count > 0)
                {
                    datosMaterialAdicional.MaterialAdicionalRegistro = registroMaterialAdicional;
                }
                if (pEspecificoMaterialAdicional != null && pEspecificoMaterialAdicional.Count > 0)
                {
                    datosMaterialAdicional.ProgramaEspecifico = pEspecificoMaterialAdicional.Select(x => x.IdPespecifico).ToList();
                }
                
                return Ok(datosMaterialAdicional);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// TipoFuncion: POST
		/// Autor: Lourdes Priscila Pacsi Gamboa
		/// Fecha: 19/06/2021
		/// Version: 1.0
		/// <summary>
		/// Funcion que inserta o actualiza en las tablas correspondientes con el modulo MaterialAdicionalAulaVirtual
		/// </summary>
		/// <param name="Data">Datos para la insercion o actualizacion</param>
        [Route("[action]")]
        [HttpPost]
        public ActionResult InsertarMaterialAdicionalAulaVirtual([FromBody] InsertarMaterialAdicionalAulaVirtualRegistroDTO Datos)
        {
            try
            {
                MaterialAdicionalAulaVirtualRepositorio _repMaterialAdicionalAulaVirtual = new MaterialAdicionalAulaVirtualRepositorio();
                MaterialAdicionalAulaVirtualPEspecificoRepositorio _repMaterialAdicionalAulaVirtualPEspecifico = new MaterialAdicionalAulaVirtualPEspecificoRepositorio();
                MaterialAdicionalAulaVirtualRegistroRepositorio _repMaterialAdicionalAulaVirtualRegistro = new MaterialAdicionalAulaVirtualRegistroRepositorio();

                

                if (_repMaterialAdicionalAulaVirtual.Exist(Datos.Id))
                {
                    MaterialAdicionalAulaVirtualBO material = new MaterialAdicionalAulaVirtualBO();
                    _repMaterialAdicionalAulaVirtualPEspecifico.DeleteLogicoPorMaterial(Datos.Id, Datos.Usuario, Datos.ProgramaEspecifico);
                    var listaIdRegistro = Datos.MaterialAdicional.Select(x => x.Id).ToList();
                    
                    _repMaterialAdicionalAulaVirtualRegistro.DeleteLogicoPorMaterial(Datos.Id, Datos.Usuario, (List<int>)listaIdRegistro);
                    material = _repMaterialAdicionalAulaVirtual.FirstById(Datos.Id);
                    material.NombreConfiguracion = Datos.NombreConfiguracion;
                    material.IdPgeneral = Datos.IdPgeneral;
                    material.EsOnline = Datos.EsOnline;
                    material.UsuarioModificacion = Datos.Usuario;
                    material.FechaModificacion = DateTime.Now;

                    _repMaterialAdicionalAulaVirtual.Update(material);
                    
                        foreach (var registro in Datos.MaterialAdicional)
                    {
                        MaterialAdicionalAulaVirtualRegistroBO _registro;
                        _registro = _repMaterialAdicionalAulaVirtualRegistro.FirstBy(x => x.Id == registro.Id && x.IdMaterialAdicionalAulaVirtual == material.Id);
                        if (_registro != null)
                        {
                            _registro.IdMaterialAdicionalAulaVirtual = material.Id;
                            _registro.NombreArchivo = registro.NombreArchivo;
                            _registro.RutaArchivo = registro.RutaArchivo;
                            _registro.EsEnlace = registro.EsEnlace;
                            _registro.Estado = true;
                            _registro.UsuarioModificacion = Datos.Usuario;
                            _registro.FechaModificacion = DateTime.Now;
                            _repMaterialAdicionalAulaVirtualRegistro.Update(_registro);
                        }
                        else
                        {
                            _registro = new MaterialAdicionalAulaVirtualRegistroBO();
                            _registro.IdMaterialAdicionalAulaVirtual = material.Id;
                            _registro.NombreArchivo = registro.NombreArchivo;
                            _registro.RutaArchivo = registro.RutaArchivo;
                            _registro.EsEnlace = registro.EsEnlace;
                            _registro.Estado = true;
                            _registro.UsuarioCreacion = Datos.Usuario;
                            _registro.UsuarioModificacion = Datos.Usuario;
                            _registro.FechaCreacion = DateTime.Now;
                            _registro.FechaModificacion = DateTime.Now;

                            _repMaterialAdicionalAulaVirtualRegistro.Insert(_registro);
                        }
                        

                    }
                    if(Datos.ProgramaEspecifico !=null && Datos.ProgramaEspecifico.Count() !=0 )
                    {
                        foreach (var pe in Datos.ProgramaEspecifico)
                        {
                            MaterialAdicionalAulaVirtualPEspecificoBO pespecifico;
                            pespecifico = _repMaterialAdicionalAulaVirtualPEspecifico.FirstBy(x => x.IdPespecifico == pe && x.IdMaterialAdicionalAulaVirtual == material.Id);
                            if (pespecifico != null)
                            {
                                pespecifico.IdPespecifico = pe;
                                pespecifico.UsuarioModificacion = Datos.Usuario;
                                pespecifico.FechaModificacion = DateTime.Now;
                                pespecifico.Estado = true;
                                _repMaterialAdicionalAulaVirtualPEspecifico.Update(pespecifico);
                            }
                            else
                            {
                                pespecifico = new MaterialAdicionalAulaVirtualPEspecificoBO();
                                pespecifico.IdMaterialAdicionalAulaVirtual = material.Id;
                                pespecifico.IdPespecifico = pe;
                                pespecifico.Estado = true;
                                pespecifico.UsuarioCreacion = Datos.Usuario;
                                pespecifico.UsuarioModificacion = Datos.Usuario;
                                pespecifico.FechaCreacion = DateTime.Now;
                                pespecifico.FechaModificacion = DateTime.Now;

                                _repMaterialAdicionalAulaVirtualPEspecifico.Insert(pespecifico);
                            }

                        }
                    }
                    
                }
                else
                {
                    MaterialAdicionalAulaVirtualBO _MaterialAdicional = new MaterialAdicionalAulaVirtualBO();
                    _MaterialAdicional.NombreConfiguracion = Datos.NombreConfiguracion;
                    _MaterialAdicional.IdPgeneral = Datos.IdPgeneral;
                    _MaterialAdicional.EsOnline = Datos.EsOnline;
                    _MaterialAdicional.Estado = true;
                    _MaterialAdicional.UsuarioCreacion = Datos.Usuario;
                    _MaterialAdicional.UsuarioModificacion = Datos.Usuario;
                    _MaterialAdicional.FechaCreacion = DateTime.Now;
                    _MaterialAdicional.FechaModificacion = DateTime.Now;

                    _repMaterialAdicionalAulaVirtual.Insert(_MaterialAdicional);
                    foreach (var material in Datos.MaterialAdicional)
                    {
                        MaterialAdicionalAulaVirtualRegistroBO _registro = new MaterialAdicionalAulaVirtualRegistroBO();
                        _registro.IdMaterialAdicionalAulaVirtual = _MaterialAdicional.Id;
                        _registro.NombreArchivo = material.NombreArchivo;
                        _registro.RutaArchivo = material.RutaArchivo;
                        _registro.EsEnlace = material.EsEnlace;
                        _registro.Estado = true;
                        _registro.UsuarioCreacion = Datos.Usuario;
                        _registro.UsuarioModificacion = Datos.Usuario;
                        _registro.FechaCreacion = DateTime.Now;
                        _registro.FechaModificacion = DateTime.Now;

                        _repMaterialAdicionalAulaVirtualRegistro.Insert(_registro);
                    }
                    if (Datos.ProgramaEspecifico != null && Datos.ProgramaEspecifico.Count() != 0)
                    {
                        foreach (var pespecifico in Datos.ProgramaEspecifico)
                        {
                            MaterialAdicionalAulaVirtualPEspecificoBO _registro = new MaterialAdicionalAulaVirtualPEspecificoBO();
                            _registro.IdMaterialAdicionalAulaVirtual = _MaterialAdicional.Id;
                            _registro.IdPespecifico = pespecifico;
                            _registro.Estado = true;
                            _registro.UsuarioCreacion = Datos.Usuario;
                            _registro.UsuarioModificacion = Datos.Usuario;
                            _registro.FechaCreacion = DateTime.Now;
                            _registro.FechaModificacion = DateTime.Now;

                            _repMaterialAdicionalAulaVirtualPEspecifico.Insert(_registro);
                        }
                    }

                       
                }              

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// TipoFuncion: POST
		/// Autor: Lourdes Priscila Pacsi Gamboa
		/// Fecha: 19/06/2021
		/// Version: 1.0
		/// <summary>
		/// Elimina un registro de la tabla T_MaterialAdicionalAulaVirtual
		/// </summary>
		/// <param name="Datos">Datos necesarios para eliminar el registro</param>
		/// <returns>un dato tipo bool </returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult EliminarRegistro([FromBody] MaterialAdicionalAulaVirtualDTO Datos)
        {
            try
            {
                MaterialAdicionalAulaVirtualRepositorio _repMaterialAdicionalAulaVirtual = new MaterialAdicionalAulaVirtualRepositorio();

                
                bool result = false;
                if (_repMaterialAdicionalAulaVirtual.Exist(Datos.Id))
                {
                    result = _repMaterialAdicionalAulaVirtual.Delete(Datos.Id, Datos.Usuario);
                }
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
