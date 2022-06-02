using BSI.Integra.Aplicacion.Classes;
using System;
using System.Collections.Generic;
using System.Text;
using BSI.Integra.Aplicacion.Base.BO;

namespace BSI.Integra.Aplicacion.Marketing.BO
{
	public class AdwordsLogBO : BaseBO
	{
		public string Mensaje { get; set; }
		public DateTime FechaCreacion { get; set; }
		public DateTime FechaModificacion { get; set; }
        public Guid? IdMigracion { get; set; }
    }
}
