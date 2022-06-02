using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.DTOs.Scode.DTOs.Planificacion;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    /// BO: Transversal/Proveedor
    /// Autor: Edgar Serruto .
    /// Fecha: 29/06/2021
    /// <summary>
    /// Columnas y funciones de la tabla T_Proveedor
    /// </summary>
    public class ProveedorBO : BaseBO
    {
        /// Propiedades		                    Significado
        /// -------------	                    -----------------------
        /// IdTipoContribuyente                 Id de Tipo de Contribuyente       
        /// IdDocumentoIdentidad                Id de Documento de Identidad
        /// NroDocIdentidad                     Número de Documento de Identidad
        /// RazonSocial                         Razón Social
        /// Nombre1                             Nombre Nro 1
        /// Nombre2                             Nombre Nro 2
        /// ApePaterno                          Apellido Paterno
        /// ApeMaterno                          Apellido Materno
        /// Direccion                           Dirección
        /// Descripcion                         Descripción
        /// IdCiudad                            Id de ciudad
        /// Telefono                            Teléfono
        /// Email                               Email
        /// Celular1                            Celular Nro 1
        /// Celular2                            Celular Nro 2
        /// Contacto1                           Contacto Nro 1
        /// Contacto2                           Contacto Nro 2
        /// IdMigracion                         Id de Migración
        /// IdPrestacionRegistro                Id de Prestación de Registros
        /// IdTipoImpuestoPreferido             Id de Tipo de Impuesto Preferido
        /// IdRetencionPreferido                Id de Retención Preferido
        /// IdDetraccionPreferido               Id de Detracción Preferido
        /// IdPersonalAsignado                  Id de Personal Asignado
        public int IdTipoContribuyente { get; set; }
        public int IdDocumentoIdentidad { get; set; }
        public string NroDocIdentidad { get; set; }
        public string RazonSocial { get; set; }
        public string Nombre1 { get; set; }
        public string Nombre2 { get; set; }
        public string ApePaterno { get; set; }
        public string ApeMaterno { get; set; }
        public string Direccion { get; set; }
        public string Descripcion { get; set; }
        public int? IdCiudad { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string Celular1 { get; set; }
        public string Celular2 { get; set; }
        public string Contacto1 { get; set; }
        public string Contacto2 { get; set; }
        public Guid? IdMigracion { get; set; }
        public int? IdPrestacionRegistro { get; set; }
        public int? IdTipoImpuestoPreferido { get; set; }
        public int? IdRetencionPreferido { get; set; }
        public int? IdDetraccionPreferido { get; set; }
        public int? IdPersonalAsignado { get; set; }
        public string Alias { get; set; }

        public List<ProveedorTipoServicioBO> ListaProveedorTipoServicio { get; set; }
        private ProveedorRepositorio _repProveedorRep;
        private ProveedorCuentaBancoRepositorio _repCuentaBancoRep;
        private ProveedorTipoServicioRepositorio _repProveedorTipoServicio;
        private PersonaBO _persona;

        //public CookieContainer cokkie;

        public ProveedorBO()
        {
            ListaProveedorTipoServicio = new List<ProveedorTipoServicioBO>();
        }

        public ProveedorBO(integraDBContext _integraDBContext)
        {
            ListaProveedorTipoServicio = new List<ProveedorTipoServicioBO>();
            _repProveedorRep = new ProveedorRepositorio(_integraDBContext);
            _repCuentaBancoRep = new ProveedorCuentaBancoRepositorio(_integraDBContext);
            _persona = new PersonaBO(_integraDBContext);
            _repProveedorTipoServicio = new ProveedorTipoServicioRepositorio(_integraDBContext);
            //cokkie = new CookieContainer();
        }

        /// <summary>
        /// Inserta el Proveedor y las cuentas asociadas a ese proveedor
        /// </summary>
        /// <param name="objetoEgresoAprobado"></param>
        /// <param name="idRecCancelado"></param>
        /// <returns></returns>
        public bool InsertarProveedorCuentaBanco(ProveedorDTO proveedor, List<ProveedorCuentaBancoDTO> listaCuentaBanco)
        {
            try
            {

                var IdProveedorEmailRepetido = _repProveedorRep.ObtenerProveedorEliminadoEmailRepetido(proveedor.Email);
                int? IdPersonaClasificacion = null;

                ProveedorBO objProveedor = new ProveedorBO();
                if (IdProveedorEmailRepetido == null || IdProveedorEmailRepetido == 0)
                {
                    objProveedor.IdTipoContribuyente = proveedor.IdTipoContribuyente;
                    objProveedor.IdDocumentoIdentidad = proveedor.IdDocumentoIdentidad;
                    objProveedor.NroDocIdentidad = proveedor.NroDocumento;
                    objProveedor.RazonSocial = proveedor.RazonSocial;
                    objProveedor.Nombre1 = proveedor.Nombre1;
                    objProveedor.Nombre2 = proveedor.Nombre2;
                    objProveedor.ApePaterno = proveedor.ApePaterno;
                    objProveedor.ApeMaterno = proveedor.ApeMaterno;
                    objProveedor.Direccion = proveedor.Direccion;
                    objProveedor.Descripcion = proveedor.Descripcion == null ? "" : proveedor.Descripcion;
                    objProveedor.IdCiudad = proveedor.IdCiudad;
                    objProveedor.Telefono = proveedor.Telefono;
                    objProveedor.Email = proveedor.Email;
                    objProveedor.Celular1 = proveedor.Celular1;
                    objProveedor.Celular2 = proveedor.Celular2;
                    objProveedor.Contacto1 = proveedor.Contacto1;
                    objProveedor.Contacto2 = proveedor.Contacto2;

                    objProveedor.IdTipoImpuestoPreferido = proveedor.IdImpuesto;
                    objProveedor.IdRetencionPreferido = proveedor.IdRetencion;
                    objProveedor.IdDetraccionPreferido = proveedor.IdDetraccion;
                    objProveedor.IdPersonalAsignado = proveedor.IdPersonalAsignado;
                    objProveedor.Alias = proveedor.Alias;

                    objProveedor.Estado = true;
                    objProveedor.FechaCreacion = DateTime.Now;
                    objProveedor.FechaModificacion = DateTime.Now;
                    objProveedor.UsuarioCreacion = proveedor.UsuarioModificacion;
                    objProveedor.UsuarioModificacion = proveedor.UsuarioModificacion;
                    
                    foreach (var item in proveedor.ListaProveedorTipoServicio)
                    {
                        var proveedorTipoServicio = new ProveedorTipoServicioBO
                        {
                            IdTipoServicio = item.IdTipoServicio,
                            UsuarioCreacion = proveedor.UsuarioModificacion,
                            UsuarioModificacion = proveedor.UsuarioModificacion,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            Estado = true
                        };
                        objProveedor.ListaProveedorTipoServicio.Add(proveedorTipoServicio);
                    }

                    _repProveedorRep.Insert(objProveedor);
                    IdPersonaClasificacion=_persona.InsertarPersona(objProveedor.Id, Aplicacion.Base.Enums.Enums.TipoPersona.Proveedor, objProveedor.UsuarioCreacion);

                }
                else
                {
                    _repProveedorRep.ActivarProveedor(IdProveedorEmailRepetido.Value);
                    objProveedor = _repProveedorRep.FirstById(IdProveedorEmailRepetido.Value);

                    objProveedor.IdTipoContribuyente = proveedor.IdTipoContribuyente;
                    objProveedor.IdDocumentoIdentidad = proveedor.IdDocumentoIdentidad;
                    objProveedor.NroDocIdentidad = proveedor.NroDocumento;
                    objProveedor.RazonSocial = proveedor.RazonSocial;
                    objProveedor.Nombre1 = proveedor.Nombre1;
                    objProveedor.Nombre2 = proveedor.Nombre2;
                    objProveedor.ApePaterno = proveedor.ApePaterno;
                    objProveedor.ApeMaterno = proveedor.ApeMaterno;
                    objProveedor.Direccion = proveedor.Direccion;
                    objProveedor.Descripcion = proveedor.Descripcion == null ? "" : proveedor.Descripcion;
                    objProveedor.IdCiudad = proveedor.IdCiudad;
                    objProveedor.Telefono = proveedor.Telefono;
                    objProveedor.Email = proveedor.Email;
                    objProveedor.Celular1 = proveedor.Celular1;
                    objProveedor.Celular2 = proveedor.Celular2;
                    objProveedor.Contacto1 = proveedor.Contacto1;
                    objProveedor.Contacto2 = proveedor.Contacto2;
                    objProveedor.IdPersonalAsignado = proveedor.IdPersonalAsignado;
                    objProveedor.Alias = proveedor.Alias;
                    objProveedor.Estado = true;
                    objProveedor.FechaCreacion = DateTime.Now;
                    objProveedor.FechaModificacion = DateTime.Now;
                    objProveedor.UsuarioCreacion = proveedor.UsuarioModificacion;
                    objProveedor.UsuarioModificacion = proveedor.UsuarioModificacion;

                    foreach (var item in proveedor.ListaProveedorTipoServicio)
                    {
                        var proveedorTipoServicio = new ProveedorTipoServicioBO
                        {
                            IdTipoServicio = item.IdTipoServicio,
                            UsuarioCreacion = proveedor.UsuarioModificacion,
                            UsuarioModificacion = proveedor.UsuarioModificacion,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            Estado = true
                        };
                        objProveedor.ListaProveedorTipoServicio.Add(proveedorTipoServicio);
                    }

                    _repProveedorRep.Update(objProveedor);
                    IdPersonaClasificacion=_persona.InsertarPersona(objProveedor.Id, Aplicacion.Base.Enums.Enums.TipoPersona.Proveedor, objProveedor.UsuarioCreacion);

                }
                if (IdPersonaClasificacion == null) {
                    throw new Exception("Error al insertar el Tipo Persona Clasificacion");
                }

                foreach (var cuenta in listaCuentaBanco)
                {

                    ProveedorCuentaBancoBO cuentaBanco = new ProveedorCuentaBancoBO();
                    cuentaBanco.IdProveedor = objProveedor.Id;
                    cuentaBanco.IdEntidadFinanciera = cuenta.IdEntidadFinanciera;
                    cuentaBanco.IdTipoCuentaBanco = cuenta.IdTipoCuentaBanco;
                    cuentaBanco.NroCuenta = cuenta.NroCuenta;
                    cuentaBanco.CuentaInterbancaria = cuenta.CuentaInterbancaria;
                    cuentaBanco.IdMoneda = cuenta.IdMoneda;
                    cuentaBanco.Estado = true;
                    cuentaBanco.FechaCreacion = DateTime.Now;
                    cuentaBanco.FechaModificacion = DateTime.Now;
                    cuentaBanco.UsuarioCreacion = cuenta.UsuarioModificacion;
                    cuentaBanco.UsuarioModificacion = cuenta.UsuarioModificacion;

                    _repCuentaBancoRep.Insert(cuentaBanco);
                }

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        /// <summary>
        /// Actualiza el Proveedor y las cuentas asociadas a ese proveedor inserta o las actualiza
        /// </summary>
        /// <param name="objetoEgresoAprobado"></param>
        /// <param name="idRecCancelado"></param>
        /// <returns></returns>
        public void ActualizarProveedorCuentaBanco(ProveedorDTO proveedor, List<ProveedorCuentaBancoDTO> listaCuentaBanco)
        {
            try
            {

                var objProveedor = _repProveedorRep.FirstById(proveedor.Id);

                if (objProveedor == null)
                    throw new Exception("No se encontro el registro de 'Proveedor' que se quiere actualizar");

                _repProveedorTipoServicio.EliminacionLogicoPorPlantilla(proveedor.Id, proveedor.UsuarioModificacion, proveedor.ListaProveedorTipoServicio.Select(x => x.IdTipoServicio).ToList());

                objProveedor.RazonSocial = proveedor.RazonSocial;
                objProveedor.Nombre1 = proveedor.Nombre1;
                objProveedor.Nombre2 = proveedor.Nombre2;
                objProveedor.ApePaterno = proveedor.ApePaterno;
                objProveedor.ApeMaterno = proveedor.ApeMaterno;
                objProveedor.Direccion = proveedor.Direccion;
                objProveedor.Descripcion = proveedor.Descripcion == null ? "" : proveedor.Descripcion;
                objProveedor.IdPrestacionRegistro = proveedor.IdPrestacionRegistro;
                objProveedor.IdCiudad = proveedor.IdCiudad;
                objProveedor.Telefono = proveedor.Telefono;
                objProveedor.Email = proveedor.Email;
                objProveedor.Celular1 = proveedor.Celular1;
                objProveedor.Celular2 = proveedor.Celular2;
                objProveedor.Contacto1 = proveedor.Contacto1;
                objProveedor.Contacto2 = proveedor.Contacto2;

                objProveedor.IdTipoImpuestoPreferido = proveedor.IdImpuesto;
                objProveedor.IdRetencionPreferido = proveedor.IdRetencion;
                objProveedor.IdDetraccionPreferido = proveedor.IdDetraccion;
                objProveedor.IdPersonalAsignado = proveedor.IdPersonalAsignado;

                objProveedor.FechaModificacion = DateTime.Now;
                objProveedor.UsuarioModificacion = proveedor.UsuarioModificacion;
                objProveedor.Alias = proveedor.Alias;
                foreach (var item in proveedor.ListaProveedorTipoServicio)
                {
                    ProveedorTipoServicioBO proveedorTipoServicio;
                    if (_repProveedorTipoServicio.Exist(x => x.IdTipoServicio == item.IdTipoServicio && x.IdProveedor == proveedor.Id))
                    {
                        proveedorTipoServicio = _repProveedorTipoServicio.FirstBy(x => x.IdTipoServicio == item.IdTipoServicio && x.IdProveedor == proveedor.Id);
                        proveedorTipoServicio.IdTipoServicio = item.IdTipoServicio;
                        proveedorTipoServicio.UsuarioModificacion = proveedor.UsuarioModificacion;
                        proveedorTipoServicio.FechaModificacion = DateTime.Now;
                    }
                    else
                    {
                        proveedorTipoServicio = new ProveedorTipoServicioBO
                        {
                            IdTipoServicio = item.IdTipoServicio,
                            UsuarioCreacion = proveedor.UsuarioModificacion,
                            UsuarioModificacion = proveedor.UsuarioModificacion,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            Estado = true
                        };
                    }
                    objProveedor.ListaProveedorTipoServicio.Add(proveedorTipoServicio);
                }

                _repProveedorRep.Update(objProveedor);

                foreach (var cuenta in listaCuentaBanco)
                {
                    if (cuenta.Id == 0)
                    {

                        ProveedorCuentaBancoBO cuentaBanco = new ProveedorCuentaBancoBO();
                        cuentaBanco.IdProveedor = objProveedor.Id;
                        cuentaBanco.IdEntidadFinanciera = cuenta.IdEntidadFinanciera;
                        cuentaBanco.IdTipoCuentaBanco = cuenta.IdTipoCuentaBanco;
                        cuentaBanco.NroCuenta = cuenta.NroCuenta;
                        cuentaBanco.CuentaInterbancaria = cuenta.CuentaInterbancaria;
                        cuentaBanco.IdMoneda = cuenta.IdMoneda;
                        cuentaBanco.Estado = true;
                        cuentaBanco.FechaCreacion = DateTime.Now;
                        cuentaBanco.FechaModificacion = DateTime.Now;
                        cuentaBanco.UsuarioCreacion = proveedor.UsuarioModificacion;
                        cuentaBanco.UsuarioModificacion = proveedor.UsuarioModificacion;

                        _repCuentaBancoRep.Insert(cuentaBanco);
                    }
                    else
                    {
                        var cuentaProveedor = _repCuentaBancoRep.FirstById(cuenta.Id);

                        if (cuentaProveedor == null)
                            throw new Exception("No se encontro el registro de 'CuentaProveedor' que se quiere actualizar");
                        cuentaProveedor.NroCuenta = cuenta.NroCuenta;
                        cuentaProveedor.CuentaInterbancaria = cuenta.CuentaInterbancaria;
                        cuentaProveedor.FechaModificacion = DateTime.Now;
                        cuentaProveedor.UsuarioModificacion = proveedor.UsuarioModificacion;

                        _repCuentaBancoRep.Update(cuentaProveedor);
                    }
                }


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public string GenerarCondicionReporteForo(ReporteRevisionDocenteDTO filtro)
        {
            try
            {
                string condicion = string.Empty;
                var filtros = new
                {
                    ListaArea = filtro.ListaArea == null ? "" : string.Join(",", filtro.ListaArea.Select(x => x)),
                    ListaSubArea = filtro.ListaSubArea == null ? "" : string.Join(",", filtro.ListaSubArea.Select(x => x)),
                    ListaProgramaGeneral = filtro.ListaProgramaGeneral == null ? "" : string.Join(",", filtro.ListaProgramaGeneral.Select(x => x)),
                    ListaProveedor = filtro.ListaDocente == null ? "" : string.Join(",", filtro.ListaDocente)
                };
                if (filtros.ListaArea.Length > 0)
                {
                    if (condicion.Length > 0)
                    {
                        condicion = condicion + " AND IdArea IN (" + filtros.ListaArea + ")";
                    }
                    else
                    {
                        condicion = condicion + " IdArea IN (" + filtros.ListaArea + ")";
                    }
                }
                if (filtros.ListaSubArea.Length > 0)
                {
                    if (condicion.Length > 0)
                    {
                        condicion = condicion + " AND IdSubArea IN (" + filtros.ListaSubArea + ")";
                    }
                    else
                    {
                        condicion = condicion + " IdSubArea IN (" + filtros.ListaSubArea + ")";
                    }
                }
                if (filtros.ListaProgramaGeneral.Length > 0)
                {
                    if (condicion.Length > 0)
                    {
                        condicion = condicion + " AND IdPGeneral IN (" + filtros.ListaProgramaGeneral + ")";
                    }
                    else
                    {
                        condicion = condicion + " IdPGeneral IN (" + filtros.ListaProgramaGeneral + ")";
                    }
                }
                if (filtros.ListaProveedor.Length > 0)
                {
                    if (condicion.Length > 0)
                    {
                        condicion = condicion + " AND IdProveedor IN (" + filtros.ListaProveedor + ")";
                    }
                    else
                    {
                        condicion = condicion + " IdProveedor IN (" + filtros.ListaProveedor + ")";
                    }
                }
                return condicion;              
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void GenerarReporteProyecto(ReporteRevisionDocenteDTO filtro)
        {
            try
            {



            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
