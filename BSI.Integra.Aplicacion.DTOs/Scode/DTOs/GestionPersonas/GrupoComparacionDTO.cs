using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class GrupoComparacionDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public List<int> ListaPuestoTrabajo { get; set; }
        public List<int> ListaSedeTrabajo { get; set; }
        public List<int> ListaPostulante { get; set; }
        public string Usuario { get; set; }
    }
    public class GrupoComparacionProcesoSeleccionDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int IdPuestoTrabajo { get; set; }
        public int IdSedeTrabajo { get; set; }
        public int? IdPostulante { get; set; }
    }
}
