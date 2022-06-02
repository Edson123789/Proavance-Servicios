using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public partial class ActividadCabeceraBO : BaseBO
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public DateTime? FechaCreacion2 { get; set; }
        public int DuracionEstimada { get; set; }
        public bool ReproManual { get; set; }
        public bool ReproAutomatica { get; set; }
        public int Idplantilla { get; set; }
        public int IdActividadBase { get; set; }
        public DateTime? FechaModificacion2 { get; set; }
        public bool ValidaLlamada { get; set; }
        public int? IdPlantillaSpeech { get; set; }
        public int NumeroMaximoLlamadas { get; set; }
        public Guid? IdMigracion { get; set; }

        public int? IdConjuntoLista { get; set; }
        public int? IdFrecuencia { get; set; }
        public DateTime? FechaInicioActividad { get; set; }
        public byte? DiaFrecuenciaMensual { get; set; }
        public bool? EsRepetitivo { get; set; }
        public TimeSpan? HoraInicio { get; set; }
        public TimeSpan? HoraFin { get; set; }
        public byte? CantidadIntevaloTiempo { get; set; }
        public int? IdTiempoIntervalo { get; set; }
        public bool? Activo { get; set; }
        public DateTime? FechaFinActividad { get; set; }
        public int IdPersonalAreaTrabajo { get; set; }
        public int? IdFacebookCampanha { get; set; }
        public int? IdFacebookCuentaPublicitaria { get; set; }
        public bool? EsEnvioMasivo { get; set; }

        private readonly ActividadCabeceraRepositorio _repActividadCabecera;

        public ActividadCabeceraBO()
        {
        }

        public ActividadCabeceraBO(integraDBContext integraDBContext)
        {
            _repActividadCabecera = new ActividadCabeceraRepositorio(integraDBContext);
        }

        public bool ActualizarOportunidadConjuntoListaResultado(int idConjuntoListaDetalle)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    _repActividadCabecera.ActualizarOportunidadConjuntoListaResultado(idConjuntoListaDetalle);

                    scope.Complete();
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}