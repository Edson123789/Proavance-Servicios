using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{ 
    public class OportunidadCompetidoresDTO
    {
        public int Id { get; set; }
        public int IdOportunidad { get; set; }
        public string Nombre { get; set; }
        public int DuracionCronologica { get; set; }
        public int CostoNeto { get; set; }
        public int Precio { get; set; }
        public string Categoria { get; set; }
        public string Empresa { get; set; }
        public string RegionCiudad { get; set; }
        public string Moneda { get; set; }
        public int? IdCompetidorVentajaDesventaja { get; set; }
        public string ContenidoCompetidorVentajaDesventaja { get; set; }
        public int? TipoCompetidorVentajaDesventaja { get; set; }
    }
}
