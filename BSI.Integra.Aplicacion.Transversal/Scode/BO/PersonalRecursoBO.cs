using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.Base.Classes;
using BSI.Integra.Aplicacion.Classes;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.SCode.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class PersonalRecursoBO:BaseBO
    {
        public string NombrePersonal { get; set; }
        public string ApellidosPersonal { get; set; }
        public string DescripcionPersonal { get; set; }
        public string UrlfotoPersonal { get; set; }
        public int CostoHorario { get; set; }
        public int IdMoneda { get; set; }
        public int Productividad { get; set; }
        public bool? EsDisponible { get; set; }
        public int IdTipoDisponibilidadPersonal { get; set; }
    }
}
