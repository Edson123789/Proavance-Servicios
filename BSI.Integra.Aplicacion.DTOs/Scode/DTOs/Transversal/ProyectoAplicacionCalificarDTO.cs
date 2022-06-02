using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs.Transversal
{
    public class ProyectoAplicacionCalificarDTO
    {
        public int IdPgeneralProyectoAplicacion { get; set; }
        public int IdPgeneralProyectoAplicacionEstado { get; set; }
        public decimal Nota { get; set; }
        public string Comentarios { get; set; }
        public string Usuario { get; set; }
    }
}
