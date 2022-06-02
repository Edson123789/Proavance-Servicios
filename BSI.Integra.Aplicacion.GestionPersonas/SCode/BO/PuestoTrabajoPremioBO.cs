using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.GestionPersonas.BO
{
    public class PuestoTrabajoPremioBO : BaseBO
    {
        public int IdPuestoTrabajo { get; set; }
        public string Denominacion { get; set; }
        public int IdFrecuencia { get; set; }
        public string Premio { get; set; }
        public string Reconocimiento { get; set; }
        public decimal MontoMeta { get; set; }
        public int IdMoneda { get; set; }
        public int? IdMigracion { get; set; }
    }
}
