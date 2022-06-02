using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class CampaniaMailingDetalleConCantidadDTO
    {
        public int Id { get; set; }
        public int Cantidad { get; set; }
    }

    public class CampaniaGeneralDetalleCantidadDTO
    {
        public int Id { get; set; }
        public int CantidadContactos { get; set; }
        public int CantidadWhatsApp { get; set; }
    }
}
