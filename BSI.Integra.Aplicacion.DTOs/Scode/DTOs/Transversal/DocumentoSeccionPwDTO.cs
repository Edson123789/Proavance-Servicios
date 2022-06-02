using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
   public class DocumentoSeccionPwDTO
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Contenido { get; set; }
        public int IdPlantillaPw { get; set; }
        public int Posicion { get; set; }
        public int Tipo { get; set; }
        public int IdDocumentoPw { get; set; }
        public int IdSeccionPw { get; set; }
        public bool VisibleWeb { get; set; }
        public int? ZonaWeb { get; set; }
        public int? OrdenWeb { get; set; }
    }
}
