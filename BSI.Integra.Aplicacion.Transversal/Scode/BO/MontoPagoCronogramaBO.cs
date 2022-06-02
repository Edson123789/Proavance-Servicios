using BSI.Integra.Aplicacion.Base.Classes;
using BSI.Integra.Aplicacion.Classes;
//using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.SCode.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Aplicacion.Transversal.Helper;
using System.Web.Helpers;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    ///BO: MontoPagoCronograma
    ///Autor: Jose Villena.
    ///Fecha: 01/05/2021
    ///<summary>
    ///Columnas de la tabla com.T_MontoPagoCronograma
    ///</summary>
    public class MontoPagoCronogramaBO : BaseBO
    {
        /// Propiedades	                        Significado
        /// -----------	                        ------------
        /// IdOportunidad		                Id de la Oportunidad (PK de la tabla com.T_Oportunidad)
        /// IdMontoPago                         Id del monto pago (PK de la tabla fin.T_MontoPago)
        /// IdPersonal		                    Id del Personal (PK de la tabla gp.T_personal)
        /// Precio		                        Precio de Pago
        /// PrecioDescuento		                Precio de descuento
        /// IdMoneda	                        Id de la moneda (PK de la tabla pla.T_moneda)
        /// IdTipoDescuento                     Id del Tipo de Descuento (PK de la tabla pla.T_TipoDescuento)
        /// EsAprobado                          Flag si es aprobado
        /// NombrePlural                        Nombre en Plural moneda
        /// Formula                             Tipo de formula designado
        /// MatriculaEnProceso                  Flag Matricula en proceso
        /// CodigoMatricula                     Codigo Matricula Alumno
        public int IdOportunidad { get; set; }
        public int IdMontoPago { get; set; }
        public int IdPersonal { get; set; }
        public double Precio { get; set; }
        public double PrecioDescuento { get; set; }
        public int IdMoneda { get; set; }
        public int IdTipoDescuento { get; set; }
        public bool EsAprobado { get; set; }
        public string NombrePlural { get; set; }
        public int Formula { get; set; }
        public int MatriculaEnProceso { get; set; }
        public string CodigoMatricula { get; set; }
        public Guid? IdMigracion { get; set; }

        //Extras
        public string TipoPersonal { get; set; }
        public string Usuario { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int IdAlumnoPortal { get; set; }
        public string CodigoBancario { get; set; }
        public String SimboloMoneda { get; set; }
        public int IdMedioPago { get; set; }
        //BO
        public List<TipoDescuentoBO> ListaTipoDescuento;
        public List<MontosPagosVentasBO> ListaMontosPagosVentas;
        public List<MontoPagoCronogramaDetalleBO> ListaDetalleCuotas;
        //COMPLEMENTARIOS
        public List<DatosMontosComplementariosDTO> ListaMontosComplementarios;
        public MontoPagoBO MontoPago;//complementario
        public AlumnoBO Alumno { get; set; }
        //DTO
        public PEspecificoInformacionDTO PEspecificoInformacion;
        public MontoPagoCronogramaBO()
        {
            inicializar();
        }
        public MontoPagoCronogramaBO(int IdOportunidad, string TipoPersonal, integraDBContext contexto)
        {
            this.IdOportunidad = IdOportunidad;
            this.TipoPersonal = TipoPersonal;
            inicializar();
            CargarTipoDescuento(contexto);
            CargarMostosPagos(contexto);
        }
        private void inicializar()
        {
            ListaTipoDescuento = new List<TipoDescuentoBO>();
            ListaMontosPagosVentas = new List<MontosPagosVentasBO>();
            ListaDetalleCuotas = new List<MontoPagoCronogramaDetalleBO>();
            ListaMontosComplementarios = new List<DatosMontosComplementariosDTO>();
            MontoPago = new MontoPagoBO();//complementario
        }

        private void CargarTipoDescuento(integraDBContext contexto)
        {
            MontoPagoCronogramaRepositorio _repMontoPagoCronograma = new MontoPagoCronogramaRepositorio(contexto);
            ListaTipoDescuento = _repMontoPagoCronograma.ObtenerTipoDescuento(this.IdOportunidad, this.TipoPersonal);
        }

        private void CargarMostosPagos(integraDBContext contexto)
        {
            MontoPagoCronogramaRepositorio _repMontoPagoCronograma = new MontoPagoCronogramaRepositorio(contexto);
            ListaMontosPagosVentas = _repMontoPagoCronograma.ObtenerMontosPagos(this.IdOportunidad);
        }
        public void CalcularCodigoMatricula(int IdAlumno)
        {
            AlumnoRepositorio _repAlumno = new AlumnoRepositorio();
            PespecificoRepositorio _repPespecifico = new PespecificoRepositorio();
            string _codigousuario = string.Empty, codigobanco = string.Empty;
            string firma = string.Empty, codigoAlumno = string.Empty, mensaje = string.Empty;
            int numero = 0;

            string _query = string.Empty;

            //proceso para guardar
            this.Alumno = _repAlumno.FirstById(IdAlumno);
            this.PEspecificoInformacion = _repPespecifico.ObtenerPespecificoPorOportunidad(IdOportunidad);


            if (Int32.TryParse(PEspecificoInformacion.CodigoBanco, out numero))
            {
                numero = (numero / 100);
                codigobanco = numero.ToString();
                _codigousuario = this.Alumno.Id.ToString() + codigobanco;
                _codigousuario = _codigousuario.PadRight(14, '0');
            }
            else
            {
                codigobanco = PEspecificoInformacion.CodigoBanco.ToUpper();
                _codigousuario = this.Alumno.Id.ToString() + codigobanco;
            }

            this.CodigoMatricula = _codigousuario;

            if (this.EsAprobado)
            {
                this.MatriculaEnProceso = 1;
            }
            else
            {
                this.MatriculaEnProceso = 0;
            }
        }
        public void EnlazarAlumno()
        {
            MontoPagoCronogramaRepositorio _repMontoPagoCronograma = new MontoPagoCronogramaRepositorio();
            DatosUsuarioPortalDTO _AspUsers = _repMontoPagoCronograma.ObtenerUsuarioClavePortalWeb(Alumno.Id, Alumno.Email1);
            string _passEncrip = string.Empty;

            if (_AspUsers.UserName != null)
            {
                this.Username = _AspUsers.UserName;
                this.Password = _AspUsers.Password;
                this.IdAlumnoPortal = _AspUsers.IdAlumno;
            }
            else
            {
                Password = CrearClave(6);
                _passEncrip = Crypto.HashPassword(Password);


                if (Alumno.IdCodigoPais == 51)
                {
                    if (Alumno.IdCodigoRegionCiudad.HasValue)
                        Alumno.Telefono = (string.IsNullOrEmpty(Alumno.Telefono) ? "" : "0" + Alumno.IdCodigoRegionCiudad + Alumno.Telefono);
                    Alumno.Celular = Alumno.Celular;
                }
                else
                {
                    if (Alumno.IdCodigoRegionCiudad.HasValue)
                    {
                        if (Alumno.IdCodigoRegionCiudad.Value == 0)
                        {
                            Alumno.Telefono = (string.IsNullOrEmpty(Alumno.Telefono) ? "" : "00" + Alumno.IdCodigoPais.Value.ToString() + Alumno.Telefono);
                            Alumno.Celular = "00" + Alumno.IdCodigoPais.Value.ToString() + Alumno.Celular;
                        }
                        else
                        {
                            Alumno.Telefono = (string.IsNullOrEmpty(Alumno.Telefono) ? "" : "00" + Alumno.IdCodigoPais.Value.ToString() + Alumno.IdCodigoRegionCiudad.Value.ToString() + Alumno.Telefono);
                            Alumno.Celular = "00" + Alumno.IdCodigoPais.Value.ToString() + Alumno.Celular;
                        }
                    }
                }
                _AspUsers = _repMontoPagoCronograma.CrearUsuarioClavePortalWeb(Alumno.Id, Alumno.Email1, Password,
                _passEncrip, (Alumno.Nombre1 + " " + Alumno.Nombre2),
                (Alumno.ApellidoPaterno + " " + Alumno.ApellidoMaterno), Alumno.Telefono, Alumno.Celular, Alumno.IdCodigoRegionCiudad,
                Alumno.IdCodigoPais, DateTime.Now);

                this.Username = _AspUsers.UserName;
                this.Password = _AspUsers.Password;
                this.IdAlumnoPortal = _AspUsers.IdAlumno;
            }
        }
        public void GenerarArchivoCrepCronograma(string EstadoCrep, string Firma)
        {
            CentroCostoRepositorio _repoCentoCosto = new CentroCostoRepositorio();

            string _query = string.Empty;
            string Monto = string.Empty, _Ciudad = string.Empty, _Banco = string.Empty, CodAutoriza = string.Empty, rpta = string.Empty;
            HelperCorreo helpCorreo = new HelperCorreo();
            HelperMensajes helpMensaje = new HelperMensajes();

            var CentroCostos = _repoCentoCosto.FirstBy(w => w.Id == PEspecificoInformacion.IdCentroCosto.Value);

            if (this.ListaDetalleCuotas != null)
            {
                _Ciudad = PEspecificoInformacion.Ciudad.ToUpper();
                if ((_Ciudad == "AREQUIPA" || _Ciudad == "LIMA" || _Ciudad.Contains("ONLINE")) && Alumno.IdCodigoPais.Value != 57 && Alumno.IdCodigoPais.Value != 591 && Alumno.IdCodigoPais.Value != 52)
                {
                    _Banco = "Banco BCP - PERÚ";
                    string _nombrearchivo = string.Empty;
                    string _monadaNombre = string.Empty;
                    _nombrearchivo = "CREP_X";

                    StringBuilder linea = new StringBuilder();
                    string _tiporegistro = "CC";
                    string _codigosucursal = "215";

                    string _nrocuenta = "Credipago a BSG Institute a la cuenta Lima-Soles"; // 215-1863341-0-42 - CCI: 002-215-001863341042-20 - Cuenta Corriente en Soles programas Lima, Online y Aonline
                    string _nrocuentacrep = "1863341"; //seccion del nro de cuenta

                    if (this.NombrePlural == "Dolares")
                    {
                        _monadaNombre = "DOLARES";
                        this.SimboloMoneda = "US$";
                        if (_Ciudad.Contains("AREQUIPA"))
                        {
                            _nrocuenta = "Credipago a BSG Institute a la cuenta Arequipa-Dolares"; //215-2307651-1-32 - CCI: 002-215-002307651132-20 - Cuenta corriente en Dólares programas Arequipa
                            _nrocuentacrep = "2307651";
                        }
                        else//Dolares otros
                        {
                            _nrocuenta = "Credipago a BSG Institute a la cuenta Lima-Dolares"; //215-1870934-1-48 - CCI: 002-215-001870934148-23 - Cuenta corriente en Dólares programas Lima, Online y Aonline
                            _nrocuentacrep = "1870934";
                        }
                    }
                    else
                    {
                        _monadaNombre = "SOLES";
                        this.SimboloMoneda = "S/.";
                        if (_Ciudad.Contains("AREQUIPA"))
                        {
                            _nrocuenta = "Credipago a BSG Institute a la cuenta Arequipa-Soles"; //215-1863344-0-72 - CCI: 002-215-001863344072-24 - Cuenta Corriente en Soles programas Arequipa
                            _nrocuentacrep = "1863344";
                        }
                        else//Soles otros
                        {
                            _nrocuenta = "Credipago a BSG Institute a la cuenta Lima-Soles"; //215-1863341-0-42 - CCI: 002-215-001863341042-20 - Cuenta corriente en Dólares programas Lima, Online y Aonline
                            _nrocuentacrep = "1863341";
                        }
                    }
                    string _codigomoneda = (_monadaNombre == "SOLES" ? "0" : "1");

                    string _tipovalidacion = "C";

                    this.CodigoBancario = _nrocuenta;

                    string _nombreempresa = ("BSGINSTITUTE" + _Ciudad + _monadaNombre).PadRight(40);
                    string _fecha = String.Format("{0:yyyyMMdd}", DateTime.Now);
                    string _temp;

                    _temp = this.ListaDetalleCuotas.Count.ToString();

                    string _totalregistros = _temp.ToString().PadLeft(9, '0');
                    string _tiporegistrod = "DD";
                    string _libred = "".PadLeft(47);

                    string _codigousuario = string.Empty, _nombreusuario, _fechaemision, _montomora, _montominimo, _tiporegistro2, nombres_b, apellidos_b;
                    string _codigoespecial, _montocuota, _fechavencimiento, _montototal = string.Empty;
                    string _tipoarchivo = string.Empty;

                    if (EstadoCrep == "Nuevo")
                    {
                        _tipoarchivo = "A";
                    }
                    else
                    {
                        _tipoarchivo = "E";
                    }

                    string _libre = "000".PadLeft(113);
                    DateTime fechaEmision = new DateTime();

                    string codigobanco = string.Empty;
                    int numero = 0;
                    if (Int32.TryParse(PEspecificoInformacion.CodigoBanco, out numero))
                    {
                        numero = (numero / 100);
                        codigobanco = numero.ToString();
                        _codigousuario = Alumno.Id.ToString() + codigobanco;
                        _codigousuario = _codigousuario.PadRight(14, '0');
                    }
                    else
                    {
                        codigobanco = PEspecificoInformacion.CodigoBanco.ToUpper();
                        _codigousuario = Alumno.Id.ToString() + codigobanco;
                    }
                    CodAutoriza = _codigousuario;

                    _codigousuario = _codigousuario.ToString().PadLeft(14);
                    this.CodigoMatricula = _codigousuario;


                    string _auxNombres = string.Empty, _auxApellidos = string.Empty;
                    _auxNombres = Alumno.Nombre1.ToLower() + " " + Alumno.Nombre2.ToLower();
                    _auxApellidos = Alumno.ApellidoPaterno.ToLower() + " " + Alumno.ApellidoMaterno.ToLower();

                    nombres_b = NormalizarCadena(_auxNombres.ToUpper());
                    apellidos_b = NormalizarCadena(_auxApellidos.ToUpper());

                    _nombreusuario = apellidos_b + " " + nombres_b;
                    _nombreusuario = _nombreusuario.ToUpper();
                    _nombreusuario = _nombreusuario.PadRight(40);

                    fechaEmision = DateTime.Now.AddDays(1);
                    _fechaemision = String.Format("{0:yyyyMMdd}", fechaEmision);

                    _montomora = "0".PadLeft(15, '0');
                    _montominimo = "0".PadLeft(9, '0');

                    _montototal = this.PrecioDescuento.ToString().PadLeft(15, '0');

                    string nombreArchivoCrep = string.Empty;
                    if (string.IsNullOrEmpty(Alumno.Dni))
                    {
                        nombreArchivoCrep = _nombrearchivo + "00000000";
                    }
                    else
                    {
                        nombreArchivoCrep = _nombrearchivo + Alumno.Dni;
                    }

                    int contadorpagos = 0;
                    _montototal = this.PrecioDescuento.ToString("0.00");
                    _montototal = _montototal.Replace(".", "").Replace(",", "");
                    byte[] registroCrepByte;

                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (StreamWriter myStreamWriter = new StreamWriter(ms))
                        {
                            linea.Append(_tiporegistro + _codigosucursal + _codigomoneda + _nrocuentacrep + _tipovalidacion + _nombreempresa + _fecha + _totalregistros + _montototal + _tipoarchivo + _libre);
                            rpta = linea.ToString();
                            myStreamWriter.WriteLine(linea.ToString());
                            linea.Remove(0, linea.Length);

                            foreach (var pagos in this.ListaDetalleCuotas)
                            {
                                contadorpagos++;
                                if (contadorpagos < 10)
                                    _codigoespecial = "10" + contadorpagos.ToString() + "01XXXXXX";
                                else
                                    _codigoespecial = "1" + contadorpagos.ToString() + "01XXXXXX";

                                _codigoespecial = _codigoespecial.PadRight(30);
                                _montocuota = String.Format("{0:0.00}", (Convert.ToDouble(pagos.MontoCuotaDescuento.ToString()) + Convert.ToDouble("0"))).Replace(".", "").Replace(",", "").PadLeft(15, '0');
                                _fechavencimiento = String.Format("{0:yyyyMMdd}", Convert.ToDateTime(pagos.FechaPago.ToString()));
                                if (EstadoCrep == "Nuevo")
                                {
                                    _tiporegistro2 = "A";
                                }
                                else
                                {
                                    _tiporegistro2 = "E";
                                }

                                linea.Append(_tiporegistrod + _codigosucursal + _codigomoneda + _nrocuentacrep + _codigousuario + _nombreusuario + _codigoespecial + _fechaemision + _fechavencimiento + _montocuota + _montomora + _montominimo + _tiporegistro2 + _libred);
                                myStreamWriter.WriteLine(linea.ToString());
                                linea.Remove(0, linea.Length);
                            }
                            myStreamWriter.Close();//cerramos al terminar la escritura del archivo crep
                        }
                        registroCrepByte = ms.ToArray();
                    }

                    string mensaje = helpMensaje.mensaje_email_finanzas(Alumno, this.ListaDetalleCuotas.ToList(), _codigousuario + " - " + _nrocuenta, this.SimboloMoneda, CentroCostos);
                    mensaje += "<br><br>" + Firma;

                    List<string> listaCorreos = new List<string>();
                    //listaCorreos.Add("yhuillcapaco@bsginstitute.com");
                    listaCorreos.Add("bamontoya@bsginstitute.com");
                    //listaCorreos.Add("pteran@bsginstitute.com");
                    listaCorreos.Add("mzegarraj@bsginstitute.com");
                    listaCorreos.Add("ccrispin@bsginstitute.com");


                    //helpCorreo.envioEmailAdjuntoBlob("ccrispin@bsginstitute.com", "Archivo CREP " + EstadoCrep + " - Ventas", EstadoCrep + " Archivo CREP " + PEspecificoInformacion.Nombre.ToString(), mensaje, nombreArchivoCrep + "-" + EstadoCrep + ".txt", registroCrepByte, listaCorreos);
                    helpCorreo.envioEmailAdjuntoBlob("bamontoya@bsginstitute.com", "Archivo CREP " + EstadoCrep + " - Ventas", EstadoCrep + " Archivo CREP " + PEspecificoInformacion.Nombre.ToString(), mensaje, nombreArchivoCrep + "-" + EstadoCrep + ".txt", registroCrepByte, listaCorreos);
                    
                }
                else if (Alumno.IdCodigoPais.Value == 591)
                {
                    _Banco = "BCP - Bolivia";
                    if (this.NombrePlural == "Dolares")
                    {
                        this.SimboloMoneda = "US$";
                    }
                    else
                    {
                        this.SimboloMoneda = "BS $";
                    }

                    List<string> listaCorreos = new List<string>();
                    //listaCorreos.Add("yhuillcapaco@bsginstitute.com");
                    listaCorreos.Add("bamontoya@bsginstitute.com");
                    //listaCorreos.Add("rchauca@bsginstitute.com");
                    listaCorreos.Add("mzegarraj@bsginstitute.com");
                    listaCorreos.Add("ccrispin@bsginstitute.com");

                    string mensaje = helpMensaje.mensaje_email_finanzas(Alumno, this.ListaDetalleCuotas.ToList(), this.CodigoMatricula + " " + _Banco, this.SimboloMoneda, CentroCostos);
                    mensaje += "<br><br>" + Firma;
                    //helpCorreo.envio_email("ccrispin@bsginstitute.com", "Peticion Bolivia [" + EstadoCrep + "] (" + this.CodigoMatricula + ")", "Código de Pago " + this.PEspecificoInformacion.Nombre.ToString(), mensaje, listaCorreos);
                    helpCorreo.envio_email("bamontoya@bsginstitute.com", "Peticion Bolivia [" + EstadoCrep + "] (" + this.CodigoMatricula + ")", "Código de Pago " + this.PEspecificoInformacion.Nombre.ToString(), mensaje, listaCorreos);

                }
                else if (Alumno.IdCodigoPais.Value == 52)
                {
                    _Banco = "Banco Mexico";
                    if (this.NombrePlural == "Dolares")
                    {
                        this.SimboloMoneda = "US$";
                    }
                    else
                    {
                        this.SimboloMoneda = "MXN $";
                    }

                    List<string> listaCorreos = new List<string>();
                    //listaCorreos.Add("yhuillcapaco@bsginstitute.com");
                    listaCorreos.Add("bamontoya@bsginstitute.com");
                    //listaCorreos.Add("rchauca@bsginstitute.com");
                    listaCorreos.Add("mzegarraj@bsginstitute.com");
                    listaCorreos.Add("ccrispin@bsginstitute.com");

                    string mensaje = helpMensaje.mensaje_email_finanzas(Alumno, this.ListaDetalleCuotas.ToList(), this.CodigoMatricula + " " + _Banco, this.SimboloMoneda, CentroCostos);
                    mensaje += "<br><br>" + Firma;
                    //helpCorreo.envio_email("ccrispin@bsginstitute.com", "Peticion Bolivia [" + EstadoCrep + "] (" + this.CodigoMatricula + ")", "Código de Pago " + this.PEspecificoInformacion.Nombre.ToString(), mensaje, listaCorreos);
                    helpCorreo.envio_email("bamontoya@bsginstitute.com", "Peticion Mexico [" + EstadoCrep + "] (" + this.CodigoMatricula + ")", "Código de Pago " + this.PEspecificoInformacion.Nombre.ToString(), mensaje, listaCorreos);

                }
                else if (Alumno.IdCodigoPais.Value == 57)
                {
                    string _codigosistema = string.Empty, _codigousuario = string.Empty;
                    _Banco = "Bancolombia - Colombia";
                    this.SimboloMoneda = "COL $";
                    _codigosistema = Alumno.Id.ToString() + this.PEspecificoInformacion.CodigoBanco.ToUpper();
                    _codigousuario = _codigosistema;

                    this.CodigoMatricula = _codigousuario;

                    this.CodigoBancario = "56470";//Nro Conv. Bancolombia: 56470 (lo que identifica nuestra cuenta)

                    List<string> listaCorreos = new List<string>();
                    //listaCorreos.Add("yhuillcapaco@bsginstitute.com");
                    listaCorreos.Add("bamontoya@bsginstitute.com");
                    //listaCorreos.Add("rchauca@bsginstitute.com");
                    listaCorreos.Add("mzegarraj@bsginstitute.com");
                    listaCorreos.Add("ccrispin@bsginstitute.com");

                    string mensaje = helpMensaje.mensaje_email_finanzas(Alumno, this.ListaDetalleCuotas.ToList(), _codigousuario.ToUpper() + " " + _Banco, this.SimboloMoneda, CentroCostos);
                    mensaje += "<br><br>" + Firma;
                    //helpCorreo.envio_email("ccrispin@bsginstitute.com", "Peticion Bancolombia", "Código de Pago " + this.PEspecificoInformacion.Nombre.ToString(), mensaje, listaCorreos);
                    helpCorreo.envio_email("bamontoya@bsginstitute.com", "Peticion Bancolombia", "Código de Pago " + this.PEspecificoInformacion.Nombre.ToString(), mensaje, listaCorreos);

                }
				else
				{
					string _codigosistema = string.Empty, _codigousuario = string.Empty;
					_Banco = "Internacional";
					this.SimboloMoneda = "US$";
					_codigosistema = Alumno.Id.ToString() + this.PEspecificoInformacion.CodigoBanco.ToUpper();
					_codigousuario = _codigosistema;

					this.CodigoMatricula = _codigousuario;

					List<string> listaCorreos = new List<string>();
					//listaCorreos.Add("yhuillcapaco@bsginstitute.com");
					listaCorreos.Add("bamontoya@bsginstitute.com");
                    //listaCorreos.Add("rchauca@bsginstitute.com");
                    listaCorreos.Add("mzegarraj@bsginstitute.com");
                    listaCorreos.Add("ccrispin@bsginstitute.com");

					string mensaje = helpMensaje.mensaje_email_finanzas(Alumno, this.ListaDetalleCuotas.ToList(), _codigousuario.ToUpper() + " " + _Banco, this.SimboloMoneda, CentroCostos);
					mensaje += "<br><br>" + Firma;
					//helpCorreo.envio_email("ccrispin@bsginstitute.com", "Internacional", "Código de Pago " + this.PEspecificoInformacion.Nombre.ToString(), mensaje, listaCorreos);
					helpCorreo.envio_email("bamontoya@bsginstitute.com", "Peticion Bancolombia", "Código de Pago " + this.PEspecificoInformacion.Nombre.ToString(), mensaje, listaCorreos);
				}
			}
        }
        private string NormalizarCadena(string input)
        {
            Regex replace_a_Accents = new Regex("[á|à|ä|â]", RegexOptions.Compiled);
            Regex replace_e_Accents = new Regex("[é|è|ë|ê]", RegexOptions.Compiled);
            Regex replace_i_Accents = new Regex("[í|ì|ï|î]", RegexOptions.Compiled);
            Regex replace_o_Accents = new Regex("[ó|ò|ö|ô]", RegexOptions.Compiled);
            Regex replace_u_Accents = new Regex("[ú|ù|ü|û]", RegexOptions.Compiled);
            Regex replace_n_Accents = new Regex("[ñ]", RegexOptions.Compiled);

            input = replace_a_Accents.Replace(input, "a");
            input = replace_e_Accents.Replace(input, "e");
            input = replace_i_Accents.Replace(input, "i");
            input = replace_o_Accents.Replace(input, "o");
            input = replace_u_Accents.Replace(input, "u");
            input = replace_n_Accents.Replace(input, "n");
            input = input.Replace("ñ", "n");
            return input;
        }
        public bool EnviarCorreosFinanzasVentas()
        {
            try
            {
                HelperCorreo helpCorreo = new HelperCorreo();
                HelperMensajes helpMensaje = new HelperMensajes();
                PgeneralRepositorio _repPgeneral = new PgeneralRepositorio();
                
                string Mensaje = string.Empty, Firma = string.Empty;

                var Tpgeneral = _repPgeneral.GetBy(w => w.Id == PEspecificoInformacion.IdProgramaGeneral).FirstOrDefault();
                Firma = "<img src='https://repositorioweb.blob.core.windows.net/firmas/" + this.UsuarioCreacion + ".png' />";

                this.GenerarArchivoCrepCronograma("Nuevo", Firma);

                string _Ciudad = this.PEspecificoInformacion.Ciudad.ToUpper();
                string CodigoAlumno = string.Empty;

                if (Alumno.IdCodigoPais.Value != 57)
                {
                    CodigoAlumno = this.CodigoMatricula.ToUpper() + " - " + this.CodigoBancario;
                    if (Alumno.IdCodigoPais.Value == 51)
                    {
                        Mensaje = helpMensaje.mensaje_email_proceso_pago_peru(this.Username, this.Password, "https://bsginstitute.com/Cuenta", Alumno, this.ListaDetalleCuotas.ToList(), Tpgeneral.Nombre, CodigoAlumno, "PE", this.SimboloMoneda);
                    }
                    else if (Alumno.IdCodigoPais.Value == 591)
                    {
                        CodigoAlumno = this.CodigoMatricula.ToUpper() + " - " + this.CodigoBancario;
                        Mensaje = helpMensaje.mensaje_email_proceso_pago_peru(this.Username, this.Password, "https://bsginstitute.com/Cuenta", Alumno, this.ListaDetalleCuotas.ToList(), Tpgeneral.Nombre, CodigoAlumno, "INT", this.SimboloMoneda);

                    }
                    else if (Alumno.IdCodigoPais.Value == 52)
                    {
                        CodigoAlumno = this.CodigoMatricula.ToUpper() + " - " + this.CodigoBancario;
                        Mensaje = helpMensaje.mensaje_email_proceso_pago_peru(this.Username, this.Password, "https://bsginstitute.com/Cuenta", Alumno, this.ListaDetalleCuotas.ToList(), Tpgeneral.Nombre, CodigoAlumno, "INT", this.SimboloMoneda);

                    }
                    else
                    {
						CodigoAlumno = this.CodigoMatricula.ToUpper();
						Mensaje = helpMensaje.mensaje_email_proceso_pago_peru(this.Username, this.Password, "https://bsginstitute.com/Cuenta", Alumno, this.ListaDetalleCuotas.ToList(), Tpgeneral.Nombre, CodigoAlumno, "INT", "US$");
                    }
                }
                else
                {
                    switch (Alumno.IdCodigoPais.Value)
                    {
                        case 57://Colombia
                            CodigoAlumno = this.CodigoMatricula.ToUpper();
                            CodigoAlumno = CodigoAlumno.Replace("A", "");
                            Mensaje = helpMensaje.mensaje_email_proceso_pago_peru(this.Username, this.Password, "https://bsginstitute.com/Cuenta", Alumno, this.ListaDetalleCuotas.ToList(), Tpgeneral.Nombre, CodigoAlumno, "CO", this.SimboloMoneda);
                            break;
                        case 591://Bolivia
                            CodigoAlumno = this.CodigoMatricula.ToUpper();
                            CodigoAlumno = CodigoAlumno.Replace("A", "");
                            Mensaje = helpMensaje.mensaje_email_proceso_pago_peru(this.Username, this.Password, "https://bsginstitute.com/Cuenta", Alumno, this.ListaDetalleCuotas.ToList(), Tpgeneral.Nombre, CodigoAlumno, "BO", this.SimboloMoneda);
                            break;
                        case 52://Mexico
                            CodigoAlumno = this.CodigoMatricula.ToUpper();
                            CodigoAlumno = CodigoAlumno.Replace("A", "");
                            Mensaje = helpMensaje.mensaje_email_proceso_pago_peru(this.Username, this.Password, "https://bsginstitute.com/Cuenta", Alumno, this.ListaDetalleCuotas.ToList(), Tpgeneral.Nombre, CodigoAlumno, "MEX", this.SimboloMoneda);
                            break;
                        default://Otros
                            CodigoAlumno = this.CodigoMatricula.ToUpper() + " - " + this.CodigoBancario;
                            Mensaje = helpMensaje.mensaje_email_proceso_pago_peru(this.Username, this.Password, "https://bsginstitute.com/Cuenta", Alumno, this.ListaDetalleCuotas.ToList(), Tpgeneral.Nombre, CodigoAlumno, "INT", "US$");
                            break;
                    }
                }
                Mensaje += "<br><br>" + Firma;

               // DESCOMENTAR PARA PRODUCCION
                var Correos = _repPgeneral.ObtenerCorreosIdPersonalAprobacion(this.IdPersonal);

                ////FALTA FUNCION OBTENER CORREOS POR ID /USERNAME
                var listaCorreos = (from x in Correos
                                    select x.Email).ToList();

				MontoPagoCronogramaRepositorio _repMontoPagoCronograma = new MontoPagoCronogramaRepositorio();
				OportunidadRepositorio _repOportunidad = new OportunidadRepositorio();
				CentroCostoRepositorio _repCentroCosto = new CentroCostoRepositorio();
				var idOportunidad = _repMontoPagoCronograma.FirstById(this.ListaDetalleCuotas[0].IdMontoPagoCronograma).IdOportunidad;

				var idCentroCosto = _repOportunidad.FirstById(idOportunidad).IdCentroCosto;

				var centroCosto = _repCentroCosto.FirstById(idCentroCosto.Value);
				//var listaCorreos = new List<string>();

                //helpCorreo.envio_emailSinCopia("ccrispin@bsginstitute.com", Alumno.Nombre1 + " " + Alumno.Nombre2 + " " + Alumno.ApellidoPaterno + " " + Alumno.ApellidoMaterno, "BSG INSTITUTE - Código de Pago " + Tpgeneral.Nombre, Mensaje);
                //helpCorreo.envio_emailSinCopia(Alumno.Email1, Alumno.Nombre1 + " " + Alumno.Nombre2 + " " + Alumno.ApellidoPaterno + " " + Alumno.ApellidoMaterno, "BSG INSTITUTE - Código de Pago " + Tpgeneral.Nombre, Mensaje);
                helpCorreo.envio_emailSinCopia(Alumno.Email1,"Matriculas BSG INSTITUTE", "BSG INSTITUTE - Código de Pago " + Tpgeneral.Nombre, Mensaje);
                string mensajeArea = helpMensaje.mensaje_email_finanzas(Alumno, this.ListaDetalleCuotas.ToList(), this.CodigoMatricula.ToUpper(), this.SimboloMoneda, centroCosto);
                //helpCorreo.envio_email("matriculas@bsginstitute.com", Alumno.Nombre1 + " " + Alumno.Nombre2 + " " + Alumno.ApellidoPaterno + " " + Alumno.ApellidoMaterno, "BSG INSTITUTE - Código de Pago " + Tpgeneral.Nombre, mensajeArea, listaCorreos);
                helpCorreo.envio_email("matriculas@bsginstitute.com", "Matriculas BSG INSTITUTE", "BSG INSTITUTE - Código de Pago " + Tpgeneral.Nombre, mensajeArea, listaCorreos);
                //fin nuevo

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }



        }
        private string CrearClave(int longitud)
        {
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < longitud--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }

            return res.ToString();
        }



        //public void eliminarMontoPagoCronograma(MontoPagoCronogramaBO registroCronogramaBD)
        //{
        //    var regMPC = _repMPC.ObtenerPorId(registroCronogramaBD.Id);
        //    regMPC.EsAprobado = false;
        //    regMPC.MatriculaEnProceso = 0;
        //    regMPC.Estado = false;
        //    regMPC.FechaModificacion = DateTime.Now;

        //    _repMPC.Actualizar(regMPC);

        //    if (regMPC.EsAprobado)
        //    {
        //        eliminarCronogramaVentasByCordinador(regMPC.Id);
        //    }

        //}

        //public void generarCronogramaVentasByCordinador(int IdCronograma, MontoPagoCronogramaBO Cronograma)
        //{
        //    MatriculaCronogramaOportunidad _PrePrograma = new MatriculaCronogramaOportunidad();
        //    string _query = string.Empty;
        //    bool Buscar = false;

        //    _query = "select MPC.Id AS IdMontoPagoCronogramas,MPC.IdMontoPago AS IdMontoPago,PE.Id AS IdPEspecifico,OO.IdAlumno,OO.IdPersonal_Asignado,PE.CodigoBanco,MPC.UsuarioModificacion AS UsuarioAprobacion " +
        //             "from com.T_MontoPagoCronograma AS MPC inner join com.T_Oportunidad AS OO ON MPC.IdOportunidad=OO.Id inner join pla.T_PEspecifico AS PE ON OO.IdCentroCosto=PE.IdCentroCosto " +
        //             "Where MPC.Estado=1 and MPC.IdOportunidad=" + IdCronograma.ToString() + " order by MPC.Id desc";

        //    var RegistroBD = _dapperRepository.FirstOrDefault(_query);
        //    if (!string.IsNullOrEmpty(RegistroBD) && !RegistroBD.Contains("{}"))
        //    {
        //        var _RegistroBD = JsonConvert.DeserializeObject<MatriculaCronogramaOportunidad>(RegistroBD);

        //        if (_RegistroBD != null)
        //        {
        //            Buscar = true;
        //            _PrePrograma = _RegistroBD;
        //        }

        //    }

        //    if (Buscar)
        //    {
        //        _query = "select * from fin.T_MatriculaCabecera where IdAlumno=" + _PrePrograma.IdAlumno.ToString() + " and IdPEspecifico=" + _PrePrograma.IdPEspecifico + " and Estado=1";
        //        var BuscarBD = _dapperRepository.FirstOrDefault(_query);

        //        if (BuscarBD.ToString() == "null")
        //        {
        //            TMatriculaCabecera mc = new TMatriculaCabecera();
        //            mc.CodigoMatricula = _PrePrograma.IdAlumno.ToString() + _PrePrograma.CodigoBanco;
        //            mc.IdAlumno = _PrePrograma.IdAlumno;
        //            mc.IdPespecifico = _PrePrograma.IdPEspecifico;
        //            mc.UsuarioCreacion = "SYSTEMV4";
        //            mc.UsuarioModificacion = "SYSTEMV4";
        //            mc.FechaCreacion = DateTime.Now;
        //            mc.FechaModificacion = DateTime.Now;
        //            mc.FechaMatricula = DateTime.Now;
        //            mc.Estado = true;
        //            mc.EstadoMatricula = "pormatricular";
        //            mc.FechaMatricula = DateTime.Now;
        //            mc.IdAsesor = _PrePrograma.IdPersonal_Asignado;
        //            mc.IdCoordinador = 0;//validar
        //            mc.IdEstadoMatricula = 1;
        //            mc.UsuarioCoordinadorAcademico = "";
        //            mc.ObservacionGeneralOperaciones = "";
        //            mc.IdCronograma = Cronograma.Id;

        //            //mc["UsuarioCreacion"] = "SYSTEMV4";
        //            //mc["UsuarioModificacion"] = "SYSTEMV4";
        //            //mc["FechaCreacion"] = DateTime.Now;
        //            //mc["FechaMatricula"] = DateTime.Now;
        //            //mc["Estado"] = true;

        //            _repMC.Insertar(mc);

        //            //Insertamos el detalle//por cada proespid
        //            _query = "SELECT Id FROM pla.T_CursoPEspecifico where IdPEspecifico=" + _PrePrograma.IdPEspecifico;
        //            var listaCursosBD = _dapperRepository.QueryDapper(_query);

        //            if (!string.IsNullOrEmpty(listaCursosBD) && !listaCursosBD.Contains("[]"))
        //            {
        //                var _listaCursosBD = JsonConvert.DeserializeObject<List<Dictionary<string, int>>>(listaCursosBD);

        //                var _listaIds = _listaCursosBD.SelectMany(x => x.Values).ToList();

        //                List<MatriculaDetalleBO> listaMatriculaDetalle = new List<MatriculaDetalleBO>();

        //                foreach (var item in _listaIds)
        //                {
        //                    TMatriculaDetalle _registroMD = new TMatriculaDetalle();
        //                    _registroMD.IdCursoPespecifico = item;
        //                    _registroMD.IdMatriculaCabecera = mc.Id;
        //                    _registroMD.FechaCreacion = DateTime.Now;
        //                    _registroMD.FechaModificacion = DateTime.Now;
        //                    _registroMD.UsuarioCreacion = "SYSTEMV4";
        //                    _registroMD.UsuarioModificacion = "SYSTEMV4";
        //                    _registroMD.Estado = true;

        //                    contexto.TMatriculaDetalle.Add(_registroMD);

        //                }
        //                contexto.SaveChanges();
        //            }

        //            //Insertar Cronograma

        //            int contadorCoutas = 1;
        //            DateTime? fechaIniPago = DateTime.Now.Date;
        //            decimal CuotaInicial = Convert.ToDecimal(Cronograma.PrecioDescuento);

        //            contadorCoutas = Cronograma._ListaCronogramaDetalle.Count;

        //            var matricula = Cronograma._ListaCronogramaDetalle.FirstOrDefault(x => x.Matricula == true && x.NumeroCuota == 1);
        //            if (matricula != null)
        //            {
        //                fechaIniPago = matricula.FechaPago;
        //                CuotaInicial = Convert.ToDecimal(matricula.MontoCuotaDescuento);
        //            }

        //            double tipoCambioCol = 3000;

        //            _query = "SELECT TOP 1 PesosDolares FROM fin.T_TipoCambioCol ORDER BY FechaCreacion DESC";
        //            var tipoCambioBD = _dapperRepository.FirstOrDefault(_query);
        //            if (tipoCambioBD != null)
        //            {
        //                var _tipoCambioBD = JsonConvert.DeserializeObject<Dictionary<string, double>>(tipoCambioBD);
        //                tipoCambioCol = _tipoCambioBD.Select(x => x.Value).FirstOrDefault();
        //            }

        //            TCronogramaPago cp = new TCronogramaPago();
        //            cp.IdMatriculaCabecera = mc.Id;
        //            cp.IdAlumno = mc.IdAlumno;
        //            cp.IdPespecifico = mc.IdPespecifico;
        //            cp.Periodo = DateTime.Now.Year.ToString();
        //            cp.Moneda = (Cronograma.IdMoneda == 19 ? "soles" : "dolares");
        //            cp.AcuerdoPago = "Crédito";
        //            cp.TipoCambio = Convert.ToDouble("0");
        //            cp.TotalPagar = Cronograma.PrecioDescuento;
        //            if (Cronograma.IdMoneda == 10)
        //            {
        //                cp.TotalPagar = Math.Round(Cronograma.PrecioDescuento / tipoCambioCol, 2, MidpointRounding.AwayFromZero);
        //            }
        //            cp.NroCuotas = contadorCoutas;
        //            cp.FechaIniPago = fechaIniPago;
        //            cp.ConCuotaInicial = true;
        //            cp.CuotaInicial = CuotaInicial;
        //            cp.CadaNdias = false;
        //            cp.Ndias = 0;

        //            string web_moneda = string.Empty;
        //            double web_tipocambio = cp.TipoCambio.Value;
        //            double web_totalPagarConv = cp.TotalPagar.Value;

        //            if (Cronograma.IdMoneda == 19)
        //            {
        //                web_moneda = "0";//Soles
        //            }
        //            else if (Cronograma.IdMoneda == 20)
        //            {
        //                web_moneda = "1";//Dolares
        //            }
        //            else if (Cronograma.IdMoneda == 10)
        //            {
        //                web_moneda = "2";//Colombianos
        //                web_tipocambio = tipoCambioCol;
        //                web_totalPagarConv = Cronograma.PrecioDescuento / web_tipocambio;
        //            }
        //            else
        //            {
        //                web_moneda = Cronograma.IdMoneda.ToString();
        //            }

        //            cp.WebMoneda = web_moneda;
        //            cp.WebTipoCambio = web_tipocambio;
        //            cp.WebTotalPagar = Cronograma.PrecioDescuento;//En la moneda original
        //            cp.WebTotalPagarConv = web_totalPagarConv;//Total convertido con todos los decimales (si es colomabianos)



        //            cp.UsuarioCreacion = "SYSTEMV4";
        //            cp.UsuarioModificacion = "SYSTEMV4";
        //            cp.FechaCreacion = DateTime.Now;
        //            cp.FechaModificacion = DateTime.Now;
        //            cp.Estado = true;


        //            //registrar
        //            contexto.TCronogramaPago.Add(cp);
        //            contexto.SaveChanges();

        //            decimal Saldo = Convert.ToDecimal(cp.TotalPagar);

        //            foreach (var item in Cronograma._ListaCronogramaDetalle)
        //            {
        //                TCronogramaPagoDetalleOriginal cpdf = new TCronogramaPagoDetalleOriginal();
        //                decimal Total = Saldo;

        //                double montoCuotaFinal = item.MontoCuotaDescuento;
        //                Saldo = Total - Convert.ToDecimal(montoCuotaFinal);

        //                cpdf.IdMatriculaCabecera = mc.Id;
        //                cpdf.NroCuota = item.NumeroCuota;
        //                cpdf.FechaVencimiento = item.FechaPago;
        //                cpdf.TotalPagar = Total;
        //                cpdf.Cuota = Convert.ToDecimal(montoCuotaFinal);
        //                cpdf.Saldo = Saldo;
        //                cpdf.Cancelado = false;
        //                cpdf.TipoCuota = (item.Matricula == true && item.NumeroCuota == 1 ? "MATRICULA" : "CUOTA");
        //                cpdf.Moneda = (Cronograma.IdMoneda == 19 ? "soles" : "dolares"); //revisar
        //                cpdf.FechaCreacion = DateTime.Now;
        //                cpdf.FechaModificacion = DateTime.Now;
        //                cpdf.UsuarioCreacion = "SYSTEMV4";
        //                cpdf.UsuarioModificacion = "SYSTEMV4";
        //                cpdf.Estado = true;


        //                contexto.TCronogramaPagoDetalleOriginal.Add(cpdf);
        //            }
        //            contexto.SaveChanges();
        //            //Add Documentos

        //            TControlDocAlumno cda = new TControlDocAlumno();
        //            cda.IdMatriculaCabecera = mc.Id; 
        //            cda.IdCriterioCalificacion = 0;
        //            cda.QuienEntrego = "";
        //            cda.FechaEntregaDocumento = null;
        //            cda.Observaciones = "";
        //            cda.ComisionableEditable = "Ninguno";
        //            cda.MontoComisionable = 0;
        //            cda.ObservacionesComisionable = "";
        //            cda.PagadoComisionable = 0;
        //            cda.UsuarioCreacion = "SYSTEMV4";
        //            cda.UsuarioModificacion = "SYSTEMV4";
        //            cda.FechaCreacion = DateTime.Now;
        //            cda.FechaModificacion = DateTime.Now;
        //            cda.Estado = true;

        //            contexto.TControlDocAlumno.Add(cda);
        //            contexto.SaveChanges();

        //            int modalidad = 0;

        //            _query = "SELECT TipoId FROM pla.T_PEspecifico WHERE Id=" + mc.IdPespecifico.ToString();
        //            var modalidadBD = _dapperRepository.FirstOrDefault(_query);
        //            if (modalidadBD != null)
        //            {
        //                var modalidadDB = JsonConvert.DeserializeObject<Dictionary<string, int>>(modalidadBD);
        //                modalidad = modalidadDB.Select(x => x.Value).FirstOrDefault();
        //            }

        //            if (modalidad >= 0 && modalidad <= 2)
        //            {
        //                _query = "SELECT Id FROM fin.T_CriterioDoc WHERE ModalidadPresencial=1";
        //                var criterioDocsBD = _dapperRepository.QueryDapper(_query);
        //                if (tipoCambioBD != null)
        //                {
        //                    var criterioDocBD = JsonConvert.DeserializeObject<List<Dictionary<string, int>>>(criterioDocsBD);

        //                    var _criterioDocBD = criterioDocBD.SelectMany(x => x.Values).ToList();

        //                    foreach (var item in _criterioDocBD)
        //                    {
        //                        TControlDoc docs = new TControlDoc();
        //                        docs.IdMatriculaCabecera = mc.Id;
        //                        docs.IdCriterioDoc = item;
        //                        docs.Estado = false;
        //                        docs.UsuarioCreacion = "SYSTEMV4";
        //                        docs.UsuarioModificacion = "SYSTEMV4";
        //                        docs.FechaCreacion = DateTime.Now;
        //                        docs.FechaModificacion = DateTime.Now;
        //                        docs.Estado = true;


        //                        contexto.TControlDoc.Add(docs);
        //                    }
        //                    contexto.SaveChanges();

        //                }
        //            }

        //        }
        //    }


        //}

        //public void eliminarCronogramaVentasByCordinador(int IdCronograma)
        //{
        //    string _query = string.Empty;

        //    _query = "select Id,IdAlumno,IdPEspecifico,EstadoMatricula,Estado,UsuarioCreacion,UsuarioModificacion,FechaCreacion,FechaModificacion from fin.T_MatriculaCabecera where IdCronograma= @idCronograma";
        //    var BuscarBD = _dapperRepository.FirstOrDefault(_query, new { idCronograma = IdCronograma});

        //    if (BuscarBD != null)
        //    {
        //        var _BuscarBD = JsonConvert.DeserializeObject<MatriculaCabeceraBO>(BuscarBD);

        //        var regMC = _repMC.ObtenerPorId(_BuscarBD.Id);
        //        regMC.Estado = false;
        //        regMC.EstadoMatricula = "eliminado";
        //        regMC.FechaModificacion = DateTime.Now;

        //        _repMC.Actualizar(regMC);

        //    }
        //}

        //public List<DetalleMontoPagoDTO> detallemontopago(int Idmonto)
        //{
        //    try
        //    {
        //        string _querydetalle = "select Titulo from pla.T_SuscripcionProgramaGeneral  as PGbene inner join" +
        //                            " pla.T_MontoPagoSuscripcion as MonPag on  PGbene.Id = MonPag.IdSuscripcionProgramaGeneral" +
        //                            " where MonPag.IdMontoPago = @idMontoPago and  PGbene.Estado=1 and   MonPag.Estado=1";
        //        var querydetalle = _dapperRepository.QueryDapper(_querydetalle, new { idMontoPago = Idmonto });

        //        List<DetalleMontoPagoDTO> DetalleMontoPago = JsonConvert.DeserializeObject<List<DetalleMontoPagoDTO>>(querydetalle);

        //        return DetalleMontoPago;
        //    }
        //    catch (Exception ex)
        //    {
        //        return null;
        //    }

        //}



        //public class FiltroProcedimientoOportunidad
        //{
        //    public int IdOportunidad { get; set; }
        //}


        //public void CargarHistorial(int IdAsesor)
        //{
        //    if (IdAsesor != 0)
        //    {
        //        var RegistrosDB = _dapperRepository.QuerySPDapper("com.ObtenerHistorialCronogramaPagoPorPeriodoAsesor", new { IdAsesor });
        //        _ListaMontoPagoCronogramaHistorial = JsonConvert.DeserializeObject<List<MontoPagoCronogramasHistorialBO>>(RegistrosDB);
        //    }
        //    else
        //    {
        //        _ListaMontoPagoCronogramaHistorial = null;
        //    }
        //}

        //public void archivo_crepCronograma(AlumnoBO contacto, PespecificoBO especifico, MontoPagoCronogramaBO objeto, string EstadoCrep, string Firma)
        //{
        //    string _query = string.Empty;
        //    string Monto = string.Empty, _Ciudad = string.Empty, _Banco = string.Empty, CodAutoriza = string.Empty, rpta = string.Empty, CodigoBancario = string.Empty, SimboloMoneda = string.Empty, CentroCostos = string.Empty;
        //    HelperCorreo helpCorreo = new HelperCorreo();
        //    HelperMensajes helpMensaje = new HelperMensajes();

        //    //var cc = _tcrm_CentroCostoService.GetAll().Where(w => w.id == especifico.IdCentroCosto).FirstOrDefault();

        //    var ccc = contexto.TCentroCosto.Where(x => x.Id == especifico.IdCentroCosto).FirstOrDefault();

        //    if (ccc != null)
        //    {
        //        CentroCostos = ccc.Codigo;
        //    }
        //    else
        //    {
        //        CentroCostos = "";
        //    }

        //    if (objeto._ListaCronogramaDetalle != null)
        //    {
        //        _Ciudad = especifico.Ciudad.ToUpper();
        //        if ((_Ciudad == "AREQUIPA" || _Ciudad == "LIMA" || _Ciudad.Contains("ONLINE")) && contacto.IdCodigoPais.Value != 57)
        //        {
        //            _Banco = "Banco BCP - PERÚ";
        //            string _nombrearchivo = string.Empty;
        //            string _monadaNombre = string.Empty;
        //            _nombrearchivo = "CREP_X";

        //            StringBuilder linea = new StringBuilder();
        //            string _tiporegistro = "CC";
        //            string _codigosucursal = "215";

        //            string _nrocuenta = "Credipago a BSG Institute a la cuenta Lima-Soles"; // 215-1863341-0-42 - CCI: 002-215-001863341042-20 - Cuenta Corriente en Soles programas Lima, Online y Aonline
        //            string _nrocuentacrep = "1863341"; //seccion del nro de cuenta

        //            if (objeto.NombrePlural == "Dolares")
        //            {
        //                _monadaNombre = "DOLARES";
        //                SimboloMoneda = "US$";
        //                if (_Ciudad.Contains("AREQUIPA"))
        //                {
        //                    _nrocuenta = "Credipago a BSG Institute a la cuenta Arequipa-Dolares"; //215-2307651-1-32 - CCI: 002-215-002307651132-20 - Cuenta corriente en Dólares programas Arequipa
        //                    _nrocuentacrep = "2307651";
        //                }
        //                else//Dolares otros
        //                {
        //                    _nrocuenta = "Credipago a BSG Institute a la cuenta Lima-Dolares"; //215-1870934-1-48 - CCI: 002-215-001870934148-23 - Cuenta corriente en Dólares programas Lima, Online y Aonline
        //                    _nrocuentacrep = "1870934";
        //                }
        //            }
        //            else
        //            {
        //                _monadaNombre = "SOLES";
        //                SimboloMoneda = "S/.";
        //                if (_Ciudad.Contains("AREQUIPA"))
        //                {
        //                    _nrocuenta = "Credipago a BSG Institute a la cuenta Arequipa-Soles"; //215-1863344-0-72 - CCI: 002-215-001863344072-24 - Cuenta Corriente en Soles programas Arequipa
        //                    _nrocuentacrep = "1863344";
        //                }
        //                else//Soles otros
        //                {
        //                    _nrocuenta = "Credipago a BSG Institute a la cuenta Lima-Soles"; //215-1863341-0-42 - CCI: 002-215-001863341042-20 - Cuenta corriente en Dólares programas Lima, Online y Aonline
        //                    _nrocuentacrep = "1863341";
        //                }
        //            }
        //            string _codigomoneda = (_monadaNombre == "SOLES" ? "0" : "1");

        //            string _tipovalidacion = "C";

        //            CodigoBancario = _nrocuenta;

        //            string _nombreempresa = ("BSGINSTITUTE" + _Ciudad + _monadaNombre).PadRight(40);
        //            string _fecha = String.Format("{0:yyyyMMdd}", DateTime.Now);
        //            string _temp;

        //            _temp = objeto._ListaCronogramaDetalle.Count.ToString();

        //            string _totalregistros = _temp.ToString().PadLeft(9, '0');
        //            string _tiporegistrod = "DD";
        //            string _libred = "".PadLeft(47);

        //            string _codigousuario = string.Empty, _nombreusuario, _fechaemision, _montomora, _montominimo, _tiporegistro2, nombres_b, apellidos_b;
        //            string _codigoespecial, _montocuota, _fechavencimiento, _montototal = string.Empty;
        //            string _tipoarchivo = string.Empty;

        //            if (EstadoCrep == "Nuevo")
        //            {
        //                _tipoarchivo = "A";
        //            }
        //            else
        //            {
        //                _tipoarchivo = "E";
        //            }

        //            string _libre = "000".PadLeft(113);
        //            DateTime fechaEmision = new DateTime();

        //            string codigobanco = string.Empty;
        //            int numero = 0;
        //            if (Int32.TryParse(especifico.CodigoBanco, out numero))
        //            {
        //                numero = (numero / 100);
        //                codigobanco = numero.ToString();
        //                _codigousuario = contacto.Id.ToString() + codigobanco;
        //                _codigousuario = _codigousuario.PadRight(14, '0');
        //            }
        //            else
        //            {
        //                codigobanco = especifico.CodigoBanco.ToUpper();
        //                _codigousuario = contacto.Id.ToString() + codigobanco;
        //            }
        //            CodAutoriza = _codigousuario;

        //            _codigousuario = _codigousuario.ToString().PadLeft(14);
        //            objeto.CodigoMatricula = _codigousuario;


        //            string _auxNombres = string.Empty, _auxApellidos = string.Empty;
        //            _auxNombres = contacto.Nombre1.ToLower() + " " + contacto.Nombre2.ToLower();
        //            _auxApellidos = contacto.ApellidoPaterno.ToLower() + " " + contacto.ApellidoMaterno.ToLower();

        //            nombres_b = NormalizarCadena(_auxNombres.ToUpper());
        //            apellidos_b = NormalizarCadena(_auxApellidos.ToUpper());

        //            _nombreusuario = apellidos_b + " " + nombres_b;
        //            _nombreusuario = _nombreusuario.ToUpper();
        //            _nombreusuario = _nombreusuario.PadRight(40);

        //            fechaEmision = DateTime.Now.AddDays(1);
        //            _fechaemision = String.Format("{0:yyyyMMdd}", fechaEmision);

        //            _montomora = "0".PadLeft(15, '0');
        //            _montominimo = "0".PadLeft(9, '0');

        //            _montototal = objeto.PrecioDescuento.ToString().PadLeft(15, '0');

        //            string nombreArchivoCrep = string.Empty;
        //            if (string.IsNullOrEmpty(contacto.Dni))
        //            {
        //                nombreArchivoCrep = _nombrearchivo + "00000000";
        //            }
        //            else
        //            {
        //                nombreArchivoCrep = _nombrearchivo + contacto.Dni;
        //            }

        //            int contadorpagos = 0;
        //            _montototal = objeto.PrecioDescuento.ToString("0.00");
        //            _montototal = _montototal.Replace(".", "").Replace(",", "");
        //            byte[] registroCrepByte;

        //            using (MemoryStream ms = new MemoryStream())
        //            {
        //                using (StreamWriter myStreamWriter = new StreamWriter(ms))
        //                {
        //                    linea.Append(_tiporegistro + _codigosucursal + _codigomoneda + _nrocuentacrep + _tipovalidacion + _nombreempresa + _fecha + _totalregistros + _montototal + _tipoarchivo + _libre);
        //                    rpta = linea.ToString();
        //                    myStreamWriter.WriteLine(linea.ToString());
        //                    linea.Remove(0, linea.Length);

        //                    foreach (var pagos in objeto._ListaCronogramaDetalle)
        //                    {
        //                        contadorpagos++;
        //                        if (contadorpagos < 10)
        //                            _codigoespecial = "10" + contadorpagos.ToString() + "01XXXXXX";
        //                        else
        //                            _codigoespecial = "1" + contadorpagos.ToString() + "01XXXXXX";

        //                        _codigoespecial = _codigoespecial.PadRight(30);
        //                        _montocuota = String.Format("{0:0.00}", (Convert.ToDouble(pagos.MontoCuotaDescuento.ToString()) + Convert.ToDouble("0"))).Replace(".", "").Replace(",", "").PadLeft(15, '0');
        //                        _fechavencimiento = String.Format("{0:yyyyMMdd}", Convert.ToDateTime(pagos.FechaPago.ToString()));
        //                        if (EstadoCrep == "Nuevo")
        //                        {
        //                            _tiporegistro2 = "A";
        //                        }
        //                        else
        //                        {
        //                            _tiporegistro2 = "E";
        //                        }

        //                        linea.Append(_tiporegistrod + _codigosucursal + _codigomoneda + _nrocuentacrep + _codigousuario + _nombreusuario + _codigoespecial + _fechaemision + _fechavencimiento + _montocuota + _montomora + _montominimo + _tiporegistro2 + _libred);
        //                        myStreamWriter.WriteLine(linea.ToString());
        //                        linea.Remove(0, linea.Length);
        //                    }
        //                    myStreamWriter.Close();//cerramos al terminar la escritura del archivo crep
        //                }
        //                registroCrepByte = ms.ToArray();
        //            }

        //            string mensaje = helpMensaje.mensaje_email_finanzas(contacto, objeto._ListaCronogramaDetalle.ToList(), _codigousuario + " - " + _nrocuenta, SimboloMoneda);
        //            mensaje += "<br><br>" + Firma;

        //            List<string> listaCorreos = new List<string>();
        //            listaCorreos.Add("pbeltran@bsginstitute.com");

        //            helpCorreo.envioEmailAdjuntoBlob("pbeltran@bsginstitute.com", "Archivo CREP " + EstadoCrep + " - Ventas", EstadoCrep + " Archivo CREP " + especifico.Nombre.ToString(), mensaje, nombreArchivoCrep + "-" + EstadoCrep + ".txt", registroCrepByte, listaCorreos);
        //            //HelperCorreo.envioEmailAdjuntoBlob("jrivera@bsgrupo.com", "Archivo CREP " + EstadoCrep + " - Ventas", EstadoCrep + " Archivo CREP " + especifico.nombre.ToString(), mensaje, nombreArchivoCrep + "-" + EstadoCrep + ".txt", registroCrepByte, listaCorreos);
        //        }
        //        else
        //        {
        //            string _codigosistema = string.Empty, _codigousuario = string.Empty;
        //            _Banco = "Bancolombia - Colombia";
        //            SimboloMoneda = "COL $";
        //            _codigosistema = contacto.Id.ToString() + especifico.CodigoBanco.ToUpper();
        //            _codigousuario = _codigosistema;

        //            objeto.CodigoMatricula = _codigousuario;

        //            CodigoBancario = "56470";//Nro Conv. Bancolombia: 56470 (lo que identifica nuestra cuenta)

        //            List<string> listaCorreos = new List<string>();
        //            listaCorreos.Add("pbeltran@bsginstitute.com");

        //            string mensaje = helpMensaje.mensaje_email_finanzas(contacto, objeto._ListaCronogramaDetalle.ToList(), _codigousuario.ToUpper() + " " + _Banco, SimboloMoneda);
        //            mensaje += "<br><br>" + Firma;
        //            helpCorreo.envio_email("pbeltran@bsginstitute.com", "Peticion Bancolombia", "Código de Pago " + especifico.Nombre.ToString(), mensaje, listaCorreos);
        //            //HelperCorreo.envio_email("jrivera@bsgrupo.com", "Peticion Bancolombia", "Código de Pago " + especifico.nombre.ToString(), mensaje, listaCorreos);
        //        }
        //    }
    }
}


