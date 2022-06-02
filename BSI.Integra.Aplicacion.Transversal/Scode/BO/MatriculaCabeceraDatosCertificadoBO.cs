using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.Base.Classes;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class MatriculaCabeceraDatosCertificadoBO : BaseBO
    {
        public int IdMatriculaCabecera { get; set; }
        public string Duracion { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFinal { get; set; }
        public string NombreCurso { get; set; }
        public bool EstadoCambioDatos { get; set; }
    }
}
