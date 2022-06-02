using System;
using System.Collections.Generic;
using System.Text;
using BSI.Integra.Aplicacion.Base.BO;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class CriterioDocBO : BaseBO
    {
        public bool ModalidadPresencial { get; set; }
        public bool ModalidadAonline { get; set; }
        public bool ModalidadOnline { get; set; }
        public string Nombre { get; set; }
        public int? IdMigracion { get; set; }
    }
}
