using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class PlantillaPlantillaMaestroPwFiltroDTO
    {
        public int Id { get; set; }
        public int IdPlantillaPw { get; set; }
        public string Nombre { get; set; }
        public int IdSeccionMaestraPw { get; set; }
        public string Contenido { get; set; }
        public string Descripcion { get; set; }
        public string Titulo { get; set; }
        public int Posicion { get; set; }
        public int Tipo { get; set; }
    }
}
