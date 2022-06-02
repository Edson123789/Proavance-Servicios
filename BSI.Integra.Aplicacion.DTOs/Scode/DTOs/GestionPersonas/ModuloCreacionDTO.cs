using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
   public  class ModuloCreacionDTO
    {
        public int IdModulo { get; set; }
        public string NombreModulo { get; set; }
        public int IdGrupo { get; set; }
        public string NombreGrupo { get; set; }
        public string URL { get; set; }
        public string Etiqueta { get; set; }
        public string Icono { get; set; }
    }

    public class UsuarioContraseñaDTO
    {
        public int PerId { get; set; }
        public string Usuario { get; set; }
        public string NuevaContrasena { get; set; }
        public int RolId { get; set; }
        public string IdIntegraAspNetUsers { get; set; }
        public string Email { get; set; }
    }


    public class ModuloCreacionAgrupadoDTO
    {
        public int IdModulo { get; set; }
        public string NombreModulo { get; set; }
        public int IdGrupo { get; set; }
        public string NombreGrupo { get; set; }
        public string URL { get; set; }
        public string Etiqueta { get; set; }
        public string Icono { get; set; }
        public int? IdModuloSistemaTipo { get; set; }
        public string NombreModuloSistemaTipo { get; set; }
    }
}