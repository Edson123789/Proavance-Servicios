using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class RankingDTO
    {
        public int IdAsesor { get; set; }
        public int? IdGrupo { get; set; }
        public string NombreGrupo { get; set; }
        public int Pais { get; set; }
        public double Meta { get; set; }
        public int Inscritos { get; set; }
        public int Cerradas { get; set; }
        public double TC { get; set; }
        public int RankingGeneral { get; set; }
        public string TipoAsesor { get; set; }
        public int RankingTipoAsesor { get; set; }
        public double TCByMeta { get; set; }
        public int RankingTotalSeniorPeru { get; set; }
        public int RankingTotalSeniorColombia { get; set; }
        public int RankingTotalSeniorBolivia { get; set; }
        public int RankingTotalJuniorPeru { get; set; }
        public int RankingTotalJuniorColombia { get; set; }
        public int RankingTotalJuniorBolivia { get; set; }
        public int RankeadoTotal { get; set; }
        public string Premio { get; set; }
        public string NombreAsesor { get; set; }
        public int PrimerosPuestos { get; set; }
        public float IngresoMeta { get; set; }
        public float IngresoReal { get; set; }
        public float IRbyIM { get; set; }
    }
}
