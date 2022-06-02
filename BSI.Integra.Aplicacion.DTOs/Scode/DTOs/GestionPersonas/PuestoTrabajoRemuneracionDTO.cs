using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs.Scode.DTOs.GestionPersonas
{
    public class PuestoTrabajoRemuneracionDTO
    {
        public int Id { get; set; }
        public int IdPuestoTrabajo { get; set; }
        public string PuestoTrabajo { get; set; }
        public int IdPersonalAreaTrabajo { get; set; }
        public string PersonalAreaTrabajo { get; set; }
        public int IdPais { get; set; }
        public string Pais { get; set; }
        public int IdCategoria { get; set; }
        public string Categoria { get; set; }
        public string Usuario { get; set; }
        public List<PuestoTrabajoRemuneracionDetalleDTO> ListaPuestoTrabajoRemuneracionDetalle { get; set;}
    }
}
