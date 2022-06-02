using BSI.Integra.Aplicacion.Classes;
using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.SCode.Repository;
using System.Linq;
using BSI.Integra.Aplicacion.Base.BO;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class AmbienteBO : BaseBO
    {
        public string Nombre { get; set; }
        public int IdLocacion { get; set; }
        public int IdTipoAmbiente { get; set; }
        public int Capacidad { get; set; }
        public bool Virtual { get; set; }
        public Guid? IdMigracion { get; set; }

        public AmbienteBO()
        {
        }
        

    }

}
