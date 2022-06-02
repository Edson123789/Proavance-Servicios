using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class CambioMonedaCronogramaModificadoDTO
    {
        public List<CambioMonedaCronogramaFinalModificadoDTO> ListaCronograma { get; set; }
        public string CodigoMatricula { get; set; }
        public string UsuarioNombre { get; set; }
        public int IdPersonal { get; set; }
        public int? IdMatriculaCabecera { get; set; }
    }
}
