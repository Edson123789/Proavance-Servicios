using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace BSI.Integra.Aplicacion.Marketing.Validador
{
    public class ValidadorBO
    {
        public void ValidarEmail(string Email)
        {
            var pattern = ConfiguracionReglas.getConfiguracion("Validar Email", "pattern") ?? "";
            var nullable = Convert.ToBoolean(ConfiguracionReglas.getConfiguracion("Validar Email", "nullable"));
            if (Email.Equals("") && nullable) return;
            bool isEmail = Regex.IsMatch(Email, @pattern, RegexOptions.IgnoreCase);
            if (!isEmail)
            {
                var Exception = new ValidatorException("Email Inválido");
                throw Exception;
            }
        }


        public void validarLongitudCelular(int? IdCiudad, string Celular)
        {
            string IRepositorioCiudad = ConfiguracionReglas.getConfiguracion("validarLongitudCelular2", "IRepositorioCiudad");
            string RepositorioCiudad = ConfiguracionReglas.getConfiguracion("validarLongitudCelular2", "RepositorioCiudad");
            string MetodoCiudad = ConfiguracionReglas.getConfiguracion("validarLongitudCelular2", "MetodoCiudad"); //"GetAllCiudad";
            string CampoCiudad = ConfiguracionReglas.getConfiguracion("validarLongitudCelular2", "CampoCiudad"); //"IdCiudad";
            string CampoLongitud = ConfiguracionReglas.getConfiguracion("validarLongitudCelular2", "longitudCampo");

            var types = AppDomain.CurrentDomain.GetAssemblies();

            //Type interfazCiudad = types.SelectMany(s => s.GetTypes()).Where(s => s.FullName.Contains("." + IRepositorioCiudad)).FirstOrDefault();
            //Type implementCiudad = types.SelectMany(s => s.GetTypes()).Where(s => s.FullName.Contains("." + RepositorioCiudad)).FirstOrDefault();

            //var container = new UnityContainer();
            //container.RegisterType(interfazCiudad, implementCiudad, new InjectionMember[] { });

            //var resolvedCiudad = container.Resolve(interfazCiudad);
            //var metodoCiudad = resolvedCiudad.GetType().GetMethod(MetodoCiudad);

            //IEnumerable<Object> rptaCiudad = (IEnumerable<Object>)metodoCiudad.Invoke(resolvedCiudad, new object[] { });
            //var resultCiudad = rptaCiudad.Where(s => s.GetType().GetProperty(CampoCiudad).GetValue(s, null).ToString().Equals(idCiudad.ToString())).FirstOrDefault();
            //if (resultCiudad != null)
            //{
            //    System.Reflection.PropertyInfo pic = resultCiudad.GetType().GetProperty(CampoLongitud);
            //    int longitud = (int)(pic.GetValue(resultCiudad, null));

            //    if (longitud != celular.Length)
            //    {
            //        throw new ValidatorException("Celular Inválido. La longitud correcta es " + longitud.ToString());
            //    }
            //}
        }
    }
}
