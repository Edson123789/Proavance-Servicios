using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class PersonalRecursoDTO
    {
        public int Id { get; set; }
        public string NombrePersonal { get; set; }
        public string ApellidosPersonal { get; set; }
        public string DescripcionPersonal { get; set; }
        public string UrlfotoPersonal { get; set; }
        public int CostoHorario { get; set; }
        public int IdMoneda { get; set; }
        public int Productividad { get; set; }
        public bool? EsDisponible { get; set; }
        public int IdTipoDisponibilidadPersonal { get; set; }
        public string Usuario { get; set; }

        public List<PersonalRecursoHabilidadDTO> PersonalRecursoHabilidad { get; set; }
    }
}
