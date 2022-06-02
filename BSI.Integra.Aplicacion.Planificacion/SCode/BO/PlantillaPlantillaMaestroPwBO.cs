﻿using BSI.Integra.Aplicacion.Classes;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.SCode.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Aplicacion.Base.BO;

namespace BSI.Integra.Aplicacion.Planificacion.BO
{
    public class PlantillaPlantillaMaestroPwBO : BaseBO
    {
        public int IdPlantillaPw { get; set; }
        public int IdSeccionMaestraPw { get; set; }
        public string Contenido { get; set; }
        public Guid? IdMigracion { get; set; }

    }
}
