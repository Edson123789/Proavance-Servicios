using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.Base.Classes;
using BSI.Integra.Aplicacion.Servicios.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Comercial.BO
{
    public class MensajeTextoBO : BaseBO
    {
        public int IdOportunidad { get; set; }
        public string IdMatriculaCabecera { get; set; }
        public string Mensaje
        {
            get {
                //Agregar cuando se configure la tablas del Portal Web Debido a que obtiene el CodigoMatricula del Portal Web
                //if (_AspUsers != null)
                //{
                //    if (rptAlumno.IdCodigoPais.Value == 57)
                //    {
                //        codigoMatricula = rptMatricula.CodigoMatricula.Replace("A", "");
                //        mensaje = "BSG Institute: Codigo de referencia " + codigoMatricula + " - Usuario: " + _AspUsers.UserName + ", Clave: " + _AspUsers.Password;
                //    }
                //    else
                //    {
                //        codigoMatricula = rptMatricula.CodigoMatricula;
                //        mensaje = "BSG Institute: Codigo Matricula " + codigoMatricula + " - Usuario: " + _AspUsers.UserName + ", Clave: " + _AspUsers.Password;
                //    }
                //}
                if (origenAgenda) {
                    string codigoMatricula = string.Empty;
                    if (CodigoPais == 57)
                    {
                        codigoMatricula = IdMatriculaCabecera.Replace("A", "");
                        mensaje = "BSG Institute: \n Codigo de referencia " + codigoMatricula + "\n Usuario: " +UserName + "\n Clave: " + Clave ;
                        //Encoding encode = System.Text.Encoding.GetEncoding(mensaje);
                    }
                    else
                    {
                        codigoMatricula = IdMatriculaCabecera;
                        mensaje = "BSG Institute: \n Codigo Matricula " + codigoMatricula + "\n Usuario: " + UserName + "\n Clave: " + Clave;
                    }
                    
                }
                return mensaje;
            }
            set { mensaje = value; }
            
        }
        public string Numero
        {
            get { return numero; }
            set
            {
                bool banderaNumero = false;
                string aux_numero = string.Empty;
                if (!string.IsNullOrEmpty(value))
                {
                    if (CodigoPais == 51)
                    {
                        numero = "+51" + value;
                    }
                    else
                    {
                        var codigoPais = value.Substring(0, 2);
                        try
                        {
                            int codigoNumero = Convert.ToInt32(codigoPais);
                            if (codigoNumero == 0)
                            {
                                banderaNumero = true; //si los digito iniciales son 00
                            }
                            else
                            {
                                banderaNumero = false; //si los digitos iniciales no son 00 y es otro numero
                            }
                        }
                        catch (Exception)
                        {
                            banderaNumero = false;
                        }

                        if (banderaNumero)
                        {
                            aux_numero = value.Remove(0, 2);
                        }
                        else
                        {
                            aux_numero = value;
                        }
                        numero = "+" + aux_numero;
                    }
                }
                else
                {
                    ValidateRequiredStringProperty(this.GetType().Name, ErrorInfo.Codigos.Obligatorio, this.GetType().GetProperty("Numero").Name,
                                                  "Celular Alumno");
                }
            }
        }
        public int CodigoPais { get; set; }
        public string IdSeguimientoTwilio { get; set; }

        private string numero {get; set;}
        private string mensaje { get; set; }
        //otros atributos
        public bool origenAgenda { get; set; } = false;
        public string UserName { get; set; }
        public string Clave { get; set; }

        private TMK_TwilioServiceImpl _twiliioServicio;
        public MensajeTextoBO()
        {
            ActualesErrores = new Dictionary<string, List<Base.Classes.ErrorInfo>>();
            _twiliioServicio = new TMK_TwilioServiceImpl();
        }
        /// <summary>
        /// Envia un mensaje de Texto al Celular del Alumno con el Codigo de Matricula atravez de la plataform TwilioClient
        /// </summary>
        /// <returns></returns>
        public string EnviarMensaje()
        {
            return _twiliioServicio.EnviarMensajeTexto(this.Mensaje, this.Numero);
            
        }
    }
      
    
}
