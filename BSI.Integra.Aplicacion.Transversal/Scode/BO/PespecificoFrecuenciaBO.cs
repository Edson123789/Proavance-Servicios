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
using BSI.Integra.Aplicacion.Base.Classes;
using BSI.Integra.Aplicacion.Transversal.Repositorio;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class PespecificoFrecuenciaBO:BaseBO
    {
        public int? IdPespecifico { get; set; }
        public DateTime FechaInicio { get; set; }
        public int Frecuencia { get; set; }
        public int NroSesiones { get; set; }
        public int? IdFrecuencia { get; set; }
        public Guid? IdMigracion { get; set; }
        public DateTime? FechaFin { get; set; }
        public List<PespecificoFrecuenciaDetalleBO> pEspecificoFrecuenciaDetalle { get; set; }

        private PespecificoFrecuenciaRepositorio _repPespecificoFrecuencia;

        public PespecificoFrecuenciaBO()
        {
            ActualesErrores = new Dictionary<string, List<ErrorInfo>>();
        }

        public PespecificoFrecuenciaBO(integraDBContext contexto)
        {
            ActualesErrores = new Dictionary<string, List<ErrorInfo>>();
            _repPespecificoFrecuencia = new PespecificoFrecuenciaRepositorio(contexto);
        }
        //public bool VerificarFrecuenciaByPEspecifico (int idEspecifico)
        //{
        //    return _pespecificoFrecuenciaRepository.Existe(idEspecifico);
        //}
    }
}
