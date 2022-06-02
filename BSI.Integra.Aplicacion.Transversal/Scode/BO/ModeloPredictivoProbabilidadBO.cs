using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.Classes;
using BSI.Integra.Aplicacion.Transversal.Helper;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class ModeloPredictivoProbabilidadBO : BaseBO
    {
        public int IdModeloPredictivoTipo { get; set; }
        public int Tipo { get; set; }
        public int IdOportunidad { get; set; }
        public decimal Probabilidad { get; set; }

    }
}
