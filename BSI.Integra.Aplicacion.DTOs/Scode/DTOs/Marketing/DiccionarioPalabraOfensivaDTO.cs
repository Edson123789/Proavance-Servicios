using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs.Scode.DTOs.Marketing
{
    public class DiccionarioPalabraOfensivaDTO
    {
        public int Id { get; set; }
        public string PalabraOfensiva { get; set; }
        public string Usuario { get; set; }       
    }

    public class PalabrasOfensivasEncontradasDTO
    {
        public int Id { get; set; }
        public int IdOrigenMensaje { get; set; }
        public int IdPersonal { get; set; }
        public string DatosAsesor { get; set; }
        public string DatosVisitante { get; set; }
        public DateTime Fecha { get; set; }
        public string Mensaje { get; set; }
    }

    public class IdAsesorChatDTO
    {
        public int Id { get; set; }
        public int IdPersonal { get; set; }
    }
}
