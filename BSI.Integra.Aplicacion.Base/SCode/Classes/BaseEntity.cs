using BSI.Integra.Aplicacion.Base.Classes;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;

namespace BSI.Integra.Aplicacion.Classes
{
    public class BaseEntity
    {
        [Column("ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }

        public object this[string propertyName]
        {
            get { return GetType().GetProperty(propertyName).GetValue(this, null); }
            set { GetType().GetProperty(propertyName).SetValue(this, value, null); }
        }
        [JsonIgnore]
        public List<string> PropiedadesExcluidas = new List<string> { "item", "propiedades", "Validadores" };
        [JsonIgnore]
        public List<string> Propiedades
        {
            get
            {

                var resultado = new List<string>();

                foreach (var item in GetType().GetProperties())
                {
                    if (!PropiedadesExcluidas.Contains(item.Name, StringComparer.OrdinalIgnoreCase))
                    {
                        resultado.Add(item.Name);
                    }

                }

                return resultado;

            }
        }
    }
}
