﻿using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Planificacion.BO
{
	public partial class TamanioEmpresaBO : BaseBO
	{
		public string Nombre { get; set; }
		public string Descripcion { get; set; }
		public Guid? IdMigracion { get; set; }
	}
}
