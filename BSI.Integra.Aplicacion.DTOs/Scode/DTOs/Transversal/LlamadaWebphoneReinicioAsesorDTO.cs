using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class LlamadaWebphoneReinicioAsesorDTO
    {
        public int Id { get; set; }
        public int IdPersonal { get; set; }
        public bool AplicaReinicio { get; set; }
        public string Usuario { get; set; }
    }
}
