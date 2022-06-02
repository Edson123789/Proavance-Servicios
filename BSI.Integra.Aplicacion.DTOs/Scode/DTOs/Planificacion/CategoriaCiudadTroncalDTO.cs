using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class CategoriaCiudadTroncalDTO
    {
        public int Id { get; set; }
        public int IdCategoriaPrograma {get; set;}
        public int IdRegionCiudad { get; set;}
        public string TroncalCompleto { get; set;}
        public string UsuarioCreacion { get; set;}
        public string UsuarioModificacion { get; set; }
    }
}
