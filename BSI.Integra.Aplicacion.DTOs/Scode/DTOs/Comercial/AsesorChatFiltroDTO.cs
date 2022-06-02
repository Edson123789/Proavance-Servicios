using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class AsesorChatFiltroDTO
    {
        public int Id { get; set; }
        public int? IdPersonal { get; set; }
        public string NombrePersonal { get; set; }
        public int? IdArea { get; set; }
        public string NombreArea { get; set; }
        public int? IdSubArea { get; set; }
        public string NombreSubArea { get; set; }
        public int? IdPais { get; set; }
        public string NombrePais { get; set; }
        public int? IdPGeneral { get; set; }
        public string NombrePGeneral { get; set; }
    }

    public class AsesorChatConsolidadoDTO {
        public int Id { get; set; }
        public int? IdPersonal { get; set; }
        public string NombrePersonal { get; set; }
        public List<AsesorChatDetalleDTO> ListaPais { get; set; }
        public List<AsesorChatDetalleDTO> ListaArea { get; set; }
        public List<AsesorChatDetalleDTO> ListaSubArea { get; set; }
        public List<AsesorChatDetalleDTO> ListaProgramaGeneral { get; set; }
    }

    public class AsesorChatConsolidadoVisualizarDTO
    {
        public int Id { get; set; }
        public int? IdPersonal { get; set; }
        public string NombrePersonal { get; set; }
        public string ListaPais { get; set; }
        public string ListaArea { get; set; }
        public string ListaSubArea { get; set; }
        public string ListaProgramaGeneral { get; set; }
    }


    public class AsesorChatDetalleDTO {
        public int? Id { get; set; }
        public string Nombre { get; set; }
    }
}
