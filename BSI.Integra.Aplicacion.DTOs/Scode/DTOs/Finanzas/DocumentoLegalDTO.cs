using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class DocumentoLegalV2DTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int IdPais { get; set; }
        public string Pais { get; set; }
        public string Url { get; set; }
        public int Area { get; set; }
        public List<int> Areas { get; set; }
        public List<int> Paises { get; set; }
        public List<DocumentoLegalPaisDTO> PaisesBD { get; set; }
        public string Roles { get; set; }
        public bool? VisualizarAgenda { get; set; }
        public bool? DescargarAgenda { get; set; }
        public string Usuario { get; set; }
        public byte[] DocumentoByte { get; set; }
    }
    public class DocumentoLegalPaisDTO
    {
        public int Id { get; set; }
        public int IdDocumentoLegal { get; set; }
        public int IdPais { get; set; }
    }
    public class DocumentoLegalDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int IdPais { get; set; }
        public string Pais { get; set; }
        public string Url { get; set; }
        public int Area { get; set; }
        public List<int> Areas { get; set; }
        public List<int> Paises { get; set; }
        public string Roles { get; set; }
        public bool? VisualizarAgenda { get; set; }
        public bool? DescargarAgenda { get; set; }
        public string Usuario { get; set; }
        public byte[] DocumentoByte { get; set; }
    }
}