//    public string CreatePassword(int length)
//    {
//        const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
//        StringBuilder res = new StringBuilder();
//        Random rnd = new Random();
//        while (0 < length--)
//        {
//            res.Append(valid[rnd.Next(valid.Length)]);
//        }
//        return res.ToString();
//    }

//    public string Encriptar(string us_clave)
//    {
//        return Encriptar(us_clave, "pass75dc@avz10", "s@lAvz", "MD5", 1, "@1B2c3D4e5F6g7H8", 128);
//    }

//    public string Hash(string password)
//    {
//        var bytes = new UTF8Encoding().GetBytes(password);
//        var hashBytes = System.Security.Cryptography.HashAlgorithm.Create().ComputeHash(bytes);
//        return Convert.ToBase64String(hashBytes);
//    }

//    public static string HashPassword(string password)
//    {
//        byte[] salt;
//        byte[] buffer2;
//        if (password == null)
//        {
//            throw new ArgumentNullException("password");
//        }
//        using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, 0x10, 0x3e8))
//        {
//            salt = bytes.Salt;
//            buffer2 = bytes.GetBytes(0x20);
//        }
//        byte[] dst = new byte[0x31];
//        Buffer.BlockCopy(salt, 0, dst, 1, 0x10);
//        Buffer.BlockCopy(buffer2, 0, dst, 0x11, 0x20);
//        return Convert.ToBase64String(dst);
//    }

