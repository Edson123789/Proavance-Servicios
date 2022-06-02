using System;
using System.Collections.Generic;
using System.Text;
using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.Base.Classes;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class EnvioCorreoMasivoBO:BaseBO
    {
        public int? IdActividadDetalleInicial { get; set; }
        public int? IdOportunidad { get; set; }
        public int? IdCentroCostoOportunidad { get; set; }
        public int IdPersonal { get; set; }
        public int? IdAlumno { get; set; }
        public int IdTipoEnvioCorreo { get; set; }
        public int? IdMandrilTipoEnvio { get; set; }
        public int? IdPlantilla { get; set; }
        public int? IdMigracion { get; set; }

        //Persistencia
        private EnvioCorreoMasivoRepositorio _repEnvioCorreoMasivo;


        public EnvioCorreoMasivoBO()
        {
            ActualesErrores = new Dictionary<string, List<ErrorInfo>>();
        }
        public EnvioCorreoMasivoBO(integraDBContext context)
        {
            _repEnvioCorreoMasivo = new EnvioCorreoMasivoRepositorio(context);
        }
    }
}
