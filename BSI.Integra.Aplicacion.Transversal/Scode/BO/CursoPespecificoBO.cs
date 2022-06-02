using System;
using System.Collections.Generic;
using System.Text;
using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.Base.Classes;
using BSI.Integra.Persistencia.Models;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class CursoPespecificoBO : BaseBO
    {
        public int IdPespecifico { get; set; }
        public string Nombre { get; set; }
        public int Duracion { get; set; }
        public int Orden { get; set; }
        public int? IdExpositor { get; set; }
        public int? NroSesiones { get; set; }
        public int? IdMigracion { get; set; }

        public CursoPespecificoBO()
        {
            ActualesErrores = new Dictionary<string, List<ErrorInfo>>();
        }

        public CursoPespecificoBO(integraDBContext contexto)
        {
            ActualesErrores = new Dictionary<string, List<ErrorInfo>>();
        }
    }
    
}
