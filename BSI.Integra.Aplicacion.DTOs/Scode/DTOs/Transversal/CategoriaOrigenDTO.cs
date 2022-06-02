using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class CategoriaOrigenDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int IdTipoDato { get; set; }
        public int IdTipoCategoriaOrigen { get; set; }
        public int Meta { get; set; }
        public int? IdProveedorCampaniaIntegra { get; set; }
        public int? IdFormularioProcedencia { get; set; }
        public bool Considerar { get; set; }
        public string CodigoOrigen { get; set; }
        public string Total { get; set; }
        public string Usuario { get; set; }

    }
}
