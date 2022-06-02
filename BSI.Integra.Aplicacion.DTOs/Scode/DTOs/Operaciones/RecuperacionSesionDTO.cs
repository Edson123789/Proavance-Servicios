using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class RecuperacionSesionDTO
    {
        public int? IdRecuperacionSesion { get; set; }
        public int IdMatriculaCabecera { get; set; }
        public int IdPespecificoSesion { get; set; }
        public bool Recupera { get; set; }
        public string Usuario { get; set; }
    }

    public class CancelarWebinarDTO
    {
        public int IdPEspespecifico { get; set; }
        public int IdPEspecificoSesion { get; set; }
        public string Usuario { get; set; }
        public string ComentarioCancelacion { get; set; }
        public bool Confirmo { get; set; }

    }

    public class ConfirmarWebinarDTO
    {
        public int IdPEspecificoSesion { get; set; }
        public string Usuario { get; set; }
        public bool Confirmo { get; set; }

    }
}
