using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs.Scode.DTOs.Transversal
{
    public class InsertarTipoBotonConfiguracionDTO
    {
        public int Id { get; set; }
        public string NombreBoton { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFin { get; set; }          
        public int Orden { get; set; }
        public string Usuario { get; set; }
    }
}
