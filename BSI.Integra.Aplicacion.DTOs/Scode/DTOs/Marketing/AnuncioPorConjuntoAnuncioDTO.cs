using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class AnuncioPorConjuntoAnuncioDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string IdAnuncioFacebook { get; set; }
        public int IdConjuntoAnuncio { get; set; }
        public string NombreConjuntoAnuncio { get; set; }
        public int IdCentroCosto { get; set; }
        public string NombreCentroCosto { get; set; }
        public int IdCreativoPublicidad { get; set; }
        public string EnlaceFormulario { get; set; }
        public string NombreUsuario { get; set; }
        public string UsuarioModificacion { get; set; }
        public List<int> Kpis { get; set; }

        public string IdConjuntoAnuncioFacebook { get; set; }
        public string IdCategoriaOrigen { get; set; }
        public string NombreCategoriaOrigen { get; set; }

        public DateTime FechaCreacion { get; set; }

    }
}
