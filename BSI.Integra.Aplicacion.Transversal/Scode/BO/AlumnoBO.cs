using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.Base.Classes;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.Helper;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Aplicacion.Transversal.Tools;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.SCode.Repository;
using Nancy.Json;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Newtonsoft.Json.NetCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    /// BO: Marketing/Alumno
    /// Autor: Fischer Valdez - Wilber Choque - Joao Benavente - Gian Miranda
    /// Fecha: 08/02/2021
    /// <summary>
    /// BO para la logica de Alumno
    /// </summary>
    public class AlumnoBO : BaseBO
    {
        /// Propiedades	                        Significado
        /// -----------	                        ------------
        /// Nombre1                             Primer nombre del alumno
        /// Nombre2                             Segundo nombre del alumno
        /// ApellidoPaterno                     Apellido paterno del alumno
        /// ApellidoMaterno                     Apellido materno del alumno
        /// Dni                                 Documento de identidad del alumno
        /// Direccion                           Direccion del alumno
        /// FechaNacimiento                     Fecha de nacimiento del alumno
        /// Pais                                Pais del alumno
        /// Ciudad                              Ciudad del alumno
        /// Telefono                            Telefono del alumno
        /// Celular                             Celular del alumno
        /// Email1                              Email1 del alumno
        /// Email2                              Email2 del alumno
        /// NivelFormacion                      Nivel de formacion del alumno
        /// Profesion                           Profesion del alumno
        /// Empresa                             Empresa que labora el alumno
        /// EstadoCivil                         Estado civil del alumno
        /// TelefonoFamiliar                    Telefono familiar de la referencia
        /// NombreFamiliar                      Nombre familia de la referencia
        /// Parentesco                          Parentesco de la referencia con el alumno
        /// TelefonoTrabajo                     Telefono de trabajo del alumno
        /// TelefonoTrabajoAnexo                Anexo del telefono del trabajo del alumno
        /// Genero                              Genero del alumno
        /// Skype                               Skype
        /// Fax                                 Fax
        /// IdPais                              Id del alumno (PK de la tabla conf.T_Pais)
        /// UbigeoPais                          Ubigeo del pais
        /// UbigeoDepartamento                  Ubigeo del departamento
        /// UbigeoProvincia                     Ubigeo de la provincia
        /// UbigeoCiudad                        Ubigeo del la ciudad
        /// UbigeoDistrito                      Ubigeo del distrito
        /// DireccionCalle                      Direccion (Calle)
        /// DireccionAv                         Direccion (Avenida)
        /// DireccionZona                       Direccion (Zona)
        /// DireccionComp                       Direccion (Comp)
        /// DireccionTorre                      Direccion (Torre)
        /// DireccionEdificio                   Direccion (Edificio)
        /// DireccionDpto                       Direccion (Departamento)
        /// DireccionUrb                        Direccion (Urbanizacion)
        /// DireccionMz                         Direccion (Manzana)
        /// DireccionLt                         Direccion (Lote)
        /// ReferenciaDetallada                 Referencia detallada del alumno
        /// HoraMaxima                          Hora maxima de disponibilidad
        /// Puesto                              Puesto laboral del alumno
        /// AniversarioBodas                    Aniversario de bodas del alumno
        /// NroHijo                             Cantidad de hijos del alumno
        /// ValidacionTelefonica                Flag para validar mediante llamada
        /// FaseContacto                        Fase de contacto
        /// IdCargo                             Id del cargo (PK de la tabla pla.T_Cargo)
        /// Cargo                               Cadena del Cargo
        /// IdAformacion                        Id del area de formacion (PK de la tabla pla.T_AreaFormacion)
        /// Aformacion                          Cadena del area de formacion
        /// IdAtrabajo                          Id del area de trabajo (PK de la tabla pla.T_AreaTrabajo)
        /// Atrabajo                            Cadena del area de trabajo
        /// IdIndustria                         Id de la industria (PK de la tabla pla.T_Industria)
        /// Industria                           Cadena de la industria del alumno
        /// IdReferido                          Id del referido
        /// Referido                            Cadena del referido del alumno
        /// IdCodigoPais                        Id del Codigo de Pais (PK de la tabla conf.T_Pais)
        /// NombrePais                          Cadena con el nombre del pais del alumno
        /// IdCiudad                            Id de la ciudad (PK de la tabla conf.T_Ciudad)
        /// NombreCiudad                        Cadena del nombre de la ciudad del alumno
        /// HoraContacto                        Hora de contacto del alumno
        /// HoraPeru                            Hora con desfase a la hora peruana
        /// IdCodigoRegionCiudad                Id del codigo de region de la ciudad del alumno
        /// Telefono2                           Telefono 02 del alumno
        /// Celular2                            Celular 02 del alumno
        /// IdEmpresa                           Id de la empresa del alumno (PK de la tabla pla.T_Empresa)
        /// IdOportunidadInicial                Id de la oportunidad inicial del alumno (PK de la tabla com.T_Oportunidad)
        /// UsClave                             Clave de usuario
        /// IdTipoDocumento                     Id del tipo de documento del alumno (PK de la pla.T_TipoDocumento)
        /// NroDocumento                        Numero de documento del alumno
        /// DescripcionCargo                    Cadena con la descripcion del cargo del alumno
        /// Asociado                            Asociado del alumno
        /// DeSuscrito                          Flag para validar si el alumno esta desuscrito
        /// IdMigracion                         Id de migracion de V3 (opcional)
        /// NroOportunidades                    Numero de oportunidades del alumno
        /// IdEstadoContactoWhatsApp            Id del estado de contacto de WhatsApp (PK de la tabla mkt.T_EstadoContactoWhatsApp)
        /// IdEstadoContactoMailing             Id del estado de contacto Mailing (PK de la tabla mkt.T_EstadoContactoMailing)
        /// DireccionEnvioCertificado           Cadena con la direccion en donde se enviara el certificado
        /// UsarNuevaDireccionParaEnvio         Cadena con la direccion nueva para el envio
        /// CiudadEnvioCertificado              Ciudad para el envio del certificado
        public string Nombre1 { get; set; }
        public string Nombre2 { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string Dni { get; set; }
        public string Direccion { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public string Pais { get; set; }
        public int? Ciudad { get; set; }
        public string Telefono { get; set; }
        public string Celular { get; set; }
        public string Email1 { get; set; }
        public string Email2 { get; set; }
        public string NivelFormacion { get; set; }
        public string Profesion { get; set; }
        public string Empresa { get; set; }
        public string EstadoCivil { get; set; }
        public string TelefonoFamiliar { get; set; }
        public string NombreFamiliar { get; set; }
        public string Parentesco { get; set; }
        public string TelefonoTrabajo { get; set; }
        public string TelefonoTrabajoAnexo { get; set; }
        public string Genero { get; set; }
        public string Skype { get; set; }
        public string Fax { get; set; }
        public int? IdPais { get; set; }
        public string UbigeoPais { get; set; }
        public string UbigeoDepartamento { get; set; }
        public string UbigeoProvincia { get; set; }
        public string UbigeoCiudad { get; set; }
        public string UbigeoDistrito { get; set; }
        public string DireccionCalle { get; set; }
        public string DireccionAv { get; set; }
        public string DireccionZona { get; set; }
        public string DireccionComp { get; set; }
        public string DireccionTorre { get; set; }
        public string DireccionEdificio { get; set; }
        public string DireccionDpto { get; set; }
        public string DireccionUrb { get; set; }
        public string DireccionMz { get; set; }
        public string DireccionLt { get; set; }
        public string ReferenciaDetallada { get; set; }
        public string HoraMaxima { get; set; }
        public string Puesto { get; set; }
        public string AniversarioBodas { get; set; }
        public string NroHijo { get; set; }
        public bool? ValidacionTelefonica { get; set; }
        public string FaseContacto { get; set; }
        public int? IdCargo { get; set; }
        public string Cargo { get; set; }
        public int? IdAformacion { get; set; }
        public string Aformacion { get; set; }
        public int? IdAtrabajo { get; set; }
        public string Atrabajo { get; set; }
        public int? IdIndustria { get; set; }
        public string Industria { get; set; }
        public int? IdReferido { get; set; }
        public string Referido { get; set; }
        public int? IdCodigoPais { get; set; }
        public string NombrePais { get; set; }
        public int? IdCiudad { get; set; }
        public string NombreCiudad { get; set; }
        public string HoraContacto { get; set; }
        public string HoraPeru { get; set; }
        public int? IdCodigoRegionCiudad { get; set; }
        public string Telefono2 { get; set; }
        public string Celular2 { get; set; }
        public int? IdEmpresa { get; set; }
        public int? IdOportunidadInicial { get; set; }
        public string UsClave { get; set; }
        public Guid? IdTipoDocumento { get; set; }
        public string NroDocumento { get; set; }
        public string DescripcionCargo { get; set; }
        public bool? Asociado { get; set; }
        public bool? DeSuscrito { get; set; }
        public int? IdMigracion { get; set; }
        public int? NroOportunidades { get; set; }
        public bool? EsPersonaValida { get; set; }
        public bool? EsEliminadoPorRegularizacion { get; set; }
        public bool? TieneOportunidad { get; set; }
        public bool? TieneMatricula { get; set; }
        public bool? EsRepetido { get; set; }
        public int? IdEstadoContactoWhatsApp { get; set; }
        public int? IdEstadoContactoMailing { get; set; }
        public string DireccionEnvioCertificado { get; set; }
        public bool? UsarNuevaDireccionParaEnvio { get; set; }
        public string CiudadEnvioCertificado { get; set; }
        public int? IdEstadoContactoWhatsAppSecundario { get; set; }

        private Parser _parser;

        public string NroWhatsAppCoordinador
        {
            get
            {
                return this.ObtenerNroWhatsAppCoordinador();
            }
        }
        public string NroTelefonoCoordinador
        {
            get
            {
                return this.ObtenerNroTelefonoCoordinador();
            }
        }
        public string FormaPago
        {
            get
            {
                return this.ObtenerFormaPago();
            }
        }

        /// <summary>
        /// Pais origen del alumno
        /// </summary>
        public string NombrePaisOrigen
        {
            get
            {
                return this.ObtenerNombrePaisOrigen();
            }
        }

        public string NombreCiudadOrigen
        {
            get
            {
                return this.ObtenerNombreCiudadOrigen();
            }
        }

        public string NroCelularCompleto { get { return this.ObtenerNroCelularCompleto(); } }
        public string NroCelularSecundarioCompleto { get { return this.ObtenerNroCelularCompletoSecundario(); } }
        public string NombreCompleto { get { return this.ObtenerNombreCompleto(); } }

        private AlumnoRepositorio _repAlumno;
        private AreaFormacionRepositorio _repAreaFormacion;
        private CargoRepositorio _repCargo;
        private AreaTrabajoRepositorio _repAreaTrabajo;
        private IndustriaRepositorio _repIndustria;

        private DapperRepository _dapperRepository;
        public AlumnoBO()
        {
            _dapperRepository = new DapperRepository();
            _repAlumno = new AlumnoRepositorio();
            _repAreaFormacion = new AreaFormacionRepositorio();
            _repCargo = new CargoRepositorio();
            _repAreaTrabajo = new AreaTrabajoRepositorio();
            _repIndustria = new IndustriaRepositorio();

            _parser = new Parser();
        }
        public AlumnoBO(integraDBContext integraDBContext)
        {
            _dapperRepository = new DapperRepository(integraDBContext);
            _repAlumno = new AlumnoRepositorio(integraDBContext);
            _repAreaFormacion = new AreaFormacionRepositorio(integraDBContext);
            _repCargo = new CargoRepositorio(integraDBContext);
            _repAreaTrabajo = new AreaTrabajoRepositorio(integraDBContext);
            _repIndustria = new IndustriaRepositorio(integraDBContext);

            _parser = new Parser();
        }
        public AlumnoBO(int id)
        {
            ActualesErrores = new Dictionary<string, List<ErrorInfo>>();
            _repAlumno = new AlumnoRepositorio();

            var alumno = _repAlumno.FirstById(id);
            this.Id = alumno.Id;
            this.Nombre1 = alumno.Nombre1;
            this.Nombre2 = alumno.Nombre2;
            this.ApellidoPaterno = alumno.ApellidoPaterno;
            this.ApellidoMaterno = alumno.ApellidoMaterno;
            this.Dni = alumno.Dni;
            this.Direccion = alumno.Direccion;
            this.FechaNacimiento = alumno.FechaNacimiento;
            this.Pais = alumno.Pais;
            this.Ciudad = alumno.Ciudad;
            this.Telefono = alumno.Telefono;
            this.Celular = alumno.Celular;
            this.Email1 = alumno.Email1;
            this.Email2 = alumno.Email2;
            this.NivelFormacion = alumno.NivelFormacion;
            this.Profesion = alumno.Profesion;
            this.Empresa = alumno.Empresa;
            this.EstadoCivil = alumno.EstadoCivil;
            this.TelefonoFamiliar = alumno.TelefonoFamiliar;
            this.NombreFamiliar = alumno.NombreFamiliar;
            this.Parentesco = alumno.Parentesco;
            this.TelefonoTrabajo = alumno.TelefonoTrabajo;
            this.TelefonoTrabajoAnexo = alumno.TelefonoTrabajoAnexo;
            this.Genero = alumno.Genero;
            this.Skype = alumno.Skype;
            this.Fax = alumno.Fax;
            this.IdPais = alumno.IdPais;
            this.UbigeoPais = alumno.UbigeoPais;
            this.UbigeoDepartamento = alumno.UbigeoDepartamento;
            this.UbigeoProvincia = alumno.UbigeoProvincia;
            this.UbigeoCiudad = alumno.UbigeoCiudad;
            this.UbigeoDistrito = alumno.UbigeoDistrito;
            this.DireccionCalle = alumno.DireccionCalle;
            this.DireccionAv = alumno.DireccionAv;
            this.DireccionZona = alumno.DireccionZona;
            this.DireccionComp = alumno.DireccionComp;
            this.DireccionTorre = alumno.DireccionTorre;
            this.DireccionEdificio = alumno.DireccionEdificio;
            this.DireccionDpto = alumno.DireccionDpto;
            this.DireccionUrb = alumno.DireccionUrb;
            this.DireccionMz = alumno.DireccionMz;
            this.DireccionLt = alumno.DireccionLt;
            this.ReferenciaDetallada = alumno.ReferenciaDetallada;
            this.HoraMaxima = alumno.HoraMaxima;
            this.Puesto = alumno.Puesto;
            this.AniversarioBodas = alumno.AniversarioBodas;
            this.NroHijo = alumno.NroHijo;
            this.ValidacionTelefonica = alumno.ValidacionTelefonica;
            this.FaseContacto = alumno.FaseContacto;
            this.IdCargo = alumno.IdCargo;
            this.Cargo = alumno.Cargo;
            this.IdAformacion = alumno.IdAformacion;
            this.Aformacion = alumno.Aformacion;
            this.IdAtrabajo = alumno.IdAtrabajo;
            this.Atrabajo = alumno.Atrabajo;
            this.IdIndustria = alumno.IdIndustria;
            this.Industria = alumno.Industria;
            this.IdReferido = alumno.IdReferido;
            this.Referido = alumno.Referido;
            this.IdCodigoPais = alumno.IdCodigoPais;
            this.NombrePais = alumno.NombrePais;
            this.IdCiudad = alumno.IdCiudad;
            this.NombreCiudad = alumno.NombreCiudad;
            this.HoraContacto = alumno.HoraContacto;
            this.HoraPeru = alumno.HoraPeru;
            this.IdCodigoRegionCiudad = alumno.IdCodigoRegionCiudad;
            this.Telefono2 = alumno.Telefono2;
            this.Celular2 = alumno.Celular2;
            this.IdEmpresa = alumno.IdEmpresa;
            this.IdOportunidadInicial = alumno.IdOportunidadInicial;
            this.UsClave = alumno.UsClave;
            this.IdTipoDocumento = alumno.IdTipoDocumento;
            this.NroDocumento = alumno.NroDocumento;
            this.DescripcionCargo = alumno.DescripcionCargo;
            this.Asociado = alumno.Asociado;
            this.DeSuscrito = alumno.DeSuscrito;
            this.Estado = alumno.Estado;
            this.UsuarioCreacion = alumno.UsuarioCreacion;
            this.UsuarioModificacion = alumno.UsuarioModificacion;
            this.FechaCreacion = alumno.FechaCreacion;
            this.FechaModificacion = alumno.FechaModificacion;
            this.RowVersion = alumno.RowVersion;
            this.IdMigracion = alumno.IdMigracion;
            this.NroOportunidades = alumno.NroOportunidades;

        }

        /// <summary>
        /// Verifica si existen alumnos con un mismo correo
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool existeContacto(int id = 0)
        {
            bool result = true;
            List<AlumnoEmailDTO> listaAlumno = _repAlumno.ObtenerAlumnoPorEmail(this.Email1, this.Email2);
            if (listaAlumno.Count() == 0)
            {
                result = false;
            }
            else if (listaAlumno.Count() == 1)
            {
                //Si es el registro que se esta editando, retorna false por que no existe duplicados, si podria admitirlo en cualquiera: email 1 o email2
                if (listaAlumno.FirstOrDefault().Id == Id)
                {
                    result = false;
                }
                else
                {
                    result = true;
                }
            }
            else
            {
                //Verificar el caso, que hay varias filas, pero en el row que se esta editando se quiere pasar el email2 y duplicarlo en email1
                bool CumpleCondiciones = false;
                AlumnoEmailDTO alumnoBD = _repAlumno.ObtenerEmailAlumno(Id);

                foreach (var iter in listaAlumno)
                {
                    if (id == iter.Id && alumnoBD != null && alumnoBD.Email1 != null && iter.Email2.Equals(Email1))
                    {
                        CumpleCondiciones = true;
                    }
                }
                if (CumpleCondiciones)
                {
                    result = false;
                }
            }
            return result;
        }

        /// <summary>
        /// Obtiene un alumno para poder actualizar el nombre del visitante en el Chat del portal Web
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public AlumnoPortalWebDTO ObtenerAlumnoChatPortalWebPorId(int id)
        {
            AlumnoPortalWebDTO alumno = new AlumnoPortalWebDTO();
            var _query = string.Empty;
            _query = "SELECT Id AS Id, Nombre1 AS Nombre1, ApellidoPaterno AS ApellidoPaterno, ApellidoMaterno AS ApellidoMaterno, Estado AS Estado FROM com.V_TAlumno_ObtenerParaChaPWSignalR WHERE Estado = 1 AND Id = @id";
            var alumnoDB = _dapperRepository.FirstOrDefault(_query, new { id });
            alumno = JsonConvert.DeserializeObject<AlumnoPortalWebDTO>(alumnoDB);
            return alumno;
        }

        /// <summary>
        /// Obtiene el Id,NombreCompleto de los alumnos por  un parametro
        /// </summary>
        /// <param name="valor"></param>
        /// <returns></returns>
        public List<AlumnoFiltroAutocompleteDTO> ObtenerTodoFiltroAutocomplete(string valor)
        {
            return _repAlumno.ObtenerTodoFiltroAutoComplete(valor);
        }

        /// <summary>
        /// Obtiene el nro de WhatsApp de la coordinadora asignada
        /// </summary>
        /// <returns></returns>
        private string ObtenerNroWhatsAppCoordinador()
        {
            try
            {
                var numeroCelular = "";
                switch (this.IdCodigoPais)
                {
                    case 51://Peru
                        numeroCelular = "+51 992 651 774";
                        break;
                    case 57://Colombia
                        numeroCelular = "+57 350 3189803";
                        break;
                    case 591://Bolivia
                        numeroCelular = "+591 76398490";
                        break;
                    default://Internacional
                        numeroCelular = "+51 932 104 477";
                        break;
                }
                return numeroCelular;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Obtiene la forma de pago en base al alumno
        /// </summary>
        /// <returns></returns>
        private string ObtenerFormaPago()
        {
            try
            {
                var formaPago = "";
                switch (this.IdCodigoPais)
                {
                    case 51://Peru
                        formaPago = $@"<p><span style='color:#ff0000;'><strong>PERU</span></strong></p><p><strong>PLATAFORMA </strong></p><ul><li>Ingrese al sitio Web de BSG Institute con su usuario y clave.</li><li>Ingrese a &ldquo;Mis Cursos&rdquo; y posteriormente a &ldquo;Mis Pagos&rdquo;.</li><li>Se mostrar&aacute; su cronograma de pagos, seleccione la cuota a pagar.</li><li>Luego seleccione el m&eacute;todo de pago: VISA / MASTERCARD- AMEX &ndash; DINERS</li><li>Se habilitar&aacute; un recuadro para colocar su DNI y para seleccionar si desea se le emita una Boleta o Factura por su pago.</li><li>De clic en &ldquo;Completar Matr&iacute;cula&rdquo; para continuar con el proceso.</li><li>Ingrese sus datos de tarjeta y confirme el pago.</li></ul><p><strong>VENTANILLA</strong><br />Los Alumnos pueden pagar acerc&aacute;ndose a cualquier agencia del Banco de Cr&eacute;dito</p><ul><li>Decir que va a realizar un&nbsp;<strong>Pago de Servicios</strong>a trav&eacute;s del Sistema&nbsp;<strong>Credipago</strong>.</li><li>Indicar que el tipo de empresa es&nbsp;<strong>EMPRESAS DIVERSAS</strong>.</li><li>La empresa es&nbsp;<strong>BSG Institute</strong>.</li><li>Cuenta a Abonar:</li></ul><ul><ul><li><strong>LIMA &ndash; SOLES</strong>: Programas Presencial Lima, Online y Aonline, con cronogramas en soles.</li><li><strong>LIMA &ndash; DOLARES</strong>: Programas Presencial Lima, Online y Aonline, con cronogramas en d&oacute;lares.</li><li><strong>AREQUIPA &ndash; SOLES: </strong>Programas Presenciales Arequipa con cronogramas en soles.</li><li><strong>AREQUIPA &ndash; DOLARES: </strong>Programas Presenciales Arequipa con cronogramas en d&oacute;lares.</li></ul></ul><ul><li>Elija seg&uacute;n su programa y la moneda de su cronograma de pagos.</li><li>Dar su&nbsp;<strong>c&oacute;digo de alumno </strong>y listo.</li></ul><p><strong>ALUMNOS CON CUENTA EN EL BANCO DE CREDITO</strong></p><ul><li>Ingrese a&nbsp;<a href='http://www.viabcp.com/'>viabcp.com</a>.</li><li>Seleccione la opci&oacute;n:&nbsp;<strong>PAGOS</strong>.</li><li>Seleccione:&nbsp;P<strong>agar un Servicio</strong>.</li><li>Digite&nbsp;<strong>BSG Institute</strong>.</li><li>Ubicada la empresa se mostrar&aacute; un nuevo cuadro donde se muestra:</li></ul><ul><ul><li><strong>LIMA &ndash; SOLES</strong>: Programas Presencial Lima, Online y Aonline, con cronogramas en soles.</li><li><strong>LIMA &ndash; DOLARES</strong>: Programas Presencial Lima, Online y Aonline, con cronogramas en d&oacute;lares.</li><li><strong>AREQUIPA &ndash; SOLES: </strong>Programas Presenciales Arequipa con cronogramas en soles.</li><li><strong>AREQUIPA &ndash; DOLARES: </strong>Programas Presenciales Arequipa con cronogramas en d&oacute;lares.</li></ul></ul><ul><li>Elija seg&uacute;n su programa y la moneda de su cronograma de pagos.</li><li>Digite su c&oacute;digo de alumno y haga click en continuar</li><li>Seleccione la cuenta o tarjeta con que pagar&aacute; y la cuota a pagar, de un clic en continuar y confirme la operaci&oacute;n con su clave token.</li></ul><p>Para pagar sus cuotas a trav&eacute;s de Internet requiere tener su clave de internet de 6 d&iacute;gitos, y llave token, para solicitarlas ac&eacute;rquese a cualquier agencia del Banco Cr&eacute;dito.</p><p><br /></p><p><strong>AGENTE BCP</strong></p><ul><li>Decir que va a realizar un&nbsp;<strong>Pago de Servicios</strong>.</li><li>Indicar que el pago es a la empresa de c&oacute;digo&nbsp;<strong>18185</strong>.</li><li>Cuenta a :</li></ul><ul><ul><li><strong>LIMA &ndash; SOLES</strong>: Programas Presencial Lima, Online y Aonline, con cronogramas en soles.</li><li><strong>LIMA &ndash; DOLARES</strong>: Programas Presencial Lima, Online y Aonline, con cronogramas en d&oacute;lares.</li><li><strong>AREQUIPA &ndash; SOLES: </strong>Programas Presenciales Arequipa con cronogramas en soles.</li><li><strong>AREQUIPA &ndash; DOLARES: </strong>Programas Presenciales Arequipa con cronogramas en d&oacute;lares.</li></ul></ul><ul><li>Elija seg&uacute;n su programa y la moneda de su cronograma de pagos.</li><li>Dar su&nbsp;<strong>c&oacute;digo de alumno</strong>y listo.</li></ul><p>&nbsp;</p><p>NOTA:</p><p>En caso de que la cuota sea en d&oacute;lares y se quiere realizar el pago en soles, se aplicar&aacute; el tipo de cambio bancario.</p><p>Para pagos en agentes BCP solo reciben soles por lo que el monto de la cuota ser&aacute; cambiada autom&aacute;ticamente a soles de acuerdo al tipo de cambio bancario.</p><p>El c&oacute;digo solo es v&aacute;lido en la cuenta detallada seg&uacute;n el programa al que esta Ud. matriculado &nbsp;y seg&uacute;n su cronograma de pagos, por ejemplo si su programa es en Modalidad Online la cuenta ser&iacute;a: Lima-Soles (si su cronograma es en soles) y Lima-D&oacute;lares (si su cronograma es en d&oacute;lares), no importando la moneda con la que realizar&aacute; el pago.</p><p>Si tiene alg&uacute;n problema para realizar su pago de las formas detalladas puede comunicarse con su coordinadora acad&eacute;mica quien lo ayudar&aacute; con formas de pago alternativas.</p>";
                        break;
                    case 57://Colombia
                        formaPago = $@"<p><span style='color: #ff0000;'><strong>COLOMBIA</strong></span></p><p><strong>PLATAFORMA </strong></p><ul><li>Ingrese al sitio Web de BSG Institute con su usuario y clave.</li><li>Ingrese a &ldquo;Mis Cursos&rdquo; y posteriormente a &ldquo;Mis Pagos&rdquo;.</li><li>Se mostrar&aacute; su cronograma de pagos, seleccione la cuota a pagar.</li><li>Luego seleccione el m&eacute;todo de pago: VISA / MASTERCARD</li><li>Se habilitar&aacute; un recuadro para colocar su C&eacute;dula.</li><li>De clic en &ldquo;Completar Matr&iacute;cula&rdquo; para continuar con el proceso.</li><li>Ingrese sus datos de tarjeta Y tarjetahabiente y confirme el pago.</li></ul><p><strong>VENTANILLA BANCOLOMBIA</strong><br /> Los&nbsp;Alumnos pueden pagar acerc&aacute;ndose a cualquier agencia del Banco Bancolombia</p><ul><li>Debe indicar&nbsp;queva a realizar un dep&oacute;sito a la empresa Bs Grupo Colombia con Nro de Convenio 56470.</li><li>Debe indicar su nro de referencia:&nbsp;<span style='color: #ff0000;'><strong>XXXXXXXX (de acuerdo al alumno)</strong>.</span></li><li>Debe indicar el monto a pagar.</li></ul><p><strong>TRANSFERENCIA</strong></p><p>Los alumnos pueden registrarnos como proveedores con los datos indicados y al cabo de dos horas pueden transferirnos el monto de su cuota.<br /> Empresa: BS GRUPO COLOMBIA SAS</p><p>NIT: 900776296</p><p>N&uacute;mero de Cuenta de Ahorros: 65231918412</p><p>&nbsp;</p><p>NOTA:</p><p>Si tiene alg&uacute;n problema para realizar su pago de las formas detalladas puede comunicarse con su coordinadora acad&eacute;mica quien lo ayudar&aacute; con formas de pago alternativas.</p>";
                        break;
                    case 591://Bolivia
                        formaPago = $@"<p><span style='color: #ff0000;'><strong>BOLIVIA</strong></span></p><p><strong>PLATAFORMA </strong></p><ul><li>Ingrese al sitio Web de BSG Institute con su usuario y clave.</li><li>Ingrese a &ldquo;Mis Cursos&rdquo; y posteriormente a &ldquo;Mis Pagos&rdquo;.</li><li>Se mostrar&aacute; su cronograma de pagos, seleccione la cuota a pagar.</li><li>Luego seleccione el m&eacute;todo de pago: VISA / MASTERCARD</li><li>Se habilitar&aacute; un recuadro para colocar su C&eacute;dula de Identidad.</li><li>De clic en &ldquo;Completar Matr&iacute;cula&rdquo; para continuar con el proceso.</li><li>Ingrese sus datos de tarjeta y confirme el pago.</li></ul><p><strong>&nbsp;</strong></p><p><strong>AGENCIA O AGENTE BANCO DE CREDITO</strong><br /> Los&nbsp;Alumnos pueden pagar acerc&aacute;ndose a cualquier agencia o agente del Banco de Cr&eacute;dito con los siguientes datos:</p><p>BSG Institute Bolivia SRL</p><p>NIT 376053024</p><p>Cuenta Corriente Banco de Cr&eacute;dito de Bolivia - Sucursal Santa Cruz</p><p>Cta en Bolivianos: 701-5051921-3-41</p><p>Cta en D&oacute;lares: 701-5041553-2-04</p><p>Indicar el monto a pagar</p><p>&nbsp;</p><p>NOTA:</p><p>Si tiene alg&uacute;n problema para realizar su pago de las formas detalladas puede comunicarse con su coordinadora acad&eacute;mica quien lo ayudar&aacute; con formas de pago alternativas.</p>";
                        break;
                    default://Internacional
                        formaPago = $@"<p><span style='color: #ff0000;'><strong>EXTRANJEROS</strong></span></p><p><strong>PLATAFORMA </strong></p><ul><li>Ingrese al sitio Web de BSG Institute con su usuario y clave.</li><li>Ingrese a &ldquo;Mis Cursos&rdquo; y posteriormente a &ldquo;Mis Pagos&rdquo;.</li><li>Se mostrar&aacute; su cronograma de pagos, seleccione la cuota a pagar.</li><li>Luego seleccione el m&eacute;todo de pago: VISA / MASTERCARD- AMEX &ndash; DINERS</li><li>Se habilitar&aacute; un recuadro para colocar su Documento de Identidad.</li><li>De clic en &ldquo;Completar Matr&iacute;cula&rdquo; para continuar con el proceso.</li><li>Ingrese sus datos de tarjeta y confirme el pago.</li></ul><p>&nbsp;</p><p>NOTA:</p><p>Si tiene alg&uacute;n problema para realizar su pago de la forma detallada puede comunicarse con su coordinadora acad&eacute;mica quien lo ayudar&aacute; con formas de pago alternativas.</p>";
                        break;
                }
                return formaPago;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Obtiene el nombre del pais de origen del alumno
        /// </summary>
        /// <returns></returns>
        private string ObtenerNombrePaisOrigen()
        {
            try
            {

                return _repAlumno.ObtenerPaisOrigen(this.Id);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private string ObtenerNombreCiudadOrigen()
        {
            try
            {

                return _repAlumno.ObtenerCiudadOrigen(this.Id);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Calcula la forma de pago en base a la matricula
        /// </summary>
        /// <param name="idModalidadCurso">Id de la modalidad del curso (PK de la tabla pla.T_ModalidadCurso)</param>
        /// <param name="idCiudad">Id de la ciudad (PK de la tabla conf.T_Ciudad)</param>
        /// <param name="codigoMatricula">Codigo de matricula del alumno</param>
        /// <param name="monedaCronograma">Moneda registrada en el cronograma</param>
        /// <returns>Cadena formateada con la forma de pago</returns>
        public string ObtenerFormaPago(int idModalidadCurso, int idCiudad, string codigoMatricula, string monedaCronograma)
        {
            try
            {
                var formaPago = "";
                switch (this.IdCodigoPais)
                {
                    case 51://Peru
                        formaPago = @"<p> <span lang='EN-US' style='font-size:11pt;font-family:Calibri, sans-serif;'> <strong>Plataforma</strong> </span></p><p> <span lang='EN-US' style='font-size:11pt;font-family:Calibri, sans-serif;'>Pasos a seguir:</span> <br /> <br /> <span lang='EN-US' style='font-size:11pt;font-family:Calibri, sans-serif;'>Ingrese al sitio Web de BSG Institute con su usuario y clave.</span> <br /> <span lang='EN-US' style='font-size:11pt;font-family:Calibri, sans-serif;'>Ingrese a &ldquo;Mis Cursos&rdquo; y posteriormente a &ldquo;Mis Pagos&rdquo;.</span> <br /> <span lang='EN-US' style='font-size:11pt;font-family:Calibri, sans-serif;'>Se mostrar&aacute; su cronograma de pagos, seleccione la cuota a pagar.</span> <br /> <span lang='EN-US' style='font-size:11pt;font-family:Calibri, sans-serif;'>Luego seleccione el m&eacute;todo de pago: VISA / MASTERCARD- AMEX &ndash; DINERS</span> <br /> <span lang='EN-US' style='font-size:11pt;font-family:Calibri, sans-serif;'>Ingrese su DNI y seleccione si desea boleta o factura, en el caso de ser factura indique la Raz&oacute;n social y el RUC.</span> <br /> <span lang='EN-US' style='font-size:11pt;font-family:Calibri, sans-serif;'>Dar click en &ldquo;Completar Matr&iacute;cula&rdquo; para continuar con el proceso.</span> <br /> <span lang='EN-US' style='font-size:11pt;font-family:Calibri, sans-serif;'>Ingrese sus datos de tarjeta y confirme el pago.</span></p><p> <span lang='EN-US' style='font-size:11pt;font-family:Calibri, sans-serif;'> <strong></strong> </span> <strong style='font-family:Calibri, sans-serif;font-size:11pt;'>Ventanilla</strong> <br /></p><p> <span lang='EN-US' style='font-size:11pt;font-family:Calibri, sans-serif;'>Los Alumnos pueden pagar acerc&aacute;ndose a cualquier agencia del Banco de Cr&eacute;dito.</span> <br /> <br /> <span lang='EN-US' style='font-size:11pt;font-family:Calibri, sans-serif;'>Pasos a seguir:</span> <br /> <br /> <span lang='EN-US' style='font-size:11pt;font-family:Calibri, sans-serif;'>Mencione que realizar&aacute; un Pago de Servicios a trav&eacute;s del Sistema Credipago a la empresa BSG Institute</span> <br /> <span lang='EN-US' style='font-size:11pt;font-family:Calibri, sans-serif;'>Indique la cuenta a Abonar: {T_MatriculaCabecera.CuentaAbonar}</span> <br /> <span lang='EN-US' style='font-size:11pt;font-family:Calibri, sans-serif;'>Brindar su c&oacute;digo de alumno.</span></p><p style='text-align:justify;'> <span lang='EN-US' style='font-size:11pt;font-family:Calibri, sans-serif;'> <strong>Alumnos con cuenta en el Banco de Cr&eacute;dito</strong> </span> <br /> <br /> <span lang='EN-US' style='font-size:11pt;font-family:Calibri, sans-serif;'>Ingrese a www.viabcp.com.</span> <br /> <span lang='EN-US' style='font-size:11pt;font-family:Calibri, sans-serif;'>Seleccione la opci&oacute;n: <strong>PAGOS.</strong> </span> <br /> <span lang='EN-US' style='font-size:11pt;font-family:Calibri, sans-serif;'>Seleccione: <strong>Pagar un Servicio.</strong> </span> <br /> <span lang='EN-US' style='font-size:11pt;font-family:Calibri, sans-serif;'>Digite y seleccione <strong>BSG Institute.</strong> </span> <br /> <span lang='EN-US' style='font-size:11pt;font-family:Calibri, sans-serif;'>Se mostrar&aacute; un nuevo cuadro con nuestras cuentas y usted deber&aacute; elegir:&nbsp;{T_MatriculaCabecera.CuentaAbonar} </span><br /> <span lang='EN-US' style='font-size:11pt;font-family:Calibri, sans-serif;'>Digite su c&oacute;digo de alumno y haga click en continuar</span> <br /> <span lang='EN-US' style='font-size:11pt;font-family:Calibri, sans-serif;'>Seleccione la cuenta o tarjeta con que pagar&aacute; y la cuota a pagar, dar click en continuar y confirme la operaci&oacute;n con su clave token.Para pagar sus cuotas a trav&eacute;s de Internet requiere tener su clave de internet de 6 d&iacute;gitos y llave token, para solicitarlas ac&eacute;rquese a cualquier agencia del Banco Cr&eacute;dito.</span></p><p style='text-align:justify;'> <span lang='EN-US' style='font-size:11pt;font-family:Calibri, sans-serif;'> <strong>Agente BCP</strong> </span> <br /> <br /> <span lang='EN-US' style='font-size:11pt;font-family:Calibri, sans-serif;'>Decir que va a realizar un <strong>Pago de Servicios.</strong> </span> <br /> <span lang='EN-US' style='font-size:11pt;font-family:Calibri, sans-serif;'>Brindar el c&oacute;digo de la empresa: <strong>18185</strong> </span> <br /> <span lang='EN-US' style='font-size:11pt;font-family:Calibri, sans-serif;'>Indicar la cuenta: {T_MatriculaCabecera.CuentaAbonar} </span> <br /> <span lang='EN-US' style='font-size:11pt;font-family:Calibri, sans-serif;'>Brindar su c&oacute;digo de alumno.</span></p><p style='text-align:justify;'> <span lang='EN-US' style='font-size:11pt;font-family:Calibri, sans-serif;'> <strong>Nota:</strong> </span> <span lang='EN-US' style='font-size:11pt;font-family:Calibri, sans-serif;'>Si su cuota es en d&oacute;lares y quiere realizar el pago en soles, se aplicar&aacute; el tipo de cambio bancario.</span> <br /> <span lang='EN-US' style='font-size:11pt;font-family:Calibri, sans-serif;'>Tener en cuenta que en los agentes BCP, s&oacute;lo reciben moneda en soles y si desea pagar una cuota en d&oacute;lares el monto ser&aacute; cambiado autom&aacute;ticamente a soles de acuerdo al tipo de cambio bancario.</span> <br /> <span lang='EN-US' style='font-size:11pt;font-family:Calibri, sans-serif;'>Si tiene alg&uacute;n problema para realizar su pago de las formas mencionadas anteriormente, puede comunicarse con su Asesor(a) de Capacitaci&oacute;n, quien lo ayudar&aacute; brind&aacute;ndole otras alternativas de pago.</span></p>";
                        break;
                    case 57://Colombia
                        formaPago = @"<p style='text-align:justify;'><span lang='EN - US' style='font-size:11pt;font-family:Calibri, sans-serif;'><strong>Plataforma</strong></span><br /> <span style='font-family:Calibri, sans-serif;font-size:11pt;'></span></p> <span lang='EN-US' style='font-size:11pt;font-family:Calibri, sans-serif;'>Ingrese al sitio Web de BSG Institute con su usuario y clave.</span><br /> <span lang='EN-US' style='font-size:11pt;font-family:Calibri, sans-serif;'>Ingrese a &ldquo;Mis Cursos&rdquo; y posteriormente a &ldquo;Mis Pagos&rdquo;.</span><br /> <span lang='EN-US' style='font-size:11pt;font-family:Calibri, sans-serif;'>Se mostrar&aacute; su cronograma de pagos, seleccione la cuota a pagar.</span><br /> <span lang='EN-US' style='font-size:11pt;font-family:Calibri, sans-serif;'>Luego seleccione el m&eacute;todo de pago: VISA / MASTERCARD</span><br /> <span lang='EN-US' style='font-size:11pt;font-family:Calibri, sans-serif;'>Se habilitar&aacute; un recuadro para colocar su C&eacute;dula.</span><br /> <span lang='EN-US' style='font-size:11pt;font-family:Calibri, sans-serif;'>Dar click en &ldquo;Completar Matr&iacute;cula&rdquo; para continuar con el proceso.</span><br /> <span lang='EN-US' style='font-size:11pt;font-family:Calibri, sans-serif;'>Ingrese sus datos de tarjeta y tarjeta habiente y confirme el pago.</span><p style='text-align:justify;'><span lang='EN-US' style='font-size:11pt;font-family:Calibri, sans-serif;'><strong>Ventanilla Bancolombia</strong></span><br /> <br /> <span lang='EN-US' style='font-size:11pt;font-family:Calibri, sans-serif;'>Los Alumnos pueden pagar acerc&aacute;ndose a cualquier agencia del Banco Bancolombia</span><br /> <br /> <span lang='EN-US' style='font-size:11pt;font-family:Calibri, sans-serif;'>Debe indicar que va a realizar un dep&oacute;sito a la empresa Bs Grupo Colombia con n&uacute;mero de Convenio 56470.</span><br /> <span lang='EN-US' style='font-size:11pt;font-family:Calibri, sans-serif;'>Debe indicar su N&uacute;mero de referencia: {T_MatriculaCabecera.NroReferenciaAlumno}</span><br /> <span lang='EN-US' style='font-size:11pt;font-family:Calibri, sans-serif;'>Debe indicar el monto a pagar.</span><br /> <br /> <span lang='EN-US' style='font-size:11pt;font-family:Calibri, sans-serif;'><strong>Transferencia</strong></span><br /> <br /> <span lang='EN-US' style='font-size:11pt;font-family:Calibri, sans-serif;'>Los alumnos pueden registrarnos como proveedores con los datos indicados y al cabo de dos horas pueden transferirnos el monto de su cuota.</span><br /> <br /> <span lang='EN-US' style='font-size:11pt;font-family:Calibri, sans-serif;'>Empresa: BS GRUPO COLOMBIA SAS</span><br /> <span lang='EN-US' style='font-size:11pt;font-family:Calibri, sans-serif;'>NIT: 900776296</span><br /> <span lang='EN-US' style='font-size:11pt;font-family:Calibri, sans-serif;'>N&uacute;mero de Cuenta de Ahorros: 65231918412</span><br /> <br /> <span lang='EN-US' style='font-size:11pt;font-family:Calibri, sans-serif;'><strong>Nota:</strong></span><br /> <br /> <span lang='EN-US' style='font-size:11pt;font-family:Calibri, sans-serif;'>Si tiene alg&uacute;n problema para realizar su pago de las formas detalladas puede comunicarse con su Asesor (a) de Capacitaci&oacute;n, quien lo ayudar&aacute; brind&aacute;ndole otras formas de pago alternativas.</span></p>";
                        break;
                    case 591://Bolivia
                        formaPago = @"<p><span lang='EN - US' style='font-size:11pt;font-family:Calibri, sans-serif;'><strong>Plataforma</strong></span><br /> <br /> <span lang='EN-US' style='font-size:11pt;font-family:Calibri, sans-serif;'>Ingrese al sitio Web de BSG Institute con su usuario y clave.</span><br /> <span lang='EN-US' style='font-size:11pt;font-family:Calibri, sans-serif;'>Ingrese a &ldquo;Mis Cursos&rdquo; y posteriormente a &ldquo;Mis Pagos&rdquo;.</span><br /> <span lang='EN-US' style='font-size:11pt;font-family:Calibri, sans-serif;'>Se mostrar&aacute; su cronograma de pagos y seleccione la cuota a pagar.</span><br /> <span lang='EN-US' style='font-size:11pt;font-family:Calibri, sans-serif;'>Luego elija el m&eacute;todo de pago: <strong>VISA / MASTERCARD</strong></span><br /> <span lang='EN-US' style='font-size:11pt;font-family:Calibri, sans-serif;'>Se habilitar&aacute; un recuadro para colocar su C&eacute;dula de Identidad.</span><br /> <span lang='EN-US' style='font-size:11pt;font-family:Calibri, sans-serif;'>Dar click en &ldquo;Completar Matr&iacute;cula&rdquo; para continuar con el proceso.</span><br /> <span lang='EN-US' style='font-size:11pt;font-family:Calibri, sans-serif;'>Ingresar sus datos de tarjeta y confirme el pago.</span><br /> <br /> <span lang='EN-US' style='font-size:11pt;font-family:Calibri, sans-serif;'><strong>Agencia o Agente Banco de Cr&eacute;dito</strong></span><br /> <br /> <span lang='EN-US' style='font-size:11pt;font-family:Calibri, sans-serif;'>Los Alumnos pueden pagar acerc&aacute;ndose a cualquier agencia o agente del Banco de Cr&eacute;dito con los siguientes datos:</span><br /> <br /> <span lang='EN-US' style='font-size:11pt;font-family:Calibri, sans-serif;'>BSG Institute Bolivia SRL</span><br /> <span lang='EN-US' style='font-size:11pt;font-family:Calibri, sans-serif;'>NIT 376053024</span><br /> <span lang='EN-US' style='font-size:11pt;font-family:Calibri, sans-serif;'>Cuenta Corriente Banco de Cr&eacute;dito de Bolivia - Sucursal Santa Cruz</span><br /> <span lang='EN-US' style='font-size:11pt;font-family:Calibri, sans-serif;'><strong>Cuenta en Bolivianos:</strong> 701-5051921-3-41</span><br /> <span lang='EN-US' style='font-size:11pt;font-family:Calibri, sans-serif;'><strong>Cuenta en D&oacute;lares</strong>: 701-5041553-2-04</span><br /> <span lang='EN-US' style='font-size:11pt;font-family:Calibri, sans-serif;'>Indicar el monto a pagar.</span><br /> <br /> <span lang='EN-US' style='font-size:11pt;font-family:Calibri, sans-serif;'><strong>Nota:</strong></span><br /> <br /> <span lang='EN-US' style='font-size:11pt;font-family:Calibri, sans-serif;'>Si tiene alg&uacute;n problema para realizar su pago de las formas mencionadas anteriormente, puede comunicarse con su Asesor (a) de Capacitaci&oacute;n, quien lo ayudar&aacute; brind&aacute;ndole otras alternativas de pago.</span><br /> <span style='font-family:Calibri, sans-serif;font-size:14.6667px;text-align:justify;'></span><strong><span style='color:red;'></span></strong><span lang='EN-US' style='font-size:11.0pt;font-family:'Calibri','sans-serif';'></span></p>";
                        break;
                    default://Internacional
                        formaPago = @"<p><span lang='EN - US' style='font-size:11pt;font-family:Calibri, sans-serif;'><strong>Plataforma</strong></span><br /> <br /> <span lang='EN-US' style='font-size:11pt;font-family:Calibri, sans-serif;'>Ingrese al sitio Web de BSG Institute con su usuario y clave.</span><br /> <span lang='EN-US' style='font-size:11pt;font-family:Calibri, sans-serif;'>Ingrese a &ldquo;Mis Cursos&rdquo; y posteriormente a &ldquo;Mis Pagos&rdquo;.</span><br /> <span lang='EN-US' style='font-size:11pt;font-family:Calibri, sans-serif;'>Se mostrar&aacute; su cronograma de pagos, seleccione la cuota a pagar.</span><br /> <span lang='EN-US' style='font-size:11pt;font-family:Calibri, sans-serif;'>Luego seleccione el m&eacute;todo de pago: VISA / MASTERCARD- AMEX &ndash; DINERS</span><br /> <span lang='EN-US' style='font-size:11pt;font-family:Calibri, sans-serif;'>Se habilitar&aacute; un recuadro para colocar su Documento de Identidad.</span><br /> <span lang='EN-US' style='font-size:11pt;font-family:Calibri, sans-serif;'>De click en &ldquo;Completar Matr&iacute;cula&rdquo; para continuar con el proceso.</span><br /> <span lang='EN-US' style='font-size:11pt;font-family:Calibri, sans-serif;'>Ingrese sus datos de tarjeta y confirme el pago.</span></p><p style='text-align:justify;'><span lang='EN-US' style='font-size:11pt;font-family:Calibri, sans-serif;'></span><span lang='EN-US' style='font-size:11pt;font-family:Calibri, sans-serif;'><strong>Nota:</strong></span><br /> <br /> <span lang='EN-US' style='font-size:11pt;font-family:Calibri, sans-serif;'>Si tiene alg&uacute;n problema para realizar su pago de las formas mencionadas anteriormente, puede comunicarse con su Asesor (a) de Capacitaci&oacute;n, quien lo ayudar&aacute; brind&aacute;ndole otras alternativas de pago.</span></p>";
                        break;
                }

                // Nro de referencia
                if (formaPago.Contains("{T_MatriculaCabecera.NroReferenciaAlumno}"))
                {
                    formaPago = formaPago.Replace("{T_MatriculaCabecera.NroReferenciaAlumno}", codigoMatricula.Replace("A", ""));
                }

                // Nro de referencia
                if (formaPago.Contains("{T_MatriculaCabecera.CuentaAbonar}"))
                {
                    formaPago = formaPago.Replace("{T_MatriculaCabecera.CuentaAbonar}", this.CalcularCuentaAbonar(idModalidadCurso, idCiudad, monedaCronograma));
                }

                return formaPago;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        /// <summary>
        /// Implementa la logica para calcular la cuenta a abonar
        /// </summary>
        /// <param name="idModalidadCurso">Id de la modalidad del curso (PK de la tabla pla.T_ModalidadCurso)</param>
        /// <param name="idCiudad">Id de la ciudad (PK de la tabla conf.T_Ciudad)</param>
        /// <param name="monedaCronograma">Moneda segun el cronograma registrado</param>
        /// <returns>Cadena formateada con la cuenta a abonar</returns>
        private string CalcularCuentaAbonar(int idModalidadCurso, int idCiudad, string monedaCronograma)
        {
            try
            {
                var monedaSoles = "soles";
                var monedaDolares = "dolares";

                var modalidadPresencial = 0;
                var modalidadOnline = 1;
                var modalidadAOnline = 2;

                var idCiudadArequipa = ValorEstatico.IdCiudadArequipa;
                var idCiudadLima = ValorEstatico.IdCiudadLima;

                var cuentaAbonar = "";

                if ((idModalidadCurso == modalidadPresencial || idModalidadCurso == modalidadOnline || idModalidadCurso == modalidadAOnline) && monedaCronograma == monedaSoles && idCiudad == idCiudadLima)
                {
                    cuentaAbonar = "Lima - Soles";
                }
                else if ((idModalidadCurso == modalidadPresencial || idModalidadCurso == modalidadOnline || idModalidadCurso == modalidadAOnline) && monedaCronograma == monedaDolares && idCiudad == idCiudadLima)
                {
                    cuentaAbonar = "Lima - Dólares";
                }
                else if (idModalidadCurso == modalidadPresencial && monedaCronograma == monedaSoles && idCiudad == idCiudadArequipa)
                {
                    cuentaAbonar = "Arequipa - Soles";
                }
                else if (idModalidadCurso == modalidadPresencial && monedaCronograma == monedaDolares && idCiudad == idCiudadArequipa)
                {
                    cuentaAbonar = "Arequipa – Dólares";
                }
                else
                {
                    cuentaAbonar = "";
                }
                return cuentaAbonar;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Calcula el estado de whatsapp del contacto
        /// </summary>
        public void ValidarEstadoContactoWhatsApp()
        {
            try
            {
                string urlToPost;
                bool banderaLogin = false;
                string _tokenComunicacion = string.Empty;
                var _idPersonal = 4589;//TODO

                WhatsAppConfiguracionRepositorio _repCredenciales = new WhatsAppConfiguracionRepositorio();
                WhatsAppUsuarioCredencialRepositorio _repTokenUsuario = new WhatsAppUsuarioCredencialRepositorio();
                WhatsAppMensajePublicidadBO whatsAppMensajePublicidad = new WhatsAppMensajePublicidadBO();

                ValidarNumerosWhatsAppDTO DTO = new ValidarNumerosWhatsAppDTO
                {
                    contacts = new List<string>(),
                    blocking = "wait"
                };
                DTO.contacts.Add("+" + this.NroCelularCompleto);
                ServicePointManager.ServerCertificateValidationCallback =
                delegate (object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
                {
                    return true;
                };

                var _credencialesHost = _repCredenciales.ObtenerCredencialHost(this.IdCodigoPais.Value);
                var tokenValida = _repTokenUsuario.ValidarCredencialesUsuario(_idPersonal, this.IdCodigoPais.Value);

                var mensajeJSON = JsonConvert.SerializeObject(DTO);

                string resultado = string.Empty;

                if (tokenValida == null || DateTime.Now >= tokenValida.ExpiresAfter)
                {
                    string urlToPostUsuario = _credencialesHost.UrlWhatsApp + "/v1/users/login";

                    var userLogin = _repTokenUsuario.CredencialUsuarioLogin(_idPersonal);

                    var client = new RestClient(urlToPostUsuario);
                    var request = new RestSharp.RestRequest(Method.POST);
                    request.AddHeader("cache-control", "no-cache");
                    request.AddHeader("Content-Length", "");
                    request.AddHeader("Accept-Encoding", "gzip, deflate");
                    request.AddHeader("Host", _credencialesHost.IpHost);
                    request.AddHeader("Cache-Control", "no-cache");
                    request.AddHeader("Authorization", "Basic " + Convert.ToBase64String(Encoding.ASCII.GetBytes(userLogin.UserUsername + ":" + userLogin.UserPassword)));
                    request.AddHeader("Content-Type", "application/json");
                    IRestResponse response = client.Execute(request);

                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        var datos = JsonConvert.DeserializeObject<userLogeo>(response.Content);

                        foreach (var item in datos.users)
                        {
                            TWhatsAppUsuarioCredencial modelCredencial = new TWhatsAppUsuarioCredencial
                            {
                                IdWhatsAppUsuario = userLogin.IdWhatsAppUsuario,
                                IdWhatsAppConfiguracion = _credencialesHost.Id,
                                UserAuthToken = item.token,
                                ExpiresAfter = Convert.ToDateTime(item.expires_after),
                                EsMigracion = true,
                                Estado = true,
                                FechaCreacion = DateTime.Now,
                                FechaModificacion = DateTime.Now,
                                UsuarioCreacion = "whatsapp",
                                UsuarioModificacion = "whatsapp"
                            };

                            var rpta = _repTokenUsuario.Insert(modelCredencial);
                            _tokenComunicacion = item.token;
                        }
                        banderaLogin = true;
                    }
                    else
                    {
                        banderaLogin = false;
                    }
                }
                else
                {
                    _tokenComunicacion = tokenValida.UserAuthToken;
                    banderaLogin = true;
                }

                urlToPost = _credencialesHost.UrlWhatsApp + "/v1/contacts";

                if (banderaLogin)
                {
                    using (WebClient client = new WebClient())
                    {
                        client.Encoding = Encoding.UTF8;
                        var serializer = new JavaScriptSerializer();
                        var serializedResult = serializer.Serialize(DTO);
                        client.Headers[HttpRequestHeader.Authorization] = "Bearer " + _tokenComunicacion;
                        client.Headers[HttpRequestHeader.ContentLength] = mensajeJSON.Length.ToString();
                        client.Headers[HttpRequestHeader.Host] = _credencialesHost.IpHost;
                        client.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                        resultado = client.UploadString(urlToPost, serializedResult);
                    }

                    var datoRespuesta = JsonConvert.DeserializeObject<numerosValidos>(resultado);
                    var resultadoEstadoContactoWhatsApp = datoRespuesta.contacts.FirstOrDefault();
                    if (resultadoEstadoContactoWhatsApp.status == "invalid")
                    {
                        this.IdEstadoContactoWhatsApp = ValorEstatico.IdEstadoContactoWhatsAppInvalido;
                    }
                    else
                    {
                        this.IdEstadoContactoWhatsApp = ValorEstatico.IdEstadoContactoWhatsAppValido;
                    }
                }
                else
                {
                    this.IdEstadoContactoWhatsApp = ValorEstatico.IdEstadoContactoWhatsAppSinValidar;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// Autor: Jashin Salazar
        /// Fecha: 11/10/2021
        /// Version: 1.0
        /// <summary>
        /// Calcula el estado de WhatsApp del contacto
        /// </summary>
        /// <param name="contexto">Contexto a usar</param>
        /// <returns>Configura el IdEstadoWhatsApp del alumno</returns>
        public void ValidarEstadoContactoWhatsAppTemporal(integraDBContext contexto)
        {
            try
            {
                string urlToPost;
                bool banderaLogin = false;
                string _tokenComunicacion = string.Empty;
                var _idPersonal = 4589;//TODO

                WhatsAppConfiguracionRepositorio _repCredenciales = new WhatsAppConfiguracionRepositorio(contexto);
                WhatsAppUsuarioCredencialRepositorio _repTokenUsuario = new WhatsAppUsuarioCredencialRepositorio(contexto);
                WhatsAppMensajePublicidadBO whatsAppMensajePublicidad = new WhatsAppMensajePublicidadBO();

                ValidarNumerosWhatsAppDTO DTO = new ValidarNumerosWhatsAppDTO
                {
                    contacts = new List<string>(),
                    blocking = "wait"
                };
                DTO.contacts.Add("+" + this.NroCelularCompleto);
                ServicePointManager.ServerCertificateValidationCallback =
                delegate (object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
                {
                    return true;
                };

                // Correccion temporal
                int[] listaIdPaisesServidoresDedicados = new int[3] { 51/*Peru*/, 57/*Colombia*/, 591 /*Bolivia*/};

                var _credencialesHost = _repCredenciales.ObtenerCredencialHost(listaIdPaisesServidoresDedicados.Contains(this.IdCodigoPais.Value) ? this.IdCodigoPais.Value : 0/*Internacional*/);
                var tokenValida = _repTokenUsuario.ValidarCredencialesUsuario(_idPersonal, this.IdCodigoPais.Value);

                var mensajeJSON = JsonConvert.SerializeObject(DTO);

                string resultado = string.Empty;

                if (tokenValida == null || DateTime.Now >= tokenValida.ExpiresAfter)
                {
                    string urlToPostUsuario = _credencialesHost.UrlWhatsApp + "/v1/users/login";

                    var userLogin = _repTokenUsuario.CredencialUsuarioLogin(_idPersonal);

                    var client = new RestClient(urlToPostUsuario);
                    var request = new RestSharp.RestRequest(Method.POST);
                    request.AddHeader("cache-control", "no-cache");
                    request.AddHeader("Content-Length", "");
                    request.AddHeader("Accept-Encoding", "gzip, deflate");
                    request.AddHeader("Host", _credencialesHost.IpHost);
                    request.AddHeader("Cache-Control", "no-cache");
                    request.AddHeader("Authorization", "Basic " + Convert.ToBase64String(Encoding.ASCII.GetBytes(userLogin.UserUsername + ":" + userLogin.UserPassword)));
                    request.AddHeader("Content-Type", "application/json");
                    IRestResponse response = client.Execute(request);

                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        var datos = JsonConvert.DeserializeObject<userLogeo>(response.Content);

                        foreach (var item in datos.users)
                        {
                            TWhatsAppUsuarioCredencial modelCredencial = new TWhatsAppUsuarioCredencial
                            {
                                IdWhatsAppUsuario = userLogin.IdWhatsAppUsuario,
                                IdWhatsAppConfiguracion = _credencialesHost.Id,
                                UserAuthToken = item.token,
                                ExpiresAfter = Convert.ToDateTime(item.expires_after),
                                EsMigracion = true,
                                Estado = true,
                                FechaCreacion = DateTime.Now,
                                FechaModificacion = DateTime.Now,
                                UsuarioCreacion = "whatsapp",
                                UsuarioModificacion = "whatsapp"
                            };

                            var rpta = _repTokenUsuario.Insert(modelCredencial);
                            _tokenComunicacion = item.token;
                        }
                        banderaLogin = true;
                    }
                    else
                    {
                        banderaLogin = false;
                    }
                }
                else
                {
                    _tokenComunicacion = tokenValida.UserAuthToken;
                    banderaLogin = true;
                }

                urlToPost = _credencialesHost.UrlWhatsApp + "/v1/contacts";

                if (banderaLogin)
                {
                    using (WebClient client = new WebClient())
                    {
                        client.Encoding = Encoding.UTF8;
                        var serializer = new JavaScriptSerializer();
                        var serializedResult = serializer.Serialize(DTO);
                        client.Headers[HttpRequestHeader.Authorization] = "Bearer " + _tokenComunicacion;
                        client.Headers[HttpRequestHeader.ContentLength] = mensajeJSON.Length.ToString();
                        client.Headers[HttpRequestHeader.Host] = _credencialesHost.IpHost;
                        client.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                        resultado = client.UploadString(urlToPost, serializedResult);
                    }

                    var datoRespuesta = JsonConvert.DeserializeObject<numerosValidos>(resultado);
                    var resultadoEstadoContactoWhatsApp = datoRespuesta.contacts.FirstOrDefault();
                    if (datoRespuesta == null)
                    {
                        this.IdEstadoContactoWhatsApp = ValorEstatico.IdEstadoContactoWhatsAppSinValidar;
                    }
                    else
                    {
                        if (resultadoEstadoContactoWhatsApp.status == "invalid")
                        {
                            this.IdEstadoContactoWhatsApp = ValorEstatico.IdEstadoContactoWhatsAppInvalido;
                        }
                        else
                        {
                            this.IdEstadoContactoWhatsApp = ValorEstatico.IdEstadoContactoWhatsAppValido;
                        }
                    }
                }
                else
                {
                    this.IdEstadoContactoWhatsApp = ValorEstatico.IdEstadoContactoWhatsAppSinValidar;
                }
            }
            catch (Exception e)
            {
            }
        }
        /// Autor: Jashin Salazar
        /// Fecha: 11/10/2021
        /// Version: 1.0
        /// <summary>
        /// Calcula el estado de WhatsApp del contacto
        /// </summary>
        /// <param name="contexto">Contexto a usar</param>
        /// <returns>Configura el IdEstadoWhatsApp del alumno</returns>
        public List<AlumnoBO> ValidarEstadoContactoWhatsAppMasivo(integraDBContext contexto, int idPais, List<AlumnoBO> alumnos)
        {
            try
            {
                string urlToPost;
                bool banderaLogin = false;
                string _tokenComunicacion = string.Empty;
                var idPersonal = 4589;//TODO
                bool secundario = false;

                WhatsAppConfiguracionRepositorio _repCredenciales = new WhatsAppConfiguracionRepositorio(contexto);
                WhatsAppUsuarioCredencialRepositorio _repTokenUsuario = new WhatsAppUsuarioCredencialRepositorio(contexto);
                WhatsAppMensajePublicidadBO whatsAppMensajePublicidad = new WhatsAppMensajePublicidadBO();

                ValidarNumerosWhatsAppDTO DTO = new ValidarNumerosWhatsAppDTO
                {
                    contacts = new List<string>(),
                    blocking = "wait"
                };
                ValidarNumerosWhatsAppDTO DTOSecundario = new ValidarNumerosWhatsAppDTO
                {
                    contacts = new List<string>(),
                    blocking = "wait"
                };
                foreach (var alumno in alumnos)
                {
                    DTO.contacts.Add("+" + alumno.NroCelularCompleto);
                    if (alumno.NroCelularSecundarioCompleto != "")
                    {
                        secundario = true;
                        DTOSecundario.contacts.Add("+" + alumno.NroCelularSecundarioCompleto);
                    }
                }
                ServicePointManager.ServerCertificateValidationCallback =
                delegate (object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
                {
                    return true;
                };

                var _credencialesHost = _repCredenciales.ObtenerCredencialHost(idPais);
                var tokenValida = _repTokenUsuario.ValidarCredencialesUsuario(idPersonal,idPais);

                var mensajeJSON = JsonConvert.SerializeObject(DTO);
                var mensajeJSONSecundario = JsonConvert.SerializeObject(DTOSecundario);

                string resultado = string.Empty;
                string resultado2 = string.Empty;

                if (tokenValida == null || DateTime.Now >= tokenValida.ExpiresAfter)
                {
                    string urlToPostUsuario = _credencialesHost.UrlWhatsApp + "/v1/users/login";

                    var userLogin = _repTokenUsuario.CredencialUsuarioLogin(idPersonal);

                    var client = new RestClient(urlToPostUsuario);
                    var request = new RestSharp.RestRequest(Method.POST);
                    request.AddHeader("cache-control", "no-cache");
                    request.AddHeader("Content-Length", "");
                    request.AddHeader("Accept-Encoding", "gzip, deflate");
                    request.AddHeader("Host", _credencialesHost.IpHost);
                    request.AddHeader("Cache-Control", "no-cache");
                    request.AddHeader("Authorization", "Basic " + Convert.ToBase64String(Encoding.ASCII.GetBytes(userLogin.UserUsername + ":" + userLogin.UserPassword)));
                    request.AddHeader("Content-Type", "application/json");
                    IRestResponse response = client.Execute(request);

                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        var datos = JsonConvert.DeserializeObject<userLogeo>(response.Content);

                        foreach (var item in datos.users)
                        {
                            TWhatsAppUsuarioCredencial modelCredencial = new TWhatsAppUsuarioCredencial
                            {
                                IdWhatsAppUsuario = userLogin.IdWhatsAppUsuario,
                                IdWhatsAppConfiguracion = _credencialesHost.Id,
                                UserAuthToken = item.token,
                                ExpiresAfter = Convert.ToDateTime(item.expires_after),
                                EsMigracion = true,
                                Estado = true,
                                FechaCreacion = DateTime.Now,
                                FechaModificacion = DateTime.Now,
                                UsuarioCreacion = "whatsapp",
                                UsuarioModificacion = "whatsapp"
                            };

                            var rpta = _repTokenUsuario.Insert(modelCredencial);
                            _tokenComunicacion = item.token;
                        }
                        banderaLogin = true;
                    }
                    else
                    {
                        banderaLogin = false;
                    }
                }
                else
                {
                    _tokenComunicacion = tokenValida.UserAuthToken;
                    banderaLogin = true;
                }

                urlToPost = _credencialesHost.UrlWhatsApp + "/v1/contacts";

                if (banderaLogin)
                {
                    try
                    {
                        //using ()
                        //{
                        WebClient client = new WebClient();
                        client.Encoding = Encoding.UTF8;
                        var serializer = new JavaScriptSerializer();
                        var serializedResult = serializer.Serialize(DTO);
                        client.Headers[HttpRequestHeader.Authorization] = "Bearer " + _tokenComunicacion;
                        client.Headers[HttpRequestHeader.ContentLength] = mensajeJSON.Length.ToString();
                        client.Headers[HttpRequestHeader.Host] = _credencialesHost.IpHost;
                        client.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                        resultado = client.UploadString(urlToPost, serializedResult);
                        //}

                        var datoRespuesta = JsonConvert.DeserializeObject<numerosValidos>(resultado);
                        numerosValidos datoRespuestaSecundario = new numerosValidos();

                        if (secundario)
                        {
                            WebClient client2 = new WebClient();
                            client2.Encoding = Encoding.UTF8;
                            var serializer2 = new JavaScriptSerializer();
                            var serializedResult2 = serializer2.Serialize(DTOSecundario);
                            client2.Headers[HttpRequestHeader.Authorization] = "Bearer " + _tokenComunicacion;
                            client2.Headers[HttpRequestHeader.ContentLength] = mensajeJSONSecundario.Length.ToString();
                            client2.Headers[HttpRequestHeader.Host] = _credencialesHost.IpHost;
                            client2.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                            resultado2 = client2.UploadString(urlToPost, serializedResult2);
                            datoRespuestaSecundario = JsonConvert.DeserializeObject<numerosValidos>(resultado2);
                        }
                        //foreach(var item in datoRespuesta.contacts)
                        //{
                        //    var alumno = alumnos.FirstOrDefault(x => x.Celular == item.input);
                        //}
                        for (int i = 0; i < alumnos.Count; i++)
                        {
                            var estadoCelular = datoRespuesta.contacts.FirstOrDefault(x => x.input.Contains(alumnos[i].NroCelularCompleto));
                            if (estadoCelular.status == "invalid")
                            {
                                alumnos[i].IdEstadoContactoWhatsApp = ValorEstatico.IdEstadoContactoWhatsAppInvalido;
                            }
                            else
                            {
                                alumnos[i].IdEstadoContactoWhatsApp = ValorEstatico.IdEstadoContactoWhatsAppValido;
                            }
                            if (!string.IsNullOrEmpty(alumnos[i].Celular2))
                            {
                                var estadoCelularSecundario = datoRespuestaSecundario.contacts.FirstOrDefault(x => x.input.Contains(alumnos[i].NroCelularSecundarioCompleto));
                                if (estadoCelularSecundario.status == "invalid")
                                {
                                    alumnos[i].IdEstadoContactoWhatsAppSecundario = ValorEstatico.IdEstadoContactoWhatsAppInvalido;
                                }
                                else
                                {
                                    alumnos[i].IdEstadoContactoWhatsAppSecundario = ValorEstatico.IdEstadoContactoWhatsAppValido;
                                }
                            }
                            alumnos[i].UsuarioModificacion = "jsalazart4";
                            alumnos[i].FechaModificacion = DateTime.Now;
                        }
                    }
                    catch (Exception e)
                    {
                        return null;
                    }
                    
                }
                else
                {
                    for (int i = 0; i < alumnos.Count; i++)
                    {
                        alumnos[i].IdEstadoContactoWhatsApp = ValorEstatico.IdEstadoContactoWhatsAppSinValidar;
                        alumnos[i].IdEstadoContactoWhatsAppSecundario = ValorEstatico.IdEstadoContactoWhatsAppSinValidar;
                    }
                }
                return alumnos;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// Autor: Jashin Salazar
        /// Fecha: 11/10/2021
        /// Version: 1.0
        /// <summary>
        /// Calcula el estado de WhatsApp del contacto
        /// </summary>
        /// <param name="contexto">Contexto a usar</param>
        /// <returns>Configura el IdEstadoWhatsApp del alumno</returns>
        public void ActualizacionMasivaEstadoWhatsApp(integraDBContext contexto, List<AlumnoBO> alumnos)
        {
            try
            {
                var alumnosValidos= alumnos.Where(x => x.IdEstadoContactoWhatsApp==1).Select(y => y.Id).ToList();
                var alumnosModificados = String.Join(",", alumnosValidos);
                var resp=_repAlumno.ActualizarValidos(alumnosModificados, 1);
                var alumnosNoValidos = alumnos.Where(x => x.IdEstadoContactoWhatsApp == 2).Select(y => y.Id).ToList();
                alumnosModificados = String.Join(",", alumnosNoValidos);
                resp = _repAlumno.ActualizarValidos(alumnosModificados, 2);
                var alumnosSinValidar = alumnos.Where(x => x.IdEstadoContactoWhatsApp == 3).Select(y => y.Id).ToList();
                alumnosModificados = String.Join(",", alumnosSinValidar);
                resp = _repAlumno.ActualizarValidos(alumnosModificados, 3);
                var alumnosErrorValidar = alumnos.Where(x => x.IdEstadoContactoWhatsApp == 4).Select(y => y.Id).ToList();
                alumnosModificados = String.Join(",", alumnosErrorValidar);
                resp = _repAlumno.ActualizarValidos(alumnosModificados, 4);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// Autor: Jashin Salazar
        /// Fecha: 11/10/2021
        /// Version: 1.0
        /// <summary>
        /// Calcula el estado de WhatsApp del contacto
        /// </summary>
        /// <param name="contexto">Contexto a usar</param>
        /// <returns>Configura el IdEstadoWhatsApp del alumno</returns>
        public void ActualizacionMasivaEstadoWhatsAppSecundario(integraDBContext contexto, List<AlumnoBO> alumnos)
        {
            try
            {
                var alumnosValidos = alumnos.Where(x => x.IdEstadoContactoWhatsAppSecundario == 1).Select(y => y.Id).ToList();
                var alumnosModificados = String.Join(",", alumnosValidos);
                var resp = _repAlumno.ActualizarValidosSecundario(alumnosModificados, 1);
                var alumnosNoValidos = alumnos.Where(x => x.IdEstadoContactoWhatsAppSecundario == 2).Select(y => y.Id).ToList();
                alumnosModificados = String.Join(",", alumnosNoValidos);
                resp = _repAlumno.ActualizarValidosSecundario(alumnosModificados, 2);
                var alumnosSinValidar = alumnos.Where(x => x.IdEstadoContactoWhatsAppSecundario == 3).Select(y => y.Id).ToList();
                alumnosModificados = String.Join(",", alumnosSinValidar);
                resp = _repAlumno.ActualizarValidosSecundario(alumnosModificados, 3);
                var alumnosErrorValidar = alumnos.Where(x => x.IdEstadoContactoWhatsAppSecundario == 4).Select(y => y.Id).ToList();
                alumnosModificados = String.Join(",", alumnosErrorValidar);
                resp = _repAlumno.ActualizarValidosSecundario(alumnosModificados, 4);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// <summary>
        /// Obtiene el nro de celular del contacto mas la extension del pais
        /// </summary>
        /// <returns></returns>
        private string ObtenerNroCelularCompleto()
        {
            try
            {
                var _numeroCelular = "";
                if (!string.IsNullOrEmpty(this.Celular))
                {
                    _numeroCelular = this.Celular;
                    _numeroCelular= _numeroCelular.TrimStart('0');
                    if (this.IdCodigoPais == 591 && _numeroCelular.StartsWith("591"))
                    {
                        _numeroCelular = _numeroCelular.Length>11?_numeroCelular.Substring(0, 11):_numeroCelular;
                    }
                    else if (this.IdCodigoPais == 591 && !_numeroCelular.StartsWith("591"))
                    {
                        _numeroCelular = _numeroCelular.Length > 8? _numeroCelular.Substring(0, 8):_numeroCelular;
                        _numeroCelular = string.Concat("591", _numeroCelular);
                    }
                    else if (this.IdCodigoPais == 57 &&  _numeroCelular.StartsWith("57"))
                    {
                        _numeroCelular = _numeroCelular.Length > 12 ? _numeroCelular.Substring(0, 12):_numeroCelular;
                    }
                    else if (this.IdCodigoPais == 57 && !_numeroCelular.StartsWith("57"))
                    {
                        _numeroCelular = _numeroCelular.Length > 10 ? _numeroCelular.Substring(0, 10) : _numeroCelular;
                        _numeroCelular = string.Concat("57", _numeroCelular);
                    }
                    else if (this.IdCodigoPais == 51 && _numeroCelular.StartsWith("51"))
                    {
                        _numeroCelular = _numeroCelular.Length > 11 ? _numeroCelular.Substring(0, 11) : _numeroCelular;
                    }
                    else if (this.IdCodigoPais == 51 && !_numeroCelular.StartsWith("51"))
                    {
                        _numeroCelular = _numeroCelular.Length > 9 ? _numeroCelular.Substring(0, 9) : _numeroCelular;
                        _numeroCelular = string.Concat("51", _numeroCelular);
                    }
                }
                return _numeroCelular;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// BO/Repositorio: Planificacion/Expositor
        /// Autor: Nombre del desarrollador
        /// Fecha: día/mes/año
        /// <summary>
        /// Obtiene el nro de celular del contacto mas la extension del pais
        /// </summary>
        /// <returns></returns>
        private string ObtenerNroCelularCompletoSecundario()
        {
            try
            {
                var _numeroCelular = "";
                if (!string.IsNullOrEmpty(this.Celular2))
                {
                    _numeroCelular = this.Celular2;
                    if (this.IdCodigoPais == 591 && _numeroCelular.StartsWith("591"))
                    {
                        _numeroCelular = _numeroCelular.Length > 11 ? _numeroCelular.Substring(0, 11) : _numeroCelular;
                    }
                    else if (this.IdCodigoPais == 591 && !_numeroCelular.StartsWith("591"))
                    {
                        _numeroCelular = _numeroCelular.Length > 8 ? _numeroCelular.Substring(0, 8) : _numeroCelular;
                        _numeroCelular = string.Concat("591", _numeroCelular);
                    }
                    else if (this.IdCodigoPais == 57 && _numeroCelular.StartsWith("57"))
                    {
                        _numeroCelular = _numeroCelular.Length > 12 ? _numeroCelular.Substring(0, 12) : _numeroCelular;
                    }
                    else if (this.IdCodigoPais == 57 && !_numeroCelular.StartsWith("57"))
                    {
                        _numeroCelular = _numeroCelular.Length > 10 ? _numeroCelular.Substring(0, 10) : _numeroCelular;
                        _numeroCelular = string.Concat("57", _numeroCelular);
                    }
                    else if (this.IdCodigoPais == 51 && _numeroCelular.StartsWith("51"))
                    {
                        _numeroCelular = _numeroCelular.Length > 11 ? _numeroCelular.Substring(0, 11) : _numeroCelular;
                    }
                    else if (this.IdCodigoPais == 51 && !_numeroCelular.StartsWith("51"))
                    {
                        _numeroCelular = _numeroCelular.Length > 9 ? _numeroCelular.Substring(0, 9) : _numeroCelular;
                        _numeroCelular = string.Concat("51", _numeroCelular);
                    }
                }
                return _numeroCelular;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// <summary>
        /// Obtiene el telefono en base al pais y ciudad
        /// </summary>
        /// <returns></returns>
        private string ObtenerNroTelefonoCoordinador()
        {
            try
            {
                var numeroTelefono = "";
                if (this.IdCodigoPais == 51 && this.IdCiudad == 4)
                {
                    numeroTelefono += "(51) 54 258787";
                }
                else if (this.IdCodigoPais == 51 && this.IdCiudad == 14)
                {
                    numeroTelefono += "(51) 1 207 2770";
                }
                else if (this.IdCodigoPais == 57)
                {
                    numeroTelefono += "(57) 1 3819462";
                }
                else if (this.IdCodigoPais == 591)
                {
                    numeroTelefono += "(591) 3 3403140";
                }
                else
                {
                    numeroTelefono += "(51) 1 207 2770";
                }
                return numeroTelefono;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public bool ValidarNumeroEnvioWhatsApp(int IdPersonal, int IdPais, ValidarNumerosWhatsAppAsyncDTO DTO)
        {
            if (DTO != null)
            {
                string urlToPost;
                bool banderaLogin = false;
                string _tokenComunicacion = string.Empty;

                try
                {
                    ServicePointManager.ServerCertificateValidationCallback =
                    delegate (object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
                    {
                        return true;
                    };

                    WhatsAppConfiguracionRepositorio _repCredenciales = new WhatsAppConfiguracionRepositorio();
                    WhatsAppUsuarioCredencialRepositorio _repTokenUsuario = new WhatsAppUsuarioCredencialRepositorio();

                    var _credencialesHost = _repCredenciales.ObtenerCredencialHost(IdPais);
                    var tokenValida = _repTokenUsuario.ValidarCredencialesUsuario(IdPersonal, IdPais);

                    var mensajeJSON = JsonConvert.SerializeObject(DTO);

                    string resultado = string.Empty;

                    if (tokenValida == null || DateTime.Now >= tokenValida.ExpiresAfter)
                    {
                        string urlToPostUsuario = _credencialesHost.UrlWhatsApp + "/v1/users/login";

                        var userLogin = _repTokenUsuario.CredencialUsuarioLogin(IdPersonal);

                        var client = new RestClient(urlToPostUsuario);
                        var request = new RestSharp.RestRequest(Method.POST);
                        request.AddHeader("cache-control", "no-cache");
                        request.AddHeader("Content-Length", "");
                        request.AddHeader("Accept-Encoding", "gzip, deflate");
                        request.AddHeader("Host", _credencialesHost.IpHost);
                        request.AddHeader("Cache-Control", "no-cache");
                        request.AddHeader("Authorization", "Basic " + Convert.ToBase64String(Encoding.ASCII.GetBytes(userLogin.UserUsername + ":" + userLogin.UserPassword)));
                        request.AddHeader("Content-Type", "application/json");
                        IRestResponse response = client.Execute(request);

                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            banderaLogin = true;
                        }
                        else
                        {
                            banderaLogin = false;
                        }
                    }
                    else
                    {
                        _tokenComunicacion = tokenValida.UserAuthToken;
                        banderaLogin = true;
                    }

                    urlToPost = _credencialesHost.UrlWhatsApp + "/v1/contacts";

                    if (banderaLogin)
                    {
                        using (WebClient client = new WebClient())
                        {
                            client.Encoding = Encoding.UTF8;

                            var serializer = new JavaScriptSerializer();

                            var serializedResult = serializer.Serialize(DTO);
                            string myParameters = serializedResult;
                            client.Headers[HttpRequestHeader.Authorization] = "Bearer " + _tokenComunicacion;
                            client.Headers[HttpRequestHeader.ContentLength] = mensajeJSON.Length.ToString();
                            client.Headers[HttpRequestHeader.Host] = _credencialesHost.IpHost;
                            client.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                            resultado = client.UploadString(urlToPost, myParameters);
                        }

                        var datoRespuesta = JsonConvert.DeserializeObject<numerosValidos>(resultado);
                        if (datoRespuesta.contacts[0].status == "invalid")
                        {
                            return false;
                        }
                        else
                        {
                            return true;
                        }
                    }
                    else
                    {
                        return false;
                    }

                }
                catch (Exception ex)
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }



        public bool ValidarNumeroEnvioWhatsAppMasivo(int IdAlumno, int IdPais, string Celular, ValidarNumerosWhatsAppAsyncDTO DTO)
        {
            if (DTO != null)
            {
                string urlToPost;
                bool banderaLogin = false;
                string _tokenComunicacion = string.Empty;

                try
                {
                    ServicePointManager.ServerCertificateValidationCallback =
                    delegate (object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
                    {
                        return true;
                    };

                    WhatsAppConfiguracionRepositorio _repCredenciales = new WhatsAppConfiguracionRepositorio();
                    WhatsAppUsuarioCredencialRepositorio _repTokenUsuario = new WhatsAppUsuarioCredencialRepositorio();

                    var _credencialesHost = _repCredenciales.ObtenerCredencialHost(IdPais);
                    var tokenValida = _repTokenUsuario.ValidarCredencialesUsuario(IdAlumno, IdPais);

                    var mensajeJSON = JsonConvert.SerializeObject(DTO);

                    string resultado = string.Empty;

                    if (tokenValida == null || DateTime.Now >= tokenValida.ExpiresAfter)
                    {
                        string urlToPostUsuario = _credencialesHost.UrlWhatsApp + "/v1/users/login";

                        var userLogin = _repTokenUsuario.CredencialUsuarioLogin(88);

                        var client = new RestClient(urlToPostUsuario);
                        var request = new RestSharp.RestRequest(Method.POST);
                        request.AddHeader("cache-control", "no-cache");
                        request.AddHeader("Content-Length", "");
                        request.AddHeader("Accept-Encoding", "gzip, deflate");
                        request.AddHeader("Host", _credencialesHost.IpHost);
                        request.AddHeader("Cache-Control", "no-cache");
                        request.AddHeader("Authorization", "Basic " + Convert.ToBase64String(Encoding.ASCII.GetBytes(userLogin.UserUsername + ":" + userLogin.UserPassword)));
                        request.AddHeader("Content-Type", "application/json");
                        IRestResponse response = client.Execute(request);

                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            banderaLogin = true;
                        }
                        else
                        {
                            banderaLogin = false;
                        }
                    }
                    else
                    {
                        _tokenComunicacion = tokenValida.UserAuthToken;
                        banderaLogin = true;
                    }

                    urlToPost = _credencialesHost.UrlWhatsApp + "/v1/contacts";

                    if (banderaLogin)
                    {
                        using (WebClient client = new WebClient())
                        {
                            client.Encoding = Encoding.UTF8;

                            var serializer = new JavaScriptSerializer();

                            var serializedResult = serializer.Serialize(DTO);
                            string myParameters = serializedResult;
                            client.Headers[HttpRequestHeader.Authorization] = "Bearer " + _tokenComunicacion;
                            client.Headers[HttpRequestHeader.ContentLength] = mensajeJSON.Length.ToString();
                            client.Headers[HttpRequestHeader.Host] = _credencialesHost.IpHost;
                            client.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                            resultado = client.UploadString(urlToPost, myParameters);
                        }

                        var datoRespuesta = JsonConvert.DeserializeObject<numerosValidos>(resultado);
                        if (datoRespuesta.contacts[0].status == "invalid")
                        {
                            return false;
                        }
                        else
                        {
                            return true;
                        }
                    }
                    else
                    {
                        return false;
                    }

                }
                catch (Exception ex)
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Obtiene el nro de celular del contacto mas la extension del pais
        /// </summary>
        /// <returns></returns>
        private string ObtenerNombreCompleto()
        {
            try
            {
                return string.Concat(this.Nombre1, " ", this.Nombre2, " ", this.ApellidoPaterno, " ", this.ApellidoMaterno).ToUpper();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Mapea los nombres del alumno
        /// </summary>
        /// <param name="nombresAlumno">Nombres del alumno</param>
        /// <returns>Bool</returns>
        public bool MapearNombresAlumno(string nombresAlumno)
        {
            try
            {
                string preNombres = System.Text.RegularExpressions.Regex.Replace(nombresAlumno, @"\s+", " ");

                var nombres = _parser.ParserCaracteres(preNombres).Split(new char[] { ' ' }).ToList()
                .Where(s => s.Trim().Length > 0).Select(s => s.Substring(0, 1).ToUpper() + s.Substring(1, s.Length - 1).ToLower()).ToList();

                // Nombres
                if (nombres.Count == 1)
                {
                    this.Nombre1 = nombres.FirstOrDefault().Length >= 100 ? nombres.FirstOrDefault().Substring(0, 100) : nombres.FirstOrDefault();
                    this.Nombre2 = string.Empty;
                }
                else if (nombres.Count == 2)
                {
                    this.Nombre1 = nombres.FirstOrDefault().Length >= 100 ? nombres.FirstOrDefault().Substring(0, 100) : nombres.FirstOrDefault();
                    this.Nombre2 = nombres[1].Length >= 100 ? nombres[1].Substring(0, 100) : nombres[1];
                }
                else if (nombres.Count > 2)
                {
                    this.Nombre1 = string.Join(" ", nombres.ToArray()).Length >= 100 ? String.Join(" ", nombres.ToArray()).Substring(0, 100) : String.Join(" ", nombres.ToArray());
                    this.Nombre2 = string.Empty;
                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Mapea los apellidos del alumno
        /// </summary>
        /// <param name="apellidosAlumno">Apellidos del alumno</param>
        /// <returns>Bool</returns>
        public bool MapearApellidosAlumno(string apellidosAlumno)
        {
            try
            {
                //Apellidos
                apellidosAlumno = apellidosAlumno ?? string.Empty;

                string preApellidos = System.Text.RegularExpressions.Regex.Replace(apellidosAlumno, @"\s+", " ");
                var apellidos = _parser.ParserCaracteres(preApellidos).Split(new char[] { ' ' }).ToList()
                    .Where(s => s.Trim().Length > 0).Select(s => s.Substring(0, 1).ToUpper() + s.Substring(1, s.Length - 1).ToLower()).ToList();

                if (apellidos.Count == 1)
                {
                    this.ApellidoPaterno = apellidos.FirstOrDefault().Length >= 100 ? apellidos.FirstOrDefault().Substring(0, 100) : apellidos.FirstOrDefault();
                    this.ApellidoMaterno = string.Empty;
                }
                else if (apellidos.Count == 2)
                {
                    this.ApellidoPaterno = apellidos.FirstOrDefault().Length >= 100 ? apellidos.FirstOrDefault().Substring(0, 100) : apellidos.FirstOrDefault();
                    this.ApellidoMaterno = apellidos[1].Length >= 100 ? apellidos[1].Substring(0, 100) : apellidos[1];
                }
                else if (apellidos.Count > 2)
                {
                    this.ApellidoPaterno = String.Join(" ", apellidos.ToArray()).Length >= 100 ? String.Join(" ", apellidos.ToArray()).Substring(0, 100) : String.Join(" ", apellidos.ToArray());
                    this.ApellidoMaterno = string.Empty;
                }
                else
                {
                    this.ApellidoPaterno = string.Empty;
                    this.ApellidoMaterno = string.Empty;
                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Actualiza datos basicos del alumno
        /// </summary>
        /// <param name="datosAlumno">Objeto de clase OportunidadWhatsAppAlumnoDTO</param>
        /// <returns>Lista de objetos de clase AlumnoLogBO</returns>
        public List<AlumnoLogBO> ActualizarDatosAlumno(OportunidadWhatsAppAlumnoActualizableDTO datosAlumno)
        {
            try
            {
                var listaAlumnoLog = new List<AlumnoLogBO>();

                string nombre1Anterior = Nombre1;
                string nombre2Anterior = Nombre2;
                string apellidoPaternoAnterior = ApellidoPaterno;
                string apellidoMaternoAnterior = ApellidoMaterno;

                bool resultadoMapeoNombres = MapearNombresAlumno(datosAlumno.Nombres);
                bool resultadoMapeoApellidos = MapearApellidosAlumno(datosAlumno.Apellidos);

                if (nombre1Anterior != Nombre1) listaAlumnoLog.Add(new AlumnoLogBO(datosAlumno.Id, "Nombre 1", nombre1Anterior ?? string.Empty, Nombre1 ?? string.Empty, datosAlumno.Usuario));
                if (nombre2Anterior != Nombre2) listaAlumnoLog.Add(new AlumnoLogBO(datosAlumno.Id, "Nombre 2", nombre2Anterior ?? string.Empty, Nombre2 ?? string.Empty, datosAlumno.Usuario));
                if (apellidoPaternoAnterior != ApellidoPaterno) listaAlumnoLog.Add(new AlumnoLogBO(datosAlumno.Id, "Apellido Paterno", apellidoPaternoAnterior ?? string.Empty, ApellidoPaterno ?? string.Empty, datosAlumno.Usuario));
                if (apellidoMaternoAnterior != ApellidoMaterno) listaAlumnoLog.Add(new AlumnoLogBO(datosAlumno.Id, "Apellido Materno", apellidoMaternoAnterior ?? string.Empty, ApellidoMaterno ?? string.Empty, datosAlumno.Usuario));
                if (Email2 != datosAlumno.Email2)
                {
                    listaAlumnoLog.Add(new AlumnoLogBO(datosAlumno.Id, "Email 2", Email2 ?? string.Empty, datosAlumno.Email2 ?? string.Empty, datosAlumno.Usuario));

                    Email2 = datosAlumno.Email2;
                }
                if (IdAformacion != datosAlumno.IdAFormacion)
                {
                    string nombreAreaFormacionAntiguo = IdAformacion != null ? _repAreaFormacion.FirstById(IdAformacion.Value).Nombre : string.Empty;
                    string nombreAreaFormacionNuevo = datosAlumno.IdAFormacion != null ? _repAreaFormacion.FirstById(datosAlumno.IdAFormacion.Value).Nombre : string.Empty;

                    if (!string.IsNullOrEmpty(nombreAreaFormacionAntiguo) && string.IsNullOrEmpty(nombreAreaFormacionNuevo))
                    {
                        throw new Exception("No se permite eliminar el area de formacion del alumno");
                    }

                    IdAformacion = datosAlumno.IdAFormacion;

                    listaAlumnoLog.Add(new AlumnoLogBO(datosAlumno.Id, "Formacion", nombreAreaFormacionAntiguo, nombreAreaFormacionNuevo, datosAlumno.Usuario));
                }
                if (IdCargo != datosAlumno.IdCargo)
                {
                    string cargoAntiguo = IdCargo != null ? _repCargo.FirstById(IdCargo.Value).Nombre : string.Empty;
                    string cargoNuevo = datosAlumno.IdCargo != null ? _repCargo.FirstById(datosAlumno.IdCargo.Value).Nombre : string.Empty;

                    if (!string.IsNullOrEmpty(cargoAntiguo) && string.IsNullOrEmpty(cargoNuevo))
                    {
                        throw new Exception("No se permite eliminar el cargo del alumno");
                    }

                    IdCargo = datosAlumno.IdCargo;

                    listaAlumnoLog.Add(new AlumnoLogBO(datosAlumno.Id, "Cargo", cargoAntiguo, cargoNuevo, datosAlumno.Usuario));
                }
                if (IdAtrabajo != datosAlumno.IdATrabajo)
                {
                    string areaTrabajoAntiguo = IdAtrabajo != null ? _repAreaTrabajo.FirstById(IdAtrabajo.Value).Nombre : string.Empty;
                    string areaTrabajoNuevo = datosAlumno.IdATrabajo != null ? _repAreaTrabajo.FirstById(datosAlumno.IdATrabajo.Value).Nombre : string.Empty;

                    if (!string.IsNullOrEmpty(areaTrabajoAntiguo) && string.IsNullOrEmpty(areaTrabajoNuevo))
                    {
                        throw new Exception("No se permite eliminar el area trabajo del alumno");
                    }

                    IdAtrabajo = datosAlumno.IdATrabajo;

                    listaAlumnoLog.Add(new AlumnoLogBO(datosAlumno.Id, "Trabajo", areaTrabajoAntiguo, areaTrabajoNuevo, datosAlumno.Usuario));
                }
                if (IdIndustria != datosAlumno.IdIndustria)
                {
                    string industriaAntigua = IdIndustria != null ? _repIndustria.FirstById(IdIndustria.Value).Nombre : string.Empty;
                    string industriaNueva = datosAlumno.IdIndustria != null ? _repIndustria.FirstById(datosAlumno.IdIndustria.Value).Nombre : string.Empty;

                    if (!string.IsNullOrEmpty(industriaAntigua) && string.IsNullOrEmpty(industriaNueva))
                    {
                        throw new Exception("No se permite eliminar la industria del alumno");
                    }

                    IdIndustria = datosAlumno.IdIndustria;

                    listaAlumnoLog.Add(new AlumnoLogBO(datosAlumno.Id, "Industria", industriaAntigua, industriaNueva, datosAlumno.Usuario));
                }

                return listaAlumnoLog;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
