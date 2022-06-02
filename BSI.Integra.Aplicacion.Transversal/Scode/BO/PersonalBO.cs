using BSI.Integra.Persistencia.SCode.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using System.Linq;

namespace BSI.Integra.Aplicacion.Transversal.BO
{    
    /// BO: Gestion de Personas/Personal
    /// Autor: Fischer Valdez - Esthephany Tanco - Wilber Choque - Gian Miranda
    /// Fecha: 06/02/2021
    /// <summary>
    /// BO para el obtener informacion de la matricula cabecera
    /// </summary>
    public class PersonalBO : BaseBO
    {
        /// Propiedades	                        Significado
        /// -----------	                        ------------
        /// Nombres		                        Nombres del personal
        /// Apellidos                           Apellidos del personal
        /// Rol		                            Rol del personal
        /// TipoPersonal		                Cadena con el tipo de personal (Coordinador, Asesor, otro)
        /// Email		                        Correo del personal
        /// AreaAbrev                           Codigo del area perteneciente al personal
        /// Anexo                               Anexo del personal
        /// IdJefe                              Id del jefe inmediato
        /// Central                             Central de llamadas asociadas al personal
        /// Activo                              Flag para identificar si el personal esta activo o no
        /// ApellidoPaterno                     Apellido paterno del personal
        /// ApellidoMaterno                     Apellido materno del personal
        /// IdSexo                              Id del sexo del personal del personal (PK de la tabla gp.T_Sexo)
        /// IdEstadocivil                       Id del estado civil del personal (PK de la tabla gp.T_EstadoCivil)
        /// FechaNacimiento                     Fecha de nacimiento del personal
        /// IdPaisNacimiento                    Id del pais de nacimiento del personal (PK de la tabla conf.T_Pais)
        /// IdRegion                            Id de la region de la ciudad del personal (PK de la tabla conf.T_RegionCiudad)
        /// IdCiudad                            Id de la ciudad del personal (PK de la tabla conf.T_Ciudad)
        /// IdTipoDocumento                     Id del tipo de documento del personal (PK de la tabla pla.T_TipoDocumento)
        /// NumeroDocumento                     Numero del documento del personal
        /// AutogeneradoEssalud                 Codigo autogenerado del seguro ESSALUD
        /// IdTipoSangre                        Id del tipo de sangre del personal (PK de la tabla gp.T_TipoSangre)
        /// UrlFirmaCorreos                     Url de la imagen de la firma de correos
        /// IdGrupoProgramasCriticos            Id del grupo de programas criticos PK de la tabla (pla.T_GrupoFiltroProgramaCritico)
        /// IdCerrador                          Id del cerrador
        /// EsCerrador                          Flag para identificar si es un cerrador
        /// IdPaisDireccion                     Id del pais (Direccion)
        /// IdRegionDireccion                   Id de la region (Direccion)
        /// CiudadDireccion                     Id de la ciudad (Direccion)
        /// NombreDireccion                     Nombre de la direccion
        /// FijoReferencia                      Numero fijo de referencia
        /// MovilReferencia                     Movil de referencia
        /// EmailReferencia                     Email de referencia
        /// IdSistemaPensionario                Id del sistema pensionario (PK de la tabla gp.T_SistemaPensionario)
        /// IdEntidadSistemaPensionario         Id del sistema pensionario (PK de la tabla gp.T_EntidadSistemaPensionario)
        /// NombreCuspp                         Nombre Cuspp
        /// DistritoDireccion                   Cadena con el distrito (Direccion)
        /// ConEssalud                          Flag para determinar si tiene ESSALUD
        /// IdBusqueda                          Id de la busqueda
        /// AliasEmailAsesor                    Cadena con el alias del asesor
        /// Anexo3Cx                            Anexo3Cx del personal
        /// Id3Cx                               Id del 3Cx
        /// Password3Cx                         Contrasena del 3Cx
        /// Dominio                             Dominio
        /// IdFacebookPersonal                  Id del Facebook del personal
        /// IdMigracion                         Id de migracion V3 (Nulleable)
        /// UrlFoto                             Url en repositorio de la foto del personal
        /// AplicaFirmaHtml                     Flag para determinar si aplica la firma HTML
        /// FirmaHtml                           Firma HTML
        /// CargoFirmaHtml                      Cargo de la firma HTML
        /// IdPostulante                        Id de la tabla postulante(PK de la tabla gp.T_Postulante)
        /// PersonalLog                         Objeto del tipo PersonalLogBO
        /// IdPuestoTrabajoNivel                FK de T_PuestoTrabajoNivel
        /// IdPersonalArchivo                   FK de T_PersonalArchivo
        /// IdPersonalAreaTrabajo               FK de T_PersonalAreaTrabajo
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Rol { get; set; }
        public string TipoPersonal { get; set; }
        public string Email { get; set; }
        public string AreaAbrev { get; set; }
        public string Anexo { get; set; }
        public int? IdJefe { get; set; }
        public string Central { get; set; }
        public bool? Activo { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public int? IdSexo { get; set; }
        public int? IdEstadocivil { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public int? IdPaisNacimiento { get; set; }
        public int? IdRegion { get; set; }
        public int? IdCiudad { get; set; }
        public int? IdTipoDocumento { get; set; }
        public string NumeroDocumento { get; set; }
        public string AutogeneradoEssalud { get; set; }
        public int? IdTipoSangre { get; set; }
        public string UrlFirmaCorreos { get; set; }
        public int? IdGrupoProgramasCriticos { get; set; }
        public int? IdCerrador { get; set; }
        public bool? EsCerrador { get; set; }
        public int? IdPaisDireccion { get; set; }
        public int? IdRegionDireccion { get; set; }
        public string CiudadDireccion { get; set; }
        public string NombreDireccion { get; set; }
        public string FijoReferencia { get; set; }
        public string MovilReferencia { get; set; }
        public string EmailReferencia { get; set; }
        public int? IdSistemaPensionario { get; set; }
        public int? IdEntidadSistemaPensionario { get; set; }
        public string NombreCuspp { get; set; }
        public string DistritoDireccion { get; set; }
        public bool? ConEssalud { get; set; }
        public int? IdBusqueda { get; set; }
        public string AliasEmailAsesor { get; set; }
        public string Anexo3Cx { get; set; }
        public string Id3Cx { get; set; }
        public string Password3Cx { get; set; }
        public string Dominio { get; set; }
        public long? IdFacebookPersonal { get; set; }
        public int? IdMigracion { get; set; }
        public string UrlFoto { get; set; }
        public bool? AplicaFirmaHtml { get; set; }
        public string FirmaHtml { get; set; }
        public string CargoFirmaHtml { get; set; }
        public int? IdPostulante { get; set; }
        public int? UsuarioAsterisk { get; set; }
        public string ContrasenaAsterisk { get; set; }
        public int? IdPuestoTrabajoNivel { get; set; }
        public int? IdPersonalArchivo { get; set; }
        public int? IdPersonalAreaTrabajo { get; set; }
        public int? IdTableroComercialCategoriaAsesor { get; set; }
        public bool? DiscadorActivo { get; set; }
        public int? DiferenciaHoraria { get; set; }
        public List<PersonalLogBO> PersonalLog { get; set; }

        public string PrimerNombreApellidoPaterno { get {
                return ObtenerPrimerNombreApellidoPaterno();
            }
        }
       
        public string Telefono
        {
            get
            {
                return ObtenerTelefono();
            }
        }
        public string PrimerNombre
        {
            get
            {
                return ObtenerPrimerNombre();
            }
        }
        public string FirmaCorreoHTML
        {
            get
            {
                return ObtenerFirmaCorreo();
            }
        }
       
        public List<PersonalBO> ListaAsesorAutocomplete;

        private DapperRepository _dapperRepository;
        private PersonalRepositorio _repPersonal;

        public PersonalBO() : base()
        {
            
            _dapperRepository = new DapperRepository();
            ListaAsesorAutocomplete = new List<PersonalBO>();
            _repPersonal = new PersonalRepositorio();
        }

        /// <summary>
        /// Obtiene una lista de asesores filtrado por el nombre
        /// </summary>
        /// <param name="valor"></param>
        /// <returns> Vacio </returns>
        public void CargarAsesorAutocomplete(string valor)
        {
            string _query = string.Empty;
            _query = "SELECT P.id as Id,nombres as Nombres,apellidos as Apellidos,email as Email,NombreCompleto = Concat(apellidos ,' ' ,nombres) FROM gp.T_Personal P WHERE  P.rol = 'VENTAS' AND P.apellidos LIKE CONCAT('%',@valor,'%') AND P.estado = 1";
            var asesoresDB = _dapperRepository.QueryDapper(_query, new { valor });
            ListaAsesorAutocomplete = JsonConvert.DeserializeObject<List<PersonalBO>>(asesoresDB);
        }

        /// <summary>
        /// Obtiene el email de personal por Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Personal ObtenerEmailPersonalPorId(int id)
        {
            Personal personal = new Personal();
            var _query = string.Empty;
            _query = "SELECT Id AS Id, Email AS Email FROM GP.V_TPersonal_EmailPersonal WHERE Estado = 1 AND Id = @id";
            var personalDB = _dapperRepository.FirstOrDefault(_query, new { id });
            personal = JsonConvert.DeserializeObject<Personal>(personalDB);
            return personal;
        }

        /// <summary>
        /// Obtiene todos los personal con rol ventas
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<PersonalBO> ObtenerTodosAsesor()
        {
            List<PersonalBO> personal = new List<PersonalBO>();
            var _query = string.Empty;
            _query = "SELECT Id, Concat(Nombres,' ',Apellidos) AS NombreCompleto FROM com.V_TPersonal_ObtenerAsesores where Rol = 'VENTAS' AND (TipoPersonal IN ('ASESOR','Coordinador')) AND estado = 1";
            var personalDB = _dapperRepository.QueryDapper(_query, null);
            personal = JsonConvert.DeserializeObject<List<PersonalBO>>(personalDB);
            return personal;
        }

        /// <summary>
        /// Obtiene el primer nombre y apellido paterno del personal
        /// </summary>
        private string ObtenerPrimerNombreApellidoPaterno()
        {
            try
            {
                return _repPersonal.ObtenerPrimerNombreApellidoPaterno(this.Id);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
     
        private string ObtenerTelefono()
        {
            try
            {
                var telefono = "";

                //if (!string.IsNullOrEmpty(this.MovilReferencia))
                //{
                //    telefono = this.MovilReferencia;
                //}
                //else
                //{
                    if (this.Central == "192.168.0.20")
                    {
                        //aqp
                        telefono = "(51) 54 258787";
                    }
                    else
                    {
                        if (this.Central == "192.168.2.20")
                        {
                            //lima
                            telefono = "(51) 1 207 2770";
                        }
                        else
                        {
                            telefono = "(51) 54 258787";
                        }
                    }
                //}
                return telefono;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private string ObtenerFirmaCorreo()
        {
            try
            {

                var _repPersonal = new PersonalRepositorio();
                var personal = _repPersonal.ObtenerDatoPersonal(this.Id);
                var puestoTrabajo = _repPersonal.ObtenerPuestoTrabajo(this.Id);

                var firmaInicial = @"
                                    <span style='font-family:Arial, color:#575756; font-size: 13pt;'>Hasta Pronto</span> <br> <br><table style='width: 460px; font-size: 13pt; font-family: Arial; line-height:normal;' cellpadding='0' cellspacing='0'><tbody><tr ><td style='width: 120px; padding-top:0px; padding-bottom:10px; width:120px; vertical-align:top ' valign='top'> <a target='_blank'> <img border='0' style='width:120px; height:120px; border:1;' src='{T_Personal.UrlFoto}'> </a></td><td style='padding-left: 10px; border-right:8px solid; border-right-color:#afca0a; padding-right:5px; text-align: right; vertical-align:top;' valign='top'></td><td style='padding-left: 10px; width: 507px; font-size: 13pt; font-family: Arial; line-height: normal;' cellpadding='0' cellspacing='0' height='123'><table style='padding-left:0px; width: 400px; font-size: 13pt; font-family: Arial; line-height:normal;' cellpadding='0' cellspacing='0'><tbody><tr><td style='padding-bottom:20px; font-size: 19pt; font-family: Arial; color: #afca0a;'> <span style='font-family: Arial; color:#afca0a'> {T_Personal.PrimerNombreApellidoPaterno} </span></td></tr><tr><td style='padding-bottom:5px; font-size: 13pt; font-family: Arial; color: #575756;'> <span style='font-family: Arial; color:#575756' > {T_Personal.NombrePuestoTrabajo} </span></td></tr><tr><td style='padding-bottom:5px; font-size: 13pt; font-family: Arial; color: #575756;'> <span style='font-family: Arial; color:#575756' > BSG Institute </span></td></tr><tr ><td style='font-size: 13pt; font-family: Arial; color: #afca0a;'> <span style='font-family: Arial; color:#afca0a'> t: {T_Personal.Telefono} anexo {T_Personal.Anexo} </span></td></tr><tr><td style='font-size: 13pt; font-family: Arial; color: #afca0a;'> <span style='font-family: Arial; color: #afca0a;'>whatsapp: {T_Alumno.NroWhatsAppCoordinador}</span></td></tr></tbody></table></td></tr></tbody></table> <br> <br><table style='width: 460px; font-size: 11pt; font-family: Arial; line-height:normal;' cellpadding='0' cellspacing='0'><tr><td colspan='1' style='border-top:0px solid; border-top-color:#FFFFFF; width: 67px; padding-top:8px; font-family:Arial,sans-serif; color:#575756; text-align:justify; vertical-align: top;'> <span style='font-weight:bold; font-family: Arial; color: #575756; font-size:11pt;'> Per&uacute; </span></td><td colspan='1' style='border-top:0px solid; border-top-color:#FFFFFF; width: 480px; padding-top:8px; font-family:Arial,sans-serif; color:#575756; text-align:left;'> <span style='font-family: Arial; color: #575756; font-size:11pt;'> : Lima - Av. Jos&eacute; Pardo 650, Miraflores <br /> : Arequipa - Urb. Le&oacute;n XIII Calle 2 N 107, Cayma </span></td></tr><tr><td colspan='1' style='border-top:0px solid; border-top-color:#FFFFFF; width: 67px; padding-top:8px; font-family:Arial,sans-serif; color:#575756; text-align:justify;'> <span style='font-weight:bold; font-family: Arial; color: #575756; font-size:11pt;'> Colombia&nbsp; </span></td><td colspan='1' style='border-top:0px solid; border-top-color:#FFFFFF; width: 480px; padding-top:8px; font-family:Arial,sans-serif; color:#575756; text-align:left;'> <span style='font-family: Arial; color: #575756; font-size:11pt;'> : Bogot&aacute; - Av. Carrera 45 N 108-27 - Torre 1 Oficina 1008 </span></td></tr><tr><td colspan='1' style='border-top:0px solid; border-top-color:#FFFFFF; width: 67px; padding-top:8px; font-family:Arial,sans-serif; color:#575756; text-align:justify;'> <span style='font-weight:bold; font-family: Arial; color: #575756; font-size:11pt;'> Bolivia</span></td><td colspan='1' style='border-top:0px solid; border-top-color:#FFFFFF; width: 480px; padding-top:8px; font-family:Arial,sans-serif; color:#575756; text-align:left;'> <span style='font-family: Arial; color: #575756; font-size:11pt;'> : Santa Cruz de la Sierra - Av. Marcelo Terceros B&aacute;nzer 304</span></td></tr></table> <br><table style='width: 460px; font-size: 13pt; font-family: Arial; line-height:normal;' cellpadding='0' cellspacing='0'><td colspan='2' style='border-top:2px solid; border-top-color:#afca0a; width: 480px; padding-top:8px; font-family:Arial,sans-serif; color:#575756; text-align:justify;'> <span style='font-family: Arial; color:#575756'> <a href='https://bsginstitute.com/' target='_blank' style='text-decoration:none;'> <span style='font-size:13pt; font-family:Verdana; color:#575756;'> <span style='color:#575756; font-family:Verdana;'> www.bsginstitute.com</span> </span> </a> </span></td></table>
                                    ";
                
                if (firmaInicial.Contains("{T_Personal.PrimerNombreApellidoPaterno}"))
                {
                    firmaInicial = firmaInicial.Replace("{T_Personal.PrimerNombreApellidoPaterno}", personal.PrimerNombreApellidoPaterno);
                }
                //if (firmaInicial.Contains("{ T_Alumno.NroWhatsAppCoordinador}"))
                //{
                //    firmaInicial = firmaInicial.Replace("{T_Alumno.NroWhatsAppCoordinador}", personal.PrimerNombreApellidoPaterno);
                //}
                if (firmaInicial.Contains("{T_Personal.NombrePuestoTrabajo}"))
                {
                    firmaInicial = firmaInicial.Replace("{T_Personal.NombrePuestoTrabajo}", this.CargoFirmaHtml);
                }
                if (firmaInicial.Contains("{T_Personal.Telefono}"))
                {
                    firmaInicial = firmaInicial.Replace("{T_Personal.Telefono}", this.Telefono);
                }
                if (firmaInicial.Contains("{T_Personal.Anexo}"))
                {
                    firmaInicial = firmaInicial.Replace("{T_Personal.Anexo}", this.Anexo);
                }
                if (firmaInicial.Contains("{T_Personal.UrlFoto}"))
                {
                    firmaInicial = firmaInicial.Replace("{T_Personal.UrlFoto}", this.UrlFoto);
                }
                
                return firmaInicial;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Obtiene la firma del personal en imagen
        /// </summary>
        /// <param name="idCodigoPais">Codigo del pais</param>
        /// <param name="idCiudad">Id de la ciudad (PK de la tabla conf.T_Ciudad)</param>
        /// <returns>Cadena formateada de la imagen de la firma de correo</returns>
        public string ObtenerFirmaCorreoImagen(int? idCodigoPais = 0, int? idCiudad = 0)
        {
            try
            {
                var urlInicial = this.UrlFoto;
                if (idCodigoPais == 51 && idCiudad == 4)
                {
                    urlInicial += "pa";
                }
                else if (idCodigoPais == 51 && idCiudad == 14)
                {
                    urlInicial += "pl";
                }
                else if (idCodigoPais == 57)
                {
                    urlInicial += "c";
                }
                else if (idCodigoPais == 591)
                {
                    urlInicial += "b";
                }
                else {
                    urlInicial += "pl";
                }

                urlInicial += ".png";
                return urlInicial;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Obtiene el primer nombre
        /// </summary>
        /// <returns></returns>
        private string ObtenerPrimerNombre()
        {
            try
            {
                return _repPersonal.ObtenerPrimerNombre(this.Id);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public class Personal
        {
            public int Id { get; set; }
            public string Email { get; set; }
        }
        /// BO: Gestion de Personas/Personal
        /// Autor: Edgar Serruto
        /// Fecha: 13/07/2021
        /// <summary>
        /// Obtiene Personal Subordinado
        /// </summary>
        /// <returns>PersonalJefaturaIteradorDTO</returns>
        public PersonalJefaturaIteradorDTO  ObtenerPersonalEncargadoJefatura()
        {
            try
            {
                PersonalJefaturaIteradorDTO resultadoFinalBSGrupo = new PersonalJefaturaIteradorDTO();
                PersonalJefaturaIteradorDTO resultadoFinal = new PersonalJefaturaIteradorDTO();
                List<PersonalJefaturaIteradorDTO> respuesta = new List<PersonalJefaturaIteradorDTO>();
                PersonalJefaturaIteradorDTO agregar;
                var listaPersonalJefatura = _repPersonal.ObtenerPersonalJefatura();
                //Lista Adaptación para Gerencia
                var gerenciaPrincipal = listaPersonalJefatura.Where(x => x.IdPersonal == 213).FirstOrDefault();//Información de Gerente
                var listaJefaturaPrincipal = listaPersonalJefatura.Where(x => x.IdJefeInmediato == 213 && x.IdPersonal != 213).ToList();
                var casoParticular = listaPersonalJefatura.Where(x => x.IdPersonal == 13).FirstOrDefault();
                casoParticular.IdJefeInmediato = 213;
                listaJefaturaPrincipal.Add(casoParticular);

                resultadoFinal.IdPersonal = gerenciaPrincipal.IdPersonal;
                resultadoFinal.Personal = gerenciaPrincipal.Personal;
                resultadoFinal.PuestoTrabajo = gerenciaPrincipal.PuestoTrabajo;
                var grupoJefaturaPrincipal = listaJefaturaPrincipal.GroupBy(x => new { x.IdJefeInmediato }).Select(x => new PersonalJefaturaAgrupadoDTO
                {
                    IdJefeInmediato = x.Key.IdJefeInmediato,
                    PersonalACargo = x.GroupBy(y => new { y.IdPersonal, y.Personal, y.PuestoTrabajo }).Select(y => new PersonalJefaturaAsociadoDTO
                    {
                        IdPersonal = y.Key.IdPersonal,
                        Personal = y.Key.Personal,
                        PuestoTrabajo = y.Key.PuestoTrabajo
                    }).ToList(),
                }).FirstOrDefault();
                foreach (var jefe in grupoJefaturaPrincipal.PersonalACargo)
                {
                    agregar = new PersonalJefaturaIteradorDTO()
                    {
                        IdPersonal = jefe.IdPersonal.GetValueOrDefault(),
                        Personal = jefe.Personal,
                        PuestoTrabajo = jefe.PuestoTrabajo,
                        PersonalACargo = this.ObtenerSubordinado(jefe.IdPersonal.GetValueOrDefault(), listaPersonalJefatura, 0),
                    };
                    respuesta.Add(agregar);
                }
                respuesta = respuesta.OrderBy(x => x.Personal).ToList();
                resultadoFinal.PersonalACargo = respuesta;

                //Caso de Personal sin Jefe
                PersonalJefaturaIteradorDTO resultadoFinalSinJefe = new PersonalJefaturaIteradorDTO();
                List<PersonalJefaturaIteradorDTO> respuestaSinJefe = new List<PersonalJefaturaIteradorDTO>();
                var listaPersonalSinJefe = listaPersonalJefatura.Where(x => x.IdJefeInmediato == 0).ToList();
                var casoParticularQuitar = listaPersonalSinJefe.Where(x => x.IdPersonal == 13).FirstOrDefault();
                var casoGerenciQuitar = listaPersonalSinJefe.Where(x => x.IdPersonal == 213).FirstOrDefault();
                if (casoParticularQuitar != null) 
                {
                    listaPersonalSinJefe.Remove(casoParticular);
                }
                if (casoGerenciQuitar != null)
                {
                    listaPersonalSinJefe.Remove(casoGerenciQuitar);
                }
                resultadoFinalSinJefe.IdPersonal = 0;
                resultadoFinalSinJefe.Personal = "Sin Jefe";
                resultadoFinalSinJefe.PuestoTrabajo = " ";
                var grupoPersonalSinJefe = listaPersonalSinJefe.GroupBy(x => new { x.IdJefeInmediato }).Select(x => new PersonalJefaturaAgrupadoDTO
                {
                    IdJefeInmediato = x.Key.IdJefeInmediato,
                    PersonalACargo = x.GroupBy(y => new { y.IdPersonal, y.Personal, y.PuestoTrabajo }).Select(y => new PersonalJefaturaAsociadoDTO
                    {
                        IdPersonal = y.Key.IdPersonal,
                        Personal = y.Key.Personal,
                        PuestoTrabajo = y.Key.PuestoTrabajo
                    }).ToList(),
                }).FirstOrDefault();
                foreach (var jefe in grupoPersonalSinJefe.PersonalACargo)
                {
                    agregar = new PersonalJefaturaIteradorDTO()
                    {
                        IdPersonal = jefe.IdPersonal.GetValueOrDefault(),
                        Personal = jefe.Personal,
                        PuestoTrabajo = jefe.PuestoTrabajo,
                        PersonalACargo = this.ObtenerSubordinado(jefe.IdPersonal.GetValueOrDefault(), listaPersonalJefatura, 0),
                    };
                    respuestaSinJefe.Add(agregar);
                }
                respuestaSinJefe = respuestaSinJefe.OrderBy(x => x.Personal).ToList();
                resultadoFinalSinJefe.PersonalACargo = respuestaSinJefe;

                List<PersonalJefaturaIteradorDTO> auxiliar = new List<PersonalJefaturaIteradorDTO>();
                auxiliar.Add(resultadoFinal);
                auxiliar.Add(resultadoFinalSinJefe);
                resultadoFinalBSGrupo.IdPersonal = 0;
                resultadoFinalBSGrupo.Personal = "BS_GRUPO_ADAPTAR_JERARQUIA";
                resultadoFinalBSGrupo.PuestoTrabajo = "BS_GRUPO_ADAPTAR_JERARQUIA";
                resultadoFinalBSGrupo.PersonalACargo = auxiliar;
                return resultadoFinalBSGrupo;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// BO: Gestion de Personas/Personal
        /// Autor: Edgar Serruto
        /// Fecha: 13/07/2021
        /// <summary>
        /// Obtiene Personal Subordinado y Jefe Asociado
        /// </summary>
        /// <param name="idJefe">Id de Jefe</param>
        /// <param name="iterador">Cantidad de Iteraciones máximas</param>
        /// <param name="lista">Lista de Asociación de Jefatura y Personal</param>
        /// <returns>List<PersonalJefaturaIteradorDTO></returns>
        public List<PersonalJefaturaIteradorDTO> ObtenerSubordinado(int idJefe, List<PersonalJefaturaDTO> lista, int iterador)
        {
            try
            {
                List<PersonalJefaturaIteradorDTO> listaRetornada = new List<PersonalJefaturaIteradorDTO>();
                if(iterador <= 10)
                {
                    var listaPersonalJefatura = lista;
                    var listaJefatura = listaPersonalJefatura.Where(x => x.IdJefeInmediato == idJefe).ToList();
                    var grupoJefatura = listaJefatura.GroupBy(x => new { x.IdJefeInmediato }).Select(x => new PersonalJefaturaAgrupadoDTO
                    {
                        IdJefeInmediato = x.Key.IdJefeInmediato,
                        PersonalACargo = x.GroupBy(y => new { y.IdPersonal, y.Personal, y.PuestoTrabajo }).Select(y => new PersonalJefaturaAsociadoDTO
                        {
                            IdPersonal = y.Key.IdPersonal,
                            Personal = y.Key.Personal,
                            PuestoTrabajo = y.Key.PuestoTrabajo
                        }).ToList(),
                    }).FirstOrDefault();
                    if (grupoJefatura != null)
                    {
                        foreach (var jefe in grupoJefatura.PersonalACargo)
                        {
                            PersonalJefaturaIteradorDTO agregar = new PersonalJefaturaIteradorDTO()
                            {
                                IdPersonal = jefe.IdPersonal.GetValueOrDefault(),
                                Personal = jefe.Personal,
                                PuestoTrabajo = jefe.PuestoTrabajo,
                                PersonalACargo = this.ObtenerSubordinado(jefe.IdPersonal.GetValueOrDefault(), lista, iterador + 1)
                            };
                            listaRetornada.Add(agregar);
                        }
                        listaRetornada = listaRetornada.OrderBy(x => x.Personal).ToList();
                    }                                       
                }
                return listaRetornada;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// BO: Gestion de Personas/Personal
        /// Autor: Edgar Serruto
        /// Fecha: 13/07/2021
        /// <summary>
        /// Obtiene Personal Subordinado y Jefe Asociado por Filtros
        /// </summary>
        /// <param name="filtro">Filtros de búsqueda</param>
        /// <returns>List<FiltroPersonalJefaturaFiltroDTO></returns>
        public List<FiltroPersonalJefaturaFiltroDTO> ObtenerReporteTodoPersonal(FiltroPersonalJefaturaDTO filtro)
        {
            try
            {
                string condicion = string.Empty;
                var filtros = new
                {
                    ListaPersonal = filtro.ListaPersonal == null ? "" : string.Join(",", filtro.ListaPersonal.Select(x => x).Distinct()),
                    ListaAreaTrabajo = filtro.ListaAreaTrabajo == null ? "" : string.Join(",", filtro.ListaAreaTrabajo),
                    Estado = filtro.Estado == null ? "" : string.Join(",", filtro.Estado),
                };
                if (filtros.ListaPersonal.Length > 0)
                {
                    if (condicion.Length > 0)
                    {
                        condicion = condicion + " AND IdPersonal IN (" + filtros.ListaPersonal + ")";
                    }
                    else
                    {
                        condicion = condicion + " IdPersonal IN (" + filtros.ListaPersonal + ")";
                    }
                }
                if (filtros.ListaAreaTrabajo.Length > 0)
                {
                    if (condicion.Length > 0)
                    {
                        condicion = condicion + " AND IdPersonalAreaTrabajo IN (" + filtros.ListaAreaTrabajo + ")";
                    }
                    else
                    {
                        condicion = condicion + " IdPersonalAreaTrabajo IN (" + filtros.ListaAreaTrabajo + ")";
                    }
                }
                if (filtros.Estado.Length > 0)
                {                    
                    if (condicion.Length > 0)
                    {
                        if (filtros.Estado == "1")
                        {
                            condicion = condicion + " AND Estado = 'Activo' ";
                        }
                        else
                        {
                            condicion = condicion + " AND Estado = 'Inactivo' ";
                        }                        
                    }
                    else
                    {
                        if (filtros.Estado == "1")
                        {
                            condicion = condicion + " Estado = 'Activo' ";
                        }
                        else
                        {
                            condicion = condicion + " Estado = 'Inactivo' ";
                        }
                    }
                } 
                return _repPersonal.ObtenerPersonalJefaturaFiltro(condicion);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
