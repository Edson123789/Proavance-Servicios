using BSI.Integra.Aplicacion.Classes;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

//Todo en el BO no partir la logica

    /// <summary>
    /// 
    /// 
    /// 1 - FUNCIONE
    /// 2 - NO FUNCIONA
    /// 3 - ERRORES
    /// </summary>

namespace BSI.Integra.Aplicacion.Base.BO
{
    public class LogsBO : BaseEntity
    {
        private string _tipo;
        private int _idPadre;
        private string _exception;
        private string _mensaje;
        
        [Required]
        public DateTime Fecha { get; set; }
        public string Ip { get; set; }
        public string Usuario { get; set; }
        public string Maquina { get; set; }
        public string Ruta { get; set; }
        public string Parametros { get; set; }
        [MaxLength(4000)]
        public string Mensaje {
            get { return _mensaje; }
            set {
                _mensaje = value;
            }
        }
        [MaxLength(2500)]
        public string Excepcion {
            get { return _exception; }
            set {
                _exception = value;
            }
        }
        public string Tipo {
            get { return _tipo; }
            set {
                _tipo = value;
            }
        }
        public int IdPadre {
            get { return _idPadre; }
            set {
                _idPadre = value;

            }
        }
    }

    

}
