using Google.Api.Ads.AdWords.v201809;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class PalabraClaveVolumenDTO
    {
        public string PalabraClave { get; set; }
        public MonthlySearchVolume[] PromedioPorMes { get; set; }
    }
}
