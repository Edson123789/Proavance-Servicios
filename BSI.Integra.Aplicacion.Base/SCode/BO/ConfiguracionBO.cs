using BSI.Integra.Aplicacion.Classes;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Base.BO
{
    public class TConfiguracion : BaseEntity //ClaseBase
    {
        private string _Codigo;
        private string _Nombre;
        private string _Valor;

        public string Codigo {
            get { return _Codigo; }
            set
            {
                _Codigo = value;
            }
        }
        public string Nombre {
            get { return _Nombre; }
            set {
                _Nombre = value;
            }
        }
        public string Valor {
            get { return _Valor; }
            set
            {
                _Valor = value;
            }
        }
        public string RowVersion { get; set; }
    }
    
}
