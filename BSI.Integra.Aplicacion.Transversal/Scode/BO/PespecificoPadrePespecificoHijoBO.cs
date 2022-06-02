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
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Base.Classes;
using BSI.Integra.Aplicacion.Transversal.Repositorio;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class PespecificoPadrePespecificoHijoBO : BaseBO
    {
        public int PespecificoPadreId { get; set; }
        public int PespecificoHijoId { get; set; }
        public Guid? IdMigracion { get; set; }
        private PespecificoRepositorio _repPespecifico;
        private PespecificoPadrePespecificoHijoRepositorio _repPespecificoPadrePespecificoHijo;

        public PespecificoPadrePespecificoHijoBO()
        {
            ActualesErrores = new Dictionary<string, List<ErrorInfo>>();
            _repPespecifico = new PespecificoRepositorio();
            _repPespecificoPadrePespecificoHijo = new PespecificoPadrePespecificoHijoRepositorio();
        }








    }


}
