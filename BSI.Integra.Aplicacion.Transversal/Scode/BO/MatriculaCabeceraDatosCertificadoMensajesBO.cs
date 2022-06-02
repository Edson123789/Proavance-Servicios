using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.Base.Classes;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class MatriculaCabeceraDatosCertificadoMensajesBO : BaseBO
    {
        public int IdMatriculaCabecera { get; set; }
        public int IdPersonalRemitente { get; set; }
        public int IdPersonalReceptor { get; set; }
        public string Mensaje { get; set; }
        public string ValorAntiguo { get; set; }
        public string ValorNuevo { get; set; }
        public bool EstadoMensaje { get; set; }
    }
}
