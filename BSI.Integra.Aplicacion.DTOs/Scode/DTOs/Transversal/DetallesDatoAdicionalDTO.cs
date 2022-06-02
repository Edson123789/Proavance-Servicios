using BSI.Integra.Aplicacion.DTOs.Scode.DTOs.Transversal;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class DetallesDatoAdicionalDTO
    {
        public int Id { get; set; }
        public int IdMatriculaCabeceraBeneficios { get; set; }
        public int IdMatriculaCabecera { get; set; }
        public string CodigoMatricula { get; set; }
        public List<DatoAdicionalPWDTO> DatosAdicionales { get; set; }
    }
}