//    public string Encriptar(string textoQueEncriptaremos, string passBase, string saltValue, string hashAlgorithm, int passwordIterations, string initVector, int keySize)
//    {
//        byte[] initVectorBytes = Encoding.ASCII.GetBytes(initVector);
//        byte[] saltValueBytes = Encoding.ASCII.GetBytes(saltValue);
//        byte[] plainTextBytes = Encoding.UTF8.GetBytes(textoQueEncriptaremos);
//        PasswordDeriveBytes password = new PasswordDeriveBytes(passBase,
//          saltValueBytes, hashAlgorithm, passwordIterations);
//        byte[] keyBytes = password.GetBytes(keySize / 8);
//        RijndaelManaged symmetricKey = new RijndaelManaged()
//        {
//            Mode = CipherMode.CBC
//        };
//        ICryptoTransform encryptor = symmetricKey.CreateEncryptor(keyBytes,
//          initVectorBytes);
//        MemoryStream memoryStream = new MemoryStream();
//        CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor,
//         CryptoStreamMode.Write);
//        cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
//        cryptoStream.FlushFinalBlock();
//        byte[] cipherTextBytes = memoryStream.ToArray();
//        memoryStream.Close();
//        cryptoStream.Close();
//        string cipherText = Convert.ToBase64String(cipherTextBytes);
//        return cipherText;
//    }
//}

//public class MatriculaCronogramaOportunidad
//{
//    public int IdMontoPagoCronogramas { get; set; }
//    public int IdMontoPago { get; set; }
//    public int IdPEspecifico { get; set; }
//    public int IdAlumno { get; set; }
//    public int IdPersonal_Asignado { get; set; }
//    public string CodigoBanco { get; set; }
//    public string UsuarioAprobacion { get; set; }
//}

//public class MatriculaDetalleBO
//{
//    public int IdMatricula { get; set; }
//    public int IdCursoEspecifico { get; set; }
//}


//public class MontoPagoCronogramasHistorialBO
//{
//    public string CodigoMatricula { get; set; }
//    public string Nombre { get; set; }
//    public int NroCuota { get; set; }
//    public string MonedaPago { get; set; }
//    public Decimal MontoPagado { get; set; }
//    public Decimal Mora { get; set; }
//    public DateTime? FechaVencimiento { get; set; }
//    public DateTime? Fechapago { get; set; }
//    public bool Cancelado { get; set; }
//    public DateTime? FechaMatricula { get; set; }
//}




