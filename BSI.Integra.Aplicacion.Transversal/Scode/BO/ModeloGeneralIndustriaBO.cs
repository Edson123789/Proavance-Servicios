﻿using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class ModeloGeneralIndustriaBO : BaseBO
    {
        public int IdModeloGeneral { get; set; }
        public int IdIndustria { get; set; }
        public string Nombre { get; set; }
        public decimal Valor { get; set; }
        public Guid? IdMigracion { get; set; }
    }
}