using AutoMapper.Configuration.Conventions;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs.Scode.DTOs.GestionPersonas
{
    public class PersonalInformacionMedicaFormularioDTO
    {
        public int Id { get; set; }
        public int IdPersonal { get; set; }
        public int? IdTipoSangre { get; set; }
        public string TipoSangre { get; set; }
        public string Alergia { get; set; }
        public string Precaucion { get; set; }
        public bool Estado { get; set; }
    }
}
