using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class CambioCentroCostoDTO
    {
        public int IdOportunidadV4  { get; set; }
        public Guid IdOportunidadV3  { get; set; }
        public Guid? IdOportunidadPadreV3  { get; set; }
        public int? IdOportunidadPadreV4  { get; set; }
        public string IdMatriculaCabeceraV3  { get; set; }
        public int IdMatriculaCabeceraV4  { get; set; }
        public int IdCronogramaPagoV4  { get; set; }
        public string IdCronogramaPagoV3 { get; set; }
        public int IdCentroCostoV3 { get; set; }
        public int IdCentroCostoV4 { get; set; }
        public int IdPespecificoV3 { get; set; }
        public int IdPespecificoV4 { get; set; }
        public string Usuario { get; set; }
    }
}
