using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Operaciones.BO
{
    public class ProyectoAplicacionDocumentoSeccionPwBO : BaseBO
    {
        public int IdDocumentoSeccionPw { get; set; }
        public int IdMatriculaCabecera { get; set; }
        public string Valor { get; set; }
        public int IdPlantillaPw { get; set; }
        public int IdDocumentoPw { get; set; }
        public int IdProyectoAplicacionEntregaVersionPw { get; set; }
        public DateTime FechaCalificacion { get; set; }
    }
}
