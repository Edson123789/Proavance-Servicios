using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;

namespace BSI.Integra.Aplicacion.Transversal.Validador
{
    public class ValidadorBO
    {
        public void ValidarEmail(string Email)
        {
            //var pattern = ConfiguracionReglas.getConfiguracion("Validar Email", "pattern") ?? "";
            //var nullable = Convert.ToBoolean(ConfiguracionReglas.getConfiguracion("Validar Email", "nullable"));
            //if (Email.Equals("") && nullable) return;
            //bool isEmail = Regex.IsMatch(Email, @pattern, RegexOptions.IgnoreCase);
            if (!IsEmailValid(Email))
            {
                var Exception = new ValidatorException("Email Inválido");
                throw Exception;
            }
        }

        public bool IsEmailValid(string emailAddress)
        {
            try
            {
                MailAddress m = new MailAddress(emailAddress);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }


		public void ValidarLongitudCelular(int? idPais, string celular, integraDBContext contexto)
        {
            CiudadRepositorio _repCiudad = new CiudadRepositorio(contexto);

            if (!_repCiudad.LongitudCelularPorPaisCorrecta(idPais, celular))
            {
                var Exception = new ValidatorException("Longitud celular Inválido");
                throw Exception;
            }
        }
    }
}
