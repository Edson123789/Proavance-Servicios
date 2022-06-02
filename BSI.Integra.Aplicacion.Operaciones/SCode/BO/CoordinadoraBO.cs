using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Operaciones.BO
{
    public partial class CoordinadoraBO : BaseBO
    {        
        public int IdPersonal { get; set; }
        public string NombreResumido { get; set; }
        public string Correo { get; set; }
        public string AliasCorreo { get; set; }
        public string Clave { get; set; }
        public string Password { get; set; }
        public string Firma { get; set; }
        public string Usuario { get; set; }
        public int? Anexo { get; set; }
        public string Modalidad { get; set; }
        public bool? Genero { get; set; }
        public int IdSede { get; set; }
        public string Skype { get; set; }
        public string Ciudad { get; set; }
        public string Htmlnumero { get; set; }
        public string Htmlhorario { get; set; }
        public string Iniciales { get; set; }
        public int? IdMigracion { get; set; }
    }
}
