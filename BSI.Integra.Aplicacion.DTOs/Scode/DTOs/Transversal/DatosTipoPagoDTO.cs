using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class DatosTipoPagoDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int Cuotas { get; set; }
        public bool Suscripciones { get; set; }
        public bool PorDefecto { get; set; }
        public string Usuario { get; set; }
        public int TipoPago { get; set; }

        public List<TipoPagoCategoriaDTO> TipoPagoCategoria;
    }
}
