using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ReporteDescargaMaterialDTO
    {
        public string AreaCapacitacion { get; set; }
        public string SubArea { get; set; }
        public string Categoria { get; set; }
        public string NombreMaterial { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Email { get; set; }
        public string Celular { get; set; }
        public string Pais { get; set; }
        public string Region { get; set; }
        public string Cargo { get; set; }
        public string AreaFormacion { get; set; }
        public string AreaTrabajo { get; set; }
        public string Industria { get; set; }
        public string FechaRegistro { get; set; }
    }

    public class ReporteDescargaMaterialComboDTO
    {
        public List<FiltroDTO> ListaAreaCapacitacion { get; set; }
        public List<FiltroDTO> ListaPais { get; set; }
    }

    public class ReporteDescargaMaterialFiltroDTO
    {
        public List<int> AreaCapacitacion{ get; set; }
        public List<int> SubAreas { get; set; }
        public List<int> Categoria { get; set; }
        public List<int> Articulos { get; set; }
        public List<int> Pais { get; set; }

        public DateTime FechaInicial { get; set; }
        public DateTime FechaFin { get; set; }

    }

    public class ListaMatarial
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
    }

    public class ReporteMaterialFiltroDTO
    {
        public List<int> TipoArticulo { get; set; }
        public List<int> Area { get; set; }
        public List<int> SubArea { get; set; }

    }

}
