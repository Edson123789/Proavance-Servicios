using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class PersonalDatosAgendaDTO
    {
        public int Id { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Rol { get; set; }
        public string TipoPersonal { get; set; }
        public string Email { get; set; }
        public string AreaAbrev { get; set; }
        public string Anexo { get; set; }
        public int? IdJefe { get; set; }
        public string Central { get; set; }
        public string Anexo3Cx { get; set; }
        public string Id3Cx { get; set; }
        public string Password3Cx { get; set; }
        public string Dominio { get; set; }
        public Nullable<int> UsuarioAsterisk { get; set; }
        public string ContrasenaAsterisk { get; set; }
    }
}
