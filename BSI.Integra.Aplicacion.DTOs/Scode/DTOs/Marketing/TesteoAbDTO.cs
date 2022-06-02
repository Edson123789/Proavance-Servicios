using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class TesteoAbDTO
    {
        public int Id { get; set; }
        public int IdFormularioLandingPage { get; set; }
        public int IdPlantillaLandingPage { get; set; }
        public string NombrePlantilla { get; set; }
        public int Cantidad { get; set; }
        public string Nombre { get; set; }
        public int Porcentaje { get; set; }
        public string Usuario { get; set; }
        public FormularioLandingAbDTO FormularioLandingAb { get; set; }
    }
}
