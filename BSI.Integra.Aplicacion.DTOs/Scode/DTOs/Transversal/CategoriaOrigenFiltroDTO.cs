using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class CategoriaOrigenFiltroDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
    }

    public class CategoriaOrigenAdwordsDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
    }

    public class CentroCostoCampaniaDTO
    {
        public int IdCentroCosto { get; set; }
        public string CentroCosto { get; set; }
        public string Codigo { get; set; }
        public string Campania { get; set; }
        public string IdConjuntoAnuncio { get; set; }
    }
    public class CategoriaOrigeCombonDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int IdTipoDato { get; set; }
        public int IdTipoCategoriaOrigen { get; set; }
    }
}
