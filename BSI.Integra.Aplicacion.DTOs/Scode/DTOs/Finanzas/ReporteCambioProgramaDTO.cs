using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ReporteCambioProgramaDTO
    {
        public DateTime Fecha { get; set; }       
        public string Alumno { get; set; }
        public string CodigoMatricula { get; set; }
        public string CentroCostoAnterior { get; set; }        
        public string CentroCostoNuevo { get; set; }

    }

}