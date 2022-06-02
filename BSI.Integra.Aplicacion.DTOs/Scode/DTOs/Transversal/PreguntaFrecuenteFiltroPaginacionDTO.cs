using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class PreguntaFrecuenteFiltroPaginacionDTO
    {
        public Int64 rowIndex { get; set; }
        public int total { get; set; }
        public int Id { get; set; }
        public string Pregunta { get; set; }
        public string Respuesta { get; set; }
        public int Tipo { get; set; }
        public int IdSeccion { get; set; }
        public string NombreSeccion { get; set; }
        public List<int> listaAreas { get; set; }
        public List<int> listaSubAreas { get; set; }
        public List<int?> listaPGenerales { get; set; }
        public List<int> listaTipos { get; set; }
    }
}
