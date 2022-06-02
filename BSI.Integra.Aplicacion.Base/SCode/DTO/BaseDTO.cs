using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTO
{
    public class BaseDTO
    {
        public BaseDTO()
        {

        }

        public int Id { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public bool? Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }


        [JsonIgnore]
        public object this[string propertyName]
        {
            get { return GetType().GetProperty(propertyName).GetValue(this, null); }
            set { GetType().GetProperty(propertyName).SetValue(this, value, null); }
        }

    }
}
