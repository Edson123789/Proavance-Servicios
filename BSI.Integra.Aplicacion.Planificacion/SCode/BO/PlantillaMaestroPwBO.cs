using BSI.Integra.Aplicacion.Classes;
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
    public class PlantillaMaestroPwBO : BaseBO
    {
        public string Nombre { get; set; }
        public int Repeticion { get; set; }
        public Guid? IdMigracion { get; set; }
        public List<SeccionMaestraPwBO> SeccionMaestra { get; set; }

    }
}
