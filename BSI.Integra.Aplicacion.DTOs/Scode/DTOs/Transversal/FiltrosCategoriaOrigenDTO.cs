using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class FiltrosCategoriaOrigenDTO
    {
        public List<TipoDatoDTO> filtroTipoDato { get; set; }
        public List<ProveedorCampaniaIntegraDTO> filtroProveedorCampania { get; set; }
        public List<TipoInteraccionDTO> filtroTipoInteraccion { get; set; }
        public List<ProcedenciaFormularioDTO> filtroProcedenciaformulario { get; set; }
        public List<TipoCategoriaOrigenDTO> filtroTipoCategoriaOrigen { get; set; }
        public List<TipoCategoriaOrigenDTO> filtroTipoCategoriaOrigenTodo { get; set; }
    }
}
