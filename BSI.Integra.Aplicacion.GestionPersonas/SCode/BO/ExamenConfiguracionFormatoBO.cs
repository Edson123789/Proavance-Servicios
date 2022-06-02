using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.GestionPersonas.BO
{
    public class ExamenConfiguracionFormatoBO : BaseBO
    {
        public bool Activo { get; set; }
        public string ColorTextoTitulo { get; set; }
        public int? TamanioTextoTitulo { get; set; }
        public string ColorFondoTitulo { get; set; }
        public string TipoLetraTitulo { get; set; }
        public string ColorTextoEnunciado { get; set; }
        public int? TamanioTextoEnunciado { get; set; }
        public string ColorFondoEnunciado { get; set; }
        public string TipoLetraEnunciado { get; set; }
        public string ColorTextoRespuesta { get; set; }
        public int? TamanioTextoRespuesta { get; set; }
        public string ColorFondoRespuesta { get; set; }
        public string TipoLetraRespuesta { get; set; }
        public int? IdMigracion { get; set; }
    }
}
