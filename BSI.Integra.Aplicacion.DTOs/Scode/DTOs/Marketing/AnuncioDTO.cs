using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class AnuncioDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string NombreCentroCosto { get; set; }
        public string IdAnuncioFacebook { get; set; }
        public int IdConjuntoAnuncio { get; set; }
        public int IdCentroCosto { get; set; }
        public int IdCreativoPublicidad { get; set; }
        public string EnlaceFormulario { get; set; }
        public int? NroAnuncioCorrelativo { get; set; }
        public List<int> Kpis { get; set; }
    }
}
