using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Finanzas.Repositorio;
using BSI.Integra.Aplicacion.GestionPersonas.Repositorio;
using BSI.Integra.Aplicacion.Maestros.Repositorio;
using BSI.Integra.Aplicacion.Marketing.BO;
using BSI.Integra.Aplicacion.Marketing.Repositorio;
using BSI.Integra.Aplicacion.Planificacion.Repositorio;
using BSI.Integra.Aplicacion.Servicios;
using BSI.Integra.Aplicacion.Servicios.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/MaterialPEspecifico")]
    public class MaterialPespecificoController : ControllerBase
    {
        private readonly integraDBContext _integraDBContext;
        private readonly MaterialTipoEntregaRepositorio _repMaterialTipoEntrega;
        private readonly MaterialTipoRepositorio _repMaterialTipo;
        private readonly MaterialPespecificoRepositorio _repMaterialPespecifico;
        private readonly MaterialPespecificoDetalleRepositorio _repMaterialPespecificoDetalle;
        private readonly PespecificoRepositorio _repPespecifico;
        private readonly PespecificoSesionRepositorio _repPespecificoSesion;
        private readonly MaterialVersionRepositorio _repMaterialVersion;
        private readonly MaterialAsociacionVersionRepositorio _repMaterialAsociacionVersion;
        private readonly MaterialEnvioRepositorio _repMaterialEnvio;
        private readonly MaterialEnvioDetalleRepositorio _repMaterialEnvioDetalle;
        private readonly MaterialEstadoRecepcionRepositorio _repMaterialEstadoRecepcion;
        private readonly SedeTrabajoRepositorio _repSedeTrabajo;
        private readonly PersonalRepositorio _repPersonal;
        private readonly OportunidadRepositorio _repOportunidad;
        private readonly AlumnoRepositorio _repAlumno;
        private readonly MatriculaCabeceraRepositorio _repMatriculaCabecera;
        private readonly ProveedorRepositorio _repProveedor;
        private readonly FurRepositorio _repFur;
        private readonly PespecificoRepositorio _repPEspecifico;
        private readonly PespecificoSesionRepositorio _repPEspecificoSesion;
        private readonly ExpositorRepositorio _repExpositor;
        private readonly AsistenciaRepositorio _repAsistencia;
        private readonly MaterialEntregaRepositorio _repMaterialEntrega;
        private readonly MaterialEstadoRepositorio _repMaterialEstado;
        private readonly MaterialAsociacionAccionRepositorio _repMaterialAsociacionAccion;
        private readonly MaterialAccionRepositorio _repMaterialAccion;
        private readonly MaterialAsociacionCriterioVerificacionRepositorio _repMaterialAsociacionCriterioVerificacion;
        private readonly MaterialCriterioVerificacionDetalleRepositorio _repMaterialCriterioVerificacionDetalle;

        public MaterialPespecificoController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
            _repMaterialTipoEntrega = new MaterialTipoEntregaRepositorio(_integraDBContext);
            _repMaterialTipo = new MaterialTipoRepositorio(_integraDBContext);
            _repMaterialPespecifico = new MaterialPespecificoRepositorio(_integraDBContext);
            _repPespecificoSesion = new PespecificoSesionRepositorio(_integraDBContext);
            _repPespecifico = new PespecificoRepositorio(_integraDBContext);
            _repMaterialVersion = new MaterialVersionRepositorio(_integraDBContext);
            _repMaterialAsociacionVersion = new MaterialAsociacionVersionRepositorio(_integraDBContext);
            _repMaterialPespecificoDetalle = new MaterialPespecificoDetalleRepositorio(_integraDBContext);
            _repMaterialEstadoRecepcion = new MaterialEstadoRecepcionRepositorio(_integraDBContext);
            _repMaterialEnvio = new MaterialEnvioRepositorio(_integraDBContext);
            _repMaterialEnvioDetalle = new MaterialEnvioDetalleRepositorio(_integraDBContext);
            _repSedeTrabajo = new SedeTrabajoRepositorio(_integraDBContext);
            _repPersonal = new PersonalRepositorio(_integraDBContext);
            _repProveedor = new ProveedorRepositorio(_integraDBContext);
            _repPEspecifico = new PespecificoRepositorio(_integraDBContext);
            _repPEspecificoSesion = new PespecificoSesionRepositorio(_integraDBContext);
            _repExpositor = new ExpositorRepositorio(_integraDBContext);
            _repAsistencia = new AsistenciaRepositorio(_integraDBContext);
            _repMaterialEntrega = new MaterialEntregaRepositorio(_integraDBContext);
            _repMaterialEstado = new MaterialEstadoRepositorio(_integraDBContext);
            _repMaterialAsociacionAccion = new MaterialAsociacionAccionRepositorio(_integraDBContext);
            _repMaterialAsociacionCriterioVerificacion = new MaterialAsociacionCriterioVerificacionRepositorio(_integraDBContext);
            _repMaterialCriterioVerificacionDetalle = new MaterialCriterioVerificacionDetalleRepositorio(_integraDBContext);

            _repMaterialAccion = new MaterialAccionRepositorio(_integraDBContext);
            _repAlumno = new AlumnoRepositorio(_integraDBContext);
            _repOportunidad = new OportunidadRepositorio(_integraDBContext);
            _repMatriculaCabecera = new MatriculaCabeceraRepositorio(_integraDBContext);
            _repFur = new FurRepositorio(_integraDBContext);
        }

        /// <summary>
        /// Obtendra los materiales que estan con estado aprobado, todos los materiales del grupo de edicion deben estar aprobados
        /// </summary>
        /// <param name="IdPEspecifico"></param>
        /// <param name="Grupo"></param>
        /// <returns></returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult ObtenerMaterialesPorProgramaEspecificoGrupoGestionEnvio([FromBody] FiltroMaterialDTO FiltroMaterial)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                //var listaIdMaterialEstado = new List<int>() {
                //    3,//aprobado
                //};
                //var lista = _repMaterialPespecifico.ObtenerPorProgramaEspecificoGrupo(IdPEspecifico, Grupo, listaIdMaterialEstado);
                ////1   Material para alumno - digital //version
                //var listaIdMaterialVersion = new List<int>() {
                //    1,//enviar correo alumno
                //};
                //foreach (var item in lista)
                //{
                //    item.ListaMaterialPEspecificoDetalle = _repMaterialPespecificoDetalle.GetBy(x => x.IdMaterialPespecifico == item.Id && listaIdMaterialVersion.Contains(x.IdMaterialVersion)).Select(x => new MaterialPEspecificoDetalleVersionDTO()
                //    {
                //        Id = x.Id,
                //        IdMaterialVersion = x.IdMaterialVersion,
                //        NombreArchivo = x.NombreArchivo
                //    }).ToList();
                //}
                var lista = _repMaterialPespecifico.ObtenerMaterialesGestionEnvio(FiltroMaterial);

                foreach (var item in lista)
                {
                    item.ListaMaterialAccion = _repMaterialPespecifico.ObtenerMaterialAccionPorMaterialTipo(item.IdMaterialTipo);
                }
                return Ok(lista);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Wilber Choque
        /// Fecha: 11/07/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los materiales por grupo de programa especifico a revisar
        /// </summary>
        /// <param name="FiltroMaterial">Objeto de clase FiltroMaterialDTO</param>
        /// <returns>Lista de objetos de clase ResultadoMaterialPEspecificoDetalleDTO</returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult ObtenerMaterialesPorProgramaEspecificoGrupoRevisar([FromBody] FiltroMaterialDTO FiltroMaterial)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                return Ok(_repMaterialPespecifico.ObtenerMateriales(FiltroMaterial));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [Route("[action]")]
        [HttpPost]
        public ActionResult ObtenerMateriales([FromBody] FiltroMaterialDTO FiltroMaterial)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                //var listaIdMaterialEstado = new List<int>() {
                //    1,//por editar
                //    2,//editado
                //    4,//observado
                //};
                //FiltroMaterial.ListaMaterialEstado = listaIdMaterialEstado.ToArray();
                return Ok(_repMaterialPespecifico.ObtenerMateriales(FiltroMaterial));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }



        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerFiltros()
        {
            try
            {


                PespecificoRepositorio _repPespecifico = new PespecificoRepositorio(_integraDBContext);
                RegionCiudadRepositorio _repRegionCiudad = new RegionCiudadRepositorio(_integraDBContext);
                ModalidadCursoRepositorio _repModalidadCurso = new ModalidadCursoRepositorio(_integraDBContext);
                PgeneralRepositorio _repPGeneral = new PgeneralRepositorio(_integraDBContext);
                AreaCapacitacionRepositorio _repArea = new AreaCapacitacionRepositorio(_integraDBContext);
                SubAreaCapacitacionRepositorio _repSubArea = new SubAreaCapacitacionRepositorio(_integraDBContext);
                PespecificoRepositorio _repPEspecifico = new PespecificoRepositorio(_integraDBContext);
                EstadoPespecificoRepositorio _repEstadoPespecifico = new EstadoPespecificoRepositorio(_integraDBContext);
                ModalidadCursoRepositorio _repModalidad = new ModalidadCursoRepositorio(_integraDBContext);
                PespecificoSesionRepositorio _repPespecificoSesion = new PespecificoSesionRepositorio(_integraDBContext);

                var res = _repPEspecifico.ObtenerPEspecificoGruposPorPEspecificoPadre();
                var ciudadBs = _repRegionCiudad.ObtenerListaCiudadesBs();
                var area = _repArea.ObtenerAreaCapacitacionFiltro();
                var subArea = _repSubArea.ObtenerSubAreasParaFiltro();
                var programaGeneral = _repPGeneral.ObtenerProgramaGeneralPadre(null);
                var programaEspecifico = _repPespecifico.ObtenerProgramasEspecificosPadres(null);
                var modalidades = _repModalidad.ObtenerModalidadCursoFiltro();
                var estadoPespecifico = _repEstadoPespecifico.ObtenerEstadoPespecificoParaCombo();
                var grupos = _repPespecificoSesion.ObtenerGruposProgramaEspecificoFiltro();
                //var grupos =  _repPEspecifico.ObtenerGrupoSesiones();

                //FUR
                ProductoRepositorio _repProducto = new ProductoRepositorio(_integraDBContext);
                ProductoPresentacionRepositorio _repProductoPresentacion = new ProductoPresentacionRepositorio(_integraDBContext);
                ProveedorRepositorio _repProveedor = new ProveedorRepositorio(_integraDBContext);

                var producto = _repProducto.ObtenerListaProductoMaterialesParaCombo();
                var proveedor = _repProveedor.ObtenerProveedorParaCombo();
                var productoPresentacion = _repProductoPresentacion.ObtenerProductoPresentacionParaCombo();

                var listaExpositor = _repExpositor.ObtenerTodoFiltro();
                var listaMaterialVersion = _repMaterialVersion.ObtenerTodoFiltro();
                var listaMaterialEstado = _repMaterialEstado.ObtenerTodoFiltro();
                var listaMaterialTipo = _repMaterialTipo.ObtenerTodoFiltro();

                var listaFiltros = new
                {
                    ListaArea = area,
                    ListaSubArea = subArea,
                    ListaProgramaGeneral = programaGeneral,
                    ListaProgramaEspecifico = programaEspecifico,
                    ListaPEspecificoCurso = res,
                    ListaEstadoPEspecifico = estadoPespecifico,
                    ListaGrupo = grupos,
                    ListaModalidad = modalidades,
                    ListaCiudadBS = ciudadBs,

                    ListaProducto = producto,
                    ListaProveedor = proveedor,
                    ListaProductoPresentacion = productoPresentacion,

                    ListaExpositor = listaExpositor,
                    ListaMaterialVersion = listaMaterialVersion,
                    ListaMaterialEstado = listaMaterialEstado,
                    ListaMaterialTipo = listaMaterialTipo
                };




                //var listaGrupo = _repPEspecifico.ObtenerGrupoSesiones();
                //var listaPEspecificoPadre = _repPEspecifico.ObtenerPadres();
                //var listaPEspecificoHijo = _repPEspecifico.ObtenerHijos();
                //var listaExpositor = _repExpositor.ObtenerTodoFiltro();
                //var listaMaterialVersion = _repMaterialVersion.ObtenerTodoFiltro();
                //var listaMaterialEstado = _repMaterialEstado.ObtenerTodoFiltro();
                //var listaMaterialTipo = _repMaterialTipo.ObtenerTodoFiltro();

                //ProductoRepositorio _repProducto = new ProductoRepositorio(_integraDBContext);
                //ProductoPresentacionRepositorio _repProductoPresentacion = new ProductoPresentacionRepositorio(_integraDBContext);
                //ProveedorRepositorio _repProveedor = new ProveedorRepositorio(_integraDBContext);

                //var listaProducto = _repProducto.ObtenerListaProductoMaterialesParaCombo();
                //var listaProveedor = _repProveedor.ObtenerProveedorParaCombo();
                //var listaProductoPresentacion = _repProductoPresentacion.ObtenerProductoPresentacionParaCombo();

                //var listaFiltros = new
                //{
                //    ListaExpositor = listaExpositor,
                //    ListaGrupo = listaGrupo,
                //    ListaPEspecificoPadre = listaPEspecificoPadre,
                //    ListaPEspecificoHijo = listaPEspecificoHijo,
                //    ListaMaterialVersion = listaMaterialVersion,
                //    ListaMaterialEstado = listaMaterialEstado,
                //    ListaMaterialTipo = listaMaterialTipo,

                //    ListaProducto = listaProducto,
                //    ListaProveedor = listaProveedor,
                //    ListaProductoPresentacion = listaProductoPresentacion
                //};
                return Ok(listaFiltros);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]/{IdPEspecifico}")]
        [HttpGet]
        public ActionResult ObtenerPorPEspecifico(int IdPEspecifico)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if (!_repPespecifico.Exist(IdPEspecifico))
                {
                    return BadRequest("Programa especifico no existente");
                }
                return Ok(_repMaterialPespecifico.ObtenerPorPEspecifico(IdPEspecifico));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [Route("[Action]/{IdPEspecifico}")]
        [HttpGet]
        public ActionResult ObtenerFursAsociadosPorPEspecifico(int IdPEspecifico)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if (!_repPespecifico.Exist(IdPEspecifico))
                {
                    return BadRequest("Programa especifico no existente");
                }
                return Ok(_repMaterialPespecifico.ObtenerFursAsociadosPorPEspecifico(IdPEspecifico));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]/{IdMaterialPEspecificoDetalle}")]
        [HttpGet]
        public ActionResult ObtenerFurAsociadoPorMaterialPEspecificoDetalle(int IdMaterialPEspecificoDetalle)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if (!_repPespecifico.Exist(IdMaterialPEspecificoDetalle))
                {
                    return BadRequest("Programa especifico no existente");
                }
                return Ok(_repMaterialPespecifico.ObtenerFurAsociadoPorMaterialPEspecificoDetalle(IdMaterialPEspecificoDetalle));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]/{Id}")]
        [HttpGet]
        public ActionResult ObtenerDetalle(int Id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if (!_repMaterialPespecifico.Exist(Id))
                {
                    return BadRequest("Material no existente");
                }
                var listaIdMaterialEstado = new List<int>() {
                    1,//por editar
                    2,//editado
                    4,//observado
                };
                return Ok(_repMaterialPespecificoDetalle.ObtenerPorMaterialPEspecifico(Id, listaIdMaterialEstado));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// Tipo Función: GET
        /// Autor: Wilber Choque
        /// Fecha: 11/07/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el detalle a revisar
        /// </summary>
        /// <returns>Lista de objetos de clase MaterialPespecificoDetalleBO</returns>
        [Route("[Action]/{Id}")]
        [HttpGet]
        public ActionResult ObtenerDetalleRevisar(int Id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if (!_repMaterialPespecifico.Exist(Id))
                {
                    return BadRequest("Material no existente");
                }
                var listaIdMaterialEstado = new List<int>() {
                    2,//editado
                };
                return Ok(_repMaterialPespecificoDetalle.ObtenerPorMaterialPEspecifico(Id, listaIdMaterialEstado));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [Route("[Action]/{Id}")]
        [HttpGet]
        public ActionResult ObtenerDetalleGestionEnvio(int Id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if (!_repMaterialPespecifico.Exist(Id))
                {
                    return BadRequest("Material no existente");
                }
                var listaIdMaterialEstado = new List<int>() {
                    3,//aprobado
                };
                //solo estado aprobado
                var listaMaterialPEspecificoDetalle = _repMaterialPespecificoDetalle.ObtenerPorMaterialPEspecifico(Id, listaIdMaterialEstado);

                List<MaterialAccionBO> listaMaterialAccion = new List<MaterialAccionBO>();
                var materialPEspecifico = _repMaterialPespecifico.FirstById(Id);
                var listaMaterialAsociacionAccion = _repMaterialAsociacionAccion.GetBy(x => x.IdMaterialTipo == materialPEspecifico.IdMaterialTipo);
                var listaAcciones = listaMaterialAsociacionAccion.Select(x => x.IdMaterialAccion);
                if (listaAcciones != null && listaAcciones.Any())
                {
                    listaMaterialAccion = _repMaterialAccion.GetBy(x => listaAcciones.Contains(x.Id)).ToList();
                }

                foreach (var item in listaMaterialPEspecificoDetalle)
                {
                    item.ListaMaterialAccion = listaMaterialAccion;
                }
                return Ok(listaMaterialPEspecificoDetalle);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Solo los con estado por editar/editado/desaprobado
        /// </summary>
        /// <param name="IdPEspecifico"></param>
        /// <param name="Grupo"></param>
        /// <returns></returns>
        [Route("[action]/{IdPEspecifico}/{Grupo}")]
        [HttpGet]
        public ActionResult ObtenerMaterialesPorProgramaEspecificoGrupo(int IdPEspecifico, int Grupo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var listaIdMaterialEstado = new List<int>() {
                    1,//por editar
                    2,//editado
                    4,//observado
                };
                return Ok(_repMaterialPespecifico.ObtenerPorProgramaEspecificoGrupo(IdPEspecifico, Grupo, listaIdMaterialEstado));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]/{IdPEspecifico}/{Grupo}")]
        [HttpGet]
        public ActionResult ObtenerMaterialesPorProgramaEspecificoGrupoRevisar(int IdPEspecifico, int Grupo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var listaIdMaterialEstado = new List<int>() {
                    2,//editado
                };
                return Ok(_repMaterialPespecifico.ObtenerPorProgramaEspecificoGrupo(IdPEspecifico, Grupo, listaIdMaterialEstado));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Obtendra los materiales que estan con estado aprobado, todos los materiales del grupo de edicion deben estar aprobados
        /// </summary>
        /// <param name="IdPEspecifico"></param>
        /// <param name="Grupo"></param>
        /// <returns></returns>
        [Route("[action]/{IdPEspecifico}/{Grupo}")]
        [HttpGet]
        public ActionResult ObtenerMaterialesPorProgramaEspecificoGrupoGestionEnvio(int IdPEspecifico, int Grupo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var listaIdMaterialEstado = new List<int>() {
                    3,//aprobado
                };
                var lista = _repMaterialPespecifico.ObtenerPorProgramaEspecificoGrupo(IdPEspecifico, Grupo, listaIdMaterialEstado);
                //1   Material para alumno - digital //version
                var listaIdMaterialVersion = new List<int>() {
                    1,//enviar correo alumno
                };
                foreach (var item in lista)
                {
                    item.ListaMaterialPEspecificoDetalle = _repMaterialPespecificoDetalle.GetBy(x => x.IdMaterialPespecifico == item.Id && listaIdMaterialVersion.Contains(x.IdMaterialVersion)).Select(x => new MaterialPEspecificoDetalleVersionDTO()
                    {
                        Id = x.Id,
                        IdMaterialVersion = x.IdMaterialVersion,
                        NombreArchivo = x.NombreArchivo
                    }).ToList();
                }
                return Ok(lista);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]/{IdMaterialPEspecifico}")]
        [HttpGet]
        public ActionResult ObtenerPEspecificoDetalleMaterialesAlumnoDigitalPorMaterialPEspecifico(int IdMaterialPEspecifico)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                //todos los materiales del grupo de edicion con material version enviar correo alumno
                var listaIdMaterialVersion = new List<int>() {
                    1,//enviar correo alumno
                };

                var listaIdMaterialEstado = new List<int>() {
                    3,//aprobado
                };
                var materialPEspecifico = _repMaterialPespecifico.FirstById(IdMaterialPEspecifico);
                var listaMaterialPEspecifico = _repMaterialPespecifico.GetBy(x => x.IdPespecifico == materialPEspecifico.IdPespecifico && x.GrupoEdicion == materialPEspecifico.GrupoEdicion).ToList();

                var listaIdMaterialPEspecifico = listaMaterialPEspecifico.Select(x => x.Id).ToList();
                var lista = _repMaterialPespecificoDetalle.GetBy(x => listaIdMaterialPEspecifico.Contains(x.IdMaterialPespecifico)
                               && listaIdMaterialVersion.Contains(x.IdMaterialVersion)
                               && listaIdMaterialEstado.Contains(x.IdMaterialEstado)
                              ).ToList();

                foreach (var item in lista)
                {
                    item.IdMaterialTipo = listaMaterialPEspecifico.Where(x => item.IdMaterialPespecifico == x.Id).FirstOrDefault().IdMaterialTipo;
                }
                return Ok(lista);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// Tipo Función: GET
        /// Autor: Wilber Choque
        /// Fecha: 11/07/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los tipos de materiales
        /// </summary>
        /// <returns>Lista de objetos de clase MaterialTipoBO</returns>
        [Route("[Action]")]
        [HttpGet]
        public ActionResult Obtener()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                return Ok(_repMaterialTipo.Obtener());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Wilber Choque
        /// Fecha: 11/07/2021
        /// Versión: 1.0
        /// <summary>
        /// Inserta los materiales de un PEspecifico
        /// </summary>
        /// <returns>Lista de objetos de clase FiltroDTO</returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult Insertar([FromBody] MaterialPespecificoDTO MaterialPespecifico)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if (!_repMaterialTipo.Exist(MaterialPespecifico.IdMaterialTipo))
                {
                    return BadRequest("Tipo de material no existente!");
                }

                if (MaterialPespecifico.GrupoEdicion == 0)
                {
                    MaterialPespecifico.GrupoEdicion = _repMaterialPespecifico.ObtenerProximoGrupoEdicion(MaterialPespecifico.IdPEspecifico);
                }

                var materialPEspecifico = new MaterialPespecificoBO()
                {
                    IdMaterialTipo = MaterialPespecifico.IdMaterialTipo,
                    IdPespecifico = MaterialPespecifico.IdPEspecifico,
                    Grupo = MaterialPespecifico.Grupo,
                    GrupoEdicion = MaterialPespecifico.GrupoEdicion,
                    GrupoEdicionOrden = MaterialPespecifico.GrupoEdicionOrden,
                    IdFur = null,
                    UsuarioCreacion = MaterialPespecifico.NombreUsuario,
                    UsuarioModificacion = MaterialPespecifico.NombreUsuario,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now,
                    Estado = true
                };

                //IdMaterialTipo = MaterialPespecifico.IdMaterialTipo,
                //Insertamos todas las versiones del tipo de material
                var idMaterialEstado_PorEditar = 1;

                var listaMaterialAsociacionVersion = _repMaterialAsociacionVersion.GetBy(x => x.IdMaterialTipo == MaterialPespecifico.IdMaterialTipo);

                MaterialPespecificoDetalleBO materialPespecificoDetalle;
                foreach (var item in listaMaterialAsociacionVersion)
                {
                    materialPespecificoDetalle = new MaterialPespecificoDetalleBO()
                    {
                        IdMaterialEstado = idMaterialEstado_PorEditar,
                        ComentarioSubida = "",
                        UrlArchivo = "",
                        FechaSubida = null,
                        IdFur = null,
                        DireccionEntrega = "",
                        FechaEntrega = null,
                        NombreArchivo = "",
                        IdMaterialVersion = item.IdMaterialVersion,
                        UsuarioCreacion = MaterialPespecifico.NombreUsuario,
                        UsuarioModificacion = MaterialPespecifico.NombreUsuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        Estado = true
                    };
                    materialPEspecifico.ListaMaterialPespecificoDetalle.Add(materialPespecificoDetalle);
                }

                if (!materialPEspecifico.HasErrors)
                {
                    _repMaterialPespecifico.Insert(materialPEspecifico);
                }
                else
                {
                    return BadRequest(materialPEspecifico.GetErrors());
                }
                var listaMaterialAsociacionAccion = _repMaterialAsociacionAccion.GetBy(x => x.IdMaterialTipo == MaterialPespecifico.IdMaterialTipo);
                var listaMaterialAsociacionCriterioVerificacion = _repMaterialAsociacionCriterioVerificacion.GetBy(x => x.IdMaterialTipo == MaterialPespecifico.IdMaterialTipo).ToList();
                var materialVersionAlumno = listaMaterialAsociacionVersion.Where(x => x.IdMaterialVersion == 2).FirstOrDefault();
                var materialAccionProveedor = listaMaterialAsociacionAccion.Where(x => x.IdMaterialAccion == 2).FirstOrDefault();
                if (materialVersionAlumno != null && materialAccionProveedor != null && listaMaterialAsociacionCriterioVerificacion.Count > 0)
                {
                    var listaMaterialDetalle = _repMaterialPespecificoDetalle.ObtenerDetalleMaterialPEspecifico(materialPEspecifico.Id, materialAccionProveedor.IdMaterialAccion, materialVersionAlumno.IdMaterialVersion);
                    MaterialCriterioVerificacionDetalleBO materialCriterioVerificacionDetalle;
                    foreach (var material in listaMaterialDetalle)
                    {
                        foreach (var item in listaMaterialAsociacionCriterioVerificacion)
                        {
                            materialCriterioVerificacionDetalle = new MaterialCriterioVerificacionDetalleBO()
                            {
                                IdMaterialPespecificoDetalle = material.IdMaterialPEspecificoDetalle,
                                IdMaterialCriterioVerificacion = item.IdMaterialCriterioVerificacion,
                                EsAprobado = false,
                                Estado = true,
                                UsuarioCreacion = MaterialPespecifico.NombreUsuario,
                                UsuarioModificacion = MaterialPespecifico.NombreUsuario,
                                FechaCreacion = DateTime.Now,
                                FechaModificacion = DateTime.Now
                            };
                            _repMaterialCriterioVerificacionDetalle.Insert(materialCriterioVerificacionDetalle);
                        }
                    }
                }

                var listaGrupoEdicion = _repPespecifico.ObtenerGrupoEdicionDisponible(MaterialPespecifico.IdPEspecifico);
                var filtros = new
                {
                    ListaGrupoEdicion = listaGrupoEdicion
                };
                return Ok(filtros);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Wilber Choque
        /// Fecha: 11/07/2021
        /// Versión: 1.0
        /// <summary>
        /// Actualiza el material de un PEspecifico
        /// </summary>
        /// <returns>Lista de objetos de clase FiltroDTO</returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult Actualizar([FromBody] MaterialPespecificoDTO MaterialPespecifico)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if (!_repMaterialPespecifico.Exist(MaterialPespecifico.Id))
                {
                    return BadRequest("Material por programa especifico no existente!");
                }
                if (!_repMaterialTipo.Exist(MaterialPespecifico.IdMaterialTipo))
                {
                    return BadRequest("Tipo de material no existente!");
                }

                var materialPespecifico = _repMaterialPespecifico.FirstById(MaterialPespecifico.Id);
                materialPespecifico.IdMaterialTipo = MaterialPespecifico.IdMaterialTipo;
                materialPespecifico.Grupo = MaterialPespecifico.Grupo;
                materialPespecifico.GrupoEdicion = MaterialPespecifico.GrupoEdicion;
                materialPespecifico.GrupoEdicionOrden = MaterialPespecifico.GrupoEdicionOrden;
                materialPespecifico.UsuarioModificacion = MaterialPespecifico.NombreUsuario;
                materialPespecifico.FechaModificacion = DateTime.Now;
                if (!materialPespecifico.HasErrors)
                {
                    _repMaterialPespecifico.Update(materialPespecifico);
                }
                else
                {
                    return BadRequest(materialPespecifico.GetErrors());
                }
                var listaGrupoEdicion = _repPespecifico.ObtenerGrupoEdicionDisponible(MaterialPespecifico.IdPEspecifico);
                var filtros = new
                {
                    ListaGrupoEdicion = listaGrupoEdicion
                };
                return Ok(filtros);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Wilber Choque
        /// Fecha: 11/07/2021
        /// Versión: 1.0
        /// <summary>
        /// Elimina el material de un PEspecifico
        /// </summary>
        /// <returns>Bool</returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult Eliminar([FromBody] EliminarDTO MaterialPespecifico)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if (!_repMaterialPespecifico.Exist(MaterialPespecifico.Id))
                {
                    return BadRequest("Material por programa especifico no existente");
                }
                _repMaterialPespecifico.Delete(MaterialPespecifico.Id, MaterialPespecifico.NombreUsuario);

                var listaMaterialPEspecificoDetalle = _repMaterialPespecificoDetalle.GetBy(x => x.IdMaterialPespecifico == MaterialPespecifico.Id).ToList();
                _repMaterialPespecificoDetalle.Delete(listaMaterialPEspecificoDetalle.Select(x => x.Id), MaterialPespecifico.NombreUsuario);

                var listaMaterialCriterioVerificacionDetalle = _repMaterialCriterioVerificacionDetalle.GetBy(x => x.IdMaterialPespecificoDetalle == MaterialPespecifico.Id).ToList();
                foreach (var item in listaMaterialCriterioVerificacionDetalle)
                {
                    _repMaterialCriterioVerificacionDetalle.Delete(item.Id, MaterialPespecifico.NombreUsuario);
                }
                //_repMaterialVersion.Delete(_repMaterialVersion.GetBy(x => x.IdMaterialPespecificoSesion == MaterialPespecificoSesion.Id).Select(x => x.Id), MaterialPespecificoSesion.NombreUsuario);
                return Ok(true);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Wilber Choque
        /// Fecha: 11/07/2021
        /// Versión: 1.0
        /// <summary>
        /// Aprobar la version de los materiales
        /// </summary>
        /// <param name="MaterialPespecificoDetalle">Objeto de clase AprobarMaterialVersionDTO</param>
        /// <returns>Bool</returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult AprobarMaterialVersion([FromBody] AprobarMaterialVersionDTO MaterialPespecificoDetalle)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if (!_repMaterialPespecificoDetalle.Exist(MaterialPespecificoDetalle.Id))
                {
                    return BadRequest("Version de material no existente");
                }
                var materialPespecificoDetalle = _repMaterialPespecificoDetalle.FirstById(MaterialPespecificoDetalle.Id);
                materialPespecificoDetalle.IdMaterialEstado = 3;//aprobado
                materialPespecificoDetalle.UsuarioAprobacion = MaterialPespecificoDetalle.NombreUsuario;
                materialPespecificoDetalle.FechaAprobacion = DateTime.Now;
                materialPespecificoDetalle.UsuarioModificacion = MaterialPespecificoDetalle.NombreUsuario;
                materialPespecificoDetalle.FechaModificacion = DateTime.Now;
                if (!materialPespecificoDetalle.HasErrors)
                {
                    _repMaterialPespecificoDetalle.Update(materialPespecificoDetalle);
                }
                else
                {
                    return BadRequest(materialPespecificoDetalle.GetErrors());
                }
                return Ok(true);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Wilber Choque
        /// Fecha: 11/07/2021
        /// Versión: 1.0
        /// <summary>
        /// Desaprueba la version del material
        /// </summary>
        /// <param name="MaterialPespecificoDetalle">Objeto de clase AprobarMaterialVersionDTO</param>
        /// <returns>Bool</returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult DesaprobarMaterialVersion([FromBody] AprobarMaterialVersionDTO MaterialPespecificoDetalle)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if (!_repMaterialPespecificoDetalle.Exist(MaterialPespecificoDetalle.Id))
                {
                    return BadRequest("Version de material no existente");
                }
                var materialPespecificoDetalle = _repMaterialPespecificoDetalle.FirstById(MaterialPespecificoDetalle.Id);
                materialPespecificoDetalle.IdMaterialEstado = 4;//observado
                materialPespecificoDetalle.UsuarioModificacion = MaterialPespecificoDetalle.NombreUsuario;
                materialPespecificoDetalle.FechaModificacion = DateTime.Now;
                if (!materialPespecificoDetalle.HasErrors)
                {
                    _repMaterialPespecificoDetalle.Update(materialPespecificoDetalle);
                }
                else
                {
                    return BadRequest(materialPespecificoDetalle.GetErrors());
                }
                return Ok(true);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult NotificarMaterialVersionAlumnoPorCorreo([FromBody] NotificarMaterialVersionDTO MaterialPespecificoDetalle)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if (!_repMaterialPespecificoDetalle.Exist(MaterialPespecificoDetalle.Id))
                {
                    return BadRequest("Version de material no existente");
                }
                var materialPespecificoDetalle = _repMaterialPespecificoDetalle.FirstById(MaterialPespecificoDetalle.Id);
                var materialPespecifico = _repMaterialPespecifico.FirstById(materialPespecificoDetalle.IdMaterialPespecifico);
                //logica enviar por correo a todos los alumnos 
                var listaOportunidades = new List<ValorIntDTO>();
                listaOportunidades = _repMatriculaCabecera.ObtenerAlumnosMatriculaProgramaEspecificoGrupo(materialPespecifico.IdPespecifico, materialPespecifico.Grupo);

                //1117    Envío de Material Digital para la Modalidad Presencial
                var idPlantilla = 1117;
                var listaGmailCorreo = new List<GmailCorreoBO>();

                foreach (var item in listaOportunidades)
                {
                    var _reemplazoEtiquetaPlantilla = new ReemplazoEtiquetaPlantillaBO(_integraDBContext)
                    {
                        IdOportunidad = item.Valor,
                        IdPlantilla = idPlantilla,
                        IdPEspecifico = materialPespecifico.IdPespecifico,//validar
                        Grupo = materialPespecifico.Grupo,//validar
                        IdMaterialPEspecificoDetalle = materialPespecificoDetalle.Id,
                        ListaIdMaterialPEspecificoDetalle = new List<int>()//en el caso que envie una lista de materiales a enviar
                    };
                    _reemplazoEtiquetaPlantilla.ReemplazarEtiquetas();

                    var emailCalculado = _reemplazoEtiquetaPlantilla.EmailReemplazado;
                    List<string> correosPersonalizadosCopiaOculta = new List<string>
                    {
                        "lhuallpa@bsginstitute.com",
                        "gchirinos@bsginstitute.com",
                        "aarcana@bsginstitute.com"
                    };

                    var oportunidad = _repOportunidad.FirstById(item.Valor);
                    var personal = _repPersonal.FirstById(oportunidad.IdPersonalAsignado);
                    var alumno = _repAlumno.FirstById(oportunidad.IdAlumno);


                    List<string> correosPersonalizados = new List<string>
                    {
                        alumno.Email1
                    };
                    TMKMailDataDTO mailDataPersonalizado = new TMKMailDataDTO
                    {
                        Sender = personal.Email,
                        //Sender = personal.Email,
                        //Sender = "w.choque.itusaca@isur.edu.pe",
                        Recipient = string.Join(",", correosPersonalizados.Distinct()),
                        Subject = emailCalculado.Asunto,
                        Message = emailCalculado.CuerpoHTML,
                        Cc = "",
                        Bcc = string.Join(",", correosPersonalizadosCopiaOculta.Distinct()),
                        AttachedFiles = emailCalculado.ListaArchivosAdjuntos
                    };
                    var mailServie = new TMK_MailServiceImpl();

                    mailServie.SetData(mailDataPersonalizado);
                    mailServie.SendMessageTask();

                    //logica envio
                    var gmailCorreo = new GmailCorreoBO
                    {
                        IdEtiqueta = 1,//sent:1 , inbox:2
                        Asunto = emailCalculado.Asunto,
                        Fecha = DateTime.Now,
                        EmailBody = emailCalculado.CuerpoHTML,
                        Seen = false,
                        Remitente = personal.Email,
                        Cc = "",
                        Bcc = "",
                        Destinatarios = string.Join(",", correosPersonalizados.Distinct()),
                        IdPersonal = personal.Id,
                        Estado = true,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        UsuarioCreacion = MaterialPespecificoDetalle.NombreUsuario,
                        UsuarioModificacion = MaterialPespecificoDetalle.NombreUsuario,
                        IdClasificacionPersona = oportunidad.IdClasificacionPersona
                    };
                    listaGmailCorreo.Add(gmailCorreo);
                }

                materialPespecificoDetalle.IdMaterialEstado = 5;//enviado

                materialPespecificoDetalle.UsuarioEnvio = MaterialPespecificoDetalle.NombreUsuario;
                materialPespecificoDetalle.FechaEnvio = DateTime.Now;

                materialPespecificoDetalle.UsuarioModificacion = MaterialPespecificoDetalle.NombreUsuario;
                materialPespecificoDetalle.FechaModificacion = DateTime.Now;
                if (!materialPespecificoDetalle.HasErrors)
                {
                    _repMaterialPespecificoDetalle.Update(materialPespecificoDetalle);
                }
                else
                {
                    return BadRequest(materialPespecificoDetalle.GetErrors());
                }

                var _repGmailCorreo = new GmailCorreoRepositorio(_integraDBContext);
                _repGmailCorreo.Insert(listaGmailCorreo);

                return Ok(true);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult NotificarListaMaterialVersionAlumnoPorCorreo([FromBody] NotificarListaMaterialVersionDTO MaterialPespecificoDetalle)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                //if (!_repMaterialPespecificoDetalle.Exist(idMaterialPEspecificoDetalle))
                //{
                //    return BadRequest("Version de material no existente");
                //}
                var materialPespecificoDetalle = _repMaterialPespecificoDetalle.FirstById(MaterialPespecificoDetalle.ListaIdMaterialPEspecificoDetalle.FirstOrDefault());//nos traemos el 1ro
                var materialPespecifico = _repMaterialPespecifico.FirstById(materialPespecificoDetalle.IdMaterialPespecifico);
                //logica enviar por correo a todos los alumnos 
                var listaOportunidades = new List<ValorIntDTO>();
                listaOportunidades = _repMatriculaCabecera.ObtenerAlumnosMatriculaProgramaEspecificoGrupo(materialPespecifico.IdPespecifico, materialPespecifico.Grupo);

                //1117    Envío de Material Digital para la Modalidad Presencial
                var idPlantilla = 1117;
                var listaGmailCorreo = new List<GmailCorreoBO>();

                foreach (var item in listaOportunidades)
                {
                    var _reemplazoEtiquetaPlantilla = new ReemplazoEtiquetaPlantillaBO(_integraDBContext)
                    {
                        IdOportunidad = item.Valor,
                        IdPlantilla = idPlantilla,
                        IdPEspecifico = 0,//validar
                        Grupo = 0,//validar
                        IdMaterialPEspecificoDetalle = 0,
                        ListaIdMaterialPEspecificoDetalle = MaterialPespecificoDetalle.ListaIdMaterialPEspecificoDetalle//en el caso que envie una lista de materiales a enviar
                    };
                    _reemplazoEtiquetaPlantilla.ReemplazarEtiquetas();

                    var emailCalculado = _reemplazoEtiquetaPlantilla.EmailReemplazado;
                    List<string> correosPersonalizadosCopiaOculta = new List<string>
                    {
                        "lhuallpa@bsginstitute.com",
                        "gchirinos@bsginstitute.com",
                        "aarcana@bsginstitute.com"
                    };

                    var oportunidad = _repOportunidad.FirstById(item.Valor);
                    var personal = _repPersonal.FirstById(oportunidad.IdPersonalAsignado);
                    var alumno = _repAlumno.FirstById(oportunidad.IdAlumno);


                    List<string> correosPersonalizados = new List<string>
                    {
                        alumno.Email1
                    };
                    TMKMailDataDTO mailDataPersonalizado = new TMKMailDataDTO
                    {
                        Sender = personal.Email,
                        //Sender = personal.Email,
                        //Sender = "w.choque.itusaca@isur.edu.pe",
                        Recipient = string.Join(",", correosPersonalizados.Distinct()),
                        Subject = emailCalculado.Asunto,
                        Message = emailCalculado.CuerpoHTML,
                        Cc = "",
                        Bcc = string.Join(",", correosPersonalizadosCopiaOculta.Distinct()),
                        AttachedFiles = emailCalculado.ListaArchivosAdjuntos
                    };
                    var mailServie = new TMK_MailServiceImpl();

                    mailServie.SetData(mailDataPersonalizado);
                    mailServie.SendMessageTask();

                    //logica envio
                    var gmailCorreo = new GmailCorreoBO
                    {
                        IdEtiqueta = 1,//sent:1 , inbox:2
                        Asunto = emailCalculado.Asunto,
                        Fecha = DateTime.Now,
                        EmailBody = emailCalculado.CuerpoHTML,
                        Seen = false,
                        Remitente = personal.Email,
                        Cc = "",
                        Bcc = "",
                        Destinatarios = string.Join(",", correosPersonalizados.Distinct()),
                        IdPersonal = personal.Id,
                        Estado = true,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        UsuarioCreacion = MaterialPespecificoDetalle.NombreUsuario,
                        UsuarioModificacion = MaterialPespecificoDetalle.NombreUsuario,
                        IdClasificacionPersona = oportunidad.IdClasificacionPersona
                    };
                    listaGmailCorreo.Add(gmailCorreo);
                }

                foreach (var item in MaterialPespecificoDetalle.ListaIdMaterialPEspecificoDetalle)
                {
                    var _materialPespecificoDetalle = _repMaterialPespecificoDetalle.FirstById(item);//nos traemos el 1ro
                    _materialPespecificoDetalle.IdMaterialEstado = 5;//enviado
                    _materialPespecificoDetalle.UsuarioEnvio = MaterialPespecificoDetalle.NombreUsuario;
                    _materialPespecificoDetalle.FechaEnvio = DateTime.Now;
                    _materialPespecificoDetalle.UsuarioModificacion = MaterialPespecificoDetalle.NombreUsuario;
                    _materialPespecificoDetalle.FechaModificacion = DateTime.Now;
                    if (!_materialPespecificoDetalle.HasErrors)
                    {
                        _repMaterialPespecificoDetalle.Update(_materialPespecificoDetalle);
                    }
                    else
                    {
                        return BadRequest(_materialPespecificoDetalle.GetErrors());
                    }
                }

                var _repGmailCorreo = new GmailCorreoRepositorio(_integraDBContext);
                _repGmailCorreo.Insert(listaGmailCorreo);
                return Ok(true);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult AsociarActualizarFur([FromBody] AsociarActualizarFurMaterialVersionDTO MaterialPespecificoDetalle)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                var _repFur = new FurRepositorio(_integraDBContext);

                var _materialPespecificoDetalle = _repMaterialPespecificoDetalle.FirstById(MaterialPespecificoDetalle.IdMaterialPEspecificoDetalle);

                _materialPespecificoDetalle.IdFur = MaterialPespecificoDetalle.IdFur;
                _materialPespecificoDetalle.FechaEntrega = MaterialPespecificoDetalle.FechaEntrega;
                _materialPespecificoDetalle.DireccionEntrega = MaterialPespecificoDetalle.DireccionEntrega;

                _materialPespecificoDetalle.UsuarioModificacion = MaterialPespecificoDetalle.NombreUsuario;
                _materialPespecificoDetalle.FechaModificacion = DateTime.Now;
                if (!_materialPespecificoDetalle.HasErrors)
                {
                    _repMaterialPespecificoDetalle.Update(_materialPespecificoDetalle);

                    //actualizar fur
                    PespecificoRepositorio _repPespecifico = new PespecificoRepositorio(_integraDBContext);
                    PespecificoSesionRepositorio _repPespecificoSesion = new PespecificoSesionRepositorio(_integraDBContext);
                    HistoricoProductoProveedorRepositorio _repHistoricoProductoProveedor = new HistoricoProductoProveedorRepositorio(_integraDBContext);
                    PEspecificoConsumoRepositorio _repPEspecificoConsumo = new PEspecificoConsumoRepositorio(_integraDBContext);
                    PersonalAreaTrabajoRepositorio _repPersonalAreaTrabajo = new PersonalAreaTrabajoRepositorio(_integraDBContext);
                    ProductoRepositorio _repProducto = new ProductoRepositorio(_integraDBContext);
                    PlanContableRepositorio _repPlanContable = new PlanContableRepositorio(_integraDBContext);

                    var detalleFur = _repHistoricoProductoProveedor.ObtenerDetalleFUR(MaterialPespecificoDetalle.IdProducto.Value, MaterialPespecificoDetalle.IdProveedor.Value);
                    var estado = false;
                    var semana = obtenerNumeroSemana(MaterialPespecificoDetalle.FechaEntrega.Value);
                    var producto = _repProducto.FirstById(MaterialPespecificoDetalle.IdProducto.Value);
                    var planContable = _repPlanContable.FirstBy(x => x.Cuenta == long.Parse(producto.CuentaGeneral));

                    var fur = _repFur.FirstById(MaterialPespecificoDetalle.IdFur.Value);
                    fur.IdFurTipoPedido = detalleFur.IdFurTipoPedido;
                    fur.IdProductoPresentacion = detalleFur.IdProductoPresentacion;
                    fur.IdMonedaPagoReal = detalleFur.IdMoneda;
                    fur.NumeroCuenta = detalleFur.NumeroCuenta;
                    fur.Descripcion = planContable.Descripcion;
                    fur.PrecioUnitarioMonedaOrigen = detalleFur.Precio;
                    fur.PrecioTotalMonedaOrigen = Convert.ToDecimal(detalleFur.Precio * MaterialPespecificoDetalle.Cantidad.Value);
                    fur.PrecioUnitarioDolares = detalleFur.PrecioDolares;
                    fur.PrecioTotalDolares = Convert.ToDecimal(detalleFur.PrecioDolares * MaterialPespecificoDetalle.Cantidad.Value);
                    fur.IdProveedor = MaterialPespecificoDetalle.IdProveedor;
                    fur.Cuenta = detalleFur.CuentaDescripcion;
                    fur.IdProducto = MaterialPespecificoDetalle.IdProducto;
                    fur.NumeroSemana = semana;
                    fur.Cantidad = MaterialPespecificoDetalle.Cantidad.Value;
                    fur.Descripcion = detalleFur.Descripcion;
                    fur.UsuarioModificacion = MaterialPespecificoDetalle.NombreUsuario;
                    fur.FechaModificacion = DateTime.Now;
                    fur.IdMonedaProveedor = detalleFur.IdMoneda;
                    fur.IdFurFaseAprobacion1 = 1;
                    fur.Monto = fur.PrecioTotalMonedaOrigen;
                    fur.PagoMonedaOrigen = detalleFur.PrecioOrigen * MaterialPespecificoDetalle.Cantidad;
                    fur.PagoDolares = detalleFur.PrecioDolares * MaterialPespecificoDetalle.Cantidad;
                    fur.MontoProyectado = fur.PrecioTotalMonedaOrigen;
                    fur.Monto = fur.PrecioTotalMonedaOrigen;
                    estado = _repFur.Update(fur);
                }
                else
                {
                    return BadRequest(_materialPespecificoDetalle.GetErrors());
                }
                return Ok(true);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult NotificarMaterialVersionAlumnoImpresoPorCorreoAProveedor([FromBody] NotificarMaterialVersionDTO MaterialPespecifico)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if (!_repMaterialPespecifico.Exist(MaterialPespecifico.Id))
                {
                    return BadRequest("Version de material no existente");
                }
                var materialPespecifico = _repMaterialPespecifico.FirstById(MaterialPespecifico.Id);

                var listaMaterialPEspecifoMismoGrupoEdicion = _repMaterialPespecifico.GetBy(x => x.IdPespecifico == materialPespecifico.IdPespecifico && x.GrupoEdicion == materialPespecifico.GrupoEdicion).ToList();

                var listaIdMaterialPEspecifoMismoGrupoEdicion = listaMaterialPEspecifoMismoGrupoEdicion.Select(x => x.Id).ToList();

                var listaIdMaterialVersion = new List<int>() {
                    2,//Material para alumno - impreso
                };

                var listaIdMaterialEstado = new List<int>() {
                    3,//aprobado
                };

                var listaMaterialPespecificoDetalle = _repMaterialPespecificoDetalle.GetBy(x => listaIdMaterialPEspecifoMismoGrupoEdicion.Contains(x.IdMaterialPespecifico) && listaIdMaterialVersion.Contains(x.IdMaterialVersion) && listaIdMaterialEstado.Contains(x.IdMaterialEstado) && !string.IsNullOrEmpty(x.UrlArchivo));

                var materialPEspecificoDetalleParaProveedorImpresion = listaMaterialPespecificoDetalle.FirstOrDefault();

                //1222   Envio solicitud impresión a proveedor
                var idPlantilla = 1222;

                var _reemplazoEtiquetaPlantilla = new ReemplazoEtiquetaPlantillaBO(_integraDBContext)
                {
                    IdPlantilla = idPlantilla,
                    IdPEspecifico = materialPespecifico.IdPespecifico,//validar
                    Grupo = materialPespecifico.Grupo,//validar
                    IdMaterialPEspecificoDetalle = materialPEspecificoDetalleParaProveedorImpresion.Id,// materialPespecificoDetalle.Id,
                    ListaIdMaterialPEspecificoDetalle = new List<int>()//en el caso que envie una lista de materiales a enviar
                };
                _reemplazoEtiquetaPlantilla.ReemplazarEtiquetasProveedor();

                var emailCalculado = _reemplazoEtiquetaPlantilla.EmailReemplazado;
                List<string> correosPersonalizadosCopiaOculta = new List<string>
                {
                    "lhuallpa@bsginstitute.com",
                };

                var fur = _repFur.FirstById(materialPEspecificoDetalleParaProveedorImpresion.IdFur.Value);

                var proveedor = _repProveedor.FirstById(fur.IdProveedor.Value);

                List<string> correosPersonalizados = new List<string>
                {
                    proveedor.Email
                    //"lhuallpa@bsginstitute.com",
                };
                TMKMailDataDTO mailDataPersonalizado = new TMKMailDataDTO
                {
                    Sender = "modpru@bsginstitute.com",
                    //Sender = personal.Email,
                    Recipient = string.Join(",", correosPersonalizados.Distinct()),
                    Subject = emailCalculado.Asunto,
                    Message = emailCalculado.CuerpoHTML,
                    Cc = "",
                    Bcc = string.Join(",", correosPersonalizadosCopiaOculta.Distinct()),
                    AttachedFiles = emailCalculado.ListaArchivosAdjuntos
                };
                var mailServie = new TMK_MailServiceImpl();

                mailServie.SetData(mailDataPersonalizado);
                mailServie.SendMessageTask();

                foreach (var item in listaMaterialPespecificoDetalle)
                {
                    var _materialPespecificoDetalle = _repMaterialPespecificoDetalle.FirstById(item.Id);
                    _materialPespecificoDetalle.IdMaterialEstado = 5;//enviado
                    _materialPespecificoDetalle.UsuarioEnvio = MaterialPespecifico.NombreUsuario;
                    _materialPespecificoDetalle.FechaEnvio = DateTime.Now;
                    _materialPespecificoDetalle.UsuarioModificacion = MaterialPespecifico.NombreUsuario;
                    _materialPespecificoDetalle.FechaModificacion = DateTime.Now;
                    if (!_materialPespecificoDetalle.HasErrors)
                    {
                        _repMaterialPespecificoDetalle.Update(_materialPespecificoDetalle);
                    }
                    else
                    {
                        return BadRequest(_materialPespecificoDetalle.GetErrors());
                    }
                }

                return Ok(true);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        private int obtenerNumeroSemana(DateTime fecha)
        {
            var d = fecha;
            CultureInfo cul = CultureInfo.CurrentCulture;

            var firstDayWeek = cul.Calendar.GetWeekOfYear(
                d,
                CalendarWeekRule.FirstDay,
                DayOfWeek.Monday);

            int weekNum = cul.Calendar.GetWeekOfYear(
                d,
                CalendarWeekRule.FirstDay,
                DayOfWeek.Monday);

            return weekNum;
        }

    }
}