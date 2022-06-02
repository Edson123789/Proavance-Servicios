using System;
using System.Collections.Generic;
using System.Text;


namespace BSI.Integra.Aplicacion.DTOs
{
    public class FiltroInsertarPEspecificoDTO
    {
        public PespecificoDTO Objeto { get; set; }
        public CentroCostoDTO CentroCosto { get; set; }
        public int IdCiudad { get; set; }
        public string Usuario { get; set; }
    }
}
