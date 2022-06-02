using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.DTOs.Scode.DTOs.GestionPersonas;
using BSI.Integra.Aplicacion.DTOs.Scode.DTOs.Planificacion;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    /// Repositorio: Finanzas/Proveedor
    /// Autor: Britsel C. - Jose V.
    /// Fecha: 28/04/2021
    /// <summary>
    /// Repositorio para consultas de fin.T_Proveedor
    /// </summary>
    public class ProveedorRepositorio : BaseRepository<TProveedor, ProveedorBO>
    {
        #region Metodos Base
        public ProveedorRepositorio() : base()
        {
        }
        public ProveedorRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<ProveedorBO> GetBy(Expression<Func<TProveedor, bool>> filter)
        {
            IEnumerable<TProveedor> listado = base.GetBy(filter);
            List<ProveedorBO> listadoBO = new List<ProveedorBO>();
            foreach (var itemEntidad in listado)
            {
                ProveedorBO objetoBO = Mapper.Map<TProveedor, ProveedorBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public ProveedorBO FirstById(int id)
        {
            try
            {
                TProveedor entidad = base.FirstById(id);
                ProveedorBO objetoBO = new ProveedorBO();
                Mapper.Map<TProveedor, ProveedorBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public ProveedorBO FirstBy(Expression<Func<TProveedor, bool>> filter)
        {
            try
            {
                TProveedor entidad = base.FirstBy(filter);
                ProveedorBO objetoBO = Mapper.Map<TProveedor, ProveedorBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(ProveedorBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TProveedor entidad = MapeoEntidad(objetoBO);

                bool resultado = base.Insert(entidad);
                if (resultado)
                    AsignacionId(entidad, objetoBO);

                return resultado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(IEnumerable<ProveedorBO> listadoBO)
        {
            try
            {
                foreach (var objetoBO in listadoBO)
                {
                    bool resultado = Insert(objetoBO);
                    if (resultado == false)
                        return false;
                }

                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool Update(ProveedorBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TProveedor entidad = MapeoEntidad(objetoBO);

                bool resultado = base.Update(entidad);
                if (resultado)
                    AsignacionId(entidad, objetoBO);

                return resultado;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool Update(IEnumerable<ProveedorBO> listadoBO)
        {
            try
            {
                foreach (var objetoBO in listadoBO)
                {
                    bool resultado = Update(objetoBO);
                    if (resultado == false)
                        return false;
                }

                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        private void AsignacionId(TProveedor entidad, ProveedorBO objetoBO)
        {
            try
            {
                if (entidad != null && objetoBO != null)
                {
                    objetoBO.Id = entidad.Id;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        private TProveedor MapeoEntidad(ProveedorBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TProveedor entidad = new TProveedor();
                entidad = Mapper.Map<ProveedorBO, TProveedor>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos

                if (objetoBO.ListaProveedorTipoServicio != null && objetoBO.ListaProveedorTipoServicio.Count > 0)
                {
                    foreach (var hijo in objetoBO.ListaProveedorTipoServicio)
                    {
                        TProveedorTipoServicio entidadHijo = new TProveedorTipoServicio();
                        entidadHijo = Mapper.Map<ProveedorTipoServicioBO, TProveedorTipoServicio>(hijo,
                            opt => opt.ConfigureMap(MemberList.None));
                        entidad.TProveedorTipoServicio.Add(entidadHijo);
                    }
                }

                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        #endregion

        /// <summary>
        /// Se obtiene el Id y el Nombre de Proveedor filtrado por Producto
        /// </summary>
        /// <param name="idProducto"></param>
        /// <returns></returns>
        public List<ProveedorPorProductoAutocompleteDTO> ObtenerProveedorPorProducto(int idProducto)
        {
            try
            {
                List<ProveedorPorProductoAutocompleteDTO> proveedorPorProducto = new List<ProveedorPorProductoAutocompleteDTO>();
                var proveedorPorProductoDB = _dapper.QuerySPDapper("pla.SP_ObtenerProveedorPorProducto", new { idProducto });
                if (!string.IsNullOrEmpty(proveedorPorProductoDB) && !proveedorPorProductoDB.Contains("[]"))
                {
                    proveedorPorProducto = JsonConvert.DeserializeObject<List<ProveedorPorProductoAutocompleteDTO>>(proveedorPorProductoDB);
                }
                return proveedorPorProducto;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// Obtiene el nombre de Proveedor que este asociado a un Producto
        /// </summary>
        /// <param name="valor"></param>
        /// <returns></returns>
        public List<FiltroDTO> ObtenerNombreProveedorAutocomplete(string valor)
        {
            try
            {
                List<FiltroDTO> proveedor = new List<FiltroDTO>();
                var _query = string.Empty;
                _query = "SELECT Id ,Nombre FROM FIN.V_ObtenerProveedorHistorico WHERE Nombre LIKE CONCAT('%',@valor,'%') ORDER By Nombre ASC";
                var proveedorDB = _dapper.QueryDapper(_query, new { valor });
                if (!string.IsNullOrEmpty(proveedorDB) && !proveedorDB.Contains("[]"))
                {
                    proveedor = JsonConvert.DeserializeObject<List<FiltroDTO>>(proveedorDB);
                }
                return proveedor;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        /// <summary>
        /// Obtiene el [Id, Ruc, RazonSocial] de Proveedor para Autocomplete (obtiene por RUC)
        /// </summary>
        /// <param name="valor"></param>
        /// <returns></returns>
        public List<ProveedorRucRazonSocialDTO> ObtenerProveedorPorRuc(string valor)
        {
            try
            {
                List<ProveedorRucRazonSocialDTO> proveedor = new List<ProveedorRucRazonSocialDTO>();
                var _query = string.Empty;
                _query = "SELECT Id , NroDocIdentidad, RazonSocial, IdTipoImpuesto,IdDetraccion, IdRetencion,IdPais FROM fin.V_ObtenerProveedorRazonSocialRucNombres WHERE NroDocIdentidad LIKE '%" + valor + "%' and Estado=1";
                var proveedorDB = _dapper.QueryDapper(_query, new { valor });
                if (!string.IsNullOrEmpty(proveedorDB) && !proveedorDB.Contains("[]"))
                {
                    proveedor = JsonConvert.DeserializeObject<List<ProveedorRucRazonSocialDTO>>(proveedorDB);
                }
                return proveedor;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        /// <summary>
        /// Extrae el nombre de proveedor de acuerdo a su ID
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public string ObtenerNombreProveedorById(int Id)
        {
            try
            {

                ValorStringDTO proveedor = new ValorStringDTO() ;
                var _query = string.Empty;
                _query = "SELECT Nombre as Valor FROM FIN.V_ObtenerProveedorHistorico WHERE Id=@Id";
                var proveedorDB = _dapper.FirstOrDefault(_query, new { Id });
                if (!string.IsNullOrEmpty(proveedorDB) && !proveedorDB.Contains("[]"))
                {
                    proveedor = JsonConvert.DeserializeObject<ValorStringDTO>(proveedorDB);
                }
                return proveedor.Valor;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Obtiene el nombre y Ruc de todos los proveedores, que esten activos segun la descripcion que se ingreso ya sea Ruc o Nombres del Proveedor
        /// </summary>
        /// <param name="valor"></param>
        /// <returns></returns>
        public List<FiltroRucProveedorDTO> ObtenerProveedorRucAutocomplete(string valor)
        {
            try
            {
                List<FiltroRucProveedorDTO> proveedor = new List<FiltroRucProveedorDTO>();
                var _query = string.Empty;
                _query = "SELECT Id , concat('(',Documento,')',' ',Nombre) as Nombre,Documento as Ruc FROM FIN.V_ObtenerProveedor WHERE Nombre LIKE CONCAT('%',@valor,'%') or Documento LIKE CONCAT('%',@valor,'%') ORDER By Nombre ASC";
                var proveedorDB = _dapper.QueryDapper(_query, new { valor });
                if (!string.IsNullOrEmpty(proveedorDB) && !proveedorDB.Contains("[]"))
                {
                    proveedor = JsonConvert.DeserializeObject<List<FiltroRucProveedorDTO>>(proveedorDB);
                }
                return proveedor;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Obtiene lista de proveedores para combo de programa especifico FUR
        /// </summary>
        /// <returns></returns>
        public List<ProveedorProductoDTO> ObtenerProveedorParaCombo()
        {
            try
            {
                var query = "SELECT Id, Nombre, Simbolo, NombreMoneda, Precio, IdProducto, Presentacion, IdHistorico, Version FROM fin.V_ObtenerInformacionProductoProveedor WHERE Estado = 1";
                var proveedores = _dapper.QueryDapper(query, null);

                return JsonConvert.DeserializeObject<List<ProveedorProductoDTO>>(proveedores);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// Obtiene datos para llenar grilla
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public List<ProveedorDTO> ObtenerTodoProveedorById(int? Id)
        {
            try
            {
                var camposTabla = "SELECT Id,IdTipoContribuyente,TipoContribuyente,IdDocumentoIdentidad,DocumentoIdentidad,NroDocumento,Proveedor,RazonSocial,ApePaterno,ApeMaterno,Nombre1,Nombre2,Descripcion,Direccion,IdPais,Pais,IdCiudad,Ciudad,Telefono,Email,Celular1,Celular2,Contacto1,Contacto2,IdPrestacionRegistro,Criterio1,Criterio2,Criterio3,Criterio4,Criterio5,FechaModificacion,UsuarioModificacion, IdImpuesto,IdRetencion,IdDetraccion,IdPersonalAsignado,Alias ";
                List<ProveedorDTO> Proveedor = new List<ProveedorDTO>();
                var _query = camposTabla + "FROM  [fin].[V_ObtenerDatosProveedor] where Estado=1 order by Id desc";
                if (Id != null && Id != 0)
                {
                    _query = camposTabla + "FROM  [fin].[V_ObtenerDatosProveedor] where Estado=1 And Id=" + Id + " ORDER BY Id DESC";
                }
                var ProveedorDB = _dapper.QueryDapper(_query, null);
                if (!ProveedorDB.Contains("[]") && !string.IsNullOrEmpty(ProveedorDB))
                {
                    Proveedor = JsonConvert.DeserializeObject<List<ProveedorDTO>>(ProveedorDB);
                }
                return Proveedor;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
		/// Obtiene lista de proveedores para combo
		/// </summary>
		/// <returns></returns>
		public List<FiltroDTO> ObtenerProveedorCombo()
        {
            try
            {
                var query = "SELECT Id, Nombre FROM fin.V_ObtenerInformacionProductoProveedor WHERE Estado = 1";
                var proveedores = _dapper.QueryDapper(query, null);

                return JsonConvert.DeserializeObject<List<FiltroDTO>>(proveedores);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene La lista de Proveedores con estado de Cuenta Pagado o Pendiente (Usado para combobox)
        /// </summary>
        /// <returns></returns>
        public List<FiltroDTO> ObtenerProveedoresConEstadoCuentaPagadoPendiente()
        {
            try
            {
                var query = "SELECT Id, Nombre FROM fin.V_ProveedorConEstadoCuentaPagadoPendiente";
                var proveedores = _dapper.QueryDapper(query, null);

                return JsonConvert.DeserializeObject<List<FiltroDTO>>(proveedores);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        ///Repositorio: ProveedorRepositorio
        ///Autor: Jose Villena.
        ///Fecha: 28/04/2021
        /// <summary>
        /// Obtiene el email1 del Proveedor
        /// </summary>
        /// <param name="id"> Id Proveedor </param>
        /// <returns>Email 1 del Proveedor</returns>
        public string ObtenerEmail(int id)
        {
            try
            {
                return this.GetBy(x => x.Id == id).FirstOrDefault().Email;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public int? ObtenerProveedorEliminadoEmailRepetido(string email)
        {
            try
            {
                Dictionary<string, int> proveedor = new Dictionary<string, int>();
                var _query = "SELECT Id FROM fin.T_Proveedor where estado = 0 and Email=@Email";
                var proveedorDB = _dapper.FirstOrDefault(_query, new { Email = email });
                if (!string.IsNullOrEmpty(proveedorDB) && !proveedorDB.Contains("[]") && !proveedorDB.Contains("null"))
                {
                    proveedor = JsonConvert.DeserializeObject<Dictionary<string, int>>(proveedorDB);
                }
                return proveedor.Select(x => x.Value).FirstOrDefault();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool ActivarProveedor(int Id)
        {
            try
            {
                _dapper.QueryDapper("UPDATE fin.T_Proveedor set Estado=1 where Id=@Id", new { Id = Id });
                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Se obtiene para filtro
        /// </summary>
        /// <returns></returns>
        public List<FiltroDTO> ObtenerTodoFiltro()
        {
            try
            {
                var listaProveedor = new List<FiltroDTO>();
                var query = "SELECT Id, Nombre FROM fin.V_ObtenerInformacionProductoProveedor WHERE Estado = 1";
                var resultado = _dapper.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    listaProveedor = JsonConvert.DeserializeObject<List<FiltroDTO>>(resultado);
                }
                return listaProveedor;
            }
            catch (Exception e) 
            {
                throw e;
            }
        }

        /// <summary>
        /// Se obtiene para filtro por tipo servicio
        /// </summary>
        /// <param name="idTipoServicio"></param>
        /// <returns></returns>
        public List<FiltroDTO> ObtenerTodoFiltroPorTipoServicio(int idTipoServicio)
        {
            try
            {
                var listaProveedor = new List<FiltroDTO>();
                var query = $@"
                        SELECT Id, 
                               Nombre
                        FROM fin.V_ObtenerProveedorPorTipoServicio
                        WHERE IdTipoServicio = @idTipoServicio
                              AND EstadoProveedor = 1
                              AND EstadoProveedorTipoServicio = 1
                              AND EstadoTipoServicio = 1;
                        ";
                var resultado = _dapper.QueryDapper(query, new { idTipoServicio });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    listaProveedor = JsonConvert.DeserializeObject<List<FiltroDTO>>(resultado);
                }
                return listaProveedor;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<ProveedorHonorarioComboDTO> ObtenerNombreProveedorParaHonorario()
        {
            try
            {
                var listaProveedor = new List<ProveedorHonorarioComboDTO>();
                var query = $@"
                        SELECT Id, 
                               Nombre
                        FROM fin.V_Obtener_ProveedorParaHonorario
                        ORDER BY Nombre
                        ";
                var resultado = _dapper.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    listaProveedor = JsonConvert.DeserializeObject<List<ProveedorHonorarioComboDTO>>(resultado);
                }
                return listaProveedor;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// Repositorio: ProveedorPersonalRepositorio
        /// Autor: -
        /// Fecha: 09/07/2021
        /// <summary>
		/// Obtiene lista de proveedores para Filtro
		/// </summary>
		/// <returns>List<FiltroDTO></returns>
		public List<FiltroDTO> ObtenerProveedorParaFiltro()
        {
            try
            {
                var query = "SELECT Id, Nombre FROM [fin].[V_ProveedorFiltro] WHERE Estado = 1";
                var proveedores = _dapper.QueryDapper(query, null);
                return JsonConvert.DeserializeObject<List<FiltroDTO>>(proveedores);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Repositorio: ProveedorPersonalRepositorio
        /// Autor: Luis Huallpa - Britsel Calluchi.
        /// Fecha: 24/04/2021
        /// <summary>
        /// Obtiene lista de proveedores para Filtro
        /// </summary>
        /// <returns> List<FiltroConvocatoriaPersonalDTO> </returns>
        public List<FiltroConvocatoriaPersonalDTO> ObtenerProveedoresConvocatoriaPersonal()
        {
            try
            {
                List<FiltroConvocatoriaPersonalDTO> listaProveedoresConvocatorias = new List<FiltroConvocatoriaPersonalDTO>();
                var query = "SELECT Id, IdProveedor, IdTipoServicio, EstadoPTS, EstadoP, RazonSocial FROM fin.V_TProveedorTipoServicio_ObtenerProveedorAvisoLaboral WHERE IdTipoServicio = 47 AND EstadoP = 1";
                var res = _dapper.QueryDapper(query, null);

                if (!string.IsNullOrEmpty(res) && !res.Contains("[]"))
                {
                    listaProveedoresConvocatorias = JsonConvert.DeserializeObject<List<FiltroConvocatoriaPersonalDTO>>(res);
                }
                return listaProveedoresConvocatorias;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Repositorio: ProveedorRepositorio
        /// Autor: Edgar Serruto.
        /// Fecha: 28/06/2021
        /// <summary>
        /// Genera Reporte de Revision de Docentes para Foro
        /// </summary>
        /// <param name="condicion">Cadena de condiciones</param>
        /// <returns> List<RespuestaReporteRevisionDocenteDTO> </returns>
        public List<RespuestaReporteRevisionDocenteDTO> GenerarReporteRevisionForo(string condicion)
        {
            try
            {
                List<RespuestaReporteRevisionDocenteDTO> listaReporteForo = new List<RespuestaReporteRevisionDocenteDTO>();
                var query = string.Empty;
                if (condicion.Length > 0)
                {
                    query = "SELECT IdArea, Area, IdSubArea, SubArea, IdPGeneral, PGeneral, IdProveedor, Nombre, CategoriaRevision, IdPersonalAsignado, PersonalAsignado, IdModalidadCurso, ModalidadCurso FROM pla.V_TPGeneralForoAsignacionProveedor_GenerarReporte WHERE " + condicion;
                }
                else
                {
                    query = "SELECT IdArea, Area, IdSubArea, SubArea, IdPGeneral, PGeneral, IdProveedor, Nombre, CategoriaRevision, IdPersonalAsignado, PersonalAsignado, IdModalidadCurso, ModalidadCurso FROM pla.V_TPGeneralForoAsignacionProveedor_GenerarReporte";
                }
                var respuesta = _dapper.QueryDapper(query, null);

                if (!string.IsNullOrEmpty(respuesta) && !respuesta.Contains("[]"))
                {
                    listaReporteForo = JsonConvert.DeserializeObject<List<RespuestaReporteRevisionDocenteDTO>>(respuesta);
                }
                return listaReporteForo;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Repositorio: ProveedorRepositorio
        /// Autor: Edgar Serruto.
        /// Fecha: 28/06/2021
        /// <summary>
        /// Genera Reporte de Revision de Docentes para Proyectos
        /// </summary>
        /// <param name="condicion">Cadena de condiciones</param>
        /// <returns> List<RespuestaReporteRevisionDocenteDTO> </returns>
        public List<RespuestaReporteRevisionDocenteDTO> GenerarReporteProyecto(string condicion)
        {
            try
            {
                List<RespuestaReporteRevisionDocenteDTO> listaReporteProyecto = new List<RespuestaReporteRevisionDocenteDTO>();
                var query = string.Empty;
                if (condicion.Length > 0)
                {
                    query = "SELECT IdArea, Area, IdSubArea, SubArea, IdPGeneral, PGeneral, IdProveedor, Nombre, CategoriaRevision, IdPersonalAsignado, PersonalAsignado, IdModalidadCurso, ModalidadCurso FROM pla.V_TPgeneralProyectoAplicacionProveedor_GenerarReporte WHERE " + condicion;
                }
                else
                {
                    query = "SELECT IdArea, Area, IdSubArea, SubArea, IdPGeneral, PGeneral, IdProveedor, Nombre, CategoriaRevision, IdPersonalAsignado, PersonalAsignado, IdModalidadCurso, ModalidadCurso FROM pla.V_TPgeneralProyectoAplicacionProveedor_GenerarReporte";
                }
                var respuesta = _dapper.QueryDapper(query, null);

                if (!string.IsNullOrEmpty(respuesta) && !respuesta.Contains("[]"))
                {
                    listaReporteProyecto = JsonConvert.DeserializeObject<List<RespuestaReporteRevisionDocenteDTO>>(respuesta);
                }
                return listaReporteProyecto;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Repositorio: ProveedorRepositorio
        /// Autor: Edgar Serruto
        /// Fecha: 25/06/2021
        /// <summary>
        /// Obtiene lista de proveedores para Filtro
        /// </summary>
        /// <param name="condicion">Cadena de condición para vista </param>
        /// <returns> List<CorreoProveedorPersonalDTO> </returns>
        public List<CorreoProveedorPersonalDTO> ObtenerProveedoresConvocatoriaPersonal(string condicion)
        {
            try
            {
                List<CorreoProveedorPersonalDTO> listaProveedoresConvocatorias = new List<CorreoProveedorPersonalDTO>();
                var query = "SELECT IdProveedor, Nombre, ProveedorEmail, IdPersonalAsignado, PersonalEmail, UrlFirmaPersonal FROM fin.V_TProveedor_ObtenerPersonalAsignadoProveedor WHERE IdProveedor IN (" + condicion + ")";
                var respuesta = _dapper.QueryDapper(query, null);

                if (!string.IsNullOrEmpty(respuesta) && !respuesta.Contains("[]"))
                {
                    listaProveedoresConvocatorias = JsonConvert.DeserializeObject<List<CorreoProveedorPersonalDTO>>(respuesta);
                }
                return listaProveedoresConvocatorias;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
