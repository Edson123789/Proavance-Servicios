using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;
using System.Text;
using BSI.Integra.Aplicacion.Base.Classes;
using BSI.Integra.Aplicacion.Classes;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Base.BO
{
    public class BaseBO : BaseEntity
    {
        [Column("ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public object this[string propertyName]
        {
            get { return GetType().GetProperty(propertyName).GetValue(this, null); }
            set { GetType().GetProperty(propertyName).SetValue(this, value, null); }
        }

        [JsonIgnore] public List<string> PropiedadesExcluidas = new List<string> {"item", "propiedades", "Validadores"};

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

        /// <summary>
        /// Implemetacion de Errores
        /// </summary>

        private string _error = string.Empty;
        private string _alert;
        public Dictionary<string, List<ErrorInfo>> ActualesErrores;
        //private string _usuario;
        //public bool EstadoError { get; set; }
        //public Int32 IDUsuario { get; set; }
        //public string NamePropertyName = string.Empty;
        //public DateTime FechaRegistro { get; set; }

        //public string Error
        //{
        //    get
        //    {
        //        IEnumerable<KeyValuePair<string, List<ErrorInfo>>> er = ActualesErrores.Where(w => w.Value.Count > 0);

        //        List<List<ErrorInfo>> ls = er.Select(s => s.Value).ToList();

        //        _error = string.Empty;

        //        foreach (ErrorInfo errorInfo in ls.SelectMany(infos => infos))
        //        {
        //            _error = _error + errorInfo.MensajeError + Environment.NewLine;
        //        }

        //        return _error;
        //    }
        //}

        //public String Usuario
        //{
        //    get { return _usuario; }
        //    set
        //    {
        //        if (value == string.Empty) return;
        //        _usuario = value;
        //        ClearErrorFromProperty("Usuario", ErrorInfo.Codigos.Obligatorio);
        //        //OnPropertyChanged(MethodBase.GetCurrentMethod().Name.Substring(4));
        //    }
        //}

        public bool HasAlerts
        {
            get
            {
                IEnumerable<KeyValuePair<string, List<ErrorInfo>>> er = ActualesErrores.Where(w => w.Value.Count > 0);

                List<List<ErrorInfo>> ls = er.Select(s => s.Value).ToList();

                _alert = string.Empty;


                foreach (ErrorInfo errorInfo in ls.SelectMany(infos => infos).Where(w => w.EsAdvertencia))
                {
                    _alert = _alert + errorInfo.MensajeError + Environment.NewLine;
                }


                IEnumerable<int> alertas = from a in ActualesErrores
                    from b in a.Value
                    where b.EsAdvertencia
                    select a.Value.Count;


                //return (_alert!=null);
                return (alertas.Any());
            }
        }

        public string Alert
        {
            get { return _alert; }
            set { _alert = value; }
        } 

        public IEnumerable GetErrors(string propertyName = "")
        {
            if (string.IsNullOrEmpty(propertyName))
            {
                return (ActualesErrores.Values);
            }

            MakeOrCreatePropertyErrorList(propertyName);
            return (ActualesErrores[propertyName]);
        }

        public bool HasErrors
        {
            get
            {
                IEnumerable<int> nro = from a in ActualesErrores
                    from b in a.Value
                    where b.EsAdvertencia == false
                    select a.Value.Count;

                // var nro = actualesErrores.Where(t => t.Value.Count > 0 ).Count();



                return (nro.Any());
            }
        }

        public BaseBO()
        {
            ActualesErrores = new Dictionary<string, List<ErrorInfo>>();
        }

        private void MakeOrCreatePropertyErrorList(string propertyName)
        {
            if (!ActualesErrores.ContainsKey(propertyName))
            {
                ActualesErrores[propertyName] = new List<ErrorInfo>();
            }
        }


        public void ClearErrorFromProperty(string property, ErrorInfo.Codigos errorCode)
        {
            MakeOrCreatePropertyErrorList(property);

            ErrorInfo error =
                ActualesErrores[property].SingleOrDefault(e => e.ErrorCode == errorCode);

            if (error != null)
            {
                ActualesErrores[property].Remove(error);

                if (ActualesErrores[property].Count() == 0)
                {
                    ActualesErrores.Remove(property);
                }

                //FireErrorsChanged(property);
            }
        }

        protected void AddErrorForProperty(string property, ErrorInfo error)
        {
            MakeOrCreatePropertyErrorList(property);

            if (ActualesErrores[property].SingleOrDefault(e => e.ErrorCode == error.ErrorCode) == null)
            {
                ActualesErrores[property].Add(error);
                // _error = _error + error.ErrorCode + ' ' + error.MensajeError + "\r\n";
                _error = _error + error.MensajeError + "\r\n";
                //FireErrorsChanged(property);
            }
        }

        public void ValidateRequiredStringProperty(string entidad, ErrorInfo.Codigos errorCode, string property,
            string propertyReadable)
        {
            AddErrorForProperty(
                property, new ErrorInfo
                {
                    ErrorCode = errorCode,
                    MensajeError = string.Format("{0} es obligatorio - {1}", propertyReadable, entidad),
                    EsAdvertencia = false,
                    Mensaje = propertyReadable
                });
        }

        public void ValidateRequiredCollectionProperty(string entidad, ErrorInfo.Codigos errorCode, string property,
            string propertyReadable, int elements)
        {
            if (elements == 0)
            {
                AddErrorForProperty(
                    property, new ErrorInfo
                    {
                        ErrorCode = errorCode,
                        MensajeError =
                            string.Format("No ha registrado ningún {0} - {1}", propertyReadable, entidad),
                        EsAdvertencia = false,
                        Mensaje = propertyReadable
                    });
            }
        }

        public void ValidarRangoProperty(string entidad, ErrorInfo.Codigos errorCode, string property,
            string propertyReadable, int valor, int max, int min = 0)
        {
            if (valor >= max || valor <= min)
            {
                AddErrorForProperty(
                    property, new ErrorInfo
                    {
                        ErrorCode = errorCode,
                        MensajeError =
                            string.Format("{0} debe ser mayor de {1} y menor de {2} - {3}", property,
                                min.ToString(CultureInfo.InvariantCulture), max.ToString(CultureInfo.InvariantCulture),
                                entidad),
                        EsAdvertencia = false,
                        Mensaje = propertyReadable
                    });
            }
            else
            {
                //ClearErrorFromProperty(property, errorCode);
            }
        }

        public void ValidarValorMayorCeroProperty(string entidad, ErrorInfo.Codigos errorCode, string property,
            string propertyReadable, decimal valor, int min = 0)
        {
            if (valor == min)
            {
                AddErrorForProperty(
                    property, new ErrorInfo
                    {
                        ErrorCode = errorCode,
                        MensajeError =
                            string.Format("{0} debe ser mayor de {1} - {2}", propertyReadable,
                                min.ToString(CultureInfo.InvariantCulture), entidad),
                        EsAdvertencia = false,
                        Mensaje = propertyReadable
                    });
            }
            else
            {
                //ClearErrorFromProperty(property, errorCode);
            }
        }


        public void ValidaLongitudStringProperty(string entidad, ErrorInfo.Codigos errorCode, string property,
            string propertyReadable, int longitud, string valor)
        {
            if (valor.Length + 1 <= longitud)
            {
                AddErrorForProperty(
                    property, new ErrorInfo
                    {
                        ErrorCode = errorCode,
                        MensajeError =
                            string.Format("{0} debe tener {1} caracteres - {2}", property, longitud,
                                entidad),
                        EsAdvertencia = false
                    });
            }
            else
            {
                //ClearErrorFromProperty(property, errorCode);
            }
        }

    }
}
