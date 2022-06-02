using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.GestionPersonas.BO
{
	public class RegistroMarcadorFechaBO : BaseBO
	{
        public int IdCiudad { get; set; }
        public int IdPersonal { get; set; }
        public string Pin { get; set; }
        public DateTime Fecha { get; set; }
        public TimeSpan? M1 { get; set; }
        public TimeSpan? M2 { get; set; }
        public TimeSpan? M3 { get; set; }
        public TimeSpan? M4 { get; set; }
        public TimeSpan? M5 { get; set; }
        public TimeSpan? M6 { get; set; }
    }
}
