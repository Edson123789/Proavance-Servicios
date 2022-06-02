using BSI.Integra.Aplicacion.Classes;
using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.SCode.Repository;
using System.Linq;
using System.Transactions;
using BSI.Integra.Aplicacion.Base.BO;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class FeriadoBO : BaseBO
    {
        public int? Tipo { get; set; }
        public DateTime Dia { get; set; }
        public string Motivo { get; set; }
        public int Frecuencia { get; set; }
        public int IdTroncalCiudad { get; set; }
        public Guid? IdMigracion { get; set; }

        public FeriadoBO()
        {

        }

    }
}
