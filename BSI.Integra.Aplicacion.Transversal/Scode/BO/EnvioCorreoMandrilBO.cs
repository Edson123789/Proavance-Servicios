using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.Base.Classes;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class EnvioCorreoMandrilBO:BaseBO
    {
        public int IdOportunidad { get; set; }
        public int IdOportunidadCc { get; set; }
        public int IdPersonal { get; set; }
        public int IdAlumno { get; set; }
        public int TipoAsignacion { get; set; }
        public bool EstadoEnvio { get; set; }
        public int TipoEnvio { get; set; }
        public string Asunto { get; set; }
        public DateTime? FechaEnvio { get; set; }
        public string IdMandril { get; set; }
        public int? IdMigracion { get; set; }

        //Persistencia
        private EnvioCorreoMandrilRepositorio _repEnvioCorreoMandril;

        public EnvioCorreoMandrilBO()
        {
            ActualesErrores = new Dictionary<string, List<ErrorInfo>>();
        }
        public EnvioCorreoMandrilBO(integraDBContext context)
        {
            _repEnvioCorreoMandril = new EnvioCorreoMandrilRepositorio(context);
        }
    }
}
