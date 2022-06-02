using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Text;

namespace BSI.Integra.Aplicacion.Base.Classes
{
    public sealed class ErrorInfo
    {
        public Codigos ErrorCode { get; set; }
        public bool EsAdvertencia { get; set; }
        public string MensajeError { get; set; }
        public string Mensaje { get; set; }

        public override string ToString()
        {
            return (string.Format("{0}: {1}, {2}",
              EsAdvertencia ? "Advertencia" : "Error",
              ObtenerDescription(ErrorCode),
              MensajeError));
        }

        public enum Codigos
        {
            [Description("Obligatorio")]
            Obligatorio = 101,
            [Description("Longitud Mínima")]
            LongitudMinima = 102,
            [Description("Rango Permitido")]
            Rango = 103,
            [Description("Colección Obligatoria")]
            ColeccionObligatoria = 104,
            [Description("Duplicado")]
            Duplicado = 105,
            [Description("Numérico")]
            Numerico = 106
           
        }

        private static string ObtenerDescription(Codigos value)
        {
            Type tipo = value.GetType();
            string name = Enum.GetName(tipo, value);
            if (name != null)
            {
                FieldInfo field = tipo.GetField(name);
                if (field != null)
                {
                    DescriptionAttribute attr =
                           Attribute.GetCustomAttribute(field,
                             typeof(DescriptionAttribute)) as DescriptionAttribute;
                    if (attr != null)
                    {
                        return attr.Description;
                    }
                }
            }
            return null;
        }

    }

}
