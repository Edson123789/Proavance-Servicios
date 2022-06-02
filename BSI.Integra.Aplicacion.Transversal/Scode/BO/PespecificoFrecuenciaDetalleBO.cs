using BSI.Integra.Aplicacion.Classes;
using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.SCode.Repository;
using System.Linq;
using System.Transactions;
using BSI.Integra.Aplicacion.Base.Classes;
using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.Transversal.Repositorio;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class PespecificoFrecuenciaDetalleBO : BaseBO
    {
        public int? IdPespecificoFrecuencia { get; set; }
        public byte DiaSemana { get; set; }
        public TimeSpan HoraDia { get; set; }
        public decimal Duracion { get; set; }
        public Guid? IdMigracion { get; set; }
        private PespecificoFrecuenciaDetalleRepositorio _repPespecificoFrecuenciaDetalle;
        public PespecificoFrecuenciaDetalleBO()
        {
            ActualesErrores = new Dictionary<string, List<ErrorInfo>>();
        }
        public PespecificoFrecuenciaDetalleBO(integraDBContext contexto)
        {
            ActualesErrores = new Dictionary<string, List<ErrorInfo>>();
        }        
    }
}
