using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public partial class CentroCostoCompuestoDTO
    {
        public int? IdArea { get; set; }
        public int? IdSubArea { get; set; }
        public string IdPgeneral { get; set; }
        public string Nombre { get; set; }
        public string Codigo { get; set; }
        public string IdAreaCc { get; set; }
        public int? Ismtotales { get; set; }
        public int? Icpftotales { get; set; }
        //Adicional
        public string CTroncal { get; set; }
        public int? IdCiudad { get; set; }
        public int? Total { get; set; }
        public int? IdArea1 { get; set; }
        public int? IdSubNivel { get; set; }
        public bool? Estado { get; set; }
        public int? Id { get; set; }
        //Auditoria
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }

    }
}
