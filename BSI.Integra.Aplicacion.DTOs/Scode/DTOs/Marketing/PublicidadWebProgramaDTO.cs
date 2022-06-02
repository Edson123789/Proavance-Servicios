using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class PublicidadWebProgramaDTO
    {
        public int Id { get; set; }
        public int? IdPublicidadWeb { get; set; }
        public int IdPgeneral { get; set; }
        public string Nombre { get; set; }
        public int OrdenPrograma { get; set; }
        public bool ModificarInformacion { get; set; }
        public bool Duracion { get; set; }
        public bool Inicios { get; set; }
        public bool Precios { get; set; }
    }
}
